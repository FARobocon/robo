#region Copyright & License
// Copyright 2009 Takehiko YOSHIDA  (http://www.chihayafuru.jp/etrobo/)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace NxtLogger
{
    using System;
    using MissionInterface;

    internal class LogMessege
    {
        // SPP(Bluetooth)のパケット長定義
        private const ushort PacketHeaderLen = 2;
        private const ushort PacketPayloadLen = 32;
        private const ushort PacketLen = PacketHeaderLen + PacketPayloadLen;

        private int byteNo = 0;     // パケット先頭からの番号
        // 生パケット格納配列
        private byte[] packetHeader = new byte[PacketHeaderLen];     // ヘッダー部
        private byte[] packetPayload = new byte[PacketPayloadLen];   // ペイロード部

        private uint? offTick;    // 時刻オフセット（Nullable型）
        private uint  relTick;    // 相対時刻（ログ開始時刻からの相対時刻）

        // ログデータが１パケット（１行）分
        // 出来上がったことを通知するためのデリゲート
        private AppendMessegeDelegate dlg;

        /// <summary>
        /// LogMessegeコンストラクタ
        /// </summary>
        /// <param name="dlg"></param>
        public LogMessege(AppendMessegeDelegate dlg)
        {
            this.dlg = dlg;
        }

        public RobotInput InputParam 
        { 
            get; 
            private set; 
        }

        public void Append(byte dat)
        {
            // パケットヘッダー部
            if (this.byteNo < PacketHeaderLen)
            {
                // 順送りでパケットヘッダー配列へ保存
                this.packetHeader[this.byteNo++] = dat;

                if (this.byteNo == PacketHeaderLen)
                {
                    // パケットヘッダー（パケットサイズ）のチェック
                    // NXTから送信されるパケットサイズにはヘッダの２バイト分は含まれない
                    var len = BitConverter.ToUInt16(this.packetHeader, 0);

                    if (len != PacketPayloadLen)
                    {
                        // パケット仕様： ヘッダー ＝ ペイロードサイズ
                        // 想定したヘッダー値でなければ１バイト分を読み捨てる
                        this.packetHeader[0] = this.packetHeader[1];
                        this.byteNo = 1;
                    }
                }
            }
            else if (this.byteNo < PacketLen)
            {
                // パケットペイロード(ヘッダーを除いた本体部)

                // 順送りでパケットペイロード配列へ保存
                this.packetPayload[this.byteNo++ - PacketHeaderLen] = dat;

                // １パケット分のデータサイズを取り出し終えたとき
                if (this.byteNo == PacketLen)
                {
                    RobotInput robotInput = new RobotInput();
                    // パケットをフィールドに変換
                    robotInput.SysTick = BitConverter.ToUInt32(this.packetPayload, 0);
                    robotInput.DataLeft = (sbyte)this.packetPayload[4];
                    robotInput.DataRight = (sbyte)this.packetPayload[5];
                    robotInput.Batt = BitConverter.ToUInt16(this.packetPayload, 6);
                    robotInput.MotorCnt0 = BitConverter.ToInt32(this.packetPayload, 8);
                    robotInput.MotorCnt1 = BitConverter.ToInt32(this.packetPayload, 12);
                    robotInput.MotorCnt2 = BitConverter.ToInt32(this.packetPayload, 16);
                    robotInput.SensorAdc0 = BitConverter.ToInt16(this.packetPayload, 20);
                    robotInput.SensorAdc1 = BitConverter.ToInt16(this.packetPayload, 22);
                    robotInput.SensorAdc2 = BitConverter.ToInt16(this.packetPayload, 24);
                    robotInput.SensorAdc3 = BitConverter.ToInt16(this.packetPayload, 26);
                    robotInput.I2c = BitConverter.ToInt32(this.packetPayload, 28);

                    this.InputParam = robotInput;

                    // オフセット時間が未初期化(null)ならば
                    if (this.offTick == null)
                    {
                        // オフセット時間（ログ開始時のシステム時刻）セット
                        this.offTick = robotInput.SysTick;
                    }

                    // 相対時間（ログ開始時からの時刻）計算
                    if (robotInput.SysTick >= this.offTick)
                    {
                        // 通常の場合
                        this.relTick = robotInput.SysTick - (uint)this.offTick;
                    }
                    else
                    {
                        // システム時刻が最大値を越えて一周した場合
                        this.relTick = robotInput.SysTick + uint.MaxValue - (uint)this.offTick;
                    }

                    // デリゲートを介してログデータ追加メソッドを呼び出し
                    this.dlg.Invoke();

                    this.byteNo = 0;     // バイト番号を先頭に戻す
                }
            }
            else    
            {
                // this.byteNo >= PacketLenは設計の想定外
                this.byteNo = 0;
            }
        }
    }
}

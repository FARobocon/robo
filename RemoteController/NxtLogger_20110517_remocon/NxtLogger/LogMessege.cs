#region Copyright & License
//
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
//
#endregion

namespace NxtLogger
{
    using System;
    using MissionInterface;

    class LogMessege
    {
        private int byteNo = 0;     // パケット先頭からの番号

        // SPP(Bluetooth)のパケット長定義
        private const UInt16 PacketHeaderLen = 2;
        private const UInt16 PacketPayloadLen = 32;
        private const UInt16 PacketLen = PacketHeaderLen + PacketPayloadLen;

        // 生パケット格納配列
        private Byte[] packetHeader = new Byte[PacketHeaderLen];     // ヘッダー部
        private Byte[] packetPayload = new Byte[PacketPayloadLen];   // ペイロード部

        private UInt32? offTick;    // 時刻オフセット（Nullable型）
        private UInt32  relTick;    // 相対時刻（ログ開始時刻からの相対時刻）

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

        public void Append(Byte dat)
        {
            // パケットヘッダー部
            if (byteNo < PacketHeaderLen)
            {
                // 順送りでパケットヘッダー配列へ保存
                packetHeader[byteNo++] = dat;

                if (byteNo == PacketHeaderLen)
                {
                    // パケットヘッダー（パケットサイズ）のチェック
                    // NXTから送信されるパケットサイズにはヘッダの２バイト分は含まれない
                    UInt16 len = BitConverter.ToUInt16(packetHeader, 0);

                    if (len != PacketPayloadLen)
                    {
                        // パケット仕様： ヘッダー ＝ ペイロードサイズ
                        // 想定したヘッダー値でなければ１バイト分を読み捨てる
                        packetHeader[0] = packetHeader[1];
                        byteNo = 1;
                    }
                }
            }
            // パケットペイロード（ヘッダーを除いた本体部）
            else if (byteNo < PacketLen)
            {
                // 順送りでパケットペイロード配列へ保存
                packetPayload[byteNo++ - PacketHeaderLen] = dat;

                // １パケット分のデータサイズを取り出し終えたとき
                if (byteNo == PacketLen)
                {
                    RobotInput robotInput = new RobotInput();
                    // パケットをフィールドに変換
                    robotInput.SysTick = BitConverter.ToUInt32(packetPayload, 0);
                    robotInput.DataLeft = (SByte)packetPayload[4];
                    robotInput.DataRight = (SByte)packetPayload[5];
                    robotInput.Batt = BitConverter.ToUInt16(packetPayload, 6);
                    robotInput.MotorCnt0 = BitConverter.ToInt32(packetPayload, 8);
                    robotInput.MotorCnt1 = BitConverter.ToInt32(packetPayload, 12);
                    robotInput.MotorCnt2 = BitConverter.ToInt32(packetPayload, 16);
                    robotInput.SensorAdc0 = BitConverter.ToInt16(packetPayload, 20);
                    robotInput.SensorAdc1 = BitConverter.ToInt16(packetPayload, 22);
                    robotInput.SensorAdc2 = BitConverter.ToInt16(packetPayload, 24);
                    robotInput.SensorAdc3 = BitConverter.ToInt16(packetPayload, 26);
                    robotInput.I2c = BitConverter.ToInt32(packetPayload, 28);

                    this.InputParam = robotInput;

                    // オフセット時間が未初期化(null)ならば
                    if (offTick == null)
                    {
                        // オフセット時間（ログ開始時のシステム時刻）セット
                        offTick = robotInput.SysTick;
                    }

                    // 相対時間（ログ開始時からの時刻）計算
                    if (robotInput.SysTick >= offTick)
                    {
                        // 通常の場合
                        relTick = robotInput.SysTick - (UInt32)offTick;
                    }
                    else
                    {
                        // システム時刻が最大値を越えて一周した場合
                        relTick = robotInput.SysTick + UInt32.MaxValue - (UInt32)offTick;
                    }

                    // デリゲートを介してログデータ追加メソッドを呼び出し
                    this.dlg.Invoke();

                    byteNo = 0;     // バイト番号を先頭に戻す
                }
            }
            else    // byteNo >= PacketLenは設計の想定外
            {
                byteNo = 0;
            }
        }
    }
}

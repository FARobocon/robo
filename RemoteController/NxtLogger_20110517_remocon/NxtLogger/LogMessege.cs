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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxtLogger
{
    class LogMessege : Missions.RobotInput
    {
        private int byteNo = 0;     // パケット先頭からの番号

        // SPP(Bluetooth)のパケット長定義
        private const UInt16 PacketHeaderLen = 2;
        private const UInt16 PacketPayloadLen = 32;
        private const UInt16 PacketLen = PacketHeaderLen + PacketPayloadLen;

        // 生パケット格納配列
        private Byte[] packetHeader = new Byte[PacketHeaderLen];     // ヘッダー部
        private Byte[] packetPayload = new Byte[PacketPayloadLen];   // ペイロード部

        // ログデータ
        private UInt32  sysTick;    // システム時刻
        private SByte   dataLeft;   // データ左
        private SByte   dataRight;  // データ右
        private UInt16  batt;       // バッテリーレベル
        private Int32   motorCnt0;  // モーターカウンタ０
        private Int32   motorCnt1;  // モーターカウンタ１
        private Int32   motorCnt2;  // モーターカウンタ２
        private Int16   sensorAdc0; // A/Dセンサー０
        private Int16   sensorAdc1; // A/Dセンサー１
        private Int16   sensorAdc2; // A/Dセンサー２
        private Int16   sensorAdc3; // A/Dセンサー３
        private Int32   i2c;        // I2Cセンサー

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



        /// <summary>
        /// SysTickアクセサ
        /// </summary>
        public UInt32 SysTick
        {
            get
            {
                return this.sysTick;
            }
        }

        /// <summary>
        /// relTickアクセサ
        /// </summary>
        public UInt32 RelTick
        {
            get
            {
                return this.relTick;
            }
        }

        /// <summary>
        /// DataLeftアクセサ
        /// </summary>
        public SByte DataLeft
        {
            get
            {
                return this.dataLeft;
            }
        }

        /// <summary>
        /// DataRightアクセサ
        /// </summary>
        public SByte DataRight
        {
            get
            {
                return this.dataRight;
            }
        }

        /// <summary>
        /// battアクセサ
        /// </summary>
        public UInt16 Batt
        {
            get
            {
                return this.batt;
            }
        }

        /// <summary>
        /// motorCnt0アクセサ
        /// </summary>
        public Int32 MotorCnt0
        {
            get
            {
                return this.motorCnt0;
            }
        }

        /// <summary>
        /// motorCnt1アクセサ
        /// </summary>
        public Int32 MotorCnt1
        {
            get
            {
                return this.motorCnt1;
            }
        }

        /// <summary>
        /// motorCnt2アクセサ
        /// </summary>
        public Int32 MotorCnt2
        {
            get
            {
                return this.motorCnt2;
            }
        }

        /// <summary>
        /// sensorAdc0アクセサ
        /// </summary>
        public Int16 SensorAdc0
        {
            get
            {
                return this.sensorAdc0;
            }
        }

        /// <summary>
        /// sensorAdc1アクセサ
        /// </summary>
        public Int16 SensorAdc1
        {
            get
            {
                return this.sensorAdc1;
            }
        }

        /// <summary>
        /// sensorAdc2アクセサ
        /// </summary>
        public Int16 SensorAdc2
        {
            get
            {
                return this.sensorAdc2;
            }
        }

        /// <summary>
        /// sensorAdc3アクセサ
        /// </summary>
        public Int16 SensorAdc3
        {
            get
            {
                return this.sensorAdc3;
            }
        }

        /// <summary>
        /// i2cアクセサ
        /// </summary>
        public Int32 I2c
        {
            get
            {
                return this.i2c;
            }
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
                    // パケットをフィールドに変換
                    this.sysTick = BitConverter.ToUInt32(packetPayload, 0);
                    this.dataLeft = (SByte)packetPayload[4];
                    this.dataRight = (SByte)packetPayload[5];
                    this.batt = BitConverter.ToUInt16(packetPayload, 6);
                    this.motorCnt0 = BitConverter.ToInt32(packetPayload, 8);
                    this.motorCnt1 = BitConverter.ToInt32(packetPayload, 12);
                    this.motorCnt2 = BitConverter.ToInt32(packetPayload, 16);
                    this.sensorAdc0 = BitConverter.ToInt16(packetPayload, 20);
                    this.sensorAdc1 = BitConverter.ToInt16(packetPayload, 22);
                    this.sensorAdc2 = BitConverter.ToInt16(packetPayload, 24);
                    this.sensorAdc3 = BitConverter.ToInt16(packetPayload, 26);
                    this.i2c = BitConverter.ToInt32(packetPayload, 28);


                    // オフセット時間が未初期化(null)ならば
                    if (offTick == null)
                    {
                        // オフセット時間（ログ開始時のシステム時刻）セット
                        offTick = sysTick;
                    }

                    // 相対時間（ログ開始時からの時刻）計算
                    if (sysTick >= offTick)
                    {
                        // 通常の場合
                        relTick = sysTick - (UInt32)offTick;
                    }
                    else
                    {
                        // システム時刻が最大値を越えて一周した場合
                        relTick = sysTick + UInt32.MaxValue - (UInt32)offTick;
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

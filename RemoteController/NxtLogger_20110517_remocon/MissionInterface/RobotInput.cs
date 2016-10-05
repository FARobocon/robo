namespace MissionInterface
{
    using System;

    public struct RobotInput
    {
        /// <summary>
        /// SysTickアクセサ
        /// </summary>
        public UInt32 SysTick
        {
            get;
            set;
        }

        /// <summary>
        /// relTickアクセサ
        /// </summary>
        public UInt32 RelTick
        {
            get;
            set;
        }

        /// <summary>
        /// DataLeftアクセサ
        /// </summary>
        public SByte DataLeft
        {
            get;
            set;
        }

        /// <summary>
        /// DataRightアクセサ
        /// </summary>
        public SByte DataRight
        {
            get;
            set;
        }

        /// <summary>
        /// battアクセサ
        /// </summary>
        public UInt16 Batt
        {
            get;
            set;
        }

        /// <summary>
        /// しっぽモーターアクセサ
        /// </summary>
        public Int32 MotorCnt0
        {
            get;
            set;
        }

        /// <summary>
        /// 左モーターアクセサ
        /// </summary>
        public Int32 MotorCnt1
        {
            get;
            set;
        }

        /// <summary>
        /// 右モーターアクセサ
        /// </summary>
        public Int32 MotorCnt2
        {
            get;
            set;
        }

        /// <summary>
        /// 光センサアクセサ
        /// </summary>
        public Int16 SensorAdc0
        {
            get;
            set;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        public Int16 SensorAdc1
        {
            get;
            set;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        public Int16 SensorAdc2
        {
            get;
            set;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        public Int16 SensorAdc3
        {
            get;
            set;
        }

        /// <summary>
        /// i2cアクセサ
        /// </summary>
        public Int32 I2c
        {
            get;
            set;
        }

    }
}

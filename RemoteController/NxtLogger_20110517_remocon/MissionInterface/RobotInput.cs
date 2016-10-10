namespace MissionInterface
{
    using System;

    public struct RobotInput
    {
        /// <summary>
        /// SysTickアクセサ
        /// </summary>
        public uint SysTick
        {
            get;
            set;
        }

        /// <summary>
        /// relTickアクセサ
        /// </summary>
        public uint RelTick
        {
            get;
            set;
        }

        /// <summary>
        /// DataLeftアクセサ
        /// </summary>
        public sbyte DataLeft
        {
            get;
            set;
        }

        /// <summary>
        /// DataRightアクセサ
        /// </summary>
        public sbyte DataRight
        {
            get;
            set;
        }

        /// <summary>
        /// battアクセサ
        /// </summary>
        public ushort Batt
        {
            get;
            set;
        }

        /// <summary>
        /// しっぽモーターアクセサ
        /// </summary>
        public int MotorCnt0
        {
            get;
            set;
        }

        /// <summary>
        /// 左モーターアクセサ
        /// </summary>
        public int MotorCnt1
        {
            get;
            set;
        }

        /// <summary>
        /// 右モーターアクセサ
        /// </summary>
        public int MotorCnt2
        {
            get;
            set;
        }

        /// <summary>
        /// 光センサアクセサ
        /// </summary>
        public short SensorAdc0
        {
            get;
            set;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        public short SensorAdc1
        {
            get;
            set;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        public short SensorAdc2
        {
            get;
            set;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        public short SensorAdc3
        {
            get;
            set;
        }

        /// <summary>
        /// i2cアクセサ
        /// </summary>
        public int I2c
        {
            get;
            set;
        }

    }
}

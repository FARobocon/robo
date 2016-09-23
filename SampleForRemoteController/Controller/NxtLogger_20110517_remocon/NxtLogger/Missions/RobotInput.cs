using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxtLogger.Missions
{
    public interface RobotInput
    {
        /// <summary>
        /// SysTickアクセサ
        /// </summary>
        UInt32 SysTick
        {
            get;
        }

        /// <summary>
        /// relTickアクセサ
        /// </summary>
        UInt32 RelTick
        {
            get;
        }

        /// <summary>
        /// DataLeftアクセサ
        /// </summary>
        SByte DataLeft
        {
            get;
        }

        /// <summary>
        /// DataRightアクセサ
        /// </summary>
        SByte DataRight
        {
            get;
        }

        /// <summary>
        /// battアクセサ
        /// </summary>
        UInt16 Batt
        {
            get;
        }

        /// <summary>
        /// しっぽモーターアクセサ
        /// </summary>
        Int32 MotorCnt0
        {
            get;
        }

        /// <summary>
        /// 左モーターアクセサ
        /// </summary>
        Int32 MotorCnt1
        {
            get;
        }

        /// <summary>
        /// 右モーターアクセサ
        /// </summary>
        Int32 MotorCnt2
        {
            get;
        }

        /// <summary>
        /// 光センサアクセサ
        /// </summary>
        Int16 SensorAdc0
        {
            get;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        Int16 SensorAdc1
        {
            get;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        Int16 SensorAdc2
        {
            get;
        }

        /// <summary>
        /// ジャイロセンサアクセサ
        /// </summary>
        Int16 SensorAdc3
        {
            get;
        }

        /// <summary>
        /// i2cアクセサ
        /// </summary>
        Int32 I2c
        {
            get;
        }

    }
}

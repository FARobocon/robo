using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxtLogger.Missions
{
    /// <summary>
    /// 回転方向と速度を計算するインタフェース
    /// </summary>
    interface DirectionFinder
    {
        Byte makeLMotor(Byte speed);
        Byte makeRMotor(Byte speed);
        Byte direction();
    }

    class Up : DirectionFinder
    {
        public Byte makeLMotor(Byte speed)
        {
            return speed;
        }
        public Byte makeRMotor(Byte speed)
        {
            return speed;
        }
        public Byte direction() { return (byte)1; }
    }

    class UpRight : DirectionFinder
    {

        public Byte makeLMotor(Byte speed)
        {
            return speed;
        }
        public Byte makeRMotor(Byte speed)
        {
            return (Byte)(speed/2);
        }
        public Byte direction() { return (byte)1; }
    }

    class Right : DirectionFinder
    {
        public Byte makeLMotor(Byte speed)
        {
            return speed;
        }
        public Byte makeRMotor(Byte speed)
        {
            return (Byte)(0);
        }
        public Byte direction() { return (byte)1; }
    }

    class Left : DirectionFinder
    {
        public Byte makeLMotor(Byte speed)
        {
            return (Byte)(0);
        }
        public Byte makeRMotor(Byte speed)
        {
            return speed;
        }
        public Byte direction() { return (byte)1; }
    }

    class UpLeft : DirectionFinder
    {
        public Byte makeLMotor(Byte speed)
        {
            return (Byte)(speed / 2);
        }
        public Byte makeRMotor(Byte speed)
        {
            return speed;
        }
        public Byte direction() { return (byte)1; }
    }

    class Zero : DirectionFinder
    {
        public Byte makeLMotor(Byte speed)
        {
            return (Byte)(0);
        }
        public Byte makeRMotor(Byte speed)
        {
            return (Byte)(0);
        }
        public Byte direction() { return (byte)1; }
    }

    class Down : DirectionFinder
    {
        Up up = new Up();
        public Byte makeLMotor(Byte speed) { return up.makeLMotor(speed); }
        public Byte makeRMotor(Byte speed) { return up.makeRMotor(speed); }
        public Byte direction() { return (byte)0; }
    }

    class DownRight : DirectionFinder
    {
        UpRight up = new UpRight();
        public Byte makeLMotor(Byte speed) { return up.makeLMotor(speed); }
        public Byte makeRMotor(Byte speed) { return up.makeRMotor(speed); }
        public Byte direction() { return (byte)0; }
    }

    class DownLeft : DirectionFinder
    {
        UpLeft up = new UpLeft();
        public Byte makeLMotor(Byte speed) { return up.makeLMotor(speed); }
        public Byte makeRMotor(Byte speed) { return up.makeRMotor(speed); }
        public Byte direction() { return (byte)0; }
    }

    class DirectionFinderCreater
    {
        public static DirectionFinder create(Byte dir)
        {
            switch(dir)
            {
                case (0x80 | 0x40): return new UpRight();
                case (0x20 | 0x40): return new DownRight();
                case (0x80 | 0x10): return new UpLeft();
                case (0x20 | 0x10): return new DownLeft();

                case 0x80: return new Up();
                case 0x20: return new Down();
                case 0x40: return new Right();
                case 0x10: return new Left();
            }
            return new Zero();
        }
    }

}

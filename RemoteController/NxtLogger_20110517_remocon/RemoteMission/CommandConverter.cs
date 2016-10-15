namespace RemoteMission
{
    using System;
    using System.Text;
    using MissionInterface;

    public class CommandConverter
    {
        [Flags]
        public enum Direction
        {
            None = 0x00,
            Straight = 0x01,
            Back = 0x02,
            Left = 0x04,
            Right = 0x08,
            StraightRight = 0x09,
            StraightLeft = 0x05,
            BackRight = 0x0a,
            BackLeft = 0x06,
        }

        public static RobotOutput StartCommand
        {
            get
            {
                return new RobotOutput("<START>");
            }
        }

        public static RobotOutput StopCommand
        {
            get
            {
                return new RobotOutput("<STOP>");
            }
        }

        /// <summary>
        /// 速度と方向をコマンドインタフェースに変換する
        /// </summary>
        /// <param name="speed">速度(0 to 100)</param>
        /// <param name="direction">方向</param>
        /// <returns>RobotOutput</returns>
        public RobotOutput Convert(int speed, Direction direction)
        {
            if (direction == Direction.Straight)
            {
                //直進
                return new RobotOutput(this.SetSpeed(speed,"F"));
            }
            else if (direction == Direction.Back)
            {
                //後退
                return new RobotOutput(this.SetSpeed(speed, "B"));
            }
            else if (direction == Direction.Right || direction == Direction.StraightRight || direction == Direction.BackRight)
            {
                //右旋回
                return new RobotOutput(this.SetSpeed(speed, "R"));
            }
            else if (direction == Direction.Left || direction == Direction.StraightLeft || direction == Direction.BackLeft)
            {
                //左旋回
                return new RobotOutput(this.SetSpeed(speed, "L"));
            }
            return RobotOutput.InvalidRobotOutput;
        }


        private string SetSpeed(int speed, string dir)
        {
            StringBuilder builder = new StringBuilder("<");
            builder.Append(dir);
            if (speed<10)
            {
                builder.Append("00");
            }
            else if (speed < 100)
            {
                builder.Append("0");
            }
            builder.Append(speed);
            builder.Append(">");

            return builder.ToString();
        }

        
    }
}

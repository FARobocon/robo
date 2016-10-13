namespace RemoteMission
{
    using System;
    using MissionInterface;

    public class CommandConverter
    {
        public const string NoneCommand = "<NONE>";

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
                var bytes = System.Text.Encoding.ASCII.GetBytes("<START>");
                return new RobotOutput(bytes);
            }
        }

        public static RobotOutput StopCommand
        {
            get
            {
                var bytes = System.Text.Encoding.ASCII.GetBytes("<STOP>");
                return new RobotOutput(bytes);
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
            var robotOutput = new RobotOutput();

            robotOutput[0] = Parse('<');
            if (direction == Direction.Straight)
            {
                //直進
                robotOutput[1] = Parse('F');
                this.SetSpeed(speed, robotOutput);
            }
            else if (direction == Direction.Back)
            {
                //後退
                robotOutput[1] = Parse('B');
                this.SetSpeed(speed, robotOutput);
            }
            else if (direction == Direction.Right || direction == Direction.StraightRight || direction == Direction.BackRight)
            {
                //右旋回
                robotOutput[1] = Parse('R');
                this.SetSpeed(speed, robotOutput);
            }
            else if (direction == Direction.Left || direction == Direction.StraightLeft || direction == Direction.BackLeft)
            {
                //左旋回
                robotOutput[1] = Parse('L');
                this.SetSpeed(speed, robotOutput);
            }
            else
            {
                return new RobotOutput( System.Text.Encoding.ASCII.GetBytes(NoneCommand));
            }

            robotOutput[5] = Parse('>');

            return robotOutput;
        }

        private static byte Parse(char c)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(new char[] { c });
            return bytes[0];
        }

        private void SetSpeed(int speed, RobotOutput robotOutput)
        {
            byte[] outputSpeed = BitConverter.GetBytes(speed);

            if (speed<10)
            {
                robotOutput[2] = Parse('0');
                robotOutput[3] = Parse('0');
                robotOutput[4] = Parse(speed.ToString()[0]);
            }
            else if (speed < 100)
            {
                robotOutput[2] = Parse('0');
                var str = speed.ToString();
                robotOutput[3] = Parse(str[0]);
                robotOutput[4] = Parse(str[1]);
            }
            else
            {
                var str = speed.ToString();
                robotOutput[2] = Parse(str[0]);
                robotOutput[3] = Parse(str[1]);
                robotOutput[4] = Parse(str[2]);
            }
        }

        
    }
}

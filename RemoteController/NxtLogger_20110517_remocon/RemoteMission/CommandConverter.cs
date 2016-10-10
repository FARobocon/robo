namespace RemoteMission
{
    using System;
    using MissionInterface;

    public class CommandConverter
    {
        private RobotOutput robotOutput;

        [Flags]
        public enum Direction
        {
            None = 0x00,
            Straight = 0x01,
            Back = 0x02,
            Left = 0x04,
            Right = 0x08,
        }
        
        /// <summary>
        /// 速度と方向をコマンドインタフェースに変換する
        /// </summary>
        /// <param name="speed">速度(0 to 100)</param>
        /// <param name="direction">方向</param>
        /// <returns>RobotOutput</returns>
        public RobotOutput Convert(int speed, Direction direction)
        {
            this.robotOutput = new RobotOutput();

            this.robotOutput[0] = Parse('<');
            if (direction == Direction.Straight)
            {
                //直進
                this.robotOutput[1] = Parse('F');
                this.SetSpeed(speed);
            }
            else if (direction == Direction.Back)
            {
                //後退
                this.robotOutput[1] = Parse('B');
                this.SetSpeed(speed);
            }
            else if (direction == Direction.Right)
            {
                //右旋回
                this.robotOutput[1] = Parse('R');
                this.SetSpeed(speed);
            }
            else if (direction == Direction.Left)
            {
                //左旋回
                this.robotOutput[1] = Parse('L');
                this.SetSpeed(speed);
            }
            else
            {
                //停止
                this.robotOutput[1] = Parse('S');
                this.robotOutput[2] = Parse('T');
                this.robotOutput[3] = Parse('O');
                this.robotOutput[4] = Parse('P');
            }
            this.robotOutput[5] = Parse('>');

            return this.robotOutput;
        }

        private static byte Parse(char c)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(new char[] { c });
            return bytes[0];
        }

        private void SetSpeed(int speed)
        {
            byte[] outputSpeed = BitConverter.GetBytes(speed);

            if (speed<10)
            {
                this.robotOutput[2] = Parse('0');
                this.robotOutput[3] = Parse('0');
                this.robotOutput[4] = Parse(speed.ToString()[0]);
            }
            else if (speed < 100)
            {
                this.robotOutput[2] = Parse('0');
                var str = speed.ToString();
                this.robotOutput[3] = Parse(str[0]);
                this.robotOutput[4] = Parse(str[1]);
            }
            else
            {
                var str = speed.ToString();
                this.robotOutput[2] = Parse(str[0]);
                this.robotOutput[3] = Parse(str[1]);
                this.robotOutput[4] = Parse(str[2]);
            }
        }

        
    }
}

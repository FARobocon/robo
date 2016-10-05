namespace RemoteMission
{
    using System;
    using MissionInterface;

    public class CommandConverter
    {
        private RobotOutput robotOutput;

        /// <summary>
        /// 以下定数はリファクタリングしたうえ、列挙体（ビットフラグス）に変更してください
        /// </summary>
        public readonly static byte Straight = 0x80;
        public readonly static byte Back = 0x20;
        public readonly static byte Right = 0x40;
        public readonly static byte Left = 0x10;
        public readonly static byte ReleaseStraight = 0x7F;
        public readonly static byte ReleaseBack = 0xBF;
        public readonly static byte ReleaseRight= 0xDF;
        public readonly static byte ReleaseLeft = 0xEF;
        
        /// <summary>
        /// 速度と方向をコマンドインタフェースに変換する
        /// </summary>
        /// <param name="speed">速度(0 to 100)</param>
        /// <param name="direction">方向</param>
        /// <returns>RobotOutput</returns>
        public RobotOutput Convert(int speed, byte direction)
        {
            this.robotOutput = new RobotOutput();

            byte[] outputSpeed = BitConverter.GetBytes(speed);

            this.robotOutput[0] = Parse('<');
            //直進
            if (direction == Straight)
            {
                this.robotOutput[1] = Parse('F');
                setSpeed(speed);
            }
            //後退
            else if (direction == Back)
            {
                this.robotOutput[1] = Parse('B');
                setSpeed(speed);
            }
            //右旋回
            else if (direction == Right)
            {
                this.robotOutput[1] = Parse('R');
                setSpeed(speed);
            }
            //左旋回
            else if (direction == Left)
            {
                this.robotOutput[1] = Parse('L');
                setSpeed(speed);
            }
            //停止
            else
            {
                this.robotOutput[1] = Parse('S');
                this.robotOutput[2] = Parse('T');
                this.robotOutput[3] = Parse('O');
                this.robotOutput[4] = Parse('P');
            }
            this.robotOutput[5] = Parse('>');

            return this.robotOutput;
        }

        private void setSpeed(int speed)
        {
            byte[] outputSpeed = BitConverter.GetBytes(speed);

            if (outputSpeed.Length == 1)
            {
                this.robotOutput[2] = Parse('0');
                this.robotOutput[3] = Parse('0');
            }
            else if (outputSpeed.Length == 2)
            {
                this.robotOutput[2] = Parse('0');
            }
            foreach (var byteSpeed in outputSpeed)
            {
                this.robotOutput[5 - outputSpeed.Length] = byteSpeed;

            }
        }

        private static byte Parse(char c)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(new char[]{c});
            return bytes[0];
        }
    }
}

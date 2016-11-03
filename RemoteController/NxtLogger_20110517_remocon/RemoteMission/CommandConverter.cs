namespace RemoteMission
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MissionInterface;

    public delegate int SpeedMapper(CommandConverter.Direction dir, int speed);

    public class CommandConverter
    {
        private const int QueueSize = 5;
        private Queue<RobotOutput> history = new Queue<RobotOutput>();
        private SpeedMapper speedMapper = (dir, speed) => speed;

        [Flags]
        public enum Direction
        {
            None = 0x00,
            Straight = 0x01,
            Back = 0x02,
            Left = 0x04,
            Right = 0x08,
            StraightRight = Straight | Right,
            StraightLeft = Straight | Left,
            BackRight = Back | Right,
            BackLeft = Back | Left,
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

        public SpeedMapper SpeedMap
        {
            set
            {
                this.speedMapper = value;
            }
        }

        /// <summary>
        /// コマンド生成履歴
        /// </summary>
        public IEnumerable<RobotOutput> History
        {
            get
            {
                return this.history;
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
            var output = this.ConvertBase(speed, direction);
            if( QueueSize < this.history.Count)
            {
                this.history.Dequeue();
            }
            this.history.Enqueue(output);
            return output;
        }

        public RobotOutput ConvertBase(int speed, Direction direction)
        {
            if (HasFlags(direction, Direction.Right))
            {
                //右旋回
                return new RobotOutput(this.SetSpeed(this.speedMapper(Direction.Right, speed), "R"));
            }
            else if (HasFlags(direction, Direction.Left))
            {
                //左旋回
                return new RobotOutput(this.SetSpeed(this.speedMapper(Direction.Left, speed), "L"));
            }
            else if (HasFlags(direction, Direction.Straight))
            {
                //直進
                return new RobotOutput(this.SetSpeed(this.speedMapper(Direction.Straight, speed), "F"));
            }
            else if (HasFlags(direction, Direction.Back))
            {
                //後退
                return new RobotOutput(this.SetSpeed(this.speedMapper(Direction.Back, speed), "B"));
            }

            return new RobotOutput(this.SetSpeed(speed, "F"));
        }


        private static bool HasFlags(Direction direction, Direction target)
        {
            return (direction & target) == target;
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

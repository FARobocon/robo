namespace MissionInterface
{
    using System;

    public struct RobotOutput : IEquatable<RobotOutput>
    {
        private string strVal;

        public RobotOutput(string strVal)
        {
            this.strVal = strVal;
        }

        public static RobotOutput InvalidRobotOutput
        {
            get
            {
                return new RobotOutput(string.Empty);
            }
        }

        public byte[] Data
        {
            get 
            {
                if (string.IsNullOrEmpty(this.strVal)) return null;
                return System.Text.Encoding.ASCII.GetBytes(this.strVal);
            }
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.strVal);
            }
        }

        public static bool operator ==(RobotOutput left, RobotOutput right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RobotOutput left, RobotOutput right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return this.ToString("c");
        }

        public string ToString(string fmt)
        {
            if (fmt.ToLower() == "c")
            {
                return this.strVal;
            }
            if (fmt.ToLower() == "d")
            {
                string str = string.Empty;
                foreach (byte v in this.Data)
                {
                    str += v.ToString("D") + ",";
                }
                return str;
            }
            return string.Empty;
        }

        public bool Equals(RobotOutput other)
        {
            return this.strVal == other.strVal;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null)) return false;

            if (object.ReferenceEquals(this, obj)) return true;

            if (this.GetType() != obj.GetType()) return false;

            return this.Equals((RobotOutput) obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

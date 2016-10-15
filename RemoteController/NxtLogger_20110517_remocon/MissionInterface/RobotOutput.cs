namespace MissionInterface
{
    using System;

    public struct RobotOutput
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
    }
}

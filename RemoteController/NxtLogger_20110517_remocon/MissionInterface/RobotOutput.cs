namespace MissionInterface
{
    using System;

    public class RobotOutput
    {
        private byte[] val = new byte[6]
        { 
           (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0
        };

        public RobotOutput()
        {
        }

        public RobotOutput(byte[] data)
        {
            this.val = new byte[data.Length];
            data.CopyTo(this.val, 0);
        }

        public byte[] Data
        {
            get 
            {
                var copy = new byte[this.val.Length];
                this.val.CopyTo(copy, 0);
                return copy; 
            }
        }

        public byte this[int i]
        {
            get { return this.val[i]; }
            set { this.val[i] = value; }
        }

        public override string ToString()
        {
            string str = string.Empty;
            foreach (byte v in this.val)
            {
                str += v.ToString("D") + ",";
            }
            return str;
        }

        public string ToString(string fmt)
        {
            if (fmt == "c")
            {
                return System.Text.Encoding.ASCII.GetString(this.Data);
            }
            return string.Empty;
        }
    }
}

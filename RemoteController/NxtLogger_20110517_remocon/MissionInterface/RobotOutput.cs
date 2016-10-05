namespace MissionInterface
{
    using System;

    public class RobotOutput
    {
        private byte[] val_ = new byte[6]
            { (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0
            };

        public byte this[int i]
        {
            set { val_[i] = value; }
            get { return val_[i];  }
        }

        public byte[] Data
        {
            get { return val_; }
        }

        public override String ToString()
        {
            String str = "";
            foreach(byte val in val_)
            {
                str += val.ToString("D") + ",";
            }
            return str;
        }
    }
}

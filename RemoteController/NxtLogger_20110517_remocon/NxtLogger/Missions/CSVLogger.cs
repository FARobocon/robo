namespace NxtLogger.Missions
{
    using MissionInterface;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    class CSVLogger
    {
        private String filename;

        public CSVLogger()
        {
            filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_CommandLog.csv";

            using (StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.Append)))
            {
                try
                {
                    // ファイルへ追記
                    sw.WriteLine("Data1,Data2,Data3,Data4,Data5,Data6");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FILE WRITE ERROR : {0}", ex.ToString());
                    filename = "";
                }
            }
        }

        public void append(RobotOutput output)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.Append)))
            {
                try
                {
                    // ファイルへ追記
                    sw.WriteLine(output.ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FILE WRITE ERROR : {0}", ex.ToString());
                }
            }
        }
    }

    class CSVReader
    {
        private List<String> strLines_ = new List<string>();
        private int index_;

        public CSVReader(String filename)
        {
            using (TextReader reader = new StreamReader(filename, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    strLines_.Add(line);
                }
                index_ = 1;
            }
        }

        public RobotOutput read()
        {
            if (index_ < strLines_.Count)
            {
                string[] strline = strLines_[index_++].Split(',');
                RobotOutput output = new RobotOutput();
                for (int i = 0; i < output.Data.Length; i++)
                    output[i] = Byte.Parse(strline[i]);
                return output;
            }
            return null;
        }
    }
}

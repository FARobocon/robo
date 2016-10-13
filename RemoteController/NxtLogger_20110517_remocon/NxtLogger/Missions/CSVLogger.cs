namespace NxtLogger.Missions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using MissionInterface;

    internal class CsvLogger
    {
        private string filename;

        public CsvLogger()
        {
            this.filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_CommandLog.csv";

            using (StreamWriter sw = new StreamWriter(new FileStream(this.filename, FileMode.Append)))
            {
                try
                {
                    // ファイルへ追記
                    sw.WriteLine("Data1,Data2,Data3,Data4,Data5,Data6");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FILE WRITE ERROR : {0}", ex.ToString());
                    this.filename = string.Empty;
                }
            }
        }

        public void Append(RobotOutput output)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(this.filename, FileMode.Append)))
            {
                try
                {
                    // ファイルへ追記
                    sw.WriteLine(output.ToString("c"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FILE WRITE ERROR : {0}", ex.ToString());
                }
            }
        }
    }
}

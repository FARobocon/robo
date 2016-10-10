namespace NxtLogger.Missions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using MissionInterface;

    internal class CsvReader
    {
        private List<string> strLines = new List<string>();
        private int index;

        public CsvReader(string filename)
        {
            using (TextReader reader = new StreamReader(filename, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    this.strLines.Add(line);
                }
                this.index = 1;
            }
        }

        public RobotOutput Read()
        {
            if (this.index < this.strLines.Count)
            {
                string[] strline = this.strLines[this.index++].Split(',');
                RobotOutput output = new RobotOutput();
                for (int i = 0; i < output.Data.Length; i++)
                    output[i] = byte.Parse(strline[i]);
                return output;
            }
            return null;
        }
    }
}

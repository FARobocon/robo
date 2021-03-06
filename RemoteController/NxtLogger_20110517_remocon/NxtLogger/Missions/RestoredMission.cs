﻿namespace NxtLogger.Missions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using MissionInterface;

    internal class RestoredMission : IMissionInterface
    {
        private SendMissionDelegate retryDelegate = null;
        private CsvReader reader = null;

        public void Init(SendMissionDelegate retryDelegate)
        {
            this.retryDelegate  = retryDelegate;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Stream myStream = null;
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            this.reader = new CsvReader(openFileDialog1.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public RobotOutput Run(RobotInput robotInput)
        {
            return this.reader != null ? this.reader.Read() : new RobotOutput(string.Empty);
        }
    }
}

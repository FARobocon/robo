﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NxtLogger.Missions
{
    public partial class RestoredControlForm : Form, MissionInterface
    {
        private CSVReader reader_ = null;
        private RetryMissionDelegate retryDelegate_;
        public RestoredControlForm()
        {
            InitializeComponent();
        }
        public void Init(RetryMissionDelegate retryDelegate)
        {
            retryDelegate_ = retryDelegate;
        }

        private void FileOpenButton_Click(object sender, EventArgs e)
        {
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
                            reader_ = new CSVReader(openFileDialog1.FileName);
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
            return reader_ != null ? reader_.read() : null;
        }
    }
}

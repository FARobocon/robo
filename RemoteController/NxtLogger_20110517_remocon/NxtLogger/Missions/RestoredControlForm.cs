namespace NxtLogger.Missions
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using MissionInterface;

    public partial class RestoredControlForm : Form, IMissionInterface
    {
        private CsvReader reader = null;
        private SendMissionDelegate sendMissiondelegate;
        public RestoredControlForm()
        {
            this.InitializeComponent();
        }
        public void Init(SendMissionDelegate sendDelegate)
        {
            this.sendMissiondelegate = sendDelegate;
        }

        public RobotOutput Run(RobotInput robotInput)
        {
            return this.reader != null ? this.reader.Read() : null;
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
    }
}

namespace NxtLogger.Missions
{
    using System;
    using System.Windows.Forms;
    using MissionInterface;
    using RemoteMission;

    public partial class RemoteControlForm : Form, IMissionInterface
    {
        private Byte direction = 0x00;
        private CSVLogger logger_ = null;
        /// <summary>
        /// 2016ではおそらく無関係
        /// </summary>
        private RetryMissionDelegate retryDelegate_;

        public RemoteControlForm()
        {
            InitializeComponent();
        }

        public void Init(RetryMissionDelegate retryDelegate)
        {
            retryDelegate_ = retryDelegate;
            this.Show();
        }
        public RobotOutput Run(RobotInput robotInput)
        {
            Byte absSpeed = (Byte) Math.Abs(SpeedTrackBar.Value);

            var converter = new CommandConverter();

            var robotOutput = converter.Convert(absSpeed, this.direction);

            if (LoggingChkBox.Checked)
            {
                if (logger_ == null)
                {
                    logger_ = new CSVLogger();
                }
                logger_.append(robotOutput);
            }

            return robotOutput;
        }

        /// <summary>
        /// キーボードで走行方向（WSZAを使用）を入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteControlForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyData)
            {
                case Keys.W: this.direction |= CommandConverter.Straight; break;
                case Keys.S: this.direction |= CommandConverter.Right; break;
                case Keys.Z: this.direction |= CommandConverter.Back; break;
                case Keys.A: this.direction |= CommandConverter.Left; break;
            }
        }

        private void RemoteControlForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            SpeedTrackBar.Minimum = 0;
            SpeedTrackBar.Maximum = 100;

            // 初期値を設定
            SpeedTrackBar.Value = 0;

            // 描画される目盛りの刻みを設定
            SpeedTrackBar.TickFrequency = 10;

            // 最小値、最大値を設定
            TailTrackBar.Minimum = 50;
            TailTrackBar.Maximum = 120;

            // 初期値を設定
            TailTrackBar.Value = 106;

        }

        private void TailResetButton_Click(object sender, EventArgs e)
        {
            // 初期値を設定
            TailTrackBar.Value = 106;
        }

        private void RemoteControlForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W: this.direction &= CommandConverter.ReleaseStraight; break;
                case Keys.S: this.direction &= CommandConverter.ReleaseRight; break;
                case Keys.Z: this.direction &= CommandConverter.ReleaseBack; break;
                case Keys.A: this.direction &= CommandConverter.ReleaseLeft; break;
            }
        }

        /// <summary>
        /// 2016ではおそらく無関係
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            retryDelegate_(new RobotOutput());
        }

    }
}

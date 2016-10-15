namespace NxtLogger.Missions
{
    
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using MissionInterface;
    using RemoteMission;

    public partial class RemoteControlForm : Form, IMissionInterface
    {
        private CommandConverter.Direction direction = CommandConverter.Direction.None;
        private CsvLogger logger = null;
        /// <summary>
        /// コマンドを送信するためのデリゲート
        /// </summary>
        private SendMissionDelegate sendMissionDelegate;

        public RemoteControlForm()
        {
            this.InitializeComponent();
        }

        public void Init(SendMissionDelegate sendDelegate)
        {
            this.sendMissionDelegate = sendDelegate;
            this.sendMissionDelegate(CommandConverter.StartCommand);
            this.Show();
            this.commandTimer.Enabled = true;
        }

        /// <summary>
        /// ロボットの走行パラメータをもとにロボットへの送信コマンドを作成する
        /// </summary>
        /// <param name="robotInput">ロボットの走行パラメータ</param>
        /// <returns>送信コマンド情報</returns>
        public RobotOutput Run(RobotInput robotInput)
        {
            var absSpeed = (byte) Math.Abs(this.SpeedTrackBar.Value);

            var converter = new CommandConverter();

            return converter.Convert(absSpeed, this.direction);
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
                case Keys.W: this.direction |= CommandConverter.Direction.Straight; 
                    break;
                case Keys.S: this.direction |= CommandConverter.Direction.Right; 
                    break;
                case Keys.Z: this.direction |= CommandConverter.Direction.Back; 
                    break;
                case Keys.A: this.direction |= CommandConverter.Direction.Left; 
                    break;
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
                case Keys.W: this.direction &= ~CommandConverter.Direction.Straight; 
                    break;
                case Keys.S: this.direction &= ~CommandConverter.Direction.Right;
                    break;
                case Keys.Z: this.direction &= ~CommandConverter.Direction.Back; 
                    break;
                case Keys.A: this.direction &= ~CommandConverter.Direction.Left;
                    break;
            }
        }

        /// <summary>
        /// 250msタイマ
        /// タイマ間隔でロボットにコマンドを送信する
        /// </summary>
        /// <param name="sender">未使用</param>
        /// <param name="e">未使用</param>
        private void CommandTimerTick(object sender, EventArgs e)
        {
            var output = this.Run(new RobotInput());
            if (output.IsValid)
            {
                var str = output.ToString();

                this.sendMissionDelegate(output);

                if (str != this.sendMsgText.Text)
                {
                    this.sendMsgText.Text = str;
                }
                if (this.LoggingChkBox.Checked)
                {
                    if (this.logger == null)
                    {
                        this.logger = new CsvLogger();
                    }
                    this.logger.Append(output);
                }
            }
        }

        private void RemoteControlFormFormClosed(object sender, FormClosedEventArgs e)
        {
            this.commandTimer.Enabled = false;
            this.sendMissionDelegate(CommandConverter.StopCommand);
        }

    }
}

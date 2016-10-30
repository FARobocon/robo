using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RemoteMission;
using NxtLogger.Missions;
using MissionInterface;

namespace NxtLogger
{
    public partial class RemoteControlPanel : UserControl
    {
        private CommandConverter.Direction direction = CommandConverter.Direction.None;
        private CsvLogger logger = null;
        /// <summary>
        /// コマンドを送信するためのデリゲート
        /// </summary>
        private SendMissionDelegate sendMissionDelegate;

        public RemoteControlPanel()
        {
            InitializeComponent();
        }

        public void Init(SendMissionDelegate sendMissionDelegate)
        {
            this.sendMissionDelegate = sendMissionDelegate;
        }

        private void RemoteControlPanelKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
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

        private void RemoteControlPanelLoad(object sender, EventArgs e)
        {
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

        private void TailResetButtonClick(object sender, EventArgs e)
        {
            // 初期値を設定
            TailTrackBar.Value = 106;
        }

        private void TailResetButtonKeyUp(object sender, KeyEventArgs e)
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

        private void CommandTimerTick(object sender, EventArgs e)
        {
            var absSpeed = (byte)Math.Abs(this.SpeedTrackBar.Value);

            var converter = new CommandConverter();

            var output = converter.Convert(absSpeed, this.direction);
            
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

    }
}

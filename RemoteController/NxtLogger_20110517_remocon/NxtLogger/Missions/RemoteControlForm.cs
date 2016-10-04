using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NxtLogger.Missions
{
    public partial class RemoteControlForm : Form, MissionInterface
    {
        private Byte direction_ = 0x00;
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
            RobotOutput robotOutput = new RobotOutput();

            Byte absSpeed = (Byte) Math.Abs(SpeedTrackBar.Value);

            //スライドバーの値を0～100にキャスト
            byte[] outputSpeed = BitConverter.GetBytes(absSpeed / (SpeedTrackBar.Maximum - SpeedTrackBar.Minimum) * 100);

            robotOutput[0] = byte.Parse("<");
            //直進
            if (direction_ == 0x80)
            {
                robotOutput[1] = byte.Parse("F");
                setSpeed(robotOutput);
            }
            //後退
            else if (direction_ == 0x20)
            {
                robotOutput[1] = byte.Parse("B");
                setSpeed(robotOutput);
            }
            //右旋回
            else if (direction_ == 0x40)
            {
                robotOutput[1] = byte.Parse("R");
                setSpeed(robotOutput);
            }
            //左旋回
            else if (direction_ == 0x10)
            {
                robotOutput[1] = byte.Parse("L");
                setSpeed(robotOutput);
            }
            //停止
            else
            {
                robotOutput[1] = byte.Parse("S");
                robotOutput[2] = byte.Parse("T");
                robotOutput[3] = byte.Parse("O");
                robotOutput[4] = byte.Parse("P");
            }
            robotOutput[5] = byte.Parse(">");

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
                case Keys.W: direction_    |= 0x80; break;
                case Keys.S: direction_ |= 0x40; break;
                case Keys.Z: direction_  |= 0x20; break;
                case Keys.A: direction_  |= 0x10; break;
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
                case Keys.W: direction_    &= 0x7F; break;
                case Keys.S: direction_ &= 0xBF; break;
                case Keys.Z: direction_  &= 0xDF; break;
                case Keys.A: direction_  &= 0xEF; break;
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

        private void setSpeed(RobotOutput robotOutput)
        {
            //スライドバーの値を取得
            Byte absSpeed = (Byte)Math.Abs(SpeedTrackBar.Value);

            //スライドバーの値を0～100にキャスト
            byte[] outputSpeed = BitConverter.GetBytes(absSpeed / (SpeedTrackBar.Maximum - SpeedTrackBar.Minimum) * 100);

            if (outputSpeed.Length == 1)
            {
                robotOutput[2] = byte.Parse("0");
                robotOutput[3] = byte.Parse("0");
            }
            else if (outputSpeed.Length == 2)
            {
                robotOutput[2] = byte.Parse("0");
            }
            foreach (var byteSpeed in outputSpeed)
            {
                robotOutput[5 - outputSpeed.Length] = byteSpeed;

            }
        }
    }
}

﻿namespace NxtLogger.Missions
{
    using System;
    using System.Diagnostics;
    using System.IO.Ports;
    using System.Linq;
    using System.Windows.Forms;
    using MissionInterface;
    using RemoteMission;

    public partial class RemoteControlForm : Form
    {
        private const int SpeedIncStep = 5;
        private const int SpeedDecStep = 10;
        private CommandConverter.Direction direction = CommandConverter.Direction.None;
        private CommandConverter converter = new CommandConverter() { SpeedMap = MapSpeed };
        private CsvLogger logger = null;
        private bool increaseFLG = false;
        // シリアルポート
        private LogPort port = new LogPort();

        public RemoteControlForm()
        {
            this.InitializeComponent();
            this.portControl1.Port = this.port;
            this.portControl1.ConnectEvent += (result, portNum) =>
                {
                    this.debugTextBox.Text = "Connected.";
                };
            // シリアルポート受信イベントハンドラー登録
            this.port.DataReceived += new SerialDataReceivedEventHandler(this.SerialPortDataReceived);
            this.commandTimer.Enabled = true;
        }

        // デリゲート宣言
        public delegate void DlgLogOutput(byte[] mes);

        /// <summary>
        /// 速度マッピング
        /// 粒度を細かくし過ぎると、送信回数が多くなりハングアップにつながるので注意
        /// </summary>
        /// <param name="dir">回転</param>
        /// <param name="speed">生速度</param>
        /// <returns>マッピング後速度</returns>
        private static int MapSpeed(CommandConverter.Direction dir, int speed)
        {
            // 回転速度は常に30
            if(dir == CommandConverter.Direction.Left ||dir == CommandConverter.Direction.Right)
                return 30;
            // 速度は4段階に変換する
            if (speed > 0 && speed <= 50) return 30;
            if (speed > 50 && speed <= 75) return 60;
            if (speed > 75) return 100;
            return 0;
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
                case Keys.D:
                    this.SendCommand(CommandConverter.StartCommand);
                    break;
                case Keys.P:
                    this.SendCommand(CommandConverter.StopCommand);
                    break;
                case Keys.N:
                    this.increaseFLG = true;
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

            // スピードの数値の初期化
            SpeedValueLabel.Text = SpeedTrackBar.Value.ToString();
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
                case Keys.N: this.increaseFLG = false;
                    break;
            }
        }

        /// <summary>
        /// 100msタイマ
        /// タイマ間隔でロボットにコマンドを送信する
        /// </summary>
        /// <param name="sender">未使用</param>
        /// <param name="e">未使用</param>
        private void CommandTimerTick(object sender, EventArgs e)
        {
            this.SetSpeed();
            var absSpeed = (byte)Math.Abs(this.SpeedTrackBar.Value);

            var output = this.converter.Convert(absSpeed, this.direction);

            if (output.IsValid)
            {
                // 同じコマンドを繰り返し送らないよう、履歴をもとに重複送信を抑制する
                // コマンドが送信中に喪失するリスクも考慮し、5件以上同じコマンドであった場合に送信抑制する
                if (this.converter.History.All(hst => hst == output))
                {
                    return;
                }

                var str = output.ToString();

                this.SendCommand(output);
            }
        }

        /// <summary>
        /// ボタン押下有無でスピードの数値を変更する
        /// </summary>                 
        private void SetSpeed()
        {
            var speedvalue = this.SpeedTrackBar.Value;

            if (this.increaseFLG)
            {
                speedvalue += RemoteControlForm.SpeedIncStep;
                if (speedvalue > this.SpeedTrackBar.Maximum)
                {
                    speedvalue = this.SpeedTrackBar.Maximum;
                }
            }
            else
            {
                speedvalue -= RemoteControlForm.SpeedDecStep;
                if (speedvalue < this.SpeedTrackBar.Minimum)
                {
                    speedvalue = this.SpeedTrackBar.Minimum;
                }
            }
            this.SpeedTrackBar.Value = speedvalue;
        }

        private void SendCommand(RobotOutput output)
        {
            try
            {
                var text = output.ToString();

                if (this.port.IsOpen)
                {
                    this.port.Write(text);
                }
                if (this.LoggingChkBox.Checked)
                {
                    if (this.logger == null)
                    {
                        this.logger = new CsvLogger();
                    }
                    this.logger.Append(output);
                }
                Debug.WriteLine(text);
                this.sendMsgText.Text = text;
            }
            catch (Exception ex)
            {
                // フォームにエラー表示
                this.sendMsgText.Text = "COMMAND ERROR:" + ex.ToString();

                Debug.WriteLine("FILE WRITE ERROR : {0}", ex.ToString());
            }
        }

        /// <summary>
        /// （メインスレッドの）ログデータ受信
        /// </summary>
        /// <param name="mes">データ</param>
        private void MessegeReceive(byte[] mes)
        {
            var txt = System.Text.Encoding.ASCII.GetString(mes);

            try
            {
                if (this.port.IsOpen )
                {
                    this.debugTextBox.Text = "Receive:" + txt;
                }
            }
            catch (Exception ex)
            {
                // フォームにエラー表示
                this.debugTextBox.Text = "RECEIVE ERROR:" + ex.ToString();

                Debug.WriteLine("FILE WRITE ERROR : {0}", ex.ToString());
            }
        }

        /// <summary>
        /// シリアルポート受信イベントハンドラ
        /// </summary>
        /// <param name="sender">センダー</param>
        /// <param name="e">イベント引数</param>
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DlgLogOutput dlgByteOut = new DlgLogOutput(this.MessegeReceive);

            // データ受信バッファ
            var buf = new byte[this.port.BytesToRead];

            if (buf.Length > 0)
            {
                try
                {
                    // シリアルポートより受信
                    this.port.Read(buf, 0, buf.Length);

                    // 受信データをメインスレッドへ
                    this.BeginInvoke(dlgByteOut, buf);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("UNEXPECTED EXCEPTION : {0}", ex.ToString());
                }
            }
        }

        private void RemoteControlFormFormClosed(object sender, FormClosedEventArgs e)
        {
            this.commandTimer.Enabled = false;
            // シリアルポートインスタンス破棄
            if (this.port != null) this.port.Dispose();
            // アプリケーション終了処理
            Application.Exit();
        }

        // スピードの数値更新
        private void SpeedTrackBarValueChanged(object sender, EventArgs e)
        {
            this.SpeedValueLabel.Text = this.SpeedTrackBar.Value.ToString();
        }
    }
}

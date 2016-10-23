namespace NxtLogger
{
    using System;
    using System.Windows.Forms;

    public delegate void ConnectEventDelegate(bool result, string portNo);

    public partial class PortControl : UserControl
    {
        public PortControl()
        {
            this.InitializeComponent();
            this.PortNoLoad();
        }

        public event ConnectEventDelegate ConnectEvent;

        public LogPort Port
        {
            private get;
            set;
        }

        private void ChkConnectCheckedChanged(object sender, EventArgs e)
        {
            if (this.chkConnect.Checked)
            {
                // シリアルポート番号設定
                this.Port.Connect(this.portNoBox.Text);

                if (!this.Port.IsOpen)
                {
                    MessageBox.Show("接続失敗");
                    this.ConnectEvent(false, this.portNoBox.Text);
                    return;
                }

                // COMポート番号選択COMBO BOXを無効化
                this.portNoBox.Enabled = false;

                // 接続OKをコールバック
                this.ConnectEvent(true, this.portNoBox.Text);
            }
            else
            {
                // ポートの切断処理
                this.Port.Disconnect();

                // COMポート番号選択COMBO BOXを有効化
                this.portNoBox.Enabled = true;
            }
        }

        /// <summary>
        /// シリアルポート名をシステムより取得しコンボボックスに反映
        /// </summary>
        private void PortNoLoad()
        {
            // ソート済みのポート名一覧を取得
            string[] portNames = LogPort.GetSortedPortNames();

            // 取得したシリアル・ポート名をコンボボックスにセット
            portNoBox.Items.Clear();
            foreach (string port in portNames)
            {
                this.portNoBox.Items.Add(port);
            }

            if (portNames.Length > 0)
            {
                // コンボボックスのデフォルト選択をセット
                this.portNoBox.Text = portNames[0];
            }
            else
            {
                // CONNECTボタンを無効状態
                chkConnect.Enabled = false;

                // コンボボックスを無効状態
                this.portNoBox.Enabled = false;
            }
        }

        private void MonitorTimerTick(object sender, EventArgs e)
        {
            // ポート状態監視
            if (this.Port != null && this.Port.IsOpen)
            {
                // ハードウェアフロー制御端子(CTS:Clear To Send)監視
                if (this.Port.CtsHolding)
                {
                    // ラベル文字をアクティブ表示
                    this.labelCTS.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                else
                {
                    // ラベル文字を非アクティブ表示
                    this.labelCTS.ForeColor = System.Drawing.SystemColors.ControlDark;
                }

                // ハードウェアフロー制御端子(DSR:Data Set Ready)監視
                if (this.Port.DsrHolding)
                {
                    // ラベル文字をアクティブ表示
                    this.labelDSR.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                else
                {
                    // ラベル文字を非アクティブ表示
                    this.labelDSR.ForeColor = System.Drawing.SystemColors.ControlDark;
                }

                if (this.Port.CtsHolding != true || this.Port.DsrHolding != true)
                {
                    // CONNECTボタンをアンチェック状態
                    this.chkConnect.Checked = false;
                }
            }
            else
            {
                // ラベル文字を非アクティブ表示
                this.labelCTS.ForeColor = System.Drawing.SystemColors.ControlDark;
                this.labelDSR.ForeColor = System.Drawing.SystemColors.ControlDark;
            }
        }
    }

    
}

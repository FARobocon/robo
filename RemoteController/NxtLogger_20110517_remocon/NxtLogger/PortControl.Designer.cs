namespace NxtLogger
{
    partial class PortControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelDSR = new System.Windows.Forms.Label();
            this.labelCTS = new System.Windows.Forms.Label();
            this.portNoBox = new System.Windows.Forms.ComboBox();
            this.chkConnect = new System.Windows.Forms.CheckBox();
            this.monitorTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelDSR
            // 
            this.labelDSR.AutoSize = true;
            this.labelDSR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDSR.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.labelDSR.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelDSR.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDSR.Location = new System.Drawing.Point(308, 11);
            this.labelDSR.Name = "labelDSR";
            this.labelDSR.Padding = new System.Windows.Forms.Padding(2);
            this.labelDSR.Size = new System.Drawing.Size(34, 18);
            this.labelDSR.TabIndex = 12;
            this.labelDSR.Text = "DSR";
            this.labelDSR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCTS
            // 
            this.labelCTS.AutoSize = true;
            this.labelCTS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCTS.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.labelCTS.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelCTS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCTS.Location = new System.Drawing.Point(269, 11);
            this.labelCTS.Name = "labelCTS";
            this.labelCTS.Padding = new System.Windows.Forms.Padding(2);
            this.labelCTS.Size = new System.Drawing.Size(33, 18);
            this.labelCTS.TabIndex = 11;
            this.labelCTS.Text = "CTS";
            this.labelCTS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portNoBox
            // 
            this.portNoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNoBox.FormattingEnabled = true;
            this.portNoBox.Location = new System.Drawing.Point(13, 11);
            this.portNoBox.Name = "portNoBox";
            this.portNoBox.Size = new System.Drawing.Size(74, 20);
            this.portNoBox.TabIndex = 10;
            // 
            // chkConnect
            // 
            this.chkConnect.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkConnect.AutoSize = true;
            this.chkConnect.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.chkConnect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkConnect.Location = new System.Drawing.Point(350, 6);
            this.chkConnect.Name = "chkConnect";
            this.chkConnect.Size = new System.Drawing.Size(70, 26);
            this.chkConnect.TabIndex = 9;
            this.chkConnect.Text = "CONNECT";
            this.chkConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkConnect.UseVisualStyleBackColor = true;
            this.chkConnect.CheckedChanged += new System.EventHandler(this.ChkConnectCheckedChanged);
            // 
            // monitorTimer
            // 
            this.monitorTimer.Enabled = true;
            this.monitorTimer.Interval = 1000;
            this.monitorTimer.Tick += new System.EventHandler(this.MonitorTimerTick);
            // 
            // PortControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDSR);
            this.Controls.Add(this.labelCTS);
            this.Controls.Add(this.portNoBox);
            this.Controls.Add(this.chkConnect);
            this.Name = "PortControl";
            this.Size = new System.Drawing.Size(435, 41);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDSR;
        private System.Windows.Forms.Label labelCTS;
        private System.Windows.Forms.ComboBox portNoBox;
        private System.Windows.Forms.CheckBox chkConnect;
        private System.Windows.Forms.Timer monitorTimer;
    }
}

namespace NxtLogger
{
    partial class RemoteControlPanel
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sendMsgText = new System.Windows.Forms.TextBox();
            this.LoggingChkBox = new System.Windows.Forms.CheckBox();
            this.TailResetButton = new System.Windows.Forms.Button();
            this.TailTrackBar = new System.Windows.Forms.TrackBar();
            this.SpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.commandTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TailTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "送信コマンド";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "尻尾(未定)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "速度(0...100)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "キー割り当て";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(90, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 38);
            this.label1.TabIndex = 16;
            this.label1.Text = "　　↑(w)\r\n(a)←　→(s)\r\n　　↓(z)";
            // 
            // sendMsgText
            // 
            this.sendMsgText.Location = new System.Drawing.Point(17, 186);
            this.sendMsgText.Multiline = true;
            this.sendMsgText.Name = "sendMsgText";
            this.sendMsgText.ReadOnly = true;
            this.sendMsgText.Size = new System.Drawing.Size(259, 43);
            this.sendMsgText.TabIndex = 15;
            // 
            // LoggingChkBox
            // 
            this.LoggingChkBox.AutoSize = true;
            this.LoggingChkBox.Location = new System.Drawing.Point(216, 163);
            this.LoggingChkBox.Name = "LoggingChkBox";
            this.LoggingChkBox.Size = new System.Drawing.Size(44, 16);
            this.LoggingChkBox.TabIndex = 14;
            this.LoggingChkBox.Text = "Rec";
            this.LoggingChkBox.UseVisualStyleBackColor = true;
            // 
            // TailResetButton
            // 
            this.TailResetButton.Location = new System.Drawing.Point(191, 110);
            this.TailResetButton.Name = "TailResetButton";
            this.TailResetButton.Size = new System.Drawing.Size(69, 23);
            this.TailResetButton.TabIndex = 13;
            this.TailResetButton.Text = "尻尾 reset";
            this.TailResetButton.UseVisualStyleBackColor = true;
            this.TailResetButton.Click += new System.EventHandler(this.TailResetButtonClick);
            this.TailResetButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TailResetButtonKeyUp);
            // 
            // TailTrackBar
            // 
            this.TailTrackBar.Location = new System.Drawing.Point(76, 67);
            this.TailTrackBar.Maximum = 100;
            this.TailTrackBar.Name = "TailTrackBar";
            this.TailTrackBar.Size = new System.Drawing.Size(200, 45);
            this.TailTrackBar.TabIndex = 12;
            // 
            // SpeedTrackBar
            // 
            this.SpeedTrackBar.Location = new System.Drawing.Point(77, 16);
            this.SpeedTrackBar.Maximum = 100;
            this.SpeedTrackBar.Name = "SpeedTrackBar";
            this.SpeedTrackBar.Size = new System.Drawing.Size(200, 45);
            this.SpeedTrackBar.TabIndex = 11;
            // 
            // commandTimer
            // 
            this.commandTimer.Interval = 250;
            this.commandTimer.Tick += new System.EventHandler(this.CommandTimerTick);
            // 
            // RemoteControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendMsgText);
            this.Controls.Add(this.LoggingChkBox);
            this.Controls.Add(this.TailResetButton);
            this.Controls.Add(this.TailTrackBar);
            this.Controls.Add(this.SpeedTrackBar);
            this.Name = "RemoteControlPanel";
            this.Size = new System.Drawing.Size(287, 244);
            this.Load += new System.EventHandler(this.RemoteControlPanelLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteControlPanelKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.TailTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sendMsgText;
        private System.Windows.Forms.CheckBox LoggingChkBox;
        private System.Windows.Forms.Button TailResetButton;
        private System.Windows.Forms.TrackBar TailTrackBar;
        private System.Windows.Forms.TrackBar SpeedTrackBar;
        private System.Windows.Forms.Timer commandTimer;

    }
}

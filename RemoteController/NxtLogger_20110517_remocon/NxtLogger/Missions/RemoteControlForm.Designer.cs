namespace NxtLogger.Missions
{
    partial class RemoteControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.TailTrackBar = new System.Windows.Forms.TrackBar();
            this.TailResetButton = new System.Windows.Forms.Button();
            this.LoggingChkBox = new System.Windows.Forms.CheckBox();
            this.commandTimer = new System.Windows.Forms.Timer(this.components);
            this.sendMsgText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TailTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SpeedTrackBar
            // 
            this.SpeedTrackBar.Location = new System.Drawing.Point(72, 36);
            this.SpeedTrackBar.Maximum = 100;
            this.SpeedTrackBar.Name = "SpeedTrackBar";
            this.SpeedTrackBar.Size = new System.Drawing.Size(200, 45);
            this.SpeedTrackBar.TabIndex = 0;
            // 
            // TailTrackBar
            // 
            this.TailTrackBar.Location = new System.Drawing.Point(71, 87);
            this.TailTrackBar.Maximum = 100;
            this.TailTrackBar.Name = "TailTrackBar";
            this.TailTrackBar.Size = new System.Drawing.Size(200, 45);
            this.TailTrackBar.TabIndex = 1;
            // 
            // TailResetButton
            // 
            this.TailResetButton.Location = new System.Drawing.Point(186, 130);
            this.TailResetButton.Name = "TailResetButton";
            this.TailResetButton.Size = new System.Drawing.Size(69, 23);
            this.TailResetButton.TabIndex = 2;
            this.TailResetButton.Text = "尻尾 reset";
            this.TailResetButton.UseVisualStyleBackColor = true;
            this.TailResetButton.Click += new System.EventHandler(this.TailResetButton_Click);
            // 
            // LoggingChkBox
            // 
            this.LoggingChkBox.AutoSize = true;
            this.LoggingChkBox.Location = new System.Drawing.Point(211, 183);
            this.LoggingChkBox.Name = "LoggingChkBox";
            this.LoggingChkBox.Size = new System.Drawing.Size(44, 16);
            this.LoggingChkBox.TabIndex = 3;
            this.LoggingChkBox.Text = "Rec";
            this.LoggingChkBox.UseVisualStyleBackColor = true;
            // 
            // commandTimer
            // 
            this.commandTimer.Tick += new System.EventHandler(this.CommandTimerTick);
            // 
            // sendMsgText
            // 
            this.sendMsgText.Location = new System.Drawing.Point(12, 206);
            this.sendMsgText.Multiline = true;
            this.sendMsgText.Name = "sendMsgText";
            this.sendMsgText.ReadOnly = true;
            this.sendMsgText.Size = new System.Drawing.Size(259, 43);
            this.sendMsgText.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(85, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 38);
            this.label1.TabIndex = 6;
            this.label1.Text = "　　↑(w)\r\n(a)←　→(s)\r\n　　↓(z)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "キー割り当て";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "速度(0...100)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "尻尾(未定)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "送信コマンド";
            // 
            // RemoteControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 261);
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
            this.Name = "RemoteControlForm";
            this.Text = "RemoteControlForm";
            this.Load += new System.EventHandler(this.RemoteControlForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteControlForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RemoteControlForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TailTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar SpeedTrackBar;
        private System.Windows.Forms.TrackBar TailTrackBar;
        private System.Windows.Forms.Button TailResetButton;
        private System.Windows.Forms.CheckBox LoggingChkBox;
        private System.Windows.Forms.Timer commandTimer;
        private System.Windows.Forms.TextBox sendMsgText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;

    }
}
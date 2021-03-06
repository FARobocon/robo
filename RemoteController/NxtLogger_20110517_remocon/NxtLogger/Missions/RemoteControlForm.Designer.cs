﻿namespace NxtLogger.Missions
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
            this.LoggingChkBox = new System.Windows.Forms.CheckBox();
            this.commandTimer = new System.Windows.Forms.Timer(this.components);
            this.sendMsgText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.debugTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SpeedValueLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.portControl1 = new NxtLogger.PortControl();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SpeedTrackBar
            // 
            this.SpeedTrackBar.Location = new System.Drawing.Point(72, 58);
            this.SpeedTrackBar.Maximum = 100;
            this.SpeedTrackBar.Name = "SpeedTrackBar";
            this.SpeedTrackBar.Size = new System.Drawing.Size(357, 45);
            this.SpeedTrackBar.TabIndex = 0;
            this.SpeedTrackBar.ValueChanged += new System.EventHandler(this.SpeedTrackBarValueChanged);
            // 
            // LoggingChkBox
            // 
            this.LoggingChkBox.AutoSize = true;
            this.LoggingChkBox.Location = new System.Drawing.Point(325, 213);
            this.LoggingChkBox.Name = "LoggingChkBox";
            this.LoggingChkBox.Size = new System.Drawing.Size(44, 16);
            this.LoggingChkBox.TabIndex = 3;
            this.LoggingChkBox.Text = "Rec";
            this.LoggingChkBox.UseVisualStyleBackColor = true;
            // 
            // commandTimer
            // 
            this.commandTimer.Enabled = true;
            this.commandTimer.Tick += new System.EventHandler(this.CommandTimerTick);
            // 
            // sendMsgText
            // 
            this.sendMsgText.Location = new System.Drawing.Point(12, 186);
            this.sendMsgText.Multiline = true;
            this.sendMsgText.Name = "sendMsgText";
            this.sendMsgText.ReadOnly = true;
            this.sendMsgText.Size = new System.Drawing.Size(140, 43);
            this.sendMsgText.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(85, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 38);
            this.label1.TabIndex = 6;
            this.label1.Text = "　　↑(w)\r\n(a)←　→(s)\r\n　　↓(z)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "キー割り当て";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "速度:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "送信コマンド";
            // 
            // debugTextBox
            // 
            this.debugTextBox.Location = new System.Drawing.Point(158, 186);
            this.debugTextBox.Multiline = true;
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.ReadOnly = true;
            this.debugTextBox.Size = new System.Drawing.Size(152, 43);
            this.debugTextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "デバッグ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(159, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 26);
            this.label4.TabIndex = 14;
            this.label4.Text = "(d) Start \r\n(p) Stop";
            // 
            // SpeedValueLabel
            // 
            this.SpeedValueLabel.AutoSize = true;
            this.SpeedValueLabel.Location = new System.Drawing.Point(48, 68);
            this.SpeedValueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SpeedValueLabel.Name = "SpeedValueLabel";
            this.SpeedValueLabel.Size = new System.Drawing.Size(11, 12);
            this.SpeedValueLabel.TabIndex = 15;
            this.SpeedValueLabel.Text = "0";
            this.SpeedValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(407, 85);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(80, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "0";
            // 
            // portControl1
            // 
            this.portControl1.Location = new System.Drawing.Point(2, 2);
            this.portControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.portControl1.Name = "portControl1";
            this.portControl1.Size = new System.Drawing.Size(435, 41);
            this.portControl1.TabIndex = 11;
            // 
            // RemoteControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 245);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SpeedValueLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.debugTextBox);
            this.Controls.Add(this.portControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendMsgText);
            this.Controls.Add(this.LoggingChkBox);
            this.Controls.Add(this.SpeedTrackBar);
            this.Name = "RemoteControlForm";
            this.Text = "RemoteControlForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteControlFormFormClosed);
            this.Load += new System.EventHandler(this.RemoteControlForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteControlForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RemoteControlForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar SpeedTrackBar;
        private System.Windows.Forms.CheckBox LoggingChkBox;
        private System.Windows.Forms.Timer commandTimer;
        private System.Windows.Forms.TextBox sendMsgText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private PortControl portControl1;
        private System.Windows.Forms.TextBox debugTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label SpeedValueLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
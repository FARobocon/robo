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
            this.SpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.TailTrackBar = new System.Windows.Forms.TrackBar();
            this.TailResetButton = new System.Windows.Forms.Button();
            this.LoggingChkBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TailTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SpeedTrackBar
            // 
            this.SpeedTrackBar.Location = new System.Drawing.Point(12, 36);
            this.SpeedTrackBar.Maximum = 100;
            this.SpeedTrackBar.Name = "SpeedTrackBar";
            this.SpeedTrackBar.Size = new System.Drawing.Size(200, 45);
            this.SpeedTrackBar.TabIndex = 0;
            // 
            // TailTrackBar
            // 
            this.TailTrackBar.Location = new System.Drawing.Point(12, 87);
            this.TailTrackBar.Maximum = 100;
            this.TailTrackBar.Name = "TailTrackBar";
            this.TailTrackBar.Size = new System.Drawing.Size(200, 45);
            this.TailTrackBar.TabIndex = 1;
            // 
            // TailResetButton
            // 
            this.TailResetButton.Location = new System.Drawing.Point(219, 87);
            this.TailResetButton.Name = "TailResetButton";
            this.TailResetButton.Size = new System.Drawing.Size(53, 23);
            this.TailResetButton.TabIndex = 2;
            this.TailResetButton.Text = "R";
            this.TailResetButton.UseVisualStyleBackColor = true;
            this.TailResetButton.Click += new System.EventHandler(this.TailResetButton_Click);
            // 
            // LoggingChkBox
            // 
            this.LoggingChkBox.AutoSize = true;
            this.LoggingChkBox.Location = new System.Drawing.Point(24, 157);
            this.LoggingChkBox.Name = "LoggingChkBox";
            this.LoggingChkBox.Size = new System.Drawing.Size(44, 16);
            this.LoggingChkBox.TabIndex = 3;
            this.LoggingChkBox.Text = "Rec";
            this.LoggingChkBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Retry";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RemoteControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;

    }
}
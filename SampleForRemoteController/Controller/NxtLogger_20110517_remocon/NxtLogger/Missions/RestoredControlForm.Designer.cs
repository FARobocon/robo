namespace NxtLogger.Missions
{
    partial class RestoredControlForm
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
            this.FileOpenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FileOpenButton
            // 
            this.FileOpenButton.Location = new System.Drawing.Point(109, 12);
            this.FileOpenButton.Name = "FileOpenButton";
            this.FileOpenButton.Size = new System.Drawing.Size(75, 23);
            this.FileOpenButton.TabIndex = 0;
            this.FileOpenButton.Text = "OpenCSV";
            this.FileOpenButton.UseVisualStyleBackColor = true;
            this.FileOpenButton.Click += new System.EventHandler(this.FileOpenButton_Click);
            // 
            // RestoredControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 56);
            this.Controls.Add(this.FileOpenButton);
            this.Name = "RestoredControlForm";
            this.Text = "RestoredControlForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FileOpenButton;
    }
}
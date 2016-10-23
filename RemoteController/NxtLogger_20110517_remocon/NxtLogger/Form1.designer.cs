namespace NxtLogger
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnQuit = new System.Windows.Forms.Button();
            this.boxDataView = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLogFile = new System.Windows.Forms.Label();
            this.textLogFile = new System.Windows.Forms.TextBox();
            this.missionButton = new System.Windows.Forms.Button();
            this.portControl1 = new NxtLogger.PortControl();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            resources.ApplyResources(this.btnQuit, "btnQuit");
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.BtnQuitClick);
            // 
            // boxDataView
            // 
            this.boxDataView.AcceptsReturn = true;
            this.boxDataView.BackColor = System.Drawing.SystemColors.HighlightText;
            resources.ApplyResources(this.boxDataView, "boxDataView");
            this.boxDataView.Name = "boxDataView";
            this.boxDataView.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelLogFile
            // 
            resources.ApplyResources(this.labelLogFile, "labelLogFile");
            this.labelLogFile.Name = "labelLogFile";
            // 
            // textLogFile
            // 
            this.textLogFile.BackColor = System.Drawing.SystemColors.HighlightText;
            resources.ApplyResources(this.textLogFile, "textLogFile");
            this.textLogFile.Name = "textLogFile";
            this.textLogFile.ReadOnly = true;
            // 
            // missionButton
            // 
            resources.ApplyResources(this.missionButton, "missionButton");
            this.missionButton.Name = "missionButton";
            this.missionButton.UseVisualStyleBackColor = true;
            this.missionButton.Click += new System.EventHandler(this.MissionButtonClick);
            // 
            // portControl1
            // 
            resources.ApplyResources(this.portControl1, "portControl1");
            this.portControl1.Name = "portControl1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.portControl1);
            this.Controls.Add(this.missionButton);
            this.Controls.Add(this.textLogFile);
            this.Controls.Add(this.labelLogFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxDataView);
            this.Controls.Add(this.btnQuit);
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TextBox boxDataView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLogFile;
        private System.Windows.Forms.TextBox textLogFile;
        private System.Windows.Forms.Button missionButton;
        private PortControl portControl1;
    }
}


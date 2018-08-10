namespace BigPicture {
	partial class OptionForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
			this.dataLoadDialog = new System.Windows.Forms.OpenFileDialog();
			this.dataSaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.mainPanel = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// dataLoadDialog
			// 
			this.dataLoadDialog.DefaultExt = "data";
			this.dataLoadDialog.Filter = "일정 데이터|*.data";
			// 
			// dataSaveDialog
			// 
			this.dataSaveDialog.Filter = "일정 데이터|*.data";
			// 
			// fontDialog
			// 
			this.fontDialog.AllowScriptChange = false;
			this.fontDialog.AllowSimulations = false;
			this.fontDialog.AllowVerticalFonts = false;
			this.fontDialog.MaxSize = 32;
			this.fontDialog.MinSize = 8;
			this.fontDialog.ShowEffects = false;
			// 
			// mainPanel
			// 
			this.mainPanel.Image = global::BigPicture.Properties.Resources.Option_Panel;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(669, 407);
			this.mainPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.mainPanel.TabIndex = 0;
			this.mainPanel.TabStop = false;
			// 
			// OptionForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(669, 407);
			this.Controls.Add(this.mainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "OptionForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnDestroy);
			this.Load += new System.EventHandler(this.OnLoad);
			((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox mainPanel;
		private System.Windows.Forms.OpenFileDialog dataLoadDialog;
		private System.Windows.Forms.SaveFileDialog dataSaveDialog;
		private System.Windows.Forms.FontDialog fontDialog;
	}
}
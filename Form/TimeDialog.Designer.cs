namespace BigPicture.Forms {
	partial class TimeDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeDialog));
			this.cancelBtn = new System.Windows.Forms.PictureBox();
			this.applyBtn = new System.Windows.Forms.PictureBox();
			this.PMBtn = new System.Windows.Forms.PictureBox();
			this.panelImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.cancelBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.applyBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PMBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelImage)).BeginInit();
			this.SuspendLayout();
			// 
			// cancelBtn
			// 
			this.cancelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.cancelBtn.Image = global::BigPicture.Properties.Resources.TimeDialog_CancelBtn;
			this.cancelBtn.Location = new System.Drawing.Point(140, 101);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(140, 38);
			this.cancelBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.cancelBtn.TabIndex = 2;
			this.cancelBtn.TabStop = false;
			// 
			// applyBtn
			// 
			this.applyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
			this.applyBtn.Image = global::BigPicture.Properties.Resources.TimeDialog_ApplyBtn;
			this.applyBtn.Location = new System.Drawing.Point(0, 101);
			this.applyBtn.Name = "applyBtn";
			this.applyBtn.Size = new System.Drawing.Size(140, 37);
			this.applyBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.applyBtn.TabIndex = 1;
			this.applyBtn.TabStop = false;
			// 
			// PMBtn
			// 
			this.PMBtn.Image = global::BigPicture.Properties.Resources.TimeDialog_AM;
			this.PMBtn.Location = new System.Drawing.Point(159, 62);
			this.PMBtn.Name = "PMBtn";
			this.PMBtn.Size = new System.Drawing.Size(40, 23);
			this.PMBtn.TabIndex = 10;
			this.PMBtn.TabStop = false;
			// 
			// panelImage
			// 
			this.panelImage.BackColor = System.Drawing.Color.Transparent;
			this.panelImage.Image = ((System.Drawing.Image)(resources.GetObject("panelImage.Image")));
			this.panelImage.Location = new System.Drawing.Point(0, 12);
			this.panelImage.Name = "panelImage";
			this.panelImage.Size = new System.Drawing.Size(280, 89);
			this.panelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.panelImage.TabIndex = 3;
			this.panelImage.TabStop = false;
			// 
			// TimeDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
			this.ClientSize = new System.Drawing.Size(280, 138);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.applyBtn);
			this.Controls.Add(this.PMBtn);
			this.Controls.Add(this.panelImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TimeDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "시간 설정";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnDestroy);
			((System.ComponentModel.ISupportInitialize)(this.cancelBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.applyBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PMBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.PictureBox applyBtn;
		private System.Windows.Forms.PictureBox cancelBtn;
		private System.Windows.Forms.PictureBox panelImage;
		private System.Windows.Forms.PictureBox PMBtn;
	}
}
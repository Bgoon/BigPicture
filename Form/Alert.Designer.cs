namespace BigPicture.Forms {
	partial class Alert {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert));
			this.closeBtn = new System.Windows.Forms.PictureBox();
			this.panel = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.closeBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
			this.SuspendLayout();
			// 
			// closeBtn
			// 
			this.closeBtn.BackColor = System.Drawing.Color.White;
			this.closeBtn.Image = global::BigPicture.Properties.Resources.Alert_closeBtn;
			this.closeBtn.Location = new System.Drawing.Point(353, 3);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(24, 24);
			this.closeBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.closeBtn.TabIndex = 1;
			this.closeBtn.TabStop = false;
			// 
			// panel
			// 
			this.panel.Image = global::BigPicture.Properties.Resources.Alert_Panel;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(380, 68);
			this.panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.panel.TabIndex = 0;
			this.panel.TabStop = false;
			// 
			// Alert
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(380, 68);
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.panel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Alert";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Alert";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			((System.ComponentModel.ISupportInitialize)(this.closeBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox panel;
		private System.Windows.Forms.PictureBox closeBtn;
	}
}
namespace BigPicture.Forms {
	partial class Tutorial {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tutorial));
			this.panel = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Image = global::BigPicture.Properties.Resources.Tutorial_Panel;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(341, 361);
			this.panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.panel.TabIndex = 0;
			this.panel.TabStop = false;
			// 
			// Tutorial
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(340, 361);
			this.Controls.Add(this.panel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Tutorial";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tutorial";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnDestroy);
			((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox panel;
	}
}
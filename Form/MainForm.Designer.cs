namespace BigPicture {
	partial class MainForm {
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.layerArea = new System.Windows.Forms.Panel();
			this.trashPanel = new System.Windows.Forms.Panel();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.trayMenu_열기 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.trayMenu_정보 = new System.Windows.Forms.ToolStripMenuItem();
			this.trayMenu_종료 = new System.Windows.Forms.ToolStripMenuItem();
			this.trashBtn = new System.Windows.Forms.PictureBox();
			this.undoBtn = new System.Windows.Forms.PictureBox();
			this.hideBtn = new System.Windows.Forms.PictureBox();
			this.addBtn = new System.Windows.Forms.PictureBox();
			this.trayMenu_설정 = new System.Windows.Forms.ToolStripMenuItem();
			this.trayMenu_업데이트 = new System.Windows.Forms.ToolStripMenuItem();
			this.trayMenu_사용방법 = new System.Windows.Forms.ToolStripMenuItem();
			this.layerArea.SuspendLayout();
			this.trashPanel.SuspendLayout();
			this.trayMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trashBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.undoBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hideBtn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.addBtn)).BeginInit();
			this.SuspendLayout();
			// 
			// layerArea
			// 
			this.layerArea.BackColor = System.Drawing.Color.Transparent;
			this.layerArea.Controls.Add(this.trashPanel);
			this.layerArea.Controls.Add(this.undoBtn);
			this.layerArea.Controls.Add(this.hideBtn);
			this.layerArea.Controls.Add(this.addBtn);
			this.layerArea.Location = new System.Drawing.Point(0, 0);
			this.layerArea.Name = "layerArea";
			this.layerArea.Size = new System.Drawing.Size(1024, 200);
			this.layerArea.TabIndex = 2;
			// 
			// trashPanel
			// 
			this.trashPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.trashPanel.Controls.Add(this.trashBtn);
			this.trashPanel.Location = new System.Drawing.Point(0, 0);
			this.trashPanel.Name = "trashPanel";
			this.trashPanel.Size = new System.Drawing.Size(40, 200);
			this.trashPanel.TabIndex = 4;
			this.trashPanel.Visible = false;
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenuStrip = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "빅픽처";
			this.trayIcon.Visible = true;
			this.trayIcon.DoubleClick += new System.EventHandler(this.OnTrayClick_열기);
			// 
			// trayMenu
			// 
			this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayMenu_열기,
            this.trayMenu_설정,
            this.trayMenu_업데이트,
            this.trayMenu_사용방법,
            this.toolStripSeparator1,
            this.trayMenu_정보,
            this.trayMenu_종료});
			this.trayMenu.Name = "trayMenu";
			this.trayMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.trayMenu.Size = new System.Drawing.Size(155, 166);
			this.trayMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OnTrayMenu);
			// 
			// trayMenu_열기
			// 
			this.trayMenu_열기.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.trayMenu_열기.Name = "trayMenu_열기";
			this.trayMenu_열기.Size = new System.Drawing.Size(154, 26);
			this.trayMenu_열기.Text = "열기";
			this.trayMenu_열기.Click += new System.EventHandler(this.OnTrayClick_열기);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
			// 
			// trayMenu_정보
			// 
			this.trayMenu_정보.Name = "trayMenu_정보";
			this.trayMenu_정보.Size = new System.Drawing.Size(154, 26);
			this.trayMenu_정보.Text = "프로그램 정보";
			this.trayMenu_정보.Click += new System.EventHandler(this.OnTrayClick_정보);
			// 
			// trayMenu_종료
			// 
			this.trayMenu_종료.Name = "trayMenu_종료";
			this.trayMenu_종료.Size = new System.Drawing.Size(154, 26);
			this.trayMenu_종료.Text = "종료";
			this.trayMenu_종료.Click += new System.EventHandler(this.OnTrayClick_종료);
			// 
			// trashBtn
			// 
			this.trashBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
			this.trashBtn.Image = global::BigPicture.Properties.Resources.Root_trashBtn;
			this.trashBtn.Location = new System.Drawing.Point(0, 0);
			this.trashBtn.Name = "trashBtn";
			this.trashBtn.Size = new System.Drawing.Size(40, 200);
			this.trashBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.trashBtn.TabIndex = 3;
			this.trashBtn.TabStop = false;
			// 
			// undoBtn
			// 
			this.undoBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
			this.undoBtn.Image = global::BigPicture.Properties.Resources.Root_UndoBtn;
			this.undoBtn.Location = new System.Drawing.Point(0, 0);
			this.undoBtn.Name = "undoBtn";
			this.undoBtn.Size = new System.Drawing.Size(40, 40);
			this.undoBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.undoBtn.TabIndex = 5;
			this.undoBtn.TabStop = false;
			this.undoBtn.Tag = "";
			// 
			// hideBtn
			// 
			this.hideBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
			this.hideBtn.Image = global::BigPicture.Properties.Resources.Root_HideBtn;
			this.hideBtn.Location = new System.Drawing.Point(0, 100);
			this.hideBtn.Name = "hideBtn";
			this.hideBtn.Size = new System.Drawing.Size(40, 100);
			this.hideBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.hideBtn.TabIndex = 1;
			this.hideBtn.TabStop = false;
			this.hideBtn.Tag = "";
			// 
			// addBtn
			// 
			this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
			this.addBtn.Image = global::BigPicture.Properties.Resources.Root_PlusBtn;
			this.addBtn.Location = new System.Drawing.Point(0, 0);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(40, 100);
			this.addBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.addBtn.TabIndex = 0;
			this.addBtn.TabStop = false;
			this.addBtn.Tag = "";
			// 
			// trayMenu_설정
			// 
			this.trayMenu_설정.Image = global::BigPicture.Properties.Resources.Tray_OptionIcon;
			this.trayMenu_설정.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.trayMenu_설정.Name = "trayMenu_설정";
			this.trayMenu_설정.Size = new System.Drawing.Size(154, 26);
			this.trayMenu_설정.Text = "설정";
			this.trayMenu_설정.Click += new System.EventHandler(this.OnTrayClick_설정);
			// 
			// trayMenu_업데이트
			// 
			this.trayMenu_업데이트.Image = global::BigPicture.Properties.Resources.Tray_UpdateIcon;
			this.trayMenu_업데이트.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.trayMenu_업데이트.Name = "trayMenu_업데이트";
			this.trayMenu_업데이트.Size = new System.Drawing.Size(154, 26);
			this.trayMenu_업데이트.Text = "업데이트 확인";
			this.trayMenu_업데이트.Click += new System.EventHandler(this.OnTrayClick_업데이트);
			// 
			// trayMenu_사용방법
			// 
			this.trayMenu_사용방법.Image = global::BigPicture.Properties.Resources.Tray_HelpIcon;
			this.trayMenu_사용방법.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.trayMenu_사용방법.Name = "trayMenu_사용방법";
			this.trayMenu_사용방법.Size = new System.Drawing.Size(154, 26);
			this.trayMenu_사용방법.Text = "사용 방법";
			this.trayMenu_사용방법.Click += new System.EventHandler(this.OnTrayClick_사용방법);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Violet;
			this.ClientSize = new System.Drawing.Size(1024, 300);
			this.ControlBox = false;
			this.Controls.Add(this.layerArea);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BigPicture";
			this.TransparencyKey = System.Drawing.Color.Violet;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnDestroy);
			this.layerArea.ResumeLayout(false);
			this.trashPanel.ResumeLayout(false);
			this.trayMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trashBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.undoBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hideBtn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.addBtn)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.PictureBox addBtn;
		public System.Windows.Forms.PictureBox hideBtn;
		public System.Windows.Forms.Panel layerArea;
		public System.Windows.Forms.PictureBox trashBtn;
		private System.Windows.Forms.ContextMenuStrip trayMenu;
		public System.Windows.Forms.ToolStripMenuItem trayMenu_열기;
		public System.Windows.Forms.ToolStripMenuItem trayMenu_종료;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		public System.Windows.Forms.ToolStripMenuItem trayMenu_설정;
		public System.Windows.Forms.ToolStripMenuItem trayMenu_사용방법;
		public System.Windows.Forms.NotifyIcon trayIcon;
		public System.Windows.Forms.Panel trashPanel;
		private System.Windows.Forms.ToolStripMenuItem trayMenu_업데이트;
		private System.Windows.Forms.ToolStripMenuItem trayMenu_정보;
		public System.Windows.Forms.PictureBox undoBtn;
	}
}


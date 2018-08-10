using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BigPicture.Graphic;
using BigPicture.Properties;

namespace BigPicture.Forms {
	public partial class ProgramInfo : Form {
		public static ProgramInfo instance;

		PictureBox panel, closeBtn;
		AAText versionText;

		public ProgramInfo() {
			instance = this;
			InitializeComponent();
			Initialize();
		}
		private void Initialize() {
			CreateControls();
		}
		private void CreateControls() {
			SuspendLayout();

			panel = new PictureBox();
			panel.SizeMode = PictureBoxSizeMode.AutoSize;
			panel.Image = Resources.Info_Panel;
			panel.SetDragable(this);
			Size = panel.Size;
			Controls.Add(panel);

			//닫기버튼
			closeBtn = new PictureBox();
			closeBtn.Cursor = Cursors.Hand;
			closeBtn.SizeMode = PictureBoxSizeMode.AutoSize;
			closeBtn.Image = Resources.Option_CloseBtn;
			closeBtn.Location = new Point(536, Height - 46);
			closeBtn.SetMouseEvent(Color.White, Color.White.Lighting(-10), Color.White.Lighting(-20));
			closeBtn.Click += (object sender, EventArgs e) => {
				Close();
			};
			panel.Controls.Add(closeBtn);

			//버전
			versionText = new AAText();
			versionText.Location = new Point(38, 80);
			versionText.Font = FontManager.스웨거체;
			versionText.FontSize = 14;
			versionText.Text = AppInfo.version;
			versionText.color = Color.FromArgb(64, 64, 64);
			panel.Controls.Add(versionText);

			ResumeLayout();
		}
		private void OnDestroy(object sender, FormClosingEventArgs e) {
			instance = null;
		}

	}
}

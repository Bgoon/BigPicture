using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BigPicture.Properties;

namespace BigPicture.Forms {
	public partial class Tutorial : Form {
		public static Tutorial instance;
		public Tutorial() {
			instance = this;
			InitializeComponent();
			Initialize();
		}

		Image[] pages = {
				Resources.Tutorial_Page0,
				Resources.Tutorial_Page1,
				Resources.Tutorial_Page2,
				Resources.Tutorial_Page3,
				Resources.Tutorial_Page4,
				Resources.Tutorial_Page5,
				Resources.Tutorial_Page6};
		int MaxPage {
			get {
				return pages.Length;
			}
		}
		PictureBox leftBtn, rightBtn, exitBtn;
		PictureBox[] indicators;
		Panel contentArea, scrollArea;
		Scroller scroller;

		private void Initialize() {
			CreateControls();
			SetEvent();
			panel.SetDragable(this);
		}
		private void CreateControls() {
			SuspendLayout();

			scroller = new Scroller(this);

			indicators = new PictureBox[MaxPage];
			for(int i=0; i<MaxPage; i++) {
				indicators[i] = new PictureBox();
				indicators[i].SizeMode = PictureBoxSizeMode.AutoSize;
				int indicatorWidth = Resources.Tutorial_State_on.Width;
				indicators[i].Location = new Point((Width - indicatorWidth * MaxPage) / 2 + indicatorWidth * i, 318);
				indicators[i].Enabled = false;
				panel.Controls.Add(indicators[i]);
			}

			leftBtn = new PictureBox();
			leftBtn.SizeMode = PictureBoxSizeMode.AutoSize;
			leftBtn.Image = Resources.Tutorial_LeftBtn_on;
			leftBtn.SetMouseEvent(Resources.Tutorial_LeftBtn_on, Resources.Tutorial_LeftBtn_over, Resources.Tutorial_LeftBtn_down);
			leftBtn.Location = new Point(14, 308);
			panel.Controls.Add(leftBtn);

			rightBtn = new PictureBox();
			rightBtn.SizeMode = PictureBoxSizeMode.AutoSize;
			rightBtn.Image = Resources.Tutorial_RightBtn_on;
			rightBtn.SetMouseEvent(Resources.Tutorial_RightBtn_on, Resources.Tutorial_RightBtn_over, Resources.Tutorial_RightBtn_down);
			rightBtn.Location = new Point(294, 308);
			panel.Controls.Add(rightBtn);

			exitBtn = new PictureBox();
			exitBtn.SizeMode = PictureBoxSizeMode.AutoSize;
			exitBtn.Image = Resources.Tutorial_ExitBtn_on;
			exitBtn.SetMouseEvent(Resources.Tutorial_ExitBtn_on, Resources.Tutorial_ExitBtn_over, Resources.Tutorial_ExitBtn_down);
			exitBtn.Location = rightBtn.Location;
			exitBtn.Visible = false;
			panel.Controls.Add(exitBtn);

			contentArea = new Panel();
			contentArea.Size = new Size(291, 283);
			contentArea.Location = new Point(24, 22);
			contentArea.Enabled = false;
			Controls.Add(contentArea);

			scrollArea = new Panel();
			scrollArea.Size = new Size(contentArea.Width * (MaxPage + 1), contentArea.Height);
			contentArea.Controls.Add(scrollArea);
			
			for (int i = 0; i < pages.Length; i++) {
				PictureBox page = new PictureBox();
				page.SizeMode = PictureBoxSizeMode.AutoSize;
				page.Location = new Point(contentArea.Width * i, 0);
				page.Image = pages[i];
				scrollArea.Controls.Add(page);
			}
			panel.SendToBack();
			scroller.SetPage(0);

			ResumeLayout();
		}
		private void OnDestroy(object sender, FormClosingEventArgs e) {
			instance = null;
		}
		private void SetEvent() {
			leftBtn.MouseClick += (object sender, MouseEventArgs e)=>{
				if(e.Button == MouseButtons.Left) {
					scroller.SetPage(scroller.page - 1);
				}
			};
			rightBtn.MouseClick += (object sender, MouseEventArgs e)=>{
				if(e.Button == MouseButtons.Left) {
					scroller.SetPage(scroller.page + 1);
				}
			};
			exitBtn.MouseClick += (object sender, MouseEventArgs e) => {
				if(e.Button == MouseButtons.Left) {
					Hide();
				}
			};
		}

		public void Reset() {
			scroller.SetPage(0);
		}

		private void SetIndicator(int page) {
			for(int i=0; i<MaxPage; i++) {
				if(i == page) {
					indicators[i].Image = Resources.Tutorial_State_on;
				} else {
					indicators[i].Image = Resources.Tutorial_State_off;
				}
			}
		}
		private class Scroller : UIObject {
			public Scroller(Tutorial form) {
				this.form = form;
			}
			Tutorial form;
			public int page;
			public float posX;
			public float DestX {
				get {
					return -form.contentArea.Width * page;
				}
			}
			public override bool Render() {
				bool requestDequeue = false;
				float delta = DestX - posX;
				if(Math.Abs(delta) < 1f) {
					posX = DestX;
					requestDequeue = true;
				} else {
					posX += delta * 0.2f;
				}

				form.scrollArea.Location = new Point((int)posX, 0);

				return !requestDequeue;
			}
			public void SetPage(int page) {
				this.page = page;
				if(page == 0) {
					form.leftBtn.Visible = false;
					form.exitBtn.Visible = false;
					form.rightBtn.Visible = true;
				} else if(page == form.MaxPage-1) {
					form.leftBtn.Visible = true;
					form.rightBtn.Visible = false;
					form.exitBtn.Visible = true;
				} else {
					form.leftBtn.Visible = form.rightBtn.Visible = true;
					form.exitBtn.Visible = false;
				}
				form.SetIndicator(page);
				Renderer.AddRenderQueue(this);
			}
		}

	}
}

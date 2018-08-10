using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BigPicture.Layers;
using BigPicture.Graphic;
using BigPicture.Properties;

namespace BigPicture.Forms {
	public partial class DetailPanel : Form {
		public DetailPanel() {
			InitializeComponent();
			Initialize();
			instance = this;
		}
		protected override CreateParams CreateParams {
			get {
				var Params = base.CreateParams;
				Params.ExStyle |= 0x80;
				return Params;
			}
		}
		public static DetailPanel instance;


		Layer attachedLayer;

		//날자표시
		ThreePatch panel;
		AAText text;
		

		private void Initialize() {
			ShowInTaskbar = false;

			CreateControls();
		}
		private void CreateControls() {
			SuspendLayout();

			panel = new ThreePatch(Resources.Detail_Panel_Left, Resources.Detail_Panel_Mid, Resources.Detail_Panel_Right);
			panel.BackColor = Color.Transparent;
			text = new AAText();
			text.BackColor = Color.FromArgb(41,41, 41);
			text.Font = FontManager.만화진흥원체;
			text.FontSize = 12;
			text.color = Color.White;
			text.Location = new Point(panel.left.Width, (panel.left.Height - text.Height) / 2 + 3);

			Controls.Add(panel);
			Controls.Add(text);
			text.BringToFront();

			ResumeLayout();
		}
		public void Render() {
			if(attachedLayer != null) {
				SuspendLayout();

				Show();

				Location = new Point(MainForm.instance.Location.X + Layer.LeftPadding + attachedLayer.LeftSpace + attachedLayer.Width + 5, MainForm.instance.Location.Y + MainForm.instance.layerArea.Location.Y + attachedLayer.group.Location.Y  + (Layer.Height - Height) / 2);
				if(attachedLayer.TargetDate.Year == DateTime.Now.Year) {
					text.Text = attachedLayer.TargetDate.ToString("M월 d일 ddd");
				} else {
					text.Text = attachedLayer.TargetDate.ToString("yyyy년 M월 d일 ddd");
				}
				
				text.UpdateWidth();
				Size = new Size(text.Width + panel.right.Width + 3, Height);
				panel.SetWidth(Width);

				ResumeLayout();
				Update();
			} else {
				Hide();
			}
		}

		//레이어에 붙이기
		public void AttachLayer(Layer layer) { //새 레이어에 부착
			if (attachedLayer != layer) {
				attachedLayer = layer;

				Render();
			}
		}
		public void ClearLayer() {
			attachedLayer = null;
			Render();
		}
	}
}

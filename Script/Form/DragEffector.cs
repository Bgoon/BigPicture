using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using BigPicture.Layers;
using BigPicture.Graphic;

namespace BigPicture.Forms {
	public class DragEffector : Panel {
		public DragEffector() : base() {
			Initialize();
		}

		const int outlineWidth = 3;
		Layer attachedLayer;
		Panel innerPanel;

		private void Initialize() {
			SuspendLayout();
			
			innerPanel = new Panel();
			innerPanel.Location = new Point(outlineWidth, outlineWidth);
			Controls.Add(innerPanel);
			
			innerPanel.BackColor = Color.White;
			Visible = false;

			ResumeLayout();
		}
		public void Render() {
			if (attachedLayer != null) {
				SuspendLayout();
				
				Visible = true;
				BackColor = attachedLayer.BGColor;

				int width = Math.Abs(attachedLayer.ChangedWidth);
				if (attachedLayer.TargetDate.CompareTo(attachedLayer.pressedTime) >= 0) {
					Location = new Point(Layer.LeftPadding + attachedLayer.LeftSpace + attachedLayer.PressedWidth, MainForm.instance.layerArea.Location.Y + attachedLayer.group.Location.Y);
				} else {
					Location = new Point(Layer.LeftPadding + attachedLayer.LeftSpace +  attachedLayer.Width, MainForm.instance.layerArea.Location.Y + attachedLayer.group.Location.Y);
				}
				innerPanel.Size = new Size(width - outlineWidth * 2, Layer.Height - outlineWidth * 2);
				Size = new Size(width, Layer.Height);

				ResumeLayout();
			} else {
				ResumeLayout();
				Visible = false;
			}
		}

		//레이어에 붙이기
		public void AttachLayer(Layer layer) { //새 레이어에 부착
				if (attachedLayer != layer) {
					attachedLayer = layer;
					
					Render();
				}
		}
		public void DetachLayer(Layer layer) { //레이어로부터 떨어질때
				if (attachedLayer == layer) {
					attachedLayer = null;
					Render();
				}
		}
		public void ClearLayer() {
			attachedLayer = null;
			Render();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using BigPicture.Layers;
using BigPicture.Properties;
using BigPicture.Graphic;

namespace BigPicture.Forms {
	public partial class LayerNamer : Panel {
		public Panel backPanel;
		public ThreePatch textBoxBG;
		public AATextBox textBox;
		public PictureBox renameBtn;
		public bool isEditing;
		private const int MaxWidth = 200;

		private Layer attachedLayer;

		public LayerNamer() : base() {
			Initialize();
			SetBtnEvent();
		}
		private void Initialize() {

			textBox = new AATextBox();
			renameBtn = new PictureBox();

			renameBtn.SizeMode = PictureBoxSizeMode.AutoSize;
			renameBtn.Image = Resources.Layer_RenameBtn_on;

			textBoxBG = new ThreePatch(Resources.Layer_TextBox_Left, Resources.Layer_TextBox_mid, Resources.Layer_TextBox_Right);
			textBox.BorderStyle = BorderStyle.None;
			textBox.Font = new Font(FontManager.스웨거체.FontFamily, 16);
			textBox.BackColor = Color.FromArgb(235, 235, 235);
			textBox.Location = new Point(Layer.textPadding, (int)((textBoxBG.Height - textBox.Height) * 0.5f));
			textBoxBG.Visible = false;

			Controls.Add(textBoxBG);
			Controls.Add(renameBtn);
			textBoxBG.Controls.Add(textBox);
			textBoxBG.SetWidth(MaxWidth);
			textBoxBG.BringToFront();
			textBox.BringToFront();
			renameBtn.BringToFront();

			Size = new Size(0, textBoxBG.left.Height);
			Render();
		}

		//초기화
		private void SetBtnEvent() {
			Control[] controls = new Control[] { textBoxBG, textBox, renameBtn };
			for(int i=0; i<controls.Length; i++) {
			}
			renameBtn.MouseEnter += OnMouseEnter;
			renameBtn.MouseLeave += OnMouseExit;
			renameBtn.MouseDown += OnMouseDown;
			renameBtn.MouseMove += OnMouseMove;
			renameBtn.MouseUp += OnMouseUp;

			textBox.KeyDown += OnTextKeyDown;
		}

		//레이어에 붙이기
		public void AttachLayer(Layer layer) { //새 레이어에 부착
			if (!isEditing) {
				if (attachedLayer != layer) {
					attachedLayer = layer;

					renameBtn.Image = Resources.Layer_RenameBtn_on;
					
					Render();
				}
			}
		}
		public void DetachLayer(Layer layer) { //레이어로부터 떨어질때
			if (!isEditing) {
				if (attachedLayer == layer) {
					attachedLayer = null;
					Render();
				}
			}
		}
		public void ClearLayer() {
			if (isEditing && attachedLayer != null) {
				ApplyText();
			}
			attachedLayer = null;
			isEditing = false;
			textBoxBG.Visible = false;
			Render();
		}

		//외부함수
		public void Render() {
			if (attachedLayer != null && !attachedLayer.isPeriodPressed) {
				SuspendLayout();

				Visible = true;

				textBoxBG.Visible = isEditing;
				int textWidth = attachedLayer.mainText.Width;
				Point layerPos = new Point(Layer.LeftPadding, (int)(attachedLayer.IndexY * Layer.Height) + MainForm.instance.layerArea.Location.Y);
				BackColor = attachedLayer.BGColor;

				if (isEditing) {
					Location = new Point(
						layerPos.X + attachedLayer.LeftSpace + Layer.textPadding,
						layerPos.Y + (int)((Layer.Height - textBoxBG.Height) * 0.5f));
					int textBoxBGWidth = 0;
					if (attachedLayer.mainText.Width > MaxWidth) {
						textBoxBGWidth = textWidth;
					} else {
						textBoxBGWidth = Math.Min(MaxWidth, attachedLayer.mainText.maxWidth);
					}
					textBoxBG.SetWidth(textBoxBGWidth);
					textBox.Size = new Size(textBoxBG.Width - 10, Height);
					textBox.Text = attachedLayer.mainText.Text;
					textBox.Select(textBox.Text.Length, 0);
					renameBtn.Location = new Point(textBoxBG.Width, 0);
					Size = new Size(renameBtn.Location.X + renameBtn.Width, Height);
					textBox.Focus();
				} else {
					Location = new Point(
						layerPos.X + attachedLayer.LeftSpace + Layer.textPadding + textWidth,
						layerPos.Y + (int)((Layer.Height - textBoxBG.Height) * 0.5f));
					renameBtn.Location = new Point(0, 0);
					Size = new Size(renameBtn.Width, Height);
					renameBtn.Focus();
				}
				ResumeLayout();
				Refresh();
			} else {
				Visible = false;
			}
		}
		public void UpdateFont() {
			textBox.Font = new Font(Option.Display.Font.FontFamily, Option.Display.FontEMSize);
		}
		private void ApplyText() {
			attachedLayer.mainText.Text = textBox.Text;
			attachedLayer.mainText.UpdateWidth();
			attachedLayer.mainText.Invalidate();
		}

		//이벤트
		private void OnClick() {
			isEditing = !isEditing;
			if (!isEditing) {
				//레이어에 적용
				ApplyText();
			}
			Render();
		}

		//마우스이벤트
		private Point pressedPoint;
		private bool 
			isPressed, isDragging;
		private void OnMouseEnter(object sender, EventArgs e) {
			if (!isPressed) {
				if (isEditing) {
					renameBtn.Image = Resources.Layer_ApplyBtn_over;
				} else {
					renameBtn.Image = Resources.Layer_RenameBtn_over;
				}
			}
		}
		private void OnMouseExit(object sender, EventArgs e) {
			if (!isPressed) {
				if (isEditing) {
					renameBtn.Image = Resources.Layer_ApplyBtn_on;
				} else {
					renameBtn.Image = Resources.Layer_RenameBtn_on;
				}
			}
		}
		private void OnMouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				pressedPoint = Mouse.Position;
				isPressed = true;

				if (isEditing) {
					renameBtn.Image = Resources.Layer_ApplyBtn_down;
				} else {
					renameBtn.Image = Resources.Layer_RenameBtn_down;
				}
			}
		}
		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (isPressed) {
				if (!isDragging) {
					float mouseDistance = (float)Math.Sqrt(Math.Pow(Mouse.X - pressedPoint.X, 2) + Math.Pow(Mouse.Y - pressedPoint.Y, 2));
					if (mouseDistance > Mouse.ClickRange) {
						isDragging = true;
					}
				} else {
					//드래그

				}
			}
		}
		private void OnMouseUp(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				if (!isDragging) {
					OnClick();
				}
				isPressed = isDragging = false;

				if (isEditing) {
					renameBtn.Image = Resources.Layer_ApplyBtn_on;
				} else {
					renameBtn.Image = Resources.Layer_RenameBtn_on;
				}
			}
		}


		//키보드이벤트
		private void OnTextKeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					OnClick();

					e.SuppressKeyPress = e.Handled = true;
					break;
			}
			
		}
	}
}

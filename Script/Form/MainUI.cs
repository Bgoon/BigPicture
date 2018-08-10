using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using BigPicture.Layers;

namespace BigPicture.Forms {
	public class MainUI : UIObject {
		float 
			addBtnPosY,
			hideBtnPosY,
			btnSizeY;
		bool ShowUndoBtn {
			get {
				return RemoveStack.stack.Count > 0;
			}
		}
		int UndoPadding {
			get {
				return ShowUndoBtn ? undoBtn.Height : 0;
			}
		}

		MainForm mainForm;
		PictureBox 
			undoBtn,
			addBtn,
			hideBtn,
			trashBtn;
		Panel trashPanel;

		public MainUI() {
			Initialize();
			SetBtnEvent();
		}
		private void Initialize() {
			mainForm = MainForm.instance;
			undoBtn = mainForm.undoBtn;
			addBtn = mainForm.addBtn;
			hideBtn = mainForm.hideBtn;
			trashBtn = mainForm.trashBtn;
			trashPanel = mainForm.trashPanel;
		}
		private void SetBtnEvent() {
			PictureBox[] btns = { undoBtn, addBtn, hideBtn };
			for(int i=0; i<btns.Length; i++) {
				btns[i].SetMouseEvent(Color.FromArgb(82, 82, 82), Color.FromArgb(100, 100, 100), Color.FromArgb(70, 70, 70));
				btns[i].MouseDown += OnBtnMouseDown;
				btns[i].MouseMove += OnBtnMouseMove;
				btns[i].MouseUp += OnBtnMouseUp;
			}
			undoBtn.Click += OnUndoBtnClick;
			addBtn.Click += OnAddBtnClick;
			hideBtn.Click += OnHideBtnClick;
		}

		bool isPressed, isDragging;
		Point pressedPoint, originPoint;
		private void OnBtnMouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				pressedPoint = Cursor.Position;
				originPoint = MainForm.instance.Location;
			}
		}
		private void OnBtnMouseMove(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				int deltaX = Cursor.Position.X - pressedPoint.X,
					deltaY = Cursor.Position.Y - pressedPoint.Y;
				int dragDistance = (int)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
				if (dragDistance > 13) {
					isDragging = true;
				}
				if (isDragging) {
					MainForm.instance.Location = new Point(
						originPoint.X + deltaX, 
						originPoint.Y + deltaY);
				}
			}
		}
		private void OnBtnMouseUp(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				isPressed = isDragging = false;
			}
		}
		private void OnUndoBtnClick(object sender, EventArgs e) {
			if (isDragging) {
				return;
			}
			RemoveStack.Pop();
		}
		private void OnAddBtnClick(object sender, EventArgs e) {
			if(isDragging) {
				return;
			}
			Layer layer = LayerManager.AddLayer();
			if (layer != null) {
				Renderer.AddRenderQueue(layer);
				layer.mainText.UpdateWidth();
			}
		}
		private void OnHideBtnClick(object sender, EventArgs e) {
			if (isDragging) {
				return;
			}
			MainForm.instance.Hide();
		}
		public override bool Render() {
			bool renderContinue;
			const int MinHeight = 180;
			mainForm.Size = new Size(1024, Math.Max(MinHeight, LayerManager.Count * Layer.Height + mainForm.layerArea.Location.Y));
			mainForm.layerArea.Size = new Size(1024, mainForm.Height - mainForm.layerArea.Location.Y);

			//버튼
			addBtn.SuspendLayout();
			hideBtn.SuspendLayout();
			
			undoBtn.Visible = ShowUndoBtn;
			
			int btnTargetSizeY = (mainForm.layerArea.Height - UndoPadding) / 2;
			float 
				addBtnPosDelta = addBtnPosY.Delta(UndoPadding, Option.Display.Speed),
				hideBtnPosDelta = hideBtnPosY.Delta(UndoPadding + btnTargetSizeY, Option.Display.Speed);
			addBtnPosY += addBtnPosDelta;
			hideBtnPosY += hideBtnPosDelta;
			btnSizeY += btnSizeY.Delta(btnTargetSizeY, Option.Display.Speed);

			if(Math.Abs(addBtnPosDelta) <= 0.1f && Math.Abs(hideBtnPosDelta) <= 0.1f) {
				btnSizeY = btnTargetSizeY;
				addBtnPosY = UndoPadding;
				hideBtnPosY = UndoPadding + btnTargetSizeY;
				renderContinue = false;
			} else {
				renderContinue = true;
			}

			//Apply
			addBtn.Location = new Point(0, (int)addBtnPosY);
			hideBtn.Size = addBtn.Size = new Size(Layer.LeftPadding, (int)btnSizeY);
			hideBtn.Location = new Point(0, (int)hideBtnPosY);
			trashPanel.Size = trashBtn.Size = new Size(Layer.LeftPadding, mainForm.Height - mainForm.layerArea.Location.Y);

			addBtn.ResumeLayout();
			hideBtn.ResumeLayout();

			return renderContinue;
		}
	}
	public class TrashArea : UIObject {
		public static TrashArea instance;
		public bool isOpened;
		public bool Visible {
			set {
				MainForm.instance.trashPanel.Visible = value;
			}
		}
		const float speed = 0.4f;
		float width = Layer.LeftPadding;
		public override bool Render() {
			MainForm mainForm = MainForm.instance;

			mainForm.trashBtn.SuspendLayout();

			float targetWidth;
			if(isOpened) {
				targetWidth = Layer.LeftPadding / 8;
			} else {
				targetWidth = Layer.LeftPadding;
			}
			bool requestDequeue = false;
			float delta = targetWidth - width;
			if(Math.Abs(delta) < 0.1f) {
				width = targetWidth;
				requestDequeue = true;
			}
			width += delta * speed;

			mainForm.trashBtn.Size = new Size((int)Math.Round(width), mainForm.trashPanel.Height);

			mainForm.trashBtn.ResumeLayout();

			if (requestDequeue) {
				return false;
			} else {
				return true;
			}
		}
		public void Show() {
			MainForm.instance.trashPanel.Visible = true;
		}
		public void Hide() {
			MainForm.instance.trashPanel.Visible = false;
		}
		public void Open() {
			isOpened = true;
			Renderer.AddRenderQueue(this);
		}
		public void Close() {
			isOpened = false;
			Renderer.AddRenderQueue(this);
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using BigPicture.Properties;
using BigPicture.Graphic;
using BigPicture.Forms;
using BigPicture.IO;
using BigPicture.Layers;

namespace BigPicture {
	public partial class OptionForm : Form {
		public static OptionForm instance;

		PictureBox closeBtn;
		Scroller scroller;
		PictureBox scrollBar;
		Panel separator;
		Panel contentArea;
		Panel scrollArea;
		MenuButton[] buttons;
		ScrollItem[] panels;
		public int heightStack;

		//시스템
		CheckBox startProcessCheckbox;
		CheckBox topMostCheckbox;
		CheckBox
			alertCheckbox,
			alertSoundCheckbox,
			alert_comCheckbox, 
			alert_1hCheckbox, 
			alert_1dCheckbox;
		//화면
		DragBar opacityBar;
		RadioBtnSet fontSelector;
		Button fontInspector;
		RadioBtnSet widthOffsetSelector;
		RadioBtnSet fontSizeSelector;
		RadioBtnSet dateTypeSelector;
		RadioBtnSet animSpeedSelector;
		//데이터
		Button saveBtn;
		Button loadBtn;

		public OptionForm() {
			InitializeComponent();
			instance = this;
		}
		private void OnLoad(object sender, EventArgs e) {
			scroller = new Scroller(this);
			CreateControls();
			ScrollTo(0);
		}
		private void OnDestroy(object sender, FormClosingEventArgs e) {
			instance = null;
		}
		private void CreateControls() {
			SuspendLayout();

#region 패널
			//분리선
			separator = new Panel();
			separator.Location = new Point(139, 12);
			separator.Size = new Size(1, 352);
			separator.BackColor = Color.FromArgb(202, 202, 202);
			separator.Enabled = false;

			//컨텐츠영역
			contentArea = new Panel();
			contentArea.Location = new Point(18, 21);
			contentArea.Size = new Size(636, 343);
			contentArea.BackColor = Color.White;

			//닫기버튼
			closeBtn = new PictureBox();
			closeBtn.Cursor = Cursors.Hand;
			closeBtn.SizeMode = PictureBoxSizeMode.AutoSize;
			closeBtn.Image = Resources.Option_CloseBtn;
			closeBtn.Location = new Point(30, 364);
			closeBtn.SetMouseEvent(Color.White, Color.White.Lighting(-10), Color.White.Lighting(-20));
			closeBtn.Click += (object sender, EventArgs e) => {
				Close();
			};

			//버튼
			const int btnPadding = 10;
			const int btnHeight = 31;
			buttons = new MenuButton[3];
			buttons[0] = new MenuButton(Resources.Option_SystemBtn_on, Resources.Option_SystemBtn_off);
			buttons[1] = new MenuButton(Resources.Option_DisplayBtn_on, Resources.Option_DisplayBtn_off);
			buttons[2] = new MenuButton(Resources.Option_DataBtn_on, Resources.Option_DataBtn_off);
			for(int i=0; i<buttons.Length; i++) {
				buttons[i].Cursor = Cursors.Hand;
				buttons[i].Location = new Point(5, (btnHeight + btnPadding) * i);
				contentArea.Controls.Add(buttons[i]);

				int index = i;
				buttons[i].Click += (object sender, EventArgs e) => {
					ScrollTo(index);
				};
			}

			//스크롤바
			scrollBar = new PictureBox();
			scrollBar.SizeMode = PictureBoxSizeMode.AutoSize;
			scrollBar.Image = Resources.Option_ScrollBar;
			scrollBar.Location = new Point(642 - contentArea.Location.X, 27);

			//패널
			const int itemPadding = 10;
			scrollArea = new Panel();
			scrollArea.Location = new Point(125, 0);

			panels = new ScrollItem[3];
			panels[0] = new ScrollItem(Resources.Option_SystemPanel);
			panels[1] = new ScrollItem(Resources.Option_DisplayPanel);
			panels[2] = new ScrollItem(Resources.Option_DataPanel);
			for(int i=0; i<buttons.Length; i++) {
				panels[i].SizeMode = PictureBoxSizeMode.AutoSize;
				panels[i].Location = new Point(0, heightStack);
				heightStack += panels[i].Height + itemPadding;
				scrollArea.Controls.Add(panels[i]);
			}
			scrollArea.Size = new Size(500, heightStack + contentArea.Height - panels.Last().Height - itemPadding);

			//마우스 휠 이벤트
			scrollArea.MouseWheel += (object sender, MouseEventArgs e) => {
				scroller.scrollMode = ScrollMode.Manual;
				scroller.speedY += e.Delta / 120 * 15;
				Renderer.AddRenderQueue(scroller);
			};
#endregion

			//시스템
			startProcessCheckbox = new CheckBox();
			startProcessCheckbox.Location = new Point(27, 105);
			topMostCheckbox = new CheckBox();
			topMostCheckbox.Location = new Point(27, 133);

			panels[0].Controls.Add(startProcessCheckbox);
			panels[0].Controls.Add(topMostCheckbox);

			startProcessCheckbox.OnSelected += (bool isChecked) => {
				Option.System.startProcess = isChecked;
				Option.OnOptionChanged();
			};
			topMostCheckbox.OnSelected += (bool isChecked) => {
				Option.System.topMost = isChecked;
				Option.OnOptionChanged();
			};
			//알림
			alertCheckbox = new CheckBox();
			alertCheckbox.Location = new Point(27, 222);
			alertSoundCheckbox = new CheckBox();
			alertSoundCheckbox.Location = new Point(27, 247);
			alert_comCheckbox = new CheckBox();
			alert_comCheckbox.Location = new Point(27, 296);
			alert_1hCheckbox = new CheckBox();
			alert_1hCheckbox.Location = new Point(213, 296);
			alert_1dCheckbox = new CheckBox();
			alert_1dCheckbox.Location = new Point(425, 296);
			panels[0].Controls.Add(alertCheckbox);
			panels[0].Controls.Add(alertSoundCheckbox);
			panels[0].Controls.Add(alert_comCheckbox);
			panels[0].Controls.Add(alert_1hCheckbox);
			panels[0].Controls.Add(alert_1dCheckbox);

			alertCheckbox.OnSelected += (bool isChecked) => {
				Option.System.alert = isChecked;
				Option.OnOptionChanged();
			};
			alertSoundCheckbox.OnSelected += (bool isChecked) => {
				Option.System.alertSound = isChecked;
				Option.OnOptionChanged();
			};
			alert_comCheckbox.OnSelected += (bool isChecked) => {
				Option.System.alert_com= isChecked;
				Option.OnOptionChanged();
			};
			alert_1hCheckbox.OnSelected += (bool isChecked) => {
				Option.System.alert_1h = isChecked;
				Option.OnOptionChanged();
			};
			alert_1dCheckbox.OnSelected += (bool isChecked) => {
				Option.System.alert_1d = isChecked;
				Option.OnOptionChanged();
			};



			//화면
			//투명도
			const int opacityBarY = 101;
			opacityBar = new DragBar(180);
			opacityBar.Location = new Point(26, opacityBarY);
			panels[1].Controls.Add(opacityBar);

			//폰트
			const int fontSelectorY = 181;
			fontSelector = new RadioBtnSet(2);
			fontSelector.radioBtn[0].Location = new Point(26, fontSelectorY);
			fontSelector.radioBtn[1].Location = new Point(26, fontSelectorY + 28);
			fontSelector.AddControl(panels[1]);
			fontInspector = new Button();
			fontInspector.Location = new Point(127, fontSelectorY + 26);
			panels[1].Controls.Add(fontInspector);

			//폰트크기
			const int fontSizeY = 277;
			fontSizeSelector = new RadioBtnSet(3);
			fontSizeSelector.radioBtn[0].Location = new Point(28, fontSizeY);
			fontSizeSelector.radioBtn[1].Location = new Point(188, fontSizeY);
			fontSizeSelector.radioBtn[2].Location = new Point(352, fontSizeY);
			fontSizeSelector.AddControl(panels[1]);

			//최소길이
			const int widthOffset = 372;
			widthOffsetSelector = new RadioBtnSet(3);
			widthOffsetSelector.radioBtn[0].Location = new Point(28, widthOffset);
			widthOffsetSelector.radioBtn[1].Location = new Point(188, widthOffset);
			widthOffsetSelector.radioBtn[2].Location = new Point(352, widthOffset);
			widthOffsetSelector.AddControl(panels[1]);

			//날자타입
			const int dateTypeY = 505;
			dateTypeSelector = new RadioBtnSet(3);
			dateTypeSelector.radioBtn[0].Location = new Point(28, dateTypeY);
			dateTypeSelector.radioBtn[1].Location = new Point(188, dateTypeY);
			dateTypeSelector.radioBtn[2].Location = new Point(352, dateTypeY);
			dateTypeSelector.AddControl(panels[1]);

			//애니스피드
			const int animSpeedY = 665;
			animSpeedSelector = new RadioBtnSet(3);
			animSpeedSelector.radioBtn[0].Location = new Point(28, animSpeedY);
			animSpeedSelector.radioBtn[1].Location = new Point(188, animSpeedY);
			animSpeedSelector.radioBtn[2].Location = new Point(352, animSpeedY);
			animSpeedSelector.AddControl(panels[1]);


			//이벤트
			bool isScrollbarPressed = false;
			int scrollbarPressedPos = 0, scrollbarOriginPos = 0;
			scrollBar.MouseDown += (object sender, MouseEventArgs e) => {
				if(e.Button == MouseButtons.Left) {
					isScrollbarPressed = true;
					scrollbarPressedPos = Cursor.Position.Y;
					scrollbarOriginPos = scrollBar.Location.Y;
				}
			};
			scrollBar.MouseMove += (object sender, MouseEventArgs e) => {
				if(isScrollbarPressed) {
					ScrollAreaPos(scrollbarOriginPos + Cursor.Position.Y - scrollbarPressedPos);
				}
			};
			scrollBar.MouseUp += (object sender, MouseEventArgs e) => {
				if(e.Button == MouseButtons.Left) {
					isScrollbarPressed = false;
				}
			};

			//화면
			opacityBar.OnDrag += (int percent) => {
				Option.Display.opacity = percent;
				Option.OnOptionChanged();
			};
			RadioBtnSet.IndexHandler fontSelectEvent = (int index) => {
				switch (index) {
					case 0:
						Option.Display.fontValue = index;
						break;
					case 1:
						Option.Display.fontValue = index;
						Option.Display.font = fontInspector.textArea.Font = this.fontDialog.Font;
						break;
				}
				Option.OnOptionChanged();
			};
			fontSelector.OnSelected += fontSelectEvent;
			fontInspector.Click += (object sender, EventArgs e) => {
				DialogResult result = this.fontDialog.ShowDialog();
				if (result == DialogResult.OK) {
					Option.Display.fontValue = 1;
					Option.Display.font = fontInspector.textArea.Font = this.fontDialog.Font;
					fontInspector.textArea.Text = this.fontDialog.Font.FontFamily.Name;
					Option.OnOptionChanged();

					fontSelector.SelectWithoutEvent(1);
				}
			};
			fontSizeSelector.OnSelected += (int index) => {
				Option.Display.fontSize = index;
				Option.OnOptionChanged();
			};
			widthOffsetSelector.OnSelected += (int index) => {
				Option.Display.widthOffset = index;
				Option.OnOptionChanged();
			};
			dateTypeSelector.OnSelected += (int index) => {
				Option.Display.dateType = index;
				Option.OnOptionChanged();
			};
			animSpeedSelector.OnSelected += (int index) => {
				Option.Display.animSpeed = index;
				Option.OnOptionChanged();
			};

			//데이터
			saveBtn = new Button("일정 저장");
			saveBtn.Location = new Point(29, 101);
			loadBtn = new Button("일정 불러오기");
			loadBtn.Location = new Point(29, 137);
			panels[2].Controls.Add(saveBtn);
			panels[2].Controls.Add(loadBtn);

			saveBtn.OnClick += () => {
				DialogResult result = dataSaveDialog.ShowDialog();
				if(result == DialogResult.OK) {
					SaveManager.Data.Save(new FileInfo(dataSaveDialog.FileName));
				}
			};
			loadBtn.OnClick += () => {
				DialogResult result = dataLoadDialog.ShowDialog();
				if(result == DialogResult.OK) {
					LayerManager.ClearLayer();
					SaveManager.Data.Load(new FileInfo(dataLoadDialog.FileName));
				}
			};


			Controls.Add(closeBtn);
			Controls.Add(separator);
			Controls.Add(contentArea);
			contentArea.Controls.Add(scrollArea);
			contentArea.Controls.Add(scrollBar);

			contentArea.BringToFront();
			separator.BringToFront();
			closeBtn.BringToFront();
			scrollBar.BringToFront();

			

			mainPanel.SetDragable(this);
			contentArea.SetDragable(this);
			scrollArea.SetDragable(this);
			for(int i=0; i<panels.Length; i++) {
				panels[i].SetDragable(this);
			}

			ResumeLayout();

			LoadOption();
		}

		private void LoadOption() {
			//시스템
			startProcessCheckbox.CheckWithoutEvent(Option.System.startProcess);
			topMostCheckbox.CheckWithoutEvent(Option.System.topMost);
			alertCheckbox.CheckWithoutEvent(Option.System.alert);
			alertSoundCheckbox.CheckWithoutEvent(Option.System.alertSound);
			alert_comCheckbox.CheckWithoutEvent(Option.System.alert_com);
			alert_1hCheckbox.CheckWithoutEvent(Option.System.alert_1h);
			alert_1dCheckbox.CheckWithoutEvent(Option.System.alert_1d);

			//화면
			opacityBar.SetValueWithoutEvent(Option.Display.opacity * 0.01f);
			fontSelector.SelectWithoutEvent(Option.Display.fontValue);
			fontInspector.textArea.Text = Option.Display.font.FontFamily.Name;
			fontDialog.Font =  fontInspector.textArea.Font = Option.Display.font;
			fontSizeSelector.SelectWithoutEvent(Option.Display.fontSize);
			widthOffsetSelector.SelectWithoutEvent(Option.Display.widthOffset);
			dateTypeSelector.SelectWithoutEvent(Option.Display.dateType);
			animSpeedSelector.SelectWithoutEvent(Option.Display.animSpeed);
		}

		private void ScrollTo(int index) {
			MenuBtnUpdate(index);
			scroller.SetDestY(index);
			scroller.scrollMode = ScrollMode.Auto;
			Renderer.AddRenderQueue(scroller);
		}
		private void MenuBtnUpdate(int index) {
			for(int i=0; i< buttons.Length; i++) {
				if (i == index) {
					buttons[i].On();
				} else {
					buttons[i].Off();
				}
			}
		}

		public enum ScrollMode {
			Auto,
			Manual,
		}
		public class Scroller : UIObject {
			public Scroller(OptionForm form) {
				this.form = form;
			}
			private OptionForm form;
			public float posY;
			public float speedY;
			public int destY;
			public ScrollMode scrollMode = ScrollMode.Auto;


			public void SetDestY(int menuIndex) {
				destY = -form.panels[menuIndex].Location.Y;
			}
			public override bool Render() {
				bool dequeueRequest = false;
				if(form == null) {
					return false;
				}

				form.SuspendLayout();

				switch (scrollMode) {
					default:
					case ScrollMode.Auto:
						float delta = destY - posY;
						posY += delta * 0.2f;

						if (Math.Abs(delta) < 1f) {
							posY = destY;
							dequeueRequest = true;
						}
						break;
					case ScrollMode.Manual:
						posY += speedY;
						
						speedY *= 0.8f;
						
						//버튼 활성화
						for(int i=0; i < form.panels.Length; i++) {
							if (-posY < form.panels[i].Bottom) {
								form.MenuBtnUpdate(i);
								break;
							}
						}

						if (Math.Abs(speedY) < 0.01f || posY > 0 || posY < -form.scrollArea.Height + form.contentArea.Height) {
							posY = Math.Max(-form.scrollArea.Height + form.contentArea.Height, Math.Min(0, posY));
							speedY = 0f;
							dequeueRequest = true;
						}
						
						break;
				}
				form.scrollArea.Location = new Point(form.scrollArea.Location.X, (int)posY);
				form.ScrollbarPos();

				form.ResumeLayout();
				return !dequeueRequest;
			}
		}
		private void ScrollDirect(int index) {
			scrollArea.Location = new Point(scrollArea.Location.X, -panels[index].Location.Y);
		}
		private void ScrollbarPos() {
			scrollBar.Location = new Point(scrollBar.Location.X, (int)(-scroller.posY / (scrollArea.Height - contentArea.Height) * (contentArea.Height - scrollBar.Height)));
		}
		private void ScrollAreaPos(int scrollbarPosY) {
			scroller.scrollMode = ScrollMode.Manual;
			int posY = (int)((float)-scrollbarPosY / (contentArea.Height - scrollBar.Height) * (scrollArea.Height - contentArea.Height));
			scroller.posY = scroller.destY = Math.Min(0, Math.Max(posY, -scrollArea.Height + contentArea.Height));
			ScrollbarPos();
			Renderer.AddRenderQueue(scroller);
		}


		private class MenuButton : PictureBox{
			public MenuButton(Image on, Image off) : base() {
				this.on = on;
				this.off = off;

				SizeMode = PictureBoxSizeMode.AutoSize;
				Image = on;
			}
			private Image on, off;

			public void On() {
				Image = on;
			}
			public void Off() {
				Image = off;
			}
		}
		private class ScrollItem : PictureBox {
			public ScrollItem(Image image) : base() {
				Image = image;
			}
		}
		private class RadioButton : PictureBox {
			private static Image
				on = Resources.Option_RadioBtn_checked,
				off = Resources.Option_RadioBtn_unchecked;
			public int Index {
				get {
					return index;
				}
			}
			private int index;
			
			public RadioButton(int index) : base() {
				this.index = index;
				SizeMode = PictureBoxSizeMode.AutoSize;
				Cursor = Cursors.Hand;
				Image = on;
			}

			public void On() {
				Image = on;
			}
			public void Off() {
				Image = off;
			}
		}
		private class RadioBtnSet {
			public int Length {
				get {
					return length;
				}
			}
			private int length;
			
			public RadioButton[] radioBtn;
			public delegate void IndexHandler(int index);
			public event IndexHandler OnSelected;

			public RadioBtnSet(int count) {
				length = count;
				radioBtn = new RadioButton[count];
				for (int i = 0; i < count; i++) {
					int index = i;
					radioBtn[i] = new RadioButton(i);
					if (i == 0) {
						radioBtn[i].On();
					} else {
						radioBtn[i].Off();
					}
					radioBtn[i].Click += (object sender, EventArgs e) => {
						SelectWithoutEvent(index);
						OnSelected?.Invoke(index);
					};
				}
			}
			public void AddControl(Control parent) {
				for(int i=0; i<length; i++) {
					parent.Controls.Add(radioBtn[i]);
				}
			}
			public void Select(int index) {
				SelectWithoutEvent(index);
				OnSelected?.Invoke(index);
			}
			public void SelectWithoutEvent(int index) {
				for(int i2=0; i2 < length; i2++) {
					if (i2 == index) {
						radioBtn[i2].On();
					} else {
						radioBtn[i2].Off();
					}
				}
			}
		}
		private class CheckBox : PictureBox {
			private static Image
				on = Resources.Option_Checkbox_checked,
				off = Resources.Option_Checkbox_unchecked;

			public bool IsChecked {
				get {
					return isChecked;
				}
			}
			private bool isChecked;
			public delegate void BoolHandler(bool isChecked);
			public event BoolHandler OnSelected;
			public CheckBox() : base() {
				SizeMode = PictureBoxSizeMode.AutoSize;
				Cursor = Cursors.Hand;
				isChecked = true;
				On();

				Click += OnClick;
			}
			private void OnClick(object sender, EventArgs e) {
				isChecked = !isChecked;
				if (isChecked) {
					On();
				} else {
					Off();
				}

				OnSelected?.Invoke(isChecked);
			}
			public void Check(bool check) {
				CheckWithoutEvent(check);

				OnSelected?.Invoke(isChecked);
			}
			public void CheckWithoutEvent(bool check) {
				isChecked = check;
				if (isChecked) {
					On();
				} else {
					Off();
				}
			}
			public void On() {
				Image = on;
			}
			public void Off() {
				Image = off;
			}
		}
		private class Button : PictureBox {
			public AAText textArea;
			public new Action OnClick;
			public Button() : this("") {
			}
			public Button(string text) : base() {
				SizeMode = PictureBoxSizeMode.AutoSize;
				Image = Resources.Option_Button_on;
				this.SetMouseEvent(Resources.Option_Button_on, Resources.Option_Button_over, Resources.Option_Button_down);
				textArea = new AAText();
				textArea.Font = FontManager.만화진흥원체;
				textArea.FontSize = 9;
				textArea.color = Color.FromArgb(82, 82, 82);
				textArea.Text = text;
				textArea.BackColor = Color.Transparent;
				textArea.Enabled = false;
				textArea.maxWidth = 85;
				textArea.Location = new Point(8, 8);

				Controls.Add(textArea);


				Click += (object sender, EventArgs e) => {
					OnClick?.Invoke();
				};
			}
			public void SetText(string text) {
				textArea.Text = text;
			}
		}
		private class DragBar : Panel {
			public delegate void PercentHandler(int percent);
			public event PercentHandler OnDrag;

			private PixelImage bar;
			private PictureBox btn;
			private ThreePatch bg;
			private int barWidth;
			public float Value {
				get;
				private set;
			}
			public int BarWidth {
				get {
					return barWidth;
				} set {
					barWidth = value;
					bg.SetWidth(barWidth);
					bg.Size = new Size(bg.Width, Resources.Option_DragBarBtn.Height);
					Size = new Size(barWidth, bg.Height);
				}
			}
			private int BtnMaxX {
				get {
					return (BarWidth - btn.Width - Resources.Option_DragBarBG_Left.Width);
				}
			}
			private int BtnMinX {
				get {
					return Resources.Option_DragBarBG_Left.Width / 2;
				}
			}

			public DragBar(int width) : base() {
				bg = new ThreePatch(Resources.Option_DragBarBG_Left, Resources.Option_DragBarBG_Mid, Resources.Option_DragBarBG_Right);
				btn = new PictureBox();
				btn.SizeMode = PictureBoxSizeMode.AutoSize;
				btn.Image = Resources.Option_DragBarBtn;
				btn.Cursor = Cursors.Hand;
				bar = new PixelImage();
				bar.SizeMode = PictureBoxSizeMode.StretchImage;
				bar.Image = Resources.Option_DragBar_Mid;
				bar.Location = new Point(BtnMinX / 2, 0);
				BarWidth = width;

				bg.Controls.Add(bar);
				bg.Controls.Add(btn);
				Controls.Add(bg);

				bar.BringToFront();
				btn.BringToFront();

				SetEvent();
				SetValueWithoutEvent(1f);
			}
			private void SetEvent() {
				btn.MouseDown += OnMouseDown;
				btn.MouseMove += OnMouseMove;
				btn.MouseUp += OnMouseUp;
			}
			bool isPressed;
			int pressedPointX;
			int originX;
			private void OnMouseDown(object sender, MouseEventArgs e) {
				if(e.Button == MouseButtons.Left) {
					isPressed = true;
					pressedPointX = Cursor.Position.X;
					originX = btn.Location.X;
				}
			}
			private void OnMouseMove(object sender, MouseEventArgs e) {
				if (isPressed) {
					int deltaX = Cursor.Position.X - pressedPointX;
					SetValue((float)(originX + deltaX) / (BtnMaxX - BtnMinX));
					Update();
				}
			}
			private void OnMouseUp(object sender, MouseEventArgs e) {
				if (e.Button == MouseButtons.Left) {
					isPressed = false;
				}
			}

			public void SetValue(float value) {
				SetValueWithoutEvent(value);
				OnDrag?.Invoke((int)(Value * 100f));
			}
			public void SetValueWithoutEvent(float value) {
				Value = Math.Min(Math.Max(value, 0f), 1f);
				btn.Location = new Point((int)((BtnMaxX - BtnMinX) * Value + BtnMinX), btn.Location.Y);
				bar.Size = new Size(btn.Location.X + BtnMinX / 2, Resources.Option_DragBar_Mid.Height);
			}
		}

		
	}
}

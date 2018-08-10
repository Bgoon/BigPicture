using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

using BigPicture.Forms;
using BigPicture.Graphic;
using BigPicture.Properties;

namespace BigPicture.Layers {
	public class Layer : UIObject {
		public Layer() {
			targetDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
				.AddDays(7).AddHours(23);
			CreateControls();
			SetEvent();
		}
		public Panel group;
		public Panel backPanel;
		public Panel mainPanel;
		public AAText mainText;
		public AAText PeriodDaysText;
		public Panel dragArea;
		public TextBox memoText;

		//Info
		public DateTime TargetDate {
			get {
				return targetDate;
			} set {
				targetDate = value;
				OnPeriodDaysChanged();
			}
		}
		private DateTime targetDate;
		public DateTime targetChangeDate;
		/// <summary>
		/// 중요도(왼쪽 여백, y)
		/// </summary>
		public float IndexY = 0;
		private Color bgColor;
		private Bitmap bgImage; //확장성을 위해 가지고 있게 함
		public bool IsFocus {
			get {
				return LayerManager.focusedLayer == this;
			}
		}
		public bool alert_complete, alert_1h, alert_1d;

		//Property
		/// <summary>
		/// 남은 기간(길이)
		/// </summary>
		public int PeriodDays {
			get {
				return targetDate.Subtract(DateTime.Now).Days;
			}
		}
		public int PressedPeriodDays {
			get {
				return pressedTime.Subtract(DateTime.Now).Days;
			}
		}
		public int ChangedPeriodDays {
			get {
				return targetDate.Subtract(pressedTime).Days;
			}
		}
		public int PeriodHours {
			get {
				return targetDate.Subtract(DateTime.Now).Hours;
			}
		}
		public int PeriodMinutes {
			get {
				return targetDate.Subtract(DateTime.Now).Minutes;
			}
		}
		public bool isComplete {
			get {
				return targetDate.CompareTo(DateTime.Now) <= 0;
			}
		}
		public int LeftSpace {
			get {
				return (int)(IndexY * scale * 0.4f); //On 0.4 Off 0
			}
		}
		public int Width {
			get {
				int width = Math.Min(MaxWidth, Math.Max(MinWidth, (int)((PeriodDays * scale) + MinWidth)));
				if (IsFocus) { //포커스일때
					int minWidth = textPadding * 4 + MainForm.instance.layerNamer.renameBtn.Width + mainText.OriginWidth + PeriodDaysText.Width;
					if (width < minWidth) { //글자가 잘렸으면 모두 보이게
						return minWidth;
					}
				}
				return width;
			}
		}
		public int PressedWidth {
			get {
				return Math.Min(MaxWidth, Math.Max(MinWidth, (int)((PressedPeriodDays * scale) + MinWidth)));
			}
		}
		public int ChangedWidth {
			get {
				return Width - PressedWidth;
			}
			//get {
			//	int width = (int)(Width - PressedWidth * scale));
			//	if (width < MinWidth) {
			//		return MinWidth - PressedWidth;
			//	} else if (width > MaxWidth) {
			//		return MaxWidth - PressedWidth;
			//	} else {
			//		return ChangedPeriodDays * scale;
			//	}
			//}
		}
		public int GetWidth(int periodDays) {
			return Math.Max(MinWidth, (int)((periodDays * scale) + MinWidth));
		}
		public Color BGColor {
			get {
				return bgColor;
			}
		}
		public Bitmap BGImage {
			get {
				return bgImage;
			}
		}

		//Common
		private const int MaxWidth = 750;
		private static int MinWidth {
			get {
				switch (Option.Display.widthOffset) {
					case 0:
						return 180;
					case 1:
					default:
						return 220;
					case 2:
						return 270;
				}
			}
		}
		public static int Height {
			get {
				return (int)(gridHeight * scale * 4);
			}
		}
		public static float gridHeight = 3;
		public static float speed = Option.Display.Speed;
		private const int scale = 10;

		public const int LeftPadding = 40;
		public const int textPadding = 5;
		private const int dragAreaWidth = 8;

		//Utility
		public bool showMemo;
		private float PosY;
		private float compareIndexY;
		private float MemoTargetWidth {
			get {
				if(showMemo) {
					return MainForm.instance.ClientRectangle.Width - LeftPadding - Width - LeftSpace;
				} else {
					return 0;
				}
			}
		}
		private float memoWidth;


		//초기화
		private void CreateControls() {
			group = new Panel();
			backPanel = new Panel();
			mainPanel = new Panel();
			mainText = new AAText();
			PeriodDaysText = new AAText();
			dragArea = new Panel();
			memoText = new TextBox();

			group.SuspendLayout();

			
			backPanel.BackColor = Color.FromArgb(245, 245, 245);
			group.BackColor = dragArea.BackColor = mainText.BackColor = PeriodDaysText.BackColor = Color.Transparent;
			int hue;
			if (LayerManager.layerList.Count > 0) {
				hue = (int)(LayerManager.layerList.Last().BGColor.GetHue() + Random.Next(0, 300));
				if(hue > 360) {
					hue -= 360;
				}
			} else {
				hue = Random.Next(0, 360);
			}
			SetColorHue(hue);

			dragArea.Size = new Size(dragAreaWidth, Height);
			dragArea.Cursor = Cursors.SizeWE; 
			
			PeriodDaysText.Font = FontManager.스웨거체;
			UpdateFont();
			mainText.FontSize = PeriodDaysText.FontSize = 16f;
			memoText.ForeColor = Color.Black;
			memoText.Font = new Font(memoText.Font.FontFamily, 10);
			
			memoText.BorderStyle = BorderStyle.None;
			memoText.ScrollBars = ScrollBars.Vertical;
			memoText.Multiline = true;
			memoText.Visible = false;
			
			Render();

			MainForm.instance.layerArea.Controls.Add(group);
			group.Controls.Add(backPanel);
			group.Controls.Add(mainPanel);
			group.Controls.Add(memoText);
			mainPanel.Controls.Add(mainText);
			mainPanel.Controls.Add(PeriodDaysText);
			mainPanel.Controls.Add(dragArea);

			UpdateFont();
			
			PeriodDaysText.BringToFront();
			dragArea.BringToFront();

			group.ResumeLayout();
		}
		private void SetEvent() {
			Control[] controls = new Control[] { group, backPanel, mainPanel, mainText, PeriodDaysText };

			for(int i=0; i<controls.Length; i++) {
				Control control = controls[i];
				control.MouseDown += OnMouseDown;
				control.MouseMove += OnMouseMove;
				control.MouseUp += OnMouseUp;
				control.MouseClick += OnMouseClick;
			}
			dragArea.MouseDown += OnPeriodDown;
			dragArea.MouseMove += OnPeriodMove;
			dragArea.MouseUp += OnPeriodUp;
		}

		//타이머
		private bool IsTimerTick {
			get {
				return timer == 0;
			}
		}
		private int timer;
		private const int timerTick = 4;

		//렌더링
		public override bool Render() {
			timer = timer >= timerTick-1 ? 0 : timer+1;
			bool renderContinue;

			//애니메이션 연산
			float posDelta = IndexY - PosY;
			PosY += posDelta * speed;

			float memoWidthDelta = MemoTargetWidth - memoWidth;
			memoWidth += memoWidthDelta * speed;

			if (Math.Abs(posDelta) < 0.01f && Math.Abs(memoWidthDelta) < 0.1f) {
				PosY = IndexY;

				memoWidth = MemoTargetWidth;
				memoText.Visible = showMemo;

				renderContinue = false;
			} else {
				renderContinue = true;
			}

			//컨트롤 배치 시작
			group.SuspendLayout();
			
			group.Size = (memoText.Visible) ? 
				new Size(MainForm.instance.ClientRectangle.Width - LeftPadding, Height) : new Size(LeftSpace + Width, Height);
			group.Location = new Point(LeftPadding, (int)(PosY * Height));
			if (!isComplete) { //마감 이전일 때

				switch (Option.Display.dateType) {
					case 0:
						if (PeriodDays > 0) {
							PeriodDaysText.Text = PeriodDays + "일";
						} else if (PeriodHours > 0) {
							PeriodDaysText.Text = PeriodHours + "시간";
						} else {
							PeriodDaysText.Text = PeriodMinutes + "분";
						}
						break;
					case 1:
						if (TargetDate.Date != DateTime.Now.Date) {
							PeriodDaysText.Text = targetDate.ToString("M월 d일");
						} else {
							if(targetDate.Hour > 12) {
								PeriodDaysText.Text = targetDate.ToString("오후 %h시");
							} else {
								PeriodDaysText.Text = targetDate.ToString("오전 %h시");
							}
						}
						break;
					case 2:
						if (TargetDate.Date != DateTime.Now.Date) {
							var current = DateTime.Now;
							var periodMonth = (targetDate.Year - current.Year) * 12 + targetDate.Month - current.Month;
							var periodYear = targetDate.Year - current.Year;
							
							DateTime weekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
							DateTime targetWeekStart = targetDate.AddDays(-(int)targetDate.DayOfWeek);
							int weekDistance = targetWeekStart.Subtract(weekStart).Days / 7;
							if(weekDistance <= 0) {
								PeriodDaysText.Text = targetDate.ToString("이번주 ddd");
							} else if(weekDistance <= 1) {
								PeriodDaysText.Text = targetDate.ToString("다음주 ddd");
							} else if(weekDistance <= 7) {
								PeriodDaysText.Text = weekDistance + targetDate.ToString("주 뒤 ddd");
							} else if(periodYear < 1) {
								PeriodDaysText.Text = periodMonth + "달 뒤";
							} else {
								PeriodDaysText.Text = periodYear + "년 뒤";
							}
						} else {
							if(targetDate.Hour > 12) {
								PeriodDaysText.Text = targetDate.ToString("오후 %h시");
							} else {
								PeriodDaysText.Text = targetDate.ToString("오전 %h시");
							}
						}
						break;
				}
			} else { //마감일 때
				PeriodDaysText.Text = "마감";
			}
			
			mainPanel.Location = new Point(LeftSpace, 0);
			
			mainText.Location = new Point(textPadding, (int)(Height * 0.5f - mainText.Height * 0.5f));
			dragArea.Location = new Point(Width - dragAreaWidth, 0);
			memoText.Size = new Size((int)memoWidth, Height);
			backPanel.Size = new Size(LeftSpace, Height);
			mainPanel.Size = new Size(Width, Height);

			int periodDays = ChangedPeriodDays;
			if (isPeriodPressed && periodDays >= 0) { //늘어날 때
				//PeriodDaysText.Location = new Point(GetWidth(PressedPeriodDays) - PeriodDaysText.Width - textPadding, mainText.Location.Y);
				PeriodDaysText.Location = new Point(PressedWidth - PeriodDaysText.Width - textPadding, (Height - PeriodDaysText.Height) / 2);
				memoText.Location = new Point(Width + LeftSpace, 0);
				mainText.maxWidth = PressedWidth - PeriodDaysText.Width - textPadding * 2 - 10;
			} else { //줄어들 때
				PeriodDaysText.Location = new Point(Width - PeriodDaysText.Width - textPadding, (Height - PeriodDaysText.Height) / 2);
				memoText.Location = new Point(Width + LeftSpace, 0);
				
				mainText.maxWidth = Width - PeriodDaysText.Width - textPadding * 2 - 10;
			}
			
			


			group.ResumeLayout();
			if (IsTimerTick) {
				mainText.Update();
				PeriodDaysText.Update();
			}

			return renderContinue;
		}
		public void UpdateFont() {
			mainText.Font = Option.Display.Font;
			mainText.FontSize = Option.Display.FontEMSize;
			Render();
			mainText.UpdateWidth();
		}
		/// <param name="hue">0f~360f</param>
		public void SetColorHue(int hue) {
			bgColor = ColorManager.HSVtoRGB(hue, 0.6f, 1f);
			memoText.BackColor = ColorManager.HSVtoRGB(hue, 0.2f, 0.9f); 
			PeriodDaysText.color = BGColor.Lighting(-80);
			mainPanel.BackColor = bgColor;
		}



		//이벤트
		private void OnPeriodDaysChanged() {
			Render();
			MainForm.instance.layerNamer.Render();
			mainText.UpdateWidth();
			mainText.Update();
			DetailPanel.instance.Render();
			MainForm.instance.dragEffector.Render();

			alert_complete = alert_1d = alert_1d = false;
		}
		private void OnDestroy() {
			MainForm.instance.layerNamer.DetachLayer(this);
			RemoveStack.stack.Push(this);
		}

		//마우스이벤트
		private Point pressedPoint;
		public bool isPanelPressed, isPanelDragging;
		private void OnMouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				memoText.Select(memoText.Text.Length, 0);
				showMemo = !showMemo;
				if (showMemo) {
					memoText.Visible = true;
				}
				Renderer.AddRenderQueue(this);
			}
		}
		private void OnMouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				CancelTask();
				isPanelPressed = true;
				pressedPoint = Mouse.Position;
				mainPanel.BackColor = bgColor.Lighting(-20);
				MainForm.instance.layerNamer.ClearLayer();
			}
		}
		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (isPanelPressed) {
				if (!isPanelDragging) {
					//클릭 or 드래그 검사
					float mouseDistance = (float)Math.Sqrt(Math.Pow(Mouse.X - pressedPoint.X, 2) + Math.Pow(Mouse.Y - pressedPoint.Y, 2));
					if (mouseDistance > Mouse.ClickRange) {
						isPanelDragging = true;
						TrashArea.instance.Show();
						MainForm.instance.trashBtn.Image = Resources.Root_trashBtn;
					}
				} else {
					//드래그
					//쓰레기통 버튼
					
					if (MainForm.instance.trashPanel.OnMouse()) {
						TrashArea.instance.Open();
					} else {
						TrashArea.instance.Close();
					}

					//인덱스 판정
					int relativeIndex = (int)(Mouse.Y - Height * 0.5f - MainForm.instance.layerArea.Location.Y - Height * (int)IndexY) / Height;

					IndexY += (relativeIndex * 1.5f);
					if(IndexY < 0) {
						IndexY = -0.0001f;
					} else if(IndexY > LayerManager.layerList.Count-1) {
						IndexY = LayerManager.layerList.Count-0.9999f;
					}

					//인덱스 변경
					if (IndexY != compareIndexY) {
						compareIndexY = IndexY;

						MainForm.instance.layerNamer.ClearLayer();
						LayerManager.Sort();
						LayerManager.RenderAllLayers();
					}
				}
			}
		}
		private void OnMouseUp(object sender, MouseEventArgs e) {
			if (e == null || e.Button == MouseButtons.Left) {
				mainPanel.BackColor = bgColor;
				TrashArea.instance.Hide();
				TrashArea.instance.Close();
				if (isPanelDragging) {
					if (MainForm.instance.trashPanel.OnMouse()) {
						OnDestroy();
						LayerManager.RemoveLayer(this);
					}
					Renderer.AddRenderQueue(MainForm.instance.trashArea);
				}

				isPanelPressed = isPanelDragging = false;
			}
		}

		public DateTime pressedTime;
		public bool isPeriodPressed;
		private int comparePeriod;
		private void OnPeriodDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				pressedPoint = Mouse.Position;
				pressedTime = targetDate;
				isPeriodPressed = true;
				//세부설정 버튼
				MainForm.instance.trashBtn.Image = Resources.Root_ScheduleBtn;
				TrashArea.instance.Show();

				MainForm.instance.layerNamer.ClearLayer();
				DetailPanel.instance.AttachLayer(this);
				MainForm.instance.dragEffector.AttachLayer(this);
			}
		}
		private void OnPeriodMove(object sender, MouseEventArgs e) {
			if (isPeriodPressed) {
				int distanceX = Mouse.X - pressedPoint.X;
				int relativePeriod = distanceX / (int)(scale);
				if (comparePeriod != relativePeriod) {
					comparePeriod = relativePeriod;
					TargetDate = pressedTime.AddDays(relativePeriod);
				}

				if (MainForm.instance.trashPanel.OnMouse()) {
					TrashArea.instance.Open();
				} else {
					TrashArea.instance.Close();
				}
			}
		}
		private void OnPeriodUp(object sender, MouseEventArgs e) {
			if (e == null || e.Button == MouseButtons.Left) {
				isPeriodPressed = false;
				TrashArea.instance.Close();
				TrashArea.instance.Hide();
				DetailPanel.instance.ClearLayer();
				MainForm.instance.dragEffector.ClearLayer();
				if (MainForm.instance.trashPanel.OnMouse()) {
					targetDate = pressedTime;
					Render();
					mainText.UpdateWidth();
					TimeDialog timeDialog = new TimeDialog(this);
					timeDialog.ShowDialog(MainForm.instance);
				}
				Render();
				mainText.UpdateWidth();
				MainForm.instance.BringToFront();
			}
		}

		public void CancelTask() {
			if (isPeriodPressed) {
				OnPeriodUp(null, null);
			}
			if (isPanelPressed) {
				OnMouseUp(null, null);
			}
		}
	}
}

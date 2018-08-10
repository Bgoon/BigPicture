using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using BigPicture.Graphic;
using BigPicture.Layers;
using BigPicture.Properties;

namespace BigPicture.Forms {
	public partial class TimeDialog : Form {
		public TimeDialog(Layer layer) {
			attachedLayer = layer;
			InitializeComponent();
			Initialize();
		}
		enum TimeType {
			YY,
			MM,
			DD,
			HH,
		}

		Layer attachedLayer;

		bool isApplyed;
		DateTime dateTime;
		AAText yyText, mmText, ddText, hhText;
		TimeType selectedType;
		const int gridHeight = 25;

		const int
			yyTextRight = 62,
			mmTextRight = 58,
			ddTextRight = 133,
			hhTextRIght = 245;


		//초기화
		private void Initialize() {
			CreateControls();
			SetEvent();
			dateTime = attachedLayer.pressedTime;
			Render();
		}
		private void CreateControls() {
			SuspendLayout();

			yyText = new AAText();
			mmText = new AAText();
			ddText = new AAText();
			hhText = new AAText();

			yyText.BackColor = mmText.BackColor = ddText.BackColor = hhText.BackColor = Color.Transparent;
			yyText.Cursor = mmText.Cursor = ddText.Cursor = hhText.Cursor = Cursors.SizeNS;
			yyText.Location = new Point(yyTextRight, 26);
			mmText.Location = new Point(mmTextRight, 54);
			ddText.Location = new Point(ddTextRight, 54);
			hhText.Location = new Point(hhTextRIght, 54);
			
			yyText.Font = mmText.Font = ddText.Font = hhText.Font = FontManager.만화진흥원체;
			yyText.FontSize = 16;
			mmText.FontSize = ddText.FontSize = hhText.FontSize = 24;
			yyText.color = mmText.color = ddText.color = hhText.color = Color.FromArgb(184, 184, 184);

			Controls.Add(yyText);
			Controls.Add(mmText);
			Controls.Add(ddText);
			Controls.Add(hhText);

			panelImage.SendToBack();
			hhText.BringToFront();

			ResumeLayout();
		}
		private void SetEvent() {
			yyText.SetMouseEvent(BackColor, BackColor.Lighting(15), BackColor.Lighting(-10));
			mmText.SetMouseEvent(BackColor, BackColor.Lighting(15), BackColor.Lighting(-10));
			ddText.SetMouseEvent(BackColor, BackColor.Lighting(15), BackColor.Lighting(-10));
			hhText.SetMouseEvent(BackColor, BackColor.Lighting(15), BackColor.Lighting(-10));
			PMBtn.SetMouseEvent(BackColor, BackColor.Lighting(15), BackColor.Lighting(-10));
			applyBtn.SetMouseEvent(applyBtn.BackColor, applyBtn.BackColor.Lighting(20), applyBtn.BackColor.Lighting(-10));
			cancelBtn.SetMouseEvent(cancelBtn.BackColor, cancelBtn.BackColor.Lighting(20), cancelBtn.BackColor.Lighting(-10));

			yyText.MouseDown += OnMouseDown;
			mmText.MouseDown += OnMouseDown;
			ddText.MouseDown += OnMouseDown;
			hhText.MouseDown += OnMouseDown;
			yyText.MouseDown += OnYearDown;
			mmText.MouseDown += OnMonthDown;
			ddText.MouseDown += OnDayDown;
			hhText.MouseDown += OnHourDown;
			yyText.MouseMove += OnMouseMove;
			mmText.MouseMove += OnMouseMove;
			ddText.MouseMove += OnMouseMove;
			hhText.MouseMove += OnMouseMove;
			yyText.MouseUp += OnMouseUp;
			mmText.MouseUp += OnMouseUp;
			ddText.MouseUp += OnMouseUp;
			hhText.MouseUp += OnMouseUp;
			PMBtn.Click += OnPMClick;
			applyBtn.Click += OnApplyClick;
			cancelBtn.Click += OnCancelClick;
		}

		private void Render() {
			yyText.Text = dateTime.Year.ToString();
			mmText.Text = dateTime.Month.ToString();
			ddText.Text = dateTime.Day.ToString();
			int hour = dateTime.Hour;
			if(hour == 0) {
				hhText.Text = "0";
			} else {
				hour = (hour > 12) ? hour - 12 : hour;
				hhText.Text = hour.ToString();
			}
			

			yyText.Location = new Point(yyTextRight - yyText.Width, yyText.Location.Y);
			mmText.Location = new Point(mmTextRight - mmText.Width, mmText.Location.Y);
			ddText.Location = new Point(ddTextRight - ddText.Width, ddText.Location.Y);
			hhText.Location = new Point(hhTextRIght - hhText.Width, hhText.Location.Y);

			if(dateTime.Hour >= 12) {
				PMBtn.Image = Resources.TimeDialog_PM;
			} else {
				PMBtn.Image = Resources.TimeDialog_AM;
			}
		}

		//이벤트
		//마우스이벤트
		bool isPressed;
		int pressedPointY;
		DateTime pressedTime, compareTime;
		private void OnYearDown(object sender, MouseEventArgs e) {
			selectedType = TimeType.YY;
		}
		private void OnMonthDown(object sender, MouseEventArgs e) {
			selectedType = TimeType.MM;
		}
		private void OnDayDown(object sender, MouseEventArgs e) {
			selectedType = TimeType.DD;
		}
		private void OnHourDown(object sender, MouseEventArgs e) {
			
			selectedType = TimeType.HH;
		}

		private void OnMouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				isPressed = true;
				pressedTime = dateTime;
				pressedPointY = Cursor.Position.Y;
			}
		}


		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (isPressed) {
				int distanceY = pressedPointY - Cursor.Position.Y;
				int relativeIndex = distanceY / gridHeight;
				try {
					switch (selectedType) {
						case TimeType.YY:
							dateTime = pressedTime.AddYears(relativeIndex);
							break;
						case TimeType.MM:
							dateTime = pressedTime.AddMonths(relativeIndex);
							break;
						case TimeType.DD:
							dateTime = pressedTime.AddDays(relativeIndex);
							break;
						case TimeType.HH:
							dateTime = pressedTime.AddHours(relativeIndex);
							break;
					}
				} catch(ArgumentOutOfRangeException ex) {
				}
				if(compareTime != dateTime) {
					compareTime = dateTime;
					Render();
				}
			}
		}
		private void OnMouseUp(object sender, MouseEventArgs e) {
			isPressed = false;
		}

		private void OnPMClick(object sender, EventArgs e) {
			if(dateTime.Hour >= 12) {
				dateTime = dateTime.AddHours(-12);
			} else {
				dateTime = dateTime.AddHours(12);
			}
			Render();
		}
		private void OnApplyClick(object sender, EventArgs e) {
			isApplyed = true;
			attachedLayer.TargetDate = dateTime;
			Close();
		}
		private void OnCancelClick(object sender, EventArgs e) {
			Close();
		}
		private void OnDestroy(object sender, FormClosingEventArgs e) {
			if(!isApplyed) {
				attachedLayer.TargetDate = attachedLayer.pressedTime;
			}
			Renderer.AddRenderQueue(attachedLayer);
		}
	}
}

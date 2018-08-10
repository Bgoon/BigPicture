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
using System.Media;

using BigPicture.Graphic;

namespace BigPicture.Forms {
	public partial class Alert : Form {
		private const int maxLength = 5;
		private const int pushTimeSec = 10;
		private static int alertCount;
		
		private bool isClosed;
		public RichTextBox text;

		public Alert() {
			InitializeComponent();
			Initialize();
		}
		private void Initialize() {
			text = new RichTextBox() {
				Location = new Point(64, 24),
				Size = new Size(285, 30),
				BorderStyle = BorderStyle.None,
				Multiline = false,
				ScrollBars = RichTextBoxScrollBars.None,
				BackColor = Color.White,
				Cursor = Cursors.Default,
				Font = new Font(FontManager.만화진흥원체.FontFamily, 11, FontStyle.Regular),
			};
			text.Enter += (object sender, EventArgs e) => {
				panel.Focus();
			};
			closeBtn.Cursor = Cursors.Hand;
			closeBtn.Click += (object sender, EventArgs e) => {
				Close();
			};
			Anim();
		}
		private async void Anim() {
			await Task.Delay(pushTimeSec * 1000);
			for(float alpha=1f; alpha>0; alpha-= 0.03f) {
				await Task.Delay(1000 / 60);
				if(isClosed) {
					return;
				}
				Opacity = alpha;
			}
			Close();
		}
		private new void Close() {
			if(isClosed) {
				return;
			}
			isClosed = true;
			alertCount--;
			base.Close();
		}

		private static Alert CreateAlert() {
			alertCount++;
			
			Point screenRightBot;
			Screen screen = Screen.FromControl(MainForm.instance);
				screenRightBot = new Point(
				screen.WorkingArea.Right, screen.WorkingArea.Bottom);
			Alert alert = new Alert();
			alert.Location = new Point(screenRightBot.X - alert.Width, screenRightBot.Y - alert.Height * alertCount);
			alert.TopMost = true;
			return alert;
		}
		/// <summary>
		/// 일정 마감 시
		/// </summary>
		public static void Show(string schedule) {
			Alert alert = CreateAlert();
			
			Color lightBlack = Color.FromArgb(20, 20, 20);
			Color orange = Color.FromArgb(255, 100, 54);
			if(schedule.Length > maxLength) {
				schedule = schedule.Substring(0, maxLength) + "…";
			}

			alert.text.AppendText("일정 ", lightBlack);
			alert.text.AppendText(schedule, orange);
			alert.text.AppendText(" 이 마감되었습니다.", lightBlack);
			alert.panel.Controls.Add(alert.text);
			alert.Show();
		}
		public static void Show(string schedule, int otherCount) {
			Alert alert = CreateAlert();
			
			Color lightBlack = Color.FromArgb(20, 20, 20);
			Color orange = Color.FromArgb(255, 100, 54);
			if(schedule.Length > maxLength) {
				schedule = schedule.Substring(0, maxLength) + "…";
			}

			alert.text.AppendText("일정 ", lightBlack);
			alert.text.AppendText(schedule, orange);
			alert.text.AppendText(" 외 " + otherCount + "개가 마감되었습니다.", lightBlack);
			alert.panel.Controls.Add(alert.text);
			alert.Show();
		}
		public static void Show(string schedule, string periodTime, string periodUnit) {
			Alert alert = CreateAlert();
			
			Color lightBlack = Color.FromArgb(20, 20, 20);
			Color orange = Color.FromArgb(255, 100, 54);
			if(schedule.Length > maxLength) {
				schedule = schedule.Substring(0, maxLength) + "…";
			}

			alert.text.AppendText("일정 ", lightBlack);
			alert.text.AppendText(schedule, orange);
			alert.text.AppendText(" 가 ", lightBlack);
			alert.text.AppendText(periodTime, orange);
			alert.text.AppendText(periodUnit, lightBlack);
			alert.text.AppendText(" 남았습니다.", lightBlack);
			alert.panel.Controls.Add(alert.text);
			alert.Show();
		}
		public static void Show(string schedule, int otherCount, string periodTime, string periodUnit) {
			Alert alert = CreateAlert();
			
			Color lightBlack = Color.FromArgb(20, 20, 20);
			Color orange = Color.FromArgb(255, 100, 54);
			if(schedule.Length > maxLength) {
				schedule = schedule.Substring(0, maxLength) + "…";
			}

			alert.text.AppendText("일정 ", lightBlack);
			alert.text.AppendText(schedule, orange);
			alert.text.AppendText(" 외 " + otherCount + "개가 ", lightBlack);
			alert.text.AppendText(periodTime, orange);
			alert.text.AppendText(periodUnit, lightBlack);
			alert.text.AppendText(" 남았습니다.", lightBlack);
			alert.panel.Controls.Add(alert.text);
			alert.Show();
		}

		public static void Sound() {
			Stream stream = Properties.Resources.Alert;
			SoundPlayer player = new SoundPlayer(stream);
			player.Play();
		}
	}
}

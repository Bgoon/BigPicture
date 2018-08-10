using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BigPicture {
	public static class Extension {
		public static void SetMouseEvent(this Control control, Color on, Color over, Color down) {
			control.MouseUp += (object sender, MouseEventArgs e) => {
				if (e.X < control.Width && e.X > 0 && e.Y < control.Height && e.Y > 0) {
					control.BackColor = over;
				} else {
					control.BackColor = on;
				}
			};
			control.MouseLeave += (object sender, EventArgs e) => {
				control.BackColor = on;
			};
			control.MouseEnter += (object sender, EventArgs e) => {
				control.BackColor = over;
			};
			control.MouseDown += (object sender, MouseEventArgs e) => {
				if (e.Button == MouseButtons.Left) {
					control.BackColor = down;
				}
			};
		}
		public static void SetMouseEvent(this PictureBox control, Image on, Image over, Image down) {
			control.MouseUp += (object sender, MouseEventArgs e) => {
				if (e.X < control.Width && e.X > 0 && e.Y < control.Height && e.Y > 0) {
					control.Image = over;
				} else {
					control.Image = on;
				}
			};
			control.MouseLeave += (object sender, EventArgs e) => {
				control.Image = on;
			};
			control.MouseEnter += (object sender, EventArgs e) => {
				control.Image = over;
			};
			control.MouseDown += (object sender, MouseEventArgs e) => {
				if (e.Button == MouseButtons.Left) {
					control.Image = down;
				}
			};
		}
		public static void SetDragable(this Control control, Control target) {
			bool isPressed = false;
			Point pressedPoint = new Point(),
				originPos = new Point();
			control.MouseDown += (object sender, MouseEventArgs e) => {
				if (e.Button == MouseButtons.Left) {
					isPressed = true;
					pressedPoint = Cursor.Position;
					originPos = target.Location;
				}
			};
			control.MouseMove += (object sender, MouseEventArgs e) => {
				if (isPressed) {
					Point distance = new Point(Cursor.Position.X - pressedPoint.X, Cursor.Position.Y - pressedPoint.Y);
					target.Location = new Point(originPos.X + distance.X, originPos.Y + distance.Y);
				}
			};
			control.MouseUp += (object sender, MouseEventArgs e) => {
				if (e.Button == MouseButtons.Left) {
					isPressed = false;
				}
			};
		}

		public static List<Control> GetAllControls(this Control containerControl) {
		List<Control> allControls = new List<Control>();

		Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
		queue.Enqueue(containerControl.Controls);

		while (queue.Count > 0) {
			Control.ControlCollection controls
						= (Control.ControlCollection)queue.Dequeue();
			if (controls == null || controls.Count == 0)
				continue;

			foreach (Control control in controls) {
				allControls.Add(control);
				queue.Enqueue(control.Controls);
			}
		}

		return allControls;
	}

		public static void Run(this Action action) {
			if(action != null) {
				action();
			}
		}

		public static float Lerp(this float value, float dest, float speed) {
			return value + (dest - value) * speed;
		}
		public static int Lerp(this int value, float dest, float speed) {
			return (int)(value + (dest - value) * speed);
		}
		public static float Delta(this float value, float dest, float speed) {
			return (dest - value) * speed;
		}
		public static int Delta(this int value, float dest, float speed) {
			return (int)((dest - value) * speed);
		}

		public static string StringBuild(params string[] strings) {
			StringBuilder builder = new StringBuilder();
			for(int i=0; i<strings.Length; i++) {
				builder.Append(strings[i]);
			}
			return builder.ToString();
		}
		public static void AppendText(this RichTextBox box, string text, Color color) {
			box.SelectionStart = box.TextLength;
			box.SelectionLength = 0;

			box.SelectionColor = color;
			box.AppendText(text);
			box.SelectionColor = box.ForeColor;
		}
	}
}

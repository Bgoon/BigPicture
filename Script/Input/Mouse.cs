using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BigPicture {
	public static class Mouse {
		public const float ClickRange = 13f;
		public static int X {
			get {
				return Cursor.Position.X - MainForm.instance.DesktopLocation.X;
			}
		}
		public static int Y {
			get {
				return Cursor.Position.Y - MainForm.instance.DesktopLocation.Y;
			}
		}
		public static Point Position {
			get {
				return new Point(X, Y);
			}
		}

		public static Point LocationOnClient(this Control control) {
			Point retval = new Point(0, 0);
			for (; control.Parent != null; control = control.Parent) {
				retval.Offset(control.Location);
			}
			return retval;
		}
		public static bool OnMouse(this Control control) {
			Point mouseLocalPos = control.PointToClient(Cursor.Position);
			if (mouseLocalPos.X >= 0 &&
				mouseLocalPos.X <= control.Width &&
				mouseLocalPos.Y >= 0 &&
				mouseLocalPos.Y <= control.Height) {
				return true;
			} else {
				return false;
			}
			/*
			Point controlWorldPos = control.LocationOnClient();
			Point mouseLocalPos = new Point(X - controlWorldPos.X, Y - controlWorldPos.Y);
			if(mouseLocalPos.X >= 0 &&
				mouseLocalPos.X <= control.Width &&
				mouseLocalPos.Y >= 0 &&
				mouseLocalPos.Y <= control.Height) {
				return true;
			} else {
				return false;
			}*/
		}
	}
}

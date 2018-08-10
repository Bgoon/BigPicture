using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BigPicture {
	public static class ProcessProtocol {
		public const int
			WM_ExitRequest = 0x4A,
			WM_ProcessHandle = 0x4B;
		
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
  		private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, IntPtr lParam);
		[DllImport("user32.dll", SetLastError=true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

		public static void SendWMMsg(IntPtr handle, uint windowMessage, Int32 wParam = 0) {
			SendMessage(handle, windowMessage, wParam, IntPtr.Zero);
		}
		public static void SendWMMsg(IntPtr handle, uint windowMessage, Int32 wParam, IntPtr lParam) {
			SendMessage(handle, windowMessage, wParam, lParam);
		}
	}
}
 
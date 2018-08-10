using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BigPicture.Forms {
	public static class SetDesktop {
		[DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string  lpClassName,string  lpWindowName);
        [DllImport("user32.dll")] 
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
        [DllImport("User32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		[DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern IntPtr GetParent(IntPtr hWnd);
		[DllImport("User32.dll")]
		public static extern IntPtr GetDesktopWindow();
		 [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

		const int GW_HWNDFIRST = 0;
		const int GW_HWNDLAST = 1;
		const int GW_HWNDNEXT = 2;
		const int GW_HWNDPREV = 3;
		const int GW_OWNER = 4;
		const int GW_CHILD = 5;

		public static void Set(IntPtr handle, bool attach) {
			if (attach) {
				IntPtr desktopHandle = FindWindow("Progman", "Program Manager");
				//desktopHandle = GetWindow(desktopHandle, GW_HWNDPREV);
				//desktopHandle = GetWindow(desktopHandle, GW_HWNDPREV);
				//MessageBox.Show(desktopHandle.ToString());
				SetParent(handle, desktopHandle);
				
			} else {
				SetParent(handle, GetDesktopWindow());
			}
		}
	}
}

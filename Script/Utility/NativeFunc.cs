using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public static class NativeFunc {
	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
	[DllImport("user32")]
	public static extern int UpdateWindow(IntPtr hwnd);
	[DllImport("user32")]
	public static extern int SetForegroundWindow(IntPtr hwnd);
	[DllImport("user32.dll")]
	public static extern int FindWindow(string lpClassName, string lpWindowName);
	[DllImport("user32.dll")]
    public static extern bool LockWindowUpdate(IntPtr hWndLock);

}
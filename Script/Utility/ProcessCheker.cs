using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

public static class ProcessChecker {
	public static bool IsSingleInstance() {
		Process[] processArray = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
		int currentID = Process.GetCurrentProcess().Id;

		for (int i = 0; i < processArray.Length; i++) {
			if (processArray[i].Id != currentID) {
				IntPtr handle = new IntPtr(NativeFunc.FindWindow(null, "BigPicture"));
				NativeFunc.SetForegroundWindow(handle);
				return false;
			}
		}
		return true;
	}
}
 
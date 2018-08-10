using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigPicture {
	static class Program {
		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main() {
			if (!ProcessChecker.IsSingleInstance()) {
				Application.Exit();
			} else {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(true);
				//Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
	}
}

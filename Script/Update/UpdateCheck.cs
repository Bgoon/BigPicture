using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

using BigPicture.IO;

namespace BigPicture {
	public static class UpdateCheck {
		public static async void StartCheck() {
			for (int tryCount = 0; tryCount < 3; tryCount++) {
				try {
					await Task.Delay(500);
					FileInfo newUpdaterInfo = new FileInfo(SaveManager.updaterInfo.FullName + ".new");
					if (newUpdaterInfo.Exists) {
						if (SaveManager.updaterInfo.Exists) {
							SaveManager.updaterInfo.Delete();
						}
						newUpdaterInfo.MoveTo(SaveManager.updaterInfo.FullName);
					}
					break;
				} catch {
					continue;
				}
			}
			Check(false);
		}
		public static void Check(bool showResult) {
			try {
				if (SaveManager.updaterInfo.Exists) {
					if (showResult) {
						Process.Start(SaveManager.updaterInfo.FullName, 
							AppInfo.version + ' ' + 
							"showResult" + ' ' +
							MainForm.instance.Handle.ToString());
					} else {
						Process.Start(SaveManager.updaterInfo.FullName, 
							AppInfo.version + ' ' + 
							"hideResult" + ' ' +
							MainForm.instance.Handle.ToString());
					}
				} else {
				}
			} catch (Exception e) {
				MessageBox.Show(
					"업데이트를 실패했습니다" +
					Environment.NewLine +
					e.Message);
			}
		}
	}
}

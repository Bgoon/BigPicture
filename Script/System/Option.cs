using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BigPicture.Layers;
using BigPicture.IO;
using BigPicture.Graphic;

namespace BigPicture {
	public static class Option {
		public static class System {
			public static bool
				startProcess = false,
				topMost = false;

			public static bool
				alert = true,
				alertSound = true,
				alert_1h = true,
				alert_1d = true,
				alert_com = true;
		}
		public static class Display {
			public static int opacity = 100;
			/// <summary>
			/// 0기본 1커스텀폰트
			/// </summary>
			public static int fontValue = 0;
			public static Font font = new Font("Arial", 9);
			public static Font Font {
				get {
					if (Option.Display.fontValue == 0 || Option.Display.font == null) {
						return FontManager.스웨거체;
					} else {
						return Option.Display.font;
					}
				}
			}

			/// <summary>
			/// 0크게 1중간 2작게
			/// </summary>
			public static int fontSize = 0;
			public static int FontEMSize {
				get {
					switch (Option.Display.fontSize) {
						default:
						case 0:
							return 20;
						case 1:
							return 17;
						case 2:
							return 14;
					}
				}
			}

			/// <summary>
			/// 0짧게 1중간 2길게
			/// </summary>
			public static int widthOffset = 1;

			/// <summary>
			/// 0일 단위 1날자 2주 단위
			/// </summary>
			public static int dateType = 0;

			/// <summary>
			/// 0빠름 1중간 2없음
			/// </summary>
			public static int animSpeed = 1;
			public static float Speed {
				get {
					switch (animSpeed) {
						default:
						case 0:
							return 0.4f;
						case 1:
							return 0.2f;
						case 2:
							return 1f;
					}
				}
			}
		}
		/*public static class Data {

		}*/

		public static void OnOptionChanged() {
			SaveManager.Startup.Set(AppInfo.projectName, System.startProcess);
			MainForm.instance.TopMost = System.topMost;

			MainForm.instance.Opacity = Display.opacity * 0.008f + 0.2f;
			Layer.speed = Display.Speed;
			LayerManager.UpdateFont();
			LayerManager.RenderAllLayers();
		}
	}
}

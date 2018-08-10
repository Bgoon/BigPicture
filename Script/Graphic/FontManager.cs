using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using BigPicture.Properties;

namespace BigPicture.Graphic {
	public static class FontManager {
		public static Font 스웨거체, 만화진흥원체, Koverwatch;
		public static PrivateFontCollection privateFonts = new PrivateFontCollection();
		 [DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
		public static void Initialize() {
			MarshalFontResource(Resources.Koverwatch);
			MarshalFontResource(Resources.KOMACON);
			MarshalFontResource(Resources.SDSwaggerTTF);
			Koverwatch = new Font(privateFonts.Families[0], 12);
			만화진흥원체 = new Font(privateFonts.Families[1], 12);
			스웨거체 = new Font(privateFonts.Families[2], 12);
		}
		private static void MarshalFontResource(byte[] fontData) {
			System.IntPtr data = Marshal.AllocCoTaskMem(fontData.Length);
			Marshal.Copy(fontData, 0, data, fontData.Length);

			uint cfonts = 0;
			AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cfonts);
			privateFonts.AddMemoryFont(data, fontData.Length);

			Marshal.FreeCoTaskMem(data);
		}
		public static float GetEmSize(this Graphics graphics, float fontSize) {
			return graphics.DpiY * fontSize / 72;
		}
	}
}
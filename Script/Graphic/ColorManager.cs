using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BigPicture.Graphic {
	public static class ColorManager {
		public static Color getHashColor(string String) {
			MD5 md5 = MD5.Create();
			byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(String));
			float hue = ((float)hash[0] + hash[1] + hash[2]) / 8;
			if (hue > 50) {
				hue -= 50;
			}
			hue *= 7;
			return HSVtoRGB(hue, 0.4f, 0.8f);
		}
		public static void RGBtoHSV(Color color, ref float hue, ref float saturation, ref float value) {
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1f - (1f * min / max);
			value = max / 255f;
		}
		public static Color HSVtoRGB(float hue, float saturation, float value) {
			if (hue < 0f) {
				hue = 0f;
			} else if (hue > 360f) {
				hue = 360f;
			}
			if (saturation < 0f) {
				saturation = 0f;
			} else if (saturation > 1f) {
				saturation = 1f;
			}
			if (value < 0f) {
				value = 0f;
			} else if (value > 1f) {
				value = 1f;
			}
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0)
				return Color.FromArgb(v, t, p);
			else if (hi == 1)
				return Color.FromArgb(q, v, p);
			else if (hi == 2)
				return Color.FromArgb(p, v, t);
			else if (hi == 3)
				return Color.FromArgb(p, q, v);
			else if (hi == 4)
				return Color.FromArgb(t, p, v);
			else
				return Color.FromArgb(v, p, q);
		}
		public static Color Lighting(this Color color, int value) {
			return Color.FromArgb(
				Math.Min(255, Math.Max(0, color.R + value)),
				Math.Min(255, Math.Max(0, color.G + value)),
				Math.Min(255, Math.Max(0, color.B + value)));
		}
		public static string ColorToHEX(Color color) {
			return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}
	}
}
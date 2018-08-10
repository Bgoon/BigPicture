using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using BigPicture.Graphic;

namespace BigPicture.Forms {
	public class PixelImage : PictureBox {
		protected override void OnPaint(PaintEventArgs p) {
			p.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			base.OnPaint(p);
		}
	}
	public class ThreePatch : Panel {
		public ThreePatch(Image leftImage, Image midImage, Image rightImage) : base() {
			BackColor = Color.Transparent;
			SetImage(leftImage, midImage, rightImage);
		}
		public void SetImage(Image leftImage, Image midImage, Image rightImage) {
			left = new PixelImage();
			mid = new PixelImage();
			right = new PixelImage();
			left.Image = leftImage;
			mid.Image = midImage;
			right.Image = rightImage;
			left.Parent = mid.Parent = right.Parent = this;
			left.SizeMode = right.SizeMode = PictureBoxSizeMode.AutoSize;
			mid.SizeMode = PictureBoxSizeMode.StretchImage;
			mid.SendToBack();
			SetWidth(50);
		}
		public void SetWidth(int width) {
			SuspendLayout();
			this.width = width;
			Size = new Size(width, left.Height);
			mid.Size = new Size(Width, Height);
			right.Location = new Point(width - right.Width, 0);
			ResumeLayout();
		}
		private int width;
		public PixelImage left, mid, right;

	}
	public class AATextBox : TextBox {
		public AATextBox() : base() {
			Font = new Font(FontManager.스웨거체.FontFamily, 20);
		}
		/*protected override void OnPaint(PaintEventArgs p) {
			p.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			base.OnPaint(p);
		}*/
	}
	public class AAText : Label {
		public AAText() : base() {
			BackColor = Color.White;
			AutoSize = true;
			UseCompatibleTextRendering = true;
			Font = FontManager.스웨거체;
			FontSize = 16;
		}
		public Color color = ColorPalett.Black;
		public int maxWidth = 0;
		public int OriginWidth {
			get {
				return originWidth;
			}
		}
		public bool IsLong {
			get {
				return isLong;
			}
		}
		public new string Text {
			get {
				return text;
			}
			set {
				displayText = base.Text = text = value;
			}
		}
		public string DisplayText {
			get {
				return displayText;
			}
		}
		public new Font Font {
			get {
				return font;
			}
			set {
				base.Font = new Font(value.FontFamily, base.Font.Size, value.Style);
				font = value;
			}
		}
		public float FontSize {
			get {
				return fontSize;
			}
			set {
				base.Font = new Font(base.Font.FontFamily, value, base.Font.Style);
				fontSize = value;
			}
		}

		private int originWidth;
		private bool isLong;
		private string text;
		private string displayText;
		private Font font;
		private float fontSize = 9;

		public void UpdateWidth() {
			base.Text = text;
			originWidth = Width;

			if (maxWidth <= 0) {
				displayText = text;
				isLong = false;
			} else {
				if (Width > maxWidth) {
					isLong = true;
					base.Text = "";
					const int readBlock = 1;
					int readedBlock;
					for (int i = 0; i < text.Length; i += readBlock) {
						int remainderCount = text.Length - i;
						if (remainderCount < readBlock) {
							readedBlock = remainderCount;
							base.Text += text.Substring(i, remainderCount);
						} else {
							readedBlock = readBlock;
							base.Text += text.Substring(i, readBlock);
						}

						if (Width > maxWidth) {
							if (i >= readedBlock) {
								base.Text = text.Substring(0, i - readedBlock);
							}
							base.Text += "…";
							displayText = base.Text;
							break;
						}
					}
				} else {
					base.Text = displayText = text;
				}
			}
			Update();
		}
		
		protected override void OnPaint(PaintEventArgs e) {
			if (string.IsNullOrEmpty(text) == false) {
				using (Brush foreBrush = new SolidBrush(color)) {
					using (StringFormat sf = new StringFormat()) {
						using (GraphicsPath gp = new GraphicsPath()) {
							gp.AddString(displayText + ' ', font.FontFamily, (int)font.Style,
							e.Graphics.GetEmSize(fontSize), ClientRectangle, sf);
							e.Graphics.ScaleTransform(1f, 1f);
							e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
							e.Graphics.FillPath(foreBrush, gp);
						}
					}
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteMaker
{
    public class SpriteLib
    {
		private static Bitmap rotateImage(Bitmap b, float angle)
		{
			int num = (int)Math.Sqrt(b.Width * b.Width + b.Height * b.Height);
			Bitmap bitmap = new Bitmap(b.Width, b.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.TranslateTransform((float)b.Width / 2f, (float)b.Height / 2f);
			graphics.RotateTransform(angle);
			graphics.TranslateTransform((0f - (float)b.Width) / 2f, (0f - (float)b.Height) / 2f);
			graphics.DrawImage(b, new Point(0, 0));
			return bitmap;
		}

		public static IEnumerable<Image> RotateImageClockwise(Image image, int imNum)
        {
			Bitmap bitmap = new Bitmap(image);
			float num = 0;
			float dispFac = 360 / imNum;

			for (int i = 0; i < imNum; i++)
			{
				Image spriteImage = rotateImage(bitmap, num);
				num += dispFac;
				yield return spriteImage;
			}
		}

		public static IEnumerable<Image> RotateImageAnti_clockwise(Image image, int imNum)
		{
			Bitmap bitmap = new Bitmap(image);
			float num = 0;
			float dispFac = 360 / imNum;
			for (int i = 0; i < imNum; i++)
			{
				Image spriteImage = rotateImage(bitmap, num);
				num -= dispFac;
				yield return spriteImage;
			}
		}
	}
}

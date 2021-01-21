using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpriteMaker
{
    public class SpriteLib
    {
		public static Color GetMostCommon(Bitmap bmp)
		{
			List<Color> colors = new List<Color>();

			//List all pixel colours
			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					colors.Add(bmp.GetPixel(x, y));
				}
			}

			//Return most occuring colour
			return colors.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
		}

		private static Bitmap RotateImage(Bitmap b, float angle)
		{
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
				Image spriteImage = RotateImage(bitmap, num);
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
				Image spriteImage = RotateImage(bitmap, num);
				num -= dispFac;
				yield return spriteImage;
			}
		}


	}
}

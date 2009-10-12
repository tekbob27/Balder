using System;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

namespace Balder.Core.Math
{
	public struct ColorVector
	{
		public float Red;
		public float Green;
		public float Blue;
		public float Alpha;

		public ColorVector(float red, float green, float blue, float alpha)
			: this()
		{
			Red = red;
			Green = green;
			Blue = blue;
			Alpha = alpha;
		}

		public UInt32 ToUInt32()
		{
			var red = (UInt32)(Red * 255f);
			var green = (UInt32)(Green * 255f);
			var blue = (UInt32)(Blue * 255f);
			var alpha = (UInt32)(Alpha * 255f);
			//alpha = 0xff;

			var color = (alpha << 24) | (red << 16) | (green << 8) | blue;
			return color;
		}


		public Color ToColor()
		{
			var red = (int)(Red * 255f);
			var green = (int)(Green * 255f);
			var blue = (int)(Blue * 255f);
			var alpha = (int)(Alpha * 255f);
			var color = Color.FromArgb((byte)alpha,
									   (byte)red,
									   (byte)green,
									   (byte)blue);
			return color;
		}

		public Color ToColorWithClamp()
		{
			Clamp(0f, 1f);

			var red = (int)(Red * 255f);
			var green = (int)(Green * 255f);
			var blue = (int)(Blue * 255f);
			var alpha = (int)(Alpha * 255f);

			if (red > 0xff)
			{
				red = 0xff;
			}
			if (green > 0xff)
			{
				green = 0xff;
			}
			if (blue > 0xff)
			{
				blue = 0xff;
			}
			if (alpha > 0xff)
			{
				alpha = 0xff;
			}

			var color = Color.FromArgb((byte)alpha,
									   (byte)red,
									   (byte)green,
									   (byte)blue);
			return color;
		}

		public void Clamp(float min, float max)
		{
			if (Red > max)
			{
				Red = max;
			}
			if (Red < min)
			{
				Red = min;
			}
			if (Green > max)
			{
				Green = max;
			}
			if (Green < min)
			{
				Green = min;
			}
			if (Blue > max)
			{
				Blue = max;
			}
			if (Blue < min)
			{
				Blue = min;
			}
			if (Alpha > max)
			{
				Alpha = max;
			}
			if (Alpha < min)
			{
				Alpha = min;
			}
		}

		public static ColorVector operator +(ColorVector v1, ColorVector v2)
		{
			return new ColorVector(v1.Red + v2.Red, v1.Green + v2.Green, v1.Blue + v2.Blue, v1.Alpha + v2.Alpha);
		}

		public static ColorVector operator *(ColorVector v1, float s2)
		{
			return new ColorVector(v1.Red * s2, v1.Green * s2, v1.Blue * s2, v1.Alpha * s2);
		}

		public static ColorVector operator *(float s2, ColorVector v1)
		{
			return new ColorVector(v1.Red * s2, v1.Green * s2, v1.Blue * s2, v1.Alpha * s2);
		}

	}
}

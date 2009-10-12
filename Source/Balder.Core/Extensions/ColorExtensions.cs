using System;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Math;

namespace Balder.Core.Extensions
{
	public static class ColorExtensions
	{
		public static ColorVector ToVector(this Color color)
		{
			var colorVector = new ColorVector(((float)color.R) / 255f, ((float)color.G) / 255f, ((float)color.B) / 255f, ((float)color.A) / 255f);
			return colorVector;
		}

		public static UInt32 ToUInt32(this Color color)
		{
			var uint32Color =	(((UInt32)color.A) << 24) |
								(((UInt32)color.R) << 16) |
								(((UInt32)color.G) << 8) |
								(UInt32)color.B;
			return uint32Color;
		}
	}
}

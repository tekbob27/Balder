#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Display;
using Balder.Core.Extensions;

namespace Balder.Core.SoftwareRendering
{
	public static class Shapes
	{
		public static void DrawLine(Viewport viewport, IBuffers buffers, int xstart, int ystart, int xend, int yend, Color color)
		{
			var colorAsInt = (int)color.ToUInt32();

			var deltaX = xend - xstart;
			var deltaY = yend - ystart;

			var lengthX = deltaX >= 0 ? deltaX : -deltaX;
			var lengthY = deltaY >= 0 ? deltaY : -deltaY;

			var actualLength = lengthX > lengthY ? lengthX : lengthY;

			if( actualLength != 0 )
			{
				var slopeX = (float) deltaX/(float) actualLength;
				var slopeY = (float) deltaY/(float) actualLength;

				var currentX = (float)xstart;
				var currentY = (float)ystart;

				for( var pixel=0; pixel<actualLength; pixel++)
				{
					if (currentX > 0 && currentY > 0 && currentX < viewport.Width && currentY < viewport.Height)
					{
						var bufferOffset = (buffers.FrameBuffer.Stride*(int) currentY) + (int) currentX;
						buffers.FrameBuffer.Pixels[bufferOffset] = colorAsInt;
					}

					currentX += slopeX;
					currentY += slopeY;
				}
			}
		}
	}
}

using System.IO;
using System.Windows.Media.Imaging;
using Balder.Core.FlatObjects;
using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Silverlight.Implementation
{
	public class SpriteFrameContext : ISpriteFrameContext
	{
		private BitmapImage _frame;


		public void SetFrame(byte[] frameBytes)
		{
			var stream = new MemoryStream();
			stream.Write(frameBytes,0,frameBytes.Length);
			stream.Seek(0, SeekOrigin.Begin);

			_frame = new BitmapImage();
			_frame.SetSource(stream);
		}

		public void Render(IViewport viewport, Matrix view, Matrix projection, Matrix world)
		{
			var actualViewport = viewport as Viewport;

			var position = new Vector(0, 0, 0);
            var actualPosition = new Vector(world.data[12], world.data[13], world.data[14]);
			var transformedPosition = Vector.Transform(position, world, view);
			var translatedPosition = Vector.Translate(transformedPosition, projection, viewport.Width, viewport.Height);
            /*

                w = width passed to D3DXMatrixPerspectiveLH
                h = height passed to D3DXMatrixPerspectiveLH
                n = z near passed to D3DXMatrixPerspectiveLH
                f = z far passed to D3DXMatrixPerspectiveLH
                d = distance of sprite from camera along Z
                qw = width of sprite quad
                qh = height of sprite quad
                vw = viewport height
                vh = viewport width
                scale = n / d
                (i.e. near/distance, such that at d = n, scale = 1.0)
                renderedWidth = vw * qw * scale / w 
                renderedHeight = vh * qh * scale / h

             */

            var distanceVector = actualViewport.Camera.Position - actualPosition;
            float distance = distanceVector.Length;
            float N = 30.0f;
            distance = MathHelper.Abs(distance);

            float scale = 0.0f + ((2 * N) / distance);
            if (scale <= 0)
                scale = 0;

			actualViewport.RenderImage(_frame,transformedPosition,translatedPosition, scale);
		}
	}
}

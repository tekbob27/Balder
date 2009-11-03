using System;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using Color = System.Drawing.Color;
#endif
using Balder.Core.Imaging;
using Balder.Core.Math;
using Balder.Core.Extensions;

namespace Balder.Core.SoftwareRendering
{
	public class SimpleSpanRenderer : ISpanRenderer
	{
		public const int DefaultSpanLength = 640;

		private static readonly Interpolator DepthInterpolator;
		private static readonly Interpolator GouraudInterpolator;
		private static readonly Interpolator TextureInterpolator;
		private static ColorVector ColorAsVector;

		static SimpleSpanRenderer()
		{
			DepthInterpolator = new Interpolator();
			DepthInterpolator.SetNumberOfInterpolationPoints(1);
			DepthInterpolator.InitializePoints(DefaultSpanLength);

			GouraudInterpolator = new Interpolator();
			GouraudInterpolator.SetNumberOfInterpolationPoints(5);
			GouraudInterpolator.InitializePoints(DefaultSpanLength);

			TextureInterpolator = new Interpolator();
			TextureInterpolator.SetNumberOfInterpolationPoints(3);
			TextureInterpolator.InitializePoints(DefaultSpanLength);
		}

		public bool SupportsDepthBuffer { get { return true; } }

		public void Flat(IBuffers buffer, Span span, Color color)
		{
			var spreadCount = span.Length; //span.XEnd - span.XStart;
			DepthInterpolator.SetPoint(0, span.ZStart, span.ZEnd);
			var yOffset = span.Y*buffer.FrameBuffer.Stride;
			var rOffset = buffer.FrameBuffer.RedPosition;
			var gOffset = buffer.FrameBuffer.GreenPosition;
			var bOffset = buffer.FrameBuffer.BluePosition;
			var aOffset = buffer.FrameBuffer.AlphaPosition;
			var bufferOffset = yOffset + span.XStart;
			var depthBufferOffset = (buffer.Width*span.Y) + span.XStart;
			DepthInterpolator.Interpolate(spreadCount);

			var colorAsInt = (int)color.ToUInt32();

			var xOffset = span.XStart;
			for( var index=0; index<spreadCount; index++ )
			{
				if (xOffset >= 0 && xOffset < buffer.Width)
				{
					var z = DepthInterpolator.Points[0].InterpolatedValues[index];
					var bufferZ = (UInt32) (z*(float) UInt32.MaxValue);
					/*
					if (bufferZ < buffer.DepthBuffer[depthBufferOffset] &&
					    z >= 0f &&
					    z < 1f
						)*/
					{
						buffer.FrameBuffer.Pixels[bufferOffset] = colorAsInt;
						buffer.DepthBuffer[depthBufferOffset] = bufferZ;
					}
				}

				xOffset++;
				bufferOffset++;
				depthBufferOffset++;
			}
		}

		public void Gouraud(IBuffers buffer, Span span)
		{
			var spreadCount = span.XEnd - span.XStart;
			GouraudInterpolator.SetPoint(0, span.ZStart, span.ZEnd);
			GouraudInterpolator.SetPoint(1, span.ColorStart.Red, span.ColorEnd.Red);
			GouraudInterpolator.SetPoint(2, span.ColorStart.Green, span.ColorEnd.Green);
			GouraudInterpolator.SetPoint(3, span.ColorStart.Blue, span.ColorEnd.Blue);
			GouraudInterpolator.SetPoint(4, span.ColorStart.Alpha, span.ColorEnd.Alpha);

			var yOffset = buffer.FrameBuffer.Stride * span.Y;
			var rOffset = buffer.FrameBuffer.RedPosition;
			var gOffset = buffer.FrameBuffer.GreenPosition;
			var bOffset = buffer.FrameBuffer.BluePosition;
			var aOffset = buffer.FrameBuffer.AlphaPosition;
			var bufferOffset = yOffset + span.XStart;
			var depthBufferOffset = (buffer.Width*span.Y)+span.XStart;
			GouraudInterpolator.Interpolate(spreadCount);

			var xOffset = span.XStart;

			for (var index = 0; index < spreadCount; index++)
			{
				if (xOffset >= 0 && xOffset < buffer.Width)
				{

					var z = GouraudInterpolator.Points[0].InterpolatedValues[index];
					var bufferZ = (UInt32) (z*(float) UInt32.MaxValue);

					/*
					if (bufferZ < buffer.DepthBuffer[depthBufferOffset] &&
					    z >= 0f &&
					    z < 1f
						)*/
					{
						buffer.DepthBuffer[depthBufferOffset] = bufferZ;

						ColorAsVector.Red = GouraudInterpolator.Points[1].InterpolatedValues[index];
						ColorAsVector.Green = GouraudInterpolator.Points[2].InterpolatedValues[index];
						ColorAsVector.Blue = GouraudInterpolator.Points[3].InterpolatedValues[index];
						ColorAsVector.Alpha = GouraudInterpolator.Points[4].InterpolatedValues[index];
						var color = (int)ColorAsVector.ToColor().ToUInt32();
						buffer.FrameBuffer.Pixels[bufferOffset] = color;
					}
				}

				xOffset++;
				bufferOffset ++;

				depthBufferOffset++;
			}
		}

		public void Texture(IBuffers buffer, Span span, Image image, ImageContext texture)
		{
			var spreadCount = span.XEnd - span.XStart;
			TextureInterpolator.SetPoint(0, span.ZStart, span.ZEnd);
			TextureInterpolator.SetPoint(1, span.UStart, span.UEnd);
			TextureInterpolator.SetPoint(2, span.VStart, span.VEnd);
			var yOffset = span.Y * buffer.FrameBuffer.Stride;
			var rOffset = buffer.FrameBuffer.RedPosition;
			var gOffset = buffer.FrameBuffer.GreenPosition;
			var bOffset = buffer.FrameBuffer.BluePosition;
			var aOffset = buffer.FrameBuffer.AlphaPosition;
			var bufferOffset = yOffset + span.XStart;
			var depthBufferOffset = (buffer.Width * span.Y) + span.XStart;
			TextureInterpolator.Interpolate(spreadCount);

			var xOffset = span.XStart;

			for (var index = 0; index < spreadCount; index++)
			{
				if (xOffset >= 0 && xOffset < buffer.Width)
				{
					var z = TextureInterpolator.Points[0].InterpolatedValues[index];
					var bufferZ = (UInt32) (z*(float) UInt32.MaxValue);


					var u = TextureInterpolator.Points[1].InterpolatedValues[index];
					var v = TextureInterpolator.Points[2].InterpolatedValues[index];

					var intu = (int) (u*image.Width) & (image.Width - 1);
					var intv = (int) (v*image.Height) & (image.Height - 1);

					var texel = ((intv*image.Width) + intu);


					
					if (bufferZ < buffer.DepthBuffer[depthBufferOffset] &&
					    z >= 0f &&
					    z < 1f
						)
					{
						buffer.FrameBuffer.Pixels[bufferOffset] = texture.Pixels[texel];

						buffer.DepthBuffer[depthBufferOffset] = bufferZ;
					}
				}

				bufferOffset ++;
				depthBufferOffset++;
				xOffset++;
			}
		}
	}

}

using System;
#if(SILVERLIGHT)
using System.Windows.Media;
using System.Windows.Media.Imaging;
#else
using System.Drawing;
#endif

namespace Balder.Core.SoftwareRendering
{
	public delegate void FrameBufferUpdated();
	public delegate void FrameBufferRender();
	public delegate void FrameBufferClear();
	public delegate void FrameBufferSwapped();
	public delegate void FrameBufferShow();

	public interface IFrameBuffer
	{
		event FrameBufferUpdated Updated;
		event FrameBufferRender Render;
		event FrameBufferClear Clear;
		event FrameBufferSwapped Swapped;

		void Initialize(int width, int height);
		int Stride { get; }

		int RedPosition { get; }
		int BluePosition { get; }
		int GreenPosition { get; }
		int AlphaPosition { get; }

		void SetPixel(int x, int y, Color color);
		Color GetPixel(int x, int y);

#if(SILVERLIGHT)
		BitmapSource BitmapSource { get; }
#endif

		int[] Pixels { get;  }
	}
}

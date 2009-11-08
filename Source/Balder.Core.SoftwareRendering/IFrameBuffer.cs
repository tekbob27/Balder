#if(SILVERLIGHT)
using System.Windows.Media.Imaging;
#else
using System.Drawing;
#endif

namespace Balder.Core.SoftwareRendering
{
	public interface IFrameBuffer
	{
		void Initialize(int width, int height);
		int Stride { get; }

		int RedPosition { get; }
		int BluePosition { get; }
		int GreenPosition { get; }
		int AlphaPosition { get; }

#if(SILVERLIGHT)
		BitmapSource BitmapSource { get; }
#endif

		int[] Pixels { get;  }

		void Clear();
		void Swap();
		void Show();
	}
}

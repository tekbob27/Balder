using System.Windows.Media.Imaging;
using Balder.Core.SoftwareRendering;

namespace Balder.Silverlight.SoftwareRendering
{
    public class FrameBuffer : IFrameBuffer
    {
    	private int[] _renderbuffer;
		private int[] _clearBuffer;
		private int[] _showBuffer;
    	private int[] _emptyBuffer;
    	private int _length;

    	public void Initialize(int width, int height)
    	{
    		Stride = width;
			_length = width * height;

			_renderbuffer = new int[_length];
			_clearBuffer = new int[_length];
			_showBuffer = new int[_length];
			_emptyBuffer = new int[_length];

			Swap();
    	}


    	public int Stride { get; private set; }
		public int RedPosition { get { return 2; } }
		public int BluePosition { get { return 0; } }
		public int GreenPosition { get { return 1; } }
		public int AlphaPosition { get { return 3; } }
		public int[] Pixels { get { return _renderbuffer; } }
		public int[] BackBuffer { get { return _showBuffer;  } }

		public void Swap()
		{
			var renderBuffer = _renderbuffer;
			var clearBuffer = _clearBuffer;
			var showBuffer = _showBuffer;

			_renderbuffer = clearBuffer;
			_showBuffer = renderBuffer;
			_clearBuffer = showBuffer;
		}


		public void Clear()
		{
			_emptyBuffer.CopyTo(_clearBuffer, 0);
			//_clearBuffer = new int[_length];
		}


		public void Show()
		{
			//_showBuffer.CopyTo(_writeableBitmap.Pixels, 0);
		}

		public void Update()
		{
			//_writeableBitmap.Invalidate();
		}
	}
}
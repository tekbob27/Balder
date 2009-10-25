using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Balder.Core.SoftwareRendering;

namespace Balder.Silverlight.SoftwareRendering
{
    public class FrameBuffer : IFrameBuffer
    {
    	public event FrameBufferUpdated Updated = () => { };
    	public event FrameBufferRender Render = () => { };
    	public event FrameBufferClear Clear = () => { };
    	public event FrameBufferSwapped Swapped = () => { };

    	private int[] _renderbuffer;
		private int[] _clearBuffer;
		private int[] _showBuffer;
    	private int[] _emptyBuffer;


    	private WriteableBitmap _writeableBitmap;
    	private int _length;


    	public void Initialize(int width, int height)
    	{
    		Stride = width;
			_writeableBitmap = new WriteableBitmap(width, height);
			_length = width * height;

			_renderbuffer = new int[_length];
			_clearBuffer = new int[_length];
			_showBuffer = new int[_length];
			_emptyBuffer = new int[_length];

			Swap();

			FrameBufferManager.Instance.Updated += FrameBufferManagerUpdated;
    		FrameBufferManager.Instance.Render += FrameBufferManagerRender;
    		FrameBufferManager.Instance.Clear += FrameBufferManagerClear;
    		FrameBufferManager.Instance.Swapped += FrameBufferManagerSwapped;
    		FrameBufferManager.Instance.Show += FrameBufferManagerShow;
    	}


    	public int Stride { get; private set; }
		public int RedPosition { get { return 2; } }
		public int BluePosition { get { return 0; } }
		public int GreenPosition { get { return 1; } }
		public int AlphaPosition { get { return 3; } }
		public BitmapSource BitmapSource { get { return _writeableBitmap;  } }
		public int[] Pixels { get { return _renderbuffer; } }

    	public void SetPixel(int x, int y, Color color)
    	{
    	}

    	public Color GetPixel(int x, int y)
    	{
    		return Colors.Black;
    	}



		private void Swap()
		{
			var renderBuffer = _renderbuffer;
			var clearBuffer = _clearBuffer;
			var showBuffer = _showBuffer;

			_renderbuffer = clearBuffer;
			_showBuffer = renderBuffer;
			_clearBuffer = showBuffer;

			Swapped();
		}

		#region FrameBufferManager Event Handlers
		private void FrameBufferManagerUpdated()
		{
			_writeableBitmap.Invalidate();
			Updated();
		}

		private void FrameBufferManagerRender()
		{
			Render();
		}

		private void FrameBufferManagerClear()
		{
			_emptyBuffer.CopyTo(_clearBuffer, 0);
			//_clearBuffer = new int[_length];
			Clear();
		}

		private void FrameBufferManagerSwapped()
		{
			Swap();
		}

		private void FrameBufferManagerShow()
		{
			_showBuffer.CopyTo(_writeableBitmap.Pixels, 0);
		}
		#endregion

	}
}
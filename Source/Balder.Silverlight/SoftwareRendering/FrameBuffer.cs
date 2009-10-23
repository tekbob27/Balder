using System;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Balder.Core.SoftwareRendering;
using Balder.Core.Extensions;

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

		private ManualResetEvent _renderEvent;
		private ManualResetEvent _clearEvent;
		private ManualResetEvent _showEvent;
		private ManualResetEvent _renderFinishedEvent;
		private ManualResetEvent _clearFinishedEvent;
		private ManualResetEvent _showFinishedEvent;

		private Thread _renderThread;
		private Thread _clearThread;
		private Thread _showThread;
		private Thread _swapThread;

    	private WriteableBitmap _writeableBitmap;
    	private int _length;

    	private bool _frameBufferAlive;


    	public void Initialize(int width, int height)
    	{
    		Stride = width;
			_writeableBitmap = new WriteableBitmap(width, height);
			_length = width * height;

			_renderbuffer = new int[_length];
			_clearBuffer = new int[_length];
			_showBuffer = new int[_length];
			_emptyBuffer = new int[_length];

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

		public void Start()
		{
			_frameBufferAlive = true;

			_renderEvent = new ManualResetEvent(false);
			_clearEvent = new ManualResetEvent(false);
			_showEvent = new ManualResetEvent(false);
			_renderFinishedEvent = new ManualResetEvent(true);
			_clearFinishedEvent = new ManualResetEvent(true);
			_showFinishedEvent = new ManualResetEvent(true);

			Swap();

			CompositionTarget.Rendering += ShowTimer;

			_swapThread = new Thread(SwapThread);
			_showThread = new Thread(ShowThread);
			_renderThread = new Thread(RenderThread);
			_clearThread = new Thread(ClearThread);

			_swapThread.Start();
			_showThread.Start();
			_renderThread.Start();
			_clearThread.Start();
		}

		public void Stop()
		{
			_frameBufferAlive = false;
			
			_renderEvent.Set();
			_clearEvent.Set();
			_showEvent.Set();

			_renderFinishedEvent.Set();
			_clearFinishedEvent.Set();
			_showFinishedEvent.Set();
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


		private void Handleshow()
		{
			_showBuffer.CopyTo(_writeableBitmap.Pixels,0);
		}

		private void ShowTimer(object sender, EventArgs e)
		{
			_writeableBitmap.Invalidate();
			Updated();
		}

		private void SwapThread()
		{
			var waitEvents = new[]
			                 	{
									_showFinishedEvent,
									_renderFinishedEvent,
									_clearFinishedEvent
			                 	};
			var startEvents = new[]
			                  	{
			                  		_showEvent,
			                  		_renderEvent,
			                  		_clearEvent
			                  	};

			for (; ; )
			{
				WaitHandle.WaitAll(waitEvents);
				Swap();
				waitEvents.ResetAll();
				startEvents.SetAll();
			}
		}

		private void ShowThread()
		{
			for (; ; )
			{
				_showEvent.WaitOne();
				Handleshow();
				_showEvent.Reset();
				_showFinishedEvent.Set();
			}
		}


		private void RenderThread()
		{
			while( _frameBufferAlive )
			{
				_renderEvent.WaitOne();

				Render();
				
				_renderEvent.Reset();
				_renderFinishedEvent.Set();
			}
		}

		private void ClearThread()
		{
			while (_frameBufferAlive)
			{
				_clearEvent.WaitOne();
				_emptyBuffer.CopyTo(_clearBuffer,0);
				//_clearBuffer = new int[_length];
				Clear();
				_clearEvent.Reset();
				_clearFinishedEvent.Set();
			}
		}
    }
}
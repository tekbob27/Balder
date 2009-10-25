using System;
using System.Threading;
using System.Windows.Media;
using Balder.Core.Extensions;
using Balder.Core.SoftwareRendering;

namespace Balder.Silverlight.SoftwareRendering
{
	public class FrameBufferManager : IDisposable
	{
		public static readonly FrameBufferManager	Instance = new FrameBufferManager();

		public event FrameBufferUpdated Updated = () => { };
		public event FrameBufferRender Render = () => { };
		public event FrameBufferClear Clear = () => { };
		public event FrameBufferSwapped Swapped = () => { };
		public event FrameBufferShow Show = () => { };

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

		private bool _frameBufferAlive;


		private FrameBufferManager()
		{
			Start();
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
				Swapped();
				waitEvents.ResetAll();
				startEvents.SetAll();
			}
		}

		private void ShowThread()
		{
			for (; ; )
			{
				_showEvent.WaitOne();
				Show();
				_showEvent.Reset();
				_showFinishedEvent.Set();
			}
		}


		private void RenderThread()
		{
			while (_frameBufferAlive)
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
				Clear();
				_clearEvent.Reset();
				_clearFinishedEvent.Set();
			}
		}

		private void ShowTimer(object sender, EventArgs e)
		{
			Updated();
		}


		public void Dispose()
		{
			Stop();
		}
	}
}

using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Balder.Core.Display;
using Balder.Core.Interfaces;
using Balder.Core.SoftwareRendering;
using Balder.Silverlight.Implementation;
using Balder.Silverlight.SoftwareRendering;

namespace Balder.Silverlight.Display
{
	public class Display : IDisplay, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
		private IBuffers _buffers;

		public void Initialize(int width, int height)
		{
			_buffers = BufferManager.Instance.Create<FrameBuffer>(width, height);
			FramebufferBitmap = _buffers.FrameBuffer.BitmapSource;
		}

		public Color BackgroundColor { get; set; }
		public BitmapSource FramebufferBitmap { get; private set; }

		public IViewport CreateViewport()
		{
			var viewport = new Viewport();
			return viewport;
		}

		public IViewport CreateViewport(int xpos, int ypos, int width, int height)
		{
			var viewport = CreateViewport();
			viewport.XPosition = xpos;
			viewport.YPosition = ypos;
			viewport.Width = width;
			viewport.Height = height;
			return viewport;
		}


		public void Swap()
		{
			_buffers.Swap();
		}

		public void Clear()
		{
			_buffers.Clear();
		}

		public void Show()
		{
			_buffers.Show();
			FramebufferBitmap = _buffers.FrameBuffer.BitmapSource;
		}
	}
}
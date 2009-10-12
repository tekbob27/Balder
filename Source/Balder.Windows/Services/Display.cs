using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Balder.Core.Services;
using Balder.Core.SoftwareRendering;
using Balder.Windows.Implementation;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Balder.Windows.Services
{
	public class Display : IDisplay
	{
		private readonly Rectangle _lockRectangle;
		private readonly IBuffers _buffers;


		public Display()
		{
			BitmapMutex = new Mutex();
			Bitmap = new Bitmap(640, 480, PixelFormat.Format32bppArgb);
			_lockRectangle = new Rectangle(0, 0, 640, 480);

			_buffers = BufferManager.Instance.Create<FrameBuffer>(640, 480);
		}

		public Bitmap Bitmap { get; private set; }
		public Mutex BitmapMutex { get; private set; }

		public event EventHandler Draw = (s, e) => { };
		public event EventHandler Render = (s, e) => { };
		public event EventHandler Update = (s, e) => { };

		public Color BackgroundColor { get; set; }

		public IViewport CreateViewport(int xpos, int ypos, int width, int height)
		{
			var viewport = new Viewport {XPosition = xpos, YPosition = ypos, Width = width, Height = height};
			return viewport;
		}

		public void Stop()
		{
			
		}
	}
}

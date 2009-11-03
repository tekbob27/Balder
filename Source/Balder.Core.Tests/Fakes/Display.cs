using System;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Display;
using Balder.Core.Interfaces;

namespace Balder.Core.Tests.Fakes
{
	public class Display : IDisplay
	{

		#region IDisplay Members

		public event EventHandler Draw;
		public event EventHandler Render;
		public event EventHandler Update;
		public event EventHandler Initialized;


		public bool IsInitialized
		{
			get { throw new NotImplementedException(); }
		}

		public IViewport CreateViewport()
		{
			var viewport = new Viewport();
			return viewport;
		}

		public IViewport CreateViewport(int xpos, int ypos, int width, int height)
		{
			var viewport = new Viewport();
			return viewport;
		}

		public void Stop()
		{
			
		}

		#endregion

		public Color BackgroundColor { get; set; }
	}
}

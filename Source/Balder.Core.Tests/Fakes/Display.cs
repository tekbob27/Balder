using System;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Core.Tests.Fakes
{
	public class Display : IDisplay
	{

		#region IDisplay Members

		public event EventHandler Draw;
		public event EventHandler Render;
		public event EventHandler Update;
		

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

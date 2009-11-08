using System;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Interfaces;

namespace Balder.Core.Display
{
	public interface IDisplay
	{
		Color BackgroundColor { get; set; }

		IViewport CreateViewport();
		IViewport CreateViewport(int xpos, int ypos, int width, int height);
	}
}
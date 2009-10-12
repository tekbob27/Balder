using System;
using System.Drawing;
using Balder.Core;
using Balder.Core.Interfaces;

namespace Balder.Windows.Implementation
{
	public class Viewport : IViewport
	{
		public Color BackgroundColor { get; set; }

		public int XPosition { get; set; }
		public int YPosition { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Scene Scene { get; set; }
		public Camera Camera { get; set; }

		public void Prepare()
		{
		}

		public void BeforeRender()
		{
		}

		public void AfterRender()
		{
		}
	}
}

using Balder.Core.Interfaces;

namespace Balder.Core.Tests.Fakes
{
	public class Viewport : IViewport
	{
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

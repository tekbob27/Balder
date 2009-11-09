namespace Balder.Core.Display
{
	public class Viewport
	{
		public int XPosition { get; set; }
		public int YPosition { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Scene Scene { get; set; }
		public Camera Camera { get; set; }
	}
}

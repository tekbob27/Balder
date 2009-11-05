namespace Balder.Core.Input
{
	public class Mouse
	{
		public Mouse()
		{
			LeftButton = new MouseButtonState();
			RightButton = new MouseButtonState();
		}

		public int XPosition { get; set; }
		public int YPosition { get; set; }

		public MouseButtonState LeftButton { get; private set; }
		public MouseButtonState RightButton { get; private set; }
	}
}

using CThru.Silverlight;
using NUnit.Framework;

namespace Balder.Silverlight.Tests.Display
{
	[TestFixture]
	public class DisplayTests
	{
		[Test, SilverlightUnitTest]
		public void CreatingViewportShouldReturnValidViewport()
		{
			var display = new Silverlight.Display.Display();

			const int xpos = 0;
			const int ypos = 0;
			const int width = 640;
			const int height = 480;

			var viewport = display.CreateViewport(xpos,ypos,width,height);

			Assert.IsNotNull(viewport);
			Assert.AreEqual(xpos,viewport.XPosition);
			Assert.AreEqual(ypos,viewport.YPosition);
			Assert.AreEqual(width,viewport.Width);
			Assert.AreEqual(height,viewport.Height);
		}

	}
}

using Balder.Core.Services;
using Balder.Silverlight.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balder.Silverlight.Tests.Specs_for_display
{
	[TestClass]
	public class When_working_with_viewports
	{
		[TestMethod]
		public void Creating_viewport_returns_valid_viewport()
		{
			var display = new Display();

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

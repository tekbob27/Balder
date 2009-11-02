using Balder.Core.Extensions;
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Moq;
using NUnit.Framework;

namespace Balder.Core.Tests.Extensions
{
	[TestFixture]
	public class ViewportExtensionsTests
	{
		[Test]
		public void UnprojectingCenterOfViewportWithIdentityViewShouldGenerateAVectorAtCenter()
		{
			var viewportMock = new Mock<IViewport>();
			viewportMock.ExpectGet(v => v.Width).Returns(640);
			viewportMock.ExpectGet(v => v.Height).Returns(480);
			var aspect = (float) viewportMock.Object.Height/(float) viewportMock.Object.Width;
			var projection = Matrix.CreatePerspectiveFieldOfView(40f, aspect, 1, 4000f);
			var view = Matrix.Identity;
			var world = Matrix.Identity;
			var position = new Vector((float)viewportMock.Object.Width/2, (float)viewportMock.Object.Height/2,0);
			var result = viewportMock.Object.Unproject(position, projection, view, world);
			Assert.That(result,Is.EqualTo(new Vector(0,0,-1)));
		}

		[Test]
		public void UnprojectingCenterOfViewportWithViewRotated90DegreesAroundYAxisShouldGenerateAVectorRotated90DegreesInOpositeDirection()
		{
			var viewportMock = new Mock<IViewport>();
			viewportMock.ExpectGet(v => v.Width).Returns(640);
			viewportMock.ExpectGet(v => v.Height).Returns(480);
			var aspect = (float)viewportMock.Object.Height / (float)viewportMock.Object.Width;
			var projection = Matrix.CreatePerspectiveFieldOfView(40f, aspect, 1, 4000f);
			var view = Matrix.CreateRotationY(90);
			var world = Matrix.Identity;
			var position = new Vector((float)viewportMock.Object.Width / 2, (float)viewportMock.Object.Height / 2, 0);
			var result = viewportMock.Object.Unproject(position, projection, view, world);

			var negativeView = Matrix.CreateRotationY(-90);
			var expected = new Vector(0, 0, -1);
			var rotatedExpected = expected*negativeView;

			Assert.That(result, Is.EqualTo(rotatedExpected));
		}

	}
}

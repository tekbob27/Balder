using Balder.Core.Display;
using Balder.Core.Extensions;
using Balder.Core.Math;
using NUnit.Framework;

namespace Balder.Core.Tests.Extensions
{
	[TestFixture]
	public class ViewportExtensionsTests
	{
		[Test]
		public void UnprojectingCenterOfViewportWithIdentityViewShouldGenerateAVectorAtCenter()
		{
			var viewport = new Viewport { Width = 640, Height = 480 };

			var aspect = (float) viewport.Height/(float) viewport.Width;
			var projection = Matrix.CreatePerspectiveFieldOfView(40f, aspect, 1, 4000f);
			var view = Matrix.Identity;
			var world = Matrix.Identity;
			var position = new Vector((float)viewport.Width/2, (float)viewport.Height/2,0);
			var result = viewport.Unproject(position, projection, view, world);
			Assert.That(result,Is.EqualTo(new Vector(0,0,-1)));
		}

		[Test]
		public void UnprojectingCenterOfViewportWithViewRotated90DegreesAroundYAxisShouldGenerateAVectorRotated90DegreesInOpositeDirection()
		{
			var viewport = new Viewport { Width = 640, Height = 480 };
			var aspect = (float)viewport.Height / (float)viewport.Width;
			var projection = Matrix.CreatePerspectiveFieldOfView(40f, aspect, 1, 4000f);
			var view = Matrix.CreateRotationY(90);
			var world = Matrix.Identity;
			var position = new Vector((float)viewport.Width / 2, (float)viewport.Height / 2, 0);
			var result = viewport.Unproject(position, projection, view, world);

			var negativeView = Matrix.CreateRotationY(-90);
			var expected = new Vector(0, 0, -1);
			var rotatedExpected = expected*negativeView;

			Assert.That(result, Is.EqualTo(rotatedExpected));
		}

	}
}

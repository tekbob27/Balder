using Balder.Core.Math;
using NUnit.Framework;

namespace Balder.Core.Tests.Math
{
	[TestFixture]
	public class RayTests
	{
		[Test]
		public void RayPointingThroughBoundingSphereFromFrontShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(0, 0, -10), new Vector(0, 0, 1));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result,Is.Not.Null);
		}

		[Test]
		public void RayPointingThroughBoundingSphereFromLeftShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(-10, 0, 0), new Vector(1, 0, 0));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public void RayPointingThroughBoundingSphereFromRightShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(10, 0, 0), new Vector(-1, 0, 0));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public void RayPointingThroughBoundingSphereFromBackShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(0, 0, 10), new Vector(0, 0, -1));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public void RayPointingThroughBoundingSphereFromTopShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(0, 10, 0), new Vector(0, -1, 0));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public void RayPointingThroughBoundingSphereFromBottomShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(0, -10, 0), new Vector(0, 1, 0));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public void RayPointingAwayFromBoundingSphereShouldBeIntersected()
		{
			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 1);
			var ray = new Ray(new Vector(0, -10, 0), new Vector(0, -1, 0));
			var result = ray.Intersects(boundingSphere);
			Assert.That(result, Is.Null);
		}
	}
}

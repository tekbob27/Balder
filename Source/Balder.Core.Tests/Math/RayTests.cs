﻿using Balder.Core.Math;
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

		[Test]
		public void RayPointintIntoBoundSphereFromMultipleLocationsShouldBeIntersected()
		{

			var rays = new Ray[]
			           	{
							new Ray(new Vector(-12,12,-14), Vector.Zero), 
							new Ray(new Vector(12,12,-14), Vector.Zero), 
							new Ray(new Vector(-12,-12,-14), Vector.Zero), 
							new Ray(new Vector(12,-12,-14), Vector.Zero), 
			           	};

			var boundingSphere = new BoundingSphere(new Vector(0, 0, 0), 10);
			var destination = new Vector(0, 0, -1);

			foreach( var ray in rays )
			{
				var direction = destination - ray.Position;
				direction.Normalize();
				ray.Direction = direction;

				var result = ray.Intersects(boundingSphere);
				Assert.That(result, Is.Not.Null);
			}
		}
	}
}
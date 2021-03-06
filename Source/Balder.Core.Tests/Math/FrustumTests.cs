﻿using Balder.Core.Math;
using Balder.Core.Tests.Fakes;
using NUnit.Framework;

namespace Balder.Core.Tests.Math
{
	[TestFixture]
	public class FrustumTests
	{
		private Frustum _frustum;

		[TestFixtureSetUp]
		public void Setup()
		{
			var viewport = new Viewport {Width = 640, Height = 480};
			var camera = new Camera {Target = Vector.Forward, Position = Vector.Zero};
			camera.Prepare(viewport);
			camera.Update();
			_frustum = new Frustum();
			_frustum.SetCameraDefinition(camera);
		}

		[Test]
		public void VectorInsideShouldNotBeClipped()
		{
			var vectorToTest = new Vector(0f, 0f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Inside));
		}

		[Test]
		public void VectorAboveTopShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, 1000f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Outside));
		}

		[Test]
		public void VectorBelowBottomShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, -1000f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Outside));
		}

		[Test]
		public void VectorLeftOfLeftShouldBeClipped()
		{
			var vectorToTest = new Vector(-1000f, 0f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Outside));
		}

		[Test]
		public void VectorRightOfRightShouldBeClipped()
		{
			var vectorToTest = new Vector(1000f, 0f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Outside));
		}

		[Test]
		public void VectorBehindNearShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, 0f, -1000f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Outside));
		}

		[Test]
		public void VectorBeyondFarShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, 0f, 1000f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			Assert.That(result, Is.EqualTo(FrustumIntersection.Outside));
		}
	}
}

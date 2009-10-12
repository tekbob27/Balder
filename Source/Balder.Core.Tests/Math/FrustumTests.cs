using Balder.Core.Math;
using Balder.Core.Tests.Fakes;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balder.Core.Tests.Math
{
	[TestClass]
	public class FrustumTests
	{
		private Frustum _frustum;
		[ClassInitialize]
		public void Setup()
		{
			var viewport = new Viewport {Width = 640, Height = 480};
			var camera = new Camera {Target = Vector.Forward, Position = Vector.Zero};
			camera.Prepare(viewport);
			camera.Update();
			_frustum = new Frustum();
			_frustum.SetCameraDefinition(camera);
		}


		[TestMethod]
		public void VectorInsideShouldNotBeClipped()
		{
			var vectorToTest = new Vector(0f, 0f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Inside);
		}

		[TestMethod]
		public void VectorAboveTopShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, 1000f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Outside);
		}

		[TestMethod]
		public void VectorBelowBottomShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, -1000f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Outside);
		}

		[TestMethod]
		public void VectorLeftOfLeftShouldBeClipped()
		{
			var vectorToTest = new Vector(-1000f, 0f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Outside);
		}

		[TestMethod]
		public void VectorRightOfRightShouldBeClipped()
		{
			var vectorToTest = new Vector(1000f, 0f, 0.5f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Outside);
		}

		[TestMethod]
		public void VectorBehindNearShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, 0f, -1000f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Outside);
		}

		[TestMethod]
		public void VectorBeyondFarShouldBeClipped()
		{
			var vectorToTest = new Vector(0f, 0f, 1000f);
			var result = _frustum.IsPointInFrustum(vectorToTest);
			result.ShouldBe(FrustumIntersection.Outside);
		}

	}
}

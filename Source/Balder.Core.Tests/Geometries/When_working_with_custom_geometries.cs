using Balder.Core.Runtime;
using Balder.Core.Services;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balder.Core.Tests.Fakes;

namespace Balder.Core.Tests.Specs_for_geometries
{
	[TestClass]
	public class When_working_with_custom_geometries
	{
		[TestInitialize]
		public static void Setup()
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);
		}


		[TestMethod]
		public void Vertices_should_be_sent_to_the_geometry_context()
		{
			var contentManager = EngineRuntime.Instance.Kernel.Get<IContentManager>();

			var geometry = contentManager.CreateGeometry<MockGeometry>();
			var geometryContext = geometry.GeometryContext as GeometryContext;

			Assert.IsNotNull(geometryContext);

			geometry.SetVertexCount(1);
			geometry.SetVertex(0, 1f, 2f, 3f);

			geometryContext.Vertices.Length.ShouldBe(1);
			geometryContext.Vertices[0].Vector.X.ShouldBe(1f);
			geometryContext.Vertices[0].Vector.Y.ShouldBe(2f);
			geometryContext.Vertices[0].Vector.Z.ShouldBe(3f);
		}
	}
}

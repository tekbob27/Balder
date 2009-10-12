using Balder.Core.Implementation;
using Balder.Core.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balder.Core.SoftwareRendering;

namespace Balder.Silverlight.Tests.Specs_for_geometry
{
	[TestClass]
	public class When_working_with_geometry_creation
	{
		[TestMethod]
		public void Creating_a_geometry_context_gives_valid_context()
		{
			Assert.Inconclusive();
			/*
			var geometry = ContentManager.Instance.CreateGeometry<MockGeometry>();
			Assert.IsNotNull(geometry.GeometryContext);
			Assert.AreEqual(typeof (GeometryContext), geometry.GeometryContext.GetType());
			 * */
		}

		[TestMethod]
		public void Allocating_vertices_actually_allocates()
		{
			const int vertexCount = 5;

			var context = new GeometryContext();
			context.AllocateVertices(vertexCount);

			Assert.IsNotNull(context.Vertices);
			Assert.AreEqual(vertexCount,context.Vertices.Length);
		}
	}
}

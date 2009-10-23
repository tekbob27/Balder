using Balder.Core.Objects.Geometries;

namespace Balder.Core.Tests.Fakes
{
	public class MockGeometry : Geometry
	{
		public void SetVertexCount(int count)
		{
			GeometryContext.AllocateVertices(count);	
		}

		public void SetVertex(int index, float x,float y, float z)
		{
			GeometryContext.SetVertex(index, new Vertex(x,y,z));
		}
	}
}

using Balder.Core.Geometries;

namespace Balder.Core.Tests.Fakes
{
	public class MockGeometry : Geometry
	{
		public void SetVertexCount(int count)
		{
			this.GeometryContext.AllocateVertices(count);	
		}

		public void SetVertex(int index, float x,float y, float z)
		{
			this.GeometryContext.SetVertex(index, new Vertex(x,y,z));
		}
	}
}

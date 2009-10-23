using Balder.Core.Interfaces;
using Balder.Core.Materials;
using Balder.Core.Math;

namespace Balder.Core.Objects.Geometries
{
	public interface IGeometryContext
	{
		int FaceCount { get; }
		int VertexCount { get; }
		int TextureCoordinateCount { get; }


		void AllocateFaces(int count);
		void SetFace(int index,Face face);
		Face[] GetFaces();
		void SetFaceTextureCoordinateIndex(int index, int a, int b, int c);
		void SetMaterial(int index, Material material);

		void AllocateVertices(int count);
		void SetVertex(int index,Vertex vertex);
		Vertex[] GetVertices();
		
		void AllocateTextureCoordinates(int count);
		void SetTextureCoordinate(int index,TextureCoordinate textureCoordinate);

		void Prepare();

		void Render(IViewport viewport, Matrix view, Matrix projection, Matrix world);
	}
}
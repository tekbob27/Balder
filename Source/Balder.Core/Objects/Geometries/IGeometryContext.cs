using Balder.Core.Display;
using Balder.Core.Materials;
using Balder.Core.Math;

namespace Balder.Core.Objects.Geometries
{
	public interface IGeometryContext
	{
		int FaceCount { get; }
		int VertexCount { get; }
		int TextureCoordinateCount { get; }
		int LineCount { get; }

		void AllocateFaces(int count);
		void SetFace(int index,Face face);
		Face[] GetFaces();

		void AllocateVertices(int count);
		void SetVertex(int index,Vertex vertex);
		Vertex[] GetVertices();

		void AllocateLines(int count);
		void SetLine(int index, Line line);
		Line[] GetLines();
		
		void AllocateTextureCoordinates(int count);
		void SetTextureCoordinate(int index,TextureCoordinate textureCoordinate);
		void SetFaceTextureCoordinateIndex(int index, int a, int b, int c);

		void SetMaterial(int index, Material material);

		void Prepare();

		void Render(Viewport viewport, RenderableNode geometry, Matrix view, Matrix projection, Matrix world);
		
	}
}
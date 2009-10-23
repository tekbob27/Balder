using System;
using Balder.Core.Interfaces;
using Balder.Core.Materials;
using Balder.Core.Math;
using Balder.Core.Objects.Geometries;

namespace Balder.Core.Tests.Fakes
{
	public class GeometryContext : IGeometryContext
	{
		public Vertex[] Vertices { get; private set; }
		public Face[] Faces { get; private set; }
		public TextureCoordinate[] TextureCoordinates { get; private set; }

		public int FaceCount { get { return Faces.Length; } }
		public int VertexCount { get { return Vertices.Length; }}
		public int TextureCoordinateCount { get { return TextureCoordinates.Length; }}

		public void AllocateFaces(int count)
		{
			Faces = new Face[count];
		}

		public void SetFace(int index, Face face)
		{
			Faces[index] = face;
		}

		public Face[] GetFaces()
		{
			return Faces;
		}

		public void SetFaceTextureCoordinateIndex(int index, int a, int b, int c)
		{
			Faces[index].DiffuseA = a;
			Faces[index].DiffuseB = b;
			Faces[index].DiffuseC = c;
		}

		public void SetMaterial(int index, Material material)
		{
			Faces[index].Material = material;
		}

		public void AllocateVertices(int count)
		{
			Vertices = new Vertex[count];
		}

		public void SetVertex(int index, Vertex vertex)
		{
			Vertices[index] = vertex;
		}

		public Vertex[] GetVertices()
		{
			return Vertices;
		}

		public void AllocateTextureCoordinates(int count)
		{
			TextureCoordinates = new TextureCoordinate[count];
		}

		public void SetTextureCoordinate(int index, TextureCoordinate textureCoordinate)
		{
			TextureCoordinates[index] = textureCoordinate;
		}

		public void Prepare()
		{
			
		}

		public void Render(IViewport viewport, Matrix view, Matrix projection, Matrix world)
		{
			
		}
	}
}

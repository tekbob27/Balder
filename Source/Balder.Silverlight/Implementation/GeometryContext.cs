using System;
using System.Collections.Generic;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Balder.Core.Runtime;

namespace Balder.Silverlight.Implementation
{
	public class GeometryContext : IGeometryContext
	{
		public Vertex[] Vertices { get; private set; }
		public Face[] Faces { get; private set; }


		public int FaceCount { get { return this.Faces.Length; } }
		public int VertexCount { get { return this.Vertices.Length; } }
		public int TextureCoordinateCount
		{
			get { throw new NotImplementedException(); }
		}

		public void AllocateFaces(int count)
		{
			this.Faces = new Face[count];

		}

		public void SetFace(int index, Face face)
		{
			var v1 = Vertices[face.B].Vector - Vertices[face.A].Vector;
			var v2 = Vertices[face.C].Vector - Vertices[face.A].Vector;

			var cross = v1.Cross(v2);
			cross.Normalize();
			face.Normal = cross;

			var v = Vertices[face.A].Vector + Vertices[face.B].Vector + Vertices[face.C].Vector;
			face.Position = v/3;

			this.Faces[index] = face;
		}

		public Face[] GetFaces()
		{
			return Faces;
		}

		public void AllocateVertices(int count)
		{
			this.Vertices = new Vertex[count];
		}

		public void SetVertex(int index, Vertex vertex)
		{
			this.Vertices[index] = vertex;
		}

		public Vertex[] GetVertices()
		{
			return Vertices;
		}

		public void AllocateTextureCoordinates(int count)
		{
			throw new NotImplementedException();
		}

		public void SetTextureCoordinate(int index, TextureCoordinate textureCoordinate)
		{
			throw new NotImplementedException();
		}

		public void Prepare()
		{
			var vertexCount = new Dictionary<Vertex, int>();
			var vertexNormal = new Dictionary<Vertex, Vector>();

			Func<Vertex, Vector, int> AddNormal =
				delegate(Vertex vertex, Vector normal)
				{
					if (!vertexNormal.ContainsKey(vertex))
					{
						vertexNormal[vertex] = normal;
						vertexCount[vertex] = 1;
					}
					else
					{
						vertexNormal[vertex] += normal;
						vertexCount[vertex]++;
					}
					return 0;
				};

			foreach (var face in Faces)
			{
				AddNormal(Vertices[face.A], face.Normal);
				AddNormal(Vertices[face.B], face.Normal);
				AddNormal(Vertices[face.C], face.Normal);
			}

			/*
			foreach (var vertex in vertexNormal.Keys)
			{
				var addedNormals = vertexNormal[vertex];
				var count = vertexCount[vertex];

				var normal = new Vector(addedNormals.X / count,
											addedNormals.Y / count,
											addedNormals.Z / count);

				

				vertex.Normal = normal;
			}
			 * */
		}

		public void Render(IViewport viewport, Matrix view, Matrix projection, Matrix world)
		{
			var actualViewport = viewport as Viewport;

			for (var vertexIndex = 0; vertexIndex < this.Vertices.Length; vertexIndex++)
			{
				var vertex = Vertices[vertexIndex];
				vertex.Transform(world, view);
				vertex.Translate(projection, viewport.Width, viewport.Height);
				Vertices[vertexIndex] = vertex;
			}

			for (var faceIndex = 0; faceIndex < this.Faces.Length; faceIndex++)
			{
				var face = Faces[faceIndex];

				var a = Vertices[face.A];
				var b = Vertices[face.B];
				var c = Vertices[face.C];

				face.Transform(world, view);
				face.Translate(projection, viewport.Width, viewport.Height);

				var mixedProduct = (b.TranslatedVector.X - a.TranslatedVector.X) * (c.TranslatedVector.Y - a.TranslatedVector.Y) -
										(c.TranslatedVector.X - a.TranslatedVector.X) * (b.TranslatedVector.Y - a.TranslatedVector.Y);

				var visible = mixedProduct > 0;
				if (!visible)
				{
					continue;
				}

				actualViewport.RenderPolygon(face, a, b, c, mixedProduct);

				if( EngineRuntime.Instance.DebugLevel.IsFaceNormalsSet() )
				{
					actualViewport.DebugRenderFace(face, a, b, c);	
				}
			}
		}
	}
}

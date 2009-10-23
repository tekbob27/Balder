using System;
using System.Collections.Generic;
using Balder.Core.Math;

namespace Balder.Core.Objects.Geometries
{
	public static class GeometryHelper
	{
		public static void CalculateVertexNormals(IGeometryContext context)
		{
			var vertexCount = new Dictionary<int, int>();
			var vertexNormal = new Dictionary<int, Vector>();

			var vertices = context.GetVertices();
			var faces = context.GetFaces();

			Func<int, Vector, int> addNormal =
				delegate(int vertex, Vector normal)
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

			foreach (var face in faces)
			{
				addNormal(face.A, face.Normal);
				addNormal(face.B, face.Normal);
				addNormal(face.C, face.Normal);
			}

			foreach (var vertex in vertexNormal.Keys)
			{
				var addedNormals = vertexNormal[vertex];
				var count = vertexCount[vertex];

				var normal = new Vector(addedNormals.X / count,
				                        addedNormals.Y / count,
				                        addedNormals.Z / count);
				vertices[vertex].Normal = normal;
			}
		}
	}
}
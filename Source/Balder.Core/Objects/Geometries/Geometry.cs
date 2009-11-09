using Balder.Core.Assets;
using Balder.Core.Debug;
using Balder.Core.Display;
using Balder.Core.Math;
using Ninject.Core;

namespace Balder.Core.Objects.Geometries
{
	public class Geometry : RenderableNode, IAssetPart
	{
		[Inject]
		public IGeometryContext GeometryContext { get; set; }

		[Inject]
		public IDebugRenderer DebugRenderer { get; set; }



		public void InitializeBoundingSphere()
		{
			var lowestVector = Vector.Zero;
			var highestVector = Vector.Zero;
			var vertices = GeometryContext.GetVertices();
			for (var vertexIndex = 0; vertexIndex < vertices.Length; vertexIndex++)
			{
				var vector = vertices[vertexIndex].Vector;
				if (vector.X < lowestVector.X)
				{
					lowestVector.X = vector.X;
				}
				if (vector.Y < lowestVector.Y)
				{
					lowestVector.Y = vector.Y;
				}
				if (vector.Z < lowestVector.Z)
				{
					lowestVector.Z = vector.Z;
				}
				if (vector.X > highestVector.X)
				{
					highestVector.X = vector.X;
				}
				if (vector.Y > highestVector.Y)
				{
					highestVector.Y = vector.Y;
				}
				if (vector.Z > highestVector.Z)
				{
					highestVector.Z = vector.Z;
				}
			}

			var length = highestVector - lowestVector;
			var center = lowestVector + (length / 2);

			BoundingSphere = new BoundingSphere(center, length.Length/2);
		}

		public override void Render(Viewport viewport, Matrix view, Matrix projection)
		{
			/*
			var xRotation = Matrix.CreateRotationX(XRotation);
			var yRotation = Matrix.CreateRotationY(YRotation);
			var zRotation = Matrix.CreateRotationZ(ZRotation);
			var translation = Matrix.CreateTranslation(Position);
			var scale = Matrix.CreateScale(Scale);

			var localToWorld = World * xRotation * yRotation * zRotation * translation * scale;
			*/

			DebugRenderer.RenderBoundingSphere(BoundingSphere, viewport, view, projection, World);
			GeometryContext.Render(viewport, view, projection, World);
		}

		public string Name { get; set; }
	}
}
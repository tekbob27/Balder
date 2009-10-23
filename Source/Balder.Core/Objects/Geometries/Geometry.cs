using Balder.Core.Assets;
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Balder.Core.Services;
using Ninject.Core;

namespace Balder.Core.Objects.Geometries
{
	public class Geometry : RenderableNode, IAssetPart
	{
		[Inject]
		public ITargetDevice TargetDevice { get; set; }

		[Inject]
		public IGeometryContext GeometryContext { get; set; }

		public override void Render(IViewport viewport, Matrix view, Matrix projection)
		{
			/*
			var xRotation = Matrix.CreateRotationX(XRotation);
			var yRotation = Matrix.CreateRotationY(YRotation);
			var zRotation = Matrix.CreateRotationZ(ZRotation);
			var translation = Matrix.CreateTranslation(Position);
			var scale = Matrix.CreateScale(Scale);

			var localToWorld = World * xRotation * yRotation * zRotation * translation * scale;
			*/

			GeometryContext.Render(viewport, view, projection, World);
		}

		public string Name { get; set; }
	}
}
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Execution;
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Ninject.Core;
using Matrix = Balder.Core.Math.Matrix;

namespace Balder.Core.Debug
{
	[Singleton]
	public class DebugRenderer : IDebugRenderer
	{
		public static readonly Color DebugInfoColor = Colors.Yellow;
		private readonly IObjectFactory _objectFactory;

		private DebugShape _boundingSphereDebugShape;

		public DebugRenderer(IObjectFactory objectFactory)
		{
			_objectFactory = objectFactory;
			CreateShapes();
		}


		private void CreateShapes()
		{
			_boundingSphereDebugShape = _objectFactory.Get<BoundingSphereDebugShape>();
			_boundingSphereDebugShape.Initialize();
		}

		public void RenderBoundingSphere(BoundingSphere sphere, IViewport viewport, Matrix view, Matrix projection, Matrix world)
		{
			var scaleMatrix = Matrix.CreateScale(sphere.Radius);
			var translationMatrix = Matrix.CreateTranslation(sphere.Center) * world;
			var rotateYMatrix = Matrix.CreateRotationY(90);
			var rotateXMatrix = Matrix.CreateRotationX(90);

			_boundingSphereDebugShape.World = scaleMatrix * translationMatrix;
			_boundingSphereDebugShape.Render(viewport, view, projection);

			_boundingSphereDebugShape.World = rotateYMatrix * scaleMatrix * translationMatrix;
			_boundingSphereDebugShape.Render(viewport, view, projection);

			_boundingSphereDebugShape.World = rotateXMatrix * scaleMatrix * translationMatrix;
			_boundingSphereDebugShape.Render(viewport, view, projection);
		}
	}
}

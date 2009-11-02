using System;
using System.Windows.Controls;
using Balder.Core.Extensions;
using Balder.Core.Math;
using Balder.Silverlight.Controls;

namespace Balder.Silverlight.TestApp
{
	public partial class Page : UserControl
	{
		public Page()
		{
			InitializeComponent();

			
			_renderingContainer.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(_renderingContainer_MouseLeftButtonUp);
		}

		void _renderingContainer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var mousePosition = e.GetPosition(_renderingContainer);
			var nearSource = new Vector((float)mousePosition.X,(float)mousePosition.Y,0f);
			var farSource = new Vector((float)mousePosition.X,(float)mousePosition.Y,1f);

			var world = Matrix.CreateTranslation(new Vector(0f, 0f, 0f));

			var camera = _renderingContainer.Camera;

			var nearPoint = _renderingContainer.Viewport.Unproject(nearSource, camera.ProjectionMatrix, camera.ViewMatrix, world);
			var farPoint = _renderingContainer.Viewport.Unproject(farSource, camera.ProjectionMatrix, camera.ViewMatrix, world);

			var direction = farPoint - nearPoint;
			direction.Normalize();

			var pickRay = new Ray(nearPoint, direction);

			foreach( var node in _renderingContainer.Scene.RenderableNodes )
			{
				node.BoundingSphere.Transform(camera.ViewMatrix);
				var intersects = pickRay.Intersects(node.BoundingSphere);
				if( null != intersects )
				{
					int i = 0;
					i++;
				}
			}
		}

		private float _angle = 0f;
		private double cameraSin;

		private void Updated(RenderingContainer renderingContainer)
		{
			renderingContainer.Camera.Position.X = (float)Math.Sin(cameraSin) * 50f;
			renderingContainer.Camera.Position.Y = -10; // ((float)Math.Sin(cameraSin) * 15f) - 20f;
			renderingContainer.Camera.Position.Z = (float)Math.Cos(cameraSin) * 50f;
			cameraSin += 0.05;

			//_audi.Node.World = Matrix.CreateRotationY(_angle);
			_angle += 0.5f;
			//_renderingContainer.Camera.Position = new Vector(0,-5,-20);
			_renderingContainer.Camera.Target = new Vector(-4, 0, 0);
		}
	}
}

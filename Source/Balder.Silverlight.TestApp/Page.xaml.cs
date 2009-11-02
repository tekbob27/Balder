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

			_renderingContainer.MouseMove += _renderingContainer_MouseMove;
			_renderingContainer.MouseLeftButtonUp += _renderingContainer_MouseLeftButtonUp;
		}

		void _renderingContainer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var mousePosition = e.GetPosition(_renderingContainer);
			_mousePosition.Text = string.Format("X: {0}, Y: {1}", mousePosition.X, mousePosition.Y);

			var nearSource = new Vector((float)mousePosition.X, (float)mousePosition.Y, 0f);
			var farSource = new Vector((float)mousePosition.X, (float)mousePosition.Y, 1f);

			var camera = _renderingContainer.Camera;

			var world = Matrix.CreateTranslation(new Vector(0f, 0f, 0f));
			var nearPoint = _renderingContainer.Viewport.Unproject(nearSource, camera.ProjectionMatrix, camera.ViewMatrix, world);
			var farPoint = _renderingContainer.Viewport.Unproject(farSource, camera.ProjectionMatrix, camera.ViewMatrix, world);

			//farPoint.X = nearPoint.X;
			//farPoint.Y = nearPoint.Y;

			var direction = farPoint - nearPoint;
			direction.Normalize();

			_nearPosition.Text = nearPoint.ToString();
			_farPosition.Text = farPoint.ToString();

			var pickRay = new Ray(nearPoint, direction);

			foreach (var node in _renderingContainer.Scene.RenderableNodes)
			{
				var transformedSphere = node.BoundingSphere.Transform(node.World);

				var delta = nearPoint - transformedSphere.Center;
				delta.Normalize();

				var intersects = pickRay.Intersects(transformedSphere);
				if (null != intersects)
				{
					var hitCount = Convert.ToInt32(_hitCounter.Text);
					hitCount++;
					_hitCounter.Text = hitCount.ToString();
				}
			}
			
		}

		void _renderingContainer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
		}

		private float _angle = 0f;
		private double cameraSin;

		private void Updated(RenderingContainer renderingContainer)
		{
			renderingContainer.Camera.Position = new Vector(0,0,-50);
			/*
			renderingContainer.Camera.Position.X = (float)Math.Sin(cameraSin) * 50f;
			renderingContainer.Camera.Position.Y = 0; // ((float)Math.Sin(cameraSin) * 15f) - 20f;
			renderingContainer.Camera.Position.Z = (float)Math.Cos(cameraSin) * 50f;
			cameraSin += 0.005;
			 * */

			//_audi.Node.World = Matrix.CreateRotationY(_angle);
			_angle += 0.05f;
			//_renderingContainer.Camera.Position = new Vector(0,-5,-20);
			_renderingContainer.Camera.Target = new Vector(0, 0, 0);
		}
	}
}

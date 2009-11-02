using System;
using System.Windows.Controls;
using Balder.Core.Debug;
using Balder.Core.Extensions;
using Balder.Core.Math;
using Balder.Core.Runtime;
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

			Loaded += Page_Loaded;
		}

		void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
		}


		void _renderingContainer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var mousePosition = e.GetPosition(_renderingContainer);
			_mousePosition.Text = string.Format("X: {0}, Y: {1}", mousePosition.X, mousePosition.Y);

			var camera = _renderingContainer.Camera;

			_viewMatrix.Text = camera.ViewMatrix.ToString();
			_projectionMatrix.Text = camera.ProjectionMatrix.ToString();

			var nearSource = new Vector((float)mousePosition.X, (float)mousePosition.Y, 0f);
			var farSource = new Vector((float)mousePosition.X, (float)mousePosition.Y, 1f);

			var nearPoint = _renderingContainer.Viewport.Unproject(nearSource, camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);
			var farPoint = _renderingContainer.Viewport.Unproject(farSource, camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);

			var direction = farPoint - nearPoint;
			direction.Normalize();

			var pickRay = new Ray(nearPoint, direction);

			_nearPosition.Text = nearPoint.ToString();
			_farPosition.Text = farPoint.ToString();

			foreach (var node in _renderingContainer.Scene.RenderableNodes)
			{
				_spherePosition.Text = node.BoundingSphere.Center.ToString();

				var transformedSphere = node.BoundingSphere.Transform(node.World);
				_transformedSpherePosition.Text = transformedSphere.Center.ToString();

				var distance = pickRay.Intersects(transformedSphere);
				_intersects.Text = distance.ToString();
				if (distance.HasValue)
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
			_renderingContainer.Camera.Target = new Vector(-4, 0, 0);

			_cameraPosition.Text = renderingContainer.Camera.Position.ToString();
			_cameraTarget.Text = renderingContainer.Camera.Target.ToString();
		}
	}
}

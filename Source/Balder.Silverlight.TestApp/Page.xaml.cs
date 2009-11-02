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

			var hitObject = _renderingContainer.Scene.GetNodeAtScreenCoordinate(_renderingContainer.Viewport, (int)mousePosition.X,
			                                                                      (int)mousePosition.Y);
			if( null != hitObject )
			{
				var hitCount = Convert.ToInt32(_hitCounter.Text);
				hitCount++;
				_hitCounter.Text = hitCount.ToString();
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
			//_renderingContainer.Camera.Target = new Vector(-4, 0, 0);

			_cameraPosition.Text = renderingContainer.Camera.Position.ToString();
			_cameraTarget.Text = renderingContainer.Camera.Target.ToString();
		}
	}
}

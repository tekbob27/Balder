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

			LayoutRoot.MouseMove += _renderingContainer_MouseMove;
			LayoutRoot.MouseLeftButtonUp += _renderingContainer_MouseLeftButtonUp;

			Loaded += Page_Loaded;
		}

		void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			App.MyGame.Scene.ObjectHitChanged += Scene_ObjectHitChanged;
		}

		void Scene_ObjectHitChanged(object sender, EventArgs e)
		{
			var node = App.MyGame.Scene.ObjectHit;

			Dispatcher.BeginInvoke(() =>
			                       	{
										if (null == node)
										{
											_hitObject.Text = "No object was hit";
										}
										else
										{
											_hitObject.Text = string.Format("'{0}' was hit", node.Name);
										}

			                       	});
			
		}


		void _renderingContainer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var mousePosition = e.GetPosition(LayoutRoot);
			_mousePosition.Text = string.Format("X: {0}, Y: {1}", mousePosition.X, mousePosition.Y);

			App.MyGame.Scene.MouseXPosition = (int)mousePosition.X;
			App.MyGame.Scene.MouseYPosition = (int)mousePosition.Y;
		}

		void _renderingContainer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			int i = 0;
			i++;
		}

	}
}

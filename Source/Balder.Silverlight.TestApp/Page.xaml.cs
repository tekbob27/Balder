using System;
using System.Windows.Controls;
using Balder.Core;
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

			Loaded += Page_Loaded;
		}

		void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{

			foreach (var node in App.MyGame.Scene.RenderableNodes)
			{
				node.Hover +=
					(ss, ee) => Dispatcher.BeginInvoke(() =>
					                                   	{
					                                   		_hitObject.Text = string.Format("'{0}' was hit", ((Node)ss).Name);
					                                   	});

			}
		}
	}
}

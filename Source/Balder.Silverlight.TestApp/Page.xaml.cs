using System.Windows.Controls;
using Balder.Core;

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

			/*
			foreach (var node in App.MyGame.Scene.RenderableNodes)
			{
				node.Click +=
					(ss, ee) => Dispatcher.BeginInvoke(() =>
					                                   	{
					                                   		_hitObject.Text = string.Format("'{0}' was clicked", ((Node)ss).Name);
					                                   	});

			}*/
		}
	}
}

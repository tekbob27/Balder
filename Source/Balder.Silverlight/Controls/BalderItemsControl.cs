using System.Windows;
using System.Windows.Controls;
using Balder.Core;
using Balder.Core.Execution;
using Ninject.Core;

namespace Balder.Silverlight.Controls
{
	public class BalderItemsControl : ItemsControl
	{
		public BalderItemsControl()
		{
			Loaded += ControlLoaded;
		}

		private void ControlLoaded(object sender, RoutedEventArgs e)
		{
			if( null == Runtime )
			{
				Runtime = Execution.Platform.Runtime;
				Core.Runtime.Instance.WireUpDependencies(this);
			}
		}

		public IRuntime Runtime { get; set; }

		[Inject]
		public IPlatform Platform { get; set; }

	}
}

using System.Windows.Controls;
using Balder.Core;
using Balder.Core.Execution;
using Balder.Silverlight.Execution;
using Ninject.Core;

namespace Balder.Silverlight.Controls
{
	public class BalderControl : ItemsControl
	{
		public BalderControl()
		{
			Loaded += BalderControlLoaded;
		}

		private void BalderControlLoaded(object sender, System.Windows.RoutedEventArgs e)
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

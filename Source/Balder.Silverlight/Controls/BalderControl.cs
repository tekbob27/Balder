using System.Windows;
using System.Windows.Controls;
using Balder.Core;
using Balder.Core.Content;
using Balder.Core.Execution;
using Ninject.Core;

namespace Balder.Silverlight.Controls
{
	public class BalderControl : Grid
	{
		public BalderControl()
		{
			Loaded += ControlLoaded;
		}

		private void ControlLoaded(object sender, RoutedEventArgs e)
		{
			if( null == Runtime )
			{
				Runtime = Execution.Platform.Runtime;
				Core.Runtime.Instance.WireUpDependencies(this);
				OnLoaded();
				
				Initialize();
				InitializeProperties();
			}
		}

		protected virtual void InitializeProperties() {}
		protected virtual void OnLoaded() {}
		protected virtual void Initialize() {}


		public IRuntime Runtime { get; set; }

		[Inject]
		public IPlatform Platform { get; set; }

		[Inject]
		public IContentManager ContentManager { get; set; }
	}
}

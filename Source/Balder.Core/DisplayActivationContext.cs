using System;
using Balder.Core.Display;
using Ninject.Core;
using Ninject.Core.Activation;
using Ninject.Core.Tracking;

namespace Balder.Core
{
	public class DisplayActivationContext : StandardContext
	{
		public DisplayActivationContext(IDisplay display, IKernel kernel, Type service, IScope scope)
			: base(kernel, service, scope)
		{
			Display = display;
		}

		public DisplayActivationContext(IDisplay display, IKernel kernel, Type service, IContext parent)
			: base(kernel, service, parent)
		{
			Display = display;
		}

		public IDisplay Display { get; private set; }
	}
}
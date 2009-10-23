using System;
using Ninject.Core;
using Ninject.Core.Activation;
using Ninject.Core.Binding;
using Ninject.Core.Creation.Providers;

namespace Balder.Core.Runtime
{
	public class AutoKernel : StandardKernel
	{
		public AutoKernel(params IModule[] modules)
			: base(modules)
		{
			
		}


		protected override IBinding ResolveBinding(Type service, IContext context)
		{
			try
			{
				var binding = base.ResolveBinding(service, context);
				if( null != binding )
				{
					return binding;
				}
			} catch
			{
			}


			var serviceName = service.Name;
			if( serviceName.StartsWith("I"))
			{
				var instanceName = string.Format("{0}.{1}", service.Namespace, serviceName.Substring(1));
				var serviceInstanceType = service.Assembly.GetType(instanceName);
				if( null != serviceInstanceType )
				{
					var binding = new StandardBinding(this, service);
					var provider = new StandardProvider(serviceInstanceType);
					binding.Provider = provider;
					return binding;
				}
			}

			return null;
		}
	}
}
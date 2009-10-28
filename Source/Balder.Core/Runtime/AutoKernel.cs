using System;
using System.Collections.Generic;
using Ninject.Core;
using Ninject.Core.Activation;
using Ninject.Core.Binding;
using Ninject.Core.Creation.Providers;

namespace Balder.Core.Runtime
{
	public delegate IBinding BindingResolver(IContext context);

	public class AutoKernel : StandardKernel
	{
		private Dictionary<Type, BindingResolver> _bindingResolvers;

		public AutoKernel(params IModule[] modules)
			: base(modules)
		{
			_bindingResolvers = new Dictionary<Type, BindingResolver>();
		}

		public void AddBindingResolver<T>(BindingResolver resolver)
		{
			_bindingResolvers[typeof (T)] = resolver;
			
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

			if (_bindingResolvers.ContainsKey(service))
			{
				var binding = _bindingResolvers[service](context);
				return binding;
			}
			else
			{
				var serviceName = service.Name;
				if (serviceName.StartsWith("I"))
				{
					var instanceName = string.Format("{0}.{1}", service.Namespace, serviceName.Substring(1));
					var serviceInstanceType = service.Assembly.GetType(instanceName);
					if (null != serviceInstanceType)
					{
						var binding = new StandardBinding(this, service);
						var provider = new StandardProvider(serviceInstanceType);
						binding.Provider = provider;
						return binding;
					}
				}
			}

			return null;
		}
	}
}
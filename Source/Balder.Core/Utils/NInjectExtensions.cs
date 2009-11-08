using System;
using Ninject.Core;
using Ninject.Core.Binding;
using Ninject.Core.Binding.Syntax;

namespace Balder.Core.Utils
{
	public static class NinjectExtensions
	{
		public static IBindingTargetSyntax Bind(this IKernel kernel, Type service)
		{
			var binding = new StandardBinding(kernel, service);
			IBindingTargetSyntax binder = new StandardBindingBuilder(binding);
			kernel.AddBinding(binding);
			return binder;
		}

		public static IBindingTargetSyntax Bind<T>(this IKernel kernel)
		{
			var binding = new StandardBinding(kernel, typeof(T));
			IBindingTargetSyntax binder = new StandardBindingBuilder(binding);
			kernel.AddBinding(binding);
			return binder;
		}

		public static void Bind<T>(this IKernel kernel, T obj)
		{
			var binding = new StandardBinding(kernel, typeof(T));
			IBindingTargetSyntax binder = new StandardBindingBuilder(binding);
			binder.ToConstant<T>(obj);
			kernel.AddBinding(binding);
		}
	}
}
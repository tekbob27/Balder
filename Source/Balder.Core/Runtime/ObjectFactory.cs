using System;
using Ninject.Core;

namespace Balder.Core.Runtime
{
	public class ObjectFactory : IObjectFactory
	{
		private readonly IKernel _kernel;

		public ObjectFactory(IKernel kernel)
		{
			_kernel = kernel;
		}

		public T Get<T>()
		{
			var objectToReturn = _kernel.Get<T>();
			return objectToReturn;
		}

		public object Get(Type type)
		{
			var objectToReturn = _kernel.Get(type);
			return objectToReturn;
		}
	}
}
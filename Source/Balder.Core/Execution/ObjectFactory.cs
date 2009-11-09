using System;
using System.Collections.Generic;
using Ninject.Core;
using Ninject.Core.Parameters;

namespace Balder.Core.Execution
{
	[Singleton]
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

		public T Get<T>(params ConstructorArgument[] constructorArguments)
		{
			var arguments = CreateConstructorArgumentsDictionary(constructorArguments);
			var objectToReturn = _kernel.Get<T>(With.Parameters.ConstructorArguments(arguments));
			return objectToReturn;
		}

		public object Get(Type type)
		{
			var objectToReturn = _kernel.Get(type);
			return objectToReturn;
		}

		public object Get(Type type, params ConstructorArgument[] constructorArguments)
		{
			var arguments = CreateConstructorArgumentsDictionary(constructorArguments);
			var objectToReturn = _kernel.Get(type, With.Parameters.ConstructorArguments(arguments));
			return objectToReturn;
		}


		public void WireUpDependencies(object objectToWire)
		{
			_kernel.Inject(objectToWire);
		}


		private static Dictionary<string, object> CreateConstructorArgumentsDictionary(IEnumerable<ConstructorArgument> constructorArguments)
		{
			var constructorArgumentsDictionary = new Dictionary<string, object>();
			foreach( var constructorArgument in constructorArguments )
			{
				constructorArgumentsDictionary[constructorArgument.Name] = constructorArgument.Value;
			}
			return constructorArgumentsDictionary;
		}

	}
}
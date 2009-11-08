using System;

namespace Balder.Core.Execution
{
	public interface IObjectFactory
	{
		T Get<T>();
		T Get<T>(params ConstructorArgument[] constructorArguments);
		object Get(Type type);
		object Get(Type type, params ConstructorArgument[] constructorArguments);
	}
}
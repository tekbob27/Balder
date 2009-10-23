using System;

namespace Balder.Core.Runtime
{
	public interface IObjectFactory
	{
		T Get<T>();
		object Get(Type type);
	}
}
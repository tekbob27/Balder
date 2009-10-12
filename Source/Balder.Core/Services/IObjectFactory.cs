namespace Balder.Core.Services
{
	public interface IObjectFactory
	{
		T Get<T>();
	}
}

namespace Balder.Core.Runtime
{
	public interface IRuntime
	{
		T CreateGame<T>() where T : Game;
		void RegisterGame<T>(T game) where T : Game;
	}
}

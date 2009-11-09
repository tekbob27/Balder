using System;
using Balder.Core.Display;
using Balder.Core.Execution;

namespace Balder.Core
{
	public interface IRuntime
	{
		T CreateGame<T>() where T : Game;
		Game CreateGame(Type type);
		void RegisterGame(IDisplay display, Game game);
		void WireUpDependencies(object objectToWire);
	}
}
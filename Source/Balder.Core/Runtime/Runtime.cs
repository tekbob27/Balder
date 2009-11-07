using Balder.Core.Collections;

namespace Balder.Core.Runtime
{
	public class Runtime : IRuntime
	{
		private readonly IPlatform _platform;
		private readonly IObjectFactory _objectFactory;

		private ActorCollection _games;

		public Runtime(IPlatform platform, IObjectFactory objectFactory)
		{
			_platform = platform;
			_objectFactory = objectFactory;
			InitializePlatformEventHandlers();
		}

		private void InitializePlatformEventHandlers()
		{
			_platform.StateChanged += PlatformStateChanged;
		}

		private void PlatformStateChanged(IPlatform platform, PlatformState state)
		{
			
		}

		public T CreateGame<T>() where T : Game
		{
			var game = _objectFactory.Get<T>();
			return game;
		}

		public void RegisterGame<T>(T game) where T : Game
		{
			_games.Add(game);
			HandleEventsForActor(game);
		}

		private void HandleEventsForActor<T>(T game) where T : Actor
		{
			
		}
	}
}

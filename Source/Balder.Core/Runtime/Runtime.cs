using Balder.Core.Collections;

namespace Balder.Core.Runtime
{
	public class Runtime : IRuntime
	{
		private readonly IPlatform _platform;
		private readonly IObjectFactory _objectFactory;
		private readonly ActorCollection _games;

		private bool _hasPlatformInitialized;
		private bool _hasPlatformLoaded;
		private bool _hasPlatformRun;

		public Runtime(IPlatform platform, IObjectFactory objectFactory)
		{
			_games = new ActorCollection();
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
			switch( state )
			{
				case PlatformState.Initialize:
					{
						_hasPlatformInitialized = true;
					}
					break;
				case PlatformState.Load:
					{
						_hasPlatformLoaded = true;
					}
					break;
				case PlatformState.Run:
					{
						_hasPlatformRun = true;
					}
					break;
			}
			HandleEventsForGames();
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

		private void HandleEventsForGames()
		{
			foreach( var game in _games )
			{
				HandleEventsForActor(game);
			}
		}

		private void HandleEventsForActor<T>(T actor) where T : Actor
		{
			if( !actor.HasInitialized && _hasPlatformInitialized )
			{
				actor.OnInitialize();
			}
			if( !actor.HasLoaded && _hasPlatformLoaded )
			{
				actor.OnLoadContent();
			}
			if( !actor.HasUpdated && _hasPlatformRun )
			{
				actor.OnUpdate();
			}
		}
	}
}

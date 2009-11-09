using System;
using System.Collections.Generic;
using Balder.Core.Assets;
using Balder.Core.Collections;
using Balder.Core.Display;
using Balder.Core.Execution;
using Balder.Core.Imaging;
using Balder.Core.Input;
using Balder.Core.Interfaces;
using Balder.Core.Objects.Flat;
using Balder.Core.Objects.Geometries;
using Ninject.Core;
using Ninject.Core.Behavior;

namespace Balder.Core
{
	[Singleton]
	public class Runtime : IRuntime
	{
		private static AutoKernel _kernel;
		private static Runtime _instance;
		private static readonly object InstanceLockObject = new object();

		private readonly IPlatform _platform;
		private readonly IObjectFactory _objectFactory;
		private readonly IAssetLoaderService _assetLoaderService;
		private readonly Dictionary<IDisplay, ActorCollection> _gamesPerDisplay;

		private bool _hasPlatformInitialized;
		private bool _hasPlatformLoaded;
		private bool _hasPlatformRun;

		public Runtime(IPlatform platform, IObjectFactory objectFactory, IAssetLoaderService assetLoaderService)
		{
			_gamesPerDisplay = new Dictionary<IDisplay, ActorCollection>();
			_platform = platform;
			_objectFactory = objectFactory;
			_assetLoaderService = assetLoaderService;
			InitializePlatformEventHandlers();
			_assetLoaderService.RegisterAssembly(GetType().Assembly);
		}

		public static Runtime Instance
		{
			get
			{
				lock( InstanceLockObject )
				{
					if( null == _instance )
					{
						_instance = _kernel.Get<IRuntime>() as Runtime;
					}
					return _instance;
				}
			}
		}

		public static void Initialize(IPlatform platform)
		{
			var runtimeModule = GetRuntimeModule(platform);
			_kernel = new AutoKernel(runtimeModule);
		}

		public T CreateGame<T>() where T : Game
		{
			var game = _objectFactory.Get<T>();
			return game;
		}

		public Game CreateGame(Type type)
		{
			var game = _objectFactory.Get(type) as Game;
			return game;
		}


		public void RegisterGame(IDisplay display, Game game)
		{
			WireUpDependencies(game);
			var actorCollection = GetGameCollectionForDisplay(display);
			actorCollection.Add(game);
			HandleEventsForActor(game);
		}

		public void WireUpDependencies(object objectToWire)
		{
			_objectFactory.WireUpDependencies(objectToWire);
		}

		private ActorCollection GetGameCollectionForDisplay(IDisplay display)
		{
			ActorCollection actorCollection = null;
			if( _gamesPerDisplay.ContainsKey(display) )
			{
				actorCollection = _gamesPerDisplay[display];
			} else
			{
				actorCollection = new ActorCollection();
				_gamesPerDisplay[display] = actorCollection;
			}
			return actorCollection;
		}


		private static InlineModule GetRuntimeModule(IPlatform platform)
		{
			var module = new InlineModule(
				m => m.Bind<IPlatform>().ToConstant(platform),
				m => m.Bind<IDisplayDevice>().ToConstant(platform.DisplayDevice),
				m => m.Bind<IMouseDevice>().ToConstant(platform.MouseDevice),
				m => m.Bind<IFileLoader>().To(platform.FileLoaderType).Using<SingletonBehavior>(),
				m => m.Bind<IGeometryContext>().To(platform.GeometryContextType),
				m => m.Bind<ISpriteContext>().To(platform.SpriteContextType),
				m => m.Bind<IImageContext>().To(platform.ImageContextType)
			);
			return module;
		}


		private void InitializePlatformEventHandlers()
		{
			_platform.StateChanged += PlatformStateChanged;
			_platform.DisplayDevice.Render += PlatformRender;
			_platform.DisplayDevice.Update += PlatformUpdate;
		}

		private void HandleEventsForGames()
		{
			foreach( var games in _gamesPerDisplay.Values )
			{
				foreach (var game in games )
				{
					HandleEventsForActor(game);
				}
			}
		}

		private void HandleEventsForActor<T>(T actor) where T : Actor
		{
			if( !actor.HasInitialized && HasPlatformInitialized )
			{
				actor.OnInitialize();
			}
			if( !actor.HasLoaded && HasPlatformLoaded )
			{
				actor.OnLoadContent();
			}
			if( !actor.HasUpdated && HasPlatformRun )
			{
				actor.OnUpdate();
			}
		}

		private bool IsPlatformInStateOrLater(PlatformState state, ref bool field)
		{
			if( field )
			{
				return true;
			}
			if( _platform.CurrentState >= state )
			{
				return true;
			}
			return false;
		}

		private bool HasPlatformLoaded { get { return IsPlatformInStateOrLater(PlatformState.Load, ref _hasPlatformLoaded); } }
		private bool HasPlatformInitialized { get { return IsPlatformInStateOrLater(PlatformState.Initialize, ref _hasPlatformInitialized); } }
		private bool HasPlatformRun { get { return IsPlatformInStateOrLater(PlatformState.Run, ref _hasPlatformRun); } }


		private void PlatformUpdate(IDisplay display)
		{
			CallMethodOnGames(display, g => g.OnUpdate());
		}

		private void PlatformRender(IDisplay display)
		{
			CallMethodOnGames(display, g => g.OnRender());
		}

		private void CallMethodOnGames(IDisplay display, Action<Game> action)
		{
			if( _gamesPerDisplay.ContainsKey(display))
			{
				var games = _gamesPerDisplay[display];
				foreach (Game game in games)
				{
					action(game);
				}
			}
		}

		private void PlatformStateChanged(IPlatform platform, PlatformState state)
		{
			switch (state)
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

	}
}

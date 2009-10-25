using System;
using System.Collections.Generic;
using Balder.Core.Assets;
using Balder.Core.Content;
using Balder.Core.Imaging;
using Balder.Core.Interfaces;
using Balder.Core.Objects.Flat;
using Balder.Core.Objects.Geometries;
using Balder.Core.Services;
using Ninject.Core;
using Ninject.Core.Activation;
using Ninject.Core.Tracking;

namespace Balder.Core.Runtime
{
	public class EngineRuntime
	{
		public static readonly EngineRuntime Instance = new EngineRuntime();

		private IKernel _kernel;
		private List<Game> _games;

		private EngineRuntime()
		{
			_games = new List<Game>();
		}

		public void Initialize(ITargetDevice targetDevice)
		{
			if (null == _kernel)
			{
				_kernel = new AutoKernel(GetSpecificModule(targetDevice));
				TargetDevice = targetDevice;
				var assetLoaderService = _kernel.Get<IAssetLoaderService>();
				assetLoaderService.Initialize();
				targetDevice.RegisterAssetLoaders(assetLoaderService);
			}
		}

		public class DisplayActivationContext : StandardContext
		{
			public DisplayActivationContext(IDisplay display, IKernel kernel, Type service, IScope scope)
				: base(kernel, service, scope)
			{
				Display = display;
			}

			public DisplayActivationContext(IDisplay display, IKernel kernel, Type service, IContext parent)
				: base(kernel, service, parent)
			{
				Display = display;
			}

			public IDisplay Display { get; private set; }
		}

		public void RegisterGame<T>(IDisplay display)
			where T:Game
		{
			var displayActivationContext = new DisplayActivationContext(display, _kernel, typeof (T),
			                                                            _kernel.CreateScope());
			var game = _kernel.Get<T>(displayActivationContext);
			if (null != game)
			{
				game.OnInitialize();
				game.OnLoadContent();
				game.OnLoaded();
				_games.Add(game);
			}
		}

		private StandardModule GetSpecificModule(ITargetDevice targetDevice)
		{
			var inlineModule = new InlineModule(
				(m) =>m.Bind<IDisplay>().ToMethod(c=>((DisplayActivationContext)c.ParentContext).Display),
				(m)=>m.Bind<ITargetDevice>().ToConstant(targetDevice),
				(m)=>m.Bind<IGeometryContext>().To(targetDevice.GeometryContextType),
				(m)=>m.Bind<IImageContext>().To(targetDevice.ImageContextType),
				(m)=>m.Bind<ISpriteContext>().To(targetDevice.SpriteContextType),
				(m)=>m.Bind<IFileLoader>().ToMethod((c)=>CreateFileLoader(targetDevice))
			);
			return inlineModule;
		}

		private IFileLoader CreateFileLoader(ITargetDevice targetDevice)
		{
			var fileLoader = _kernel.Get(targetDevice.FileLoaderType) as IFileLoader;
			if (null != fileLoader)
			{
				fileLoader.ContentManager = _kernel.Get<IContentManager>();
			}
			return fileLoader;
		}

		public ITargetDevice TargetDevice { get; private set; }
		public DebugLevel DebugLevel { get; set; }
	}
}
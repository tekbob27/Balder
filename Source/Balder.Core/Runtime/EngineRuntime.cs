using System;
using Balder.Core.FlatObjects;
using Balder.Core.Geometries;
using Balder.Core.Imaging;
using Balder.Core.Interfaces;
using Balder.Core.Services;
using Ninject.Core;

namespace Balder.Core.Runtime
{
	public class EngineRuntime
	{
		public static readonly EngineRuntime Instance = new EngineRuntime();

		private EngineRuntime()
		{
		}

		public void Initialize(ITargetDevice targetDevice)
		{
			Kernel = new StandardKernel(new CoreModule(), GetSpecificModule(targetDevice));

			TargetDevice = targetDevice;

			if (null != targetDevice.GameType)
			{
				CurrentGame = Kernel.Get(targetDevice.GameType) as Game;
				if (null != CurrentGame)
				{
					var assetLoaderService = Kernel.Get<IAssetLoaderService>();
					assetLoaderService.Initialize();

					targetDevice.RegisterAssetLoaders(assetLoaderService);

					CurrentGame.OnInitialize();
					CurrentGame.OnLoadContent();
					CurrentGame.OnLoaded();
				}
			}
		}

		private StandardModule GetSpecificModule(ITargetDevice targetDevice)
		{
			var display = targetDevice.Display;

			var inlineModule = new InlineModule(
				(m)=>m.Bind<IDisplay>().ToConstant(display),
				(m)=>m.Bind<ITargetDevice>().ToConstant(targetDevice),
				(m)=>m.Bind<IGeometryContext>().To(targetDevice.GeometryContextType),
				(m)=>m.Bind<IImageContext>().To(targetDevice.ImageContextType),
				(m)=>m.Bind<ISpriteContext>().To(targetDevice.SpriteContextType),
				(m)=>m.Bind<IFileLoader>().ToMethod((c)=>CreateFileLoader(targetDevice))
			);
			return inlineModule;
		}

		private IFileLoader	CreateFileLoader(ITargetDevice targetDevice)
		{
			var fileLoader = Activator.CreateInstance(targetDevice.FileLoaderType) as IFileLoader;
			if (null != fileLoader)
			{
				fileLoader.Game = CurrentGame;
				fileLoader.ContentManager = Kernel.Get<IContentManager>();
			}
			return fileLoader;
		}

		public IKernel Kernel { get; private set; }
		public Game CurrentGame { get; private set; }
		public ITargetDevice TargetDevice { get; private set; }

		public DebugLevel DebugLevel { get; set; }
	}
}
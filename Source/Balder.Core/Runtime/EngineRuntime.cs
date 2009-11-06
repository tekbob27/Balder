using System;
using System.Collections.Generic;
using Balder.Core.Assets;
using Balder.Core.Content;
using Balder.Core.Display;
using Balder.Core.Imaging;
using Balder.Core.Input;
using Balder.Core.Interfaces;
using Balder.Core.Objects.Flat;
using Balder.Core.Objects.Geometries;
using Balder.Core.Services;
using Ninject.Core;
using Ninject.Core.Activation;
using Ninject.Core.Binding;
using Ninject.Core.Binding.Syntax;
using Ninject.Core.Tracking;

namespace Balder.Core.Runtime
{
	public class EngineRuntime
	{
		public static readonly EngineRuntime Instance = new EngineRuntime();

		private AutoKernel _kernel;
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
				_kernel.AddBindingResolver<IDisplay>(DisplayBindingResolver);
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


		public IDisplay CreateDisplay()
		{
			var display = _kernel.Get<IDisplay>();
			return display;
		}


		public T RegisterGame<T>(IDisplay display)
			where T : Game
		{
			var displayActivationContext = new DisplayActivationContext(display, _kernel, typeof(T),
																		_kernel.CreateScope());
			var game = _kernel.Get<T>(displayActivationContext);
			if (null != game)
			{
				game.OnInitialize();
				game.OnLoadContent();
				game.OnLoaded();
				_games.Add(game);
				return game;
			}
			return null;
		}

		private StandardModule GetSpecificModule(ITargetDevice targetDevice)
		{
			var inlineModule = new InlineModule(
				(m) => m.Bind<ITargetDevice>().ToConstant(targetDevice),
				(m) => m.Bind<IGeometryContext>().To(targetDevice.GeometryContextType),
				(m) => m.Bind<IImageContext>().To(targetDevice.ImageContextType),
				(m) => m.Bind<ISpriteContext>().To(targetDevice.SpriteContextType),
				(m) => m.Bind<IFileLoader>().ToMethod((c) => CreateFileLoader(targetDevice)),
				(m) => m.Bind<IMouseDevice>().ToConstant(targetDevice.MouseDevice)

			);
			return inlineModule;
		}

		private IBinding DisplayBindingResolver(IContext context)
		{
			var binding = new StandardBinding(_kernel, typeof(IDisplay));
			IBindingTargetSyntax binder = new StandardBindingBuilder(binding);

			if (null != context.ParentContext &&
				context.ParentContext is DisplayActivationContext)
			{
				var display = ((DisplayActivationContext)context.ParentContext).Display;
				binder.ToConstant(display);
			} else
			{
				binder.To(TargetDevice.DisplayType);
			}
			
			return binding;
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
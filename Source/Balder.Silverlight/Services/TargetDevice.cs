using System;
using Balder.Core;
using Balder.Core.Assets;
using Balder.Core.Display;
using Balder.Core.Input;
using Balder.Core.Services;
using Balder.Core.SoftwareRendering.Rendering;
using Balder.Silverlight.Implementation;
using Balder.Core.Runtime;
using Balder.Core.SoftwareRendering;
using Balder.Silverlight.Input;

namespace Balder.Silverlight.Services
{
	public class TargetDevice : ITargetDevice
	{
		public TargetDevice()
		{
			MouseDevice = new MouseDevice();
		}


		public static void Initialize()
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);
		}

		public static T Initialize<T>()
			where T:Game
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);

			var display = new Display();
			display.Initialize();

			((MouseDevice) targetDevice.MouseDevice).Initialize(display);
			var game = EngineRuntime.Instance.RegisterGame<T>(display);
			return game;
		}


		public string Name { get { return "Silverlight Target Device"; } }
		public string Description { get { return string.Empty; }}


		public IDisplay Display { get; private set; }
		public Type GeometryContextType { get { return typeof (GeometryContext); } }
		public Type ImageContextType { get { return typeof (ImageContext); } }
		public Type SpriteContextType { get { return typeof (SpriteContext); } }
		public Type ShapeContextType { get { return typeof (ShapeContext); } }
		public Type DisplayType { get { return typeof (Display); } }
		public IMouseDevice MouseDevice { get; private set; }

		public Type FileLoaderType { get { return typeof (FileLoader); } }
		

		public void RegisterAssetLoaders(IAssetLoaderService assetLoaderService)
		{
			var type = GetType();
			var assembly = type.Assembly;
			
			// Todo: Look into the literal below - my enemy number one: Literals
			assetLoaderService.RegisterNamespace(assembly,"Balder.Silverlight.AssetLoaders");
		}
	}
}

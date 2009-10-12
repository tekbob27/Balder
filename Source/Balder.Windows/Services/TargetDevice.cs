using System;
using Balder.Core;
using Balder.Core.Runtime;
using Balder.Core.Services;
using Balder.Core.SoftwareRendering;
using Balder.Windows.Implementation;

namespace Balder.Windows.Services
{
	public class TargetDevice : ITargetDevice
	{
		public TargetDevice(Type gameType)
		{
			Display = new Display();
			GameType = gameType;
		}


		public static void Initialize()
		{
			var targetDevice = new TargetDevice(null);
			EngineRuntime.Instance.Initialize(targetDevice);
		}

		public static void Initialize<T>()
			where T : Game
		{
			var targetDevice = new TargetDevice(typeof(T));
			EngineRuntime.Instance.Initialize(targetDevice);
		}

		public string Name { get { return "Windows"; } }
		public string Description { get { return ""; } }

		public IDisplay Display { get; private set; }

		public Type GeometryContextType { get { return typeof(GeometryContext); } }
		public Type ImageContextType { get { return typeof (ImageContext); } }
		public Type SpriteContextType { get { return typeof (SpriteContext); } }

		public Type GameType { get; private set; }

		public Type FileLoaderType { get { return typeof(FileLoader); } }


		public void RegisterAssetLoaders(IAssetLoaderService assetLoaderService)
		{
			var type = GetType();
			var assembly = type.Assembly;

			// Todo: Look into the literal below - my enemy number one: Literals
			assetLoaderService.RegisterNamespace(assembly, "Balder.Windows.AssetLoaders");

		}
	}
}

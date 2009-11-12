using System;
using Balder.Core;
using Balder.Core.Assets;
using Balder.Core.Display;
using Balder.Core.Execution;
using Balder.Core.Input;
using Balder.Core.SoftwareRendering;
using Balder.Core.SoftwareRendering.Rendering;
using Balder.Silverlight.Display;
using Balder.Silverlight.Implementation;
using Balder.Silverlight.Input;

namespace Balder.Silverlight.Execution
{
	public class Platform : IPlatform
	{
		public static IRuntime Runtime;

		public event PlatformStateChange BeforeStateChange = (p, s) => { };
		public event PlatformStateChange StateChanged = (p, s) => { };

		static Platform()
		{
			var platform = new Platform();
			Core.Runtime.Initialize(platform);
			Runtime = Core.Runtime.Instance;
		}

		public Platform()
		{
			CurrentState = PlatformState.Idle;
			InitializeObjects();
			Initialize();
		}

		private void InitializeObjects()
		{
			DisplayDevice = new DisplayDevice();
			MouseDevice = new MouseDevice();
		}

		private void Initialize()
		{
			ChangeState(PlatformState.Initialize);
			ChangeState(PlatformState.Load);
			ChangeState(PlatformState.Run);
		}


		public string PlatformName
		{
			get { return "Silverlight"; }
		}

		public IDisplayDevice DisplayDevice { get; private set; }
		public IMouseDevice MouseDevice { get; private set; }
		public Type FileLoaderType { get { return typeof(FileLoader); } }
		public Type GeometryContextType { get { return typeof(GeometryContext); } }
		public Type SpriteContextType { get { return typeof(SpriteContext); } }
		public Type ImageContextType { get { return typeof(ImageContext); } }
		public Type ShapeContextType { get { return typeof(ShapeContext); } }

		public PlatformState CurrentState { get; private set; }

		private void ChangeState(PlatformState platformState)
		{
			BeforeStateChange(this, platformState);
			CurrentState = platformState;
			StateChanged(this, platformState);
		}

		public void RegisterAssetLoaders(IAssetLoaderService assetLoaderService)
		{
			var type = GetType();
			var assembly = type.Assembly;

			// Todo: Look into the literal below - my enemy number one: Literals
			assetLoaderService.RegisterNamespace(assembly, "Balder.Silverlight.AssetLoaders");
		}
	}
}

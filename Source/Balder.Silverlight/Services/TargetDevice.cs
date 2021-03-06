﻿using System;
using Balder.Core;
using Balder.Core.Assets;
using Balder.Core.Display;
using Balder.Core.Services;
using Balder.Silverlight.Implementation;
using Balder.Core.Runtime;
using Balder.Core.SoftwareRendering;

namespace Balder.Silverlight.Services
{
	public class TargetDevice : ITargetDevice
	{
		public static void Initialize()
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);
		}

		public static void Initialize<T>()
			where T:Game
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);

			var display = new Display();
			display.Initialize();
			EngineRuntime.Instance.RegisterGame<T>(display);
		}


		public string Name { get { return "Silverlight Target Device"; } }
		public string Description { get { return string.Empty; }}


		public IDisplay Display { get; private set; }
		public Type GeometryContextType { get { return typeof (GeometryContext); } }
		public Type ImageContextType { get { return typeof (ImageContext); } }
		public Type SpriteContextType { get { return typeof (SpriteContext); } }
		public Type DisplayType { get { return typeof (Display); } }
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

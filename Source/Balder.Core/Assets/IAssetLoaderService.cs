﻿using System.Reflection;

namespace Balder.Core.Assets
{
	public interface IAssetLoaderService
	{
		void Initialize();
		void RegisterAssembly(string fullyQualifiedName);
		void RegisterAssembly(Assembly assembly);
		void RegisterNamespace(Assembly assembly, string ns);
		void RegisterNamespace(Assembly assembly, string ns, bool recursive);
		void RegisterLoader<T>(AssetLoader<T> loader) where T : IAssetPart;
		AssetLoader<T> GetLoader<T>(string assetName) where T : IAssetPart;
		IAssetLoader[] AvailableLoaders { get; }
	}
}
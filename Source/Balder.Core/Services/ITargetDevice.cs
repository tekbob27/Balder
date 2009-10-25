using System;
using Balder.Core.Assets;

namespace Balder.Core.Services
{
	public interface ITargetDevice
	{
		string Name { get; }
		string Description { get; }

		Type GeometryContextType { get; }
		Type ImageContextType { get; }
		Type SpriteContextType { get; }
		Type FileLoaderType { get; }
		Type DisplayType { get;  }

		void RegisterAssetLoaders(IAssetLoaderService assetLoaderService);
	}
}

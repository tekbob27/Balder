using System;
using Balder.Core.Assets;

namespace Balder.Core.Services
{
	public interface ITargetDevice
	{
		string Name { get; }
		string Description { get; }

		IDisplay Display { get; }
		Type GeometryContextType { get; }
		Type ImageContextType { get; }
		Type SpriteContextType { get; }
		Type GameType { get; }
		Type FileLoaderType { get; }

		void RegisterAssetLoaders(IAssetLoaderService assetLoaderService);
	}
}

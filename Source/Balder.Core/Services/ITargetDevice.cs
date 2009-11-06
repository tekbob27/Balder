using System;
using Balder.Core.Assets;
using Balder.Core.Input;

namespace Balder.Core.Services
{
	public interface ITargetDevice
	{
		string Name { get; }
		string Description { get; }

		Type GeometryContextType { get; }
		Type ImageContextType { get; }
		Type SpriteContextType { get; }
		Type ShapeContextType { get; }
		Type FileLoaderType { get; }
		Type DisplayType { get;  }
		IMouseDevice MouseDevice { get; }

		void RegisterAssetLoaders(IAssetLoaderService assetLoaderService);
	}
}

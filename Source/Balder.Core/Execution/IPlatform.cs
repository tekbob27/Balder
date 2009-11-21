using System;
using Balder.Core.Assets;
using Balder.Core.Display;
using Balder.Core.Input;

namespace Balder.Core.Execution
{
	public enum PlatformState
	{
		Idle=0,
		Initialize,
		Load,
		Run,
		Exit
	}

	public delegate void PlatformStateChange(IPlatform platform, PlatformState state);

	public interface IPlatform
	{
		event PlatformStateChange BeforeStateChange;
		event PlatformStateChange StateChanged;

		string PlatformName { get; }

		string EntryAssemblyName { get; }

		bool IsInDesignMode { get; }
		IDisplayDevice DisplayDevice { get; }
		IMouseDevice MouseDevice { get; }


		Type FileLoaderType { get; }
		Type GeometryContextType { get; }
		Type SpriteContextType { get; }
		Type ImageContextType { get; }
		Type ShapeContextType { get; }

		PlatformState CurrentState { get; }
		void RegisterAssetLoaders(IAssetLoaderService assetLoaderService);
	}
}
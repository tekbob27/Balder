using System.Reflection;
using Balder.Core.Interfaces;

namespace Balder.Core.Services
{
	public interface IAssetLoaderService
	{
		void Initialize();
		void RegisterAssembly(string fullyQualifiedName);
		void RegisterAssembly(Assembly assembly);
		void RegisterNamespace(Assembly assembly, string ns);
		void RegisterNamespace(Assembly assembly, string ns, bool recursive);
		void RegisterLoader(IAssetLoader loader);
		AssetLoader<T> GetLoader<T>(string assetName) where T : IAssetPart;
		IAssetLoader[] AvailableLoaders { get; }
	}
}

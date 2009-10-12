using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Core
{
	public interface IAssetLoader
	{
		string[] FileExtensions { get; }
	}

	public abstract class AssetLoader<T> : IAssetLoader
		where T:IAssetPart
	{
		protected AssetLoader(IFileLoader fileLoader, IContentManager contentManager)
		{
			FileLoader = fileLoader;
			ContentManager = contentManager;
		}

		public IFileLoader FileLoader { get; private set; }
		public IContentManager ContentManager { get; private set; }

		public abstract string[] FileExtensions { get; }
		public abstract T[] Load(string assetName);
	}
}

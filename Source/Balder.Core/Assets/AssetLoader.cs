using Balder.Core.Content;

namespace Balder.Core.Assets
{
	public abstract class AssetLoader<T> : IAssetLoader
		where T:IAssetPart
	{
		protected AssetLoader(IFileLoader fileLoader, IContentManager contentManager)
		{
			FileLoader = fileLoader;
			ContentManager = contentManager;
		}

		protected IFileLoader FileLoader { get; private set; }
		protected IContentManager ContentManager { get; private set; }

		public abstract string[] FileExtensions { get; }
		public abstract T[] Load(string assetName);
	}
}
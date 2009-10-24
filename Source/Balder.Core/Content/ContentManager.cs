using Balder.Core.Assets;
using Balder.Core.Runtime;
using Ninject.Core;

namespace Balder.Core.Content
{
	[Singleton]
	public class ContentManager : IContentManager
	{
		private readonly IObjectFactory _objectFactory;
		public const string DefaultAssetsRoot = "Assets";

		public ContentManager(IObjectFactory objectFactory)
		{
			_objectFactory = objectFactory;
			AssetsRoot = DefaultAssetsRoot;
			Creator = new ContentCreator(objectFactory);
		}

		public T Load<T>(string assetName)
			where T:IAsset
		{
			var asset = _objectFactory.Get<T>();
			asset.Load(assetName);
			return asset;
		}

		public T CreateAssetPart<T>() where T:IAssetPart
		{
			var part = _objectFactory.Get<T>();
			return part;
		}

		public ContentCreator Creator { get; private set; }

		public string AssetsRoot { get; set;}
	}
}
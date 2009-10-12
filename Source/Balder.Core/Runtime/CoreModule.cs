using Balder.Core.Implementation;
using Balder.Core.Services;
using Ninject.Core;

namespace Balder.Core.Runtime
{
	public class CoreModule : StandardModule
	{
		public override void Load()
		{
			var contentManager = new ContentManager();
			Bind<IContentManager>().ToConstant(contentManager);

			var assetLoaderService = new AssetLoaderService();
			Bind<IAssetLoaderService>().ToConstant(assetLoaderService);
		}
	}
}

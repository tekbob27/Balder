using Balder.Core.Assets;

namespace Balder.Core.Content
{
	public interface IContentManager
	{
		T Load<T>(string assetName) where T : IAsset;
		T CreateAssetPart<T>() where T : IAssetPart;

		ContentCreator Creator { get; }

		string AssetsRoot { get; set; }
	}
}
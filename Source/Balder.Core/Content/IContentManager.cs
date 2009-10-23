using Balder.Core.Assets;
using Balder.Core.Objects.Geometries;

namespace Balder.Core.Content
{
	public interface IContentManager
	{
		T Load<T>(string assetName) where T : IAsset;
		T CreateGeometry<T>() where T : Geometry;

		T CreateAssetPart<T>() where T : IAssetPart;

		Box CreateBox();
		Geometry CreateSphere();
		Cylinder CreateCylinder(float radius, float height, int segments, int heightSegments);

		Game Game { get; set; }
		string AssetsRoot { get; set; }
	}
}
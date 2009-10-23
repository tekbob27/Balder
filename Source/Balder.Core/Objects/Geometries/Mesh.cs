using Balder.Core.Assets;
using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Core.Objects.Geometries
{
	public class Mesh : RenderableNode, IAsset
	{
		private Geometry[] _geometries;
		private readonly IAssetLoaderService _assetLoaderService;

		public Mesh(IAssetLoaderService assetLoaderService)
		{
			_assetLoaderService = assetLoaderService;
		}
		
		public void Load(string assetName)
		{
			var loader = _assetLoaderService.GetLoader<Geometry>(assetName);
			_geometries = loader.Load(assetName);
		}

		public override void Render(IViewport viewport, Matrix view, Matrix projection)
		{
			for( var geometryIndex=0; geometryIndex<_geometries.Length; geometryIndex++ )
			{
				var geometry = _geometries[geometryIndex];

				var localWorld = World*geometry.World;

				geometry.GeometryContext.Render(viewport,view,projection,localWorld);
			}
		}

		public int TotalFaceCount
		{
			get
			{
				var faceCount = 0;
				for (var geometryIndex = 0; geometryIndex < _geometries.Length; geometryIndex++)
				{
					var geometry = _geometries[geometryIndex];
					faceCount += geometry.GeometryContext.FaceCount;
				}
				return faceCount;
			}
		}
	}
}
using Balder.Core.Assets;
using Balder.Core.Debug;
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Ninject.Core;

namespace Balder.Core.Objects.Geometries
{
	public class Mesh : RenderableNode, IAsset
	{
		private Geometry[] _geometries;
		private readonly IAssetLoaderService _assetLoaderService;
		private readonly IDebugRenderer _debugRenderer;

		public Mesh(IAssetLoaderService assetLoaderService, IDebugRenderer debugRenderer)
		{
			_assetLoaderService = assetLoaderService;
			_debugRenderer = debugRenderer;
		}

		public void Load(string assetName)
		{
			var loader = _assetLoaderService.GetLoader<Geometry>(assetName);
			_geometries = loader.Load(assetName);

			var boundingSphere = new BoundingSphere(Vector.Zero,0);
			for( var geometryIndex=0; geometryIndex<_geometries.Length; geometryIndex++ )
			{
				var geometry = _geometries[geometryIndex];
				geometry.InitializeBoundingSphere();
				boundingSphere = BoundingSphere.CreateMerged(boundingSphere, geometry.BoundingSphere);
			}
			//boundingSphere = new BoundingSphere(Vector.Zero, 100);
			BoundingSphere = boundingSphere;
		}

		public override void Render(IViewport viewport, Matrix view, Matrix projection)
		{
			for( var geometryIndex=0; geometryIndex<_geometries.Length; geometryIndex++ )
			{
				var geometry = _geometries[geometryIndex];

				var localWorld = World * geometry.World;

				_debugRenderer.RenderBoundingSphere(BoundingSphere, viewport, view, projection, localWorld);
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
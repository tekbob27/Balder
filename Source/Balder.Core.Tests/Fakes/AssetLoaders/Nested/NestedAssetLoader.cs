using System;
using Balder.Core.Assets;
using Balder.Core.Objects.Geometries;

namespace Balder.Core.Tests.Fakes.AssetLoaders.Nested
{
	public class NestedAssetLoader : AssetLoader<Geometry>
	{
		public NestedAssetLoader()
			: base(null,null)
		{
			
		}


		public override string[] FileExtensions
		{
			get { return new[] {"fakeNested"}; }
		}

		public override Geometry[] Load(string assetName)
		{
			throw new NotImplementedException();
		}
	}
}
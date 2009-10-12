using System;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Core.Tests.Fakes.AssetLoaders.Nested
{
	public class NestedAssetLoader : AssetLoader<Geometry>
	{
		public NestedAssetLoader(IFileLoader fileLoader, IContentManager contentManager)
			: base(fileLoader, contentManager)
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
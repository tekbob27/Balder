using System;
using Balder.Core.Geometries;

namespace Balder.Core.Tests.Fakes.AssetLoaders
{
	public class RootAssetLoader : AssetLoader<Geometry>
	{
		public RootAssetLoader()
			: base(null,null)
		{
			
		}

		public override string[] FileExtensions
		{
			get { return new[] {"fake"}; }
		}

		public override Geometry[] Load(string assetName)
		{
			throw new NotImplementedException();
		}
	}
}
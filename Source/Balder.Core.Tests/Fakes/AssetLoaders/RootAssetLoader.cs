using System;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Core.Tests.Fakes.AssetLoaders
{
	public class RootAssetLoader : AssetLoader<Geometry>
	{
		public RootAssetLoader(IFileLoader fileLoader, IContentManager contentManager)
			: base(fileLoader, contentManager)
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
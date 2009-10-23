using System;
using Balder.Core.Content;
using Balder.Core.Interfaces;
using Balder.Core.Objects.Geometries;

namespace Balder.Core.Assets.AssetLoaders
{
	public class XLoader : AssetLoader<Geometry>
	{
		public XLoader(IFileLoader fileLoader, IContentManager contentManager)
			: base(fileLoader, contentManager)
		{
		}

		public override Geometry[] Load(string assetName)
		{
			throw new NotImplementedException();
		}

		public override string[] FileExtensions
		{
			get { return new[] {"x"}; }
		}
	}
}
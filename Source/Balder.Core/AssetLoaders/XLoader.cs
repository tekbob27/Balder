using System;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Core.AssetLoaders
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
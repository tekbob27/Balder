using System;
using Balder.Core.Interfaces;
using Ninject.Core;

namespace Balder.Core.Imaging
{
	public class Image : IAsset, IAssetPart
	{
		[Inject]
		public IImageContext ImageContext { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public string Name { get; set; }

		public void Load(string assetName)
		{
		}
	}
}
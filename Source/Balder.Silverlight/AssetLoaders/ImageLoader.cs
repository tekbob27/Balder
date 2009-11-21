using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Balder.Core.Assets;
using Balder.Core.Content;
using Balder.Core.Imaging;

namespace Balder.Silverlight.AssetLoaders
{
	public class ImageLoader : AssetLoader<Image>
	{
		public ImageLoader(IFileLoader fileLoader, IContentManager contentManager)
			: base(fileLoader, contentManager)
		{
		}


		public override Image[] Load(string assetName)
		{
			var stream = FileLoader.GetStream(assetName);

			var bitmapImage = new BitmapImage();
			bitmapImage.SetSource(stream);


			var writeableBitmap = new WriteableBitmap(bitmapImage);
			var width = writeableBitmap.PixelWidth;
			var height = writeableBitmap.PixelHeight;
			var frame = ContentManager.CreateAssetPart<Image>();
			frame.Width = width;
			frame.Height = height;

			var imageAsBytes = new byte[width * height * 4];

			Buffer.BlockCopy(writeableBitmap.Pixels,0,imageAsBytes,0,imageAsBytes.Length);

			frame.ImageContext.SetFrame(imageAsBytes);

			return new[] { frame };
		}


		public override string[] FileExtensions
		{
			get { return new[] { "png", "jpg" }; }
		}
	}
}

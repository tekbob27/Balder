using Balder.Core.Imaging;
using Balder.Core.SharpPNG;
using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Core.AssetLoaders
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

			byte[] bytes = null;
			var width = 0;
			var height = 0;
			if( Png.IsPng(stream) )
			{
				stream.Position = 0;
				var png = new Png();
				var imageOutput = new PngImageOutput();
				png.read(stream,imageOutput);
				width = png.ihdr.width;
				height = png.ihdr.height;
				bytes = imageOutput.Bytes;
			}

			var frame = ContentManager.CreateAssetPart<Image>();
			frame.Width = width;
			frame.Height = height;
			if (null != bytes)
			{
				frame.ImageContext.SetFrame(bytes);
			}

			return new[] { frame };
		}

		public override string[] FileExtensions
		{
			get { return new[] {"png","jpg"}; }
		}
	}
}
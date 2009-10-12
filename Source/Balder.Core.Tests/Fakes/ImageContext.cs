using System;
using Balder.Core.Imaging;

namespace Balder.Core.Tests.Fakes
{
	public class ImageContext : IImageContext
	{
		public void SetFrame(byte[] frameBytes)
		{
			
		}

		public void SetFrame(ImageFormat format, byte[] frameBytes)
		{
		}

		public void SetFrame(ImageFormat format, byte[] frameBytes, ImagePalette palette)
		{
		}

		public ImageFormat[] SupportedImageFormats
		{
			get { throw new NotImplementedException(); }
		}
	}
}

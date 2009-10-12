using Balder.Core.Imaging;

namespace Balder.Core.Extensions
{
	public static class ImageFormatExtensions
	{
		public static bool IsSupported(this ImageFormat[] formats, ImageFormat desiredFormat)
		{
			for( var formatIndex=0; formatIndex<formats.Length; formatIndex++ )
			{
				if( formats[formatIndex].Equals(desiredFormat) )
				{
					return true;
				}
			}
			return false;
		}

		public static ImageFormat GetBestSuitedFormat(this ImageFormat[] formats, ImageFormat desiredFormat)
		{
			// Todo: Also look for PixelFormat - if there is a format that matches with both Depth and PixelFormat, choose this first.
			// Do a prioritizing of formats and depths - needs some thinking. :)
			for (var formatIndex = 0; formatIndex < formats.Length; formatIndex++)
			{
				if (formats[formatIndex].Depth >= desiredFormat.Depth)
				{
					return formats[formatIndex];
				}
			}

			return null;
		}
	}
}

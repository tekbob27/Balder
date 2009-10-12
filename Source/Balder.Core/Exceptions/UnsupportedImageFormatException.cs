using System;
using Balder.Core.Imaging;

namespace Balder.Core.Exceptions
{
	public class UnsupportedImageFormatException : ArgumentException
	{
		public UnsupportedImageFormatException(ImageFormat format)
			: base("Unsupported ImageFormat ("+format.ToString()+")")
		{
		}

		public UnsupportedImageFormatException(ImageFormat format, string message)
			: base("Unsupported ImageFormat (" + format.ToString() + ") - Message: "+message)
		{
		}

	}
}

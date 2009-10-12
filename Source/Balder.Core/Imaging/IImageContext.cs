namespace Balder.Core.Imaging
{
	public interface IImageContext
	{
		void SetFrame(byte[] frameBytes);
		void SetFrame(ImageFormat format, byte[] frameBytes);
		void SetFrame(ImageFormat format, byte[] frameBytes, ImagePalette palette);

		ImageFormat[] SupportedImageFormats { get; }
	}
}
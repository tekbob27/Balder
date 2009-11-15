using Balder.Core.Imaging;

namespace Balder.Core.SoftwareRendering
{
	public interface ISpanRenderer
	{
		bool SupportsDepthBuffer { get; }
		void Flat(IBuffers buffer, Span span, Color color);
		void Gouraud(IBuffers buffer, Span span);
		void Texture(IBuffers buffer, Span span, Image image, ImageContext texture);
	}
}

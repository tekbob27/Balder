using System;

namespace Balder.Core.SoftwareRendering
{
	public interface IBuffers
	{
		IFrameBuffer FrameBuffer { get; }
		UInt32[] DepthBuffer { get; }
		int Width { get; }
		int Height { get; }

		void Swap();
		void Clear();
		void Show();
	}
}

namespace Balder.Core.SoftwareRendering
{
	public interface IFrameBuffer
	{
		void Initialize(int width, int height);
		int Stride { get; }

		int RedPosition { get; }
		int BluePosition { get; }
		int GreenPosition { get; }
		int AlphaPosition { get; }

		int[] Pixels { get; }
		int[] BackBuffer { get; }

		void Clear();
		void Swap();
		void Show();
	}
}

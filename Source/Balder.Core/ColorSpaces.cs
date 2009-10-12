namespace Balder.Core
{
	public static class ColorSpaces
	{
		public static readonly ColorSpace RGB24BPP = new ColorSpace
		{
			RedPosition = 0,
			GreenPosition = 1,
			BluePosition = 2,
			BytePositions = true
		};

		public static readonly ColorSpace RGBAlpha32BPP = new ColorSpace
		{
			RedPosition = 0,
			GreenPosition = 1,
			BluePosition = 2,
			AlphaPosition = 3,
			BytePositions = true
		};
	}
}
namespace Balder.Core
{
	public struct ColorSpace
	{
		public int RedPosition;
		public int GreenPosition;
		public int BluePosition;
		public int AlphaPosition;


		/// <summary>
		/// Gets or sets wether or not the position of the components are in bytes 
		/// or in bits within the depth of the colorspace.
		/// 
		/// The depth would typically be a dword/long word (32 bits) for 32 bits per pixel or a word for 15/16 bits per
		/// pixel
		/// </summary>
		public bool BytePositions;
	}
}
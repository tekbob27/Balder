namespace Balder.Core.Imaging
{
	public class ImageFormat
	{
		public PixelFormat PixelFormat { get; set; }
		public int Depth { get; set; }

		public override int GetHashCode()
		{
			return PixelFormat.GetHashCode() << 16 | Depth;
		}


		public override bool Equals(object obj)
		{
			var otherFormat = obj as ImageFormat;
			if( null == otherFormat )
			{
				return false;
			}
			if( PixelFormat == otherFormat.PixelFormat && 
				Depth == otherFormat.Depth )
			{
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			return string.Format("Depth: {0}, PixelFormat: {1}", Depth, PixelFormat);
		}
	}
}

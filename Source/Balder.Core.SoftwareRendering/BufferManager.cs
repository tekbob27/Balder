namespace Balder.Core.SoftwareRendering
{
	public class BufferManager 
	{
		public static readonly BufferManager Instance = new BufferManager();

		private BufferManager()
		{
		}

		public IBuffers Create<T>(int width, int height)
			where T:IFrameBuffer, new()
		{
			var buffers = new Buffers<T>(width,height);
			return buffers;
		}

		public IBuffers Current { get; set; } 
	}
}

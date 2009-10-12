namespace Balder.Core.Geometries
{
	public struct TextureCoordinate
	{
		public TextureCoordinate(float u, float v)
			: this()
		{
			U = u;
			V = v;
		}


		public float U { get; set; }
		public float V { get; set; }
	}
}
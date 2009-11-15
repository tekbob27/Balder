using Balder.Core.Imaging;

namespace Balder.Core.Materials
{
	public class Material
	{
		public Color Ambient;
		public Color Diffuse;
		public Color Specular;
		public float Shine;
		public float ShineStrength;

		public Image DiffuseMap;
	}
}

#if(SILVERLIGHT)
using System.Windows.Media;
#else
using Color=System.Drawing.Color;
#endif
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

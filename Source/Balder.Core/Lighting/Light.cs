using Balder.Core.Display;
using Balder.Core.Math;

namespace Balder.Core.Lighting
{
	public abstract class Light : EnvironmentalNode
	{
		public Color Diffuse;
        public Color Specular;
        public Color Ambient;

		public abstract Color Calculate(Viewport viewport, Vector point, Vector normal);
	}
}

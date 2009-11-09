#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Display;
using Balder.Core.Math;

namespace Balder.Core.Lighting
{
	public abstract class Light : EnvironmentalNode
	{
		public Color ColorDiffuse;
        public Color ColorSpecular;
        public Color ColorAmbient;

		public abstract Color Calculate(Viewport viewport, Vector point, Vector normal);
	}
}

#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Interfaces;
using Balder.Core.Math;
using Balder.Core.Extensions;

namespace Balder.Core.Lighting
{
	public abstract class Light : EnvironmentalNode
	{
		public Color ColorDiffuse;
        public Color ColorSpecular;
        public Color ColorAmbient;

		public abstract Color Calculate(IViewport viewport, Vector point, Vector normal);
	}
}

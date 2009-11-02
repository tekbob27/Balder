#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

namespace Balder.Core.Objects.Geometries
{
	public struct Line
	{
		public int A;
		public int B;
		public Color Color;

		public Line(int a, int b)
			: this()
		{
			A = a;
			B = b;
			Color = Colors.White;
		}
	}
}

#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

namespace Balder.Core.Rendering
{
	public interface IShapeContext
	{
		void DrawLine(int xstart, int ystart, int xend, int yend, Color color);
		void DrawLine(int xstart, int ystart, float zstart, int xend, int yend, float zend, Color color);
	}
}

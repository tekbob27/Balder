using System;
using System.Windows.Media;
using Balder.Core.Rendering;

namespace Balder.Core.SoftwareRendering.Rendering
{
	public class ShapeContext : IShapeContext
	{
		public void DrawLine(int xstart, int ystart, int xend, int yend, Color color)
		{
			throw new NotImplementedException();
		}

		public void DrawLine(int xstart, int ystart, float zstart, int xend, int yend, float zend, Color color)
		{
			throw new NotImplementedException();
		}
	}
}

using Balder.Core.Display;
using Balder.Core.Math;
using Balder.Core.Objects.Geometries;
using Ninject.Core;

namespace Balder.Core.Debug
{
	public class DebugShape : RenderableNode
	{
		[Inject]
		public IGeometryContext GeometryContext { get; set; }


		public virtual void Initialize()
		{
			
		}

		public override void Render(Viewport viewport, Matrix view, Matrix projection)
		{
			GeometryContext.Render(viewport, view, projection, World);
		}
	}
}

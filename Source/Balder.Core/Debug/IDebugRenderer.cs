using Balder.Core.Display;
using Balder.Core.Math;

namespace Balder.Core.Debug
{
	public interface IDebugRenderer
	{
		void RenderBoundingSphere(BoundingSphere sphere, Viewport viewport, Matrix view, Matrix projection, Matrix world);
	}
}

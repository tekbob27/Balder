using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Core.Debug
{
	public interface IDebugRenderer
	{
		void RenderBoundingSphere(BoundingSphere sphere, IViewport viewport, Matrix view, Matrix projection, Matrix world);
	}
}

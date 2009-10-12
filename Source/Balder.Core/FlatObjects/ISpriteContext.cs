using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Core.FlatObjects
{
	public interface ISpriteContext
	{
		void Render(IViewport viewport, Sprite sprite, Matrix view, Matrix projection, Matrix world, float xScale, float yScale, float rotation);
	}
}

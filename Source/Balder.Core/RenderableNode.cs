using Balder.Core.Display;
using Balder.Core.Math;

namespace Balder.Core
{
	public abstract class RenderableNode : Node
	{
		public void PrepareRender()
		{
			PositionMatrix[3, 0] = Position.X;
			PositionMatrix[3, 1] = Position.Y;
			PositionMatrix[3, 2] = Position.Z;
		}

		public virtual void BeforeRender() {}
		public virtual void AfterRender() {}

		/// <summary>
		/// Color of the node - this will be used if node supports it
		/// during lighting calculations. If Node has different ways of defining
		/// its color, for instance Materialing or similar - this color
		/// will most likely be overridden
		/// </summary>
		public Color Color { get; set; }


		#region Public Abstract Methods

		public abstract void Render(Viewport viewport, Matrix view, Matrix projection);

		public virtual void PostRender(Viewport viewport, Matrix renderMatrix, Matrix projectionMatrix)
		{
		}

		#endregion
	}
}

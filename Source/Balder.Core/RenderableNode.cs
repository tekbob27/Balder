using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Core
{
	public abstract class RenderableNode : Node
	{
		public void PrepareRender()
		{
			/*
			World[3, 0] = Position.X;
			World[3, 1] = Position.Y;
			World[3, 2] = Position.Z;
			 * */
		}


		public virtual void BeforeRender() {}
		public virtual void AfterRender() {}

		#region Public Abstract Methods

		public abstract void Render(IViewport viewport, Matrix view, Matrix projection);

		public virtual void PostRender(IViewport viewport, Matrix renderMatrix, Matrix projectionMatrix)
		{
		}

		#endregion
	}
}

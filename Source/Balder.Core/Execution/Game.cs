using Balder.Core.Display;
using Balder.Core.Math;

namespace Balder.Core.Execution
{
	public class Game : Actor
	{
		protected Game()
		{
		}

		public Scene Scene { get; private set; }
		public Viewport Viewport { get; private set; }
		public Camera Camera { get; private set; }

		public override void BeforeInitialize()
		{
			Scene = new Scene();
			Viewport = new Viewport { Scene = Scene };
			Viewport.Width = 800;
			Viewport.Height = 600;
			Camera = new Camera(Viewport);
			Camera.Target = Vector.Forward;
			Camera.Position = Vector.Zero;

			// Todo: bi-directional referencing..  Not a good idea!
			Viewport.Camera = Camera;

			base.BeforeInitialize();
		}

		public override void BeforeUpdate()
		{
			/*
			if( null != MouseManager)
			{
				MouseManager.HandleButtonSignals(Mouse);
				MouseManager.HandlePosition(Mouse);
			}*/
		}

		public virtual void OnRender()
		{
			Camera.Update();
			Scene.Render(Viewport, Camera.ViewMatrix, Camera.ProjectionMatrix);
		}
	}
}
using Balder.Core.Interfaces;

namespace Balder.Core.Execution
{
	public class Game : Actor
	{
		protected Game()
		{
		}

		private void InitializeVariables()
		{
			Scene = new Scene();
		}
		

		/*
		private void SetupDefaults()
		{
			Scene = new Scene();
			if( Display.IsInitialized )
			{
				Display_Initialized(null,null);
			} else
			{
				Display.Initialized += Display_Initialized;	
			}
			
			

			
		}

		void Display_Initialized(object sender, EventArgs e)
		{
			Viewport = Display.CreateViewport();
			Camera = new Camera();
			Camera.Prepare(Viewport);

			Viewport.Camera = Camera;
			Viewport.Scene = Scene;
		}
		 * */


		public Scene Scene { get; private set; }
		public IViewport Viewport { get; private set; }
		public Camera Camera { get; private set; }



		#region Runtime

		public override void Loaded()
		{
			if (null != Viewport)
			{
				Viewport.Prepare();
			}
			//Display.Draw += (s, e) => OnDraw();
			//Display.Render += (s, e) => OnRender();
			//Display.Update += (s, e) => OnUpdate();
			base.Loaded();
		}

		private void OnRender()
		{
			Camera.Update();

			Viewport.BeforeRender();
			Scene.Render(Viewport, Camera.ViewMatrix, Camera.ProjectionMatrix);
			Viewport.AfterRender();
		}

		private void OnUpdate()
		{
			/*
			BeforeUpdate();
			Scene.HandleMouseEvents(Viewport, Mouse);
			ExecuteActionOnActors(a => a.BeforeUpdate());
			ExecuteActionOnActors(a => a.Update());
			Updated(this);
			ExecuteActionOnActors(a => a.AfterUpdate());
			AfterUpdate();
			 * */
		}
		
		#endregion

	}
}
using System;
using System.Collections.Generic;

#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

using Balder.Core.Collections;
using Balder.Core.Interfaces;
using Balder.Core.Runtime;

namespace Balder.Core
{
	public delegate void GameEvent(Game game);

	public class Game : Actor
	{
		public event GameEvent Updated = (g) => { };

		private ActorCollection Actors { get; set; }

		protected Game()
		{
		}


		private void SetupDefaults()
		{
			Scene = new Scene();
			Display.Initialized += Display_Initialized;
			Display.BackgroundColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);

			Actors = new ActorCollection();
		}

		void Display_Initialized(object sender, EventArgs e)
		{
			Viewport = Display.CreateViewport();
			Camera = new Camera();
			Camera.Prepare(Viewport);

			Viewport.Camera = Camera;
			Viewport.Scene = Scene;
		}

		public Scene Scene { get; private set; }
		public IViewport Viewport { get; private set; }
		public Camera Camera { get; private set; }


		protected void AddActor(Actor actor)
		{
			Actors.Add(actor);
		}

		#region Runtime

		internal void OnInitialize()
		{
			SetupDefaults();
			ExecuteActionOnActors(a => a.Initialize());
		}

		internal void OnLoadContent()
		{
			ExecuteActionOnActors(a => a.LoadContent());
		}

		internal void OnLoaded()
		{
			ExecuteActionOnActors(a => a.Loaded());
			if( null != Viewport )
			{
				Viewport.Prepare();
			}
			Display.Draw += (s, e) => OnDraw();
			Display.Render += (s, e) => OnRender();
			Display.Update += (s, e) => OnUpdate();
		}

		private void OnDraw()
		{
		}

		private void OnRender()
		{
			Camera.Update();

			Viewport.BeforeRender();
			Scene.Render(Viewport, Camera.ViewMatrix, Camera.ProjectionMatrix);
			Viewport.AfterRender();

			ExecuteActionOnActors(a => a.Draw());
		}

		private void OnUpdate()
		{
			ExecuteActionOnActors(a => a.Update());
			Updated(this);
		}

		private Actor[] GetActors()
		{
			var actors = new List<Actor> {this};
			actors.AddRange(Actors);
			return actors.ToArray();
		}

		private void ExecuteActionOnActors(Action<Actor> action)
		{
			foreach( var actor in GetActors() )
			{
				action(actor);
			}
		}

		protected void Stop()
		{
			foreach (var actor in GetActors())
			{
				actor.Stopped();
			}
		}
		
		#endregion

	}
}

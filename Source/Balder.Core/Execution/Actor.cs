using System;
using Balder.Core.Collections;
using Balder.Core.Content;
using Balder.Core.Display;
using Balder.Core.Input;
using Ninject.Core;

namespace Balder.Core.Execution
{
	public class Actor : IActor
	{
		protected Actor()
		{
			Actors = new ActorCollection();
		}

		public ActorCollection Actors { get; private set; }
		public bool HasInitialized { get; private set; }
		public bool HasLoaded { get; private set; }
		public bool HasUpdated { get; private set; }

		protected void AddActor(Actor actor)
		{
			Actors.Add(actor);
		}
		

		public virtual void Initialize() { }
		public virtual void LoadContent() { }
		public virtual void Loaded() { }
		public virtual void Update() { }
		public virtual void Stopped() { }

		public virtual void BeforeUpdate()
		{
			/*
			if( null != MouseManager)
			{
				MouseManager.HandleButtonSignals(Mouse);
				MouseManager.HandlePosition(Mouse);
			}*/
		}

		public virtual void AfterUpdate() { }


		private void ExecuteActionOnActors(Action<Actor> action)
		{
			foreach (var actor in Actors)
			{
				action(actor);
			}
		}

		public void Stop()
		{
			foreach (var actor in Actors)
			{
				actor.Stopped();
			}
		}


		internal void OnInitialize()
		{
			Initialize();
			ExecuteActionOnActors(a => a.Initialize());
			HasInitialized = true;
		}

		internal void OnLoadContent()
		{
			LoadContent();
			ExecuteActionOnActors(a => a.LoadContent());
			HasLoaded = true;
		}

		internal void OnUpdate()
		{
			Update();
			HasUpdated = true;
		}

		

		#region Services

		/*
		[Inject]
		public Mouse Mouse { get; set; }

		[Inject]
		public IMouseManager MouseManager { get; set; }

		[Inject]
		public IDisplay Display { get; set; }

		[Inject]
		public ITargetDevice TargetDevice { get; set; }

		[Inject]
		public IContentManager ContentManager { get; set; }
		 * */

		#endregion
	}
}
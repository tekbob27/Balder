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
		
		public virtual void BeforeInitialize() { }
		public virtual void Initialize() { }

		public virtual void LoadContent() { }
		public virtual void Loaded() { }
		public virtual void Stopped() { }

		public virtual void BeforeUpdate() { }
		public virtual void Update() { }
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
			BeforeInitialize();
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
			ExecuteActionOnActors(a => a.BeforeUpdate());
			ExecuteActionOnActors(a => a.Update());
			BeforeUpdate();
			Update();
			AfterUpdate();
			ExecuteActionOnActors(a => a.AfterUpdate());
			HasUpdated = true;
		}

		

		#region Services
		[Inject]
		public IContentManager ContentManager { get; set; }

		/*
		[Inject]
		public Mouse Mouse { get; set; }

		[Inject]
		public IMouseManager MouseManager { get; set; }

		[Inject]
		public IDisplay Display { get; set; }

		[Inject]
		public ITargetDevice TargetDevice { get; set; }

		 * */

		#endregion
	}
}
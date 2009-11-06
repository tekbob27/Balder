using Balder.Core.Content;
using Balder.Core.Display;
using Balder.Core.Input;
using Balder.Core.Services;
using Ninject.Core;

namespace Balder.Core.Runtime
{
	public class Actor : IActor
	{
		public virtual void Initialize() { }
		public virtual void LoadContent() { }
		public virtual void Loaded() { }
		public virtual void BeforeDraw() { }
		public virtual void Draw() { }
		public virtual void AfterDraw() { }
		public virtual void Update() { }
		public virtual void Stopped() { }

		public virtual void BeforeUpdate()
		{
			if( null != MouseManager)
			{
				MouseManager.HandleButtonSignals(Mouse);
				MouseManager.HandlePosition(Mouse);
			}
		}

		public virtual void AfterUpdate() { }

		#region Services
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
		#endregion
	}
}

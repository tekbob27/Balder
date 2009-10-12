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

		#region Services
		[Inject]
		public IDisplay Display { get; set; }

		[Inject]
		public ITargetDevice TargetDevice { get; set; }

		[Inject]
		public IContentManager ContentManager { get; set; }
		#endregion
	}
}

using Balder.Core;
using Balder.Core.Debug;
using Balder.Core.Execution;
using Balder.Core.Objects.Geometries;

namespace Balder.Silverlight.TestApp
{
	public class MyGame : Game
	{
		public override void Initialize()
		{
			Runtime.Instance.DebugLevel |= DebugLevel.BoundingSpheres;
			Camera.Position.Z = -30;
		}

		public override void LoadContent()
		{
			var audi = ContentManager.Load<Mesh>("audi.ASE");
			audi.Click += audiClick;
			Scene.AddNode(audi);
		}

		private void audiClick(object sender, System.EventArgs e)
		{
			int i = 0;
			i++;
		}

		public override void Update()
		{
		}
	}
}

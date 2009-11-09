using Balder.Core.Execution;
using Balder.Core.Objects.Geometries;

namespace Balder.Silverlight.TestApp
{
	public class MyGame : Game
	{
		public override void Initialize()
		{
		}

		public override void LoadContent()
		{
			var teapot = ContentManager.Load<Mesh>("teapot.ASE");
			Scene.AddNode(teapot);
		}

		public override void Update()
		{
		}
	}
}

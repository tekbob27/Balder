using Balder.Core.Execution;
using Balder.Core.Objects.Geometries;

namespace Balder.Silverlight.TestApp
{
	public class MyGame : Game
	{
		public override void Initialize()
		{
			Camera.Position.Z = -100;
		}

		public override void LoadContent()
		{
			var teapot = ContentManager.Load<Mesh>("teapot.ASE");
			teapot.Click += new System.EventHandler(teapot_Click);
			Scene.AddNode(teapot);
		}

		void teapot_Click(object sender, System.EventArgs e)
		{
			int i = 0;
			i++;
		}

		public override void Update()
		{
		}
	}
}

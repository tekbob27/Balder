using System;
using System.Windows.Media;
using Balder.Core;
using Balder.Core.Imaging;
using Balder.Core.Lighting;
using Balder.Core.Math;
using Balder.Core.Objects.Flat;
using Balder.Core.Objects.Geometries;
using Balder.Core.Runtime;
using Matrix=Balder.Core.Math.Matrix;

namespace Balder.Silverlight.TestApp
{
	public class MyGame : Game
	{
        private OmniLight light;
		private Sprite _lightSprite;

		public override void Initialize()
		{
			//EngineRuntime.Instance.DebugLevel |= DebugLevel.BoundingSpheres;
		}

		public override void LoadContent()
		{
			Mesh mesh;

			var image = ContentManager.Load<Image>("Bricks.png");

			mesh = ContentManager.Load<Mesh>("CoordinateSystem.ASE");
			mesh.Name = "Box1";
			Scene.AddNode(mesh);
			mesh = ContentManager.Load<Mesh>("box.ASE");
			//mesh.World = Matrix.CreateTranslation(new Vector(300, 50, 0));
			mesh.Position = new Vector(300, 50, 0);
			mesh.Name = "Box2";
			Scene.AddNode(mesh);
			
			mesh = ContentManager.Load<Mesh>("box.ASE");
			mesh.World = Matrix.CreateTranslation(new Vector(-300, 0, 0));
			mesh.Name = "Box3";
			 
			Scene.AddNode(mesh);

			Display.BackgroundColor = Color.FromArgb(0xff, 0, 0, 0);

			light = new OmniLight();
            light.Range = 2.0f;
			light.Position.X = 0;
			light.Position.Y = 0;
			light.Position.Z = -30;
            light.ColorDiffuse = Color.FromArgb(0xff, 255, 121, 32);
            light.ColorSpecular = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            light.ColorAmbient = Color.FromArgb(0xff, 0x7f, 0x3f, 0x10);
			Scene.AddNode(light);
		}

		private float zPos = -1050;
		private double pos = 0;

		public override void Update()
		{
			Camera.Position.X = (float)Math.Sin(pos) * 1050; //  = new Vector(0, 0, zPos);
			Camera.Position.Z = (float)Math.Cos(pos) * 1050;

			pos += 0.005;

			//zPos -= 0.5f;
			
			Camera.Target.X = 0f;
			Camera.Target.Y = 0f;
			Camera.Target.Z = 0f;
		}
	}
}

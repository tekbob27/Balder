using System;
using System.Windows.Media;
using Balder.Core;
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
			//EngineRuntime.Instance.DebugLevel |= DebugLevel.FaceNormals;
		}

		public override void LoadContent()
		{
			var ring = new AnnularRing(ContentManager, 5, 5, 8);
			//Scene.AddNode(ring);
			

			var mesh = ContentManager.Load<Mesh>("audi.ASE");
			//mesh.Position.X = -30;
			mesh.World = Matrix.CreateScale(new Vector(10f, 10f, 10f));
			Scene.AddNode(mesh);

			_lightSprite = ContentManager.Load<Sprite>("sun.png");
			//Scene.AddNode(_lightSprite);


			var sprite = ContentManager.Load<Sprite>("recycle.png");
			sprite.Position = new Vector(-10,0,0);
			//Scene.AddNode(sprite);

			Display.BackgroundColor = Color.FromArgb(0xff, 0, 0, 0);

			var cylinder = ContentManager.Creator.CreateCylinder(10f, 20f, 8, 1);

			light = new OmniLight();
            light.Range = 3.0f;
			light.Position.X = 0;
			light.Position.Y = 0;
			light.Position.Z = -30;
            light.ColorDiffuse = Color.FromArgb(0xff, 0x1f, 0x1f, 0x6f);
            light.ColorSpecular = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            light.ColorAmbient = Color.FromArgb(0xff, 0, 0x5f, 0);
			Scene.AddNode(light);
		}

		private double sinPos;
		private double cameraSin;

		public override void Update()
		{
			Camera.Position.X = (float)Math.Sin(cameraSin) * 130f;
			Camera.Position.Y = -60; // ((float)Math.Sin(cameraSin) * 15f) - 20f;
			Camera.Position.Z = (float)Math.Cos(cameraSin) * 130f;

			light.Position.X = (float)Math.Cos(sinPos) * 20f;
			light.Position.Y = (float) (((Math.Sin(sinPos) + Math.Cos(sinPos))/2)*20f);
			light.Position.Z = (float)Math.Sin(sinPos) * 20f;
			
			_lightSprite.Position = light.Position;


			Camera.Target.X = 0;
			Camera.Target.Y = -5f;
			Camera.Target.Z = 0;

			sinPos -= 0.11;
			cameraSin += 0.05;
		}
	}
}

using System;
using System.Windows.Media;
using Balder.Core;
using Balder.Core.Lighting;
using Balder.Core.Math;
using Balder.Core.Objects.Flat;
using Balder.Core.Objects.Geometries;
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
			var mesh = ContentManager.Load<Mesh>("pumpkin.ASE");
			//mesh.World = Matrix.CreateScale(new Vector(10f, 10f, 10f));
			Scene.AddNode(mesh);

			_lightSprite = ContentManager.Load<Sprite>("sun.png");
			//Scene.AddNode(_lightSprite);

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

		private double sinPos;
		private double cameraSin;

		public override void Update()
		{
			Camera.Position.X = (float)Math.Sin(cameraSin) * 430f;
			Camera.Position.Y = -60; // ((float)Math.Sin(cameraSin) * 15f) - 20f;
			Camera.Position.Z = (float)Math.Cos(cameraSin) * 430f;

			light.Position.X = (float)Math.Cos(sinPos) * 20f;
			light.Position.Y = (float) (((Math.Sin(sinPos) + Math.Cos(sinPos))/2)*20f);
			light.Position.Z = (float)Math.Sin(sinPos) * 20f;
			
			_lightSprite.Position = light.Position;

			Camera.Target.X = 0;
			Camera.Target.Y = -5f;
			Camera.Target.Z = 0;

			sinPos -= 0.1;
			cameraSin += 0.05;
		}
	}
}

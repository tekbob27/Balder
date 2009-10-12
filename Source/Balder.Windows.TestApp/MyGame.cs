using System;
using System.Drawing;
using Balder.Core;
using Balder.Core.FlatObjects;
using Balder.Core.Geometries;
using Balder.Core.Lighting;
using Balder.Core.Math;

namespace Balder.Windows.TestApp
{
	public class MyGame : Game
	{
        private OmniLight light;

		public override void Initialize()
		{
			//EngineRuntime.Instance.DebugLevel |= DebugLevel.FaceNormals;
		}

		public override void LoadContent()
		{
			var mesh = ContentManager.Load<Mesh>("teapot.ase");
			Scene.AddNode(mesh);

			var cylinder = ContentManager.CreateCylinder(10f, 20f, 8, 1);

			light = new OmniLight();
            light.Range = 2.0f;
			light.Position.X = 0;
			light.Position.Y = 0;
			light.Position.Z = -30;
            light.ColorDiffuse = Color.FromArgb(0xff, 0x7f, 0x7f, 0x1f);
            light.ColorSpecular = Color.FromArgb(0xff, 0x7f, 0x7f, 0x1f);
            light.ColorAmbient = Color.FromArgb(0x1f, 0x1f, 0x1f, 0x1f);

			this.Scene.AddNode(light);
		}

		private double sinPos;
		private double cameraSin;

		public override void Update()
		{
			this.Camera.Position.X = (float)Math.Sin(cameraSin) * 50f;
			this.Camera.Position.Y = -15f;
			this.Camera.Position.Z = (float)Math.Cos(cameraSin) * 50f;

			light.Position.X = (float)Math.Sin(sinPos) * 30f;
			light.Position.Y = -5;
			light.Position.Z = (float)Math.Cos(sinPos) * 30f;


			this.Camera.Target.X = 0;
			this.Camera.Target.Y = -5f;
			this.Camera.Target.Z = 0;

			sinPos += 0.05;
			cameraSin += 0.005;
		}
	}
}

using System.Windows.Media;
using Balder.Core;
using Balder.Core.Geometries;
using Balder.Core.Math;
using Matrix=Balder.Core.Math.Matrix;

namespace GettingStarted
{
	public class MyGame : Game
	{
		private Mesh _audi;
		private float _audiRotation;

		public override void Initialize()
		{
			// We set the background color for the display to black
			Display.BackgroundColor = Colors.Black;
			base.Initialize();
		}

		public override void LoadContent()
		{
			// Load the audi asset found in the assets folder.
			// One does not need to add the assets folder in the path, as this is implicit
			// All assets must be of type resource in the properties window in Visual Studio
			_audi = ContentManager.Load<Mesh>("audi.ASE");
			Scene.AddNode(_audi);

			// Set the camera to point at the Zero location in 3D space and its position to be a
			// bit from the car itself
			Camera.Target = Vector.Zero;
			Camera.Position = new Vector(0,-2,-10);

			base.LoadContent();
		}

		// The update method is called every frame
		public override void Update()
		{
			_audi.World = Matrix.CreateRotationY(_audiRotation);
			_audiRotation += 0.5f;

			base.Update();
		}
	}
}

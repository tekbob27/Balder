using System.Windows.Controls;
using Balder.Core.Math;
using Balder.Silverlight.Controls;

namespace Balder.Silverlight.TestApp
{
	public partial class Page : UserControl
	{
		public Page()
		{
			InitializeComponent();
		}

		private float _angle = 0f;

		private void Updated(RenderingContainer renderingContainer)
		{
			//_audi.Node.World = Matrix.CreateRotationY(_angle);
			_angle += 0.5f;
			//_renderingContainer.Camera.Position = new Vector(0,-5,-20);
		}
	}
}

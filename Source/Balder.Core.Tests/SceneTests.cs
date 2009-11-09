using Balder.Core.Display;
using Balder.Core.Math;
using Balder.Core.Objects.Geometries;
using NUnit.Framework;

namespace Balder.Core.Tests
{
	[TestFixture]
	public class SceneTests
	{
		[Test]
		public void GettingObjectAtCenterOfScreenWithSingleObjectAtCenterOfSceneShouldReturnTheObject()
		{
			var viewport = new Viewport {Width = 640, Height = 480};
			var scene = new Scene();
			var camera = new Camera(viewport) {Position = {Z = -10}};
			camera.Update();

			var node = new Geometry
			           	{
			           		BoundingSphere = new BoundingSphere(Vector.Zero, 10f)
			           	};
			scene.AddNode(node);

			var nodeAtCoordinate = scene.GetNodeAtScreenCoordinate(viewport, viewport.Width / 2, viewport.Height / 2);
			Assert.That(nodeAtCoordinate, Is.Not.Null);
			Assert.That(nodeAtCoordinate, Is.EqualTo(node));
		}

		[Test]
		public void GettingObjectAtTopLeftOfScreenWithSingleObjectAtCenterOfSceneShouldReturnTheObject()
		{
			var viewport = new Viewport { Width = 640, Height = 480 };
			var scene = new Scene();
			var camera = new Camera(viewport);

			camera.Position.Z = -100;

			camera.Update();

			var node = new Geometry
			           	{
			           		BoundingSphere = new BoundingSphere(Vector.Zero, 10f)
			           	};
			scene.AddNode(node);

			var nodeAtCoordinate = scene.GetNodeAtScreenCoordinate(viewport, 0, 0);
			Assert.That(nodeAtCoordinate, Is.Null);
		}

	}
}

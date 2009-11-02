using Balder.Core.Interfaces;
using Balder.Core.Math;
using Balder.Core.Objects.Geometries;
using Moq;
using NUnit.Framework;

namespace Balder.Core.Tests
{
	[TestFixture]
	public class SceneTests
	{
		[Test]
		public void GettingObjectAtCenterOfScreenWithSingleObjectAtCenterOfSceneShouldReturnTheObject()
		{
			var viewportMock = new Mock<IViewport>();
			viewportMock.ExpectGet(v => v.Width).Returns(640);
			viewportMock.ExpectGet(v => v.Height).Returns(480);
			var scene = new Scene();
			var camera = new Camera();
			camera.Prepare(viewportMock.Object);
			viewportMock.ExpectGet(v => v.Camera).Returns(camera);

			camera.Position.Z = -10;

			camera.Update();

			var node = new Geometry();
			node.BoundingSphere = new BoundingSphere(Vector.Zero, 10f);
			scene.AddNode(node);

			var nodeAtCoordinate = scene.GetNodeAtScreenCoordinate(viewportMock.Object, viewportMock.Object.Width / 2, viewportMock.Object.Height / 2);
			Assert.That(nodeAtCoordinate, Is.Not.Null);
			Assert.That(nodeAtCoordinate, Is.EqualTo(node));
		}

		[Test]
		public void GettingObjectAtTopLeftOfScreenWithSingleObjectAtCenterOfSceneShouldReturnTheObject()
		{
			var viewportMock = new Mock<IViewport>();
			viewportMock.ExpectGet(v => v.Width).Returns(640);
			viewportMock.ExpectGet(v => v.Height).Returns(480);
			var scene = new Scene();
			var camera = new Camera();
			camera.Prepare(viewportMock.Object);
			viewportMock.ExpectGet(v => v.Camera).Returns(camera);

			camera.Position.Z = -100;

			camera.Update();

			var node = new Geometry();
			node.BoundingSphere = new BoundingSphere(Vector.Zero, 10f);
			scene.AddNode(node);

			var nodeAtCoordinate = scene.GetNodeAtScreenCoordinate(viewportMock.Object, 0, 0);
			Assert.That(nodeAtCoordinate, Is.Null);
		}

	}
}

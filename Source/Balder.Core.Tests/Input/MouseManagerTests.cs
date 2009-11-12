using System;
using Balder.Core.Input;
using Moq;
using NUnit.Framework;

namespace Balder.Core.Tests.Input
{
	[TestFixture]
	public class MouseManagerTests
	{

		[Test]
		public void LeftMouseButtonPressedShouldResultInEdgeSetForButton()
		{
			var mouse = new Mouse();

			var mouseDeviceMock = new Mock<IMouseDevice>();
			mouseDeviceMock.Expect(m => m.IsButtonPressed(MouseButton.Left)).Returns(true);
			var mouseManager = new MouseManager(mouseDeviceMock.Object);
			mouseManager.HandleButtonSignals(mouse);

			Assert.That(mouse.LeftButton.IsEdge,Is.True);
		}

		[Test]
		public void LeftMouseButtonPressedShouldResultInDownSetForButton()
		{
			var mouse = new Mouse();

			var mouseDeviceMock = new Mock<IMouseDevice>();
			mouseDeviceMock.Expect(m => m.IsButtonPressed(MouseButton.Left)).Returns(true);
			var mouseManager = new MouseManager(mouseDeviceMock.Object);
			mouseManager.HandleButtonSignals(mouse);

			Assert.That(mouse.LeftButton.IsDown, Is.True);
		}

		[Test]
		public void LeftMouseButtonHeldForMultipleHandlesShouldSetEdgeSignalToFalse()
		{
			var mouse = new Mouse();

			var mouseDeviceMock = new Mock<IMouseDevice>();
			mouseDeviceMock.Expect(m => m.IsButtonPressed(MouseButton.Left)).Returns(true);
			var mouseManager = new MouseManager(mouseDeviceMock.Object);
			for (var i = 0; i < 10; i++)
			{
				mouseManager.HandleButtonSignals(mouse);
			}

			Assert.That(mouse.LeftButton.IsEdge, Is.False);
		}

		[Test]
		public void MousePositionsShouldGetUpdatedFromDevice()
		{
			var mouse = new Mouse();

			var random = new Random();
			var expectedXPosition = random.Next();
			var expectedYPosition = random.Next();

			var mouseDeviceMock = new Mock<IMouseDevice>();
			mouseDeviceMock.Expect(m => m.GetXPosition()).Returns(expectedXPosition);
			mouseDeviceMock.Expect(m => m.GetYPosition()).Returns(expectedYPosition);

			var mouseManager = new MouseManager(mouseDeviceMock.Object);
			mouseManager.HandlePosition(mouse);

			Assert.That(mouse.XPosition, Is.EqualTo(expectedXPosition));
			Assert.That(mouse.YPosition, Is.EqualTo(expectedYPosition));
		}
	}
}

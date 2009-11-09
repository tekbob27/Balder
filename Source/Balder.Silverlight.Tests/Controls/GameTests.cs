using Balder.Core;
using Balder.Core.Display;
using Balder.Core.Execution;
using CThru.Silverlight;
using Moq;
using NUnit.Framework;
using Game=Balder.Silverlight.Controls.Game;

namespace Balder.Silverlight.Tests.Controls
{
	[TestFixture]
	public class GameTests
	{
		public class MyGame : Core.Execution.Game { }

		[Test, SilverlightUnitTest]
		public void NotSpecifyingWidthOrHeightShouldThrowException()
		{
			var exceptionThrown = false;
			try
			{
				var gameControl = new Game();
				SilverUnit.FireEvent(gameControl, "Loaded", null, null);
			} catch
			{
				exceptionThrown = true;
			}
			Assert.That(exceptionThrown,Is.True);
		}

		[Test, SilverlightUnitTest]
		public void	SpecifyingGameClassShouldRegisterGame()
		{
			var game = new MyGame();
			var runtimeMock = new Mock<IRuntime>();
			var displayMock = new Mock<IDisplay>();
			var displayDeviceMock = new Mock<IDisplayDevice>();
			displayDeviceMock.Expect(d => d.CreateDisplay()).Returns(displayMock.Object);
			var platformMock = new Mock<IPlatform>();
			platformMock.Expect(p => p.DisplayDevice).Returns(displayDeviceMock.Object);
			runtimeMock.Expect(r => r.RegisterGame(It.IsAny<IDisplay>(),game));
			var gameControl = new Game {GameClass = game, Runtime = runtimeMock.Object, Platform = platformMock.Object, Width=640, Height=480};
			SilverUnit.FireEvent(gameControl, "Loaded", null, null);
			runtimeMock.VerifyAll();
		}

	}
}

using System;
using System.Linq.Expressions;
using Balder.Core.Runtime;
using Moq;
using NUnit.Framework;

namespace Balder.Core.Tests.Runtime
{
	[TestFixture]
	public class RuntimeTests
	{

		private void EventShouldBeCalledForState(Expression<Action<Game>> eventExpression, PlatformState state)
		{
			var eventCalled = false;
			var stateChanged = false;
			var platform = new FakePlatform();
			var objectFactoryMock = new Mock<IObjectFactory>();

			platform.StateChanged +=
				(p, s) =>
				{
					if (s == state)
					{
						stateChanged = true;
					}
				};


			var gameMock = new Mock<Game>();
			gameMock.Expect(eventExpression).Callback(
				() =>
				{
					Assert.That(stateChanged, Is.True);
					eventCalled = true;
				});


			var runtime = new Core.Runtime.Runtime(platform, objectFactoryMock.Object);
			runtime.RegisterGame(gameMock.Object);

			Assert.That(eventCalled, Is.True);
			
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsInitializeCalledBeforeInitializeStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForState(g => g.Initialize(),PlatformState.Initialize);
		}

		[Test]
		public void GameRegisteredAfterInitializeStateChangeOccuredOnPlatformShouldHaveItsInitializeEventCalledDirectly()
		{
			Assert.Fail();
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsLoadCalledBeforeLoadStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForState(g => g.LoadContent(), PlatformState.Load);
		}

		[Test]
		public void GameRegisteredAfterLoadStateChangeOccuredOnPlatformShouldHaveItsLoadEventCalledDirectly()
		{
			Assert.Fail();
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsUpdateCalledBeforeRunStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForState(g => g.Update(), PlatformState.Run);
		}

		[Test]
		public void ActorsWithinGameShouldHaveItsInitializeCalledAfterGamesInitializeIsCalled()
		{
			Assert.Fail();
		}

		[Test]
		public void ActorsRegisteredInGameAfterGameHasStartedRunningShouldHaveItsInitializeCalled()
		{
			Assert.Fail();
		}

		[Test]
		public void ActorsRegisteredInGameAfterGameHasStartedRunningShouldHaveItsLoadCalled()
		{
			Assert.Fail();
		}
	}
}

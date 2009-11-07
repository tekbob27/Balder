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

		private void EventShouldBeCalledForStateDuringRegistration(Expression<Action<Game>> eventExpression, PlatformState state, bool changeStateFirst)
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

			if( changeStateFirst )
			{
				platform.ChangeState(state);
			}

			var runtime = new Core.Runtime.Runtime(platform, objectFactoryMock.Object);
			runtime.RegisterGame(gameMock.Object);

			if( !changeStateFirst )
			{
				platform.ChangeState(state);
			}

			Assert.That(eventCalled, Is.True);
			
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsInitializeCalledAfterInitializeStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.Initialize(),PlatformState.Initialize, false);
		}

		[Test]
		public void GameRegisteredAfterInitializeStateChangeOccuredOnPlatformShouldHaveItsInitializeEventCalledDirectly()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.Initialize(), PlatformState.Initialize, true);
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsLoadCalledAfterLoadStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.LoadContent(), PlatformState.Load, false);
		}

		[Test]
		public void GameRegisteredAfterLoadStateChangeOccuredOnPlatformShouldHaveItsLoadEventCalledDirectly()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.LoadContent(), PlatformState.Load, true);
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsUpdateCalledAfterRunStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.Update(), PlatformState.Run, false);
		}

		[Test]
		public void ActorsWithinGameShouldHaveItsInitializeCalledAfterGamesInitializeIsCalled()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.LoadContent(), PlatformState.Load, true);
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

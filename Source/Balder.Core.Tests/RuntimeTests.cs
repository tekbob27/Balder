using System;
using System.Linq.Expressions;
using Balder.Core.Assets;
using Balder.Core.Display;
using Balder.Core.Tests.Fakes;
using Moq;
using NUnit.Framework;
using Balder.Core.Execution;

namespace Balder.Core.Tests
{
	[TestFixture]
	public class RuntimeTests
	{

		private static void EventShouldBeCalledForStateDuringRegistration(Expression<Action<Game>> eventExpression, PlatformState state, bool changeStateFirst)
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

			var assetLoaderServiceMock = new Mock<IAssetLoaderService>();
			var runtime = new Core.Runtime(platform, objectFactoryMock.Object,assetLoaderServiceMock.Object);

			if( changeStateFirst )
			{
				platform.ChangeState(state);
			}

			var displayMock = new Mock<IDisplay>();
			runtime.RegisterGame(displayMock.Object, gameMock.Object);

			if( !changeStateFirst )
			{
				platform.ChangeState(state);
			}

			Assert.That(eventCalled, Is.True);
		}

		[Test]
		public void RegisteredGameShouldHaveItsInitializeCalledAfterInitializeStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.Initialize(),PlatformState.Initialize, false);
		}

		[Test]
		public void GameRegisteredAfterInitializeStateChangeOccuredOnPlatformShouldHaveItsInitializeEventCalledDirectly()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.Initialize(), PlatformState.Initialize, true);
		}

		[Test]
		public void RegisteredGameShouldHaveItsLoadCalledAfterLoadStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.LoadContent(), PlatformState.Load, false);
		}

		[Test]
		public void GameRegisteredAfterLoadStateChangeOccuredOnPlatformShouldHaveItsLoadEventCalledDirectly()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.LoadContent(), PlatformState.Load, true);
		}

		[Test]
		public void RegisteredGameShouldHaveItsUpdateCalledAfterRunStateChangeOccursOnPlatform()
		{
			EventShouldBeCalledForStateDuringRegistration(g => g.Update(), PlatformState.Run, false);
		}

		[Test]
		public void OnRenderForGamesShouldNotBeCalledBeforePlatformIsInRunState()
		{
			var platform = new FakePlatform();
			var objectFactoryMock = new Mock<IObjectFactory>();
			var assetLoaderServiceMock = new Mock<IAssetLoaderService>();
			var runtime = new Core.Runtime(platform, objectFactoryMock.Object, assetLoaderServiceMock.Object);
			var displayMock = new Mock<IDisplay>();
			var gameMock = new Mock<Game>();
			var onRenderCalled = false;
			
			gameMock.Expect(g => g.OnRender()).Callback(() => onRenderCalled = true);
			runtime.RegisterGame(displayMock.Object,gameMock.Object);
			((FakeDisplayDevice)platform.DisplayDevice).FireRenderEvent(displayMock.Object);
			Assert.That(onRenderCalled,Is.False);
		}

		[Test]
		public void OnRenderForGamesShouldBeCalledWhenPlatformIsInRunState()
		{
			var platform = new FakePlatform();
			var objectFactoryMock = new Mock<IObjectFactory>();
			var assetLoaderServiceMock = new Mock<IAssetLoaderService>();
			var runtime = new Core.Runtime(platform, objectFactoryMock.Object, assetLoaderServiceMock.Object);
			var displayMock = new Mock<IDisplay>();
			var gameMock = new Mock<Game>();
			var onRenderCalled = false;

			gameMock.Expect(g => g.OnRender()).Callback(() => onRenderCalled = true);
			runtime.RegisterGame(displayMock.Object, gameMock.Object);
			platform.ChangeState(PlatformState.Run);
			((FakeDisplayDevice)platform.DisplayDevice).FireRenderEvent(displayMock.Object);
			Assert.That(onRenderCalled, Is.True);
		}

		[Test]
		public void OnUpdateForGamesShouldNotBeCalledBeforePlatformIsInRunState()
		{
			var platform = new FakePlatform();
			var objectFactoryMock = new Mock<IObjectFactory>();
			var assetLoaderServiceMock = new Mock<IAssetLoaderService>();
			var runtime = new Core.Runtime(platform, objectFactoryMock.Object, assetLoaderServiceMock.Object);
			var displayMock = new Mock<IDisplay>();
			var gameMock = new Mock<Game>();
			var onUpdateCalled = false;

			gameMock.Expect(g => g.Update()).Callback(() => onUpdateCalled = true);
			runtime.RegisterGame(displayMock.Object, gameMock.Object);
			((FakeDisplayDevice)platform.DisplayDevice).FireUpdateEvent(displayMock.Object);
			Assert.That(onUpdateCalled, Is.False);
		}

		[Test]
		public void OnUpdateForGamesShouldBeCalledWhenPlatformIsInRunState()
		{
			var platform = new FakePlatform();
			var objectFactoryMock = new Mock<IObjectFactory>();
			var assetLoaderServiceMock = new Mock<IAssetLoaderService>();
			var runtime = new Core.Runtime(platform, objectFactoryMock.Object, assetLoaderServiceMock.Object);
			var displayMock = new Mock<IDisplay>();
			var gameMock = new Mock<Game>();
			var onUpdateCalled = false;

			gameMock.Expect(g => g.Update()).Callback(() => onUpdateCalled = true);
			runtime.RegisterGame(displayMock.Object, gameMock.Object);
			platform.ChangeState(PlatformState.Run);
			((FakeDisplayDevice)platform.DisplayDevice).FireUpdateEvent(displayMock.Object);
			Assert.That(onUpdateCalled, Is.True);
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
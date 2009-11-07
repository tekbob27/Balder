using NUnit.Framework;

namespace Balder.Core.Tests.Runtime
{
	[TestFixture]
	public class RuntimeTests
	{
		[Test]
		public void RegisteredGameShouldNotHaveItsInitializeCalledBeforeInitializeStateChangeOccursOnPlatform()
		{
			Assert.Fail();
		}

		[Test]
		public void GameRegisteredAfterInitializeStateChangeOccuredOnPlatformShouldHaveItsInitializeEventCalledDirectly()
		{
			Assert.Fail();
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsLoadCalledBeforeLoadStateChangeOccursOnPlatform()
		{
			Assert.Fail();
		}

		[Test]
		public void GameRegisteredAfterLoadStateChangeOccuredOnPlatformShouldHaveItsLoadEventCalledDirectly()
		{
			Assert.Fail();
		}

		[Test]
		public void RegisteredGameShouldNotHaveItsUpdateCalledBeforeRunStateChangeOccursOnPlatform()
		{
			Assert.Fail();
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

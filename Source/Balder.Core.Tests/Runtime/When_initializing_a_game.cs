using Balder.Core.Runtime;
using Balder.Core.Tests.Fakes;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balder.Core.Tests.Runtime
{
	[TestClass]
	public class When_initializing_a_game
	{
		private static MockGame Game;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			var targetDevice = new TargetDevice {GameType = typeof (MockGame)};
			EngineRuntime.Instance.Initialize(targetDevice);

			Game = EngineRuntime.Instance.CurrentGame as MockGame;
			
		}

		[TestMethod]
		public void Initialize_should_be_called()
		{
			Game.IsInitializeCalled.ShouldBeTrue();
		}

		[TestMethod]
		public void LoadContent_should_be_called()
		{
			Game.IsLoadContentCalled.ShouldBeTrue();
		}

		[TestMethod]
		public void Loaded_should_be_called()
		{
			Game.IsLoadedCalled.ShouldBeTrue();
		}
	}
}

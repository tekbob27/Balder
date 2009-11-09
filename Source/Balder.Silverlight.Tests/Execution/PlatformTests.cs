using Balder.Silverlight.Execution;
using NUnit.Framework;

namespace Balder.Silverlight.Tests.Execution
{
	[TestFixture]
	public class PlatformTests
	{

		[Test]
		public void BeforeStateChangeShouldFireBeforeStateChanged()
		{
			var platform = new Platform();
			var stateChangedCalled = false;
			platform.StateChanged += (p, s) => stateChangedCalled = true;
			platform.BeforeStateChange += (p, s) => Assert.That(stateChangedCalled, Is.False);

			Assert.Inconclusive();
		}

	}
}

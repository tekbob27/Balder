using Balder.Core.Input;
using NUnit.Framework;

namespace Balder.Core.Tests.Input
{
	[TestFixture]
	public class MouseButtonStateTests
	{
		[Test]
		public void SignalingEdgeWhenPreviousIsTrueShouldSetPreviousToFalse()
		{
			var mouseButtonState = new MouseButtonState();
			mouseButtonState.IsPreviousEdge = true;
			mouseButtonState.IsEdge = true;
			Assert.That(mouseButtonState.IsPreviousEdge, Is.False);
		}

		[Test]
		public void UnsignalingEdgeWhenPreviousIsFalseShouldSetPreviousToTrue()
		{
			var mouseButtonState = new MouseButtonState();
			mouseButtonState.IsEdge = true;
			mouseButtonState.IsPreviousEdge = false;
			mouseButtonState.IsEdge = false;
			Assert.That(mouseButtonState.IsPreviousEdge, Is.True);
		}
	}
}

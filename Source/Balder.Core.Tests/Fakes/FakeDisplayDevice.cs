using System;
using Balder.Core.Display;
using Moq;

namespace Balder.Core.Tests.Fakes
{
	public class FakeDisplayDevice : IDisplayDevice
	{
		public event DisplayEvent Update = (d) => { };
		public event DisplayEvent Render = (d) => { };



		public IDisplay CreateDisplay()
		{
			var mock = new Mock<IDisplay>();
			return mock.Object;
		}

		public void FireUpdateEvent(IDisplay display)
		{
			Update(display);
		}

		public void FireRenderEvent(IDisplay display)
		{
			Render(display);
		}
	}
}

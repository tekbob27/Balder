using Balder.Core.Display;
using Balder.Core.Input;
using Balder.Core.Runtime;

namespace Balder.Core.Tests.Runtime
{
	public class FakePlatform : IPlatform
	{
		public event PlatformStateChange BeforeStateChange = (p, s) => { };
		public event PlatformStateChange StateChanged = (p, s) => { };

		public IDisplayDevice DisplayDevice { get; set; }
		public IMouseDevice MouseDevice { get; set; }

		public PlatformState CurrentState { get; set; }

		public void ChangeState(PlatformState state)
		{
			BeforeStateChange(this, state);
			CurrentState = state;
			StateChanged(this, state);
		}
	}
}

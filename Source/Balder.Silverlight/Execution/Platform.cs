using Balder.Core.Display;
using Balder.Core.Execution;
using Balder.Core.Input;
using Balder.Silverlight.Display;
using Balder.Silverlight.Input;

namespace Balder.Silverlight.Execution
{
	public class Platform : IPlatform
	{
		public Platform()
		{
			DisplayDevice = new DisplayDevice();
			MouseDevice = new MouseDevice();
			CurrentState = PlatformState.Idle;
		}

		public event PlatformStateChange BeforeStateChange = (p, s) => { };
		public event PlatformStateChange StateChanged  = (p, s) => { };
		public IDisplayDevice DisplayDevice { get; private set; }
		public IMouseDevice MouseDevice { get; private set; }
		public PlatformState CurrentState { get; private set; }
	}
}

using Balder.Core.Display;
using Balder.Core.Input;

namespace Balder.Core.Execution
{
	public enum PlatformState
	{
		Idle=0,
		Initialize,
		Load,
		Run,
		Exit
	}

	public delegate void PlatformStateChange(IPlatform platform, PlatformState state);

	public interface IPlatform
	{
		event PlatformStateChange BeforeStateChange;
		event PlatformStateChange StateChanged;

		IDisplayDevice DisplayDevice { get; }
		IMouseDevice MouseDevice { get; }

		PlatformState CurrentState { get; }
	}
}
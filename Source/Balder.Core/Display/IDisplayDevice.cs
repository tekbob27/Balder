namespace Balder.Core.Display
{
	public delegate void DisplayEvent(IDisplay display);

	public interface IDisplayDevice
	{
		event DisplayEvent Update;
		event DisplayEvent Render;

		IDisplay CreateDisplay();
	}
}

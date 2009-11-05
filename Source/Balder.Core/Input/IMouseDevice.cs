namespace Balder.Core.Input
{
	public enum MouseButton
	{
		Left,
		Middle,
		Right
	}

	public interface IMouseDevice
	{
		bool IsButtonPressed(MouseButton button);
		int GetXPosition();
		int GetYPosition();
	}
}

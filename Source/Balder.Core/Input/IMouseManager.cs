namespace Balder.Core.Input
{
	public interface IMouseManager
	{
		void HandleButtonSignals(Mouse mouse);
		void HandlePosition(Mouse mouse);
	}
}

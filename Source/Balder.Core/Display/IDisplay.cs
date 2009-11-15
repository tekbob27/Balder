namespace Balder.Core.Display
{
	public interface IDisplay
	{
		Color BackgroundColor { get; set; }

		void Initialize(int width, int height);
	}
}
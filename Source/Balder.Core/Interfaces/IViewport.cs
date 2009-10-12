#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

namespace Balder.Core.Interfaces
{
	public interface IViewport
	{
		int XPosition { get; set; }
		int YPosition { get; set; }
		int Width { get; set; }
		int Height { get; set; }

		Scene Scene { get; set; }
		Camera Camera { get; set; }

		void Prepare();

		void BeforeRender();
		void AfterRender();
	}
}
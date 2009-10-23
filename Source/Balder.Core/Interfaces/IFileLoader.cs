using System.IO;
using Balder.Core.Content;

namespace Balder.Core.Interfaces
{
	public interface IFileLoader
	{
		IContentManager ContentManager { get; set; }
		Game Game { get; set; }
		Stream GetStream(string assetName);
	}
}

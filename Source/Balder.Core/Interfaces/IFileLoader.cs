using System.IO;
using Balder.Core.Content;

namespace Balder.Core.Interfaces
{
	public interface IFileLoader
	{
		IContentManager ContentManager { get; set; }
		Stream GetStream(string assetName);
	}
}

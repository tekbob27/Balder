using System.IO;

namespace Balder.Core.Content
{
	public interface IFileLoader
	{
		Stream GetStream(string assetName);
	}
}
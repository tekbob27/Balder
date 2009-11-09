using System.IO;

namespace Balder.Core.Interfaces
{
	public interface IFileLoader
	{
		Stream GetStream(string assetName);
	}
}

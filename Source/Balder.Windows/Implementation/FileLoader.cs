using System;
using System.IO;
using Balder.Core;
using Balder.Core.Interfaces;
using Balder.Core.Services;

namespace Balder.Windows.Implementation
{
	public class FileLoader : IFileLoader
	{
		public IContentManager ContentManager { get; set; }
		public Game Game { get; set; }

		public Stream GetStream(string assetName)
		{
			var assembly = Game.GetType().Assembly;
			var path = Path.GetDirectoryName(assembly.Location);

			var fileName = string.Format("{0}\\{1}\\{2}", path, ContentManager.AssetsRoot, assetName);

			var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			return stream;
		}
	}
}

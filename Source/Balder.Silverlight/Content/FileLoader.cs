using System;
using System.IO;
using System.Windows;
using Balder.Core.Content;

namespace Balder.Silverlight.Content
{
	public class FileLoader : IFileLoader
	{
		private readonly IContentManager _contentManager;
		private readonly FilePathHelper _filePathHelper;


		public FileLoader(IContentManager contentManager, FilePathHelper filePathHelper)
		{
			_contentManager = contentManager;
			_filePathHelper = filePathHelper;
		}


		public Stream GetStream(string assetName)
		{
			var filename = _filePathHelper.GetFileNameForAsset(assetName);

			// Todo: add check to see if file exists
			var resourceStream = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
			
			return resourceStream.Stream;
		}
	}
}
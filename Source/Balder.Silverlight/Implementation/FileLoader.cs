using System;
using System.IO;
using System.Windows;
using Balder.Core.Content;
using Balder.Core.Interfaces;
using Balder.Core.Utils;

namespace Balder.Silverlight.Implementation
{
	public class FileLoader : IFileLoader
	{
		private readonly IContentManager _contentManager;

		public FileLoader(IContentManager contentManager)
		{
			_contentManager = contentManager;
		}

		public Stream GetStream(string assetName)
		{
			var fullAssemblyName = string.Empty;
			if( null != Application.Current )
			{
				fullAssemblyName = Application.Current.GetType().Assembly.FullName;
			} 
			var assemblyName = AssemblyHelper.GetAssemblyShortName(fullAssemblyName);

			var filename = string.Format("/{0};component/{1}/{2}",assemblyName,_contentManager.AssetsRoot,assetName);

			// Todo: add check to see if file exists
			var resourceStream = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
			
			return resourceStream.Stream;
		}
	}
}

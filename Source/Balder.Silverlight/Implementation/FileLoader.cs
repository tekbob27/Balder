using System;
using System.IO;
using System.Windows;
using Balder.Core;
using Balder.Core.Interfaces;
using Balder.Core.Services;
using Balder.Core.Utils;

namespace Balder.Silverlight.Implementation
{
	public class FileLoader : IFileLoader
	{
		public IContentManager ContentManager { get; set;}
		public Game Game { get; set; }

		public Stream GetStream(string assetName)
		{
			var fullAssemblyName = string.Empty;
			if( null != Application.Current.RootVisual )
			{
				fullAssemblyName = Application.Current.RootVisual.GetType().Assembly.FullName;
			} else
			{
				fullAssemblyName = Game.GetType().Assembly.FullName;
			}
			
			var assemblyName = AssemblyHelper.GetAssemblyShortName(fullAssemblyName);

			var filename = string.Format("/{0};component/{1}/{2}",assemblyName,ContentManager.AssetsRoot,assetName);

			// Todo: add check to see if file exists
			var resourceStream = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
			
			return resourceStream.Stream;
		}
	}
}

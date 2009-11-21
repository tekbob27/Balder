using Balder.Core.Content;
using Balder.Core.Execution;
using Balder.Core.Utils;

namespace Balder.Silverlight.Content
{
	public class FilePathHelper
	{
		private readonly IContentManager _contentManager;
		private readonly IPlatform _platform;

		public FilePathHelper(IContentManager contentManager, IPlatform platform)
		{
			_contentManager = contentManager;
			_platform = platform;
		}

		public string GetFileNameForAsset(string assetName)
		{
			var fullAssemblyName = _platform.EntryAssemblyName;
			var assemblyName = AssemblyHelper.GetAssemblyShortName(fullAssemblyName);
			var filename = string.Empty;

			if( assetName.Contains(";component/"))
			{
				filename = assetName;
			} else
			{
				filename = string.Format("/{0};component/{1}/{2}", assemblyName, _contentManager.AssetsRoot, assetName);	
			}

			
			return filename;
		}
	}
}

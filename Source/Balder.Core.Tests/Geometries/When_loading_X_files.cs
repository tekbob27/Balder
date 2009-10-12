using Balder.Core.Geometries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balder.Core.Tests.Specs_for_geometries
{
	[TestClass]
	public class When_loading_X_files
	{
		[TestMethod]
		public void X_file_is_a_supported_format()
		{
			var service = new AssetLoaderService();
			var loader = service.GetLoader<Geometry>("whatever.x");
			Assert.IsNotNull(loader);
		}
	}
}

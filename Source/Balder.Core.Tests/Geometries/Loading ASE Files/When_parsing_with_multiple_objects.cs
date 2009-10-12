using Balder.Core.Tests.Resources;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balder.Core.Tests.Geometries.Loading_ASE_Files
{
	[TestClass]
	public class When_parsing_with_multiple_objects
	{
		[TestMethod]
		public void Multiple_geometries_should_be_generated()
		{
			var geometries = AseLoaderUtils.LoadGeometryFromBytes(Models.ThreeObjectsNoMaterials);
			geometries.Length.ShouldBe(3);
		}
	}
}

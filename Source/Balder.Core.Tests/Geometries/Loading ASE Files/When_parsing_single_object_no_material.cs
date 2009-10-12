using Balder.Core.AssetLoaders;
using Balder.Core.Exceptions;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Runtime;
using Balder.Core.Services;
using Balder.Core.Tests.Fakes;
using Balder.Core.Tests.Resources;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Balder.Core.Tests.Geometries.Loading_ASE_Files
{
	[TestClass]
	public class When_parsing_single_object_no_material
	{
		[TestInitialize]
		public void Setup()
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);
		}

		[TestMethod]
		public void ASE_is_a_recognized_asset_type()
		{
			var service = new AssetLoaderService();
			var loader = service.GetLoader<Geometry>("test.ase");
			loader.ShouldNotBeNull();
		}

		[TestMethod]
		public void Nonexisting_assets_should_throw_AssetNotFoundException()
		{
			const string asset = "test.ase";
			var loaderServiceMock = new Mock<IAssetLoaderService>();

			var fileLoaderMock = new Mock<IFileLoader>();
			fileLoaderMock.Expect(l => l.GetStream(asset)).Returns(
				() =>
				{
					return null;
				});
			var contentManagerMock = new Mock<IContentManager>();

			var loader = new AseLoader(loaderServiceMock.Object, fileLoaderMock.Object, contentManagerMock.Object);
			loader.ShouldThrowException<AssetNotFoundException>(()=>loader.Load(asset));
		}

		[TestMethod]
		public void Geometry_should_have_name()
		{
			var geometries = AseLoaderUtils.LoadGeometryFromBytes(Models.SingleObjectNoMaterials);
			geometries[0].Name.ShouldNotBeNullOrEmpty();
		}

		[TestMethod]
		public void Geometry_should_have_the_matrix_defined_in_file()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void Geometry_should_have_scale_set()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void Geometry_should_have_color_set()
		{
			Assert.Inconclusive();			
		}

		[TestMethod]
		public void Vertices_should_be_recognized()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void Faces_should_be_recognized()
		{
			Assert.Inconclusive();
		}
	}
}

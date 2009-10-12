using System.IO;
using Balder.Core.AssetLoaders;
using Balder.Core.Geometries;
using Balder.Core.Interfaces;
using Balder.Core.Runtime;
using Balder.Core.Services;
using Moq;

namespace Balder.Core.Tests.Geometries.Loading_ASE_Files
{
	public class AseLoaderUtils
	{
		public static Geometry[] LoadGeometryFromBytes(byte[] data)
		{
			const string asset = "test.ase";
			var loaderServiceMock = new Mock<IAssetLoaderService>();

			var fileLoaderMock = new Mock<IFileLoader>();
			fileLoaderMock.Expect(l => l.GetStream(asset)).Returns(
				() =>
				{
					var memoryStream = new MemoryStream(data);
					return memoryStream;
				});
			var contentManagerMock = new Mock<IContentManager>();
			contentManagerMock.Expect(c => c.CreateAssetPart<Geometry>()).Returns(() => EngineRuntime.Instance.Kernel.Get<Geometry>());

			var loader = new AseLoader(loaderServiceMock.Object, fileLoaderMock.Object, contentManagerMock.Object);
			var geometries = loader.Load(asset);
			return geometries;
		}

	}
}

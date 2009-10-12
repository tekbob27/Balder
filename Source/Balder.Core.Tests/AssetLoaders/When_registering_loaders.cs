using Balder.Core.Geometries;
using Balder.Core.Tests.Fakes;
using Balder.Core.Tests.Fakes.AssetLoaders;
using Balder.Core.Tests.Fakes.AssetLoaders.Nested;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balder.Core.Runtime;

namespace Balder.Core.Tests.AssetLoaders
{
	[TestClass]
	public class When_registering_loaders
	{
		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			var targetDevice = new TargetDevice();
			EngineRuntime.Instance.Initialize(targetDevice);
		}

		[TestMethod]
		public void With_an_assembly_should_register()
		{
			var asm = typeof(When_registering_loaders).Assembly;
			var rootAssetLoaderType = typeof(RootAssetLoader);

			var service = new AssetLoaderService();
			var countBefore = service.AvailableLoaders.Length;
			service.RegisterAssembly(asm);
			var loaders = service.AvailableLoaders;
			var countAfter = loaders.Length;

			Assert.AreEqual(2, countAfter - countBefore);

			var foundLoader = false;
			foreach( var loader in loaders )
			{
				if (loader.GetType().Equals(rootAssetLoaderType))
				{
					foundLoader = true;
					break;
				}
			}
			foundLoader.ShouldBeTrue();
		}


		[TestMethod]
		public void With_a_namespace_without_recursive_should_register()
		{
			
			var asm = typeof (When_registering_loaders).Assembly;
			var rootAssetLoaderType = typeof (RootAssetLoader);

			var service = new AssetLoaderService();

			var countBefore = service.AvailableLoaders.Length;
			service.RegisterNamespace(asm,rootAssetLoaderType.Namespace);
			var loaders = service.AvailableLoaders;
			var countAfter = loaders.Length;

			(countAfter-countBefore).ShouldBe(1);
			loaders[countBefore].ShouldBeInstanceOfType(rootAssetLoaderType);
		}


		[TestMethod]
		public void With_a_namespaces_with_recursive_registers()
		{
			var asm = typeof(When_registering_loaders).Assembly;
			var rootAssetLoaderType = typeof(RootAssetLoader);
			var nestedAssetLoaderType = typeof (NestedAssetLoader);

			var service = new AssetLoaderService();

			var countBefore = service.AvailableLoaders.Length;
			service.RegisterNamespace(asm, rootAssetLoaderType.Namespace,true);
			var loaders = service.AvailableLoaders;
			var countAfter = loaders.Length;

			(countAfter - countBefore).ShouldBe(2);
			loaders[countBefore].ShouldBeInstanceOfType(rootAssetLoaderType);
			loaders[countBefore+1].ShouldBeInstanceOfType(nestedAssetLoaderType);
		}

		[TestMethod]
		public void Getting_specific_registered_loader_should_return_valid_loader()
		{
			
			var asm = typeof(When_registering_loaders).Assembly;
			var rootAssetLoader = new RootAssetLoader(null,null);


			var service = new AssetLoaderService();
			service.RegisterLoader(rootAssetLoader);

			var assetName = string.Format("test.{0}", rootAssetLoader.FileExtensions[0]);
			var loader = service.GetLoader<Geometry>(assetName);
			loader.ShouldNotBeNull();
			loader.ShouldBeSameAs(rootAssetLoader);
		}

	}
}

using System;
using System.Collections.Generic;
using Balder.Core.Assets;
using Balder.Core.Objects.Geometries;
using Balder.Core.Runtime;
using Ninject.Core;
using Ninject.Core.Parameters;

namespace Balder.Core.Content
{
	[Singleton]
	public class ContentManager : IContentManager
	{
		public const string DefaultAssetsRoot = "Assets";

		public ContentManager()
		{
			AssetsRoot = DefaultAssetsRoot;
		}

		public T Load<T>(string assetName)
			where T:IAsset
		{
			var asset = EngineRuntime.Instance.Kernel.Get<T>();
			asset.Load(assetName);
			return asset;
		}

		public T CreateGeometry<T>() where T : Geometry
		{
			var geometry = EngineRuntime.Instance.Kernel.Get<T>();
			return geometry;
		}


		public Box CreateBox()
		{
			throw new NotImplementedException();
		}

		public Geometry CreateSphere()
		{
			throw new NotImplementedException();
		}

		public Cylinder CreateCylinder(float radius, float height, int segments, int heightSegments)
		{
			var arguments = new Dictionary<string, object>
			                	{
			                		{"radius", radius},
			                		{"height", height},
			                		{"segments", segments},
			                		{"heightSegments", heightSegments}
			                	};
			
			var cylinder = EngineRuntime.Instance.Kernel.Get<Cylinder>(
				With.Parameters.ConstructorArguments(arguments)
				);
			return cylinder;
		}

		public T CreateAssetPart<T>() where T:IAssetPart
		{
			var part = EngineRuntime.Instance.Kernel.Get<T>();
			return part;
		}

		public Game Game { get; set; }
		public string AssetsRoot { get; set;}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Balder.Core.Runtime;
using Balder.Core.Utils;
using Ninject.Core;

namespace Balder.Core.Assets
{
	[Singleton]
	public class AssetLoaderService : IAssetLoaderService
	{
		private readonly IObjectFactory _objectFactory;
		private readonly Dictionary<string, IAssetLoader> _assetLoaders;

		public AssetLoaderService(IObjectFactory objectFactory)
		{
			_objectFactory = objectFactory;
			_assetLoaders = new Dictionary<string, IAssetLoader>();
		}

		public void Initialize()
		{
			var assembly = typeof(AssetLoaderService).Assembly;
			var shortName = AssemblyHelper.GetAssemblyShortName(assembly.FullName);

			// Todo: Bad bad bad - the name of the assembly should be discovered automatically!
			var loaderNamespace = string.Format("Balder.Core.Assets.AssetLoaders");

			RegisterNamespace(typeof(AssetLoaderService).Assembly, loaderNamespace);
		}

		public void RegisterAssembly(string fullyQualifiedName)
		{
			var asm = Assembly.Load(fullyQualifiedName);
			RegisterAssembly(asm);
		}


		public void RegisterAssembly(Assembly assembly)
		{
			var types = assembly.GetTypes();
			var baseType = typeof(AssetLoader<>);

			var query = from t in types
			            where t.BaseType.Name.Equals(baseType.Name)
			            select t;

			foreach (var type in query)
			{
				var loader = _objectFactory.Get(type) as IAssetLoader;
				RegisterLoader(loader);
			}
		}

		public void RegisterNamespace(Assembly assembly, string ns)
		{
			RegisterNamespace(assembly,ns,false);
		}

		public void RegisterNamespace(Assembly assembly, string ns, bool recursive)
		{
			var types = assembly.GetTypes();
			var baseType = typeof(AssetLoader<>);

			Func<Type,bool>   nsCheck;
			
			if( recursive )
			{
				nsCheck = (t) => t.Namespace.StartsWith(ns);
			} else
			{
				nsCheck = (t) => t.Namespace.Equals(ns);
			}

			var query = from t in types
			            where null != t.Namespace && nsCheck(t) && t.BaseType.Name.Equals(baseType.Name)
			            select t;

			foreach (var type in query)
			{
				var loader = _objectFactory.Get(type) as IAssetLoader;
				RegisterLoader(loader);
			}
		}


		public void RegisterLoader(IAssetLoader loader)
		{
			var type = loader.GetType();
			if( !type.BaseType.Name.Equals(typeof(AssetLoader<>).Name) )
			{
				throw new ArgumentException("The loader must be of type AssetLoader<T>");
			}

			foreach( var extension in loader.FileExtensions )
			{
				_assetLoaders[extension.ToLower()] = loader;
			}
		}

		public AssetLoader<T> GetLoader<T>(string assetName)
			where T:IAssetPart
		{
			var extension = Path.GetExtension(assetName).ToLower();
			extension = extension.Substring(1);

			if( !_assetLoaders.ContainsKey(extension))
			{
				throw new ArgumentException("There is no loader for the specified file type '"+extension+"'");
			}

			return _assetLoaders[extension] as AssetLoader<T>;
		}


		public IAssetLoader[]	AvailableLoaders
		{
			get
			{
				return _assetLoaders.Values.ToArray();
			}
		}
	}
}
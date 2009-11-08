using System;
using Balder.Core.Execution;
using Balder.Core.Objects.Geometries;

namespace Balder.Core.Content
{
	public class ContentCreator
	{
		private readonly IObjectFactory _objectFactory;

		public ContentCreator(IObjectFactory objectFactory)
		{
			_objectFactory = objectFactory;
		}

		public T CreateGeometry<T>() where T : Geometry
		{
			var geometry = _objectFactory.Get<T>();
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
			var cylinder = _objectFactory.Get<Cylinder>(
					new[]
						{
							new ConstructorArgument { Name="radius", Value=radius},
							new ConstructorArgument { Name="height", Value=height},
							new ConstructorArgument { Name="segments", Value=segments},
							new ConstructorArgument { Name="heightSegments", Value=heightSegments},
						}
				);
			return cylinder;
		}


	}
}

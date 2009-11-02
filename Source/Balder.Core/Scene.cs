#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Collections;
using Balder.Core.Interfaces;
using Balder.Core.Lighting;
using Balder.Core.Math;
using Balder.Core.Objects.Flat;
using Balder.Core.Objects.Geometries;
using Geometry=Balder.Core.Objects.Geometries.Geometry;
using Matrix=Balder.Core.Math.Matrix;
using Balder.Core.Extensions;

namespace Balder.Core
{
	public class Scene
	{
		private readonly NodeCollection _renderableNodes;
		private readonly NodeCollection _flatNodes;
		private readonly NodeCollection _environmentalNodes;

		public Color AmbientColor;

		public Scene()
		{
			_renderableNodes = new NodeCollection();
			_flatNodes = new NodeCollection();
			_environmentalNodes = new NodeCollection();

			AmbientColor = Color.FromArgb(0xff, 0x3f, 0x3f, 0x3f);
		}

		public void AddNode(Node node)
		{
			node.Scene = this;
			if( node is RenderableNode )
			{
				_renderableNodes.Add(node);
				if( node is Sprite )
				{
					_flatNodes.Add(node);
				}
			} else
			{
				_environmentalNodes.Add(node);
			}
		}

		public NodeCollection RenderableNodes { get { return _renderableNodes;  } }


		public Color CalculateColorForVector(IViewport viewport, Vector vector, Vector normal)
		{
			var color = AmbientColor.ToVector();

			foreach( var node in _environmentalNodes )
			{
				if( node is Light )
				{
					var light = node as Light;
					var currentLightResult = light.Calculate(viewport, vector, normal);
					var currentLightResultAsVector = currentLightResult.ToVector();
					color += currentLightResultAsVector;
				}
			}
			return color.ToColorWithClamp();
		}

		public void Render(IViewport viewport, Matrix view, Matrix projection)
		{
			foreach( RenderableNode node in _renderableNodes )
			{
				node.PrepareRender();
				node.Render(viewport,view,projection);
			}
		}

		public int TotalVertexCount
		{
			get
			{
				var count = 0;
				foreach( var node in _renderableNodes )
				{
					if( node is Geometry )
					{
						var geometry = node as Geometry;
						count += geometry.GeometryContext.VertexCount;
					}
				}
				return count;
			}
		}

		public int TotalFaceCount
		{
			get
			{
				var count = 0;
				foreach (var node in _renderableNodes)
				{
					if (node is Geometry)
					{
						var geometry = node as Geometry;
						count += geometry.GeometryContext.FaceCount;
					}
					if( node is Mesh)
					{
						var mesh = node as Mesh;
						count += mesh.TotalFaceCount;
					}
				}
				return count;
			}
		}

		public int TotalFlatNodes
		{
			get
			{
				return _flatNodes.Count;
			}
		}

		public RenderableNode	GetNodeAtScreenCoordinate(IViewport viewport, int x, int y)
		{
			var nearSource = new Vector((float)x, (float)y, 0f);
			var farSource = new Vector((float)x, (float)y, 1f);
			var camera = viewport.Camera;
			var nearPoint = viewport.Unproject(nearSource, camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);
			var farPoint = viewport.Unproject(farSource, camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);

			var direction = farPoint - nearPoint;
			direction.Normalize();

			var pickRay = new Ray(nearPoint, direction);

			var closestObjectDistance = float.MaxValue;
			RenderableNode closestObject = null;

			foreach (var node in RenderableNodes)
			{
				var transformedSphere = node.BoundingSphere.Transform(node.World);
				var distance = pickRay.Intersects(transformedSphere);
				if( distance.HasValue )
				{
					if( distance < closestObjectDistance )
					{
						closestObject = node as RenderableNode;
						closestObjectDistance = distance.Value;
					}
				}
			}

			return closestObject;
		}
	}
}

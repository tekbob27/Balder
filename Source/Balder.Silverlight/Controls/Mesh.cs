using Balder.Core;
using Balder.Core.Content;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Mesh : RenderedNode
	{
		public static DependencyProperty<Mesh, string> AssetNameProperty =
			DependencyProperty<Mesh, string>.Register(o => o.AssetName);
		public string AssetName
		{
			get { return AssetNameProperty.GetValue(this); }
			set
			{
				AssetNameProperty.SetValue(this,value);
			}
		}

		public override void Initialize(IContentManager contentManager, Scene scene)
		{
			var asset = contentManager.Load<Balder.Core.Objects.Geometries.Mesh>(AssetName);
			scene.AddNode(asset);
			Node = asset;
		}



	}
}

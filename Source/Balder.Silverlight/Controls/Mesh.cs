using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Mesh : Geometry
	{
		public static DependencyProperty<Mesh, string> AssetNameProperty =
			DependencyProperty<Mesh, string>.Register(o => o.AssetName);
		public string AssetName
		{
			get { return AssetNameProperty.GetValue(this); }
			set
			{
				AssetNameProperty.SetValue(this, value);
			}
		}

		/*
		protected override void Initialize()
		{
			ActualNode = ContentManager.Load<Core.Objects.Geometries.Mesh>(AssetName);
			//ActualScene.AddNode(ActualNode);
			base.Initialize();
		}
		 * */
	}
}

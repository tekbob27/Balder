using System;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Mesh : Geometry
	{
		public static DependencyProperty<Mesh, Uri> AssetNameProperty =
			DependencyProperty<Mesh, Uri>.Register(o => o.AssetName);
		public Uri AssetName
		{
			get { return AssetNameProperty.GetValue(this); }
			set
			{
				AssetNameProperty.SetValue(this, value);
			}
		}

		protected override void Initialize()
		{
			if( null != AssetName )
			{
				ActualNode = ContentManager.Load<Core.Objects.Geometries.Mesh>(AssetName.ToString());	
			}
			base.Initialize();
		}

	}
}

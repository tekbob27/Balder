using System.Reflection;
using Balder.Silverlight.Controls.Math;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Node : BalderControl
	{
		public Core.Node ActualNode { get; protected set; }

		protected override void OnLoaded()
		{
			Children.Add(Position);
			base.OnLoaded();
		}

		public static readonly DependencyProperty<Mesh, Vector> PositionProperty =
			DependencyProperty<Mesh, Vector>.Register(o => o.Position);
		public Vector Position
		{
			get
			{
				var vector = PositionProperty.GetValue(this);
				if (null == vector)
				{
					vector = new Vector();
					SetupVectorSubscription(vector);
					PositionProperty.SetValue(this,vector);
				}
				return vector;
			}
			set
			{
				SetupVectorSubscription(value);
				PositionProperty.SetValue(this, value);
			}
		}

		private static readonly FieldInfo XField = typeof(Core.Math.Vector).GetField("X");
		private static readonly FieldInfo YField = typeof(Core.Math.Vector).GetField("Y");
		private static readonly FieldInfo ZField = typeof(Core.Math.Vector).GetField("Z");

		public void SetupVectorSubscription(Vector vector)
		{
			vector.PropertyChanged += (s, e) =>
			{
				ActualNode.Position.X = (float)vector.X;
				ActualNode.Position.Y = (float)vector.Y;
				ActualNode.Position.Z = (float)vector.Z;
			};
		}
	}
}

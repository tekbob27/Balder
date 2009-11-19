using System.Reflection;
using System.Windows;
using Balder.Silverlight.Controls.Math;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Node : FrameworkElement
	{
		public Core.Node ActualNode { get; protected set; }

		public static readonly DependencyProperty<Node, Vector> PositionProperty =
			DependencyProperty<Node, Vector>.Register(o => o.Position);
		public Vector Position
		{
			get
			{
				var vector = PositionProperty.GetValue(this);
				if( null == vector )
				{
					vector = new Vector();
				}
				return vector;
			}
			private set
			{
				SetupVectorSubscription(value);
				PositionProperty.SetValue(this,value);
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

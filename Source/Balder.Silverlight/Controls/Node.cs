using System.Reflection;
using Balder.Silverlight.Controls.Math;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Node : BalderControl
	{
		private Core.Node _actualNode;
		public Core.Node ActualNode
		{
			get { return _actualNode; }
			protected set
			{
				_actualNode = value;
				InitializeNativeVector();
			}
		}


		private void InitializeNativeVector()
		{
			Position.SetNativeAction((x,y,z) =>
			                         	{
			                         		ActualNode.Position.X = x;
			                         		ActualNode.Position.Y = y;
			                         		ActualNode.Position.Z = z;
			                         	});
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
					PositionProperty.SetValue(this,vector);
					InitializeNativeVector();
				}
				return vector;
			}
			set
			{
				PositionProperty.SetValue(this, value);
				InitializeNativeVector();
			}
		}
	}
}

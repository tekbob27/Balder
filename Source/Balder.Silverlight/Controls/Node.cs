#region License
//
// Author: Einar Ingebrigtsen <einar@dolittle.com>
// Copyright (c) 2007-2009, DoLittle Studios
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://balder.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
				Position = new Vector();
			}
		}

		private void InitializePosition()
		{
			Position.SetNativeAction((x, y, z) =>
			{
				ActualNode.Position.X = x;
				ActualNode.Position.Y = y;
				ActualNode.Position.Z = z;
			});
			
		}

		public static readonly DependencyProperty<Node, Vector> PositionProperty =
			DependencyProperty<Node, Vector>.Register(o => o.Position);
		public Vector Position
		{
			get { return PositionProperty.GetValue(this); }
			set
			{
				PositionProperty.SetValue(this, value);
				InitializePosition();
			}
		}
	}
}

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

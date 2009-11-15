using System;
using Balder.Core.Math;

namespace Balder.Core.SoftwareRendering
{
	public struct Span
	{
		public int Y;
		public int XStart;
		public int XEnd;
		public int Length;

		public float ZStart;
		public float ZEnd;

		public float UStart;
		public float UEnd;

		public float VStart;
		public float VEnd;

		public Color ColorStart;
		public Color ColorEnd;

		public bool Swap;
	}
}

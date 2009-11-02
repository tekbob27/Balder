using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Core.Extensions
{
	public static class ViewportExtensions
	{
		private const float MinDepth = 0f;
		private const float MaxDepth = 1f;

		private static bool WithinEpsilon(float a, float b)
		{
			var num = a - b;
			return ((-1.401298E-45f <= num) && (num <= float.Epsilon));
		}

		public static Vector Unproject(this IViewport viewport, Vector source, Matrix projection, Matrix view, Matrix world)
		{
			var multipliedMatrix = (world*view)*projection;
			var matrix = Matrix.Invert(multipliedMatrix);
			source.X = (((source.X - viewport.XPosition) / ((float)viewport.Width)) * 2f) - 1f;
			source.Y = -((((source.Y - viewport.YPosition) / ((float)viewport.Height)) * 2f) - 1f);
			source.Z = (source.Z - MinDepth) / (MaxDepth - MinDepth);
			var vector = Vector.Transform(source, matrix);
			var a = (((source.X * matrix[0,3]) + (source.Y * matrix[1,3])) + (source.Z * matrix[2,3])) + matrix[3,3];
			if (!WithinEpsilon(a, 1f))
			{
				vector = (Vector)(vector / a);
			}
			return vector;
		}
	}
}

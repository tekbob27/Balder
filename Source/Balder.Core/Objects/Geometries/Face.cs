#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Materials;
using Balder.Core.Math;
using Matrix = Balder.Core.Math.Matrix;

namespace Balder.Core.Objects.Geometries
{
	public struct Face
	{
		public static readonly float DebugNormalLength = 5f;
		public Vector Normal;
		public Vector TransformedNormal;
		public Vector Position;
		public Vector TransformedPosition;
		public Vector TranslatedPosition;
		public Vector TransformedDebugNormal;
		public Vector TranslatedDebugNormal;

		public Material Material;
		public Color Color;

		public int A;
		public int B;
		public int C;

		public int DiffuseA;
		public int DiffuseB;
		public int DiffuseC;

		public Face(int a, int b, int c)
			: this()
		{
			A = a;
			B = b;
			C = c;
		}



		public void Transform(Matrix world, Matrix view)
		{
			TransformedNormal = Vector.TransformNormal(Normal, world);
			TransformedNormal = Vector.TransformNormal(TransformedNormal, view);
			TransformedPosition = Vector.Transform(Position, world, view);

			TransformedDebugNormal = TransformedPosition +(TransformedNormal); //*DebugNormalLength);
		}

		public void Translate(Matrix projectionMatrix, float width, float height)
		{
			TranslatedPosition = Vector.Translate(TransformedPosition, projectionMatrix, width, height);
			TranslatedDebugNormal = Vector.Translate(TransformedDebugNormal, projectionMatrix, width, height);
		}
	}
}
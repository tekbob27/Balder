namespace Balder.Core.Math
{
	public class Plane
	{
		private Vector _vector1;
		private Vector _vector2;
		private Vector _vector3;
		private Vector _normal;
		private Vector _point;
		private float _distance;

		public Plane()
		{

		}

		public Plane(Vector vector, float distance)
		{
			_normal = vector;
			_normal.Normalize();
			_distance = distance;
		}


		public void SetVectors(Vector vector1, Vector vector2, Vector vector3)
		{
			_vector1 = vector1;
			_vector2 = vector2;
			_vector3 = vector3;

			var aux1 = vector1 - vector2;
			var aux2 = vector3 - vector2;

			_normal = aux2 * aux1;
			_normal.Normalize();
			_point = vector2;
			_distance = -_normal.Dot(_point);
		}


		public void SetNormalAndPoint(Vector normal, Vector point)
		{
			_normal = normal;
			_normal.Normalize();
			_point = point;
			_distance = -_normal.Dot(_point);
		}

		public void SetCoefficients(float a, float b, float c, float d)
		{
			_normal.X = a;
			_normal.Y = b;
			_normal.Z = c;

			var length = _normal.Length;

			_normal.Normalize();

			_distance = d / length;
		}

		public float GetDistanceFromVector(Vector vector)
		{
			return _distance + _normal.Dot(vector);
		}
	}
}

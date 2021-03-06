using System;

namespace Balder.Core.Math
{
    public struct Vector
    {
		// Something new

		// Something else

        #region Constructors

        public Vector(float x, float y, float z)
			: this()
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }

        public Vector(float x, float y, float z, float w)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector(Vector v1)
			: this()
        {
            X = v1.X;
            Y = v1.Y;
            Z = v1.Z;
            W = v1.W;
        }

        #endregion

        #region Public fields

    	public static Vector Zero = new Vector(0f, 0f, 0f);
		public static Vector Forward = new Vector(0f,0f,1f);
		public static Vector Left = new Vector(-1f,0f,0f);
		public static Vector Right = new Vector(1f,0f,0f);
		public static Vector Up = new Vector(0f,1f,0f);		// Todo: 18th of july 2009 This is not reflected yet in the rest of the engine

    	public float X; 
    	public float Y; 
    	public float Z;
        public float W;

        #endregion

        #region Operators

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector operator *(Vector v1, float s2)
        {
            return new Vector(v1.X * s2, v1.Y * s2, v1.Z * s2);
        }

        public static Vector operator *(float s1, Vector v2)
        {
            return v2 * s1;
        }

        public static Vector operator *(Vector v1, Vector v2)
        {
            return v1.Cross(v2);
        }

        public static Vector operator /(Vector v1, float s2)
        {
            return new Vector(v1.X / s2, v1.Y / s2, v1.Z / s2);
        }

        public static Vector operator -(Vector v1)
        {
            return new Vector(-v1.X, -v1.Y, -v1.Z);
        }

        public static bool operator <(Vector v1, Vector v2)
        {
            return v1.Length < v2.Length;
        }

        public static bool operator >(Vector v1, Vector v2)
        {
            return v1.Length > v2.Length;
        }

        public static bool operator <=(Vector v1, Vector v2)
        {
            return v1.Length <= v2.Length;
        }

        public static bool operator >=(Vector v1, Vector v2)
        {
            return v1.Length >= v2.Length;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            if (+(v1.X - v2.X) < float.Epsilon && +(v1.Y - v2.Y) < float.Epsilon && +(v1.Z - v2.Z) < float.Epsilon)
                return true;
            return false;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        #endregion

        #region Public Static Methods

        public static Vector Cross(Vector v1, Vector v2)
        {
            return new Vector(v1.Y * v2.Z - v2.Y * v1.Z, v1.Z * v2.X - v2.Z * v1.X, v1.X * v2.Y - v2.X * v1.Y);
        }

        public static float MixedProduct(Vector v1, Vector v2, Vector v3)
        {
            return Dot(Cross(v1, v2), v3);
        }

        public static float Dot(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector Normalize(Vector v)
        {
            if (v.X * 2 + v.Y * 2 + v.Z * 2 == 0)
                return v;

            float r = 1 / v.Length;

            v.X *= r;
            v.Y *= r;
            v.Z *= r;

            return v;
        }

        // Calculate the reflectionvector around a given normal, and return that
        public static Vector Reflect(Vector vI, Vector vN)
        {
            //R = 2 * (N.I) * N – I 
            Vector R = 2 * vI.Dot(vN) * vN - vI;

            return R;
        }

		private static readonly float[] vectorOut = new float[4];
		private static readonly float[] vectorIn = new float[4];

		public static Vector Transform(Vector position, Matrix matrix)
		{
			var outVector = new Vector();
			//Create a new vector and make it 4d homogenous
			vectorIn[0] = position.X;
			vectorIn[1] = position.Y;
			vectorIn[2] = position.Z;
            vectorIn[3] = position.W;

			for (int i = 0; i < 4; i++)
			{
				vectorOut[i] = 0.0f;
				for (int j = 0; j < 4; j++)
				{
					vectorOut[i] += vectorIn[j] * matrix[j, i];
				}
			}

			//Unhomogenize the result vector and return a 3d vector
			outVector.X = vectorOut[0] / vectorOut[3];
			outVector.Y = vectorOut[1] / vectorOut[3];
			outVector.Z = vectorOut[2] / vectorOut[3];
            outVector.W = vectorOut[3];

			return outVector;
		}

		public static Vector TransformNormal(Vector position, Matrix matrix)
		{
			Vector vector = new Vector();
			float x = (((position.X * matrix[0, 0]) + (position.Y * matrix[1, 0])) + (position.Z * matrix[2, 0]));
			float y = (((position.X * matrix[0, 1]) + (position.Y * matrix[1, 1])) + (position.Z * matrix[2, 1]));
			float z = (((position.X * matrix[0, 2]) + (position.Y * matrix[1, 2])) + (position.Z * matrix[2, 2]));
			vector.X = x;
			vector.Y = y;
			vector.Z = z;
			return vector;
		}
 

		public static Vector Transform(Vector vector, Matrix world, Matrix view)
		{
			var transformedVector = Vector.Transform(vector, world);
			transformedVector = Vector.Transform(transformedVector, view);
			return transformedVector;
		}

		public static Vector Translate(Vector vector, Matrix projection, float width, float height)
		{
			var translated = Vector.Transform(vector, projection);

			translated.X = (translated.X * width) + (width / 2);
			translated.Y = (translated.Y * height) + (height / 2);
			translated.Z = 0;

			return translated;
		}



    	#endregion

        #region Private Methods

        public Vector Cross(Vector v)
        {
            return Cross(this, v);
        }

        public float Dot(Vector v)
        {
            return Dot(this, v);
        }

        public float MixedProduct(Vector v1, Vector v2)
        {
            return Dot(Cross(this, v1), v2);
        }

        public void Normalize()
        {
			if (X * 2 + Y * 2 + Z * 2 == 0)
			{
				return;
			}

            float r = 1 / Length;

            X *= r;
			Y *= r;
			Z *= r;
        }

        public float Length
        {
            get
            {
            	return (float)System.Math.Sqrt(X*X + Y*Y + Z*Z); 
					//Core.Sqrt(X*X + Y*Y + Z*Z);
            }
            set
            {
                Vector newVector = this * (value / Length);

            	X = newVector.X;
            	Y = newVector.Y;
            	Z = newVector.Z;
            }
        }

        #endregion

		#region Overrides

		public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            // If no format is passed
            if (string.IsNullOrEmpty(format))
            {
                return String.Format("({0}, {1}, {2}, {3})", X, Y, Z, W);
            }

            var firstChar = format[0];
            string remainder = null;

			if (format.Length > 1)
			{
				remainder = format.Substring(1);
			}

        	switch (firstChar)
            {
                case 'x':
            		{
            			return X.ToString(remainder, formatProvider);
            		}
                case 'y':
            		{
            			return Y.ToString(remainder, formatProvider);
            		}
                case 'z':
            		{
            			return Z.ToString(remainder, formatProvider);
            		}
                case 'w':
                    {
                        return W.ToString(remainder, formatProvider);
                    }
                default:
                    return String.Format
                        (
                            "({0}, {1}, {2}, {3})",
                            X.ToString(format, formatProvider),
                            Y.ToString(format, formatProvider),
                            Z.ToString(format, formatProvider),
                            W.ToString(format, formatProvider)
                        );
            }
        }

		/*
        public override int GetHashCode()
        {
            return 0;
        }
		 * */

        public override bool Equals(object other)
        {
            // Check object other is a Vector3 object
            if (other is Vector)
            {
                // Convert object to Vector3
                Vector otherVector = (Vector)other;

                // Check for equality
                return otherVector == this;
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Vector other)
        {
            return other == this;
        }

        public int CompareTo(Vector other)
        {
            if (this < other)
            {
                return -1;
            }
            else if (this > other)
            {
                return 1;
            }
            return 0;
        }

        public int CompareTo(object other)
        {
            if (other is Vector)
            {
                return CompareTo((Vector)other);
            }
            else
            {
                return -1;
            }
        }

        #endregion

    }
}
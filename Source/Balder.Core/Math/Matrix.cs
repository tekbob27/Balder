namespace Balder.Core.Math
{
    public class Matrix
    {
        public float[] data;

		public Matrix()
		{
			Initialize();
		}

        public Matrix(float[] Matrix)
        {
			Initialize();
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = Matrix[i];
			}
        }

		private void Initialize()
		{
			data = new float[4 * 4];
		}


		private static void SetIdentity(Matrix matrix)
		{
			for (int i = 0; i < matrix.data.Length; i++)
			{
				matrix.data[i] = 0;
			}

			for (int i = 0; i < 4; i++)
			{
				matrix[i, i] = 1;
			}
		}

        public float this[int i, int j]
        {
            get
            {
                if (data == null)
                    data = new float[4*4];
                return data[(i << 2) + j];
            }
            set
            {
                if (data == null)
                    data = new float[4 * 4];
                data[(i << 2) + j] = value;
            }
        }

        public static explicit operator float[](Matrix value)
        {
            return value.data;
        }


		public static Matrix operator +(Matrix matrix1, Matrix matrix2)
		{
			Matrix matrix = new Matrix();
			for( int i=0; i<matrix.data.Length; i++ )
			{
				matrix.data[i] = matrix1.data[i] + matrix2.data[i];
			}
			return matrix;
		}

        // [ ][ ][ ][ ]   [ ][ ][ ][ ]
        // [ ][ ][ ][ ] x [ ][ ][ ][ ]
        // [ ][ ][ ][ ]   [ ][ ][ ][ ]
        // [ ][ ][ ][ ]   [ ][ ][ ][ ]
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
			/*
        	Matrix result = Matrix.Identity;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					result[i, j] = 0;
					for (int k = 0; k < 4; k++)
					{
						result[i,j] += matrix1[i,k] * matrix2[k,j];
					}
				}
			}
			 

            return result;
			*/
			
        	Matrix matrix = Matrix.Identity;
			matrix[0,0] = (((matrix1[0,0] * matrix2[0,0]) + (matrix1[0,1] * matrix2[1,0])) + (matrix1[0,2] * matrix2[2,0])) + (matrix1[0,3] * matrix2[3,0]);
			matrix[0,1] = (((matrix1[0,0] * matrix2[0,1]) + (matrix1[0,1] * matrix2[1,1])) + (matrix1[0,2] * matrix2[2,1])) + (matrix1[0,3] * matrix2[3,1]);
			matrix[0,2] = (((matrix1[0,0] * matrix2[0,2]) + (matrix1[0,1] * matrix2[1,2])) + (matrix1[0,2] * matrix2[2,2])) + (matrix1[0,3] * matrix2[3,2]);
			matrix[0,3] = (((matrix1[0,0] * matrix2[0,3]) + (matrix1[0,1] * matrix2[1,3])) + (matrix1[0,2] * matrix2[2,3])) + (matrix1[0,3] * matrix2[3,3]);
			matrix[1,0] = (((matrix1[1,0] * matrix2[0,0]) + (matrix1[1,1] * matrix2[1,0])) + (matrix1[1,2] * matrix2[2,0])) + (matrix1[1,3] * matrix2[3,0]);
			matrix[1,1] = (((matrix1[1,0] * matrix2[0,1]) + (matrix1[1,1] * matrix2[1,1])) + (matrix1[1,2] * matrix2[2,1])) + (matrix1[1,3] * matrix2[3,1]);
			matrix[1,2] = (((matrix1[1,0] * matrix2[0,2]) + (matrix1[1,1] * matrix2[1,2])) + (matrix1[1,2] * matrix2[2,2])) + (matrix1[1,3] * matrix2[3,2]);
			matrix[1,3] = (((matrix1[1,0] * matrix2[0,3]) + (matrix1[1,1] * matrix2[1,3])) + (matrix1[1,2] * matrix2[2,3])) + (matrix1[1,3] * matrix2[3,3]);
			matrix[2,0] = (((matrix1[2,0] * matrix2[0,0]) + (matrix1[2,1] * matrix2[1,0])) + (matrix1[2,2] * matrix2[2,0])) + (matrix1[2,3] * matrix2[3,0]);
			matrix[2,1] = (((matrix1[2,0] * matrix2[0,1]) + (matrix1[2,1] * matrix2[1,1])) + (matrix1[2,2] * matrix2[2,1])) + (matrix1[2,3] * matrix2[3,1]);
			matrix[2,2] = (((matrix1[2,0] * matrix2[0,2]) + (matrix1[2,1] * matrix2[1,2])) + (matrix1[2,2] * matrix2[2,2])) + (matrix1[2,3] * matrix2[3,2]);
			matrix[2,3] = (((matrix1[2,0] * matrix2[0,3]) + (matrix1[2,1] * matrix2[1,3])) + (matrix1[2,2] * matrix2[2,3])) + (matrix1[2,3] * matrix2[3,3]);
			matrix[3,0] = (((matrix1[3,0] * matrix2[0,0]) + (matrix1[3,1] * matrix2[1,0])) + (matrix1[3,2] * matrix2[2,0])) + (matrix1[3,3] * matrix2[3,0]);
			matrix[3,1] = (((matrix1[3,0] * matrix2[0,1]) + (matrix1[3,1] * matrix2[1,1])) + (matrix1[3,2] * matrix2[2,1])) + (matrix1[3,3] * matrix2[3,1]);
			matrix[3,2] = (((matrix1[3,0] * matrix2[0,2]) + (matrix1[3,1] * matrix2[1,2])) + (matrix1[3,2] * matrix2[2,2])) + (matrix1[3,3] * matrix2[3,2]);
			matrix[3,3] = (((matrix1[3,0] * matrix2[0,3]) + (matrix1[3,1] * matrix2[1,3])) + (matrix1[3,2] * matrix2[2,3])) + (matrix1[3,3] * matrix2[3,3]);
			

        	return matrix;
			
		}


		private static readonly float[] vectorOut = new float[4];
		private static readonly float[] vectorIn = new float[4];

		public static Vector operator *(Vector vector, Matrix matrix)
		{
			Vector retval = Vector.Transform(vector, matrix);
				//matrix.ApplyToVector(vector);
			return retval;
		}

        public static explicit operator Vector(Matrix Matrix)
        {
            return new Vector( Matrix[3, 0], Matrix[3, 1], Matrix[3, 2]) ;
        }


		/*
		public Vector ApplyToVector(Vector position)
		{
			Vector outVector = new Vector();
			//Create a new vector and make it 4d homogenous
			vectorIn[0] = position.X;
			vectorIn[1] = position.Y;
			vectorIn[2] = position.Z;
			vectorIn[3] = 1.0d;

			for (int i = 0; i < 4; i++)
			{
				vectorOut[i] = 0.0d;
				for (int j = 0; j < 4; j++)
				{
					vectorOut[i] += vectorIn[j] * this[i, j];
				}
			}

			//Unhomogenize the result vector and return a 3d vector
			outVector.X = vectorOut[0]/vectorOut[3];
			outVector.Y = vectorOut[1]/vectorOut[3];
			outVector.Z = vectorOut[2]/vectorOut[3];

			return outVector;
		}
		 * */



        public static Matrix Identity
        {
			get 
			{
				Matrix identityMatrix = new Matrix();
				SetIdentity(identityMatrix);
				return identityMatrix;
			}
        }

		public static Matrix CreateLookAt(Vector cameraPosition, Vector cameraTarget, Vector cameraUpVector)
		{
			Matrix matrix = Matrix.Identity;
			Vector vector = Vector.Normalize(cameraPosition - cameraTarget);
			Vector vector2 = Vector.Normalize(Vector.Cross(cameraUpVector, vector));
			Vector vector3 = Vector.Cross(vector, vector2);
			matrix[0,0] = vector2.X;
			matrix[0,1] = vector3.X;
			matrix[0,2] = vector.X;
			matrix[0,3] = 0f;
			matrix[1,0] = vector2.Y;
			matrix[1,1] = vector3.Y;
			matrix[1,2] = vector.Y;
			matrix[1,3] = 0f;
			matrix[2,0] = vector2.Z;
			matrix[2,1] = vector3.Z;
			matrix[2,2] = vector.Z;
			matrix[2,3] = 0f;
			matrix[3,0] = -Vector.Dot(vector2, cameraPosition);
			matrix[3,1] = -Vector.Dot(vector3, cameraPosition);
			matrix[3,2] = -Vector.Dot(vector, cameraPosition);
			matrix[3,3] = 1f;
			return matrix;
		}

		public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			Matrix matrix = new Matrix(); //Matrix.Identity);
			float num = 1f / (float)System.Math.Tan(fieldOfView * 0.5);
			float num9 = num / aspectRatio;
			matrix[0, 0] = num9;
			matrix[0, 1] = matrix[0, 2] = matrix[0, 3] = 0f;
			matrix[1, 1] = num;
			matrix[1, 0] = matrix[1, 2] = matrix[1, 3] = 0f;
			matrix[2, 0] = matrix[2, 1] = 0f;
			matrix[2, 2] = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			matrix[2, 3] = -1f;
			matrix[3, 0] = matrix[3, 1] = matrix[3, 3] = 0f;
			matrix[3, 2] = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
			return matrix;
		}

		public static Matrix CreateRotationX(float degrees)
		{
			Matrix matrix = new Matrix();

			float num2 = (float)System.Math.Cos(MathHelper.ToRadians(degrees));
			float num = (float)System.Math.Sin(MathHelper.ToRadians(degrees));
			matrix[0, 0] = 1f;
			matrix[0, 1] = 0f;
			matrix[0, 2] = 0f;
			matrix[0, 3] = 0f;
			matrix[1, 0] = 0f;
			matrix[1, 1] = num2;
			matrix[1, 2] = num;
			matrix[1, 3] = 0f;
			matrix[2, 0] = 0f;
			matrix[2, 1] = -num;
			matrix[2, 2] = num2;
			matrix[2, 3] = 0f;
			matrix[3, 0] = 0f;
			matrix[3, 1] = 0f;
			matrix[3, 2] = 0f;
			matrix[3, 3] = 1f;
			return matrix;
		}
 
		public static Matrix CreateRotationY(float degrees)
		{
			Matrix matrix = new Matrix();
			float num2 = (float)System.Math.Cos(MathHelper.ToRadians(degrees));
			float num = (float)System.Math.Sin(MathHelper.ToRadians(degrees));
			matrix[0, 0] = num2;
			matrix[0, 1] = 0f;
			matrix[0, 2] = -num;
			matrix[0, 3] = 0f;
			matrix[1, 0] = 0f;
			matrix[1, 1] = 1f;
			matrix[1, 2] = 0f;
			matrix[1, 3] = 0f;
			matrix[2, 0] = num;
			matrix[2, 1] = 0f;
			matrix[2, 2] = num2;
			matrix[2, 3] = 0f;
			matrix[3, 0] = 0f;
			matrix[3, 1] = 0f;
			matrix[3, 2] = 0f;
			matrix[3, 3] = 1f;
			return matrix;
		}

		public static Matrix CreateRotationZ(float degrees)
		{
			Matrix matrix = new Matrix();
			float num2 = (float)System.Math.Cos(MathHelper.ToRadians(degrees));
			float num = (float)System.Math.Sin(MathHelper.ToRadians(degrees));

			matrix[0, 0] = num2;
			matrix[0, 1] = num;
			matrix[0, 2] = 0f;
			matrix[0, 3] = 0f;
			matrix[1, 0] = -num;
			matrix[1, 1] = num2;
			matrix[1, 2] = 0f;
			matrix[1, 3] = 0f;
			matrix[2, 0] = 0f;
			matrix[2, 1] = 0f;
			matrix[2, 2] = 1f;
			matrix[2, 3] = 0f;
			matrix[3, 0] = 0f;
			matrix[3, 1] = 0f;
			matrix[3, 2] = 0f;
			matrix[3, 3] = 1f;
			return matrix;
		}


		public static Matrix CreateTranslation(Vector position)
		{
			Matrix matrix = new Matrix();
			matrix[0, 0] = 1f;
			matrix[0, 1] = 0f;
			matrix[0, 2] = 0f;
			matrix[0, 3] = 0f;
			matrix[1, 0] = 0f;
			matrix[1, 1] = 1f;
			matrix[1, 2] = 0f;
			matrix[1, 3] = 0f;
			matrix[2, 0] = 0f;
			matrix[2, 1] = 0f;
			matrix[2, 2] = 1f;
			matrix[2, 3] = 0f;
			matrix[3, 0] = position.X;
			matrix[3, 1] = position.Y;
			matrix[3, 2] = position.Z;
			matrix[3, 3] = 1f;
			return matrix;
		}


		public static Matrix CreateScale(Vector scales)
		{
			Matrix matrix = Matrix.Identity;
			float x = scales.X;
			float y = scales.Y;
			float z = scales.Z;
			matrix[0,0] = x;
			matrix[0,1] = 0f;
			matrix[0,2] = 0f;
			matrix[0,3] = 0f;
			matrix[1,0] = 0f;
			matrix[1,1] = y;
			matrix[1,2] = 0f;
			matrix[1,3] = 0f;
			matrix[2,0] = 0f;
			matrix[2,1] = 0f;
			matrix[2,2] = z;
			matrix[2,3] = 0f;
			matrix[3,0] = 0f;
			matrix[3,1] = 0f;
			matrix[3,2] = 0f;
			matrix[3,3] = 1f;
			return matrix;
		}

 


		public override string ToString()
		{
			string format = "[ {0:##.##} - {1:##.##} - {2:##.##} - {3:##.##} ])";
			string row1 = string.Format(format,
										this[0, 0], this[0, 1], this[0, 2], this[0, 3]
				);
			string row2 = string.Format(format,
										this[1, 0], this[1, 1], this[1, 2], this[1, 3]
				);
			string row3 = string.Format(format,
										this[2, 0], this[2, 1], this[2, 2], this[2, 3]
				);
			string row4 = string.Format(format,
										this[3, 0], this[3, 1], this[3, 2], this[3, 3]
				);



			return string.Format("{0}\n{1}\n{2}\n{3}\n",
			                     row1,
			                     row2,
			                     row3,
			                     row4);
		}
 

	}
}

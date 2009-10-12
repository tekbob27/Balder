namespace Balder.Core.SoftwareRendering
{
	public struct InterpolationPoint
	{
		public float StartValue;
		public float EndValue;
		public float SecondaryStartValue;
		public float SecondaryEndValue;

		public int SecondarySpreadStart;
		public bool UseSecondary;

		public float Delta;
		public float AddValue;
		public float Current;

		public float[] InterpolatedValues;
	}

	public class Interpolator
	{
		public InterpolationPoint[] Points;

		public void SetNumberOfInterpolationPoints(int count)
		{
			Points = new InterpolationPoint[count];
		}

		public void InitializePoints(int spreadCount)
		{
			for( var index=0; index<Points.Length; index++ )
			{
				if (null == Points[index].InterpolatedValues || 
					Points[index].InterpolatedValues.Length < spreadCount)
				{
					Points[index].InterpolatedValues = new float[spreadCount];
				}
			}
		}


		public void Interpolate(int spreadCount)
		{
			InitializePoints(spreadCount);

			for (var index = 0; index < Points.Length; index++)
			{
				Points[index].Delta = Points[index].EndValue - Points[index].StartValue;
				if (Points[index].UseSecondary )
				{
					Points[index].AddValue = Points[index].Delta / (float)Points[index].SecondarySpreadStart;
				} else
				{
					Points[index].AddValue = Points[index].Delta / (float)spreadCount;	
				}
				
				Points[index].Current = Points[index].StartValue;
			}

			for( var spreadIndex=0; spreadIndex<spreadCount; spreadIndex++ )
			{
				for (var pointIndex = 0; pointIndex < Points.Length; pointIndex++)
				{
					Points[pointIndex].InterpolatedValues[spreadIndex] = Points[pointIndex].Current;
					if (Points[pointIndex].UseSecondary )
					{
						if( spreadIndex == Points[pointIndex].SecondarySpreadStart )
						{
							Points[pointIndex].Delta = Points[pointIndex].SecondaryEndValue - Points[pointIndex].SecondaryStartValue;
							Points[pointIndex].AddValue = Points[pointIndex].Delta / (float)(spreadCount - Points[pointIndex].SecondarySpreadStart);
							Points[pointIndex].Current = Points[pointIndex].SecondaryStartValue;
							
						}
					}
					Points[pointIndex].Current += Points[pointIndex].AddValue;
				}
			}
		}

		public void SetPoint(int pointIndex,float start, float end)
		{
			Points[pointIndex].UseSecondary = false;
			Points[pointIndex].StartValue = start;
			Points[pointIndex].EndValue = end;
		}

		public void SetPoint(int pointIndex, float start, float end, float secondaryStart, float secondaryEnd, int secondarySpreadStart)
		{
			Points[pointIndex].UseSecondary = true;
			Points[pointIndex].StartValue = start;
			Points[pointIndex].EndValue = end;
			Points[pointIndex].SecondaryStartValue = secondaryStart;
			Points[pointIndex].SecondaryEndValue = secondaryEnd;
			Points[pointIndex].SecondarySpreadStart = secondarySpreadStart;
		}

	}
}

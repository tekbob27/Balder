using Balder.Core.Math;
using NUnit.Framework;

namespace Balder.Core.Tests.Math
{
	[TestFixture]
	public class PlaneTests
	{
		[Test]
		public void GettingDistanceFromPlaneShouldReturnCorrectDistance()
		{
			var plane = new Plane();
			var vector1 = new Vector(-100, -100, 100);
			var vector2 = new Vector(100, -100, 100);
			var vector3 = new Vector(0, -100, 0);
			plane.SetVectors(vector1,vector2,vector3);

			var vectorToTest = new Vector(0, -200, 0);

			var length = plane.GetDistanceFromVector(vectorToTest);
			Assert.That(length,Is.EqualTo(-100f));
		}
	}
}

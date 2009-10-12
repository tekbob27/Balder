using Balder.Core.Math;
using Balder.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balder.Core.Tests.Math
{
	[TestClass]
	public class PlaneTests
	{
		[TestMethod]
		public void GettingDistanceFromPlaneShouldReturnCorrectDistance()
		{
			var plane = new Plane();
			var vector1 = new Vector(-100, -100, 100);
			var vector2 = new Vector(100, -100, 100);
			var vector3 = new Vector(0, -100, 0);
			plane.SetVectors(vector1,vector2,vector3);

			var vectorToTest = new Vector(0, -200, 0);

			var length = plane.GetDistanceFromVector(vectorToTest);
			length.ShouldBe(-100f);
		}
	}
}

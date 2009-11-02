using Balder.Core.Math;
using NUnit.Framework;

namespace Balder.Core.Tests.Math
{
	[TestFixture]
	public class BoundingSphereTests
	{
		[Test]
		public void TransformingWithOnlyTranslationShouldPositionSphereCorrectly()
		{
			var boundingSphere = new BoundingSphere(Vector.Zero, 1);
			var position = new Vector(10, 5, 3);
			var matrix = Matrix.CreateTranslation(position);
			var transformedBoundingSphere = boundingSphere.Transform(matrix);
			Assert.That(transformedBoundingSphere.Center, Is.EqualTo(position));
		}

		[Test]
		public void TransormingWith90DegreesRotationAroundYAndTranslationShouldPositionSphereCorrectly()
		{
			var boundingSphere = new BoundingSphere(Vector.Zero, 1);
			var position = new Vector(10, 5, 3);
			var translationMatrix = Matrix.CreateTranslation(position);
			var rotationMatrix = Matrix.CreateRotationY(90f);
			var matrix = translationMatrix * rotationMatrix;
			var transformedBoundingSphere = boundingSphere.Transform(matrix);
			var expectedPosition = position*rotationMatrix;
			Assert.That(transformedBoundingSphere.Center, Is.EqualTo(expectedPosition));
		}
	}
}

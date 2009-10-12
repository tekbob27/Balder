using System;

namespace Balder.Core.Runtime
{
	[Flags]
	public enum DebugLevel
	{
		Geometry,
		FaceNormals,
		VertexNormals,
		Lights,
		BoundingBoxes,
		BoundingSpheres
	}

	public static class DebugLevelExtensions
	{
		public static bool IsGeometrySet(this DebugLevel debugLevel)
		{
			return debugLevel.IsFlagSet(DebugLevel.Geometry);
		}

		public static bool IsFaceNormalsSet(this DebugLevel debugLevel)
		{
			return debugLevel.IsFlagSet(DebugLevel.FaceNormals);
		}

		private static bool IsFlagSet(this DebugLevel debugLevel, DebugLevel desiredFlag)
		{
			return (debugLevel & desiredFlag) == desiredFlag;
		}
	}
}

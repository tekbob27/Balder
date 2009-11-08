using Balder.Core.Execution;

namespace Balder.Silverlight.Services
{
	public static class TargetDevice
	{
		public static void Initialize()
		{
		}

		public static T Initialize<T>()
			where T:Game
		{
			return null;
		}
	}
}

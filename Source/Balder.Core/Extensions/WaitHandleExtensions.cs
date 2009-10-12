using System.Threading;

namespace Balder.Core.Extensions
{
	public static class WaitHandleExentions
	{
		public static void ResetAll(this ManualResetEvent[] waitHandles)
		{
			foreach (var waitHandle in waitHandles)
			{
				waitHandle.Reset();
			}
		}

		public static void SetAll(this ManualResetEvent[] waitHandles)
		{
			foreach (var waitHandle in waitHandles)
			{
				waitHandle.Set();
			}
		}
	}
}

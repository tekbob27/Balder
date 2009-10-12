namespace Balder.Core.Utils
{
	public static class AssemblyHelper
	{
		public static string GetAssemblyShortName(string fullName)
		{
			var index = fullName.IndexOf(',');
			var shortName = fullName.Substring(0, index);
			return shortName;
		}

	}
}

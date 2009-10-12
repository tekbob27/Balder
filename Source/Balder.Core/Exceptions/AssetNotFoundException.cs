using System;

namespace Balder.Core.Exceptions
{
	public class AssetNotFoundException : ArgumentException
	{
		public AssetNotFoundException(string asset)
			: base("Asset '"+asset+"' could not be found")
		{
			
		}
	}
}

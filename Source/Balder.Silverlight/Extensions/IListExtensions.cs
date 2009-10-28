using System.Collections;
using System.Collections.Generic;

namespace Balder.Silverlight.Extensions
{
	public static class IListExtensions
	{
		public static void AddRange(this IList list, IEnumerable range)
		{
			foreach (var item in range)
			{
				list.Add(item);
			}
		}

		public static void AddRange<T>(this IList<T> list, IEnumerable<T> range)
		{
			foreach( var item in range )
			{
				list.Add(item);
			}
		}
	}
}
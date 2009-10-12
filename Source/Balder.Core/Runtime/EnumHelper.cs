using System;
using System.Collections.Generic;
using System.Linq;

namespace Balder.Core.Runtime
{
	public static class EnumHelper
	{
		public static T[] GetValues<T>()
		{
			var enumType = typeof(T);

			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
			}

			var values = new List<T>();

			var fields = from field in enumType.GetFields()
						 where field.IsLiteral
						 select field;

			foreach (var field in fields)
			{
				var value = field.GetValue(enumType);
				values.Add((T)value);
			}

			return values.ToArray();
		}

		public static object[] GetValues(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
			}

			var values = new List<object>();

			var fields = from field in enumType.GetFields()
						 where field.IsLiteral
						 select field;

			foreach (var field in fields)
			{
				var value = field.GetValue(enumType);
				values.Add(value);
			}

			return values.ToArray();
		}
	}
}

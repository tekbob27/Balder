using System;
using System.ComponentModel;

namespace Balder.Silverlight.Converters
{
	public class StringToTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType.Equals(typeof (string));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			var typeName = value as string;

			if( string.IsNullOrEmpty(typeName))
			{
				return null;
			}

			return Type.GetType(typeName);
		}
	}
}

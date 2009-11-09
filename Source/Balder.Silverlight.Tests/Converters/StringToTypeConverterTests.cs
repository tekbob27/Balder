using System;
using System.Globalization;
using Balder.Silverlight.Converters;
using NUnit.Framework;

namespace Balder.Silverlight.Tests.Converters
{
	[TestFixture]
	public class StringToTypeConverterTests
	{
		[Test]
		public void UsingValidTypeStringShouldReturnTheTypeRepresentedInString()
		{
			var typeString = typeof (StringToTypeConverterTests).AssemblyQualifiedName;
			var converter = new StringToTypeConverter();
			var type = converter.ConvertFrom(null, CultureInfo.InvariantCulture, typeString) as Type;
			Assert.That(type,Is.Not.Null);
			Assert.That(type.AssemblyQualifiedName, Is.EqualTo(typeString));
		}

		[Test]
		public void UsingInvalidTypeStringShouldReturnNull()
		{
			var typeString = Guid.NewGuid().ToString();
			var converter = new StringToTypeConverter();
			var type = converter.ConvertFrom(null, CultureInfo.InvariantCulture, typeString) as Type;
			Assert.That(type,Is.Null);
		}
		
		[Test]
		public void UsingEmtpyStringShouldReturnNull()
		{
			var typeString = string.Empty;
			var converter = new StringToTypeConverter();
			var type = converter.ConvertFrom(null, CultureInfo.InvariantCulture, typeString) as Type;
			Assert.That(type, Is.Null);
 		}
	}
}

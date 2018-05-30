using System;
using System.Globalization;
using System.Reflection;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class DateTimeOffsetBuilder : SampleBuilderBase
	{
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = property.PropertyType == typeof(DateTimeOffset);
			return canHandle;
		}

		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			string sortableDate = (string)sampleValue(attribute);
			DateTimeOffset value = DateTimeOffset.ParseExact(sortableDate, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
			return value;
		}
	}
}
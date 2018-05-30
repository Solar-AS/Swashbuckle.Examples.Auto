using System;
using System.Globalization;
using System.Reflection;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class DateTimeBuilder : SampleBuilderBase
	{
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = property.PropertyType == typeof(DateTime);
			return canHandle;
		}

		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			string sortableDate = (string)sampleValue(attribute);
			DateTime value = DateTime.ParseExact(sortableDate, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
				.ToUniversalTime();
			return value;
		}
	}
}
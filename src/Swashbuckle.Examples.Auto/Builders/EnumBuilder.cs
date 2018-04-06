using System;
using System.Linq;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class EnumBuilder : SampleBuilderBase
	{
		protected override bool CanHandle(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = Reflect.IsEnum(property.PropertyType);
			return canHandle;
		}

		protected override object GetSampleValue(CustomAttributeData attribute, PropertyInfo property)
		{
			object @enum = Enum.ToObject(property.PropertyType, sampleValue(attribute));
			return @enum;
		}
	}
}
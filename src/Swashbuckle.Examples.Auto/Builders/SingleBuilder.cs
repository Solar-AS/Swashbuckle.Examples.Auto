using System;
using System.Linq;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class SingleBuilder : SampleBuilderBase
	{
		protected override bool CanHandle(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = !property.PropertyType.IsArray &&
							 !Reflect.IsList(property.PropertyType) &&
							 !Reflect.IsEnum(property.PropertyType) &&
							 !Reflect.IsNullable(property.PropertyType) &&
							 property.PropertyType != typeof(DateTime) &&
							 property.PropertyType != typeof(DateTimeOffset);
			return canHandle;
		}

		protected override object GetSampleValue(CustomAttributeData attribute, PropertyInfo property)
		{
			object value = Convert.ChangeType(attribute.ConstructorArguments.First().Value, property.PropertyType);
			return value;
		}
	}
}
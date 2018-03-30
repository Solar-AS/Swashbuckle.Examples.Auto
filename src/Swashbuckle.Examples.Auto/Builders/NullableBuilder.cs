using System;
using System.Linq;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class NullableBuilder : SampleBuilderBase
	{
		protected override bool CanHandle(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = Reflect.IsNullable(property.PropertyType);
			return canHandle;
		}

		protected override object GetSampleValue(CustomAttributeData attribute, PropertyInfo property)
		{
			Type underlying = Nullable.GetUnderlyingType(property.PropertyType);
			Type nullableType = typeof(Nullable<>).MakeGenericType(underlying);
			object argumentValue = attribute.ConstructorArguments.First().Value;
			object value = null;
			if (argumentValue != null)
			{
				object sample = Convert.ChangeType(argumentValue, underlying);
				value = Activator.CreateInstance(nullableType, sample);
			}
			return value;
		}
	}
}
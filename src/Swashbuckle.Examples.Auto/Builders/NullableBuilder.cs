using System;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class NullableBuilder : SampleBuilderBase
	{
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = Reflect.IsNullable(property.PropertyType);
			return canHandle;
		}

		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			Type underlying = Nullable.GetUnderlyingType(property.PropertyType);
			Type nullableType = typeof(Nullable<>).MakeGenericType(underlying);
			object argumentValue = sampleValue(attribute);
			object value = argumentValue != null ?
				Activator.CreateInstance(nullableType, argumentValue.As(underlying)) :
				null;
			return value;
		}
	}
}
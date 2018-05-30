using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class EnumBuilder : SampleBuilderBase
	{
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = Reflect.IsEnum(property.PropertyType);
			return canHandle;
		}

		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			object value = sampleValue(attribute);

			object @enum = specified(value) ?
				Enum.ToObject(property.PropertyType, value) :
				// set default (not very optimized)
				Activator.CreateInstance(property.PropertyType);
			
			return @enum;
		}

		private bool specified(object value)
		{
			var specified = !(value is ReadOnlyCollection<CustomAttributeTypedArgument>);
			return specified;
		}
	}
}
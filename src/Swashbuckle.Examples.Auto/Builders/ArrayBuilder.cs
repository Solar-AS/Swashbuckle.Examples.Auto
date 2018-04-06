using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class ArrayBuilder : SampleBuilderBase
	{
		protected override bool CanHandle(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = Reflect.IsArray(property.PropertyType);
			return canHandle;
		}

		protected override object GetSampleValue(CustomAttributeData attribute, PropertyInfo property)
		{
			Type arrayType = property.PropertyType.GetElementType();
			Array array;

			object sampleValue = attribute.ConstructorArguments.First().Value;

			// multiple items
			if (sampleValue is ReadOnlyCollection<CustomAttributeTypedArgument> collectionValue)
			{
				array = Array.CreateInstance(arrayType, collectionValue.Count);
				Array.Copy(collectionValue.Select(v => Convert.ChangeType(v.Value, arrayType)).ToArray(), array, array.Length);
			}
			// one item
			else
			{
				array = Array.CreateInstance(arrayType, 1);
				Array.Copy(new[] { Convert.ChangeType(sampleValue, arrayType) }, array, 1);
			}

			return array;
		}
	}
}
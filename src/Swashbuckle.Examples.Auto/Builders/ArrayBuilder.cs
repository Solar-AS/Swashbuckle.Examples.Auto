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
			bool canHandle = property.PropertyType.IsArray;
			return canHandle;
		}

		protected override object GetSampleValue(CustomAttributeData attribute, PropertyInfo property)
		{
			Type arrayType = property.PropertyType.GetElementType();
			Array array;

			object value = sampleValue(attribute);

			// multiple items
			if (value is ReadOnlyCollection<CustomAttributeTypedArgument> collectionValue)
			{
				array = Array.CreateInstance(arrayType, collectionValue.Count);
				Array.Copy(collectionValue.Select(v => v.Value.As(arrayType)).ToArray(), array, array.Length);
			}
			// one item
			else
			{
				array = Array.CreateInstance(arrayType, 1);
				Array.Copy(new[] { value.As(arrayType) }, array, 1);
			}

			return array;
		}
	}
}
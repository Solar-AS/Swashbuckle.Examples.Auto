using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	public class ListBuilder : SampleBuilderBase
	{
		protected override bool CanHandle(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = Reflect.IsList(property.PropertyType);
			return canHandle;
		}

		protected override object GetSampleValue(CustomAttributeData attribute, PropertyInfo property)
		{
			Type typeOfList = property.PropertyType.GenericTypeArguments[0];
			Type genericList = typeof(List<>).MakeGenericType(typeOfList);
			IList list = (IList)Activator.CreateInstance(genericList);
			
			object value = sampleValue(attribute);

			// a collection of multiple items
			if (value is ReadOnlyCollection<CustomAttributeTypedArgument> collectionValue)
			{
				foreach (CustomAttributeTypedArgument o in collectionValue)
				{
					list.Add(o.Value.As(typeOfList));
				}
			}
			// a collection of one item
			else
			{
				list.Add(value.As(typeOfList));
			}

			return list;
		}
	}
}
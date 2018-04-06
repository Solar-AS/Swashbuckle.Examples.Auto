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
			var listType = typeof(List<>).MakeGenericType(property.PropertyType.GenericTypeArguments);
			IList list = (IList)Activator.CreateInstance(listType);

			Type generic = property.PropertyType.GenericTypeArguments.First();
			object sampleValue = attribute.ConstructorArguments.First().Value;

			// a collection of multiple items
			if (sampleValue is ReadOnlyCollection<CustomAttributeTypedArgument> collectionValue)
			{
				foreach (CustomAttributeTypedArgument o in collectionValue)
				{
					object typed = Convert.ChangeType(o.Value, generic);
					list.Add(typed);
				}
			}
			// a collection of one item
			else
			{
				object typed = Convert.ChangeType(sampleValue, generic);
				list.Add(typed);
			}

			return list;
		}
	}
}
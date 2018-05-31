using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	/// <summary>
	/// Builds <see cref="List{T}"/> properties given the values provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class ListBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is a generic list, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canBuild = Reflect.IsList(property.PropertyType);
			return canBuild;
		}

		/// <summary>
		/// Builds a list based on the values specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>A lsit instance with the sample values.</returns>
		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
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
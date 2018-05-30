using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	
	/// <summary>
	/// Builds <see cref="Array"/> properties given the values provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class ArrayBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is an array, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = property.PropertyType.IsArray;
			return canHandle;
		}

		/// <summary>
		/// Builds an array based on the values specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>An array instance with the sample values.</returns>
		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
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
		// ReSharper restore AssignNullToNotNullAttribute
	}
}
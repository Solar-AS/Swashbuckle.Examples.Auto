using System;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	/// <summary>
	/// Builds nullable properties given the value provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class NullableBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is nullable, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canBuild = Reflect.IsNullable(property.PropertyType);
			return canBuild;
		}

		/// <summary>
		/// Builds a nullable based on the value specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <example>
		/// <code>
		/// [Sample(null)]
		/// public char? Optional { get; set; }
		///
		/// [Sample('x')]
		/// public char? AnotherOptional { get; set; }
		/// </code>
		/// </example>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>A nullable with the sample value.</returns>
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
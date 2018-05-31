using System;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	/// <summary>
	/// Builds scalar properties given the value provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class ScalarBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is a simple scalar, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canBuild = !property.PropertyType.IsArray &&
							 !Reflect.IsList(property.PropertyType) &&
							 !Reflect.IsEnum(property.PropertyType) &&
							 !Reflect.IsNullable(property.PropertyType) &&
							 property.PropertyType != typeof(DateTime) &&
							 property.PropertyType != typeof(DateTimeOffset);
			return canBuild;
		}

		/// <summary>
		/// Builds a scalar based on the value specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <example>
		/// <code>
		/// [Sample("1")]
		/// public int Compatible { get; set; }
		///
		/// [Sample('a')]
		/// public char Scalar { get; set; }
		/// </code>
		/// </example>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>A scalar with the sample value.</returns>
		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			object value = sampleValue(attribute).As(property.PropertyType);
			return value;
		}
	}
}
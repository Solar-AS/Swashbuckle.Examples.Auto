using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Builders
{
	/// <summary>
	/// Builds enum properties given the value provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class EnumBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is an enum, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canBuild = Reflect.IsEnum(property.PropertyType);
			return canBuild;
		}

		/// <summary>
		/// Builds an enum based on the value specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <remarks>The value must be specified either as an enum or as a numeric value
		/// </remarks>
		/// <example>
		/// <code>
		/// [SampleAttribute("2018-05-30T20:56:00")]
		/// public DateTimeOffset OneDay { get; set; }
		/// </code>
		/// </example>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>An enumeration with the sample value.</returns>
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
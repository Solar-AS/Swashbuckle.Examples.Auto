using System;
using System.Globalization;
using System.Reflection;

namespace Swashbuckle.Examples.Auto.Builders
{
	/// <summary>
	/// Builds <see cref="DateTime"/> properties given the value provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class DateTimeBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is a <see cref="DateTime"/>, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canHandle = property.PropertyType == typeof(DateTime);
			return canHandle;
		}

		/// <summary>
		/// Builds an instant in time based on the value specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <remarks>The value must be specified in the sortable (<c>"s"</c>) format.</remarks>
		/// <example>
		/// [SampleAttribute("2018-05-30T20:56:00")]
		/// public DateTime MyDate { get; set; }
		/// </example>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>An datetime instance with the sample values.</returns>
		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			string sortableDate = (string)sampleValue(attribute);
			DateTime value = DateTime.ParseExact(sortableDate, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
				.ToUniversalTime();
			return value;
		}
	}
}
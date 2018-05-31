using System;
using System.Globalization;
using System.Reflection;

namespace Swashbuckle.Examples.Auto.Builders
{
	/// <summary>
	/// Builds <see cref="DateTimeOffset"/> properties given the value provided in the decorating <see cref="SampleAttribute"/> attribute.
	/// </summary>
	public class DateTimeOffsetBuilder : SampleBuilderBase
	{
		/// <summary>
		/// Specifies whether a given builder can be used to build a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the property is a <see cref="DateTimeOffset"/>, <c>false</c> otherwise.</returns>
		protected override bool CanBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			bool canBuild = property.PropertyType == typeof(DateTimeOffset);
			return canBuild;
		}

		/// <summary>
		/// Builds a point in time based on the value specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <remarks>The value must be specified in the sortable (<c>"s"</c>) format.
		/// <para>The instance is returned as UTC.</para>
		/// </remarks>
		/// <example>
		/// [SampleAttribute(DayOfWeek.Saturday)]
		/// public DayOfWeek OneDay { get; set; }
		/// 
		/// [SampleAttribute(6)]
		/// public DayOfWeek AnotherDay { get; set; }
		/// </example>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>A <see cref="DateTimeOffset"/> instance with the sample value.</returns>
		protected override object DoBuild(CustomAttributeData attribute, PropertyInfo property)
		{
			string sortableDate = (string)sampleValue(attribute);
			DateTimeOffset value = DateTimeOffset.ParseExact(sortableDate, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
			return value;
		}
	}
}
using System;

namespace Swashbuckle.Examples.Auto
{
	/// <summary>
	/// Specifies the scalar value (or collection of scalar values) of the decorated property to be included in the sample documentation of the containing model.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SampleAttribute : Attribute
	{
		/// <summary>
		/// Specifies the scalar value of the decorated property in the sample documentation.
		/// </summary>
		/// <param name="value">Scalar value for the sample of the property.</param>
		/// <example>
		/// <code>
		/// [Sample(42)]
		/// public int PropertyName { get; set; }
		/// </code>
		/// </example>
		public SampleAttribute(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Specifies the scalar values of the decorated collection property in the sample documentation.
		/// </summary>
		/// <param name="values">Collection of scalar values for the sample of the property.</param>
		/// <remarks>Use this constructor to specify a collection of scalar values for a property that is a collection of scalar values.
		/// <para>If the property is a collection of complex types, it should not be decorated.</para>
		/// </remarks>
		/// <example>
		/// <code>
		/// [Sample("A", "B", "C")]
		/// public List&lt;string&gt; PropertyName { get; set; }
		/// </code>
		/// </example>
		public SampleAttribute(params object[] values)
		{
			Values = values;
		}

		/// <summary>
		/// Scalar value for the sample of the property.
		/// </summary>
		public object Value { get; }

		/// <summary>
		/// Collection of scalar values for the sample of the property.
		/// </summary>
		public object[] Values { get; }
	}
}
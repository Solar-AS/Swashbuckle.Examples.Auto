using System;

namespace Swashbuckle.Examples.Auto
{
	/// <summary>
	/// Specifies that the class should provide a sample in its documentation.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class SamplifyAttribute : Attribute
	{
		/// <summary>
		/// Initializes an instance of <see cref="SamplifyAttribute"/> with default properties.
		/// </summary>
		/// <remarks>Use when the type of the sample to be provided corresponds to the decorated class.</remarks>
		public SamplifyAttribute() { }

		/// <summary>
		/// Initializes an instance of <see cref="SamplifyAttribute"/> with a type.
		/// </summary>
		/// <param name="typeOfSample">The type of the sample to be provided.</param>
		/// <remarks>Allows creating a different sample from the decorated class.</remarks>
		/// <example>
		/// <code>
		/// [Samplify]
		/// public class Subject
		/// {
		/// 	public string NotDecoratedScalar { get; set; }
		/// 
		/// 	public Exception NotDecoratedObject { get; set; }
		/// 
		/// 	[Sample('a')]
		/// 	public char Scalar { get; set; }
		/// 
		/// 	[Sample(null)]
		/// 	public int? NullNullable { get; set; }
		/// 
		/// 	[Sample(8)]
		/// 	public byte? NotNullNullable { get; set; }
		/// 
		/// 	[Sample("2018-04-06T21:39:00")]
		/// 	public DateTime DateTime { get; set; }
		/// 
		/// 	[Sample("2018-04-06T21:42:00")]
		/// 	public DateTimeOffset DateTimeOffset { get; set; }
		/// 
		/// 	[Sample(DayOfWeek.Friday)]
		/// 	public DayOfWeek Enum { get; set; }
		/// 
		/// 	[Sample("a", "b", "c")]
		/// 	public string[] Array { get; set; }
		/// 
		/// 	[Sample('1', '2', '3')]
		/// 	public List&lt;char&gt; List { get; set; }
		/// 
		/// 	public Nested NestedObject { get; set; }
		/// 
		/// 	public class Nested
		/// 	{
		/// 		[Sample("A")]
		/// 		public string Scalar { get; set; }
		/// 
		/// 		[Sample(-1)]
		/// 		public long? Nullable { get; set; }
		/// 
		/// 		[Sample("2018-04-06T22:58:00")]
		/// 		public DateTimeOffset Date { get; set; }
		/// 
		/// 		[Sample(DayOfWeek.Friday)]
		/// 		public DayOfWeek Enum { get; set; }
		/// 
		/// 		[Sample("a", "b", "c")]
		/// 		public string[] Array { get; set; }
		/// 	}
		/// 
		/// 	public Nested[] ComplexArray { get; set; }
		/// }
		/// </code>
		/// </example>
		public SamplifyAttribute(Type typeOfSample)
		{
			TypeOfSample = typeOfSample;
		}

		/// <summary>
		/// The type of the sample to be provided.
		/// </summary>
		public Type TypeOfSample { get; }
	}
}
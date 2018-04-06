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
		/// <example>TODO </example>
		public SamplifyAttribute(Type typeOfSample)
		{
			TypeOfSample = typeOfSample;
		}

		public Type TypeOfSample { get; }
	}
}
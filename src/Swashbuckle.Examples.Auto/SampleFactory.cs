using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace Swashbuckle.Examples.Auto
{
	/// <summary>
	/// Provides object creation services for classes decorated with the <see cref="SamplifyAttribute"/> attribute.
	/// </summary>
	public class SampleFactory
	{
		private readonly IFixture _fixture;

		/// <summary>
		/// Creates an instance of the factory using the provided configuration.
		/// </summary>
		/// <param name="config">Configuration action for the <see cref="IFixture"/> instance used to create the instance.
		/// <para>If <c>null</c>, default configuration using out-of-the-box builders in the <see cref="Swashbuckle.Examples.Auto.Builders"/> namespace.</para></param>
		public SampleFactory(Action<IFixture> config = null)
		{
			_fixture = new Fixture();

			Action<IFixture> effectiveConfiguration = config ?? DefaultConfiguration;
			effectiveConfiguration(_fixture);
		}

		/// <summary>
		/// Default configuration for the provided <see cref="IFixture"/>.
		/// </summary>
		/// <remarks>Uses <see cref="ISpecimenBuilder"/> from the <see cref="Swashbuckle.Examples.Auto.Builders"/> namespace and sets the number
		/// of elements in collection to 1.</remarks>
		/// <param name="fixture">Instance to be customized with out-of-the-box <see cref="ISpecimenBuilder"/>.</param>
		public static void DefaultConfiguration(IFixture fixture)
		{
			fixture.RepeatCount = 1;

			fixture.Customizations.Add(new Builders.ListBuilder());
			fixture.Customizations.Add(new Builders.ArrayBuilder());
			fixture.Customizations.Add(new Builders.NullableBuilder());
			fixture.Customizations.Add(new Builders.EnumBuilder());
			fixture.Customizations.Add(new Builders.DateTimeBuilder());
			fixture.Customizations.Add(new Builders.DateTimeOffsetBuilder());
			fixture.Customizations.Add(new Builders.ScalarBuilder());
		}

		/// <summary>
		/// Creates a new specimen of the provided type.
		/// </summary>
		/// <remarks>
		/// An instance will be created if decorated with the <see cref="SamplifyAttribute"/> attribute. Otherwise, <c>null</c> will be returned.
		/// <para>
		/// The values for the properties will be:
		/// <list type="bullet">
		/// <item><description>A sample value if the property is decorated with the <see cref="SampleAttribute"/>
		/// and can be built by one of the configured <see cref="ISpecimenBuilder"/> of the <see cref="IFixture"/>.</description></item>
		/// <item><description>A random value according to the configured <see cref="ISpecimenBuilder"/> of the <see cref="IFixture"/>.</description></item>
		/// </list>
		/// </para>
		/// </remarks>
		/// <param name="type">The type that specifies what to create.</param>
		/// <returns>An instace of the provided type with its properties set to samples if decorated, <c>null</c> otherwise.</returns>
		public object BuildSample(Type type)
		{
			object sample = null;

			var attribute = type.GetTypeInfo().GetCustomAttribute<SamplifyAttribute>();
			if (attribute != null)
			{
				// allow create a different type from the decorated one
				Type typeOfSample = attribute.TypeOfSample ?? type;

				sample = _fixture.Create(typeOfSample, new SpecimenContext(_fixture));
			}

			return sample;
		}
	}
}
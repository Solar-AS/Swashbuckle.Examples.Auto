using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace Swashbuckle.Examples.Auto
{
	public class SampleFactory
	{
		private readonly IFixture _fixture;
		public SampleFactory(Action<IFixture> config = null)
		{
			_fixture = new Fixture();

			Action<IFixture> effectiveConfiguration = config ?? DefaultConfiguration;
			effectiveConfiguration(_fixture);
		}

		public static void DefaultConfiguration(IFixture fixture)
		{
			fixture.RepeatCount = 1;

			fixture.Customizations.Add(new Builders.ListBuilder());
			fixture.Customizations.Add(new Builders.ArrayBuilder());
			fixture.Customizations.Add(new Builders.NullableBuilder());
			fixture.Customizations.Add(new Builders.EnumBuilder());
			fixture.Customizations.Add(new Builders.DateTimeBuilder());
			fixture.Customizations.Add(new Builders.DateTimeOffsetBuilder());
			fixture.Customizations.Add(new Builders.SingleBuilder());
		}

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
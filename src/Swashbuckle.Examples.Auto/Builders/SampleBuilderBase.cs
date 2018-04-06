using System.Linq;
using System.Reflection;
using AutoFixture.Kernel;

namespace Swashbuckle.Examples.Auto.Builders
{
	public abstract class SampleBuilderBase
	{
		protected static readonly NoSpecimen NoOp = new NoSpecimen();

		public object Create(object request, ISpecimenContext context)
		{
			object value = NoOp;

			PropertyInfo property = request as PropertyInfo;
			CustomAttributeData attribute = property?.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(SampleAttribute));
			if (attribute != null && CanHandle(attribute, property))
			{
				value = GetSampleValue(attribute, property);
			}
			return value;
		}

		protected abstract bool CanHandle(CustomAttributeData attribute, PropertyInfo property);
		protected abstract object GetSampleValue(CustomAttributeData attribute, PropertyInfo property);

		protected object sampleValue(CustomAttributeData attribute)
		{
			// regardless whether there is one or many arguments, it is always attribute.ConstructorArguments[0]
			object sampleValue = attribute.ConstructorArguments[0].Value;
			return sampleValue;
		}
	}
}
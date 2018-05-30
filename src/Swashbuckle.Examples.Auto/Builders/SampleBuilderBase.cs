using System.Linq;
using System.Reflection;
using AutoFixture.Kernel;

namespace Swashbuckle.Examples.Auto.Builders
{
	
	/// <summary>
	/// Provides the abstract base class for building properties decorated with the <see cref="SampleAttribute"/> attribute. 
	/// </summary>
	public abstract class SampleBuilderBase : ISpecimenBuilder
	{
		protected static readonly NoSpecimen NoOp = new NoSpecimen();

		/// <inheritdoc />
		public object Create(object request, ISpecimenContext context)
		{
			object value = NoOp;

			PropertyInfo property = request as PropertyInfo;
			CustomAttributeData attribute = property?.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(SampleAttribute));
			if (attribute != null && CanBuild(attribute, property))
			{
				value = DoBuild(attribute, property);
			}
			return value;
		}

		/// <summary>
		/// Specifies whether a given builder can be used to create a property or not.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns><c>true</c> if the builder can be used to build the instance of the property, <c>false</c> otherwise</returns>
		protected abstract bool CanBuild(CustomAttributeData attribute, PropertyInfo property);

		/// <summary>
		/// Builds a suitable property value based on the value specified in the <see cref="SampleAttribute"/> attribute.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <param name="property">The <see cref="PropertyInfo"/> representing the decorated property.</param>
		/// <returns>An instance of type of the property according to the value specified in the decoration.</returns>
		protected abstract object DoBuild(CustomAttributeData attribute, PropertyInfo property);

		/// <summary>
		/// Gets the value of the attribute.
		/// </summary>
		/// <param name="attribute">Attribute data of the decorated property.</param>
		/// <returns>The value of the attribute</returns>
		protected object sampleValue(CustomAttributeData attribute)
		{
			// regardless whether there is one or many arguments, it is always attribute.ConstructorArguments[0]
			object sampleValue = attribute.ConstructorArguments[0].Value;
			return sampleValue;
		}
	}
}
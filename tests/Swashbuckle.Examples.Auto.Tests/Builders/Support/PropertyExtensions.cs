using System.Reflection;

namespace Swashbuckle.Examples.Auto.Tests.Builders.Support
{
	internal static class PropertyExtensions
	{
		public static PropertyInfo Property(this object container, string propertyName)
		{
			PropertyInfo property = container.GetType().GetProperty(propertyName);
			return property;
		}
	}
}
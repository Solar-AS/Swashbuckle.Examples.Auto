using System;
using System.Collections;
using System.Reflection;

namespace Swashbuckle.Examples.Auto.Builders.Support
{
	internal static class Reflect
	{
		public static bool IsEnum(Type t)
		{
			bool isEnum = t.GetTypeInfo().IsEnum;
			return isEnum;
		}

		public static bool IsList(Type t)
		{
			TypeInfo info = t.GetTypeInfo();
			bool isList = info.IsGenericType && !info.IsGenericTypeDefinition && typeof(IList).IsAssignableFrom(t);
			return isList;
		}

		public static bool IsNullable(Type t)
		{
			TypeInfo info = t.GetTypeInfo();
			bool isNullable = info.IsGenericType &&
			                  !info.IsGenericTypeDefinition &&
			                  info.GetGenericTypeDefinition() == typeof(Nullable<>);
			return isNullable;
		}
	}
}
﻿using System;

namespace Swashbuckle.Examples.Auto.Builders.Support
{
	internal static class ConversionExtensions
	{
		public static object As(this object value, Type convertionType)
		{
			object converted = Convert.ChangeType(value, convertionType);
			return converted;
		}
	}
}
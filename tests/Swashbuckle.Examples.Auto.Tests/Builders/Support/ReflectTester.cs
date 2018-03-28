using NUnit.Framework;
using Swashbuckle.Examples.Auto.Builders.Support;

namespace Swashbuckle.Examples.Auto.Tests.Builders.Support
{
	[TestFixture]
	public class ReflectTester
	{
		[Test]
		public void IsEnum_Enum_True()
		{
			Assert.That(Reflect.IsEnum(typeof(System.AttributeTargets)), Is.True);
		}

		[Test]
		public void IsEnum_NotEnum_False()
		{
			Assert.That(Reflect.IsEnum(typeof(System.Action)), Is.False);
		}

		[Test]
		public void IsList_OpenGenericList_False()
		{
			Assert.That(Reflect.IsList(typeof(System.Collections.Generic.List<>)), Is.False);
		}

		[Test]
		public void IsList_ClosedGenericList_True()
		{
			Assert.That(Reflect.IsList(typeof(System.Collections.Generic.List<int>)), Is.True);
		}

		[Test]
		public void IsList_Array_False()
		{
			Assert.That(Reflect.IsList(typeof(int[])), Is.False);
			Assert.That(Reflect.IsList(typeof(System.Array)), Is.False);
		}

		[Test]
		public void IsNullable_OpenNullable_False()
		{
			Assert.That(Reflect.IsNullable(typeof(System.Nullable<>)), Is.False);
		}

		[Test]
		public void IsNullable_ClosedNullable_True()
		{
			Assert.That(Reflect.IsNullable(typeof(int?)), Is.True);
		}
	}
}
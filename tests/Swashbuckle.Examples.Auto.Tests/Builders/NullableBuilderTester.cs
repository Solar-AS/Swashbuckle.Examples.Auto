using System.Reflection;
using AutoFixture.Kernel;
using NUnit.Framework;
using Swashbuckle.Examples.Auto.Builders;
using Swashbuckle.Examples.Auto.Tests.Builders.Support;

namespace Swashbuckle.Examples.Auto.Tests.Builders
{
	[TestFixture]
	public class NullableBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new NullableBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}
		// ReSharper disable UnusedAutoPropertyAccessor.Global
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		public int NotDecorated { get; set; }

		[Sample('a')]
		public char NotNullable { get; set; }

		[Sample(null)]
		public char? NullableNull { get; set; }

		[Sample('x')]
		public char? NullableNotNull { get; set; }

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore UnusedAutoPropertyAccessor.Global

		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new NullableBuilder();

			PropertyInfo notDecorated = this.Property(nameof(NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_NotNullable_NoOp()
		{
			var subject = new NullableBuilder();

			PropertyInfo notNullable = this.Property(nameof(NotNullable));
			var noOp = subject.Create(notNullable, null);
			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_NullableNull_Null()
		{
			var subject = new NullableBuilder();

			PropertyInfo nullableNull = this.Property(nameof(NullableNull));
			var @null = subject.Create(nullableNull, null);
			Assert.That(@null, Is.Null);
		}

		[Test]
		public void Create_NullableNotNull_NotNull()
		{
			var subject = new NullableBuilder();

			PropertyInfo nullableNotNull = this.Property(nameof(NullableNotNull));
			var notNull = subject.Create(nullableNotNull, null);
			Assert.That(notNull, Is.InstanceOf(nullableNotNull.PropertyType).And.Not.Null);
		}
	}
}
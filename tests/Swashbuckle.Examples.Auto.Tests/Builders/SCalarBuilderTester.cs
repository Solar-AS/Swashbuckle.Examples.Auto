using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFixture.Kernel;
using NUnit.Framework;
using Swashbuckle.Examples.Auto.Builders;
using Swashbuckle.Examples.Auto.Tests.Builders.Support;

namespace Swashbuckle.Examples.Auto.Tests.Builders
{
	[TestFixture]
	public class ScalarBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new ScalarBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}


		// ReSharper disable UnusedAutoPropertyAccessor.Global
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		public int NotDecorated { get; set; }

		[Sample("a", "b")]
		public List<string> Unhandled { get; set; }

		[Sample("a")]
		public int TypeMismatch { get; set; }

		[Sample("1")]
		public int CompatibleConversion { get; set; }

		[Sample('a')]
		public char ExactType { get; set; }

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore UnusedAutoPropertyAccessor.Global


		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new ScalarBuilder();

			PropertyInfo notDecorated = this.Property(nameof(NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyOfUnsupportedType_NoOp()
		{
			var subject = new ScalarBuilder();
			PropertyInfo unhandledType = this.Property(nameof(Unhandled));

			var noOp = subject.Create(unhandledType, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyTypeMissmatch_Exception()
		{
			var subject = new ScalarBuilder();
			PropertyInfo missmatch = this.Property(nameof(TypeMismatch));

			Assert.That(() => subject.Create(missmatch, null), Throws.InstanceOf<FormatException>());
		}

		[Test]
		public void Create_CompatibleConversion_Instance()
		{
			var subject = new ScalarBuilder();
			PropertyInfo compatible = this.Property(nameof(CompatibleConversion));
			object instance = subject.Create(compatible, null);
			Assert.That(instance, Is.InstanceOf(compatible.PropertyType));
		}

		[Test]
		public void Create_ExactConversion_Instance()
		{
			var subject = new ScalarBuilder();
			PropertyInfo exact = this.Property(nameof(ExactType));
			object instance = subject.Create(exact, null);
			Assert.That(instance, Is.InstanceOf(exact.PropertyType));
		}
	}
}
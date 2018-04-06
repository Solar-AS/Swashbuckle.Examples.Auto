using System;
using System.Reflection;
using AutoFixture.Kernel;
using NUnit.Framework;
using Swashbuckle.Examples.Auto.Builders;
using Swashbuckle.Examples.Auto.Tests.Builders.Support;

namespace Swashbuckle.Examples.Auto.Tests.Builders
{
	[TestFixture]
	public class EnumBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new EnumBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}


		// ReSharper disable UnusedAutoPropertyAccessor.Global
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		public int NotDecorated { get; set; }

		[Sample("whatever")]
		public byte NotEnum { get; set; }

		[Sample(DayOfWeek.Saturday)]
		public DayOfWeek EnumValue { get; set; }

		[Sample(6)]
		public DayOfWeek NumericValue { get; set; }

		[Sample("Saturday")]
		public DayOfWeek TextualValue { get; set; }

		[Sample("6")]
		public DayOfWeek TextualNumericValue { get; set; }

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore UnusedAutoPropertyAccessor.Global


		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new EnumBuilder();

			PropertyInfo notDecorated = this.Property(nameof(NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_NotAnEnum_NoOp()
		{
			var subject = new EnumBuilder();
			PropertyInfo notEnum = this.Property(nameof(NotEnum));

			var noOp = subject.Create(notEnum, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_EnumValue_Instance()
		{
			var subject = new EnumBuilder();
			PropertyInfo enumValue = this.Property(nameof(EnumValue));

			object created = subject.Create(enumValue, null);
			Assert.That(created, Is.EqualTo(DayOfWeek.Saturday));
		}

		[Test]
		public void Create_EnumNumericValue_Instance()
		{
			var subject = new EnumBuilder();
			PropertyInfo numericValue = this.Property(nameof(NumericValue));
			object instance = subject.Create(numericValue, null);
			Assert.That(instance, Is.EqualTo(DayOfWeek.Saturday));
		}

		[Test]
		public void Create_TextualValue_Exception()
		{
			var subject = new EnumBuilder();
			PropertyInfo textualValue = this.Property(nameof(TextualValue));
			Assert.That(() => subject.Create(textualValue, null), Throws.ArgumentException);
		}

		[Test]
		public void Create_TextualNumericValue_Exception()
		{
			var subject = new EnumBuilder();
			PropertyInfo textualValue = this.Property(nameof(TextualNumericValue));
			Assert.That(() => subject.Create(textualValue, null), Throws.ArgumentException);
		}
	}
}
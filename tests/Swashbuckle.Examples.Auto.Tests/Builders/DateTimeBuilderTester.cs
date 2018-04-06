using System;
using System.Reflection;
using AutoFixture.Kernel;
using NUnit.Framework;
using Swashbuckle.Examples.Auto.Builders;
using Swashbuckle.Examples.Auto.Tests.Builders.Support;

namespace Swashbuckle.Examples.Auto.Tests.Builders
{
	[TestFixture]
	public class DateTimeBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new DateTimeBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}
		// ReSharper disable UnusedAutoPropertyAccessor.Global
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		public int NotDecorated { get; set; }

		[Sample('a')]
		public char NotADate { get; set; }

		[Sample("01-01-2018")]
		public DateTime WrongFormat { get; set; }

		[Sample("2018-03-30T14:09:00")]
		public DateTime RightFormat { get; set; }

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore UnusedAutoPropertyAccessor.Global

		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new DateTimeBuilder();

			PropertyInfo notDecorated = this.Property(nameof(NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_NotADate_NoOp()
		{
			var subject = new DateTimeBuilder();

			PropertyInfo notADate = this.Property(nameof(NotADate));
			var noOp = subject.Create(notADate, null);
			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_WrongFormat_Exception()
		{
			var subject = new DateTimeBuilder();

			PropertyInfo wrongFormat = this.Property(nameof(WrongFormat));
			Assert.That(()=> subject.Create(wrongFormat, null), Throws.InstanceOf<FormatException>());
		}

		[Test]
		public void Create_SortableFormat_Date()
		{
			var subject = new DateTimeBuilder();

			PropertyInfo rightFormat = this.Property(nameof(RightFormat));
			var date = subject.Create(rightFormat, null);
			Assert.That(date, Is.InstanceOf(rightFormat.PropertyType));
		}

		[Test]
		public void Create_SortableFormat_Utc()
		{
			var subject = new DateTimeBuilder();

			PropertyInfo rightFormat = this.Property(nameof(RightFormat));
			var utc = (DateTime)subject.Create(rightFormat, null);
			Assert.That(utc.Kind, Is.EqualTo(DateTimeKind.Utc));
		}
	}
}
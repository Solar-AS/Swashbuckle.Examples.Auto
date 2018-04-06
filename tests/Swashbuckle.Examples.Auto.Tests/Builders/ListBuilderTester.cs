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
	public class ListBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new ListBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}


		// ReSharper disable UnusedAutoPropertyAccessor.Global
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		public List<int> NotDecorated { get; set; }

		[Sample("a")]
		public string Unhandled { get; set; }

		[Sample("a")]
		public List<int> TypeMismatch { get; set; }

		[Sample('a')]
		public List<char> SingleItem { get; set; }

		[Sample('a', 'b')]
		public List<char> MultipleItems { get; set; }

		[Sample("1")]
		public List<int> CompatibleConversion { get; set; }

		[Sample]
		public List<int> Empty { get; set; }

		[Sample(1, 2, 3)]
		public int[] Array { get; set; }

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore UnusedAutoPropertyAccessor.Global


		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new ListBuilder();

			PropertyInfo notDecorated = this.Property(nameof(NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyOfUnsupportedType_NoOp()
		{
			var subject = new ListBuilder();
			PropertyInfo unhandledType = this.Property(nameof(Unhandled));

			var noOp = subject.Create(unhandledType, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyTypeMissmatch_Exception()
		{
			var subject = new ListBuilder();
			PropertyInfo missmatch = this.Property(nameof(TypeMismatch));

			Assert.That(() => subject.Create(missmatch, null), Throws.InstanceOf<FormatException>());
		}

		[Test]
		public void Create_SingleItem_Instance()
		{
			var subject = new ListBuilder();
			PropertyInfo singleItem = this.Property(nameof(SingleItem));
			object instance = subject.Create(singleItem, null);
			Assert.That(instance, Is.InstanceOf(singleItem.PropertyType).And.Count.EqualTo(1));
		}

		[Test]
		public void Create_MultipleItems_Instance()
		{
			var subject = new ListBuilder();
			PropertyInfo multipleItems = this.Property(nameof(MultipleItems));
			object instance = subject.Create(multipleItems, null);
			Assert.That(instance, Is.InstanceOf(multipleItems.PropertyType).And.Count.GreaterThan(1));
		}

		[Test]
		public void Create_CompatibleConversion_Instance()
		{
			var subject = new ListBuilder();
			PropertyInfo compatible = this.Property(nameof(CompatibleConversion));
			object instance = subject.Create(compatible, null);
			Assert.That(instance, Is.InstanceOf(compatible.PropertyType));
		}

		[Test]
		public void Create_NoValues_EmptyInstance()
		{
			var subject = new ListBuilder();
			PropertyInfo empty = this.Property(nameof(Empty));
			object instance = subject.Create(empty, null);
			Assert.That(instance, Is.InstanceOf(empty.PropertyType).And.Empty);
		}

		[Test]
		public void Create_Array_NoOp()
		{
			var subject = new ListBuilder();
			PropertyInfo array = this.Property(nameof(Array));
			object noOp = subject.Create(array, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

	}
}
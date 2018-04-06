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
	public class ArrayBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new ArrayBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}


		// ReSharper disable UnusedAutoPropertyAccessor.Global
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		public List<int> NotDecorated { get; set; }

		[Sample("a")]
		public List<string> Unhandled { get; set; }

		[Sample("a")]
		public int[] TypeMismatch { get; set; }

		[Sample('a')]
		public char[] SingleItem { get; set; }

		[Sample('a', 'b')]
		public char[] MultipleItems { get; set; }

		[Sample("1")]
		public int[] CompatibleSingleConversion { get; set; }

		[Sample("1", "2")]
		public int[] CompatibleMultiConversion { get; set; }

		[Sample]
		public int[] Empty { get; set; }

		[Sample(1, 2, 3)]
		public List<int> List { get; set; }

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore UnusedAutoPropertyAccessor.Global


		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new ArrayBuilder();

			PropertyInfo notDecorated = this.Property(nameof(NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyOfUnsupportedType_NoOp()
		{
			var subject = new ArrayBuilder();
			PropertyInfo unhandledType = this.Property(nameof(Unhandled));

			var noOp = subject.Create(unhandledType, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyTypeMissmatch_Exception()
		{
			var subject = new ArrayBuilder();
			PropertyInfo missmatch = this.Property(nameof(TypeMismatch));

			Assert.That(() => subject.Create(missmatch, null), Throws.InstanceOf<FormatException>());
		}

		[Test]
		public void Create_SingleItem_Instance()
		{
			var subject = new ArrayBuilder();
			PropertyInfo singleItem = this.Property(nameof(SingleItem));
			object instance = subject.Create(singleItem, null);
			Assert.That(instance, Is.InstanceOf(singleItem.PropertyType).And.Length.EqualTo(1));
		}

		[Test]
		public void Create_MultipleItems_Instance()
		{
			var subject = new ArrayBuilder();
			PropertyInfo multipleItems = this.Property(nameof(MultipleItems));
			object instance = subject.Create(multipleItems, null);
			Assert.That(instance, Is.InstanceOf(multipleItems.PropertyType).And.Length.GreaterThan(1));
		}

		[Test]
		public void Create_CompatibleSingleConversion_Instance()
		{
			var subject = new ArrayBuilder();
			PropertyInfo compatible = this.Property(nameof(CompatibleSingleConversion));
			object instance = subject.Create(compatible, null);
			Assert.That(instance, Is.InstanceOf(compatible.PropertyType));
		}

		[Test]
		public void Create_CompatibleMultiConversion_Instance()
		{
			var subject = new ArrayBuilder();
			PropertyInfo compatible = this.Property(nameof(CompatibleMultiConversion));
			object instance = subject.Create(compatible, null);
			Assert.That(instance, Is.InstanceOf(compatible.PropertyType));
		}

		[Test]
		public void Create_NoValues_EmptyInstance()
		{
			var subject = new ArrayBuilder();
			PropertyInfo empty = this.Property(nameof(Empty));
			object instance = subject.Create(empty, null);
			Assert.That(instance, Is.InstanceOf(empty.PropertyType).And.Empty);
		}

		[Test]
		public void Create_List_NoOp()
		{
			var subject = new ArrayBuilder();
			PropertyInfo array = this.Property(nameof(List));
			object noOp = subject.Create(array, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}
	}
}
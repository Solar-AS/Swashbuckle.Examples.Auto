using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFixture.Kernel;
using NUnit.Framework;
using Swashbuckle.Examples.Auto.Builders;

namespace Swashbuckle.Examples.Auto.Tests.Builders
{
	[TestFixture]
	public class SingleBuilderTester
	{
		[Test]
		public void Create_NotAProperty_NoOp()
		{
			var subject = new SingleBuilder();
			object notAProperty = typeof(int);
			var noOp = subject.Create(notAProperty, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		// ReSharper disable MemberCanBePrivate.Local
		// ReSharper disable UnusedAutoPropertyAccessor.Local
		class C
		{
			public int NotDecorated { get; set; }
			
			[Sample("a", "b")]
			public List<string> Unhandled { get; set; }

			[Sample("a")]
			public int TypeMismatch { get; set; }

			[Sample("1")]
			public int CompatibleConversion { get; set; }

			[Sample('a')]
			
			public char ExactType { get; set; }

			internal static PropertyInfo Prop(string name)
			{
				PropertyInfo property = typeof(C).GetProperty(name);
				return property;
			}
		}
		// ReSharper restore MemberCanBePrivate.Local
		// ReSharper restore UnusedAutoPropertyAccessor.Local

		[Test]
		public void Create_PropertyNotDecorated_NoOp()
		{
			var subject = new SingleBuilder();

			PropertyInfo notDecorated = typeof(C).GetProperty(nameof(C.NotDecorated));
			var noOp = subject.Create(notDecorated, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyOfUnsupportedType_NoOp()
		{
			var subject = new SingleBuilder();
			PropertyInfo unhandledType = C.Prop(nameof(C.Unhandled));

			var noOp = subject.Create(unhandledType, null);

			Assert.That(noOp, Is.InstanceOf<NoSpecimen>());
		}

		[Test]
		public void Create_PropertyTypeMissmatch_Exception()
		{
			var subject = new SingleBuilder();
			PropertyInfo missmatch = C.Prop(nameof(C.TypeMismatch));

			Assert.That(()=> subject.Create(missmatch, null), Throws.InstanceOf<FormatException>());
		}

		[Test]
		public void Create_CompatibleConversion_Instance()
		{
			var subject = new SingleBuilder();
			PropertyInfo compatible = C.Prop(nameof(C.CompatibleConversion));
			object instance = subject.Create(compatible, null);
			Assert.That(instance, Is.InstanceOf(compatible.PropertyType));
		}

		[Test]
		public void Create_ExactConversion_Instance()
		{
			var subject = new SingleBuilder();
			PropertyInfo exact = C.Prop(nameof(C.ExactType));
			object instance = subject.Create(exact, null);
			Assert.That(instance, Is.InstanceOf(exact.PropertyType));
		}
	}
}
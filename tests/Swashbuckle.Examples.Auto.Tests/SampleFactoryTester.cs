using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NUnit.Framework;

namespace Swashbuckle.Examples.Auto.Tests
{
	[TestFixture]
	public class SampleFactoryTester
	{
		[Test]
		public void BuildSample_UndecoratedType_Null()
		{
			var subject = new SampleFactory();

			var @null = subject.BuildSample(typeof(string));

			Assert.That(@null, Is.Null);
		}

		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global
		// ReSharper disable UnusedAutoPropertyAccessor.Global
		[Samplify]
		public class Subject
		{
			public string NotDecoratedScalar { get; set; }

			public Exception NotDecoratedObject { get; set; }

			[Sample('a')]
			public char Scalar { get; set; }

			[Sample(null)]
			public int? NullNullable { get; set; }

			[Sample(8)]
			public byte? NotNullNullable { get; set; }

			[Sample("2018-04-06T21:39:00")]
			public DateTime DateTime { get; set; }

			[Sample("2018-04-06T21:42:00")]
			public DateTimeOffset DateTimeOffset { get; set; }

			[Sample(DayOfWeek.Friday)]
			public DayOfWeek Enum { get; set; }
			
			[Sample("a", "b", "c")]
			public string[] Array { get; set; }

			[Sample('1', '2', '3')]
			public List<char> List { get; set; }

			public Nested NestedObject { get; set; }

			public class Nested
			{
				[Sample("A")]
				public string Scalar { get; set; }

				[Sample(-1)]
				public long? Nullable { get; set; }

				[Sample("2018-04-06T22:58:00")]
				public DateTimeOffset Date { get; set; }

				[Sample(DayOfWeek.Friday)]
				public DayOfWeek Enum { get; set; }

				[Sample("a", "b", "c")]
				public string[] Array { get; set; }
			}

			public Nested[] ComplexArray { get; set; }
		}
		// ReSharper restore UnusedAutoPropertyAccessor.Global
		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global

		[Test]
		public void BuildSample_DecoratedType_Instance()
		{
			var subject = new SampleFactory();

			var notNull = subject.BuildSample(typeof(Subject));

			Assert.That(notNull, Is.InstanceOf<Subject>());
		}

		[Test]
		public void BuildSample_UndecoratedProperties_AutofixtureRamdom()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.NotDecoratedScalar, Is.InstanceOf<string>().And.Not.Null);
			Assert.That(built.NotDecoratedObject, Is.InstanceOf<Exception>().And.Not.Null);
		}

		[Test]
		public void BuildSample_Scalar_AsInDecoration()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.Scalar, Is.EqualTo('a'));
		}

		[Test]
		public void BuildSample_Nullables_AsInDecoration()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.NullNullable, Is.Null);
			Assert.That(built.NotNullNullable, Is.EqualTo(8));
		}

		[Test]
		public void BuildSample_Dates_AsInDecoration()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.DateTime, Is.EqualTo(new DateTime(2018, 04, 06, 21, 39, 00, DateTimeKind.Utc)));
			Assert.That(built.DateTimeOffset, Is.EqualTo(new DateTimeOffset(2018, 04, 06, 21, 42, 00, TimeSpan.Zero)));
		}

		[Test]
		public void BuildSample_Enums_AsInDecoration()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.Enum, Is.EqualTo(DayOfWeek.Friday));
			Assert.That(built.DateTimeOffset, Is.EqualTo(new DateTimeOffset(2018, 04, 06, 21, 42, 00, TimeSpan.Zero)));
		}

		[Test]
		public void BuildSample_Collections_AsInDecoration()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.Array, Is.EqualTo(new[]{"a", "b", "c"}));
			Assert.That(built.List, Is.EqualTo(new []{'1', '2', '3'}));
		}

		[Test]
		public void BuildSample_Nested_AsInDecorations()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.NestedObject.Scalar, Is.EqualTo("A"));
			Assert.That(built.NestedObject.Nullable, Is.EqualTo(-1));
			Assert.That(built.NestedObject.Date, Is.EqualTo(new DateTimeOffset(2018, 04, 06, 22, 58, 00, TimeSpan.Zero)));
			Assert.That(built.NestedObject.Enum, Is.EqualTo(DayOfWeek.Friday));
			Assert.That(built.NestedObject.Array, Is.EqualTo(new[] { "a", "b", "c" }));
		}

		[Test]
		public void BuildSample_CollectionsOfObject_SingleItemCollectionAsPerDecorations()
		{
			var subject = new SampleFactory();

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.ComplexArray, Has.Length.EqualTo(1));

			Assert.That(built.ComplexArray[0].Scalar, Is.EqualTo("A"));
			Assert.That(built.ComplexArray[0].Nullable, Is.EqualTo(-1));
			Assert.That(built.ComplexArray[0].Date, Is.EqualTo(new DateTimeOffset(2018, 04, 06, 22, 58, 00, TimeSpan.Zero)));
			Assert.That(built.ComplexArray[0].Enum, Is.EqualTo(DayOfWeek.Friday));
			Assert.That(built.ComplexArray[0].Array, Is.EqualTo(new[] { "a", "b", "c" }));
		}

		[Test]
		public void BuildSample_CustomConfiguration_WholesaleReplacement()
		{
			var subject = new SampleFactory(f =>
			{
				f.RepeatCount = 2;
			});

			var built = (Subject) subject.BuildSample(typeof(Subject));

			// all collections will contain two elements
			Assert.That(built.Array, Has.Length.EqualTo(2));
			Assert.That(built.List, Has.Count.EqualTo(2));
			Assert.That(built.NestedObject.Array, Has.Length.EqualTo(2));
			Assert.That(built.ComplexArray, Has.Length.EqualTo(2));
			Assert.That(built.ComplexArray, Has.All
				.With.Property(nameof(Subject.Nested.Array))
				.With.Length.EqualTo(2));

			// but sample attribute is not observed
			Assert.That(built.Array, Is.Not.EqualTo(new[]{ "a", "b", "c" }));
		}

		[Test]
		public void BuildSample_CustomConfiguration_CanExtended()
		{
			Action<IFixture> composed = f =>
			{
				// normal configuration
				SampleFactory.DefaultConfiguration(f);
				// remove the ability to samplify arrays
				var indexOfArrayBuilder = f.Customizations.ToList().FindIndex(b => b is Auto.Builders.ArrayBuilder);
				f.Customizations.RemoveAt(indexOfArrayBuilder);
			};
			
			var subject = new SampleFactory(composed);

			var built = (Subject)subject.BuildSample(typeof(Subject));

			Assert.That(built.Scalar, Is.EqualTo('a'), ()=> "sample attribute should be observed");

			Assert.That(built.Array, Is.Not.EqualTo(new[] { "a", "b", "c" }), ()=> "sample attribute should not be observed for arrays");
		}
	}
}
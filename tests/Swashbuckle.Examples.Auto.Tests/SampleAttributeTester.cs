using System;
using NUnit.Framework;

namespace Swashbuckle.Examples.Auto.Tests
{
	[TestFixture]
	public class SampleAttributeTester
	{
		[Test]
		public void Attribute_ApplyToProperties()
		{
			Assert.That(typeof(SampleAttribute), Has.Attribute<AttributeUsageAttribute>()
				.With.Property(nameof(AttributeUsageAttribute.ValidOn)).EqualTo(AttributeTargets.Property));
		}

		[Test]
		public void Sample_Scalar_SetValue()
		{
			string scalar = "value";

			var subject = new SampleAttribute(scalar);

			Assert.That(subject.Value, Is.EqualTo(scalar));
			Assert.That(subject.Values, Is.Null);
		}

		[Test]
		public void Sample_Collection_SetValues()
		{
			var subject = new SampleAttribute(1, 2, 3);

			Assert.That(subject.Value, Is.Null);
			Assert.That(subject.Values, Is.EqualTo(new[] { 1, 2, 3 }));
		}
	}
}
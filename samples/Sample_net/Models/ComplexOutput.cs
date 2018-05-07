using System;
using Swashbuckle.Examples.Auto;

namespace Sample_net.Models
{
	public class ComplexOutput
	{
		[Sample("2018-07-05T00:00:00")]
		public DateTimeOffset D { get; set; }
		[Sample(1)]
		public byte B { get; set; }
		[Sample('Z')]
		public char C { get; set; }
	}
}
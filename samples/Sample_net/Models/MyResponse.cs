using Swashbuckle.Examples.Auto;

namespace Sample_net.Models
{
	[Samplify]
	public class MyResponse
	{
		[Sample(42f)]
		public float Simple { get; set; }
		public ComplexOutput Complex { get; set; }
	}
}
using Swashbuckle.Examples.Auto;

namespace Sample_netcore.Models
{
	[Samplify]
	public class MyRequest
	{
		[Sample("something")]
		public string Simple { get; set; }
		public ComplexInput Complex { get; set; }
	}
}
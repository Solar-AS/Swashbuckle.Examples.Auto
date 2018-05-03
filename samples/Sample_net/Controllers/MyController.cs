using System;
using System.Web.Http;
using Sample_net.Models;

namespace Sample_net.Controllers
{
	public class MyController : ApiController
	{
		public IHttpActionResult Get()
		{
			var response = new MyResponse
			{
				Simple = 1f,
				Complex = new ComplexOutput
				{
					C= 'a',
					B= 1,
					D= DateTimeOffset.UtcNow
				}
			};

			return Ok(response);
		}

		public IHttpActionResult Post([FromBody]MyRequest request)
		{
			if (!float.TryParse(request.Simple, out float simple)) simple = 0f;
			byte b = Convert.ToByte(request.Complex.I);
			var response = new MyResponse
			{
				Simple = simple,
				Complex = new ComplexOutput
				{

					C= request.Complex.C,
					B= b,
					D= DateTimeOffset.UtcNow
				}
			};

			return Ok(response);
		}
	}
}
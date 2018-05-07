using System;
using Microsoft.AspNetCore.Mvc;
using Sample_netcore.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sample_netcore.Controllers
{
	[Route("api/[controller]")]
	public class MyController : Controller
	{
		[HttpGet]
		[SwaggerResponse(200, typeof(MyResponse), "GET description")]
		public IActionResult Get()
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

		
		[HttpPost]
		[SwaggerResponse(200, typeof(MyResponse), "POST description")]
		public IActionResult Get([FromBody]MyRequest request)
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

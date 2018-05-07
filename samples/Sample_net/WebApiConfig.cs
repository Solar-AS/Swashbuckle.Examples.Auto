using System.Net.Http.Formatting;
using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Examples.Auto;

namespace Sample_net
{
	public class WebApiConfig
	{
		public static void Init(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"default",
				"api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });

			removeFormatters(
				config.Formatters.FormUrlEncodedFormatter,
				config.Formatters.XmlFormatter);

			config.EnableSwagger(cfg =>
			{
				cfg.SingleApiVersion("v1", "My API");
				var factory = new SampleFactory();
				cfg.SchemaFilter(()=> new SchemaSamplifier(factory));
			});
		}

		private static void removeFormatters(params MediaTypeFormatter[] formatters)
		{
			for (int i = 0; i < formatters.Length; i++)
			{
				formatters[i].SupportedMediaTypes.Clear();
			}
		}
	}
}
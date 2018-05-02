using System;
using System.Web;
using System.Web.Http;

namespace Sample_net
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			WebApiConfig.Init(GlobalConfiguration.Configuration);
		}
	}
}
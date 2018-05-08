using Microsoft.AspNetCore.Hosting;

namespace Sample_netcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
			new WebHostBuilder()
				.UseKestrel()
				.UseStartup<Startup>()
                .Build();
    }
}

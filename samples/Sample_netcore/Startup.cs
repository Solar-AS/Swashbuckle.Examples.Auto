using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Examples.Auto;

namespace Sample_netcore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
	        var builder = new ConfigurationBuilder()
		        .SetBasePath(env.ContentRootPath);
	        Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddMvcCore()
		        .AddJsonFormatters()
		        .AddApiExplorer();

	        services.AddSingleton(new SampleFactory());

	        services.AddSwaggerGen(opt =>
	        {
		        opt.SwaggerDoc("v1", new Info {Title = "my Api"});
		        opt.SchemaFilter<SchemaSamplifier>();
	        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
	        app.UseSwagger();
        }
    }
}

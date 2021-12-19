using API.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.RegisterServices(Configuration);
            services.AddCustomHealthChecks(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseApiConfiguration(env);
            app.UseCustomHealthCheckConfiguration();
            app.UseSwaggerConfiguration();

            if (!env.IsEnvironment("Testing"))
            {
              
                SerilogConfig.ConfigureSerilog(Configuration, loggerFactory);
            }         
        }
    }
}

using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestesIntegracao.Config
{
    public class ApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //remove a opçao atual de configuração e seta pra salvar em memoria
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ProjetoContext>));

                services.Remove(descriptor);

                services.AddDbContext<ProjetoContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProjetoContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<ApiFactory<TStartup>>>();

                db.Database.EnsureCreated();
            });

            //caso tenha um startup especifico para ser usado
            //builder.UseStartup<TStartup>();
            //builder.UseEnvironment("Testing");
        }
    }
}

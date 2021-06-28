using System;
using System.IO;
using CraftBeer.Api.Domain;
using CraftBeer.Api.Models;
using CraftBeer.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace CraftBeer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddSingleton(typeof(BeerDbContextFactory))
                .AddTransient(typeof(IRepository<Beer>), typeof(BeerRepository))
                .AddTransient(typeof(BeerService));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.ContentRootPath, "docs")),
                    RequestPath = "/api-docs",
                    ServeUnknownFileTypes = true
                });
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/api-docs/craftbeer-spec", "CraftBeer.Api v1");
                    c.RoutePrefix = "api-docs";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

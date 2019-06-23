using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AbrasNigEnt.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Repositories;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace AbrasNigEnt
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private  IHostingEnvironment HostingEnvironment { get; }
        public Startup(IConfiguration config, IHostingEnvironment hostingEnv)
        {
            Configuration = config;
            HostingEnvironment = hostingEnv;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), HostingEnvironment.WebRootPath)));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                Configuration["ConnectionStrings:DefaultConnection"]
                ));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            DbInitializer.SeedDatabase(app.ApplicationServices);

        }
    }
}

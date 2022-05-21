using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreEmptyExample.Repository;

namespace CoreEmptyExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //FOR DATABASE CONNECTION
            services.AddDbContext<BookModelContext>(
                //options => options.UseSqlServer("Server=PG02R1PW;Database=BookStore;Integrated Security=true;")
                );

            //FOR USING MVC
            services.AddControllersWithViews();

            //FOR RUNNING RAZOR PAGES AFTER CHANGING WITHOUT REBUILDING THE PROJECT
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(options=> 
            {
                options.HtmlHelperOptions.ClientValidationEnabled = false;//Disable client side validation in debug mode
            });
#endif

            services.AddScoped<BookModelRepo, BookModelRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //For files in wwwroot folder
            app.UseStaticFiles();

            //For Custom static folder
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/MyStaticFiles"
            //});

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

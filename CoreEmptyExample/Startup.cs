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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using CoreEmptyExample.Model;
using CoreEmptyExample.Service;

namespace CoreEmptyExample
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<BookModelContext>().AddDefaultTokenProviders();

            //FOR DATABASE CONNECTION HARDCODED
            //services.AddDbContext<BookModelContext>(
            //    //options => options.UseSqlServer("Server=PG02R1PW;Database=BookStore;Integrated Security=true;")
            //    );

            //FOR DATABASE CONNECTION FROM appsettings.json
            services.AddDbContext<BookModelContext>(options=>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //REDIRECT TO LOGIN PAGE IF NOT LOGGED IN
            services.ConfigureApplicationCookie(configure =>
            {
                configure.LoginPath = _configuration["Application: LoginPath"];
            });

            //FOR USING MVC
            services.AddControllersWithViews();

            //FOR RUNNING RAZOR PAGES AFTER CHANGING WITHOUT REBUILDING THE PROJECT
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(options=> 
            {
                options.HtmlHelperOptions.ClientValidationEnabled = false;//Disable client side validation in debug mode
            });
#endif

            services.AddScoped<IBookModelRepo, BookModelRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IUserClaimsPrincipalFactory<UserModel>, UserClaimsPrincipalFactory<UserModel>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            services.Configure<IdentityOptions>(options =>
            {
                //options.Password.RequireDigit = false;
                //options.Password.RequireUppercase = false;
                //AND MANY MORE...

                options.SignIn.RequireConfirmedEmail = true; ;

            });

            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
            
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}

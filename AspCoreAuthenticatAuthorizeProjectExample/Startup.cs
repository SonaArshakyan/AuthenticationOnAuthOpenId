using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspCoreAuthenticatAuthorizeProjectExample.Models;
using AspCoreAuthenticatAuthorizeProjectExample.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspCoreAuthenticatAuthorizeProjectExample
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
            services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));
            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            //services.AddRazorPages().AddMvcOptions(o => o.Filters.Add(new AuthorizeFilter()));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:connectionString"]));
            services.AddScoped<IUserRepository, UserRespository>();
            services.AddAuthentication( o => 
            { 
             o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddCookie(ExternalAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle
                (o =>
               {
                   o.SignInScheme = ExternalAuthenticationDefaults.AuthenticationScheme;
                   o.ClientId = Configuration["Google:ClientId"];
                   o.ClientSecret = Configuration["Google:ClientSecret"];
               });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

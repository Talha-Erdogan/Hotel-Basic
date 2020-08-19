using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business;
using Hotel.Business.Common;
using Hotel.Business.Ýnterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Hotel.Web
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
            services.AddControllersWithViews();
            services.AddSingleton<IConfiguration>(Configuration); //add Configuration to our services collection

            // session kullanýmý tanýmý
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache(); //This way ASP.NET Core will use a Memory Cache to store session variables
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            //Session için 
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });


            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IPersonnelService, PersonnelService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileDetailService, ProfileDetailService>();
            services.AddTransient<IProfilePersonnelService, ProfilePersonnelService>();
            services.AddTransient<IRoomService, RoomService>();

            //https://stackoverflow.com/questions/58885384/the-json-value-could-not-be-converted-to-system-nullablesystem-int32
            services.AddControllers().AddNewtonsoftJson();

            //http isteginde bulunmak için kullanýlýr.
            //https://docs.microsoft.com/tr-tr/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1
            services.AddHttpClient();


            //newtonsoft ekleme-ajax methodlarýnda verileri okuyamýyorduk
            //https://stackoverflow.com/questions/60535734/when-posting-to-an-asp-net-core-3-1-web-app-frombodymyclass-data-is-often-n
            services.AddRazorPages().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // session kullanýmý
            app.UseSession(); //make sure add this line before UseMvc()


            // SessionHelper'a HttpContextAccessor nesnesi ataniyor
            SessionHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());


            //config helper'ý configure etmek için
            ConfigHelper.Configure(Configuration);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}

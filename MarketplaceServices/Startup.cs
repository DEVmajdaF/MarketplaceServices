using MarketplaceServices.Data;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceServices.Hubs;
using MarketplaceServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MarketplaceServices
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

            services.AddDbContext<AuthDbContext>(options =>
                   options.UseSqlServer(
                       Configuration.GetConnectionString("Connection")));
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                          .AddRoles<IdentityRole>()
                          .AddEntityFrameworkStores<AuthDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddSessionStateTempDataProvider();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();



            services.AddScoped<IProfile, Profile>();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = (Newtonsoft.Json.ReferenceLoopHandling)Newtonsoft.Json.PreserveReferencesHandling.Objects);

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseIdentity();
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
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                //Le Nom du fichier Hub.
                //make this hub available for the clients 
                endpoints.MapHub<ChatHub>("/Chatter");
            });
        }
    }
}

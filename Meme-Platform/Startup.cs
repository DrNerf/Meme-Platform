using Meme_Platform.Attributes;
using Meme_Platform.Core;
using Meme_Platform.IL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Meme_Platform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public bool IsDevelopment { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            
            var mvcBuilder = services.AddRazorPages();
            if (IsDevelopment)
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            // Registers all inhouse services including DAL repos.
            services.Bootstrap(Configuration);
            services.BootstrapIntegrationLayer();

            // Register MVC services.
            services.AddTransient<ManageUserProfilesFilter>();
            services.AddTransient<DependencyInjectorFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IPluginStore pluginStore = app.ApplicationServices.GetService<IPluginStore>();
            var plugins = pluginStore.ScanForPlugins($"{Directory.GetCurrentDirectory()}/MPPlugins");
            foreach (var plugin in plugins)
            {
                pluginStore.RegisterPlugin(plugin);
            }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // Creates/Updates the database schema.
            app.SetupDB();
        }
    }
}

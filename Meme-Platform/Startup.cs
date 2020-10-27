using Meme_Platform.Attributes;
using Meme_Platform.Core;
using Meme_Platform.IL;
using Meme_Platform.IL.Events;
using Meme_Platform.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform
{
    public class Startup
    {

        private IEnumerable<IPlugin> plugins;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public bool IsDevelopment { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            plugins = AssemblyScanner.ScanForPlugins(
                $"{Directory.GetCurrentDirectory()}/MPPlugins",
                new SerilogLoggerProvider(Log.Logger).CreateLogger(nameof(AssemblyScanner)));

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

            // Plug all plugin assemblies into the MVC app parts, thus registering all plugin controllers.
            if (plugins.Any())
            {
                foreach (var assembly in plugins.Select(p => p.GetType().Assembly).Distinct())
                {
                    mvcBuilder.AddApplicationPart(assembly);
                }

                foreach (var plugin in plugins)
                {
                    plugin.ConfigureServices(services);
                }
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meme Platform", Version = "latest" });
            });

            // Registers all inhouse services including DAL repos.
            services.Bootstrap(Configuration);
            services.BootstrapIntegrationLayer();

            services.AddSingleton<IProxyService, ProxyService>((a) => new ProxyService(new SharedProxyOptions 
            {
                PrepareRequest = (request, message) => Task.Run(() => 
                {
                    // Make sure the proxy won't expose any AzureAD cookies.
                    message.Headers.Remove("Cookie");

                    var publicCookiesStrings = request.Cookies.Where(c => !c.Key.Contains("AzureAD", StringComparison.OrdinalIgnoreCase))
                        .Select(c => $"{c.Key}={c.Value}").ToList();
                    foreach (var cookie in publicCookiesStrings)
                    {
                        message.Headers.Add("Cookie", cookie);
                    }

                    // Set the origin header to avoid CORS errors.
                    message.Headers.Remove("Origin");
                    message.Headers.Add("Origin", $"{message.RequestUri.Scheme}://{message.RequestUri.Host}");
                    message.Headers.Remove("Referer");
                    message.Headers.Add(
                        "Referer",
                        $"{message.RequestUri.Scheme}://{message.RequestUri.Host}{message.RequestUri.AbsolutePath.Substring(1)}");

#if DEBUG
                    Log.Logger.Debug($"Proxying request: {request.GetEncodedPathAndQuery()} ->\n{message}");
#endif
                })
            }));

            // Register MVC services.
            services.AddTransient<ManageUserProfilesFilter>();
            services.AddTransient<DependencyInjectorFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var pluginStore = app.ApplicationServices.GetService<IPluginStore>();

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

                //IMGFLIP proxy
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy",
                    "proxy/imgflip/{*path}",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_get_le_data",
                    "ajax_get_le_data",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_s_meme",
                    "s/meme/{*path}",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_get_meme_recs",
                    "ajax_get_meme_recs",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_ajax_meme_search",
                    "ajax_meme_search_new",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_ajax_meme_done",
                    "ajax_meme_done_canvas",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_ajax_delete_creation",
                    "ajax_delete_creation",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_service_worker.js",
                    "service-worker.js",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapControllerRoute(
                    "ImgflipHttpProxy_offline",
                    "offline",
                    new { controller = "ImgFlip", action = "Raw" });
                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meme Platform");
            });

            // Creates/Updates the database schema.
            app.SetupDB();
        }
    }
}

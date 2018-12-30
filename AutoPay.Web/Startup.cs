using AutoPay.Configuration;
using AutoPay.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoPay.Web
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
            //add data context
            MiddlewareConfiguration.ConfigureEf(services, Configuration.GetConnectionString("DataConnection"));
            //configure identity
            MiddlewareConfiguration.ConfigureIdentity(services);
            //register unit of work
            MiddlewareConfiguration.ConfigureUoW(services);
            //register repositories
            MiddlewareConfiguration.ConfigureRepositories(services);
            //register managers
            MiddlewareConfiguration.ConfigureManagers(services);
            //register services
            MiddlewareConfiguration.ConfigureServices(services);
            //register mappers
            MiddlewareConfiguration.ConfigureAutoMapper(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMemoryCache();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });

            ConfigureAppSettings();
        }

        private void ConfigureAppSettings()
        {
            //getting section
            var section = Configuration.GetSection("AppSettings");
            //binding properties
            AppSettings.AppTitle = section.GetValue<string>("AppTitle");
            AppSettings.AppVersion = section.GetValue<string>("AppVersion");
            AppSettings.BaseUrl = section.GetValue<string>("BaseUrl");
        }
    }
}

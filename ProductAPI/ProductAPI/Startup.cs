using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using ProductAPI.Extensions;

namespace ProductAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetParent(Directory.GetCurrentDirectory()), "/LoggerService/config/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCORS();

            services.ConfigureIISIntegration();

            services.ConfigureLoggerService();

            services.ConfigureDbContext(Configuration);

            services.ConfigureRepositoryWrapper();

            services.AddMvc(cfg =>
                {
                    cfg.RespectBrowserAcceptHeader = true;
                    cfg.ReturnHttpNotAcceptable = true;
                    cfg.InputFormatters.Add(new XmlSerializerInputFormatter(cfg));
                    cfg.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //default value is 30 days.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("CORSPolicy");

            //Forward Proxy headers for the current request. Predominantly for linux deployment
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}

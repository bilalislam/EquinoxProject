using Equinox.Infra.CrossCutting.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Equinox.Infra.CrossCutting.Identity.Models;
using AutoMapper;
using Equinox.Infra.CrossCutting.Identity.Authorization;
using Equinox.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using Serilog.Events;

namespace Equinox.UI.Site
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                 .WriteTo.Elasticsearch().WriteTo.Elasticsearch(
                     new ElasticsearchSinkOptions(
                         new Uri(Configuration["ElasticsearchUrl"]))
                     {
                         MinimumLogEventLevel = LogEventLevel.Verbose,
                         AutoRegisterTemplate = true
                     })
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                });

            services.AddAutoMapper();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app,
                                      IHostingEnvironment env,
                                      ILoggerFactory loggerFactory,
                                      IHttpContextAccessor accessor)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=product}/{action=list-all}/{id?}");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}


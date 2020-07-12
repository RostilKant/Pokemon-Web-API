using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokemon_Web_API.Controllers;
using Repository;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Services;

namespace Pokemon_Web_API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors( opt =>
            {
                opt.AddPolicy("CorsPolicy", builder =>
                
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()); 
            });

        public static IServiceCollection ConfigureSerilogger(this IServiceCollection services)
        {
            var name = Assembly.GetExecutingAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", name.Name)
                .Enrich.WithProperty("Version", name.Version)
                .WriteTo.File(new RenderedCompactJsonFormatter(),"log.json")
                .CreateLogger();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);

        }
        
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts => 
                opts.UseNpgsql(configuration.GetConnectionString("sqlConnection"),
                    m => m.MigrationsAssembly("Pokemon-Web-API")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager,RepositoryManager>();
        
        public static void ConfigurePokemonService(this IServiceCollection services) =>
            services.AddScoped<IPokemonService, PokemonService>();
        public static void ConfigureTypeService(this IServiceCollection services) =>
            services.AddScoped<ITypeService, TypeService>();

        public static void ConfigureCustomMediaType(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters
                    .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                newtonsoftJsonOutputFormatter?.SupportedMediaTypes
                    .Add("application/vnd.rostil.hateoas+json");
                newtonsoftJsonOutputFormatter?.SupportedMediaTypes
                    .Add("application/vnd.rostil.apiroot+json");
                
                var xmlOutputFormatter = config.OutputFormatters
                    .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                xmlOutputFormatter?.SupportedMediaTypes
                    .Add("application/vnd.rostil.hateoas+xml");
                xmlOutputFormatter?.SupportedMediaTypes
                    .Add("application/vnd.rostil.apiroot+xml");
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1,0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
                opt.Conventions.Controller<PokemonsController>().HasApiVersion(new ApiVersion(1,0));
                opt.Conventions.Controller<PokemonsControllerV2>().HasDeprecatedApiVersion(new ApiVersion(2,0));
            });

    }
}
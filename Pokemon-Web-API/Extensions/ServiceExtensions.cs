using System;
using System.Reflection;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using Serilog.Events;
using Serilog.Formatting.Compact;

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
        
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new
                CsvOutputFormatter()));


    }
}
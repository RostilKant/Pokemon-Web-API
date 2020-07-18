using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using AutoMapper;
using Contracts;
using Entities.Models;
using HttpServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.Net.Http.Headers;
using Pokemon_Web_API.ActionFilters;
using Pokemon_Web_API.Extensions;
using Repository.DataShaping;
using Serilog;
using RestSharp;
using Services.Utility;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Pokemon_Web_API
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
            services.ConfigureCors();
            services.ConfigureSerilogger();
            services.AddLogging(builder => builder.AddSerilog());
            services.AddControllers(config =>
                {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                    config.CacheProfiles.Add("Public120", new CacheProfile{Duration = 120, Location = ResponseCacheLocation.Any});
                })
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(typeof(Startup));

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigurePokemonService();
            services.ConfigureTypeService();
            services.ConfigureUserService();

            services.AddScoped<ModelValidationFilterAttribute>();
            services.AddScoped<ValidatePokemonExistsAttribute>();
            services.AddScoped<ValidateMediaTypeAttribute>();

            services.AddScoped<IDataShaper<PokemonDto>, DataShaper<PokemonDto>>();
            services.AddScoped<PokemonLinks>();
            
            services.ConfigureCustomMediaType();
            
            services.ConfigureVersioning();
            
            services.ConfigureResponseCaching();
            services.ConfigureCacheHeaders();

            services.AddMemoryCache();
            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            
            services.AddTransient<PokeApiRestClient>();
            
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");
            
            app.UseSwagger();
            app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon Web API v1");
                    s.SwaggerEndpoint("/swagger/v2/swagger.json", "Pokemon Web API v2");
                }
            );
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseIpRateLimiting();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
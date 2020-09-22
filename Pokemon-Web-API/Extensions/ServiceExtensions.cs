using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using AspNetCoreRateLimit;
using Contracts;
using Entities;
using Entities.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                opts.UseNpgsql(configuration.GetConnectionString("sqlConnection1"),
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

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching();

        public static void ConfigureCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                expirationOptions =>
                {
                    expirationOptions.MaxAge = 69;
                    expirationOptions.CacheLocation = CacheLocation.Private;
                },
                validationOptions =>
                {
                    validationOptions.MustRevalidate = true;
                });

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 5,
                    Period = "1m"
                }
            };
            services.Configure<IpRateLimitOptions>(opt => opt.GeneralRules = rateLimitRules);
            
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
            } );
            
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }
        
        public static void ConfigureUserService(this IServiceCollection services) =>
            services.AddScoped<IUserService, UserService>();

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        
                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(secretKey!))
                    };
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pokemon Web API", 
                    Version = "v1",
                    Description = "Pokemon REST API by Rostyslav Ilnytskyi  a.k.a Rostil",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Rostyslav Ilnytskyi",
                        Email = "rostyslav.ilnytskyi@gmail.com",
                        Url = new Uri("https://github.com/RostilKant")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Pokemon API LICX",
                        Url = new Uri("https://example.com/license"),
                    }

                });
                s.SwaggerDoc("v2", new OpenApiInfo{Title = "Pokemon Web API", Version = "v2"});
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });

    }
}
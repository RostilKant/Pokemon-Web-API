using Microsoft.Extensions.DependencyInjection;

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
        
    
    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using O2.Services.Certificates.API.Filters;
using O2.Services.Certificates.Business.Impl.Services;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API.IoC
{
    public static class ServiceCollectionExtensions
    {
          public static IServiceCollection AddRequiredMvcComponents(this IServiceCollection services)
        {
            services.AddTransient<ApiExceptionFilter>();

            var mvcBuilder = services.AddMvcCore(options =>
            {
                // options.Filters.AddService<ApiExceptionFilter>();
                //
                // var policy = new AuthorizationPolicyBuilder()
                //     .RequireAuthenticatedUser()
                //     .RequireClaim("scope", "CertificateManagement")
                //     .Build();
                // options.Filters.Add(new AuthorizeFilter(policy));
            });
            
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);   
            mvcBuilder.AddJsonFormatters();
            // mvcBuilder.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddConfiguredAuth(this IServiceCollection services)
        {
            // services
            //     .AddAuthentication("Bearer")
            //     .AddJwtBearer("Bearer", options =>
            //     {
            //         options.Authority = "https://localhost:5005";
            //         options.RequireHttpsMetadata = false;
            //
            //         options.Audience = "CertificateManagement";
            //     });

            return services;
        }
        
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<ICertificatesService, InMemoryCertificatesService>();
            
            //more business services...

            
            return services;
        }
        
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
 
            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
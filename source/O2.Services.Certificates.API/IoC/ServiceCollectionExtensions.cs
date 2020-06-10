using Microsoft.Extensions.DependencyInjection;
using O2.Services.Certificates.Business.Impl.Services;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<ICertificatesService, InMemoryCertificatesService>();
            //more business services...
            
            return services;
        }
    }
}
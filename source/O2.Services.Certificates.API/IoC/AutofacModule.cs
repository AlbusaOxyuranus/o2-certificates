using Autofac;
using O2.Services.Certificates.Business.Impl.Services;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API.IoC
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryCertificatesService>().As<ICertificatesService>().SingleInstance();
            base.Load(builder);
        }
    }
}
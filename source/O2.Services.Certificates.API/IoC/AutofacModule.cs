using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Logging;
using O2.Services.Certificates.Business.Impl.Services;
using O2.Services.Certificates.Business.Models;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryCertificatesService>().Named<ICertificatesService>("certificatesService").SingleInstance();
            builder.RegisterDecorator<ICertificatesService>((context, service) => new CertificatesServiceDecorator(service,context.Resolve<ILogger<CertificatesServiceDecorator>>()), "certificatesService");
            // base.Load(builder);
        }

        private class CertificatesServiceDecorator : ICertificatesService
        {
            private readonly ICertificatesService _inner;
            private readonly ILogger<CertificatesServiceDecorator> _logger;

            public CertificatesServiceDecorator(ICertificatesService inner, ILogger<CertificatesServiceDecorator> logger)
            {
                _inner = inner;
                _logger = logger;
            }

            public IReadOnlyCollection<Certificate> GetAll()
            {
                using (var scope=_logger.BeginScope("Decorator scope: {decorator}",nameof(CertificatesServiceDecorator)))
                {
                    _logger.LogTrace("#################### Hello from  {decoratedMethod} ####################",nameof(GetAll));
                    var result= _inner.GetAll();
                    _logger.LogTrace("#################### Goodbye from  {decoratedMethod} ####################",nameof(GetAll));
                    return result;
                }
            }

            public Certificate GetById(long id)
            {
                _logger.LogWarning("#################### Hello from  {decoratedMethod} ####################",nameof(GetById));
                return _inner.GetById(id);
            }

            public Certificate Update(Certificate certificate)
            {
                _logger.LogTrace("#################### Hello from  {decoratedMethod} ####################",nameof(Update));
                return _inner.Update(certificate);
            }

            public Certificate Add(Certificate certificate)
            {
                _logger.LogTrace("#################### Hello from  {decoratedMethod} ####################", nameof(Add));
                return _inner.Add(certificate);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Autofac;
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
            builder.RegisterDecorator<ICertificatesService>((context, service) => new CertificatesServiceDecorator(service), "certificatesService");
            // base.Load(builder);
        }

        private class CertificatesServiceDecorator : ICertificatesService
        {
            private readonly ICertificatesService _inner;

            public CertificatesServiceDecorator(ICertificatesService inner)
            {
                _inner = inner;
            }

            public IReadOnlyCollection<Certificate> GetAll()
            {
                Console.WriteLine($"#################### Hello from  {nameof(GetAll)} ####################");
                return _inner.GetAll();
            }

            public Certificate GetById(long id)
            {
                Console.WriteLine($"#################### Hello from  {nameof(GetById)} ####################");
                return _inner.GetById(id);
            }

            public Certificate Update(Certificate certificate)
            {
                Console.WriteLine($"#################### Hello from  {nameof(Update)} ####################");
                return _inner.Update(certificate);
            }

            public Certificate Add(Certificate certificate)
            {
                Console.WriteLine($"#################### Hello from  {nameof(Add)} ####################");
                return _inner.Add(certificate);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using O2.Services.Certificates.Business.Models;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.Business.Impl.Services
{
    public class InMemoryCertificatesService: ICertificatesService
    {
        private readonly List<Certificate> _certificates = new List<Certificate>();
        private long _currentCertificate = 0;
        public IReadOnlyCollection<Certificate> GetAll()
        {
            return _certificates.AsReadOnly();
        }

        public Certificate GetById(long id)
        {
            return _certificates.SingleOrDefault(x=>x.Id==id);
        }

        public Certificate Update(Certificate certificate)
        {
            var toUpdate = _certificates.SingleOrDefault(x => x.Id == certificate.Id);
            if (toUpdate ==null)
            {
                return null;
            }

            toUpdate.Name = certificate.Name;
            
            return toUpdate;
        }

        public Certificate Add(Certificate certificate)
        {
            certificate.Id = ++_currentCertificate;
            _certificates.Add(certificate);
            return certificate;
        }
    }
}
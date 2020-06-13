using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using O2.Services.Certificates.Business.Models;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.Business.Impl.Services
{
    public class InMemoryCertificatesService: ICertificatesService
    {
        private static readonly Random RandomGenerator = new Random();
        private readonly List<Certificate> _certificates = new List<Certificate>();
        private long _currentCertificate = 0;
        
        public Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyCollection<Certificate>>(_certificates.AsReadOnly());
        }
        
        public async Task<Certificate> GetByIdAsync(long id,CancellationToken ct)
        {
            await Task.Delay(1000,ct);
            var extResult1Task = CallExternalServiceAsync(ct);
            var extResult2Task = CallExternalServiceAsync(ct);
            await Task.WhenAll(extResult1Task,extResult2Task);
            return _certificates.SingleOrDefault(x=>x.Id==id);
        }

        public Task<Certificate> UpdateAsync(Certificate certificate,CancellationToken ct)
        {
            var toUpdate = _certificates.SingleOrDefault(x => x.Id == certificate.Id);
            if (toUpdate ==null)
            {
                return null;
            }
            
            toUpdate.Name = certificate.Name;
            return Task.FromResult(toUpdate);
        }

        public Task<Certificate> AddAsync(Certificate certificate,CancellationToken ct)
        {
            certificate.Id = ++_currentCertificate;
            _certificates.Add(certificate);
            return Task.FromResult(certificate);
        }

        private static async Task<int> CallExternalServiceAsync(CancellationToken ct)
        {
            await Task.Delay(1000, ct);
            return RandomGenerator.Next();
        }
    }
}
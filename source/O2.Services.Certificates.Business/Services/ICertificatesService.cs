using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using O2.Services.Certificates.Business.Models;

namespace O2.Services.Certificates.Business.Services
{
    public interface ICertificatesService
    {
        Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken ct);
        Task<Certificate> GetByIdAsync(long id, CancellationToken ct);
        Task<Certificate> UpdateAsync(Certificate certificate, CancellationToken ct);
        Task<Certificate> AddAsync(Certificate certificate, CancellationToken ct);
    }
}
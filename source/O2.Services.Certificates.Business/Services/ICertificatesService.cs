using System.Collections.Generic;
using O2.Services.Certificates.Business.Models;

namespace O2.Services.Certificates.Business.Services
{
    public interface ICertificatesService
    {
        IReadOnlyCollection<Certificate> GetAll();
        Certificate GetById(long id);
        Certificate Update(Certificate certificate);
        Certificate Add(Certificate certificate);
    }
}
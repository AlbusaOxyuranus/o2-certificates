using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using O2.Services.Certificates.Business.Impl.Services.ExporterHelpers;
using O2.Services.Certificates.Business.Models;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.Business.Impl.Services
{
    public class InMemoryCertificatesService: ICertificatesService
    {
        private static readonly Random RandomGenerator = new Random();
        private readonly List<Certificate> _certificates = new List<Certificate>();
        private long _currentCertificate = 0;

        public InMemoryCertificatesService()
        {
           
        }
        public static string GetDLLFolder()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
        public async Task GetData()
        {
            await Task.Delay(100);
            var path = GetDLLFolder()+ "/Services/Import_DB_PFR.json";
            
            var str = System.IO.File.ReadAllText(path);
            var certificationForListDto = JsonConvert.DeserializeObject<List<O2CCertificateForCreateDto>>(str);
            var id = 0;
            foreach (var certDto in certificationForListDto)
            {
                _certificates.Add(
                    new Certificate()
                    {
                        Id = id,
                        ShortNumber = certDto.ShortNumber,
                        Serial = certDto.Serial,
                        Number = certDto.Number,
                        Lastname = certDto.Lastname,
                        Firstname = certDto.Firstname,
                        Middlename = certDto.Middlename,
                        Education = certDto.Education,
                        DateOfCert = certDto.DateOfCert,
                        Visible = certDto.Visible,
                        Lock = certDto.Lock
                    }
                    );
                ++id;
            }
        }
        public Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken ct)
        {
            GetData().GetAwaiter().GetResult();
            return Task.FromResult<IReadOnlyCollection<Certificate>>(_certificates.AsReadOnly());
        }
        
        public async Task<Certificate> GetByIdAsync(long id,CancellationToken ct)
        {            
            GetData().GetAwaiter().GetResult();
            await Task.Delay(1000,ct);
            var extResult1Task = CallExternalServiceAsync(ct);
            var extResult2Task = CallExternalServiceAsync(ct);
            await Task.WhenAll(extResult1Task,extResult2Task);
            return _certificates.SingleOrDefault(x=>x.Id==id);
        }

        public Task<Certificate> UpdateAsync(Certificate certificate,CancellationToken ct)
        {
            GetData().GetAwaiter().GetResult();
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
            GetData().GetAwaiter().GetResult();
            certificate.Id = ++_currentCertificate;
            _certificates.Add(certificate);
            return Task.FromResult(certificate);
        }

        public async Task RemoveAsync(long id, CancellationToken ct)
        {
            GetData().GetAwaiter().GetResult();
            await Task.Delay(1000,ct);
            var toDelete = _certificates.SingleOrDefault(x => x.Id == id);
            _certificates.Remove(toDelete);
        }

        private static async Task<int> CallExternalServiceAsync(CancellationToken ct)
        {
            await Task.Delay(1000, ct);
            return RandomGenerator.Next();
        }
    }
}
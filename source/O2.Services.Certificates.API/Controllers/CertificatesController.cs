using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using O2.Services.Certificates.API.Mappings;
using O2.Services.Certificates.API.Models;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API.Controllers
{
    //localhost:5000/certificates
    [Route("certificates")]
    public class CertificatesController : Controller
    {
        private readonly ICertificatesService _certificatesService;

        public CertificatesController(ICertificatesService certificatesService)
        {
            _certificatesService = certificatesService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id, CancellationToken ct)
        {
            var certificate = await _certificatesService.GetByIdAsync(id, ct);

            if (certificate == null)
            {
                return NotFound();
            }

            return Ok(certificate.ToViewModel());
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _certificatesService.GetAllAsync(ct);
            return Ok(result.ToViewModel());
        }

        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, CertificateViewModel model, CancellationToken ct)
        {
            model.Id = id; //not needed when we move to MediatR
            var certificate = await _certificatesService.UpdateAsync(model.ToServiceModel(), ct);
            
            return Ok(certificate.ToViewModel());
        }

        [HttpPut]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync(CertificateViewModel model, CancellationToken ct)
        {
            model.Id = 0; //not needed when we move to MediatR
            var certificate = await _certificatesService.AddAsync(model.ToServiceModel(), ct);

            return CreatedAtAction(nameof(GetByIdAsync),new { id = certificate.Id }, certificate.ToViewModel());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveAsync(long id, CancellationToken ct)
        {
            await _certificatesService.RemoveAsync(id, ct);

            return NoContent();
        }
    }
}

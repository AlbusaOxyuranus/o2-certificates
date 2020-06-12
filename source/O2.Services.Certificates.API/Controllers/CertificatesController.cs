using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using O2.Services.Certificates.API.Demo.Filters;
using O2.Services.Certificates.API.Mappings;
using O2.Services.Certificates.API.Models;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API.Controllers
{

    // [ServiceFilter(typeof(DemoExceptionFilter))]
    [DemoExceptionFilterFactoryAttribute]
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
        [Route("")]
        public async Task<IActionResult> IndexAsync(CancellationToken ct)
        {
            var result = await _certificatesService.GetAllAsync(ct);
            return View(result.ToViewModel());
        }
        
        [HttpGet]
        [Route(("id"))]
        public async Task<IActionResult> DetailsAsync(long id,CancellationToken ct)
        {
            var certificate = await _certificatesService.GetByIdAsync(id,ct);
            if (certificate == null)
                return NotFound();
            
            return View(certificate.ToViewModel());
        }
        
        [DemoActionFilter]
        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(long id, CertificateViewModel model,CancellationToken ct)
        {
            var certificate = await _certificatesService.UpdateAsync(model.ToServiceModel(),ct);
            if (certificate == null)
                return NotFound();
            
            certificate.Name = model.Name;
            return RedirectToAction("IndexAsync");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [DemoActionFilter]
        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReally(CertificateViewModel model,CancellationToken ct)
        {
            await _certificatesService.AddAsync(model.ToServiceModel(),ct);
            return RedirectToAction("IndexAsync");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using O2.Services.Certificates.API.Demo;
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
        private readonly DemoSecretsConfiguration _secrets;
        private readonly SomeRootConfiguration _config;

        public CertificatesController(ICertificatesService certificatesService,
            IOptions<SomeRootConfiguration> config, DemoSecretsConfiguration secrets)
        {
            _certificatesService = certificatesService;
            _config = config.Value;
            _secrets = secrets;
        }
        private static long _currentCertificate = 1;

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(_certificatesService.GetAll().ToViewModel());
        }
        
        [HttpGet]
        [Route(("id"))]
        public IActionResult Details(long id)
        {
            var certificate = _certificatesService.GetById(id);
            if (certificate == null)
                return NotFound();
            
            return View(certificate.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, CertificateViewModel model)
        {
            var certificate = _certificatesService.Update(model.ToServiceModel());
            if (certificate == null)
                return NotFound();
            
            certificate.Name = model.Name;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReally(CertificateViewModel model)
        {
            _certificatesService.Add(model.ToServiceModel());
            return RedirectToAction("Index");
        }
    }
}

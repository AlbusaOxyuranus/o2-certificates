using Microsoft.AspNetCore.Mvc;
using O2.Services.Certificates.API.Demo.Filters;
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
        
        [DemoActionFilter]
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
        
        [DemoActionFilter]
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

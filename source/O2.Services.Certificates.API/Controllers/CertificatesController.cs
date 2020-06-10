using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using O2.Services.Certificates.API.Demo;
using O2.Services.Certificates.API.Models;

namespace O2.Services.Certificates.API.Controllers
{
    //localhostL500/certificates
    [Route("certificates")]
    public class CertificatesController : Controller
    {
        private readonly ICertificateGenerator _certificateGenerator;

        public CertificatesController( ICertificateGenerator certificateGenerator)
        {
            _certificateGenerator = certificateGenerator;
        }
        private static long currentGroup = 1;
        private static List<CertificateViewModel> _certificates = new List<CertificateViewModel>()
        {
            new CertificateViewModel()
            {
                Id =1, Name="Sample Certificate"
            }
        };
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(_certificates);
        }
        
        [HttpGet]
        [Route(("id"))]
        public IActionResult Details(long id)
        {
            var certificate = _certificates.SingleOrDefault(c => c.Id == id);
            if (certificate == null)
                return NotFound();
            
            return View(certificate);
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, CertificateViewModel model)
        {
            var certificate = _certificates.SingleOrDefault(c => c.Id == id);
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
        public IActionResult CreateRelly(CertificateViewModel model)
        {
            model.Id = _certificateGenerator.Next();
            _certificates.Add(model);
            return RedirectToAction("Index");
        }
    }
}

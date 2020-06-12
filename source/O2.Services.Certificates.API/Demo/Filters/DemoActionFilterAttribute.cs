using Microsoft.AspNetCore.Mvc.Filters;
using O2.Services.Certificates.API.Models;

namespace O2.Services.Certificates.API.Demo.Filters
{
    public class DemoActionFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("model", out var model) &&
                model is CertificateViewModel certificate &&
                certificate.Id == 1)
            {
                certificate.Name += $" (Added on {nameof(DemoActionFilterAttribute)})";
            }
        }
    }
}
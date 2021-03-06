using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace O2.Services.Certificates.API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DbUpdateConcurrencyException)
            {
                context.Result = new ConflictObjectResult(new { Message = "The updated entity has changed, please refresh your current copy." });
            }
        }
    }
}

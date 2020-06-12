using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace O2.Services.Certificates.API.Demo.Filters
{
    public class DemoExceptionFilter:IExceptionFilter
    {
        private readonly ILogger<DemoExceptionFilter> _logger;

        public DemoExceptionFilter(ILogger<DemoExceptionFilter> logger)
        {
            _logger = logger;
        }
        
        public string Suffix { get; set; } = "by ServiceFilterAttribute ";
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                _logger.LogError("Transforming ArgumentException is 400 {suffix}",Suffix);
                context.Result = new BadRequestResult();
            }
        }
    }
}
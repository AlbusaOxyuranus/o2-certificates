using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace O2.Services.Certificates.API.Demo.Filters
{
    public class DemoExceptionFilterFactoryAttribute:Attribute,IFilterFactory
    {
        private bool _isReusable;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetRequiredService<DemoExceptionFilter>();
            filter.Suffix = $"by {nameof(DemoExceptionFilterFactoryAttribute)}";
            return filter;
        }
        public bool IsReusable => _isReusable = false;
    }
}
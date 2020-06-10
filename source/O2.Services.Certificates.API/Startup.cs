using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using O2.Services.Certificates.Business.Impl.Services;
using O2.Services.Certificates.Business.Services;

namespace O2.Services.Certificates.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<ICertificatesService, InMemoryCertificatesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting((() =>
                {
                    context.Response.Headers.Add("X.Powered-By", "Asp .Net Core: O2 Certificates");
                    return Task.CompletedTask;
                }));

             
                    await next.Invoke();    
             
                
            });
            
            app.UseStaticFiles();
            
            app.UseMvc();
        }
    }
}

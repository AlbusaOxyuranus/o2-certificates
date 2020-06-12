using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using O2.Services.Certificates.API.Demo;
using O2.Services.Certificates.API.IoC;

namespace O2.Services.Certificates.API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // services.Configure<SomeRootConfiguration>(_config.GetSection("SomeRoot"));
            
            //injecting  POCO
            // var someRootConfiguration = new SomeRootConfiguration();
            // _config.GetSection("SomeRoot").Bind(someRootConfiguration);
            // services.AddSingleton(someRootConfiguration);
            
            //injecting POCO, but prettiuer :)
            services.ConfigurePOCO<SomeRootConfiguration>(_config.GetSection("SomeRoot"));
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
            //if using default DI container, uncomment
            // services.AddBusiness();
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

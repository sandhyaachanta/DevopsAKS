using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SollisHealth.Common.Helpers;
using SollisHealth.Login.Helper;
using SollisHealth.Login.Interface;
using SollisHealth.Login.Sercvices;

namespace SollisHealth.Login
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services = new StartupCommon(Configuration).ConfigureServices(services);

            services.AddTransient<IADHelper, ADHelper>();
            services.AddScoped<IUserBO, UserBO>();
            services.AddScoped<ADHelper>();
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app = new StartupCommon(Configuration).Configure(app,env,provider);
        }
    }
}

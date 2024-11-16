using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppWithASP
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
            });
        }
    }

    public class HomeController
    {
        [HttpGet("/")]
        public string Get() => "Hello World";
    }
}
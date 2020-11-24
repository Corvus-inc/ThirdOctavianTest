using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleProxy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string[] adress = new[] { "http://localhost:4200", "http://192.168.0.103:4200" };
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(adress)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });    
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/MessageHub");
            });
        }

    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            string[] adress = new[] { "http://localhost:4200", "http://192.168.0.103:4200", "http://198.168.0.103:4200", "http://*:5000", "http://*:5000", "http://198.168.0.103:5000", "http://5.16.74.108:4200", "http://5.16.74.108:5000" };

           
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200", "http://192.168.0.103:4200", "http://198.168.0.103:4200", "http://*:5000", "http://*:5000", "http://198.168.0.103:5000", "http://5.16.74.108:4200", "http://5.16.74.108:5000")
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

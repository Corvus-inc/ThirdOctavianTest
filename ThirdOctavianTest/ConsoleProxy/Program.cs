using System;
using ServiceReference1;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDefaultBuilder(args).Build().Run();

            ListOfUserServiceClient client = new ListOfUserServiceClient();
            var resultDep = client.GetUserDetailsAsync(GetCommandDB.GetAllUser).Result;
            
        }

        private static IWebHostBuilder CreateDefaultBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/MessageHub");
            });
        }

    }
}

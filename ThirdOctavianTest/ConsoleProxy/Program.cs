using System;
using ServiceReference1;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace ConsoleProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDefaultBuilder(args).Build().Run();

            //ListOfUserServiceClient client = new ListOfUserServiceClient();
            //var resultDep = client.GetUserDetailsAsync(GetCommandDB.GetAllUser).Result;

        }

        private static IWebHostBuilder CreateDefaultBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();
    }
}

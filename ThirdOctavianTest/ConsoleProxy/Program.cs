using System;
using ServiceReference1;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ConsoleProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            //ListOfUserServiceClient client = new ListOfUserServiceClient();
            //var resultDep = client.GetDeptsAsync().Result;
            //foreach (var item in resultDep)
            //{
            //    Console.WriteLine(item.Name);
            //}
            //Console.ReadKey();
            CreateDefaultBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateDefaultBuilder(string[] args)
        {
            
            return WebHost.CreateDefaultBuilder(args)
            .PreferHostingUrls(false)
            .UseUrls("http://*:5000")
              .UseStartup<Startup>();
        }
    }
}

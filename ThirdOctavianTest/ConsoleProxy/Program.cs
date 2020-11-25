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
            //ListOfUserServiceClient client = new ListOfUserServiceClient();
            //var resultDep = client.GetUsersAsync().Result;
            //foreach (var item in resultDep)
            //{
            //    Console.WriteLine(item.LoginPass);
            //}
            //Console.ReadKey();
            CreateDefaultBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateDefaultBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();
    }
}

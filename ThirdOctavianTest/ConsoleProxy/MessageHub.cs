using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using ServiceReference1;

namespace ConsoleProxy
{
    public class MessageHub : Hub
    {
        public async Task GetUsers()
        {Console.WriteLine("WCF START");
            ListOfUserServiceClient client = new ListOfUserServiceClient();
            
            var resultList = client.GetUsersAsync().Result;
            await Clients.All.SendAsync("addArrayUsers", resultList);
        }
        public async Task GetRoles()
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();

            var resultList = client.GetRolesAsync().Result;
            await Clients.All.SendAsync("addArrayRoles", resultList);
        } 
        public async Task GetDepts()
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();

            var resultList = client.GetDeptsAsync().Result;
            await Clients.All.SendAsync("addArrayRoles", resultList);
        }

        public async Task SetRequest(User userDetails, ProcedureDB SetCommand)
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();

            await client.SetUserDetailsAsync(userDetails, SetCommand);
            Console.WriteLine("SetMethod");
            await Clients.All.SendAsync("linkMethod", "RETUNED");
        }
        public async Task Send(string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("Receive", message);
        }
    }
}

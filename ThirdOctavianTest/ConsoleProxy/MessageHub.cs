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
        public async Task GetRequest()
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();

            var resultList = client.GetUsersAsync().Result;
            Console.WriteLine(resultList[0].Login);
            await Clients.All.SendAsync("SetArray", resultList);
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

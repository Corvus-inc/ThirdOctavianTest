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
        public async Task GetRequest(GetCommandDB GetAllUser)
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();

            var resultList = client.GetUserDetailsAsync(GetAllUser).Result;
            Console.WriteLine(resultList[0].Login);
            await Clients.All.SendAsync("SetArray", resultList);
        }
        public async Task Send(User message)
        {
            Console.WriteLine(message.Id);
            Console.WriteLine(message.Login);
            await Clients.All.SendAsync("Receive", message);
        }
    }
}

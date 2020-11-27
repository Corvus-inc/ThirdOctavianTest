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
        {
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
            await Clients.All.SendAsync("addArrayDepts", resultList);
        }
        public async Task SetUser(User uDetails, ProcedureDB SetCommand)
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();
            await client.SetUserDetailsAsync(uDetails, SetCommand);
            Console.WriteLine("StartUserSetMethod");
            await Clients.All.SendAsync("linkMethod", "UserWCFMethodActivate");
        }
        public async Task SetRole(Role rDetails, ProcedureDB SetCommand)
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();
            await client.SetRoleDetailsAsync(rDetails, SetCommand);
            Console.WriteLine("StartRoleSetMethod");
            await Clients.All.SendAsync("linkMethod", "RoleWCFMethodActivate");
        }
        public async Task SetDept(Dept dDetails, ProcedureDB SetCommand)
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();
            await client.SetDeptDetailsAsync(dDetails, SetCommand);
            Console.WriteLine("StartDeptSetMethod");
            await Clients.All.SendAsync("linkMethod", "DeptWCFMethodActivate");
        }
    }
}

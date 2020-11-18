using System;
using ServiceReference1;

namespace ConsoleProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ListOfUserServiceClient client = new ListOfUserServiceClient();
            var resultDep = client.GetUserDetailsAsync(GetCommandDB.GetAllUser).Result;
            
        }
    }
}

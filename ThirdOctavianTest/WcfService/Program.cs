using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract]
    public interface IListOfUserService
    {
        [OperationContract]
        Dictionary<int,string> GetDepartaments();
    }

    public class ListOfUser : IListOfUserService
    {
        public Dictionary<int, string> GetDepartaments()
        {
            Dictionary<int, string> departaments = new Dictionary<int, string>();
            //метод добычи департамента из бд
            departaments = ConectToDb.BecomeDictionaryFromBd("Departaments");
            return departaments;
        }
        public Dictionary<int, string> GetRoles()
        {
            Dictionary<int, string> departaments = new Dictionary<int, string>();
            //метод добычи Роли из бд
            departaments = ConectToDb.BecomeDictionaryFromBd("Roles");
            return departaments;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            User added = new User() { Departament = "dep", Role = "rol", Id = 1, Password = "pass", Login = "Log" };
            ConectToDb.CreateUser(added, "Users");
           
            WSHttpBinding binding = new WSHttpBinding();
            binding.Name = "binding1";
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = SecurityMode.None;
            binding.ReliableSession.Enabled = false;
            binding.TransactionFlow = false;

            Uri baseAddress = new Uri("http://localhost:8080/Departament");

            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(ListOfUser), baseAddress))
            {
                host.AddServiceEndpoint(typeof(IListOfUserService), binding, baseAddress);

                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}

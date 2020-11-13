using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
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
        string InsertUserDetails(User uDetails);
        [OperationContract]
        DataSet GetUserDetails(User uDetails);
        [OperationContract]
        DataSet FetchUpdatedRecords(User uDetails);
        [OperationContract]
        string UpdateUserDetails(User uDetails);
        [OperationContract]
        bool DeleteUserDetails(User uDetails);
    }
    #region SqlProcedurePostgress
    //      Create Procedure User_Update
    //(upId int,
    //upLogin varchar(40),  
    //upPassword varchar(40),  
    //upRole varchar(40),
    //upDeptId integer)
    //language sql
    //As $$
    //update Users Set
    //Login = upLogin,
    //Password = upPassword,
    //"departamentId"=upDeptId
    //where "Id"=upId  
    //$$ 

    //    CREATE Procedure User_Delete
    //    (uLogin varchar(40))
    //language sql
    //AS  $$  
    //Delete From users
    //where login=uLogin 
    //$$

    //    Create Procedure User_Insert
    //(inLogin varchar(40),  
    //    inPassword varchar(40),  
    //    inRole varchar(40),
    //    inDeptId integer)  
    //    LANGUAGE SQL
    //    AS $$  
    //    Insert into users
    //    (Login, Password, Role,"departamentId") Values
    //      (inLogin, inPassword, inRole, inDeptId)
    //    $$

    //    Create Procedure Get_AllUsers
    //(uId int = null)
    //language sql
    //AS $$  
    //Select E."Id", E.Login, E.Role, E."departamentId", D.Name
    //From users E
    //Join Roles D
    //On E."departamentId" = D.Id
    //where D.Status = 1
    //And Id = COALESCE(uId, Id)  
    //$$ 
    #endregion
    [DataContract]
    public class User
    {
        int? uId;
        string uLogin = string.Empty;
        string uPassword = string.Empty;
        string uRoleId = string.Empty;
        string uRole = string.Empty;
        string uDepartamentId = string.Empty;
        string uDepartament = string.Empty;
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public int DepartamentId { get; set; }
        [DataMember]
        public string Departament { get; set; }

    }

    public class ListOfUser : IListOfUserService
    {
        static string connectionString = "Host=localhost;Database=Corvus;Username=postgres;Persist Security Info=True;Password=pa$$word;";

        public string InsertUserDetails(User uDetails)
        {
            string Status;
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand($"Call User_Insert('{uDetails.Login}'," +
                    $"'{uDetails.Password}'," +
                    $"'{uDetails.Role}'," +
                    $"'{uDetails.DepartamentId}')", con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("inlogin", uDetails.Login);
                    //cmd.Parameters.AddWithValue("inpassword", uDetails.Password);values.
                    //cmd.Parameters.AddWithValue("inrole", uDetails.Role);
                    //cmd.Parameters.AddWithValue("indeptid", NpgsqlTypes.NpgsqlDbType.Integer).Value= uDetails.DepartamentId;
                    //cmd.Prepare();
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        Status = uDetails.Login + " " + uDetails.Password + " registered successfully";
                    }
                    else
                    {
                        Status = uDetails.Login + " " + uDetails.Password + " could not be registered";
                    }
                    con.Close();
                    return Status;
                }
            }

        }

        public bool DeleteUserDetails(User uDetails)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand($"Call User_Delete('{uDetails.Login}')", con))
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
        }
        public DataSet FetchUpdatedRecords(User uDatails)
        {
            throw new NotImplementedException();
        }

        public DataSet GetUserDetails(User uDatails)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand($"Call Get_AllUsers({uDatails.Id})", con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", uDatails.Id);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return ds;
                }
            }
        }

        public string UpdateUserDetails(User uDatails)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ListOfUser list = new ListOfUser();
            User added = new User() { Departament = "dep", Role = "rol", Id = 1, Password = "pass", Login = "Think", DepartamentId = 35, RoleId = 3 };
            bool d = list.DeleteUserDetails(added);
            //DataSet f = list.GetUserDetails(added);
            //string s = list.InsertUserDetails(added);
            Console.WriteLine();
            

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

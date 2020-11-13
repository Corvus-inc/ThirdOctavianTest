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
    //    Create Procedure User_Update
    //   (upId int,
    //    upLogin varchar(40),  
    //    upPassword varchar(40),  
    //    upRoleId integer,
    //    upDeptId integer)
    //    language sql
    //    As $$
    //    update Users Set
    //    Login = upLogin,
    //    Password = upPassword,
    //	  "roleid" = upRoleId,
    //    "departamentId"=upDeptId
    //    where "Id"=upId  
    //    $$ 

    //   CREATE Procedure User_Delete
    //  (uid integer)
    //   language sql
    //   AS  $$  
    //   Delete From users
    //   where "Id" = uid
    //   $$

    //    Create Procedure User_Insert
    //   (inLogin varchar(40),  
    //    inPassword varchar(40),  
    //    inRoleId integer),
    //    inDeptId integer)  
    //    LANGUAGE SQL
    //    AS $$  
    //    Insert into users
    //    (Login, Password, "roleid", "departamentId") Values
    //      (inLogin, inPassword, inRoleId, inDeptId)
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
                using (var cmd = new NpgsqlCommand($"Call User_Insert(" +
                    $"'{uDetails.Login}'," +
                    $"'{uDetails.Password}'," +
                    $"{uDetails.RoleId}," +
                    $"{uDetails.DepartamentId})", con))
               
                {
                    #region TryToCallProcedureFromProperty
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("inlogin", uDetails.Login);
                    //cmd.Parameters.AddWithValue("inpassword", uDetails.Password);values.
                    //cmd.Parameters.AddWithValue("inroleid", uDetails.Role);
                    //cmd.Parameters.AddWithValue("indeptid", NpgsqlTypes.NpgsqlDbType.Integer).Value= uDetails.DepartamentId;
                    //cmd.Prepare(); 
                    #endregion
                    int result = cmd.ExecuteNonQuery();

                    Status = result.ToString();

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
                using (var cmd = new NpgsqlCommand($"Call User_Delete({uDetails.Id})", con))
                {
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

        public string UpdateUserDetails(User uDetails)
        {
            string Status;
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand($"Call User_Update(" +
                   $"'{uDetails.Id}'," +
                   $"'{uDetails.Login}'," +
                   $"'{uDetails.Password}'," +
                   $"'{uDetails.RoleId}'," +
                   $"'{uDetails.DepartamentId}')", con))
                {
                    int result = cmd.ExecuteNonQuery();
                    Status = result.ToString();

                    con.Close(); //Актуальна ли эта строка в using  дерективе?

                    return Status;
                }
            }
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ListOfUser list = new ListOfUser();
            User added = new User() { Departament = "Ldd", Role = "Fiend", Password = "ss", Login = "Nagve", DepartamentId = 32, RoleId = 14, Id = 2 };
            string u = list.UpdateUserDetails(added);
            //bool d = list.DeleteUserDetails(added);
            ////DataSet f = list.GetUserDetails(added);
            //string s = list.InsertUserDetails(added);
            Console.WriteLine(u);


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

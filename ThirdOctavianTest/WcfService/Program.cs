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
        void SetUserDetails(User uDetails, ProcedureDB nameProcedure);
        [OperationContract]
        DataSet GetUserDetails(User uDetails, ProcedureDB nameProcedure);
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
        int ?uRoleId;
        string uRole = string.Empty;
        int ?uDepartamentId;
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

    public enum ProcedureDB
    {
        UserInsert,
        RoleInsert,
        DeptInsert,
        UserUpdate,
        RoleUpdate,
        DeptUpdate,
        UserDelete,
        RoleDelete,
        DeptDelete,
        GetAllUser,
        GetAllRole,
        GetAllDept,
    }

    public class ListOfUser : IListOfUserService
    {
        static string connectionString = "Host=localhost;Database=Corvus;Username=postgres;Persist Security Info=True;Password=pa$$word;";

        public void SetUserDetails(User uDetails, ProcedureDB nameProcedure)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand(CallProcedureString(uDetails,nameProcedure), con))
               
                {
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }
        public DataSet GetUserDetails(User uDetails, ProcedureDB nameProcedure)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand($"Call Get_AllUsers({uDetails.Id})", con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", uDetails.Id);
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
        private string CallProcedureString(User uDetails, ProcedureDB nameProcedure)
        {
            string ProcedureString = string.Empty;
            switch (nameProcedure)
            {
                case ProcedureDB.UserInsert:
                    ProcedureString = $"Call User_Insert(" +
                    $"'{uDetails.Login}'," +
                    $"'{uDetails.Password}'," +
                    $"{uDetails.RoleId}," +
                    $"{uDetails.DepartamentId})";
                    break;
                case ProcedureDB.UserUpdate:
                    ProcedureString = $"Call User_Update(" +
                    $"'{uDetails.Id}'," +
                    $"'{uDetails.Login}'," +
                    $"'{uDetails.Password}'," +
                    $"'{uDetails.RoleId}'," +
                    $"'{uDetails.DepartamentId}')";
                    break;
                case ProcedureDB.UserDelete:
                    ProcedureString = $"Call User_Delete({uDetails.Id})";
                    break;
                case ProcedureDB.GetAllUser:
                    ProcedureString = $"Call Get_AllUsers({uDetails.Id})";
                    break;
                case ProcedureDB.DeptInsert:
                   
                    break;
                case ProcedureDB.DeptUpdate:
                   
                    break;
                case ProcedureDB.DeptDelete:
                   
                    break;
                case ProcedureDB.GetAllDept:
                   
                    break;
                case ProcedureDB.RoleInsert:
                   
                    break;
                case ProcedureDB.RoleUpdate:
                   
                    break;
                case ProcedureDB.RoleDelete:
                   
                    break;
                case ProcedureDB.GetAllRole:
                   
                    break;
               
            }
            return ProcedureString;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ListOfUser list = new ListOfUser();
            User added = new User() { Departament = "Ldd", Role = "Fiend", Password = "ss", Login = "Nagve", DepartamentId = 32, RoleId = 14, Id = 2 };
            //string u = list.UpdateUserDetails(added);
            //bool d = list.DeleteUserDetails(added);
            ////DataSet f = list.GetUserDetails(added);
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

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
                using (var cmd = new NpgsqlCommand(CallProcedureString(uDetails, nameProcedure), con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    
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
                    ProcedureString = "Select U.Id, U.login, U.password, U.deptid, U.roleid, D.name, R.Name" +
                        "    From users U" +
                        "    Left Join roles R" +
                        "    On U.roleid = R.id" +
                        "    Left Join depts D" +
                        "    On U.deptid = D.Id";
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
            User added = new User() { Departament = "run", Role = "oll", Password = "enter", Login = "Third", DepartamentId = 3, RoleId = 2, Id = 1 };
            //list.SetUserDetails(added, ProcedureDB.UserInsert);
            //string u = list.UpdateUserDetails(added);
            //bool d = list.DeleteUserDetails(added);
            DataSet f = list.GetUserDetails(added, ProcedureDB.GetAllUser);
            //string s = list.InsertUserDetails(added);
            DataTable dataTable = f.Tables[0];
            Console.WriteLine(dataTable.Columns[1].ColumnName);


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

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
        List<User> GetUserDetails(GetCommandDB cmdRequest);
        //Авторизация определяется в контракте?
    }
    [DataContract]
    public class User
    {
        int? uId;
        string uLogin = string.Empty;
        string uPassword = string.Empty;
        int? uRoleId;
        string uRole = string.Empty;
        int? uDepartamentId;
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
    }
    public enum GetCommandDB
    {
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
                using (var cmd = new NpgsqlCommand(CallProcedureString(uDetails, nameProcedure), con))

                {
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }
        public List<User> GetUserDetails(GetCommandDB cmdRequest)
        {
            DataSet ds;
            List<User> listU = new List<User>();

            using (var con = new NpgsqlConnection(connectionString))
            {

                con.Open();
                using (var cmd = new NpgsqlCommand(GetCommandDBString(cmdRequest), con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    listU = UnpackDataToList(ds);
                }
            }
            return listU;
        }
        //Метод жестко привязан к таблице, любые изменения в названиях и колонках приведут к поломке.
        private List<User> UnpackDataToList(DataSet data)
        {
            List<User> users;
            User u;
            DataTable tableU = data.Tables[0];
            users = new List<User>(tableU.Rows.Count);
            for (int i = 0; i < tableU.Columns.Count-2; i++)
            {
                u = new User();

                if (!tableU.Rows[i].ItemArray[0].Equals(DBNull.Value)) u.Id = (int)tableU.Rows[i].ItemArray[0];
                else u.Id = tableU.Rows.Count+1;

                if (!tableU.Rows[i].ItemArray[1].Equals(DBNull.Value)) u.Login = (string)tableU.Rows[i].ItemArray[1];
                else u.Login = null;

                if (!tableU.Rows[i].ItemArray[2].Equals(DBNull.Value)) u.Password = (string)tableU.Rows[i].ItemArray[2];
                else u.Password = null;

                if (!tableU.Rows[i].ItemArray[3].Equals(DBNull.Value)) u.DepartamentId = (int)tableU.Rows[i].ItemArray[3];
                else u.DepartamentId = 0;

                if (!tableU.Rows[i].ItemArray[4].Equals(DBNull.Value)) u.RoleId = (int)tableU.Rows[i].ItemArray[4];
                else u.RoleId = 0;

                if (!tableU.Rows[i].ItemArray[5].Equals(DBNull.Value)) u.Departament = (string)tableU.Rows[i].ItemArray[5];
                else u.Departament = null;

                if (!tableU.Rows[i].ItemArray[6].Equals(DBNull.Value)) u.Role = (string)tableU.Rows[i].ItemArray[6];
                else u.Role = null;

                users.Add(u);
            }


            return users;
        }

        private string GetCommandDBString(GetCommandDB cmdRequest)
        {
            string cmd = string.Empty;
            switch (cmdRequest)
            {
                case GetCommandDB.GetAllUser:
                    cmd = "Select U.Id, U.login, U.password, U.deptid, U.roleid, D.name, R.Name" +
                        "    From users U" +
                        "    Left Join roles R" +
                        "    On U.roleid = R.id" +
                        "    Left Join depts D" +
                        "    On U.deptid = D.Id";
                    break;
                case GetCommandDB.GetAllDept:

                    break;
                case GetCommandDB.GetAllRole:

                    break;

            }
            return cmd;
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
                case ProcedureDB.DeptInsert:

                    break;
                case ProcedureDB.DeptUpdate:

                    break;
                case ProcedureDB.DeptDelete:

                    break;
                case ProcedureDB.RoleInsert:

                    break;
                case ProcedureDB.RoleUpdate:

                    break;
                case ProcedureDB.RoleDelete:

                    break;

            }
            return ProcedureString;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //ListOfUser list = new ListOfUser();
            //User added = new User() { Departament = "run", Role = "oll", Password = "enter", Login = "Third", DepartamentId = 3, RoleId = 2, Id = 1 };
            ////list.SetUserDetails(added, ProcedureDB.UserInsert);
            ////string u = list.UpdateUserDetails(added);
            ////bool d = list.DeleteUserDetails(added);
            //List<User> f = list.GetUserDetails(GetCommandDB.GetAllUser);
            ////string s = list.InsertUserDetails(added);
            //DataTable dataTable = f.Tables[0];
            //Console.WriteLine(dataTable.Columns[3].ColumnName);


            WSHttpBinding binding = new WSHttpBinding();
            binding.Name = "binding1";
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = SecurityMode.None;
            binding.ReliableSession.Enabled = false;
            binding.TransactionFlow = false;

            Uri baseAddress = new Uri("http://localhost:8080/Users");

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

using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
        void SetDetails(User uDetails, ProcedureDB nameProcedure);
        [OperationContract]
        void SetDetails(Role rDetailsDetails, ProcedureDB nameProcedure);
        [OperationContract]
        void SetDetails(Dept dDetails, ProcedureDB nameProcedure);
        [OperationContract]
        List<User> GetUsers();
        [OperationContract]
        List<Role> GetRoles();
        [OperationContract]
        List<Dept> GetDepts();
        //Авторизация определяется в контракте?
    }

    public class ListOfUser : IListOfUserService
    {
        static string connectionString = "Host=localhost;Database=Corvus;Username=postgres;Persist Security Info=True;Password=pa$$word;";

        public void SetDetails(User uDetails, ProcedureDB nameProcedure)
        {
            ConnectToSetRequest(uDetails, nameProcedure);
        }

        public void SetDetails(Role rDetails, ProcedureDB nameProcedure)
        {
            ConnectToSetRequest(rDetails, nameProcedure);
        }

        public void SetDetails(Dept dDetails, ProcedureDB nameProcedure)
        {
            ConnectToSetRequest(dDetails, nameProcedure);
        }
        public List<User> GetUsers()
        {
            ConnectToGetRequest(GetCommandDB.GetAllUser);
            return users;
        }

        public List<Role> GetRoles()
        {
            ConnectToGetRequest(GetCommandDB.GetAllRole);
            return roles;
        }

        public List<Dept> GetDepts()
        {
            ConnectToGetRequest(GetCommandDB.GetAllDept);
            return depts;
        }

        private GetCommandDB dataInfo;
        private List<Role> roles;
        private List<Dept> depts;
        private List<User> users;
        private DataSet data;
        DataTable tableData;
        private void ConnectToGetRequest(GetCommandDB typeList)
        {
            string scmd = GetCommandDBString(typeList);
            using (var con = new NpgsqlConnection(connectionString))
            {

                con.Open();
                using (var cmd = new NpgsqlCommand(scmd, con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    data = new DataSet();
                    da.Fill(data);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    UnpackDataToList(data, dataInfo);
                }
            }
        }
        //Метод жестко привязан к таблице, любые изменения в названиях и колонках приведут к поломке. Ихменяя таблицу нужно изменять этот метод.
        private void UnpackDataToList(DataSet data, GetCommandDB dataInfo)
        {
            switch (dataInfo)
            {
                case GetCommandDB.GetAllUser:
                    {
                        users = new List<User>();
                        User u;
                        DataTable tableData = data.Tables[0];
                        string Login;
                        string Pass;
                        for (int i = 0; i < tableData.Rows.Count; i++)
                        {
                            u = new User();

                            if (!tableData.Rows[i].ItemArray[0].Equals(DBNull.Value)) u.Id = (int)tableData.Rows[i].ItemArray[0];
                            else u.Id = tableData.Rows.Count + 1;

                            if (!tableData.Rows[i].ItemArray[1].Equals(DBNull.Value)) Login = (string)tableData.Rows[i].ItemArray[1];
                            else Login = string.Empty;

                            if (!tableData.Rows[i].ItemArray[2].Equals(DBNull.Value)) Pass = (string)tableData.Rows[i].ItemArray[2];
                            else Pass = string.Empty;

                            if (!tableData.Rows[i].ItemArray[3].Equals(DBNull.Value)) u.DepartamentId = (int)tableData.Rows[i].ItemArray[3];
                            else u.DepartamentId = 0;

                            if (!tableData.Rows[i].ItemArray[4].Equals(DBNull.Value)) u.RoleId = (int)tableData.Rows[i].ItemArray[4];
                            else u.RoleId = 0;

                            u.LoginPass = new KeyValuePair<string, string>(Login, Pass);
                            users.Add(u);
                        }
                    }
                    break;
                case GetCommandDB.GetAllDept:
                    {
                        depts = new List<Dept>();
                        Dept d;
                        tableData = data.Tables[0];
                        for (int i = 0; i < tableData.Rows.Count; i++)
                        {
                            d = new Dept();
                            if (!tableData.Rows[i].ItemArray[0].Equals(DBNull.Value)) d.Id = (int)tableData.Rows[i].ItemArray[0];
                            else d.Id = tableData.Rows.Count + 1;

                            if (!tableData.Rows[i].ItemArray[1].Equals(DBNull.Value)) d.Name = (string)tableData.Rows[i].ItemArray[1];
                            else d.Name = null;

                            depts.Add(d);
                        }
                    }
                    break;
                case GetCommandDB.GetAllRole:
                    {
                        roles = new List<Role>();
                        Role r;
                        tableData = data.Tables[0];
                        for (int i = 0; i < tableData.Rows.Count; i++)
                        {
                            r = new Role();
                            if (!tableData.Rows[i].ItemArray[0].Equals(DBNull.Value)) r.Id = (int)tableData.Rows[i].ItemArray[0];
                            else r.Id = tableData.Rows.Count + 1;

                            if (!tableData.Rows[i].ItemArray[1].Equals(DBNull.Value)) r.Name = (string)tableData.Rows[i].ItemArray[1];
                            else r.Name = null;

                            roles.Add(r);
                        }
                    }
                    break;
            }
        }
        //Возвращает строку команды получения значений из БД
        private string GetCommandDBString(GetCommandDB cmdRequest)
        {
            string cmd = string.Empty;
            switch (cmdRequest)
            {
                case GetCommandDB.GetAllUser:
                    cmd = "SELECT * FROM users ";
                    dataInfo = cmdRequest;
                    break;
                case GetCommandDB.GetAllDept:
                    cmd = "SELECT * FROM depts ";
                    dataInfo = cmdRequest;
                    break;
                case GetCommandDB.GetAllRole:
                    cmd = "SELECT * FROM roles ";
                    dataInfo = cmdRequest;
                    break;

            }
            return cmd;
        }

        private void ConnectToSetRequest(object details, ProcedureDB nameProcedure)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand(CallProcedureString(details, nameProcedure), con))

                {
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }
        //В Базе сознданы процедуры, строки для обращения к которым собираются тут. Процедуры не возвращают значения. Стоит пересмотреть подход.
        private string CallProcedureString(object details, ProcedureDB nameProcedure)
        {
            User objU = new User();
            Role objR = new Role();
            Dept objD = new Dept();

            if (details is User) objU = details as User;
            if (details is Role) objR = details as Role;
            if (details is Dept) objD = details as Dept;

            string ProcedureString = string.Empty;

            switch (nameProcedure)
            {
                case ProcedureDB.UserInsert:
                    ProcedureString = $"CALL User_Insert(" +
                    $"'{objU.LoginPass.Key}'," +
                    $"'{objU.LoginPass.Value}'," +
                    $"{objU.RoleId}," +
                    $"{objU.DepartamentId})";
                    break;
                case ProcedureDB.UserUpdate:
                    ;
                    ProcedureString = $"CALL User_Update(" +
                $"'{objU.Id}'," +
                $"'{objU.LoginPass.Key}'," +
                $"'{objU.LoginPass.Value}'," +
                $"'{objU.RoleId}'," +
                $"'{objU.DepartamentId}')";
                    break;
                case ProcedureDB.UserDelete:
                    ProcedureString = $"CALL User_Delete({objU.Id})";
                    break;
                case ProcedureDB.DeptInsert:
                    ProcedureString = $"CALL Dept_Insert{objD.Id}, {objD.Name})";
                    break;
                case ProcedureDB.DeptUpdate:
                    ProcedureString = $"CALL Dept_Update({objD.Id}, {objD.Name})";
                    break;
                case ProcedureDB.DeptDelete:
                    ProcedureString = $"CALL Dept_Delete({objD.Id})";
                    break;
                case ProcedureDB.RoleInsert:
                    ProcedureString = $"CALL Role_Insert{objR.Id}, {objR.Name})";
                    break;
                case ProcedureDB.RoleUpdate:
                    ProcedureString = $"CALL Role_Update({objR.Id}, {objR.Name})";
                    break;
                case ProcedureDB.RoleDelete:
                    ProcedureString = $"CALL Role_Delete({objR.Id})";
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
            //User added = new User() { Departament = "", Role = "", Password = "Rick@", Login = "Mortimer", DepartamentId = 2, RoleId = 1, Id = 1 };
            //list.SetUserDetails(added, ProcedureDB.UserInsert);
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

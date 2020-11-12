using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WcfService
{
    //public class User
    //{
    //    public int Id { get; set; }
    //    public string Login { get; set; }
    //    public string Password { get; set; }
    //    public string Role { get; set; }
    //    public string Departament { get; set; }


    //}

    static class ConectToDb
    {
        static string connectionString = "Host=localhost;Username=postgres;Password=pa$$word;Database=Corvus";

        public static Dictionary<int, string> BecomeDictionaryFromBd(string NameTable)
        {
            Dictionary<int, string> departaments = new Dictionary<int, string>();
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                MySqlCommands scmd = new MySqlCommands();
                var sql = scmd.ReadAllInDb(NameTable);

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int id = rdr.GetInt32(0);
                            string value = rdr.GetString(1);
                            departaments.Add(id, value);
                        }
                    }
                }
                return departaments;
            }
        }
        public static List<User> BecomeListUsersFromBd(string NameTable)
        {

            List<User> users = new List<User>();
            User member = new User();
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                MySqlCommands scmd = new MySqlCommands();
                var sql = scmd.ReadAllInDb(NameTable);

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            member.Id = rdr.GetInt32(0);
                            member.Login = rdr.GetString(1);
                            member.Password = rdr.GetString(2);
                            member.Role = rdr.GetString(3);
                            member.Departament = rdr.GetString(3);

                        }
                    }
                    return users;

                }
            }
        }
        public static void CreateUser(User added, string NameTable)
        {
            FindLastId("Users");
            User member = added;
            string fields = $"'{added.Id}','{added.Login}','{added.Password}','{added.Role}','{added.Departament}'";
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                MySqlCommands scmd = new MySqlCommands();
                var sql = scmd.CreateInDb(NameTable, fields);
                using (var cmd = new NpgsqlCommand(sql, con))
                {

                    //cmd.Parameters.AddWithValue("name", "BMW");
                    //cmd.Parameters.AddWithValue("price", 36600);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void CreateFromDictionary(User added, string NameTable)
        {
            User member = added;
            string fields = $"'{added.Id}','{added.Login}','{added.Password}','{added.Role}','{added.Departament}'";
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                MySqlCommands scmd = new MySqlCommands();
                var sql = scmd.CreateInDb(NameTable, fields);
                using (var cmd = new NpgsqlCommand(sql, con))
                {

                    //cmd.Parameters.AddWithValue("name", "BMW");
                    //cmd.Parameters.AddWithValue("price", 36600);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static int FindLastId(string NameTable)
        {
            int lastId = 0;

            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sql = $"SELECT * FROM {NameTable} WHERE id=(SELECT max(id) FROM {NameTable})";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine(rdr.GetInt32(0));

                        }
                    }
                }
            }
            return lastId;
        }

        interface IMySqlComandsToDb
        {
            string CreateInDb(string TableName, string NewValue);
            string DeleteInDb(string TableName, string KeyValue, string Column);
            string UpdateInDb(string TableName, string KeyValue, string Column, string NewValue);
            string ReadAllInDb(string TableName);

        }
        public class MySqlCommands : IMySqlComandsToDb
        {
            public string CreateInDb(string TableName, string NewValue)
            {
                string src = $"INSERT INTO {TableName} VALUES({NewValue}); ";
                return src;
            }

            public string DeleteInDb(string TableName, string KeyValue, string Column)
            {
                string src = $"DELETE FROM {TableName} WHERE {KeyValue}={Column} ";
                return src;
            }

            public string ReadAllInDb(string TableName)
            {
                string src = $"SELECT * FROM {TableName}";
                return src;
            }

            public string UpdateInDb(string TableName, string KeyValue, string Column, string NewValue)
            {
                string src = $"UPDATE {TableName} SET {Column} = {NewValue} WERE {Column} = {KeyValue}";
                return src;
            }
        }
    }
}

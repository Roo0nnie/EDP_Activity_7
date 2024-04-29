using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Project_EDP
{
    internal class DatabaseConnection
    {
        public MySqlConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;

        public DatabaseConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "newschema";
            uid = "root";
            password = "ronnie123";
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
    }
}

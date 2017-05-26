using System;
using System.Data;
using MySql.Data.MySqlClient;
using StPierre.models;

namespace StPierre.database
{
    class Connection
    {
        private string _connectionString;
        public MySqlConnection MySqlConnection { get; private set; }

        /// <summary>
        /// Creates a new connection to the database
        /// </summary>
        /// <param name="creds">Credentials required for the database connection</param>
        public Connection(Credentials creds) {
            _connectionString = 
                "Data Source=" + creds.Host + ";" +
                "Initial Catalog=" + creds.Database + ";" +
                "User id=" + creds.Username + ";" +
                "Password=" + creds.Password + ";";
            MySqlConnection = new MySqlConnection(_connectionString);
        }

        public bool Open()
        {
            if (MySqlConnection.State != ConnectionState.Closed) return false;

            MySqlConnection.ConnectionString = _connectionString;
            try {
                this.MySqlConnection.Open();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        /// <summary>
        /// Closes the Sql connection if it is open.
        /// </summary>
        public void Close()
        {
            /* TODO: Check if connection.Close waits automatically for queries to end?
              while(
                this.MySqlConnection.State == ConnectionState.Executing || 
                this.MySqlConnection.State == ConnectionState.Fetching ||
                this.MySqlConnection.State == ConnectionState.Connecting ) {
                //Wait until the connection is not working anymore
                System.Threading.Thread.Sleep(50);
            } */
            this.MySqlConnection.CloseAsync();
        }

        public static bool TestConnection(Credentials creds)
        {
            if(creds == null)
            {
                return false;
            }
            else
            {
                return TestConnection(creds.Database, creds.Username, creds.Password, creds.Host);
            }
        }

        public static bool TestConnection(string db, string username, string password, string host)
        {
            string connectionString = 
                "Data Source=" + host + ";" +
                "Initial Catalog=" + db + ";" +
                "User id=" + username + ";" +
                "Password=" + password + ";";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try {
                connection.Open();
                connection.Close();
            } catch (Exception ex) {
                return false;
            }
            return true;
        }

        public bool TestConnection()
        {
            this.Close();
            return this.Open();
        }
    }
}

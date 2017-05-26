using System;
namespace StPierre.models
{
    [Serializable]
    class Credentials
    {
        public string Host;
        public string Database;
        public string Username;
        public string Password;

        /// <summary>
        /// Creates a new credentials object with the given information
        /// </summary>
        /// <param name="host">Database host</param>
        /// <param name="database">Database name</param>
        /// <param name="username">Database username</param>
        /// <param name="password">Database password</param>
        public Credentials(string host, string database, string username, string password)
        {
            this.Host = host;
            this.Database = database;
            this.Username = username;
            this.Password = password;
        }
    }
}

using System.Data;
using System.Data.SqlClient;

namespace Simbahan.Database
{
    public static class Connection
    {
        public static string ConnectionString { private get; set; }

        public static SqlConnection Get()
        {
            var connection = new SqlConnection(ConnectionString);

            if (connection.State == ConnectionState.Open)
                connection.Close();
            connection.Open();

            return connection;
        }
    }
}
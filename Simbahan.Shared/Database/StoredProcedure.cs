using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Simbahan.Database
{
    public class StoredProcedure : IDisposable
    {
        public StoredProcedure(string name)
        {
            Name = name;
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection = Connection.Get();

            SqlCommand = new SqlCommand(Name, SqlConnection)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = SqlConnection
            };
        }

        public string Name { get; set; }
        private SqlConnection SqlConnection { get; set; }
        public SqlCommand SqlCommand { get; set; }

        public void Dispose()
        {
            if (SqlConnection.State == ConnectionState.Open)
                SqlConnection.Close();
        }
    }
}
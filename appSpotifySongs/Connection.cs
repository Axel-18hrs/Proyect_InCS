using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appSpotifySongs
{
    internal class Connection
    {
        // Pueden surgir problemas porque mi maquina no tiene ssl
        // 

        private static SqlConnection connection;
        // Connection string
        private static void Connect()
        {
            string connectionString = @"Server=localhost\SQLEXPRESS;Database=Songs_Sp;User Id=sa;Password=dussk704;TrustServerCertificate=true;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        private static void Disconnect()
        {
            connection.Close();
        }
        // Execute a query
        public static void ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            Connect();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.ExecuteNonQuery();
            }
            Disconnect();
        }
        public static DataTable GetDataTable(string query, params SqlParameter[] parameters)
        {
            Connect();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                DataTable dataTable = new DataTable();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                Disconnect();
                return dataTable;
            }
        }


        // Retrieve a single value from a query
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            Connect();
            object result = null;
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                result = command.ExecuteScalar();
            }
            Disconnect();
            return result;
        }

    }
}

using System.Data.SqlClient;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    class Export
    {
        public static void exportTables()
        {
            string connectionString = @"server=(local)\SQLExpress;database=STUV4_0;integrated Security=SSPI;";

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                string queryStatement = "";

                using (SqlCommand cmd = new SqlCommand(queryStatement, connect))
                {
                    connect.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

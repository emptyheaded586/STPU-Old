using System.Data.SqlClient;
using System.IO;
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
                string queryStatement = "SELECT * FROM [dbo].[GlobalProtocols]";

                using (SqlCommand cmd = new SqlCommand(queryStatement, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"c:\temp\GlobalProtocols.csv"))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                              reader[0], reader[1], reader[2], reader[3], reader[4], 
                              reader[5], reader[6], reader[7], reader[8], reader[9]);
                        }
                    }
                    cmd.CommandText = "SELECT * FROM [dbo].[GlobalProtocolTreatments]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"c:\temp\GlobalProtocolTreatments.csv"))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1},{2},{3},{4}",
                              reader[0], reader[1], reader[2], reader[3], reader[4]);
                        }
                    }
                    cmd.CommandText = "SELECT * FROM [dbo].[UVATreatmentTypes]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"c:\temp\UVATreatmentTypes.csv"))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1}",
                              reader[0], reader[1]);
                        }
                    }
                    cmd.CommandText = "SELECT * FROM [dbo].[UVBTreatmentTypes]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"c:\temp\UVBTreatmentTypes.csv"))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1}",
                              reader[0], reader[1]);
                        }
                    }
                }
            }
        }
    }
}

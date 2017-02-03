using System.Data.SqlClient;
using System.IO;

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
                    using (StreamWriter writer = new StreamWriter(@"F:\GlobalProtocols.csv"))
                    {
                        writer.WriteLine("GlobalProtocolID,MachineTypeCode,UVATreatmentTypeCode,UVBTreatmentTypeCode,SkinTypeListID,ScheduleListID,FYIDoseLimit,AuthDoseLimit,FYIDiff,AuthDiff");
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                              reader[0], reader[1], reader[2], reader[3], reader[4], 
                              reader[5], reader[6], reader[7], reader[8], reader[9]);
                        }
                    }
                    cmd.CommandText = "SELECT * FROM [dbo].[GlobalProtocolTreatments]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"F:\GlobalProtocolTreatments.csv"))
                    {
                        writer.WriteLine("GlobalProtocolTreatmentID,GlobalProtocolID,TreatmentNumber,PrimaryDosage,SecondaryDosage");
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1},{2},{3},{4}",
                              reader[0], reader[1], reader[2], reader[3], reader[4]);
                        }
                    }
                    cmd.CommandText = "SELECT * FROM [dbo].[UVATreatmentTypes]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"F:\UVATreatmentTypes.csv"))
                    {
                        writer.WriteLine("UVATreatmentTypeCode,UVATreatmentTypeDescription");
                        while (reader.Read())
                        {
                            writer.WriteLine("{0},{1}",
                              reader[0], reader[1]);
                        }
                    }
                    cmd.CommandText = "SELECT * FROM [dbo].[UVBTreatmentTypes]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter(@"F:\UVBTreatmentTypes.csv"))
                    {
                        writer.WriteLine("UVBTreatmentTypeCode,UVBTreatmentTypeDescription");
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

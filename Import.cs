using System.Data.SqlClient;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Data;

namespace Smart_Touch_Protocol_Utility
{
    class Import
    {
        public static void importTables()
        {
            string connectionString = @"server=(local)\SQLExpress;database=STUV4_0;integrated Security=SSPI;";
            var dataTable = new DataTable();
            string dropQuery = File.ReadAllText(@"C:\Users\kparliment\Desktop\DropQuery.txt");
            string addQuery = File.ReadAllText(@"C:\Users\kparliment\Desktop\AddQuery.txt");

            using (SqlConnection connect = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connect;
                cmd.CommandText = dropQuery;
                connect.Open();
                cmd.ExecuteNonQuery();
                using (CsvReader csv = new CsvReader(new StreamReader(@"F:\UVATreatmentTypes.csv"), true))
                {
                    dataTable.Load(csv);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
                    {
                        bulkCopy.DestinationTableName = "dbo.UVATreatmentTypes";
                        bulkCopy.WriteToServer(dataTable);
                    }
                }
                cmd.CommandText = addQuery;
                cmd.ExecuteNonQuery();
            }
        }
    }
}

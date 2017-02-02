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

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[UVATreatmentType]([UVATreatmentTypeCode] [nvarchar](4) NOT NULL,[UVATreatmentTypeDescription] [nvarchar](25) NOT NULL)", connect))
                using (CsvReader csv = new CsvReader(new StreamReader(@"F:\UVATreatmentTypes.csv"), true))
                {
                    cmd.ExecuteNonQuery();
                    dataTable.Load(csv);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
                    {
                        bulkCopy.DestinationTableName = "dbo.UVATreatmentType";
                        bulkCopy.WriteToServer(dataTable);
                    }
                }
            }
        }
    }
}

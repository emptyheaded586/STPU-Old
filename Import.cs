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

            using (CsvReader csv = new CsvReader(new StreamReader(@"F:\UVATreatmentTypes.csv"), true))
            {
                dataTable.Load(csv);
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
                {
                    bulkCopy.DestinationTableName = "dbo.UVATreatmentTypes";
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }
    }
}

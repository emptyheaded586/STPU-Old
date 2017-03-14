using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Smart_Touch_Protocol_Utility.AddProtocols
{
    class TreatmentLimits
    {
        public static void treatLim(string uvCode)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            string tlQuery = "SELECT * FROM dbo.TreatmentLimits WHERE UVBTreatmentTypeCode = 'UVBH'";
            var tempTable = new DataTable();

            using (SqlConnection connect = new SqlConnection(sqlConnection))
            using (SqlDataAdapter da = new SqlDataAdapter(tlQuery, connect))
            {
                da.Fill(tempTable);
                for (int rowIndex = 0; rowIndex < tempTable.Rows.Count; rowIndex++)
                {
                    tempTable.Rows[rowIndex][1] = uvCode;
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    bulkCopy.DestinationTableName = "dbo.TreatmentLimits";
                    bulkCopy.WriteToServer(tempTable);
                }
            }
        }
    }
}
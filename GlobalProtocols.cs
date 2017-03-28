using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Smart_Touch_Protocol_Utility.AddProtocols
{
    class GlobalProtocols
    {
        public static void uvaGlobalProtocols(string uvCode)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            var num = gpID();
            var tempTable = new DataTable();
            string globalQuery = "SELECT MachineTypeCode,UVATreatmentTypeCode,UVBTreatmentTypeCode,SkinTypeListID," +
                "ScheduleListID,FYIDoseLimit,AuthDoseLimit,FYIDiff,AuthDiff " +
                "FROM GlobalProtocols WHERE UVATreatmentTypeCode = 'UVA'";

            using (SqlConnection connect = new SqlConnection(sqlConnection))
            using (SqlDataAdapter da = new SqlDataAdapter(globalQuery, connect))
            {
                // Fills the tempTable with pre-generated information from the database.
                da.Fill(tempTable);
                // Adds a column in the first position.
                tempTable.Columns.Add("GlobalProtocolID", typeof(int)).SetOrdinal(0);
                // Enters the last known GPID, increased by 1, into the first column then the user
                // defined code into the 3rd column incrementing the GPID by 1 to have unique ID's.
                for (int rowIndex = 0; rowIndex < tempTable.Rows.Count; rowIndex++)
                {
                    tempTable.Rows[rowIndex][0] = num;
                    tempTable.Rows[rowIndex][2] = uvCode;
                    num += 1;
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    bulkCopy.DestinationTableName = "dbo.GlobalProtocols";
                    bulkCopy.WriteToServer(tempTable);
                }
            }
        }

        public static void uvbGlobalProtocols(string uvCode)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            var num = gpID();
            var tempTable = new DataTable();
            string globalQuery = "SELECT MachineTypeCode,UVATreatmentTypeCode,UVBTreatmentTypeCode,SkinTypeListID," +
                "ScheduleListID,FYIDoseLimit,AuthDoseLimit,FYIDiff,AuthDiff " +
                "FROM GlobalProtocols WHERE UVBTreatmentTypeCode = 'UVBH'";

            using (SqlConnection connect = new SqlConnection(sqlConnection))
            using (SqlDataAdapter da = new SqlDataAdapter(globalQuery, connect))
            {
                da.Fill(tempTable);
                tempTable.Columns.Add("GlobalProtocolID", typeof(int)).SetOrdinal(0);
                for (int rowIndex = 0; rowIndex < tempTable.Rows.Count; rowIndex++)
                {
                    tempTable.Rows[rowIndex][0] = num;
                    tempTable.Rows[rowIndex][3] = uvCode;
                    num += 1;
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    bulkCopy.DestinationTableName = "dbo.GlobalProtocols";
                    bulkCopy.WriteToServer(tempTable);
                }
            }
        }

        // Get the last known GlobalProtocolID, increments it by 1 and returns the new GlobalProtocolID.
        public static int gpID()
        {
            int gpID;
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            string gpIDQuery = "SELECT TOP(1) GlobalProtocolID FROM GlobalProtocols ORDER BY GlobalProtocolID DESC";

            using (SqlConnection connect = new SqlConnection(sqlConnection))
            using (SqlCommand cmd = new SqlCommand(gpIDQuery, connect))
            {
                connect.Open();
                gpID = (int)cmd.ExecuteScalar() + 1;
            }
            return gpID;
        }
    }
}

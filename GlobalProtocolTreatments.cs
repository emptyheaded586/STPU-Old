using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Smart_Touch_Protocol_Utility.AddProtocols
{
    class GlobalProtocolTreatments
    {
        private static int gpIDLimitNum = 54;
        private static double dosageMultiplier = .01;

        public static void gptUVA(int gpID, int numTreat)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            DataTable dt = new DataTable();
            DataRow row;
            DosageAmounts doseWindow = new DosageAmounts(numTreat);

            doseWindow.ShowDialog();
            DataTable doseTable = DosageAmounts.userDoses;

            dt.Columns.Add("GlobalProtocolTreatmentID");
            dt.Columns.Add("GlobalProtocolID");
            dt.Columns.Add("TreatmentNumber");
            dt.Columns.Add("PrimaryDosage");
            dt.Columns.Add("SecondaryDosage");

            for (int x = gpID; x < (gpID + gpIDLimitNum); ++x)
            {
                for (int i = 0; i < numTreat; ++i)
                {
                    row = dt.NewRow();
                    row[1] = x;
                    row[2] = i + 1;
                    row[3] = doseTable.Rows[i][1].ToString();
                    row[4] = 0;
                    dt.Rows.Add(row);
                }
            }
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
            {
                bulkCopy.DestinationTableName = "dbo.GlobalProtocolTreatments";
                bulkCopy.WriteToServer(dt);
            }
        }

        public static void gptUVB(int gpID, int numTreat)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            DataTable dt = new DataTable();
            DataRow row;
            DosageAmounts doseWindow = new DosageAmounts(numTreat);

            doseWindow.ShowDialog();
            DataTable doseTable = DosageAmounts.userDoses;

            dt.Columns.Add("GlobalProtocolTreatmentID");
            dt.Columns.Add("GlobalProtocolID");
            dt.Columns.Add("TreatmentNumber");
            dt.Columns.Add("PrimaryDosage");
            dt.Columns.Add("SecondaryDosage");

            for (int x = gpID; x < (gpID + gpIDLimitNum); ++x)
            {
                for (int i = 0; i < numTreat; ++i)
                {
                    row = dt.NewRow();
                    row[1] = x;
                    row[2] = i + 1;
                    row[3] = Double.Parse(doseTable.Rows[i][1].ToString()) * dosageMultiplier;
                    row[4] = 0;
                    dt.Rows.Add(row);
                }
            }
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
            {
                bulkCopy.DestinationTableName = "dbo.GlobalProtocolTreatments";
                bulkCopy.WriteToServer(dt);
            }
        }
    }
}
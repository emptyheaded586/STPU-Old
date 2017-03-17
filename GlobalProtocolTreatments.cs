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
            double[] dosage = new double[numTreat];
            DosageAmounts win = new DosageAmounts(numTreat);

            win.ShowDialog();
            for (int x = 0; x < numTreat; ++x)
            {
                dosage[x] = Double.Parse(Microsoft.VisualBasic.Interaction.InputBox("Enter dosage amount for treatment #" + (x + 1), "UVA Dosage"));
            }

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
                    row[3] = dosage[i];
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
            double[] dosage = new double[numTreat];

            dosage[0] = (Double.Parse(Microsoft.VisualBasic.Interaction.InputBox("Enter the starting % of MED for treatment #" + (1) +
                "\nEnter value as a whole number (50% = 50)", "UVB Dosage"))) * dosageMultiplier;
            for (int x = 1; x < numTreat; ++x)
            {
                dosage[x] = (Double.Parse(Microsoft.VisualBasic.Interaction.InputBox("Enter the % increase for treatment #" + (x + 1) +
                    "\nEnter value as a whole number (10% = 10)", "UVB Dosage"))) * dosageMultiplier;
            }

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
                    row[3] = dosage[i];
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
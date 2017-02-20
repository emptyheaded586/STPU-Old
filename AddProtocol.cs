using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    class AddProtocol
    {
        public static void uvaTreatType(string uvCode, string uvDescription)
        {
            string uvQuery = "INSERT INTO UVATreatmentTypes (UVATreatmentTypeCode, UVATreatmentTypeDescription) VALUES (@uvCode, @uvDescription)";

            using (SqlConnection connect = new SqlConnection(MainWindow.sqlConnection()))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connect;
                cmd.CommandText = uvQuery;
                cmd.Parameters.AddWithValue("@uvCode", uvCode);
                cmd.Parameters.AddWithValue("@uvDescription", uvDescription);
                connect.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void uvbTreatType(string uvCode, string uvDescription)
        {
            string uvQuery = "INSERT INTO UVBTreatmentTypes (UVBTreatmentTypeCode, UVBTreatmentTypeDescription) VALUES (@uvCode, @uvDescription)";

            using (SqlConnection connect = new SqlConnection(MainWindow.sqlConnection()))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connect;
                cmd.CommandText = uvQuery;
                cmd.Parameters.AddWithValue("@uvCode", uvCode);
                cmd.Parameters.AddWithValue("@uvDescription", uvDescription);
                connect.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void uvaGlobalProtocols(string uvCode)
        {
            var num = gpID();
            var tempTable = new DataTable();
            string globalQuery = "SELECT MachineTypeCode,UVATreatmentTypeCode,UVBTreatmentTypeCode,SkinTypeListID," +
                "ScheduleListID,FYIDoseLimit,AuthDoseLimit,FYIDiff,AuthDiff " +
                "FROM GlobalProtocols WHERE UVATreatmentTypeCode = 'UVA'";

            using (SqlConnection connect = new SqlConnection(MainWindow.sqlConnection()))
            using (SqlDataAdapter da = new SqlDataAdapter(globalQuery, connect))
            {
                da.Fill(tempTable);
                tempTable.Columns.Add("GlobalProtocolID", typeof(int)).SetOrdinal(0);
                for (int rowIndex = 0; rowIndex < tempTable.Rows.Count; rowIndex++)
                {
                    tempTable.Rows[rowIndex][0] = num;
                    tempTable.Rows[rowIndex][2] = uvCode;
                    num += 1;
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
                {
                    bulkCopy.DestinationTableName = "dbo.GlobalProtocols";
                    bulkCopy.WriteToServer(tempTable);
                }
            }
        }

        public static void uvbGlobalProtocols(string uvCode)
        {
            var num = gpID();
            var tempTable = new DataTable();
            string globalQuery = "SELECT MachineTypeCode,UVATreatmentTypeCode,UVBTreatmentTypeCode,SkinTypeListID," +
                "ScheduleListID,FYIDoseLimit,AuthDoseLimit,FYIDiff,AuthDiff " +
                "FROM GlobalProtocols WHERE UVBTreatmentTypeCode = 'UVBH'";

            using (SqlConnection connect = new SqlConnection(MainWindow.sqlConnection()))
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
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
                {
                    bulkCopy.DestinationTableName = "dbo.GlobalProtocols";
                    bulkCopy.WriteToServer(tempTable);
                }
            }
        }

        public static int gpID()
        {
            int gpID;
            string gpIDQuery = "SELECT TOP(1) GlobalProtocolID FROM GlobalProtocols ORDER BY GlobalProtocolID DESC";

            using (SqlConnection connect = new SqlConnection(MainWindow.sqlConnection()))
            using (SqlCommand cmd = new SqlCommand(gpIDQuery, connect))
            {
                connect.Open();
                gpID = (int)cmd.ExecuteScalar() + 1;
            }
            return gpID;
        }

        public static void gptUVATable(int gpID, int numTreat)
        {
            DataTable dt = new DataTable();
            DataRow row;
            double[] dosage = new double[numTreat];
            
            for (int x = 0; x < numTreat; ++x)
            {
                dosage[x] = Double.Parse(Microsoft.VisualBasic.Interaction.InputBox("Enter dosage amount for treatment #" + (x + 1), "UVA Dosage"));
            }

            dt.Columns.Add("GlobalProtocolTreatmentID");
            dt.Columns.Add("GlobalProtocolID");
            dt.Columns.Add("TreatmentNumber");
            dt.Columns.Add("PrimaryDosage");
            dt.Columns.Add("SecondaryDosage");

            for (int x = gpID; x < (gpID + 54); ++x)
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
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
            {
                bulkCopy.DestinationTableName = "dbo.GlobalProtocolTreatments";
                bulkCopy.WriteToServer(dt);
            }
        }

        public static void gptUVBTable(int gpID, int numTreat)
        {
            DataTable dt = new DataTable();
            DataRow row;
            double[] dosage = new double[numTreat];

            dosage[0] = (Double.Parse(Microsoft.VisualBasic.Interaction.InputBox("Enter the starting % of MED for treatment #" + (1) +
                "\nEnter value as a whole number (50% = 50)", "UVB Dosage"))) * .01;
            for (int x = 1; x < numTreat; ++x)
            {
                dosage[x] = (Double.Parse(Microsoft.VisualBasic.Interaction.InputBox("Enter the % increase for treatment #" + (x + 1) +
                    "\nEnter value as a whole number (10% = 10)", "UVB Dosage"))) * .01;
            }

            dt.Columns.Add("GlobalProtocolTreatmentID");
            dt.Columns.Add("GlobalProtocolID");
            dt.Columns.Add("TreatmentNumber");
            dt.Columns.Add("PrimaryDosage");
            dt.Columns.Add("SecondaryDosage");

            for (int x = gpID; x < (gpID + 54); ++x)
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
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
            {
                bulkCopy.DestinationTableName = "dbo.GlobalProtocolTreatments";
                bulkCopy.WriteToServer(dt);
            }
        }

        public static void resourceUVAEdit(string uvCode, string uvDescrip)
        {
            string path = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\English.txt";
            
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("uvatreatmenttypes.uvatreatmenttypedescription." + uvCode + " = " + uvDescrip);
            }

            ProcessStartInfo resgen = new ProcessStartInfo();
            resgen.Verb = "runas";
            resgen.FileName = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\Resgen.exe";
            resgen.Arguments = "English.txt";
            Process.Start(resgen);
        }

        public static void resourceUVBEdit(string uvCode, string uvDescrip)
        {
            string path = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\English.txt";

            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("uvbtreatmenttypes.uvbtreatmenttypedescription." + uvCode + " = " + uvDescrip);
            }

            ProcessStartInfo resgen = new ProcessStartInfo();
            resgen.Verb = "runas";
            resgen.FileName = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\Resgen.exe";
            resgen.Arguments = "English.txt";
            Process.Start(resgen);
        }
    }
}
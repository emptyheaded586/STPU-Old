using System.Data.SqlClient;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Data;
using System;
using System.Windows.Forms;

namespace Smart_Touch_Protocol_Utility
{
    class Import
    {
        public static void importTables()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dataTable = new DataTable();
            string addPath = Path.Combine(Environment.CurrentDirectory, @"Queries\", "AddQuery.txt");
            string dropPath = Path.Combine(Environment.CurrentDirectory, @"Queries\", "DropQuery.txt");
            string addQuery = File.ReadAllText(addPath);
            string dropQuery = File.ReadAllText(dropPath);

            fbd.ShowDialog();
            using (SqlConnection connect = new SqlConnection(MainWindow.sqlConnection()))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connect;
                cmd.CommandText = dropQuery;
                connect.Open();
                cmd.ExecuteNonQuery();
                using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\GlobalProtocols.csv"), true))
                {
                    dataTable.Load(csv);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
                    {
                        bulkCopy.DestinationTableName = "dbo.GlobalProtocols";
                        bulkCopy.WriteToServer(dataTable);
                    }
                    dataTable.Reset();
                }
                using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\GlobalProtocolTreatments.csv"), true))
                {
                    dataTable.Load(csv);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
                    {
                        bulkCopy.DestinationTableName = "dbo.GlobalProtocolTreatments";
                        bulkCopy.WriteToServer(dataTable);
                    }
                    dataTable.Reset();
                }
                using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\UVATreatmentTypes.csv"), true))
                {
                    dataTable.Load(csv);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
                    {
                        bulkCopy.DestinationTableName = "dbo.UVATreatmentTypes";
                        bulkCopy.WriteToServer(dataTable);
                    }
                    dataTable.Reset();
                }
                using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\UVBTreatmentTypes.csv"), true))
                {
                    dataTable.Load(csv);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(MainWindow.sqlConnection()))
                    {
                        bulkCopy.DestinationTableName = "dbo.UVBTreatmentTypes";
                        bulkCopy.WriteToServer(dataTable);
                    }
                    dataTable.Reset();
                }
                cmd.CommandText = addQuery;
                cmd.ExecuteNonQuery();
                fbd.Dispose();
            }
        }
    }
}

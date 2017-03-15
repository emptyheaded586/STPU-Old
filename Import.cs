using System.Data.SqlClient;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Data;
using System;
using System.Windows.Forms;
using System.Configuration;

namespace Smart_Touch_Protocol_Utility
{
    class Import
    {
        public static void importTables()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dataTable = new DataTable();
            string addPath = Path.Combine(Environment.CurrentDirectory, @"Queries\", "AddQuery.txt");
            string dropPath = Path.Combine(Environment.CurrentDirectory, @"Queries\", "DropQuery.txt");
            string addQuery = File.ReadAllText(addPath);
            string dropQuery = File.ReadAllText(dropPath);

            fbd.ShowNewFolderButton = false;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (SqlConnection connect = new SqlConnection(sqlConnection))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connect;
                    cmd.CommandText = dropQuery;
                    connect.Open();
                    cmd.ExecuteNonQuery();

                    using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\GlobalProtocols.csv"), true))
                    {
                        dataTable.Load(csv);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                        {
                            bulkCopy.DestinationTableName = "dbo.GlobalProtocols";
                            bulkCopy.WriteToServer(dataTable);
                        }
                        dataTable.Reset();
                    }

                    using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\GlobalProtocolTreatments.csv"), true))
                    {
                        dataTable.Load(csv);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                        {
                            bulkCopy.DestinationTableName = "dbo.GlobalProtocolTreatments";
                            bulkCopy.WriteToServer(dataTable);
                        }
                        dataTable.Reset();
                    }

                    using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\UVATreatmentTypes.csv"), true))
                    {
                        dataTable.Load(csv);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                        {
                            bulkCopy.DestinationTableName = "dbo.UVATreatmentTypes";
                            bulkCopy.WriteToServer(dataTable);
                        }
                        dataTable.Reset();
                    }

                    using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\UVBTreatmentTypes.csv"), true))
                    {
                        dataTable.Load(csv);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                        {
                            bulkCopy.DestinationTableName = "dbo.UVBTreatmentTypes";
                            bulkCopy.WriteToServer(dataTable);
                        }
                        dataTable.Reset();
                    }

                    using (CsvReader csv = new CsvReader(new StreamReader(fbd.SelectedPath + @"\TreatmentLimits.csv"), true))
                    {
                        dataTable.Load(csv);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                        {
                            bulkCopy.DestinationTableName = "dbo.TreatmentLimits";
                            bulkCopy.WriteToServer(dataTable);
                        }
                        dataTable.Reset();
                    }

                    cmd.CommandText = addQuery;
                    cmd.ExecuteNonQuery();
                    fbd.Dispose();
                    MessageBox.Show("Import Complete", "Import");
                }
            }
            else if (result == DialogResult.Cancel)
            {
                fbd.Dispose();
                return;
            }
        }
    }
}

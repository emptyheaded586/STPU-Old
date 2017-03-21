using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace Smart_Touch_Protocol_Utility
{
    class Export
    {
        public static void exportTables()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (SqlConnection connect = new SqlConnection(sqlConnection))
                {
                    int numColumns;
                    string queryStatement = "SELECT * FROM [dbo].[GlobalProtocols]";

                    using (SqlCommand cmd = new SqlCommand(queryStatement, connect))
                    {
                        connect.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        using (StreamWriter writer = new StreamWriter(fbd.SelectedPath + @"\GlobalProtocols.csv"))
                        {
                            numColumns = reader.FieldCount;

                            for (int x = 0; x < numColumns; ++x)
                            {
                                writer.Write(reader.GetName(x));
                                if (x < (numColumns - 1))
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                            while (reader.Read())
                            {
                                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                  reader[0], reader[1], reader[2], reader[3], reader[4],
                                  reader[5], reader[6], reader[7], reader[8], reader[9]);
                            }
                        }

                        cmd.CommandText = "SELECT * FROM [dbo].[GlobalProtocolTreatments]";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        using (StreamWriter writer = new StreamWriter(fbd.SelectedPath + @"\GlobalProtocolTreatments.csv"))
                        {
                            numColumns = reader.FieldCount;

                            for (int x = 0; x < numColumns; ++x)
                            {
                                writer.Write(reader.GetName(x));
                                if (x < (numColumns - 1))
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                            while (reader.Read())
                            {
                                writer.WriteLine("{0},{1},{2},{3},{4}",
                                  reader[0], reader[1], reader[2], reader[3], reader[4]);
                            }
                        }

                        cmd.CommandText = "SELECT * FROM [dbo].[UVATreatmentTypes]";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        using (StreamWriter writer = new StreamWriter(fbd.SelectedPath + @"\UVATreatmentTypes.csv"))
                        {
                            numColumns = reader.FieldCount;

                            for (int x = 0; x < numColumns; ++x)
                            {
                                writer.Write(reader.GetName(x));
                                if (x < (numColumns - 1))
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                            while (reader.Read())
                            {
                                writer.WriteLine("{0},{1}",
                                  reader[0], reader[1]);
                            }
                        }

                        cmd.CommandText = "SELECT * FROM [dbo].[UVBTreatmentTypes]";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        using (StreamWriter writer = new StreamWriter(fbd.SelectedPath + @"\UVBTreatmentTypes.csv"))
                        {
                            numColumns = reader.FieldCount;

                            for (int x = 0; x < numColumns; ++x)
                            {
                                writer.Write(reader.GetName(x));
                                if (x < (numColumns - 1))
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                            while (reader.Read())
                            {
                                writer.WriteLine("{0},{1}",
                                  reader[0], reader[1]);
                            }
                        }

                        cmd.CommandText = "SELECT * FROM [dbo].[TreatmentLimits]";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        using (StreamWriter writer = new StreamWriter(fbd.SelectedPath + @"\TreatmentLimits.csv"))
                        {
                            numColumns = reader.FieldCount;

                            for (int x = 0; x < numColumns; ++x)
                            {
                                writer.Write(reader.GetName(x));
                                if (x < (numColumns - 1))
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                            while (reader.Read())
                            {
                                writer.WriteLine("{0},{1},{2},{3},{4}",
                                  reader[0], reader[1], reader[2], reader[3], reader[4]);
                            }
                        }
                        fbd.Dispose();
                        MessageBox.Show("Export Complete", "Export");
                    }
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
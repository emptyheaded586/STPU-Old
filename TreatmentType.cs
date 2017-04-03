using System.Data.SqlClient;
using System.Configuration;

namespace Smart_Touch_Protocol_Utility.AddProtocols
{
    class TreatmentType
    {
        /// <summary>
        /// Adds the user defined UVA uvCode and UVA uvDescription into the SQL Database.
        /// </summary>
        /// <param name="uvCode"></param>
        /// <param name="uvDescription"></param>
        public static void uvaTreatType(string uvCode, string uvDescription)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            string uvQuery = "INSERT INTO UVATreatmentTypes (UVATreatmentTypeCode, UVATreatmentTypeDescription) VALUES (@uvCode, @uvDescription)";

            using (SqlConnection connect = new SqlConnection(sqlConnection))
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

        /// <summary>
        /// Adds the user defined UVB uvCode and UVB uvDescription into the SQL Database.
        /// </summary>
        /// <param name="uvCode"></param>
        /// <param name="uvDescription"></param>
        public static void uvbTreatType(string uvCode, string uvDescription)
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            string uvQuery = "INSERT INTO UVBTreatmentTypes (UVBTreatmentTypeCode, UVBTreatmentTypeDescription) VALUES (@uvCode, @uvDescription)";

            using (SqlConnection connect = new SqlConnection(sqlConnection))
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
    }
}
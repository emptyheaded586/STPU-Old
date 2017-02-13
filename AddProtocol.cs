using System.Data.SqlClient;

namespace Smart_Touch_Protocol_Utility
{
    class AddProtocol
    {
        public static void uvaTreatType(string uvCode, string uvDescription)
        {
            string connectionString = @"server=(local)\SQLExpress;database=STUV4_0;integrated Security=SSPI;";
            string uvQuery = "INSERT INTO UVATreatmentTypes (UVATreatmentTypeCode, UVATreatmentTypeDescription) VALUES (@uvCode, @uvDescription)";

            using (SqlConnection connect = new SqlConnection(connectionString))
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
            string connectionString = @"server=(local)\SQLExpress;database=STUV4_0;integrated Security=SSPI;";
            string uvQuery = "INSERT INTO UVBTreatmentTypes (UVBTreatmentTypeCode, UVBTreatmentTypeDescription) VALUES (@uvCode, @uvDescription)";

            using (SqlConnection connect = new SqlConnection(connectionString))
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
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ST10263992.Models
{
    public class LoginModel
    {
        public static string con_string =
            "Server=tcp:st10263992.database.windows.net,1433;Initial Catalog=st10263992Database;Persist Security Info=False;User ID=ConnorGray;Password=Dexter3772!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int SelectUser(string email, string name)
        {
            int userId = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql =
                    "SELECT userID FROM userTable WHERE userEmail = @Email AND userName = @Name";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Name", name);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    // For now, rethrow the exception
                    throw ex;
                }
            }
            return userId;
        }
    }
}

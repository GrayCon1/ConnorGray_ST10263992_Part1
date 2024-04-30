using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace st10263992.Models
{
    public class LoginModel
    {
        public static string con_string = "Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = test; Data Source = labVMH8OX\\SQLEXPRESS";

        public int SelectUser(string email, string password)
        {
            int userID = -1;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT userID FROM tblUser WHERE userEmail = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                //can use try catch
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    userID = Convert.ToInt32(result);
                }
                return userID;
            }


        }
    }
}
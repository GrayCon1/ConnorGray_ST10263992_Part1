using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Cloud1.Controllers;

public class UserProfileController : Controller
{
    public ActionResult Index()
    {
        ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
        ViewData["User"] = GetCurrentUser();
        return View();
    }

    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    private UserModel GetCurrentUser()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "SELECT userName, userEmail, userPassword FROM tblUser WHERE userID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, con);

            con.Open();
            cmd.Parameters.AddWithValue("@UserID", userId);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                return new UserModel
                {
                    Name = rdr["userName"].ToString(),
                    Email = rdr["userEmail"].ToString(),
                    Password = ""
                };
            }
            con.Close();
        }

        return null;
    }
}
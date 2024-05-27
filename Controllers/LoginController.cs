using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cloud1.Controllers;

public class LoginController : Controller
{

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(UserModel user)
    {
        int success = LoginUser(user);
        if (success != -1)
        {
            HttpContext.Session.SetInt32("UserID", success);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }
    }

    private int LoginUser(UserModel user)
    {
        int userId = -1;
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql =
                "SELECT userID FROM tblUser WHERE userEmail = @Email AND userPassword = @Password";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            con.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            con.Close();
        }
        return userId;
    }
}


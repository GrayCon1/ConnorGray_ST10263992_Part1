using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cloud1.Controllers;

public class SignUpController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult SignUp(UserModel user)
    {
        AddUser(user);
        return RedirectToAction("Index", "Home");
    }

    private void AddUser(UserModel user)
    {
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql =
                "INSERT INTO tblUser (userName, userEmail, userPassword) VALUES (@Name, @Email, @Password)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            con.Open();
            int userId = cmd.ExecuteNonQuery();
            HttpContext.Session.SetInt32("UserID", userId);
            con.Close();
        }
    }
}


﻿using ST10263992.Models;
using Microsoft.AspNetCore.Mvc;

namespace ST10263992.Controllers
{
    public class SignUpController : Controller
    {
        private readonly SignUpModel SignUp;
        public SignUpController(){
            SignUp= new SignUpModel();
        }
        public ActionResult Privacy(string email, string name)
        {
            var loginModel = new LoginModel();
            int userID = loginModel.SelectUser(email, name);
            if (userID != -1)
            {
                // User found, proceed with login logic (e.g., set authentication cookie)
                // For demonstration, redirecting to a dummy page
                return RedirectToAction("Index", "Home", new { userID = userID });
            }
            else
            {
                // User not found, handle accordingly (e.g., show error message)
                return View("LoginFailed");
            }
        }
    }
}

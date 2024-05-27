using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cloud1.Controllers;

public class ProductController : Controller
{
   
    [HttpPost]
    public ActionResult MyWork(Product products)
    {
        
        return RedirectToAction("Index", "Home");
    }

    public ActionResult Index()
    {
        string[] images = new string[10];
        for (int i = 0; i < images.Length; i++)
        {
            images[i] = "/Images/Product_" + (i + 1) + ".jpg"; 
        }
        ViewData["ProductImages"] = images;
        ViewData["Products"] = GetAllProducts();
        ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
        return View();
    }

    private List<Product> GetAllProducts()
    {
        List<Product> products = new List<Product>();

        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "SELECT * FROM tblProduct";
            SqlCommand cmd = new SqlCommand(sql, con);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Product product = new Product
                {
                    ProductID = Convert.ToInt32(rdr["productID"]),
                    Name = rdr["productName"].ToString(),
                    Price = rdr["productPrice"].ToString(),
                    Category = rdr["productCategory"].ToString(),
                    Availability = rdr["productAvailability"].ToString()
                };

                products.Add(product);
            }
            con.Close();
        }

        return products;
    }
}


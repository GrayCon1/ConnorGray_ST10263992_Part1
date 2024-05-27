using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Cloud1.Controllers;

public class CartController : Controller
{

    private readonly ILogger<CartController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CartController(ILogger<CartController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public ActionResult Index()
    {
        ViewData["CartItems"] = GetCartItems();
        return View();
    }

    private List<CartModel> GetCartItems()
    {
        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
        List<CartModel> cartItems = new List<CartModel>();

        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "SELECT tblCart.productID, tblProduct.productName AS productName, tblProduct.productPrice AS productPrice " +
                         "FROM tblCart " +
                         "JOIN tblProduct ON tblCart.productID = tblProduct.productID " +
                         "WHERE tblCart.userID = @UserID";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CartModel item = new CartModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = reader["ProductPrice"].ToString()
                    };
                    cartItems.Add(item);
                }
            }
        }

        return cartItems;
    }

    [HttpPost]
    public IActionResult AddToCart(int productID)
    {
        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "INSERT INTO tblCart (userID, productID) VALUES (@UserID, @ProductID)";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    public ActionResult RemoveCartItem(int productId)
    {
        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "DELETE FROM tblCart WHERE userID = @UserID AND productID = @ProductID";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ProductID", productId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index", "Cart");
    }

    public ActionResult ClearCart()
    {
        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "DELETE FROM tblCart WHERE userID = @UserID";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index", "Cart");
    }
}
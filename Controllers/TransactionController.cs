using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Cloud1.Controllers;

public class TransactionController : Controller
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TransactionController(ILogger<TransactionController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public ActionResult Index()
    {
        ViewData["Transactions"] = GetAllTransactions();
        return View();
    }

    private List<TransactionModel> GetAllTransactions()
    {
        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
        List<TransactionModel> transactions = new List<TransactionModel>();

        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            string sql = "SELECT tblTransaction.productID, tblProduct.productName AS productName, tblProduct.productPrice AS productPrice " +
                         "FROM tblTransaction " +
                         "JOIN tblProduct ON tblTransaction.productID = tblProduct.productID " +
                         "WHERE tblTransaction.userID = @UserID";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TransactionModel item = new TransactionModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = reader["ProductPrice"].ToString()
                    };
                    transactions.Add(item);
                }
            }
        }

        return transactions;
    }

    public ActionResult InsertTransactions()
    {
        int? userID = HttpContext?.Session?.GetInt32("UserID");
        using (SqlConnection con = new SqlConnection(Util.CON_STRING))
        {
            con.Open();
            string sql = "SELECT productID  FROM tblCart WHERE userID = @UserID";
            List<CartModel> cartItems = new List<CartModel>();

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartModel item = new CartModel
                        {
                            ProductID = Convert.ToInt32(reader["productID"])
                        };
                        cartItems.Add(item);
                    }
                }
            }

            foreach (var item in cartItems)
            {
                string insertSql = "INSERT INTO tblTransaction (userID, productID) VALUES (@UserID, @ProductID)";
                using (SqlCommand cmd = new SqlCommand(insertSql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                    cmd.ExecuteNonQuery();
                }
            }

            // Clear the cart after placing the order
            string deleteCartSql = "DELETE FROM tblCart WHERE userID = @UserID";
            using (SqlCommand cmd = new SqlCommand(deleteCartSql, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index", "Transaction");
    }
}

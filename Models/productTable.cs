using System.Data.SqlClient;

namespace ST10263992.Models
{
    public class ProductTable
    {
        public static string con_string =
            "Server=tcp:st10263992.database.windows.net,1433;Initial Catalog=st10263992Database;Persist Security Info=False;User ID=ConnorGray;Password=Dexter3772!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(con_string);
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public bool Availability { get; set; }

        public int InsertProduct()
        {
            try
            {
                string sql =
                    "INSERT INTO tblProduct (productName, productPrice, productCategory, productAvailability) VALUES (@Name, @Price, @Category, @Availability)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Availability", Availability);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }
        }

        // Method to retrieve all products from the database
        public static List<ProductTable> GetAllProducts()
        {
            List<ProductTable> products = new List<ProductTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM tblProduct";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProductTable product = new ProductTable();
                    product.ProductID = Convert.ToInt32(rdr["productID"]);
                    product.Name = rdr["productName"].ToString();
                    product.Price = rdr["productPrice"].ToString();
                    product.Category = rdr["productCategory"].ToString();
                    product.Availability = (bool)rdr["productAvailability"];

                    products.Add(product);
                }
                con.Close();
            }

            return products;
        }
    }
}

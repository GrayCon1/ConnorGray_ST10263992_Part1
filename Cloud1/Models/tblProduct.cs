using st10263992.Models;
using System.Data.SqlClient;

namespace st10263992.Models
{
    public class tblProduct
    {
        public static string con_string = "Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = test; Data Source = labVMH8OX\\SQLEXPRESS";

        //public static string con_string = "Server=tcp:cldv-sql.database.windows.net,1433;Initial Catalog=cldv-sql-database;Persist Security Info=False;User ID=ConnorG;Password=Dexter3772!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Category { get; set; }

        public string Availability { get; set; }
        public int insert_product(tblProduct p)
        {

            try
            {
                string sql = "INSERT INTO tblProduct (Name, Price, Category, Availability) VALUES (@Name, @Price, @Category, @Availability)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
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
        public int delete_product(tblProduct p)
        {
            try
            {
                string sql = "DELETE FROM tblProduct (Name, Price, Category, Availability) VALUES (@Name, @Price, @Category, @Availability)";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
        // Method to retrieve all products from the database
        public static List<tblProduct> GetAllProducts()
        {
            List<tblProduct> products = new List<tblProduct>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    tblProduct product = new tblProduct();
                    product.ProductID = Convert.ToInt32(rdr["productID"]);
                    product.Name = rdr["productName"].ToString();
                    product.Price = rdr["productPrice"].ToString();
                    product.Category = rdr["productCategory"].ToString();
                    product.Availability = rdr["productAvailability"].ToString();

                    products.Add(product);
                }
            }

            return products;
        }
    }
}

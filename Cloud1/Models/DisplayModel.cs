using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace st10263992.Models
{
    public class DisplayModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool Availability { get; set; }

        public DisplayModel() { }

        //Parameterized Constructor: This constructor takes five parameters (id, name, price, category, availability) and initializes the corresponding properties of ProductDisplayModel with the provided values.
        public DisplayModel(int id, string name, decimal price, string category, bool availability)
        {
            ProductID = id;
            Name = name;
            Price = price;
            Category = category;
            Availability = availability;
        }

        public static List<DisplayModel> SelectProducts()
        {
            List<DisplayModel> products = new List<DisplayModel>();

            string con_string = "Integrated Security=SSPI;Persist Security Info=False;User ID=\"\";Initial Catalog=test;Data Source=labVMH8OX\\SQLEXPRESS";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductID, Name, Price, Category, Availability FROM tblProduct";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DisplayModel product = new DisplayModel();
                    product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    product.Name = Convert.ToString(reader["Name"]);
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Category = Convert.ToString(reader["Category"]);
                    product.Availability = Convert.ToBoolean(reader["Availability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}

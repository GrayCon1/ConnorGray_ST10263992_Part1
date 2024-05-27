using System.Data.SqlClient;

namespace Cloud1.Models
{
    public class Product
    {
        public Product(int id, string name, string price, string category, string availability)
        {
            ProductID = id;
            Name = name;
            Price = price;
            Category = category;
            Availability = availability;
        }

        public Product() { }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
    }
}

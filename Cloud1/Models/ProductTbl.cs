using System.Data.SqlClient;

namespace Cloud1.Models{
    public class productTbl{
        public static string con_string ="Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = test; Data Source = labVMH8OX\\SQLEXPRESS";

        public static SqlConnection con = new SqlConnection(con_string);

        public string Name{get;set;}
        public string Price{get;set;}
        public string Category{get;set;}
        public string Availabilty{get;set;}

        public int insert_product(productTbl p){
            //try and catch
            string SqlQuery = "INSERT INTO productTable (productName, productPrice, productCategory, productAvailability) VALUES (@Name, @Price, @Category, @Availability)";
            SqlCommand cmd = new SqlCommand(SqlQuery,con);
            cmd.Parameters.AddWithValue("@Name",p.Name);
            cmd.Parameters.AddWithValue("@Price",p.Price);
            cmd.Parameters.AddWithValue("@Category",p.Category);
            cmd.Parameters.AddWithValue("@Availability",p.Availabilty);
            con.Close();
            int rowsAffected=cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }
    }
}
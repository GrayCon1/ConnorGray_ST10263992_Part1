﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace st10263992.Models
{
    public class tblUser
    {
        public static string con_string = "Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = test; Data Source = labVMH8OX\\SQLEXPRESS";

        //public static string con_string = "Server=tcp:cldv-sql.database.windows.net,1433;Initial Catalog=cldv-sql-database;Persist Security Info=False;User ID=ConnorG;Password=Dexter3772!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con =new SqlConnection(con_string);

        public string Name { get;set;}
        public string Surname { get; set; }
        public string Email { get;set;}
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public int insert_User(tblUser m)
        {
            try
            {
                string sql = "INSERT INTO tblUser (Name, Surname, Email, Password, Phone) VALUES (@Name, @Surname, @Email, @Password, @Phone)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Surname", m.Surname);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                cmd.Parameters.AddWithValue("@Password",m.Password);
                cmd.Parameters.AddWithValue("@Phone", m.PhoneNumber);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

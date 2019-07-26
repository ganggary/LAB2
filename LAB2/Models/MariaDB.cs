using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class MariaDB
    {
        public static SqlConnection GetConnection()
        {
            //string ConnectionString = "Data Source=SOFTDEV; Initial Catalog=Marina; Integrated Security=true;";
            string ConnectionString = GetConnectionString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            return conn;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
        }

    }
}
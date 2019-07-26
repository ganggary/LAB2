using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Windows.Forms;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace LAB2.Models
{
    public class CustomerDB
    {
        public static Customer CheckLogin(string stUserName, string stPassword)
        {
            SqlConnection conn = new SqlConnection();
            Customer CustomerObj = new Customer();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[FirstName],[LastName],[Phone],[City],[UserName],[Password] FROM [dbo].[Customer]" +
                    " WHERE [UserName] = @UserName AND [Password] = @Password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserName", stUserName);
                cmd.Parameters.AddWithValue("@Password", stPassword);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    CustomerObj.ID = Convert.ToInt32(dr["ID"]);
                    CustomerObj.FirstName = dr["FirstName"].ToString();
                    CustomerObj.LastName = Convert.ToString(dr["LastName"]);
                    CustomerObj.Phone = dr["Phone"].ToString();
                    CustomerObj.City = dr["City"].ToString();
                    CustomerObj.UserName = dr["UserName"].ToString();
                    CustomerObj.Password = dr["Password"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return CustomerObj;
        }

        public static List<Customer> GetAllCustomer()
        {
            SqlConnection conn = new SqlConnection();
            List<Customer> lstCustomer = new List<Customer>();
            Customer CustomerObj = new Customer();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[FirstName],[LastName],[Phone],[City],[UserName],[Password] FROM [dbo].[Customer]";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CustomerObj.ID = Convert.ToInt32(dr["ID"]);
                    CustomerObj.FirstName = dr["FirstName"].ToString();
                    CustomerObj.LastName = Convert.ToString(dr["LastName"]);
                    CustomerObj.Phone = dr["Phone"].ToString();
                    CustomerObj.City = dr["City"].ToString();
                    CustomerObj.UserName = dr["UserName"].ToString();
                    CustomerObj.Password = dr["Password"].ToString();

                    lstCustomer.Add(CustomerObj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return lstCustomer;
        }

        public static Customer GetCustomer(int ID)
        {
            SqlConnection conn = new SqlConnection();
            Customer CustomerObj = new Customer();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[FirstName],[LastName],[Phone],[City],[UserName],[Password] FROM [dbo].[Customer]" +
                    " WHERE [ID]= " + ID;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CustomerObj.ID = Convert.ToInt32(dr["ID"]);
                    CustomerObj.FirstName = dr["FirstName"].ToString();
                    CustomerObj.LastName = Convert.ToString(dr["LastName"]);
                    CustomerObj.Phone = dr["Phone"].ToString();
                    CustomerObj.City = dr["City"].ToString();
                    CustomerObj.UserName = dr["UserName"].ToString();
                    CustomerObj.Password = dr["Password"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return CustomerObj;
        }

        public static Int32 AddCustomer(int ID, string FirstName, string LastName, string Phone, string City, string UserName, string Password)
        {
            /*
            MessageBox.Show("FirstName=" + FirstName);
            MessageBox.Show("LastName=" + LastName);
            MessageBox.Show("Phone=" + Phone);
            MessageBox.Show("City=" + City);
            MessageBox.Show("UserName=" + UserName);
            MessageBox.Show("Password=" + Password);
            */

            Int32 inSupplierID = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "INSERT INTO [dbo].[Customer]" +
                    " ([FirstName],[LastName],[Phone],[City],[UserName],[Password]) " +
                    " VALUES(  @FirstName1,   @LastName1 ,   @Phone1,   @City1,   @UserName1,   @Password1) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName1", FirstName.ToString());
                cmd.Parameters.AddWithValue("@LastName1", LastName.ToString());
                cmd.Parameters.AddWithValue("@Phone1", Phone.ToString());
                cmd.Parameters.AddWithValue("@City1", City.ToString());
                cmd.Parameters.AddWithValue("@UserName1", UserName.ToString());
                cmd.Parameters.AddWithValue("@Password1", PasswordHashed(Password).ToString());
                inSupplierID = cmd.ExecuteNonQuery();

                //if succeeded, it should set a CustomerID SESSION for other programs to use
                if (inSupplierID >= 1)
                {
                    string sql1 = "select ID from [dbo].[Customer] where UserName = @UserName1 and Password = @Password1 ";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd.Parameters.AddWithValue("@UserName1", UserName.ToString());
                    cmd.Parameters.AddWithValue("@Password1", PasswordHashed(Password).ToString());

                    int CustomerId = (Int32)cmd1.ExecuteScalar();
                    System.Web.HttpContext.Current.Session["CustomerID"] = Convert.ToString(CustomerId);
                    System.Web.HttpContext.Current.Session["CustomerFirstname"] = FirstName;
                    System.Web.HttpContext.Current.Session["CustomerLastName"] = LastName;
                    System.Web.HttpContext.Current.Session["CustomerFullName"] = FirstName + " " + LastName;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error : " + ex.ToString());
                    }
            finally
            {
                conn.Close();
            }
            return inSupplierID;
        }

        public static Int32 AddCustomer(Customer objCustomer)
        {
            Int32 inCustomerId = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "INSERT INTO [dbo].[Customer]" +
                    " ([FirstName],[LastName],[Phone],[City],[UserName],[Password]) " +
                    " VALUES(@FirstName,@LastName,@Phone,@City,@UserName,@Password)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", objCustomer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", objCustomer.LastName);
                cmd.Parameters.AddWithValue("@Phone", objCustomer.Phone);
                cmd.Parameters.AddWithValue("@City", objCustomer.City);
                cmd.Parameters.AddWithValue("@UserName", objCustomer.UserName);
                cmd.Parameters.AddWithValue("@Password", objCustomer.Password);
                inCustomerId = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return inCustomerId;
        }

        public static Int32 UpdateCustomer(int ID, string FirstName, string LastName, string Phone, string City)
        {
            Int32 inCustomerId = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "UPDATE [dbo].[Customer] SET" +
                    " FirstName = @FirstName" +
                    " LastName = @LastName" +
                    " Phone = @Phone" +
                    " City = @City" +
                    " WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@City", City);
                inCustomerId = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            return inCustomerId;
        }

        public static Int32 UpdateCustomer(Customer objCustomer)
        {
            Int32 inSupplierId = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "UPDATE [dbo].[Customer] SET" +
                    " FirstName = @FirstName" +
                    " LastName = @LastName" +
                    " Phone = @Phone" +
                    " City = @City" +
                    " WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", objCustomer.ID);
                cmd.Parameters.AddWithValue("@FirstName", objCustomer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", objCustomer.LastName);
                cmd.Parameters.AddWithValue("@Phone", objCustomer.Phone);
                cmd.Parameters.AddWithValue("@City", objCustomer.City);
                inSupplierId = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            return inSupplierId;
        }

        public static Int32 DeleteCustomer(Int32 ID)
        {
            Int32 inSupplierId = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "DELETE FROM [dbo].[Customer]" +
                    " WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", ID);

                inSupplierId = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            return inSupplierId;
        }

        //passwordhash
        public static string PasswordHashed(string password1)
        {
            //string HashedPassword;

            //Create the salt value with a cryptographic PRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //Create the Rfc2898DeriveBytes and get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password1, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //Combine the salt and password bytes for later use:
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //Turn the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            //DBContext.AddUser(new User { ..., Password = savedPasswordHash });

            //get hashedpassword
            /* Fetch the stored value */
            //string savedPasswordHash = DbContext.GetUser(u => u.UserName == user).Password;

            return savedPasswordHash;
        }



        //get customerID and CustomerFullName based on User.Identity.Name
        public static List<Customer> GetCustomerIDFullName()
        {
            List<Customer> lstCust = new List<Customer>();

            //string Username = User.Identity.Name;
            string username = Convert.ToString(HttpContext.Current.Session["UserName"]);

            MessageBox.Show("username = " + username);

            //Get customerID and CustomerFullName from database
            Customer CustomerObj = new Customer();

            if (username != null)
            {
                //Int32 inSupplierID = 0;
                SqlConnection conn = new SqlConnection();
                try
                {
                    conn = MariaDB.GetConnection();
                    string sql = "SELECT ID, FirstName, LastName FROM [dbo].[Customer]" +
                        " WHERE UserName = @UserName ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserName", username.ToString());

                    SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        CustomerObj.ID = Convert.ToInt32(dr["ID"]);
                        CustomerObj.FirstName = dr["FirstName"].ToString();
                        CustomerObj.LastName = Convert.ToString(dr["LastName"]);
                        /*
                        CustomerObj.Phone = dr["Phone"].ToString();
                        CustomerObj.City = dr["City"].ToString();
                        CustomerObj.UserName = dr["UserName"].ToString();
                        CustomerObj.Password = dr["Password"].ToString();
                        */
                        lstCust.Add(CustomerObj);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
                finally
                {
                    conn.Close();
                }

                }


            return lstCust; 
        }
        //copy data from AspNetUsers to Customer
        public static void CopyDataFromAspNetUsersToCustomers()
        {
            //put Customer data into a List
            List<Customer> customers = new List<Customer>();
            Customer CustomerObj = new Customer();
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[FirstName],[LastName],[Phone],[City],[UserName],[Password] FROM [dbo].[Customer]";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CustomerObj.ID = Convert.ToInt32(dr["ID"]);
                    CustomerObj.FirstName = dr["FirstName"].ToString();
                    CustomerObj.LastName = Convert.ToString(dr["LastName"]);
                    CustomerObj.Phone = dr["Phone"].ToString();
                    CustomerObj.City = dr["City"].ToString();
                    CustomerObj.UserName = dr["UserName"].ToString();
                    CustomerObj.Password = dr["Password"].ToString();

                    customers.Add(CustomerObj);
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            //put AspNetUsers data into a List
            List<AspNetUser> aspnetUsers = new List<AspNetUser>();
            AspNetUser aspnetuserobj = new AspNetUser();
            conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [Id],[FirstName],[LastName],[Phone],[City],[UserName],[Password] FROM [dbo].[AspNetUsers]";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    aspnetuserobj.Id = Convert.ToInt32(dr["Id"]);
                    aspnetuserobj.FirstName = dr["FirstName"].ToString();
                    aspnetuserobj.LastName = Convert.ToString(dr["LastName"]);
                    aspnetuserobj.Phone = dr["Phone"].ToString();
                    aspnetuserobj.City = dr["City"].ToString();
                    aspnetuserobj.UserName = dr["UserName"].ToString();
                    aspnetuserobj.PasswordHash = dr["PasswordHash"].ToString();

                    aspnetUsers.Add(aspnetuserobj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            //compare two lists, if username and password are identical, then ignore, if not, add the record into Customers
            for(int i=0; i<aspnetUsers.Count; i++)
            {
                for(int j = 0; j < customers.Count; j++)
                {
                    //if(customers[j][5] == )
                }
            }


        }

        private static void FillCustomerList(ref List<Customer> lt1, ref List<AspNetUser> lt2)
        {
            /*
            lt1 = new List<Employee> {new Employee{ID=1,Name="Kavya",Age="24",Address="No.1,Nehru Street,Chennai",ContactNo="9874521456"},
            new Employee{ID=2,Name="Ravi",Age="24",Address="Flat No.25/A1,Gandhi Street,Chennai",ContactNo="9658745258"},
            new Employee{ID=3,Name="Lavnya",Age="30",Address="No.12,Shastri nagar,Chennai",ContactNo="5214587896"},
            new Employee{ID=4,Name="Rupa",Age="31",Address="No.23/5,Nehru Street,Chennai",ContactNo="9874521256"},
            new Employee{ID=5,Name="Divya",Age="32",Address="No.1/227,Nehru Street,Chennai",ContactNo="8541256387"},
            };

            lt2 = new List<Employee> {new Employee{ID=1,Name="Kavya",Age="24",Address="No.1,Nehru Street,Chennai",ContactNo="9874521456"},
            new Employee{ID=2,Name="Ravindran",Age="30",Address="Flat No.25/A1,Gandhi Street,Chennai",ContactNo="9658745258"},
            new Employee{ID=3,Name="Chandru",Age="30",Address="No.12,Shastri nagar,Chennai",ContactNo="5214587896"},
            new Employee{ID=4,Name="Rakesh",Age="32",Address="No.23/5,Nehru Street,Chennai",ContactNo="9874021256"},
            new Employee{ID=5,Name="Suresh",Age="32",Address="No.1/227,Nehru Street,Chennai",ContactNo="8541056387"},
            new Employee{ID=11,Name="Suryakala",Age="28",Address="No.1,Pillayar koil Street,Chennai",ContactNo="9541204782"},
            new Employee{ID=12,Name="Thivya",Age="41",Address="No.42,Ellaiamman koil Street,Chennai",ContactNo="9632140874"},
            };
            */
        }

        protected List<Customer> ListCompare(List<Customer> lt1, List<AspNetUser> lt2)
        {
            //FillCustomerList(ref lt1, ref lt2);
            List<Customer> lst = new List<Customer>();
            /*
            if (lt1.Count > 0 && lt2.Count > 0)
            {
                // Displaying Matching Records from List1 and List2 by Username

                var result = (from l1 in lt1
                              join l2 in lt2
                              on l1.UserName equals l2.UserName  
                              orderby l1.ID
                              select new
                              {

                                  ID = l1.ID,
                                  Name = (l1.Name == l2.Name) ? "$" : (l2.Name + " (Modified)"),
                                  Age = (l1.Age == l2.Age) ? "$" : (l2.Age + " (Modified)"),
                                  Address = (l1.Address == l2.Address) ? "$" : (l2.Address + " (Modified)"),
                                  ContactNo = (l1.ContactNo == l2.ContactNo) ? "$" : (l2.ContactNo + " (Modified)")
                              }).ToList();

                // Displaying Records from List1 which is not in List2
                var result1 = from l1 in lt1
                              where !(from l2 in lt2
                                      select l2.ID).Contains(l1.ID)
                              orderby l1.ID
                              select new
                              {
                                  ID = l1.ID,
                                  Name = " Deleted",
                                  Age = " Deleted",
                                  Address = " Deleted",
                                  ContactNo = " Deleted"
                              };

                // Displaying Records from List1 which is not in List2
                var result2 = from l1 in lt2
                              where !(from l2 in lt1
                                      select l2.ID).Contains(l1.ID)
                              orderby l1.ID
                              select new
                              {
                                  ID = l1.ID,
                                  Name = l1.Name + " (Added)",
                                  Age = l1.Age + " (Added)",
                                  Address = l1.Address + " (Added)",
                                  ContactNo = l1.ContactNo + " (Added)"
                              };

                var res1 = result.Concat(result1).Concat(result2);

                foreach (var item in res1)
                {
                    Employee emp = new Employee();
                    //Response.Write(item + "<br/>");
                    emp.ID = item.ID;
                    emp.Name = item.Name;
                    emp.Age = item.Age;
                    emp.Address = item.Address;
                    emp.ContactNo = item.ContactNo;
                    lst.Add(emp);
                }
            }*/
            return lst;
        }
    }
}
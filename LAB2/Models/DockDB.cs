using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace LAB2.Models
{
    public class DockDB
    {
        /*
        public static DataSet GetAllDock()
        {
            SqlConnection conn = new SqlConnection();
            DataSet ds = new DataSet();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[Name],[WaterService],[ElectricalService] FROM [dbo].[Dock]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        */
        public static List<Dock> GetAllDock()
        {
            List<Dock> dockList = new List<Dock>();
            string sql = " SELECT [ID], [Name], [WaterService], [ElectricalService] FROM [dbo].[Dock] ";
            using (SqlConnection con = new SqlConnection(MariaDB.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    Dock dock;
                    while (dr.Read())
                    {
                        dock = new Dock();
                        dock.ID = Convert.ToInt32(dr["ID"]);
                        dock.Name = dr["Name"].ToString();
                        dock.WaterService = Convert.ToBoolean(dr["WaterService"]);
                        dock.ElectricalService = Convert.ToBoolean(dr["ElectricalService"]);
                        

                        dockList.Add(dock);
                    }
                    dr.Close();
                }
            }
            return dockList;
        }

        public static Dock GetDock(int ID)
        {
            SqlConnection conn = new SqlConnection();
            Dock DockObj = new Dock();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[Name],[WaterService],[ElectricalService] FROM [dbo].[Dock]" +
                    " WHERE [ID]=@ID ";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                cmd.Parameters.AddWithValue("@ID", ID);

                while (dr.Read())
                {
                    DockObj.ID = Convert.ToInt32(dr["ID"]);
                    DockObj.Name = dr["Name"].ToString();
                    DockObj.WaterService = Convert.ToBoolean(dr["WaterService"]);
                    DockObj.ElectricalService = Convert.ToBoolean(dr["ElectricalService"]);
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
            return DockObj;
        }

        //delete dock
        public static Dock DelDock(int ID)
        {
            SqlConnection conn = new SqlConnection();
            Dock DockObj = new Dock();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "DELETE FROM [dbo].[Dock]" +
                    " WHERE [ID]=@ID ";
                SqlCommand cmd = new SqlCommand(sql, conn);

                
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DockObj;
        }

        public static void AddDock(string name, bool waterService, bool electricalService)
        {
            Int32 inID = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "INSERT INTO [dbo].[Dock]" +
                    " ( [Name],[WaterService],[ElectricalService]) " +
                    " VALUES(@Name,@WaterService,@ElectricalService)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@WaterService", waterService);
                cmd.Parameters.AddWithValue("@ElectricalService", electricalService);
                inID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {

                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            
        }

        //Update dock
        public static void UpdateDock(int intid, string name, bool waterService, bool electricalService)
        {
            Int32 inID = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "UPDATE [dbo].[Dock]" +
                    " ( [Name],[WaterService],[ElectricalService]) " +
                    " VALUES(@Name,@WaterService,@ElectricalService) WHERE ID=@intId ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@intId", intid);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@WaterService", waterService);
                cmd.Parameters.AddWithValue("@ElectricalService", electricalService);
                inID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

        }


        //public static Int32 AddCustomer(Customer objCustomer)
        //{
        //    Int32 inCustomerId = 0;
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        conn = MariaDB.GetConnection();
        //        string sql = "INSERT INTO [dbo].[Customer]" +
        //            " ([ID], [FirstName],[LastName],[Phone],[City]) " +
        //            " VALUES(@ID,@FirstName,@LastName,@Phone,@City)";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@ID", objCustomer.ID);
        //        cmd.Parameters.AddWithValue("@FirstName", objCustomer.FirstName);
        //        cmd.Parameters.AddWithValue("@LastName", objCustomer.LastName);
        //        cmd.Parameters.AddWithValue("@Phone", objCustomer.Phone);
        //        cmd.Parameters.AddWithValue("@City", objCustomer.City);
        //        inCustomerId = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return inCustomerId;
        //}

        //public static Int32 UpdateCustomer(int ID, string FirstName, string LastName, string Phone, string City)
        //{
        //    Int32 inCustomerId = 0;
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        conn = MariaDB.GetConnection();
        //        string sql = "UPDATE [dbo].[Customer] SET" +
        //            " FirstName = @FirstName" +
        //            " LastName = @LastName" +
        //            " Phone = @Phone" +
        //            " City = @City" +
        //            " WHERE ID = @ID";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@ID", ID);
        //        cmd.Parameters.AddWithValue("@FirstName", FirstName);
        //        cmd.Parameters.AddWithValue("@LastName", LastName);
        //        cmd.Parameters.AddWithValue("@Phone", Phone);
        //        cmd.Parameters.AddWithValue("@City", City);
        //        inCustomerId = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return inCustomerId;
        //}

        //public static Int32 UpdateCustomer(Customer objCustomer)
        //{
        //    Int32 inSupplierId = 0;
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        conn = MariaDB.GetConnection();
        //        string sql = "UPDATE [dbo].[Customer] SET" +
        //            " FirstName = @FirstName" +
        //            " LastName = @LastName" +
        //            " Phone = @Phone" +
        //            " City = @City" +
        //            " WHERE ID = @ID";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@ID", objCustomer.ID);
        //        cmd.Parameters.AddWithValue("@FirstName", objCustomer.FirstName);
        //        cmd.Parameters.AddWithValue("@LastName", objCustomer.LastName);
        //        cmd.Parameters.AddWithValue("@Phone", objCustomer.Phone);
        //        cmd.Parameters.AddWithValue("@City", objCustomer.City);
        //        inSupplierId = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return inSupplierId;
        //}

        //public static Int32 DeleteCustomer(Int32 ID)
        //{
        //    Int32 inSupplierId = 0;
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        conn = MariaDB.GetConnection();
        //        string sql = "DELETE FROM [dbo].[Customer]" +
        //            " WHERE ID = @ID";
        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@ID", ID);

        //        inSupplierId = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return inSupplierId;
        //}
    }
}
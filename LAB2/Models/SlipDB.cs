using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace LAB2.Models
{
    public  class SlipDB
    {
        public static List<Slip> GetAllAvailableDock()
        {
            SqlConnection conn = new SqlConnection();
            List<Slip> lstSlip = new List<Slip>();
            Slip SlipObj;
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [Slip].[ID] AS SlipID,[Slip].[Width],[Slip].[Length],[Slip].[DockID] " + //,[Dock].[Name] AS DockName" +
                                " FROM [dbo].[Slip]" +
                                " JOIN [dbo].[Dock] ON [dbo].[Dock].[ID] = [dbo].[Slip].[DockID]" +
                                " WHERE [Slip].[ID] NOT IN(SELECT[Lease].[SlipID] FROM [dbo].[Lease] WHERE [Lease].[SlipID] = [Slip].[ID])";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    SlipObj = new Slip();
                    SlipObj.ID = Convert.ToInt32(dr["SlipID"]);
                    SlipObj.Width = Convert.ToInt32(dr["Width"]);
                    SlipObj.Length = Convert.ToInt32(dr["Length"]);
                    SlipObj.DockID = Convert.ToInt32(dr["DockID"]);
                    //SlipObj.DockName = Convert.ToString(dr["DockName"]);

                    lstSlip.Add(SlipObj);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return lstSlip;
        }

        public static DataSet GetAllLease(int inCustomerID)
        {
            SqlConnection conn = new SqlConnection();
            DataSet ds = new DataSet();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [Lease].[ID],[Slip].[Width],[Slip].[Length],[Slip].[DockID],[SlipID],[CustomerID],Customer.FirstName + ' ' + Customer.LastName AS FullName, [Dock].Name" +
                            " FROM [dbo].[Lease] " +
                            " JOIN [dbo].[Customer] ON [dbo].[Customer].ID = [dbo].[Lease].CustomerID " +
                            " JOIN [dbo].[Slip] ON [dbo].[Slip].ID = [dbo].[Lease].SlipID " +
                            " JOIN [dbo].[Dock] ON [dbo].[Dock].ID = [dbo].[Slip].DockID " +
                            " WHERE [CustomerID] = @CustomerID ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CustomerID", inCustomerID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex) {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static List<SlipAndLease> GetAllHistoryLease()
        {
            //get customerID from session
            int inCustomerID = Convert.ToInt32(HttpContext.Current.Session["CustomerID"]);
            SqlConnection conn = new SqlConnection();
            //DataSet ds = new DataSet();
            List<SlipAndLease> lstSlipLease = new List<SlipAndLease>();
            SlipAndLease SlipLeaseObj;

            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [Lease].[ID] as LeaseID,[Slip].[Width],[Slip].[Length],[Slip].[DockID],[Dock].Name as Dockname,[Dock].WaterService as DockWaterService, [Dock].ElectricalService as DockElectricalService, [SlipID],[CustomerID],Customer.FirstName + ' ' + Customer.LastName AS FullName " +
                            " FROM [dbo].[Lease] " +
                            " JOIN [dbo].[Customer] ON [dbo].[Customer].ID = [dbo].[Lease].CustomerID " +
                            " JOIN [dbo].[Slip] ON [dbo].[Slip].ID = [dbo].[Lease].SlipID " +
                            " JOIN [dbo].[Dock] ON [dbo].[Dock].ID = [dbo].[Slip].DockID " +
                            " WHERE [CustomerID] = @CustomerID ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CustomerID", inCustomerID);
                
                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    SlipLeaseObj = new SlipAndLease();
                    SlipLeaseObj.LeaseID = Convert.ToInt32(dr["ID"]);
                    SlipLeaseObj.Width = Convert.ToInt32(dr["Width"]);
                    SlipLeaseObj.Length = Convert.ToInt32(dr["Length"]);
                    SlipLeaseObj.DockID = Convert.ToInt32(dr["DockID"]);
                    SlipLeaseObj.DockName = Convert.ToString(dr["DockName"]);
                    SlipLeaseObj.WaterService = Convert.ToBoolean(dr["DockWaterService"]);
                    SlipLeaseObj.ElectricalService = Convert.ToBoolean(dr["DockElectricalService"]);
                    SlipLeaseObj.SlipID = Convert.ToInt32(dr["SlipID"]);
                    SlipLeaseObj.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                    SlipLeaseObj.FullName = Convert.ToString(dr["FullName"]);


                    lstSlipLease.Add(SlipLeaseObj);
                }


                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return lstSlipLease;
        }

        public static SlipAndLease GetSlipAndLeaseByID(string SlipID)
        {
            //write one record into hold table.
            WriteSlipAndLeaseByID(SlipID);


            SlipAndLease product = null;
            string sql = "SELECT [Slip].[ID] as SlipID,[Slip].[Width],[Slip].[Length],[Slip].[DockID],[Dock].Name as Dockname,[Dock].WaterService as DockWaterService, [Dock].ElectricalService as DockElectricalService " + //, [Customer].[ID] as CustomerID, Customer.FirstName + ' ' + Customer.LastName AS FullName " +
                            " FROM [dbo].[Slip] " +
                            //" JOIN [dbo].[Customer] ON [dbo].[Customer].ID = [dbo].[Lease].CustomerID " +
                            //" JOIN [dbo].[Slip] ON [dbo].[Slip].ID = [dbo].[Lease].SlipID " +
                            " JOIN [dbo].[Dock] ON [dbo].[Dock].ID = [dbo].[Slip].DockID " +
                            " WHERE [Slip].[ID] = @SlipID ";

            using (SqlConnection con = new SqlConnection(MariaDB.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    int SlipID1 = Convert.ToInt32(SlipID);
                    cmd.Parameters.AddWithValue("@SlipID", SlipID1);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        product = new SlipAndLease();
                        product.SlipID = Convert.ToInt32(dr["SlipID"]);
                        product.Width = Convert.ToInt32(dr["Width"]);
                        product.Length = Convert.ToInt32(dr["Length"]);
                        product.DockID = Convert.ToInt32(dr["DockID"]);
                        product.DockName = dr["DockName"].ToString();
                        product.WaterService = Convert.ToBoolean(dr["DockWaterService"]);
                        product.ElectricalService = Convert.ToBoolean(dr["DockElectricalService"]);
                        //product.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                        //product.FullName = Convert.ToString(dr["FullName"]);
                    }
                    dr.Close();
                }
            }
            return product;
        }

        public static SlipAndLease WriteSlipAndLeaseByID(string SlipID)
        {
            

            //read info from slip table
            SlipAndLease product = null;
            string sql = "SELECT [Slip].[ID] as SlipID,[Slip].[Width],[Slip].[Length],[Slip].[DockID],[Dock].Name as Dockname,[Dock].WaterService as DockWaterService, [Dock].ElectricalService as DockElectricalService " + //, [Customer].[ID] as CustomerID, Customer.FirstName + ' ' + Customer.LastName AS FullName " +
                            " FROM [dbo].[Slip] " +
                            //" JOIN [dbo].[Customer] ON [dbo].[Customer].ID = [dbo].[Lease].CustomerID " +
                            //" JOIN [dbo].[Slip] ON [dbo].[Slip].ID = [dbo].[Lease].SlipID " +
                            " JOIN [dbo].[Dock] ON [dbo].[Dock].ID = [dbo].[Slip].DockID " +
                            " WHERE [Slip].[ID] = @SlipID ";

            using (SqlConnection con = new SqlConnection(MariaDB.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    int SlipID1 = Convert.ToInt32(SlipID);
                    cmd.Parameters.AddWithValue("@SlipID", SlipID1);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        product = new SlipAndLease();
                        product.SlipID = Convert.ToInt32(dr["SlipID"]);
                        product.Width = Convert.ToInt32(dr["Width"]);
                        product.Length = Convert.ToInt32(dr["Length"]);
                        product.DockID = Convert.ToInt32(dr["DockID"]);
                        product.DockName = dr["DockName"].ToString();
                        product.WaterService = Convert.ToBoolean(dr["DockWaterService"]);
                        product.ElectricalService = Convert.ToBoolean(dr["DockElectricalService"]);
                        //product.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                        //product.FullName = Convert.ToString(dr["FullName"]);
                    }
                    dr.Close();
                }
            }

            //get CustomerID and customer FullName from Session
            int CustID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CustomerID"]);
            string CustFullName = Convert.ToString(System.Web.HttpContext.Current.Session["CustomerFullName"]);

            if((CustID<=0) || (CustFullName==""))
            {
                List<Customer> cust = new List<Customer>();
                cust=CustomerDB.GetCustomerIDFullName();
                foreach(var cus in cust)
                {
                    CustID = cus.ID;
                    CustFullName = cus.FirstName + " " + cus.LastName;
                }

            }

            //insert into table 

            string sql1 = "INSERT INTO [dbo].[SlipAndLease]" +
                    " ([SlipID],[Width],[Length],[DockID],[DockName],[LeaseID],[CustomerID],[FullName]) " +
                    " VALUES (@SlipID, @Width, @Length, @DockID, @DockName, @LeaseID, @CustomerID, @FullName) ";




            return product;

           

        }



        public static List<SlipDock> GetAllHoldLease()
        {
            //get CustomerID vaia SESSION

            
            SqlConnection conn = new SqlConnection();
            List<SlipDock> lstSlip = new List<SlipDock>();
            SlipDock SlipDockObj;

            //DataSet ds = new DataSet();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [Slip].[ID] AS SlipID,[Slip].[Width],[Slip].[Length],[Slip].[DockID] ,[Dock].[Name] AS DockName, [Dock].[WaterService] as DockWaterService, [Dock].[ElectricalService] as DockElectricalService " +
                                " FROM [dbo].[Slip]" +
                                " JOIN [dbo].[Dock] ON [dbo].[Dock].[ID] = [dbo].[Slip].[DockID]" +
                                " WHERE [Slip].[ID] NOT IN(SELECT[Lease].[SlipID] FROM [dbo].[Lease] WHERE [Lease].[SlipID] = [Slip].[ID])";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@CustomerID", inCustomerID);
                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    SlipDockObj = new SlipDock();
                    SlipDockObj.ID = Convert.ToInt32(dr["SlipID"]);
                    SlipDockObj.Width = Convert.ToInt32(dr["Width"]);
                    SlipDockObj.Length = Convert.ToInt32(dr["Length"]);
                    SlipDockObj.DockID = Convert.ToInt32(dr["DockID"]);
                    SlipDockObj.DockName = Convert.ToString(dr["DockName"]);
                    SlipDockObj.WaterService = Convert.ToBoolean(dr["DockWaterService"]);
                    SlipDockObj.ElectricalService = Convert.ToBoolean(dr["DockElectricalService"]);

                    lstSlip.Add(SlipDockObj);
                }
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return lstSlip;
        }



        public static Slip GetSlip(int ID)
        {
            SqlConnection conn = new SqlConnection();
            Slip SlipObj = new Slip();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [ID],[Width],[Length],[DockID] FROM [dbo].[Slip]" +
                    " WHERE [ID]= " + ID;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    SlipObj.ID = Convert.ToInt32(dr["ID"]);
                    SlipObj.Width = Convert.ToInt32(dr["Width"].ToString());
                    SlipObj.Length = Convert.ToInt32(dr["Length"]);
                    SlipObj.DockID = Convert.ToInt32(dr["DockID"]);
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
            return SlipObj;
        }

        public static DataTable GetSlipByDockID(int DockID)
        {
            SqlConnection conn = new SqlConnection();
            DataTable dt = new DataTable();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "SELECT [Slip].[ID] AS SlipID,[Slip].[Width],[Slip].[Length],[Slip].[DockID],[Dock].[Name] AS DockName" +
                                " FROM [dbo].[Slip]" +
                                " JOIN [dbo].[Dock] ON [dbo].[Dock].[ID] = [dbo].[Slip].[DockID]" +
                                " WHERE [Slip].[ID] NOT IN(SELECT[Lease].[SlipID] FROM [dbo].[Lease] WHERE [Lease].[SlipID] = [Slip].[ID])" +
                                " AND [Slip].[DockID] != @DockID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@DockID", DockID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }



        public static Int32 AddSlip(int ID, string Width, string Length, string DockID)
        {
            Int32 inID = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "INSERT INTO [dbo].[Slip]" +
                    " ([ID],[Width],[Length],[DockID]) " +
                    " VALUES(@ID,@Width,@Length,@DockID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Width", Width);
                cmd.Parameters.AddWithValue("@Length", Length);
                cmd.Parameters.AddWithValue("@DockID", DockID);
                inID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return inID;
        }

    }
}
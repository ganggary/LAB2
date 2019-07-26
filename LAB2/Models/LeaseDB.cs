using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class LeaseDB
    {
        public static Int32 AddLease(Lease objLease)
        {
            Int32 inLeaseId = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = MariaDB.GetConnection();
                string sql = "INSERT INTO [dbo].[Lease]" +
                    " ([SlipID],[CustomerID]) " +
                    " VALUES(@SlipID,@CustomerID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SlipID", objLease.SlipID);
                cmd.Parameters.AddWithValue("@CustomerID", objLease.CustomerID);

                inLeaseId = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            return inLeaseId;
        }
    }
}
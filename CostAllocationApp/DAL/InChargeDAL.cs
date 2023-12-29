using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class InChargeDAL : DbContext
    {
        public int CreateInCharge(InCharge inCharge)
        {
            int result = 0;
            string query = $@"insert into InCharges(Name,CreatedBy,CreatedDate,IsActive) values(@inChargeName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@inChargeName", inCharge.InChargeName);
                cmd.Parameters.AddWithValue("@createdBy", inCharge.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", inCharge.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", inCharge.IsActive);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }

        public int UpdateIncharge(InCharge inCharge)
        {
            int result = 0;
            string query = "";
            query = query + "UPDATE InCharges ";
            query = query + "SET Name = N'" + inCharge.InChargeName + "',UpdatedBy=N'" + inCharge.UpdatedBy + "',UpdatedDate='" + inCharge.UpdatedDate + "',IsActive=1";
            query = query + "WHERE Id = " + inCharge.Id + " ";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return result = 0;
                }

                return result;
            }

        }

        public List<InCharge> GetAllInCharges()
        {
            List<InCharge> inCharges = new List<InCharge>();
            string query = "";
            query = query + "SELECT inch.Id 'InchargeId',inch.Name 'InchargeName',inch.CreatedDate,inch.CreatedBy ";
            query = query + "FROM InCharges inch ";
            query = query + "WHERE inch.isactive = 1 ";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            InCharge inCharge = new InCharge();
                            inCharge.Id = Convert.ToInt32(rdr["InchargeId"]);
                            inCharge.InChargeName = rdr["InchargeName"].ToString();
                            inCharge.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            inCharge.CreatedBy = rdr["CreatedBy"].ToString();

                            inCharges.Add(inCharge);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return inCharges;
            }
        }

        public int RemoveInCharge(int inChargeIds)
        {
            int result = 0;            
            string query = $@"DELETE FROM InCharges WHERE id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", inChargeIds);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }

        public bool CheckInCharge(string incharegeName)
        {
            string query = "select * from InCharges where Name=N'" + incharegeName + "'";
            bool result = false;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }

        public int GetInChargeCountWithEmployeeAsignment(int inChargeId)
        {
            string query = "select * from EmployeesAssignments where InChargeId=" + inChargeId;
            int result = 0;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            result++;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public InCharge GetInChargeByInChargeId(int inChargeId)
        {
            InCharge incharge = null;
            string query = "select * from InCharges where Id = " + inChargeId;
            bool result = false;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            incharge = new InCharge();
                            incharge.InChargeName = rdr["Name"].ToString();
                            incharge.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    incharge = null;
                }

                return incharge;
            }
        }

        public int GetInchargeIdByName(string inchargeName)
        {
            int inchargeId = 0;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(inchargeName))
            {
                where += $" Name =N'{inchargeName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM InCharges
                            where {where}";

                using (SqlConnection sqlConnection = this.GetConnection())
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    try
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                inchargeId = Convert.ToInt32(rdr["Id"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Response.Write(ex);
                        HttpContext.Current.Response.End();
                    }
                }

            }

            return inchargeId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class ActualCostDAL:DbContext
    {
        public List<ActualCost> GetActualCostsByYear(int year)
        {
            List<ActualCost> actualCosts = new List<ActualCost>();

            string query = $@"select * from ActualCosts where Year={year}";


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
                            ActualCost actualCost = new ActualCost();
                            actualCost.Id = Convert.ToInt32(rdr["Id"]);
                            actualCost.AssignmentId = Convert.ToInt32(rdr["AssignmentId"]);
                            actualCost.Year = Convert.ToInt32(rdr["Year"]);
                            actualCost.OctCost = Convert.ToDouble(rdr["OctCost"]);
                            actualCost.NovCost = Convert.ToDouble(rdr["NovCost"]);
                            actualCost.DecCost = Convert.ToDouble(rdr["DecCost"]);
                            actualCost.JanCost = Convert.ToDouble(rdr["JanCost"]);
                            actualCost.FebCost = Convert.ToDouble(rdr["FebCost"]);
                            actualCost.MarCost = Convert.ToDouble(rdr["MarCost"]);
                            actualCost.AprCost = Convert.ToDouble(rdr["AprCost"]);
                            actualCost.MayCost = Convert.ToDouble(rdr["MayCost"]);
                            actualCost.JunCost = Convert.ToDouble(rdr["JunCost"]);
                            actualCost.JulCost = Convert.ToDouble(rdr["JulCost"]);
                            actualCost.AugCost = Convert.ToDouble(rdr["AugCost"]);
                            actualCost.SepCost = Convert.ToDouble(rdr["SepCost"]);



                            actualCosts.Add(actualCost);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return actualCosts;



        }

        public bool CheckAssignmentId(int assignmentId, int year)
        {
            bool result = false;
            string query = "select * from ActualCosts where AssignmentId=" + assignmentId + " and year = " + year;
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

        public int CreateActualCost(ActualCost actualCost)
        {
            int result = 0;
            string query = $@"insert into ActualCosts(AssignmentId,Year,OctCost,NovCost,DecCost,JanCost,FebCost,MarCost,AprCost,MayCost,JunCost,JulCost,AugCost,SepCost,CreatedBy,CreatedDate) values(@assignmentId,@year,@octCost,@novCost,@decCost,@janCost,@febCost,@marCost,@aprCost,@mayCost,@junCost,@julCost,@augCost,@sepCost,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@assignmentId", actualCost.AssignmentId);
                cmd.Parameters.AddWithValue("@year", actualCost.Year);
                cmd.Parameters.AddWithValue("@octCost", actualCost.OctCost);
                cmd.Parameters.AddWithValue("@novCost", actualCost.NovCost);
                cmd.Parameters.AddWithValue("@decCost", actualCost.DecCost);
                cmd.Parameters.AddWithValue("@janCost", actualCost.JanCost);
                cmd.Parameters.AddWithValue("@febCost", actualCost.FebCost);
                cmd.Parameters.AddWithValue("@marCost", actualCost.MarCost);
                cmd.Parameters.AddWithValue("@aprCost", actualCost.AprCost);
                cmd.Parameters.AddWithValue("@mayCost", actualCost.MayCost);
                cmd.Parameters.AddWithValue("@junCost", actualCost.JunCost);
                cmd.Parameters.AddWithValue("@julCost", actualCost.JulCost);
                cmd.Parameters.AddWithValue("@augCost", actualCost.AugCost);
                cmd.Parameters.AddWithValue("@sepCost", actualCost.SepCost);
                cmd.Parameters.AddWithValue("@createdBy", actualCost.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", actualCost.CreatedDate);
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

        public int UpdateActualCost(ActualCost actualCost)
        {
            int result = 0;
            string query = $@"update ActualCosts set OctCost=@octCost,NovCost=@novCost,DecCost=@decCost,JanCost=@janCost,FebCost=@febCost,MarCost=@marCost,AprCost=@aprCost,MayCost=@mayCost,JunCost=@junCost,JulCost=@julCost,AugCost=@augCost,SepCost=@sepCost,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where AssignmentId=@assignmentId and Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@octCost", actualCost.OctCost);
                cmd.Parameters.AddWithValue("@novCost", actualCost.NovCost);
                cmd.Parameters.AddWithValue("@decCost", actualCost.DecCost);
                cmd.Parameters.AddWithValue("@janCost", actualCost.JanCost);
                cmd.Parameters.AddWithValue("@febCost", actualCost.FebCost);
                cmd.Parameters.AddWithValue("@marCost", actualCost.MarCost);
                cmd.Parameters.AddWithValue("@aprCost", actualCost.AprCost);
                cmd.Parameters.AddWithValue("@mayCost", actualCost.MayCost);
                cmd.Parameters.AddWithValue("@junCost", actualCost.JunCost);
                cmd.Parameters.AddWithValue("@julCost", actualCost.JulCost);
                cmd.Parameters.AddWithValue("@augCost", actualCost.AugCost);
                cmd.Parameters.AddWithValue("@sepCost", actualCost.SepCost);
                cmd.Parameters.AddWithValue("@updatedBy", actualCost.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", actualCost.UpdatedDate);
                cmd.Parameters.AddWithValue("@assignmentId", actualCost.AssignmentId);
                cmd.Parameters.AddWithValue("@year", actualCost.Year);
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

        public bool CheckSukeyAssignmentId(int assignmentId, int year)
        {
            bool result = false;
            string query = "select * from Sukey where AssignmentId=" + assignmentId + " and year = " + year;
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

        public int CreateSukey(Sukey sukey)
        {
            int result = 0;
            string query = $@"insert into Sukey(AssignmentId,Year,OctCost,NovCost,DecCost,JanCost,FebCost,MarCost,AprCost,MayCost,JunCost,JulCost,AugCost,SepCost,CreatedBy,CreatedDate) values(@assignmentId,@year,@octCost,@novCost,@decCost,@janCost,@febCost,@marCost,@aprCost,@mayCost,@junCost,@julCost,@augCost,@sepCost,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@assignmentId", sukey.AssignmentId);
                cmd.Parameters.AddWithValue("@year", sukey.Year);
                cmd.Parameters.AddWithValue("@octCost", sukey.OctCost);
                cmd.Parameters.AddWithValue("@novCost", sukey.NovCost);
                cmd.Parameters.AddWithValue("@decCost", sukey.DecCost);
                cmd.Parameters.AddWithValue("@janCost", sukey.JanCost);
                cmd.Parameters.AddWithValue("@febCost", sukey.FebCost);
                cmd.Parameters.AddWithValue("@marCost", sukey.MarCost);
                cmd.Parameters.AddWithValue("@aprCost", sukey.AprCost);
                cmd.Parameters.AddWithValue("@mayCost", sukey.MayCost);
                cmd.Parameters.AddWithValue("@junCost", sukey.JunCost);
                cmd.Parameters.AddWithValue("@julCost", sukey.JulCost);
                cmd.Parameters.AddWithValue("@augCost", sukey.AugCost);
                cmd.Parameters.AddWithValue("@sepCost", sukey.SepCost);
                cmd.Parameters.AddWithValue("@createdBy", sukey.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", sukey.CreatedDate);
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

        public int UpdateSukey(Sukey sukey)
        {
            int result = 0;
            string query = $@"update Sukey set OctCost=@octCost,NovCost=@novCost,DecCost=@decCost,JanCost=@janCost,FebCost=@febCost,MarCost=@marCost,AprCost=@aprCost,MayCost=@mayCost,JunCost=@junCost,JulCost=@julCost,AugCost=@augCost,SepCost=@sepCost,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where AssignmentId=@assignmentId and Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@octCost", sukey.OctCost);
                cmd.Parameters.AddWithValue("@novCost", sukey.NovCost);
                cmd.Parameters.AddWithValue("@decCost", sukey.DecCost);
                cmd.Parameters.AddWithValue("@janCost", sukey.JanCost);
                cmd.Parameters.AddWithValue("@febCost", sukey.FebCost);
                cmd.Parameters.AddWithValue("@marCost", sukey.MarCost);
                cmd.Parameters.AddWithValue("@aprCost", sukey.AprCost);
                cmd.Parameters.AddWithValue("@mayCost", sukey.MayCost);
                cmd.Parameters.AddWithValue("@junCost", sukey.JunCost);
                cmd.Parameters.AddWithValue("@julCost", sukey.JulCost);
                cmd.Parameters.AddWithValue("@augCost", sukey.AugCost);
                cmd.Parameters.AddWithValue("@sepCost", sukey.SepCost);
                cmd.Parameters.AddWithValue("@updatedBy", sukey.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", sukey.UpdatedDate);
                cmd.Parameters.AddWithValue("@assignmentId", sukey.AssignmentId);
                cmd.Parameters.AddWithValue("@year", sukey.Year);
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
    }
}
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

            string query = $@"select * from ActualCosts where ea.Year={year}";


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
    }
}
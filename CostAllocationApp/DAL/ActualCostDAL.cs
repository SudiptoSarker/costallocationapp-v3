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

        public List<ActualCost> GetActualCostsByYear_AssignmentId(int year,int assignmentId)
        {
            List<ActualCost> actualCosts = new List<ActualCost>();

            string query = $@"select * from ActualCosts where Year={year} and AssignmentId={assignmentId}";

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

                            actualCost.OctPoint = Convert.ToDouble(rdr["OctPoint"]);
                            actualCost.NovPoint = Convert.ToDouble(rdr["NovPoint"]);
                            actualCost.DecPoint = Convert.ToDouble(rdr["DecPoint"]);
                            actualCost.JanPoint = Convert.ToDouble(rdr["JanPoint"]);
                            actualCost.FebPoint = Convert.ToDouble(rdr["FebPoint"]);
                            actualCost.MarPoint = Convert.ToDouble(rdr["MarPoint"]);
                            actualCost.AprPoint = Convert.ToDouble(rdr["AprPoint"]);
                            actualCost.MayPoint = Convert.ToDouble(rdr["MayPoint"]);
                            actualCost.JunPoint = Convert.ToDouble(rdr["JunPoint"]);
                            actualCost.JulPoint = Convert.ToDouble(rdr["JulPoint"]);
                            actualCost.AugPoint = Convert.ToDouble(rdr["AugPoint"]);
                            actualCost.SepPoint = Convert.ToDouble(rdr["SepPoint"]);



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
            string query = $@"insert into ActualCosts(AssignmentId,Year,OctCost,NovCost,DecCost,JanCost,FebCost,MarCost,AprCost,MayCost,JunCost,JulCost,AugCost,SepCost,CreatedBy,CreatedDate,OctPoint,NovPoint,DecPoint,JanPoint,FebPoint,MarPoint,AprPoint,MayPoint,JunPoint,JulPoint,AugPoint,SepPoint) values(@assignmentId,@year,@octCost,@novCost,@decCost,@janCost,@febCost,@marCost,@aprCost,@mayCost,@junCost,@julCost,@augCost,@sepCost,@createdBy,@createdDate,@octPoint,@novPoint,@decPoint,@janPoint,@febPoint,@marPoint,@aprPoint,@mayPoint,@junPoint,@julPoint,@augPoint,@sepPoint)";
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

                cmd.Parameters.AddWithValue("@octPoint", actualCost.OctPoint);
                cmd.Parameters.AddWithValue("@novPoint", actualCost.NovPoint);
                cmd.Parameters.AddWithValue("@decPoint", actualCost.DecPoint);
                cmd.Parameters.AddWithValue("@janPoint", actualCost.JanPoint);
                cmd.Parameters.AddWithValue("@febPoint", actualCost.FebPoint);
                cmd.Parameters.AddWithValue("@marPoint", actualCost.MarPoint);
                cmd.Parameters.AddWithValue("@aprPoint", actualCost.AprPoint);
                cmd.Parameters.AddWithValue("@mayPoint", actualCost.MayPoint);
                cmd.Parameters.AddWithValue("@junPoint", actualCost.JunPoint);
                cmd.Parameters.AddWithValue("@julPoint", actualCost.JulPoint);
                cmd.Parameters.AddWithValue("@augPoint", actualCost.AugPoint);
                cmd.Parameters.AddWithValue("@sepPoint", actualCost.SepPoint);
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

        public int UpdateActualCost(int year, int assignmentId, string costColumnName,string pointColumnName,double costAmount, double pointAmount, string updatedBy, DateTime updatedDate)
        {
            int result = 0;
            string query = $@"update ActualCosts set {costColumnName}={costAmount}, {pointColumnName}={pointAmount}, UpdatedBy='{updatedBy}',UpdatedDate='{updatedDate}' where AssignmentId={assignmentId} and Year={year}";
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

        public List<Sukey> GetAllSukeyData(int year)
        {
            List<Sukey> sukeys = new List<Sukey>();

            string query = $@"select * from Sukey where Year={year}";


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
                            Sukey sukey = new Sukey();
                            sukey.Id = Convert.ToInt32(rdr["Id"]);
                            sukey.AssignmentId = Convert.ToInt32(rdr["AssignmentId"]);
                            //actualCost.Year = Convert.ToInt32(rdr["Year"]);
                            sukey.OctCost = Convert.ToDouble(rdr["OctCost"]);
                            sukey.NovCost = Convert.ToDouble(rdr["NovCost"]);
                            sukey.DecCost = Convert.ToDouble(rdr["DecCost"]);
                            sukey.JanCost = Convert.ToDouble(rdr["JanCost"]);
                            sukey.FebCost = Convert.ToDouble(rdr["FebCost"]);
                            sukey.MarCost = Convert.ToDouble(rdr["MarCost"]);
                            sukey.AprCost = Convert.ToDouble(rdr["AprCost"]);
                            sukey.MayCost = Convert.ToDouble(rdr["MayCost"]);
                            sukey.JunCost = Convert.ToDouble(rdr["JunCost"]);
                            sukey.JulCost = Convert.ToDouble(rdr["JulCost"]);
                            sukey.AugCost = Convert.ToDouble(rdr["AugCost"]);
                            sukey.SepCost = Convert.ToDouble(rdr["SepCost"]);



                            sukeys.Add(sukey);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return sukeys;
        }

        public List<Apportionment> GetAllApportionmentData(int year)
        {
            List<Apportionment> apportionments = new List<Apportionment>();

            string query = $@"select * from Apportionments where Year={year}";


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
                            Apportionment apportionment = new Apportionment();
                            apportionment.Id = Convert.ToInt32(rdr["Id"]);
                            apportionment.Year = Convert.ToInt32(rdr["Year"]);
                            apportionment.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                            apportionment.OctPercentage = Convert.ToDouble(rdr["OctPercentage"]);
                            apportionment.NovPercentage = Convert.ToDouble(rdr["NovPercentage"]);
                            apportionment.DecPercentage = Convert.ToDouble(rdr["DecPercentage"]);
                            apportionment.JanPercentage = Convert.ToDouble(rdr["JanPercentage"]);
                            apportionment.FebPercentage = Convert.ToDouble(rdr["FebPercentage"]);
                            apportionment.MarPercentage = Convert.ToDouble(rdr["MarPercentage"]);
                            apportionment.AprPercentage = Convert.ToDouble(rdr["AprPercentage"]);
                            apportionment.MayPercentage = Convert.ToDouble(rdr["MayPercentage"]);
                            apportionment.JunPercentage = Convert.ToDouble(rdr["JunPercentage"]);
                            apportionment.JulPercentage = Convert.ToDouble(rdr["JulPercentage"]);
                            apportionment.AugPercentage = Convert.ToDouble(rdr["AugPercentage"]);
                            apportionment.SepPercentage = Convert.ToDouble(rdr["SepPercentage"]);



                            apportionments.Add(apportionment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return apportionments;
        }

        public int CreateApportionment(Apportionment apportionment)
        {
            int result = 0;
            string query = $@"insert into Apportionments(DepartmentId,Year,OctPercentage,NovPercentage,DecPercentage,JanPercentage,FebPercentage,MarPercentage,AprPercentage,MayPercentage,JunPercentage,JulPercentage,AugPercentage,SepPercentage,CreatedBy,CreatedDate) values(@departmentId,@year,@octCost,@novCost,@decCost,@janCost,@febCost,@marCost,@aprCost,@mayCost,@junCost,@julCost,@augCost,@sepCost,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@departmentId", apportionment.DepartmentId);
                cmd.Parameters.AddWithValue("@year", apportionment.Year);
                cmd.Parameters.AddWithValue("@octCost", apportionment.OctPercentage);
                cmd.Parameters.AddWithValue("@novCost", apportionment.NovPercentage);
                cmd.Parameters.AddWithValue("@decCost", apportionment.DecPercentage);
                cmd.Parameters.AddWithValue("@janCost", apportionment.JanPercentage);
                cmd.Parameters.AddWithValue("@febCost", apportionment.FebPercentage);
                cmd.Parameters.AddWithValue("@marCost", apportionment.MarPercentage);
                cmd.Parameters.AddWithValue("@aprCost", apportionment.AprPercentage);
                cmd.Parameters.AddWithValue("@mayCost", apportionment.MayPercentage);
                cmd.Parameters.AddWithValue("@junCost", apportionment.JunPercentage);
                cmd.Parameters.AddWithValue("@julCost", apportionment.JulPercentage);
                cmd.Parameters.AddWithValue("@augCost", apportionment.AugPercentage);
                cmd.Parameters.AddWithValue("@sepCost", apportionment.SepPercentage);
                cmd.Parameters.AddWithValue("@createdBy", apportionment.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", apportionment.CreatedDate);
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

        public int UpdateApportionment(Apportionment apportionment)
        {
            int result = 0;
            string query = $@"update Apportionments set OctPercentage=@octCost,NovPercentage=@novCost,DecPercentage=@decCost,JanPercentage=@janCost,FebPercentage=@febCost,MarPercentage=@marCost,AprPercentage=@aprCost,MayPercentage=@mayCost,JunPercentage=@junCost,JulPercentage=@julCost,AugPercentage=@augCost,SepPercentage=@sepCost,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where DepartmentId=@departmentId and Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@octCost", apportionment.OctPercentage);
                cmd.Parameters.AddWithValue("@novCost", apportionment.NovPercentage);
                cmd.Parameters.AddWithValue("@decCost", apportionment.DecPercentage);
                cmd.Parameters.AddWithValue("@janCost", apportionment.JanPercentage);
                cmd.Parameters.AddWithValue("@febCost", apportionment.FebPercentage);
                cmd.Parameters.AddWithValue("@marCost", apportionment.MarPercentage);
                cmd.Parameters.AddWithValue("@aprCost", apportionment.AprPercentage);
                cmd.Parameters.AddWithValue("@mayCost", apportionment.MayPercentage);
                cmd.Parameters.AddWithValue("@junCost", apportionment.JunPercentage);
                cmd.Parameters.AddWithValue("@julCost", apportionment.JulPercentage);
                cmd.Parameters.AddWithValue("@augCost", apportionment.AugPercentage);
                cmd.Parameters.AddWithValue("@sepCost", apportionment.SepPercentage);
                cmd.Parameters.AddWithValue("@updatedBy", apportionment.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", apportionment.UpdatedDate);
                cmd.Parameters.AddWithValue("@departmentId", apportionment.DepartmentId);
                cmd.Parameters.AddWithValue("@year", apportionment.Year);
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

        public bool CheckApportionment(int departmentId, int year)
        {
            bool result = false;
            string query = "select * from Apportionments where DepartmentId=" + departmentId + " and year = " + year;
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

        public int GetLeatestForcastYear()
        {
            int result = 0;

            string query = $@"select distinct max(year) year from Costs";


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
                            result = Convert.ToInt32(rdr["year"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public int GetLeatestActualCostYear()
        {
            int result = 0;

            string query = $@"select distinct max(year) year from ActualCosts";


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
                            result = Convert.ToInt32(rdr["year"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
        public int UpdateAssignmentIds(int previousId,int updateId)
        {
            int result = 0;
            string query = $@"update ActualCosts set AssignmentId={updateId} where AssignmentId={previousId}";
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

                }

                return result;
            }
        }
    }
}
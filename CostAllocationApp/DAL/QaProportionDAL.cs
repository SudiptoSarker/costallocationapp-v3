using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class QaProportionDAL : DbContext
    {
        public List<QaProportionViewModel> SearchAssignmentByYear_Department(int year, int departmentId)
        {
            string where = "";
            if (departmentId > 0)
            {
                where += $" ea.DepartmentId={departmentId} and ";
            }


            where += $" 1=1 ";
            string query = $@"select ea.id as AssignmentId, ea.DepartmentId, e.FullName, ea.EmployeeId 
                            from EmployeesAssignments ea join Employees e on e.Id = ea.EmployeeId  where ea.year={year} and {where}";

            List<QaProportionViewModel> qaProportionEmployeeNameList = new List<QaProportionViewModel>();

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
                            QaProportionViewModel qaProportionViewModel = new QaProportionViewModel();
                            qaProportionViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            qaProportionViewModel.EmployeeName = rdr["FullName"].ToString();

                            qaProportionEmployeeNameList.Add(qaProportionViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return qaProportionEmployeeNameList;
        }

        public List<QaProportionViewModel> SearchAssignmentByYear_Department_Data(int year, int departmentId)
        {
            string where = "";
            if (departmentId > 0)
            {
                where += $" ea.DepartmentId={departmentId} and ";
            }


            where += $" 1=1 ";
            string query = $@"select ea.id as AssignmentId, ea.DepartmentId, e.FullName, ea.EmployeeId 
                            from EmployeesAssignments ea join Employees e on e.Id = ea.EmployeeId  where ea.year={year} and {where}";

            List<QaProportionViewModel> qaProportionEmployeeList = new List<QaProportionViewModel>();

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
                            QaProportionViewModel qaProportionViewModel = new QaProportionViewModel();
                            qaProportionViewModel.AssignmentId = Convert.ToInt32(rdr["AssignmentId"]);
                            qaProportionViewModel.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            qaProportionViewModel.EmployeeName = rdr["FullName"].ToString();
                            qaProportionViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            qaProportionEmployeeList.Add(qaProportionViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return qaProportionEmployeeList;
        }

        public double GetUnitPriceByAssignmentId(int assignmentId)
        {
            double unitPrice = 0;
            string query = $@"select UnitPrice from EmployeesAssignments where Id={assignmentId}";

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
                            unitPrice = Convert.ToDouble(rdr["UnitPrice"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return unitPrice;
        }

        public int CreateQaProportion(QaProportion qaProportion)
        {
            int result = 0;
            string query = $@"insert into QaProportions(EmployeeId,DepartmentId,OctPercentage,NovPercentage,DecPercentage,JanPercentage,FebPercentage,MarPercentage,AprPercentage,MayPercentage,JunPercentage,JulPercentage,AugPercentage,SepPercentage,Year,CreatedBy,CreatedDate) values (@employeeId,@departmentId,@octPercentage,@novPercentage,@decPercentage,@janPercentage,@febPercentage,@marPercentage,@aprPercentage,@mayPercentage,@junPercentage,@julPercentage,@augPercentage,@sepPercentage,@year,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);


                cmd.Parameters.AddWithValue("@employeeId", qaProportion.EmployeeId);
                if (String.IsNullOrEmpty(qaProportion.DepartmentId))
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", Convert.ToInt32(qaProportion.DepartmentId));
                }
                
                cmd.Parameters.AddWithValue("@octPercentage", qaProportion.OctPercentage);
                cmd.Parameters.AddWithValue("@novPercentage", qaProportion.NovPercentage);
                cmd.Parameters.AddWithValue("@decPercentage", qaProportion.DecPercentage);
                cmd.Parameters.AddWithValue("@janPercentage", qaProportion.JanPercentage);
                cmd.Parameters.AddWithValue("@febPercentage", qaProportion.FebPercentage);
                cmd.Parameters.AddWithValue("@marPercentage", qaProportion.MarPercentage);
                cmd.Parameters.AddWithValue("@aprPercentage", qaProportion.AprPercentage);
                cmd.Parameters.AddWithValue("@mayPercentage", qaProportion.MayPercentage);
                cmd.Parameters.AddWithValue("@junPercentage", qaProportion.JunPercentage);
                cmd.Parameters.AddWithValue("@julPercentage", qaProportion.JulPercentage);
                cmd.Parameters.AddWithValue("@augPercentage", qaProportion.AugPercentage);
                cmd.Parameters.AddWithValue("@sepPercentage", qaProportion.SepPercentage);
                cmd.Parameters.AddWithValue("@year", qaProportion.Year);
                cmd.Parameters.AddWithValue("@createdBy", qaProportion.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", qaProportion.CreatedDate);

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

        public List<QaProportionViewModel> GetQaProportionDataByYear(int year)
        {

            string query = "select * from QaProportions join Employees on QaProportions.EmployeeId = Employees.Id  where QaProportions.year = " + year+ " Order By FullName ASC";

            List<QaProportionViewModel> qaProportionEmployeeList = new List<QaProportionViewModel>();

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
                            QaProportionViewModel qaProportionViewModel = new QaProportionViewModel();
                            qaProportionViewModel.Id = Convert.ToInt32(rdr["Id"]);
                            qaProportionViewModel.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            qaProportionViewModel.EmployeeName = rdr["FullName"].ToString();
                            qaProportionViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            qaProportionViewModel.OctPercentage = Convert.ToDouble(rdr["OctPercentage"]);
                            qaProportionViewModel.NovPercentage = Convert.ToDouble(rdr["NovPercentage"]);
                            qaProportionViewModel.DecPercentage = Convert.ToDouble(rdr["DecPercentage"]);
                            qaProportionViewModel.JanPercentage = Convert.ToDouble(rdr["JanPercentage"]);
                            qaProportionViewModel.FebPercentage = Convert.ToDouble(rdr["FebPercentage"]);
                            qaProportionViewModel.MarPercentage = Convert.ToDouble(rdr["MarPercentage"]);
                            qaProportionViewModel.AprPercentage = Convert.ToDouble(rdr["AprPercentage"]);
                            qaProportionViewModel.MayPercentage = Convert.ToDouble(rdr["MayPercentage"]);
                            qaProportionViewModel.JunPercentage = Convert.ToDouble(rdr["JunPercentage"]);
                            qaProportionViewModel.JulPercentage = Convert.ToDouble(rdr["JulPercentage"]);
                            qaProportionViewModel.AugPercentage = Convert.ToDouble(rdr["AugPercentage"]);
                            qaProportionViewModel.SepPercentage = Convert.ToDouble(rdr["SepPercentage"]);
                            qaProportionEmployeeList.Add(qaProportionViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return qaProportionEmployeeList;
        }

        public int UpdateQaProportion(QaProportion qaProportion)
        {
            int result = 0;
            string query = $@"update QaProportions set DepartmentId = @departmentId, OctPercentage= @octPercentage, NovPercentage=@novPercentage, DecPercentage=@decPercentage, JanPercentage=@janPercentage, FebPercentage=@febPercentage,MarPercentage = @marPercentage, AprPercentage=@aprPercentage, MayPercentage=@mayPercentage, JunPercentage=@junPercentage, JulPercentage=@julPercentage, AugPercentage=@augPercentage, SepPercentage=@sepPercentage, UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                if (String.IsNullOrEmpty(qaProportion.DepartmentId))
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", Convert.ToInt32(qaProportion.DepartmentId));
                }
                cmd.Parameters.AddWithValue("@octPercentage", qaProportion.OctPercentage);
                cmd.Parameters.AddWithValue("@novPercentage", qaProportion.NovPercentage);
                cmd.Parameters.AddWithValue("@decPercentage", qaProportion.DecPercentage);
                cmd.Parameters.AddWithValue("@janPercentage", qaProportion.JanPercentage);
                cmd.Parameters.AddWithValue("@febPercentage", qaProportion.FebPercentage);
                cmd.Parameters.AddWithValue("@marPercentage", qaProportion.MarPercentage);
                cmd.Parameters.AddWithValue("@aprPercentage", qaProportion.AprPercentage);
                cmd.Parameters.AddWithValue("@mayPercentage", qaProportion.MayPercentage);
                cmd.Parameters.AddWithValue("@junPercentage", qaProportion.JunPercentage);
                cmd.Parameters.AddWithValue("@julPercentage", qaProportion.JulPercentage);
                cmd.Parameters.AddWithValue("@augPercentage", qaProportion.AugPercentage);
                cmd.Parameters.AddWithValue("@sepPercentage", qaProportion.SepPercentage);
                cmd.Parameters.AddWithValue("@updatedBy", qaProportion.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", qaProportion.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", qaProportion.Id);
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
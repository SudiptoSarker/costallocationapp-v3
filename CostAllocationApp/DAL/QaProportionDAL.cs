using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.ViewModels;

namespace CostAllocationApp.DAL
{
    public class QaProportionDAL : DbContext
    {
        public List<string> SearchAssignmentByYear_Department(int year, int departmentId)
        {
            string where = "";
            if (departmentId > 0)
            {
                where += $" ea.DepartmentId={departmentId} and ";
            }


            where += $" 1=1 ";
            string query = $@"select ea.id as AssignmentId, ea.DepartmentId, e.FullName, ea.EmployeeId 
                            from EmployeesAssignments ea join Employees e on e.Id = ea.EmployeeId  where ea.year={year} and {where}";

            List<string> qaProportionEmployeeNameList = new List<string>();

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
                            //QaProportionViewModel qaProportionViewModel = new QaProportionViewModel();
                            //qaProportionViewModel.AssignmentId = Convert.ToInt32(rdr["AssignmentId"]);
                            //qaProportionViewModel.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            //qaProportionViewModel.EmployeeName = rdr["FullName"].ToString();

                            qaProportionEmployeeNameList.Add(rdr["FullName"].ToString());
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

            //EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();

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


    }
}
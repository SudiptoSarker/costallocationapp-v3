using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class DepartmentDAL : DbContext
    {
        public int CreateDepartment(Department department)
        {
            int result = 0;
            string query = $@"insert into departments(Name,SectionId,CreatedBy,CreatedDate,IsActive) values(@departmentName,@sectionId,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@departmentName", department.DepartmentName);
                cmd.Parameters.AddWithValue("@sectionId", department.SectionId);
                cmd.Parameters.AddWithValue("@createdBy", department.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", department.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", department.IsActive);
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
        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            string query = "";
            query = "SELECT * FROM Departments WHERE isactive=1";

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
                            Department department = new Department();
                            department.Id = Convert.ToInt32(rdr["Id"]);
                            department.DepartmentName = rdr["Name"].ToString();
                            department.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            department.CreatedBy = rdr["CreatedBy"].ToString();
                            department.SubCategoryId = rdr["SubCategoryId"].ToString();

                            departments.Add(department);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return departments;
            }
        }

        public bool CheckForBudgetInitialDataExists(int budgetYear)
        {            
            bool results = false;

            string query = "";
            query = "SELECT TOP 2 * FROM EmployeeeBudgets WHERE Year="+ budgetYear + " AND FirstHalfBudget=1";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        results = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return results;
            }
        }
        public bool CheckForBudgetSecondHalfDataExists(int budgetYear)
        {
            bool results = false;

            string query = "";
            query = "SELECT TOP 2 * FROM EmployeeeBudgets WHERE Year=" + budgetYear + " AND SecondHalfBudget=1";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        results = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return results;
            }
        }
        public bool CheckForBudgetInitialDataFinalizeExists(int budgetYear)
        {
            bool results = false;

            string query = "";
            query = "SELECT TOP 2 * FROM EmployeeeBudgets WHERE Year="+ budgetYear + " AND FirstHalfBudget=1 AND FinalizedBudget=1";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        results = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return results;
            }
        }
        public bool CheckForBudgetSecondHalfDataFinalizeExists(int budgetYear)
        {
            bool results = false;

            string query = "";
            query = "SELECT TOP 2 * FROM EmployeeeBudgets WHERE Year="+ budgetYear + " AND SecondHalfBudget=1 AND FinalizedBudget=1";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        results = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return results;
            }
        }

        public List<Department> GetAllDepartmentsBySectionId(int sectionId)
        {
            List<Department> departments = new List<Department>();
            string query = "";
            query = "SELECT dpt.*,sc.Name as SectionName ";
            query = query + "FROM Departments dpt ";
            query = query + "    INNER JOIN Sections sc ON dpt.SectionId = sc.Id ";
            query = query + "WHERE dpt.isactive=1 and SectionId="+sectionId;
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
                            Department department = new Department();
                            department.Id = Convert.ToInt32(rdr["Id"]);
                            department.DepartmentName = rdr["Name"].ToString();
                            department.SectionName = rdr["SectionName"].ToString();
                            department.SectionId = Convert.ToInt32(rdr["SectionId"]);
                            department.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            department.CreatedBy = rdr["CreatedBy"].ToString();

                            departments.Add(department);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return departments;
            }
        }

        public int RemoveDepartment(int departmentId)
        {
            int result = 0;
            string query = $@"update Departments set isactive=0 where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", departmentId);
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

        public bool CheckDepartment(Department department)
        {
            string query = "select * from Departments  where Name=N'" + department.DepartmentName + "' and SectionId="+department.SectionId;
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

        public int GetDepartmentCountWithEmployeeAsignment(int departmentId)
        {
            string query = "select * from EmployeesAssignments where DepartmentId=" + departmentId;
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

        public Department GetDepartmentByDepartemntId(int departmentId)
        {
            Department department = null;
            string query = "select * from Departments where Id = " + departmentId;
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
                            department = new Department();
                            department.DepartmentName = rdr["Name"].ToString();
                            department.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    department = null;
                }

                return department;
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "";
            query = "SELECT * FROM Categories";

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
                            Category category = new Category();
                            category.Id = Convert.ToInt32(rdr["Id"]);
                            category.CategoryName = rdr["Name"].ToString();
                            categories.Add(category);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return categories;
            }
        }

        public List<SubCategory> GetAllSubCategories()
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            string query = "";
            query = "SELECT * FROM SubCategories join Categories on SubCategories.CategoryId = Categories.Id";

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
                            SubCategory subCategory = new SubCategory();
                            subCategory.Id = Convert.ToInt32(rdr["Id"]);
                            subCategory.SubCategoryName = rdr["SubCategoryName"].ToString();
                            subCategory.CategoryId = rdr["CategoryId"].ToString();
                            subCategory.CategoryName = rdr["CategoryName"].ToString();
                            subCategories.Add(subCategory);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return subCategories;
            }
        }
    }
}
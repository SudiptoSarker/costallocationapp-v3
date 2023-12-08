using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;


namespace CostAllocationApp.DAL
{
    public class EmployeeDAL : DbContext
    {
        public int GetLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from Employees;";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }
        public int CreateEmployee(Employee employee)
        {
            int result = 0;
            string query = $@"insert into Employees(FullName,CreatedBy,CreatedDate,IsActive) values(@fullName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@fullName", employee.FullName);
                cmd.Parameters.AddWithValue("@createdBy", employee.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", employee.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", employee.IsActive);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    if (result>0)
                    {
                        result = GetLastId();
                    }
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }
        public int CheckForEmployeeName(string employeeName)
        {
            string query = "select Id,FullName from Employees where FullName=N'" + employeeName + "' AND Isactive=1";
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
                            result = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public List<Employee> GetAllEmployees(string orderby = "")
        {
            orderby = string.IsNullOrEmpty(orderby) ? "FullName" : orderby;
            List<Employee> employees = new List<Employee>();
            string query = "select * from employees where IsActive = 1 order by "+orderby+" asc";

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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["Id"]);
                            employee.FullName = rdr["FullName"].ToString();
                            employee.CreatedBy = rdr["CreatedBy"].ToString();
                            employee.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }

        public List<Employee> GetEmployeeListForBudgetEdit(int budgetYear,int budgetType)
        {
            List<Employee> employees = new List<Employee>();
            string query = "";
            if (budgetType == 1)
            {
                query = "EXEC SP_GetEmployeeListForFirstHalfBudget @year = "+ budgetYear + " ,@budgetType = "+ budgetType+" ";
            }
            else
            {
                query = "EXEC SP_GetEmployeeListForSecondHalfBudget @year = " + budgetYear + " ,@budgetType = " + budgetType + " ";
            }                        

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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["EmployeeId"]);
                            employee.FullName = rdr["EmployeeName"].ToString();

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }
        public List<Employee> GetEmployeeListEmployeeAssignments(int assignmentYear)
        {
            List<Employee> employees = new List<Employee>();
            string query = "";
            query = "EXEC SP_GetEmployeeListForYearlyEdit @year = "+ assignmentYear + " ";

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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["EmployeeId"]);
                            employee.FullName = rdr["EmployeeName"].ToString();

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }
        public List<Employee> EmployeeListByNameFilter(string employeeName)
        {
            List<Employee> employees = new List<Employee>();
            string whereSql = "";
            if (employeeName.ToLower() == "all")
            {
                whereSql = "IsActive = 1";
            }
            else
            {
                whereSql = "FullName like N'%" + employeeName + "%' and IsActive = 1";
            }
            string query = "select * from employees where "+ whereSql + "  order by FullName asc";
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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["Id"]);
                            employee.FullName = rdr["FullName"].ToString();
                            employee.CreatedBy = rdr["CreatedBy"].ToString();
                            employee.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            int result = 0;
            string query = $@"update Employees set FullName=@fullName,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@fullName", employee.FullName);
                cmd.Parameters.AddWithValue("@updatedBy", employee.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", employee.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", employee.Id);
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

        public int InactiveEmployee(Employee employee)
        {
            int result = 0;
            string query = $@"update Employees set IsActive=@isActive,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isActive", employee.IsActive);
                cmd.Parameters.AddWithValue("@updatedBy", employee.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", employee.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", employee.Id);
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
        public bool CheckEmployeeDuplication(string employeeName)
        {
            string query = "select * from Employees where FullName=N'" + employeeName + "' And IsActive=1";
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

        public bool CheckUserNameDuplication(string userName)
        {
            string query = "select * from Users where UserName=N'" + userName + "'";
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
        public bool CheckUserEmailDuplication(string userEmail)
        {
            string query = "select * from Users where Email=N'" + userEmail + "'";
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

        public string GetEmployeeNameByAssignmentId(int assignmentId)
        {
            string fullName = "";
            string query = " select Employees.FullName from EmployeesAssignments join Employees on Employees.Id = EmployeesAssignments.EmployeeId where EmployeesAssignments.Id="+ assignmentId;

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
                            fullName = rdr["FullName"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return fullName;
            }
        }
        public int RemoveEmployees(int employeeId)
        {
            int result = 0;
            //string query = $@"DELETE FROM Employees WHERE id = @id ";
            string query = $@"UPDATE Employees SET IsActive = 0 WHERE Id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", employeeId);
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
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            string query = "select * from Employees where Id = " + employeeId;            
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
                            employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["Id"]);
                            employee.FullName = rdr["FullName"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    employee = null;
                }

                return employee;
            }
        }
        public DeleteEmployees GetMaxYears()
        {
            DeleteEmployees deleteEmployees = new DeleteEmployees();

            string query = "";
            query = query +"select max(b.year) 'budget_year',max(fb.year) 'final_budget_year',max(ea.year) 'assignment_year',max(ac.year) 'actualcost_year',max(qap.year) 'proration_year' ";
            query = query + "from EmployeeeBudgets b ";
            query = query +"    left join EmployeeeFinalBudgets fb on b.EmployeeId=fb.EmployeeId ";
	        query = query +"    left join EmployeesAssignments ea on b.EmployeeId=ea.EmployeeId ";
	        query = query +"    left join ActualCosts ac on ea.id=ac.AssignmentId ";
	        query = query +"    left join QaProportions qap on b.EmployeeId=qap.EmployeeId ";

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
                            deleteEmployees.BudgetYear = rdr["budget_year"] is DBNull ? "" : rdr["budget_year"].ToString();
                            deleteEmployees.FinalBudgetYear = rdr["final_budget_year"] is DBNull ? "" : rdr["final_budget_year"].ToString();
                            deleteEmployees.AssignmentYear = rdr["assignment_year"] is DBNull ? "" : rdr["assignment_year"].ToString();
                            deleteEmployees.ActualCostYear = rdr["actualcost_year"] is DBNull ? "" : rdr["actualcost_year"].ToString();
                            deleteEmployees.QAProportionYear = rdr["proration_year"] is DBNull ? "" : rdr["proration_year"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    deleteEmployees = null;
                }

                return deleteEmployees;
            }                        
        }

        public List<EmployeeBudget> GetBudgetIdsByYearAndEmployeeId(string year,int employeeId)
        {
            List<EmployeeBudget> employeeBudgets = new List<EmployeeBudget>();
            string query = "SELECT Id FROM EmployeeeBudgets WHERE Year="+ year + " AND EmployeeId="+ employeeId;
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
                            EmployeeBudget employeeBudget = new EmployeeBudget();                            
                            employeeBudget.Id = Convert.ToInt32(rdr["Id"]);
                            employeeBudgets.Add(employeeBudget);
                        }
                    }
                }
                catch (Exception ex)
                {
                    employeeBudgets = null;
                }

                return employeeBudgets;
            }
        }
        public int RemoveBudgetByYearAndEmployeeId(string year, int employeeId)
        {
            int result = 0;            
            string query = "DELETE FROM EmployeeeBudgets WHERE Year="+ year + " AND EmployeeId="+ employeeId;
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
        public int RemoveBudgetCostsByBudgetId(int budgetId)
        {
            int result = 0;
            string query = "DELETE FROM BudgetCosts WHERE EmployeeBudgetId ="+budgetId;
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
        public List<EmployeeBudget> GetFinalBudgetIdsByYearAndEmployeeId(string year, int employeeId)
        {
            List<EmployeeBudget> employeeBudgets = new List<EmployeeBudget>();
            string query = "SELECT Id FROM EmployeeeFinalBudgets WHERE Year="+ year + " AND EmployeeId="+ employeeId;
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
                            EmployeeBudget employeeBudget = new EmployeeBudget();
                            employeeBudget.Id = Convert.ToInt32(rdr["Id"]);
                            employeeBudgets.Add(employeeBudget);
                        }
                    }
                }
                catch (Exception ex)
                {
                    employeeBudgets = null;
                }

                return employeeBudgets;
            }
        }
        public int RemoveFinalBudgetByYearAndEmployeeId(string year, int employeeId)
        {
            int result = 0;
            string query = "DELETE FROM EmployeeeFinalBudgets WHERE Year="+ year+" AND EmployeeId=" + employeeId;
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
        public int RemoveFinalBudgetCostsByBudgetId(int budgetId)
        {
            int result = 0;
            string query = "DELETE FROM FinalBudgetCosts WHERE EmployeeBudgetId =" + budgetId;
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
        public List<EmployeeAssignment> GetEmployeeAssignmentIdsByYearAndEmployeeId(string year, int employeeId)
        {
            List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();
            string query = "SELECT Id FROM EmployeesAssignments WHERE Year="+ year + " AND EmployeeId="+ employeeId;
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
                            EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                            employeeAssignment.Id = Convert.ToInt32(rdr["Id"]);
                            employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    employeeAssignments = null;
                }

                return employeeAssignments;
            }
        }
        public int RemoveEmployeeAssignmentsByYearAndEmployeeId(string year, int employeeId)
        {
            int result = 0;
            string query = "DELETE FROM EmployeesAssignments WHERE Year="+ year + " AND EmployeeId=" + employeeId;
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
        public int RemoveEmployeeAssignmentsCostsByAssignmentId(int employeeId)
        {
            int result = 0;
            string query = "DELETE FROM Costs WHERE EmployeeAssignmentsId=" + employeeId;
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

        public int RemoveActualCostByYearAndAssingmentId(string year, int assignmentId)
        {
            int result = 0;
            string query = "DELETE FROM ActualCosts WHERE Year="+ year + " AND AssignmentId="+ assignmentId;
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
        public int RemoveQAProrationByYearAndEmployeeId(string year, int employeeId)
        {
            int result = 0;
            string query = "DELETE FROM QaProportions WHERE Year="+ year+" AND EmployeeId=" + employeeId;
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
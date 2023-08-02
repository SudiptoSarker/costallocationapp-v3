using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Dtos;
using System.Globalization;

namespace CostAllocationApp.DAL
{
    public class EmployeeAssignmentDAL : DbContext
    {
        public int CreateAssignment(EmployeeAssignment employeeAssignment)
        {
            int result = 0;
            string query = $@"insert into EmployeesAssignments(EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,SubCode,Year,BCYR,BCYRCell,EmployeeName) values(@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@subCode,@year,@bCYR,@bCYRCell,@employeeName);";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@employeeId", employeeAssignment.EmployeeId);
                if (employeeAssignment.SectionId == null)
                {
                    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sectionId", employeeAssignment.SectionId);
                }
                if (employeeAssignment.DepartmentId == null)
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", employeeAssignment.DepartmentId);
                }
                if (employeeAssignment.InchargeId == null)
                {
                    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@inChargeId", employeeAssignment.InchargeId);
                }

                if (employeeAssignment.RoleId==null)
                {
                    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleId", employeeAssignment.RoleId);
                }

                if (String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@explanationId", employeeAssignment.ExplanationId);
                }

                if (employeeAssignment.CompanyId==null)
                {
                    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@companyId", employeeAssignment.CompanyId);
                }

                if (employeeAssignment.GradeId == null)
                {
                    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@gradeId", employeeAssignment.GradeId);
                }
                cmd.Parameters.AddWithValue("@unitPrice", employeeAssignment.UnitPrice);
                //cmd.Parameters.AddWithValue("@createdBy", employeeAssignment.CreatedBy);
                cmd.Parameters.AddWithValue("@createdBy", "");
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@isActive", 1);
                cmd.Parameters.AddWithValue("@remarks", employeeAssignment.Remarks);
                cmd.Parameters.AddWithValue("@subCode", employeeAssignment.SubCode);
                cmd.Parameters.AddWithValue("@year", employeeAssignment.Year);
                cmd.Parameters.AddWithValue("@bCYR", employeeAssignment.BCYR);
                cmd.Parameters.AddWithValue("@bCYRCell", employeeAssignment.BCYRCell);
                //cmd.Parameters.AddWithValue("@employeeName", "");
                cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeName);

                try
                {
                    result = cmd.ExecuteNonQuery();
                   // result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }
        public int CreateBudgets(EmployeeBudget employeeAssignment)
        {
            int result = 0;
            string query = $@"insert into EmployeeeBudgets(EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,Year,EmployeeName,FirstHalfBudget,SecondHalfBudget,FinalizedBudget) values(@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@year,@employeeName,@firstHalfBudget,@secondHalfBudget,@finalizedBudget);";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@employeeId", employeeAssignment.EmployeeId);
                if (employeeAssignment.SectionId == null)
                {
                    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sectionId", employeeAssignment.SectionId);
                }
                if (employeeAssignment.DepartmentId == null)
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", employeeAssignment.DepartmentId);
                }
                if (employeeAssignment.InchargeId == null)
                {
                    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@inChargeId", employeeAssignment.InchargeId);
                }

                if (employeeAssignment.RoleId == null)
                {
                    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleId", employeeAssignment.RoleId);
                }

                if (String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@explanationId", employeeAssignment.ExplanationId);
                }

                if (employeeAssignment.CompanyId == null)
                {
                    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@companyId", employeeAssignment.CompanyId);
                }

                if (employeeAssignment.GradeId == null)
                {
                    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@gradeId", employeeAssignment.GradeId);
                }
                cmd.Parameters.AddWithValue("@unitPrice", employeeAssignment.UnitPrice);                
                cmd.Parameters.AddWithValue("@createdBy", "");
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@isActive", 1);
                cmd.Parameters.AddWithValue("@remarks", employeeAssignment.Remarks);                
                cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeName);
                cmd.Parameters.AddWithValue("@year", employeeAssignment.Year);

                cmd.Parameters.AddWithValue("@firstHalfBudget", employeeAssignment.FirstHalfBudget);
                cmd.Parameters.AddWithValue("@secondHalfBudget", employeeAssignment.SecondHalfBudget);
                cmd.Parameters.AddWithValue("@finalizedBudget", employeeAssignment.FinalizedBudget);

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

        public int GetLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from EmployeesAssignments;";
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

        public int GetBudgetLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from EmployeeeBudgets;";
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

        //update forecast assignment data
        public int UpdateAssignment(EmployeeAssignment employeeAssignment)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set EmployeeId=@employeeId,  SectionId=@sectionId,DepartmentId=@departmentId,InChargeId=@inChargeId,RoleId=@roleId,ExplanationId=@explanationId,CompanyId=@companyId,UnitPrice=@unitPrice,GradeId=@gradeId,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate, Remarks=@remarks,IsDeletePending=@isDeletePending,IsActive=@isActiveAssignment  where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@employeeId", employeeAssignment.EmployeeId);
                if (employeeAssignment.SectionId == null)
                {
                    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sectionId", employeeAssignment.SectionId);
                }
                if (employeeAssignment.DepartmentId == null)
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", employeeAssignment.DepartmentId);
                }
                if (employeeAssignment.InchargeId == null)
                {
                    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@inChargeId", employeeAssignment.InchargeId);
                }

                if (employeeAssignment.RoleId == null)
                {
                    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleId", employeeAssignment.RoleId);
                }

                if (String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@explanationId", employeeAssignment.ExplanationId);
                }

                if (employeeAssignment.CompanyId == null)
                {
                    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@companyId", employeeAssignment.CompanyId);
                }

                if (employeeAssignment.GradeId == null)
                {
                    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@gradeId", employeeAssignment.GradeId);
                }
                cmd.Parameters.AddWithValue("@unitPrice", employeeAssignment.UnitPrice);
                cmd.Parameters.AddWithValue("@updatedBy", employeeAssignment.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", employeeAssignment.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", employeeAssignment.Id);
                cmd.Parameters.AddWithValue("@remarks", employeeAssignment.Remarks);
                cmd.Parameters.AddWithValue("@isDeletePending", employeeAssignment.IsDeletePending);
                 cmd.Parameters.AddWithValue("@isActiveAssignment", employeeAssignment.IsActiveAssignment);
                
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

        //update budget assingment data
        public int UpdateBudgetAssignment(EmployeeAssignment employeeAssignment)
        {
            int result = 0;
            string query = $@"update EmployeeeBudgets set EmployeeId=@employeeId,  SectionId=@sectionId,DepartmentId=@departmentId,InChargeId=@inChargeId,RoleId=@roleId,ExplanationId=@explanationId,CompanyId=@companyId,UnitPrice=@unitPrice,GradeId=@gradeId,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate, Remarks=@remarks,IsActive=@isActiveAssignment  where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@employeeId", employeeAssignment.EmployeeId);
                if (employeeAssignment.SectionId == null)
                {
                    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sectionId", employeeAssignment.SectionId);
                }
                if (employeeAssignment.DepartmentId == null)
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", employeeAssignment.DepartmentId);
                }
                if (employeeAssignment.InchargeId == null)
                {
                    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@inChargeId", employeeAssignment.InchargeId);
                }

                if (employeeAssignment.RoleId == null)
                {
                    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleId", employeeAssignment.RoleId);
                }

                if (String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@explanationId", employeeAssignment.ExplanationId);
                }

                if (employeeAssignment.CompanyId == null)
                {
                    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@companyId", employeeAssignment.CompanyId);
                }

                if (employeeAssignment.GradeId == null)
                {
                    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@gradeId", employeeAssignment.GradeId);
                }
                cmd.Parameters.AddWithValue("@unitPrice", employeeAssignment.UnitPrice);
                cmd.Parameters.AddWithValue("@updatedBy", employeeAssignment.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", employeeAssignment.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", employeeAssignment.Id);
                cmd.Parameters.AddWithValue("@remarks", employeeAssignment.Remarks);
                cmd.Parameters.AddWithValue("@isActiveAssignment", employeeAssignment.IsActiveAssignment);

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


        public List<EmployeeAssignmentViewModel> SearchAssignment(EmployeeAssignment employeeAssignment)
        {
            string where = "";
            if (employeeAssignment.SectionId >0)
            {
                where += $" ea.SectionId={employeeAssignment.SectionId} and ";
            }
            if (employeeAssignment.DepartmentId >0)
            {
                where += $" ea.DepartmentId={employeeAssignment.DepartmentId} and ";
            }
            if (employeeAssignment.InchargeId >0)
            {
                where += $" ea.InChargeId={employeeAssignment.InchargeId} and ";
            }
            if (employeeAssignment.RoleId > 0)
            {
                where += $" ea.RoleId={employeeAssignment.RoleId} and ";
            }
            //if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
            //{
            //    where += $" ea.ExplanationId={employeeAssignment.ExplanationId} and ";
            //}
            if (employeeAssignment.CompanyId >0)
            {
                where += $" ea.CompanyId={employeeAssignment.CompanyId} and ";
            }

            where += " 1=1 ";
            string query = $@"select ea.id as AssignmentId,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            from EmployeesAssignments ea join Sections sec on ea.SectionId = sec.Id
                            join Departments dep on ea.DepartmentId = dep.Id
                            join Companies com on ea.CompanyId = com.Id
                            join Roles rl on ea.RoleId = rl.Id
                            join InCharges inc on ea.InChargeId = inc.Id where {where}";

            List<EmployeeAssignmentViewModel> employeeAssignments = new List<EmployeeAssignmentViewModel>();

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
                            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            employeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                            employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);



                            employeeAssignments.Add(employeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;
        }

        public EmployeeAssignmentViewModel GetAssignmentById(int assignmentId)
        {

            string query = $@"select ea.id as AssignmentId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks,gd.GradePoints,ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice, ea.GradeId 
                            from EmployeesAssignments ea join Sections sec on ea.SectionId = sec.Id
                            join Departments dep on ea.DepartmentId = dep.Id
                            join Companies com on ea.CompanyId = com.Id
                            join Roles rl on ea.RoleId = rl.Id
                            join Grades gd on ea.GradeId = gd.Id 
                            join InCharges inc on ea.InChargeId = inc.Id where ea.Id={assignmentId}";

            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();

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
                            employeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            employeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            employeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                            employeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignmentViewModel;
        }

        public EmployeeAssignmentViewModel GetDepartmentByAssignmentId(int assignmentId)
        {

            string query = $@"select ea.DepartmentId, dep.Name as DepartmentName 
                            from EmployeesAssignments ea 
                            join Departments dep on ea.DepartmentId = dep.Id
                            where ea.Id={assignmentId}";

            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();

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
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"] == DBNull.Value ? "" : rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"]==DBNull.Value ? "" :rdr["DepartmentName"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignmentViewModel;
        }

        public List<EmployeeAssignmentViewModel> GetEmployeesBySearchFilter(EmployeeAssignment employeeAssignment)
        {

            string where = "";
            if (!string.IsNullOrEmpty(employeeAssignment.EmployeeName))
            {
                where += $" ea.EmployeeName like N'%{employeeAssignment.EmployeeName}%' and ";
            }
            if (employeeAssignment.SectionId > 0)
            {
                where += $" ea.SectionId={employeeAssignment.SectionId} and ";
            }
            if (employeeAssignment.DepartmentId >0)
            {
                where += $" ea.DepartmentId={employeeAssignment.DepartmentId} and ";
            }
            if (employeeAssignment.InchargeId >0)
            {
                where += $" ea.InChargeId={employeeAssignment.InchargeId} and ";
            }
            if (employeeAssignment.RoleId >0)
            {
                where += $" ea.RoleId={employeeAssignment.RoleId} and ";
            }
            //if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
            //{
            //    where += $" ea.ExplanationId={employeeAssignment.ExplanationId} and ";
            //}
            if (employeeAssignment.CompanyId >0)
            {
                where += $" ea.CompanyId={employeeAssignment.CompanyId} and ";
            }
            
            if (employeeAssignment.IsActive == "0" || employeeAssignment.IsActive == "1")
            {
                where += $" ea.IsActive={employeeAssignment.IsActive} and ";
            }
            else
            {
                where += $" ea.IsActive=1 and ";
            }

            where += " 1=1 ";

            string query = $@"select ea.id as AssignmentId,ep.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            Inner join Employees ep on ea.EmployeeId = ep.Id
                            where {where}
                            order by ep.FullName asc, ea.Id";
                            //ORDER BY ea.EmployeeName asc, ea.Id";


            List<EmployeeAssignmentViewModel> employeeAssignments = new List<EmployeeAssignmentViewModel>();
            //HttpContext.Current.Response.Write("query: " + query + "<br>");
            //HttpContext.Current.Response.End();

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
                            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.EmployeeName = rdr["FullName"].ToString();
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));

                            employeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            employeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            //employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeName;
                            employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            employeeAssignmentViewModel.AddNameSubCode = rdr["SubCode"].ToString();
                            employeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" :  rdr["Remarks"].ToString();
                            //if (!string.IsNullOrEmpty(rdr["SubCode"].ToString()))
                            //{
                            //    employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            //    employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeNameWithCodeRemarks + " " + employeeAssignmentViewModel.SubCode;
                            //}
                            //else
                            //{
                            //    employeeAssignmentViewModel.SubCode = 0;                                
                            //}

                            //if (!string.IsNullOrEmpty(rdr["Remarks"].ToString()))
                            //{
                            //    employeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                            //    employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeNameWithCodeRemarks + " (" + employeeAssignmentViewModel.Remarks + ")";
                            //}
                            //else
                            //{
                            //    employeeAssignmentViewModel.Remarks = "";
                            //}


                            //HttpContext.Current.Response.Write("employeeAssignmentViewModel.UnitPrice: " + employeeAssignmentViewModel.UnitPrice);
                            //HttpContext.Current.Response.End();

                            employeeAssignments.Add(employeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;
        }

        public List<ForecastAssignmentViewModel> GetEmployeesForecastBySearchFilter(EmployeeAssignmentForecast employeeAssignment)
        {

            string where = "";
            //if (!string.IsNullOrEmpty(employeeAssignment.EmployeeName))
            //{
            //    where += $" ea.EmployeeName like N'%{employeeAssignment.EmployeeName}%' and ";
            //}
            if (!string.IsNullOrEmpty(employeeAssignment.SectionId))
            {
                string tempSectionIds = "";
                if (employeeAssignment.SectionId.IndexOf(",") >= 0)
                {
                    string[] arrSectionIds = employeeAssignment.SectionId.Split(new[] { "," }, StringSplitOptions.None);
                    
                    for (int i = 0; i < arrSectionIds.Length; i++)
                    {
                        if(tempSectionIds == "")
                        {
                            tempSectionIds = arrSectionIds[i];
                        }
                        else
                        {
                            tempSectionIds = tempSectionIds +","+arrSectionIds[i];
                        }
                    }
                }
                else
                {
                    tempSectionIds = employeeAssignment.SectionId;
                }
                where += $" ea.SectionId In ({tempSectionIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.DepartmentId))
            {
                string tempDepartmentIds = "";
                if (employeeAssignment.DepartmentId.IndexOf(",") >= 0)
                {
                    string[] arrDepartmentIds = employeeAssignment.DepartmentId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrDepartmentIds.Length; i++)
                    {
                        if (tempDepartmentIds == "")
                        {
                            tempDepartmentIds = arrDepartmentIds[i];
                        }
                        else
                        {
                            tempDepartmentIds = tempDepartmentIds + "," + arrDepartmentIds[i];
                        }
                    }
                }
                else
                {
                    tempDepartmentIds = employeeAssignment.DepartmentId;
                }
                where += $" ea.DepartmentId In ({tempDepartmentIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.InchargeId))
            {
                string tempInchargeIdIds = "";
                if (employeeAssignment.InchargeId.IndexOf(",") >= 0)
                {
                    string[] arrInchargeIds = employeeAssignment.InchargeId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrInchargeIds.Length; i++)
                    {
                        if (tempInchargeIdIds == "")
                        {
                            tempInchargeIdIds = arrInchargeIds[i];
                        }
                        else
                        {
                            tempInchargeIdIds = tempInchargeIdIds + "," + arrInchargeIds[i];
                        }
                    }
                }
                else
                {
                    tempInchargeIdIds = employeeAssignment.InchargeId;
                }
                where += $" ea.InChargeId In ({tempInchargeIdIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.RoleId))
            {
                string tempRoleIds = "";
                if (employeeAssignment.RoleId.IndexOf(",") >= 0)
                {
                    string[] arrRoleIdss = employeeAssignment.RoleId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrRoleIdss.Length; i++)
                    {
                        if (tempRoleIds == "")
                        {
                            tempRoleIds = arrRoleIdss[i];
                        }
                        else
                        {
                            tempRoleIds = tempRoleIds + "," + arrRoleIdss[i];
                        }
                    }
                }
                else
                {
                    tempRoleIds = employeeAssignment.RoleId;
                }
                where += $" ea.RoleId In ({tempRoleIds}) and ";
            }

            if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
            {
                string tempExplanationIds = "";
                if (employeeAssignment.ExplanationId.IndexOf(",") >= 0)
                {
                    string[] arrtempExplanationIds = employeeAssignment.ExplanationId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrtempExplanationIds.Length; i++)
                    {
                        if (tempExplanationIds == "")
                        {
                            tempExplanationIds = arrtempExplanationIds[i];
                        }
                        else
                        {
                            tempExplanationIds = tempExplanationIds + "," + arrtempExplanationIds[i];
                        }
                    }
                }
                else
                {
                    tempExplanationIds = employeeAssignment.ExplanationId;
                }
                where += $" ea.ExplanationId IN ({tempExplanationIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.CompanyId))
            {
                string tempCompanyIds = "";
                if (employeeAssignment.CompanyId.IndexOf(",") >= 0)
                {
                    string[] arrCompanyIds = employeeAssignment.CompanyId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrCompanyIds.Length; i++)
                    {
                        if (tempCompanyIds == "")
                        {
                            tempCompanyIds = arrCompanyIds[i];
                        }
                        else
                        {
                            tempCompanyIds = tempCompanyIds + "," + arrCompanyIds[i];
                        }
                    }
                }
                else
                {
                    tempCompanyIds = employeeAssignment.CompanyId;
                }
                where += $" ea.CompanyId In ({tempCompanyIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.Year))
            {
                where += $" ea.Year={employeeAssignment.Year} and ";
            }
            
            //if (employeeAssignment.IsActive == "0" || employeeAssignment.IsActive == "1")
            //{
            //    where += $" ea.IsActive={employeeAssignment.IsActive} and ";
            //}
            //else
            //{
            //    where += $" ea.IsActive=1 and ";
            //}

            where += " 1=1 and ea.IsDeleted is null Or  ea.IsDeleted=0 ";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,IsDeletePending
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {where}
                            order by emp.Id asc";


            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();
            //HttpContext.Current.Response.Write("query: " + query + "<br>");
            //HttpContext.Current.Response.End();

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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            //forecastEmployeeAssignmentViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(forecastEmployeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"]  is DBNull ? "":  rdr["Remarks"].ToString() ;
                            forecastEmployeeAssignmentViewModel.BCYR = rdr["BCYR"] is DBNull ? false: Convert.ToBoolean(rdr["BCYR"]);
                            forecastEmployeeAssignmentViewModel.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            //forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            forecastEmployeeAssignmentViewModel.IsApproved = rdr["IsApproved"] is DBNull ? false : Convert.ToBoolean(rdr["IsApproved"]);
                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            forecastEmployeeAssignmentViewModel.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            forecastEmployeeAssignmentViewModel.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);

                            //if (!string.IsNullOrEmpty(rdr["SubCode"].ToString()))
                            //{
                            //    forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            //    forecastEmployeeAssignmentViewModel.EmployeeNameWithCodeRemarks = forecastEmployeeAssignmentViewModel.EmployeeNameWithCodeRemarks + " " + forecastEmployeeAssignmentViewModel.SubCode;
                            //}
                            //else
                            //{
                            //    forecastEmployeeAssignmentViewModel.SubCode = 0;
                            //}

                            //if (!string.IsNullOrEmpty(rdr["Remarks"].ToString()))
                            //{
                            //    forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                            //    forecastEmployeeAssignmentViewModel.EmployeeNameWithCodeRemarks = forecastEmployeeAssignmentViewModel.EmployeeNameWithCodeRemarks + " (" + forecastEmployeeAssignmentViewModel.Remarks + ")";
                            //}
                            //else
                            //{
                            //    forecastEmployeeAssignmentViewModel.Remarks = "";
                            //}

                            //if (!string.IsNullOrEmpty(rdr["Remarks"].ToString()))
                            //{
                            //    forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                            //}
                            //else
                            //{
                            //    forecastEmployeeAssignmentViewModel.Remarks = "";
                            //}
                            //if (!string.IsNullOrEmpty(rdr["SubCode"].ToString()))
                            //{
                            //    forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            //}
                            //else
                            //{
                            //    forecastEmployeeAssignmentViewModel.SubCode = 0;
                            //}

                            //HttpContext.Current.Response.Write("employeeAssignmentViewModel.UnitPrice: " + employeeAssignmentViewModel.UnitPrice);
                            //HttpContext.Current.Response.End();

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }


        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(int departmentId, string companyIds, int year)
        {

            string where = "";
            where += $" ea.DepartmentId = {departmentId} and ";

            string tempCompanyIds = "";
            string[] arrCompanyIds = companyIds.Split(new[] { "," }, StringSplitOptions.None);

            for (int i = 0; i < arrCompanyIds.Length; i++)
            {
                if (tempCompanyIds == "")
                {
                    tempCompanyIds = arrCompanyIds[i];
                }
                else
                {
                    tempCompanyIds = tempCompanyIds + "," + arrCompanyIds[i];
                }
            }
            where += $" ea.CompanyId In ({tempCompanyIds}) and ";
            where += $" ea.Year={year} and ";

            where += " 1=1 and ea.IsDeleted is null Or  ea.IsDeleted=0 ";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,IsDeletePending
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {where}
                            order by emp.Id asc";


            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();


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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            //forecastEmployeeAssignmentViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(forecastEmployeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            forecastEmployeeAssignmentViewModel.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            //forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            forecastEmployeeAssignmentViewModel.IsApproved = rdr["IsApproved"] is DBNull ? false : Convert.ToBoolean(rdr["IsApproved"]);
                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            forecastEmployeeAssignmentViewModel.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            forecastEmployeeAssignmentViewModel.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }


        public List<EmployeeAssignmentViewModel> GetEmployeesBySearchFilterForMultipleSearch(EmployeeAssignmentDTO employeeAssignment)
        {

            string where = "";
            if (!string.IsNullOrEmpty(employeeAssignment.EmployeeName))
            {
                where += $" ea.EmployeeName like N'%{employeeAssignment.EmployeeName.Trim()}%' and ";
            }
            if (employeeAssignment.Sections != null)
            {
                if (employeeAssignment.Sections.Length > 0 && employeeAssignment.Sections.ToString() != "all")
                {
                    string ids = "";
                    foreach (var item in employeeAssignment.Sections)
                    {
                        ids += $"{item},";
                    }
                    ids = ids.TrimEnd(',');

                    where += $" ea.SectionId in ({ids}) and ";
                }

            }
            if (employeeAssignment.Departments != null && employeeAssignment.Departments.ToString() != "all")
            {
                if (employeeAssignment.Departments.Length > 0)
                {
                    string ids = "";
                    foreach (var item in employeeAssignment.Departments)
                    {
                        ids += $"{item},";
                    }
                    ids = ids.TrimEnd(',');

                    where += $" ea.DepartmentId in ({ids}) and ";
                }

            }

            if (employeeAssignment.Incharges != null && employeeAssignment.Incharges.ToString() != "all")
            {
                if (employeeAssignment.Incharges.Length > 0)
                {
                    string ids = "";
                    foreach (var item in employeeAssignment.Incharges)
                    {
                        ids += $"{item},";
                    }
                    ids = ids.TrimEnd(',');

                    where += $" ea.InChargeId in ({ids}) and ";
                }

            }
            if (employeeAssignment.Roles != null && employeeAssignment.Roles.ToString() != "all")
            {
                if (employeeAssignment.Roles.Length > 0)
                {
                    string ids = "";
                    foreach (var item in employeeAssignment.Roles)
                    {
                        ids += $"{item},";
                    }
                    ids = ids.TrimEnd(',');

                    where += $" ea.RoleId in ({ids}) and ";
                }

            }

            //if (employeeAssignment.Explanations != null)
            //{
            //    if (employeeAssignment.Explanations.Length > 0)
            //    {
            //        string ids = "";
            //        foreach (var item in employeeAssignment.Explanations)
            //        {
            //            ids += $"{item},";
            //        }
            //        ids = ids.TrimEnd(',');

            //        where += $" ea.ExplanationId in ({ids}) and ";
            //    }

            //}
            if (employeeAssignment.Companies != null && employeeAssignment.Companies.ToString() != "all")
            {
                if (employeeAssignment.Companies.Length > 0)
                {
                    string ids = "";
                    foreach (var item in employeeAssignment.Companies)
                    {
                        ids += $"{item},";
                    }
                    ids = ids.TrimEnd(',');

                    where += $" ea.CompanyId in ({ids}) and ";
                }

            }
            else
            {
                where += $" ea.IsActive=1 and ";
            }

            where += " 1=1 ";
            string query = $@"select ea.id as AssignmentId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode,ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive
                            from EmployeesAssignments ea join Sections sec on ea.SectionId = sec.Id
                            join Departments dep on ea.DepartmentId = dep.Id
                            join Companies com on ea.CompanyId = com.Id
                            
                            join Roles rl on ea.RoleId = rl.Id
                            join InCharges inc on ea.InChargeId = inc.Id 
                            join Grades gd on ea.GradeId = gd.Id
                            where {where}";

            List<EmployeeAssignmentViewModel> employeeAssignments = new List<EmployeeAssignmentViewModel>();


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
                            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]).ToString("N2");
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            employeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            employeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeName;

                            if (!string.IsNullOrEmpty(rdr["SubCode"].ToString()))
                            {
                                employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                                employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeNameWithCodeRemarks + " " + employeeAssignmentViewModel.SubCode;
                            }
                            else
                            {
                                employeeAssignmentViewModel.SubCode = 0;
                            }

                            if (!string.IsNullOrEmpty(rdr["Remarks"].ToString()))
                            {
                                employeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                                employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeNameWithCodeRemarks + " (" + employeeAssignmentViewModel.Remarks + ")";
                            }
                            else
                            {
                                employeeAssignmentViewModel.Remarks = "";
                            }




                            employeeAssignments.Add(employeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;
        }


        public List<EmployeeAssignmentViewModel> GetEmployeesByName(string employeeName)
        {
            string where = $"emp.FullName = N'{employeeName}'";

            string query = $@"select ea.id as AssignmentId,emp.FullName,ea.EmployeeId,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode,ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive
                            from EmployeesAssignments ea join Sections sec on ea.SectionId = sec.Id
                            join Departments dep on ea.DepartmentId = dep.Id
                            join Companies com on ea.CompanyId = com.Id
                            join Roles rl on ea.RoleId = rl.Id
                            join InCharges inc on ea.InChargeId = inc.Id 
                            join Grades gd on ea.GradeId = gd.Id
                            join Employees emp on ea.EmployeeId = emp.Id
                            where {where} order by ea.SubCode asc";

            List<EmployeeAssignmentViewModel> employeeAssignments = new List<EmployeeAssignmentViewModel>();

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
                            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            //employeeAssignmentViewModel.UnitPrice = rdr["UnitPrice"].ToString();
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            employeeAssignmentViewModel.UnitPriceWithoutComma = Convert.ToInt32(rdr["UnitPrice"].ToString());
                            employeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            employeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            if (!string.IsNullOrEmpty(rdr["Remarks"].ToString()))
                            {
                                employeeAssignmentViewModel.Remarks = rdr["Remarks"].ToString();
                            }
                            else
                            {
                                employeeAssignmentViewModel.Remarks = "";
                            }
                            if (!string.IsNullOrEmpty(rdr["SubCode"].ToString()))
                            {
                                employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            }
                            else
                            {
                                employeeAssignmentViewModel.SubCode = 0;
                            }



                            employeeAssignments.Add(employeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;
        }

        public int RemoveAssignment(int rowId)
        {
            int result = 0;
            //string query = $@"update EmployeesAssignments set isactive=0 where id=@id";
            string query = $@"update EmployeesAssignments set isactive=0,BCYRCell='',BCYRCellPending='',IsRowPending='',IsDeletePending='' where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", rowId);
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

        public List<ForecastDto> GetForecastsByAssignmentId(int assignmentId,string year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from Costs where EmployeeAssignmentsId=" + assignmentId+ " and Year="+ year;
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
                            ForecastDto forecast = new ForecastDto();
                            forecast.ForecastId = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);
                            //forecast.Total = rdr["Total"].ToString();
                            //forecast.Total = rdr["Total"].ToString();
                            //forecast.Total = Convert.ToDecimal(forecast.Total).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //forecast.Total = Convert.ToInt32(rdr["Total"]).ToString("N0");

                            //if (String.IsNullOrEmpty(forecast.Total))
                            //{
                            //    forecast.Total = "0";
                            //}
                            //if (!string.IsNullOrEmpty(rdr["Total"].ToString())) {
                            //    forecast.Total = rdr["Total"].ToString();
                            //    //forecast.Total = (Convert.ToDecimal(forecast.Total)).ToString("N", new CultureInfo("en-US"));
                            //    forecast.Total = Convert.ToDecimal(forecast.Total).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //}
                            //else
                            //{
                            //    forecast.Total = "0";
                            //}

                            forecasts.Add(forecast);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecasts;
        }

        public List<ForecastDto> GetBudgetForecastById(int assignmentId, string year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from BudgetCosts where EmployeeBudgetId=" + assignmentId + " and Year=" + year;
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
                            ForecastDto forecast = new ForecastDto();
                            forecast.ForecastId = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);                            

                            forecasts.Add(forecast);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecasts;
        }


        public List<ForecastDto> GetApprovedForecastdData(int assignmentId, string year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from ApprovedCosts where ApprovedEmployeeAssignmentsId=" + assignmentId + " and Year=" + year;
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
                            ForecastDto forecast = new ForecastDto();
                            forecast.ForecastId = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);
                            //forecast.Total = rdr["Total"].ToString();
                            //forecast.Total = rdr["Total"].ToString();
                            //forecast.Total = Convert.ToDecimal(forecast.Total).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //forecast.Total = Convert.ToInt32(rdr["Total"]).ToString("N0");

                            //if (String.IsNullOrEmpty(forecast.Total))
                            //{
                            //    forecast.Total = "0";
                            //}
                            //if (!string.IsNullOrEmpty(rdr["Total"].ToString())) {
                            //    forecast.Total = rdr["Total"].ToString();
                            //    //forecast.Total = (Convert.ToDecimal(forecast.Total)).ToString("N", new CultureInfo("en-US"));
                            //    forecast.Total = Convert.ToDecimal(forecast.Total).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //}
                            //else
                            //{
                            //    forecast.Total = "0";
                            //}

                            forecasts.Add(forecast);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecasts;
        }

        public bool CheckEmployeeName(string employeeName)
        {
            string query = "select * from EmployeesAssignments where EmployeeName=N'"+employeeName+"'";
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

        public void DeleteAssignment_Excel(int assignmentId)
        {
            string queryForAssignment = $@"delete from EmployeesAssignments where id=@assignmentId";
            string queryForCost = $@"delete from Costs where EmployeeAssignmentsId=@assignmentId";
            string queryForCostHistories = $@"delete from CostHistories where EmployeeAssignmentsId=@assignmentId";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmdForAssignment = new SqlCommand(queryForAssignment, sqlConnection);
                SqlCommand cmdForCost = new SqlCommand(queryForCost, sqlConnection);
                SqlCommand cmdForCostHistories = new SqlCommand(queryForCostHistories, sqlConnection);

                cmdForAssignment.Parameters.AddWithValue("@assignmentId", assignmentId);
                cmdForCost.Parameters.AddWithValue("@assignmentId", assignmentId);
                cmdForCostHistories.Parameters.AddWithValue("@assignmentId", assignmentId);
                try
                {
                    cmdForAssignment.ExecuteNonQuery();
                    cmdForCost.ExecuteNonQuery();
                    cmdForCostHistories.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

            }
        }

        public List<EmployeeAssignmentViewModel> GetAssignmentsByYear(int year)
        {
            List<EmployeeAssignmentViewModel> employeeAssignments = new List<EmployeeAssignmentViewModel>();

            string query = $@"select ea.id as AssignmentId,ep.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            Inner join Employees ep on ea.EmployeeId = ep.Id
                            where ea.Year={year}
                            order by ep.FullName asc, ea.Id";


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
                            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.EmployeeName = rdr["FullName"].ToString();
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));

                            employeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            employeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            //employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeName;
                            employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            employeeAssignmentViewModel.AddNameSubCode = rdr["SubCode"].ToString();
                            employeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            

                            employeeAssignments.Add(employeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;



        }

        public List<EmployeeAssignmentViewModel> GetSpecificAssignmentDataData(int year,int monthId)
        {
            List<EmployeeAssignmentViewModel> employeeAssignments = new List<EmployeeAssignmentViewModel>();

            string query = $@"select ea.id as AssignmentId,ep.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive, cst.Points, cst.Total 
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            Inner join Employees ep on ea.EmployeeId = ep.Id
                            Inner join Costs cst on ea.Id = cst.EmployeeAssignmentsId
                            where ea.Year={year} and cst.MonthId={monthId}
                            order by ep.FullName asc, ea.Id";


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
                            EmployeeAssignmentViewModel employeeAssignmentViewModel = new EmployeeAssignmentViewModel();
                            employeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignmentViewModel.EmployeeName = rdr["FullName"].ToString();
                            employeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            employeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            employeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            employeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            employeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            employeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            employeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            employeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            employeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            employeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            employeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            //employeeAssignmentViewModel.UnitPrice = Convert.ToInt32(employeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));

                            employeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            employeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            //employeeAssignmentViewModel.EmployeeNameWithCodeRemarks = employeeAssignmentViewModel.EmployeeName;
                            employeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            employeeAssignmentViewModel.AddNameSubCode = rdr["SubCode"].ToString();
                            employeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            employeeAssignmentViewModel.ForecastedPoints = Convert.ToDecimal(rdr["Points"]);
                            employeeAssignmentViewModel.ForecastedTotal = Convert.ToDecimal(rdr["Total"]);

                            employeeAssignments.Add(employeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;



        }

        public string GetBCYRCellByAssignmentId(int assignmentId)
        {

            string query = $@"select BCYRCell from EmployeesAssignments where Id={assignmentId}";

            string result="";

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
                            
                            result = rdr["BCYRCell"] is DBNull ? "": rdr["BCYRCell"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public int UpdateBCYRCellByAssignmentId(int assignmentId, string cell)
        {

            string query = $@"update EmployeesAssignments set BCYRCell ='{cell}' where Id={assignmentId}";

            int result = 0;

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
            }

            return result;
        }

        public int ApproveAssignement(string approvedAssignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYRApproved=@bCYRApproved where Id=@approvedAssignmentId and BCYR=1";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@bCYRApproved", 1);
                cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);
                
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

        public int UnApproveAssignement(string approvedAssignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYRApproved=@bCYRApproved where Id=@approvedAssignmentId and BCYR=1";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@bCYRApproved", 0);
                cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);
                
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

        public int ApproveDeletedRow(string approvedAssignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set IsActive=0,BCYRApproved=1,IsDeleted=0 where Id=@approvedAssignmentId and IsActive =0";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRApproved", 1);
                cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);

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
        
        //un approve delete data
        public int UnApproveDeletedRow(string approvedAssignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set IsActive=0,BCYRApproved=0,IsDeleted=0 where Id=@approvedAssignmentId and IsActive =0";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRApproved", 1);
                cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);

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

        public int UpdateApprovedDataForDeleteRows(string assignmentYear)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYR= 0,BCYRApproved=0,IsDeleted=1 where IsActive =0 and BCYRApproved=1 and (IsDeleted is null Or  IsDeleted=0 ) and Year={assignmentYear}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRApproved", 1);
                //cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);

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

        //get approval data
        public List<ForecastAssignmentViewModel> GetApprovalEmployeesBySearchFilter(EmployeeAssignmentForecast employeeAssignment)
        {

            string where = "";
            if (!string.IsNullOrEmpty(employeeAssignment.SectionId))
            {
                string tempSectionIds = "";
                if (employeeAssignment.SectionId.IndexOf(",") >= 0)
                {
                    string[] arrSectionIds = employeeAssignment.SectionId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrSectionIds.Length; i++)
                    {
                        if (tempSectionIds == "")
                        {
                            tempSectionIds = arrSectionIds[i];
                        }
                        else
                        {
                            tempSectionIds = tempSectionIds + "," + arrSectionIds[i];
                        }
                    }
                }
                else
                {
                    tempSectionIds = employeeAssignment.SectionId;
                }
                where += $" ea.SectionId In ({tempSectionIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.DepartmentId))
            {
                string tempDepartmentIds = "";
                if (employeeAssignment.DepartmentId.IndexOf(",") >= 0)
                {
                    string[] arrDepartmentIds = employeeAssignment.DepartmentId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrDepartmentIds.Length; i++)
                    {
                        if (tempDepartmentIds == "")
                        {
                            tempDepartmentIds = arrDepartmentIds[i];
                        }
                        else
                        {
                            tempDepartmentIds = tempDepartmentIds + "," + arrDepartmentIds[i];
                        }
                    }
                }
                else
                {
                    tempDepartmentIds = employeeAssignment.DepartmentId;
                }
                where += $" ea.DepartmentId In ({tempDepartmentIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.InchargeId))
            {
                string tempInchargeIdIds = "";
                if (employeeAssignment.InchargeId.IndexOf(",") >= 0)
                {
                    string[] arrInchargeIds = employeeAssignment.InchargeId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrInchargeIds.Length; i++)
                    {
                        if (tempInchargeIdIds == "")
                        {
                            tempInchargeIdIds = arrInchargeIds[i];
                        }
                        else
                        {
                            tempInchargeIdIds = tempInchargeIdIds + "," + arrInchargeIds[i];
                        }
                    }
                }
                else
                {
                    tempInchargeIdIds = employeeAssignment.InchargeId;
                }
                where += $" ea.InChargeId In ({tempInchargeIdIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.RoleId))
            {
                string tempRoleIds = "";
                if (employeeAssignment.RoleId.IndexOf(",") >= 0)
                {
                    string[] arrRoleIdss = employeeAssignment.RoleId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrRoleIdss.Length; i++)
                    {
                        if (tempRoleIds == "")
                        {
                            tempRoleIds = arrRoleIdss[i];
                        }
                        else
                        {
                            tempRoleIds = tempRoleIds + "," + arrRoleIdss[i];
                        }
                    }
                }
                else
                {
                    tempRoleIds = employeeAssignment.RoleId;
                }
                where += $" ea.RoleId In ({tempRoleIds}) and ";
            }

            if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
            {
                string tempExplanationIds = "";
                if (employeeAssignment.ExplanationId.IndexOf(",") >= 0)
                {
                    string[] arrtempExplanationIds = employeeAssignment.ExplanationId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrtempExplanationIds.Length; i++)
                    {
                        if (tempExplanationIds == "")
                        {
                            tempExplanationIds = arrtempExplanationIds[i];
                        }
                        else
                        {
                            tempExplanationIds = tempExplanationIds + "," + arrtempExplanationIds[i];
                        }
                    }
                }
                else
                {
                    tempExplanationIds = employeeAssignment.ExplanationId;
                }
                where += $" ea.ExplanationId IN ({tempExplanationIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.CompanyId))
            {
                string tempCompanyIds = "";
                if (employeeAssignment.CompanyId.IndexOf(",") >= 0)
                {
                    string[] arrCompanyIds = employeeAssignment.CompanyId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrCompanyIds.Length; i++)
                    {
                        if (tempCompanyIds == "")
                        {
                            tempCompanyIds = arrCompanyIds[i];
                        }
                        else
                        {
                            tempCompanyIds = tempCompanyIds + "," + arrCompanyIds[i];
                        }
                    }
                }
                else
                {
                    tempCompanyIds = employeeAssignment.CompanyId;
                }
                where += $" ea.CompanyId In ({tempCompanyIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.Year))
            {
                where += $" ea.Year={employeeAssignment.Year} and ";
            }

            //if (employeeAssignment.IsActive == "0" || employeeAssignment.IsActive == "1")
            //{
            //    where += $" ea.IsActive={employeeAssignment.IsActive} and ";
            //}
            //else
            //{
            //    where += $" ea.IsActive=1 and ";
            //}

            where += " 1=1 and ea.IsDeleted is null Or  ea.IsDeleted=0";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,IsDeletePending
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {where}
                            order by emp.Id asc";

            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();          
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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            //forecastEmployeeAssignmentViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(forecastEmployeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            forecastEmployeeAssignmentViewModel.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            //forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            forecastEmployeeAssignmentViewModel.IsApproved = rdr["IsApproved"] is DBNull ? false : Convert.ToBoolean(rdr["IsApproved"]);
                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            forecastEmployeeAssignmentViewModel.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            forecastEmployeeAssignmentViewModel.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }

            return forecastEmployeeAssignments;
        }
        public int UpdateApprovedData(string assignmentYear)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYR= 0,BCYRApproved=0 where BCYR= 1 and BCYRApproved=1  and Year={assignmentYear}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRApproved", 1);
                //cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);

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
        public EmployeeAssignment GetPreviousApprovedCells(string assignementId)
        {        
            EmployeeAssignment _employeeAssignments = new EmployeeAssignment();
            string query = "select BCYRCell,BCYRCellApproved,BCYRCellPending from EmployeesAssignments where  id=" + assignementId;
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

                            _employeeAssignments.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            _employeeAssignments.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            _employeeAssignments.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return _employeeAssignments;

        }
        public int UpdateBYCRCells(string assignementId, string bCYRCellApproved,string storeBYCRCells) {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYRCell=@bCYRCell,BCYRCellApproved=@bCYRCellApproved where Id=" + assignementId;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@bCYRCell", storeBYCRCells);
                cmd.Parameters.AddWithValue("@bCYRCellApproved", bCYRCellApproved);

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

        public int UpdateCellWiseApprovdData(string assignmentYear)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYRCellApproved='',IsApproved=1 where BCYRCellApproved is not null and BCYRCellApproved !='' and BCYRCellApproved !='0' and Year={assignmentYear}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRApproved", 1);
                //cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);

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
        public List<EmployeeAssignment> GetPendingCells(string assignmentYear)
        {
            List<EmployeeAssignment> _employeeAssignments = new List<EmployeeAssignment>();

            string query = $@"select * from EmployeesAssignments where BCYRCell is not null and BCYRCell <> '' and BCYR <> 1 and IsActive =1 and Year={assignmentYear}";
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
                            employeeAssignment.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            employeeAssignment.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            employeeAssignment.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();                            

                            _employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return _employeeAssignments;
        }

        public List<EmployeeAssignment> GetPendingDeleteRows(string assignmentYear)
        {
            List<EmployeeAssignment> _employeeAssignments = new List<EmployeeAssignment>();

            string query = $@"select * from EmployeesAssignments
                            where IsActive=0 and (IsDeleted is null Or IsDeleted <> 1) and (IsDeletePending is null or IsDeletePending =0)   and Year={assignmentYear}";
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
                            employeeAssignment.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            employeeAssignment.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            employeeAssignment.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();

                            _employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return _employeeAssignments;
        }

        public int UpdatePendingCells(EmployeeAssignment employeeAssignments)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYRCell='',BCYRCellPending=@bCYRCellPending where Id={employeeAssignments.Id}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@bCYRCellPending", employeeAssignments.BCYRCell);

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

        //Update Approved Cells 
        public int UpdateCellsByAssignmentid(string updatedApprovedCells, string updatePendingCells, int assignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYRCell=@updatedApprovedCells,BCYRCellPending=@updatePendingCells where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@updatedApprovedCells", updatedApprovedCells);
                cmd.Parameters.AddWithValue("@updatePendingCells", updatePendingCells);
                cmd.Parameters.AddWithValue("@id", assignmentId);
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
        
        //updated approved row
        public int UpdateApprovedRowByAssignmentId(int assignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set BCYR=0,IsRowPending=0 where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);               
                cmd.Parameters.AddWithValue("@id", assignmentId);
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

        //updated un-approved row
        public int UpdateUnapprovedPendingRows(int year)
        {
            int result = 0;
            string query = $@"Update EmployeesAssignments Set BCYR=0,IsRowPending=1 where BCYR=1 and IsActive=1 and Year ={year}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@id", assignmentId);
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

        //updated un-approved Delete row
        public int UpdateUnapprovedPendingDeleteRows(int year)
        {
            int result = 0;
            string query = $@"Update EmployeesAssignments Set IsDeletePending=1 where (IsActive is null or IsActive=0) and (IsDeleted is null or IsDeleted=0) and Year ={year}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@id", assignmentId);
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

        //updated delete row
        public int UpdateDeletedRowByAssignmentId(int assignmentId)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set IsDeleted=1,IsDeletePending=0 where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", assignmentId);
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

        public List<EmployeeAssignment> GetPendingAddEmployee(string assignmentYear)
        {
            List<EmployeeAssignment> _employeeAssignments = new List<EmployeeAssignment>();

            string query = $@"select * from EmployeesAssignments
                            where BCYR=1 and (BCYRApproved is null OR BCYRApproved =0) and (IsRowPending is null or IsRowPending =0)  and Year={assignmentYear}";
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
                            employeeAssignment.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            employeeAssignment.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            employeeAssignment.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();

                            _employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return _employeeAssignments;
        }


        public int UpdatePendingDeleteRows(EmployeeAssignment employeeAssignments)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set IsDeletePending=1 where Id={employeeAssignments.Id}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRCellPending", employeeAssignments.BCYRCell);

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

        public int UpdatePendingAddEmployee(EmployeeAssignment employeeAssignments)
        {
            int result = 0;
            string query = $@"update EmployeesAssignments set IsRowPending =1 where Id={employeeAssignments.Id}";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@bCYRCellPending", employeeAssignments.BCYRCell);

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


        public int UpdateUnapprovedData(int year)
        {
            int result = 0;
            string query = $@"Update EmployeesAssignments Set IsApproved =@isApproved
                            WHERE Year={year} AND (IsApproved is null OR IsApproved=0) AND ((BCYR=1 OR (BCYRCell is not null AND BCYRCell <> '' )) 
	                        OR (IsActive=0 AND (IsDeleted is null OR IsDeleted=0)))";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isApproved", 1);
                //cmd.Parameters.AddWithValue("@approvedAssignmentId", approvedAssignmentId);

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
        public bool CheckForUnApprovedCells(string assignementId, string selectedCells)
        {
            EmployeeAssignment _employeeAssignments = new EmployeeAssignment();
            string query = "select * from EmployeesAssignments where  id=" + assignementId;
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

                            _employeeAssignments.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            _employeeAssignments.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            bool isValidData = false;
            if (!string.IsNullOrEmpty(_employeeAssignments.BCYRCellApproved))
            {
                var arrApprovedCells = _employeeAssignments.BCYRCellApproved.Split(',');
                foreach(var itemCells in arrApprovedCells)
                {
                    if(itemCells == selectedCells)
                    {
                        isValidData = true;
                    }
                }
            }
            return isValidData;
        }
        public int CheckForApprovedCells(string assignementId, string selectedCells)
        {
            EmployeeAssignment _employeeAssignments = new EmployeeAssignment();
            bool isBCYRCell = false;
            bool isBCYRCellPending = false;

            string query = "select * from EmployeesAssignments where  id=" + assignementId;
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

                            _employeeAssignments.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            _employeeAssignments.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            _employeeAssignments.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            bool isValidData = false;
            if (!string.IsNullOrEmpty(_employeeAssignments.BCYRCell))
            {
                var arrApprovedCells = _employeeAssignments.BCYRCell.Split(',');
                foreach (var itemCells in arrApprovedCells)
                {
                    if (itemCells == selectedCells)
                    {
                        isValidData = true;
                        isBCYRCell = true;
                    }
                }
            }
            if (!string.IsNullOrEmpty(_employeeAssignments.BCYRCellPending))
            {
                var arrApprovedPendingCells = _employeeAssignments.BCYRCellPending.Split(',');
                foreach (var itemCells in arrApprovedPendingCells)
                {
                    if (itemCells == selectedCells)
                    {
                        isValidData = true;
                        isBCYRCellPending = true;
                    }
                }
            }
            int resultData = 0;
            if (isBCYRCell)
            {
                resultData = 1;
            }else if (isBCYRCellPending)
            {
                resultData = 2;
            }
            return resultData;
        }

        public EmployeeAssignment GetEmployeeAssignmentForCheckApproval(string assignementId)
        {
            EmployeeAssignment _employeeAssignments = new EmployeeAssignment();
            string query = "select * from EmployeesAssignments where  id=" + assignementId;
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
                            _employeeAssignments.Id = Convert.ToInt32(rdr["Id"]);

                            _employeeAssignments.IsActive = rdr["IsActive"] is DBNull ? "" : rdr["IsActive"].ToString();
                            _employeeAssignments.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            _employeeAssignments.IsDeleted = rdr["IsDeleted"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleted"]);

                            _employeeAssignments.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            _employeeAssignments.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return _employeeAssignments;            
        }


        public bool CheckForUnApprovedRow(string assignementId,bool isDeletedRow)
        {
            EmployeeAssignment _employeeAssignments = new EmployeeAssignment();
            string query = "select * from EmployeesAssignments where  id=" + assignementId;
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

                            _employeeAssignments.IsActive = rdr["IsActive"] is DBNull ? "" : rdr["IsActive"].ToString();                            
                            _employeeAssignments.IsDeleted = rdr["IsDeleted"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleted"]);
                            _employeeAssignments.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]); 

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            bool isValidData = false;
            if (_employeeAssignments.BCYRApproved)
            {
                isValidData = true;
            }
            //if (isDeletedRow)
            //{
            //    if (_employeeAssignments.BCYRApproved)
            //    {
            //        isValidData = true;
            //    }
            //}
            //else
            //{
            //    if (!Convert.ToBoolean(_employeeAssignments.IsActive) && !_employeeAssignments.IsDeleted)
            //    {
            //        isValidData = true;
            //    }
            //}
            return isValidData;
        }

        public List<ForecastAssignmentViewModel> GetAllOriginalDataForDownloadFiles(EmployeeAssignmentForecast employeeAssignment,int approvedTimestampid)
        {
            string where = "";
         
            if (!string.IsNullOrEmpty(employeeAssignment.Year))
            {
                where += $" ea.Year={employeeAssignment.Year} and ";
            }
            if (!string.IsNullOrEmpty(approvedTimestampid.ToString()))
            {
                where += $" ea.ApprovedTimeStampId={approvedTimestampid} and ";
            }
            where += " 1=1 and (ema.IsRowPending is null or ema.IsRowPending =0)";

            string query = $@"select ea.Id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,ea.IsDeletePending,ea.IsAddEmployee,ea.IsDeleteEmployee,ea.IsCellWiseUpdate,ea.ApprovedCells,emp.FullName 'RootEmployeeName'
                            ,ea.IsDeleted,ea.AssignmentId 'EmployeeAssignmentIdOrg',ema.IsRowPending 'PendingRowAfterApprove'
                            from ApprovedEmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            left join EmployeesAssignments ema on ea.AssignmentId = ema.Id
                            where {where}
                            order by emp.Id asc";


            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();

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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            //forecastEmployeeAssignmentViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(forecastEmployeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            forecastEmployeeAssignmentViewModel.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            //forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            forecastEmployeeAssignmentViewModel.IsApproved = rdr["IsApproved"] is DBNull ? false : Convert.ToBoolean(rdr["IsApproved"]);                            
                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            forecastEmployeeAssignmentViewModel.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            forecastEmployeeAssignmentViewModel.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);

                            forecastEmployeeAssignmentViewModel.IsAddEmployee = rdr["IsAddEmployee"] is DBNull ? false : Convert.ToBoolean(rdr["IsAddEmployee"]);
                            forecastEmployeeAssignmentViewModel.IsDeleteEmployee = rdr["IsDeleteEmployee"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleteEmployee"]);
                            forecastEmployeeAssignmentViewModel.IsCellWiseUpdate = rdr["IsCellWiseUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsCellWiseUpdate"]);
                            forecastEmployeeAssignmentViewModel.ApprovedCells = rdr["ApprovedCells"] is DBNull ? "" : rdr["ApprovedCells"].ToString();                            
                            forecastEmployeeAssignmentViewModel.EmployeeAssignmentIdOrg = rdr["EmployeeAssignmentIdOrg"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeAssignmentIdOrg"]);
                            forecastEmployeeAssignmentViewModel.RootEmployeeName = rdr["RootEmployeeName"] is DBNull ? "" : rdr["RootEmployeeName"].ToString();

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }
        public List<ForecastDistributdViewModal> GetQCAssignemntsPercentage(int assignmentId)
        {
            string query = $@"SELECt qp.Id,qp.DepartmentId,d.Name 'DepartmentName',qp.OctPercentage,qp.NovPercentage,qp.DecPercentage,qp.JanPercentage,qp.FebPercentage,qp.MarPercentage
	                        ,qp.AprPercentage,qp.Maypercentage,qp.JunPercentage,qp.JulPercentage,qp.AugPercentage,qp.SepPercentage
                        FROM QaProportions qp
	                        INNER JOIN Departments d ON qp.DepartmentId = d.Id
                        WHERE qp.AssignmentId={assignmentId} ";

            List<ForecastDistributdViewModal> forecastEmployeeAssignments = new List<ForecastDistributdViewModal>();

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
                            ForecastDistributdViewModal forecastEmployeeAssignmentViewModel = new ForecastDistributdViewModal();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["Id"]);
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();

                            forecastEmployeeAssignmentViewModel.OctPercentage = rdr["OctPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["OctPercentage"]);
                            forecastEmployeeAssignmentViewModel.NovPercentage = rdr["NovPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["NovPercentage"]);
                            forecastEmployeeAssignmentViewModel.DecPercentage = rdr["DecPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["DecPercentage"]);
                            forecastEmployeeAssignmentViewModel.JanPercentage = rdr["JanPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["JanPercentage"]);
                            forecastEmployeeAssignmentViewModel.FebPercentage = rdr["FebPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["FebPercentage"]);
                            forecastEmployeeAssignmentViewModel.MarPercentage = rdr["MarPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["MarPercentage"]);
                            forecastEmployeeAssignmentViewModel.AprPercentage = rdr["AprPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["AprPercentage"]);
                            forecastEmployeeAssignmentViewModel.Maypercentage = rdr["Maypercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["Maypercentage"]);
                            forecastEmployeeAssignmentViewModel.JunPercentage = rdr["JunPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["JunPercentage"]);
                            forecastEmployeeAssignmentViewModel.JulPercentage = rdr["JulPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["JulPercentage"]);                            
                            forecastEmployeeAssignmentViewModel.AugPercentage = rdr["AugPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["AugPercentage"]);
                            forecastEmployeeAssignmentViewModel.SepPercentage = rdr["SepPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["SepPercentage"]);
                            
                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }

        public List<ForecastDistributdViewModal> GetQCAssignemntsPercentageByEmployeeIdAndYear(int employeeId,int year)
        {
            string query = $@"SELECt qp.Id,qp.DepartmentId,d.Name 'DepartmentName',qp.OctPercentage,qp.NovPercentage,qp.DecPercentage,qp.JanPercentage,qp.FebPercentage,qp.MarPercentage
	                            ,qp.AprPercentage,qp.Maypercentage,qp.JunPercentage,qp.JulPercentage,qp.AugPercentage,qp.SepPercentage
                            FROM QaProportions qp
	                            INNER JOIN Departments d ON qp.DepartmentId = d.Id
                            WHERE qp.EmployeeId={employeeId} and Year={year} ";

            List<ForecastDistributdViewModal> forecastEmployeeAssignments = new List<ForecastDistributdViewModal>();

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
                            ForecastDistributdViewModal forecastEmployeeAssignmentViewModel = new ForecastDistributdViewModal();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["Id"]);
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();

                            forecastEmployeeAssignmentViewModel.OctPercentage = rdr["OctPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["OctPercentage"]);
                            forecastEmployeeAssignmentViewModel.NovPercentage = rdr["NovPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["NovPercentage"]);
                            forecastEmployeeAssignmentViewModel.DecPercentage = rdr["DecPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["DecPercentage"]);
                            forecastEmployeeAssignmentViewModel.JanPercentage = rdr["JanPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["JanPercentage"]);
                            forecastEmployeeAssignmentViewModel.FebPercentage = rdr["FebPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["FebPercentage"]);
                            forecastEmployeeAssignmentViewModel.MarPercentage = rdr["MarPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["MarPercentage"]);
                            forecastEmployeeAssignmentViewModel.AprPercentage = rdr["AprPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["AprPercentage"]);
                            forecastEmployeeAssignmentViewModel.Maypercentage = rdr["Maypercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["Maypercentage"]);
                            forecastEmployeeAssignmentViewModel.JunPercentage = rdr["JunPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["JunPercentage"]);
                            forecastEmployeeAssignmentViewModel.JulPercentage = rdr["JulPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["JulPercentage"]);
                            forecastEmployeeAssignmentViewModel.AugPercentage = rdr["AugPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["AugPercentage"]);
                            forecastEmployeeAssignmentViewModel.SepPercentage = rdr["SepPercentage"] is DBNull ? 0 : Convert.ToDecimal(rdr["SepPercentage"]);

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }
        public List<ForecastAssignmentViewModel> GetAllAssignmentData(EmployeeAssignmentForecast employeeAssignment)
        {

            string where = "";
            //if (!string.IsNullOrEmpty(employeeAssignment.EmployeeName))
            //{
            //    where += $" ea.EmployeeName like N'%{employeeAssignment.EmployeeName}%' and ";
            //}
            if (!string.IsNullOrEmpty(employeeAssignment.SectionId))
            {
                string tempSectionIds = "";
                if (employeeAssignment.SectionId.IndexOf(",") >= 0)
                {
                    string[] arrSectionIds = employeeAssignment.SectionId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrSectionIds.Length; i++)
                    {
                        if (tempSectionIds == "")
                        {
                            tempSectionIds = arrSectionIds[i];
                        }
                        else
                        {
                            tempSectionIds = tempSectionIds + "," + arrSectionIds[i];
                        }
                    }
                }
                else
                {
                    tempSectionIds = employeeAssignment.SectionId;
                }
                where += $" ea.SectionId In ({tempSectionIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.DepartmentId))
            {
                string tempDepartmentIds = "";
                if (employeeAssignment.DepartmentId.IndexOf(",") >= 0)
                {
                    string[] arrDepartmentIds = employeeAssignment.DepartmentId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrDepartmentIds.Length; i++)
                    {
                        if (tempDepartmentIds == "")
                        {
                            tempDepartmentIds = arrDepartmentIds[i];
                        }
                        else
                        {
                            tempDepartmentIds = tempDepartmentIds + "," + arrDepartmentIds[i];
                        }
                    }
                }
                else
                {
                    tempDepartmentIds = employeeAssignment.DepartmentId;
                }
                where += $" ea.DepartmentId In ({tempDepartmentIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.InchargeId))
            {
                string tempInchargeIdIds = "";
                if (employeeAssignment.InchargeId.IndexOf(",") >= 0)
                {
                    string[] arrInchargeIds = employeeAssignment.InchargeId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrInchargeIds.Length; i++)
                    {
                        if (tempInchargeIdIds == "")
                        {
                            tempInchargeIdIds = arrInchargeIds[i];
                        }
                        else
                        {
                            tempInchargeIdIds = tempInchargeIdIds + "," + arrInchargeIds[i];
                        }
                    }
                }
                else
                {
                    tempInchargeIdIds = employeeAssignment.InchargeId;
                }
                where += $" ea.InChargeId In ({tempInchargeIdIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.RoleId))
            {
                string tempRoleIds = "";
                if (employeeAssignment.RoleId.IndexOf(",") >= 0)
                {
                    string[] arrRoleIdss = employeeAssignment.RoleId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrRoleIdss.Length; i++)
                    {
                        if (tempRoleIds == "")
                        {
                            tempRoleIds = arrRoleIdss[i];
                        }
                        else
                        {
                            tempRoleIds = tempRoleIds + "," + arrRoleIdss[i];
                        }
                    }
                }
                else
                {
                    tempRoleIds = employeeAssignment.RoleId;
                }
                where += $" ea.RoleId In ({tempRoleIds}) and ";
            }

            if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
            {
                string tempExplanationIds = "";
                if (employeeAssignment.ExplanationId.IndexOf(",") >= 0)
                {
                    string[] arrtempExplanationIds = employeeAssignment.ExplanationId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrtempExplanationIds.Length; i++)
                    {
                        if (tempExplanationIds == "")
                        {
                            tempExplanationIds = arrtempExplanationIds[i];
                        }
                        else
                        {
                            tempExplanationIds = tempExplanationIds + "," + arrtempExplanationIds[i];
                        }
                    }
                }
                else
                {
                    tempExplanationIds = employeeAssignment.ExplanationId;
                }
                where += $" ea.ExplanationId IN ({tempExplanationIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.CompanyId))
            {
                string tempCompanyIds = "";
                if (employeeAssignment.CompanyId.IndexOf(",") >= 0)
                {
                    string[] arrCompanyIds = employeeAssignment.CompanyId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrCompanyIds.Length; i++)
                    {
                        if (tempCompanyIds == "")
                        {
                            tempCompanyIds = arrCompanyIds[i];
                        }
                        else
                        {
                            tempCompanyIds = tempCompanyIds + "," + arrCompanyIds[i];
                        }
                    }
                }
                else
                {
                    tempCompanyIds = employeeAssignment.CompanyId;
                }
                where += $" ea.CompanyId In ({tempCompanyIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.Year))
            {
                where += $" ea.Year={employeeAssignment.Year} and ";
            }

            //if (employeeAssignment.IsActive == "0" || employeeAssignment.IsActive == "1")
            //{
            //    where += $" ea.IsActive={employeeAssignment.IsActive} and ";
            //}
            //else
            //{
            //    where += $" ea.IsActive=1 and ";
            //}

            where += " 1=1 and ea.IsDeleted is null Or  ea.IsDeleted=0 ";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,IsDeletePending
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {where}
                            order by emp.Id asc";


            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();
            //HttpContext.Current.Response.Write("query: " + query + "<br>");
            //HttpContext.Current.Response.End();

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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            //forecastEmployeeAssignmentViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            //employeeAssignmentViewModel.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
                            //forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(forecastEmployeeAssignmentViewModel.UnitPrice).ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"));
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.SubCode = Convert.ToInt32(rdr["SubCode"]);
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);

                            if(forecastEmployeeAssignmentViewModel.Id== 224)
                            {
                                var tepp = 1;
                            }
                            forecastEmployeeAssignmentViewModel.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            //check for if duplicate pending cells is there!
                            string bcyrCellWithoutDuplicateData = "";
                            if (!string.IsNullOrEmpty(forecastEmployeeAssignmentViewModel.BCYRCell))
                            {
                                var arrBCYRCells = forecastEmployeeAssignmentViewModel.BCYRCell.Split(',');
                                foreach (var bCYRCellsCellitem in arrBCYRCells)
                                {
                                    if (string.IsNullOrEmpty(bcyrCellWithoutDuplicateData))
                                    {
                                        bcyrCellWithoutDuplicateData = bCYRCellsCellitem;
                                    }
                                    else
                                    {
                                        bool isDuplicatedCells = false;
                                        var arrPendingCellWithoutDuplicateCells = bcyrCellWithoutDuplicateData.Split(',');
                                        foreach (var duplicateBCYRCellItem in arrPendingCellWithoutDuplicateCells)
                                        {
                                            if (duplicateBCYRCellItem == bCYRCellsCellitem)
                                            {
                                                isDuplicatedCells = true;
                                            }
                                        }
                                        if (!isDuplicatedCells)
                                        {
                                            bcyrCellWithoutDuplicateData = bcyrCellWithoutDuplicateData + "," + bCYRCellsCellitem;
                                        }
                                    }
                                }
                                forecastEmployeeAssignmentViewModel.BCYRCell = bcyrCellWithoutDuplicateData;
                            }


                            forecastEmployeeAssignmentViewModel.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            forecastEmployeeAssignmentViewModel.BCYRApproved = rdr["BCYRApproved"] is DBNull ? false : Convert.ToBoolean(rdr["BCYRApproved"]);
                            forecastEmployeeAssignmentViewModel.IsApproved = rdr["IsApproved"] is DBNull ? false : Convert.ToBoolean(rdr["IsApproved"]);

                            forecastEmployeeAssignmentViewModel.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            //check for if duplicate pending cells is there!
                            string pendingCellWithoutDuplicateCells = "";
                            if (!string.IsNullOrEmpty(forecastEmployeeAssignmentViewModel.BCYRCellPending))
                            {
                                var arrPendingCells = forecastEmployeeAssignmentViewModel.BCYRCellPending.Split(',');
                                foreach (var pendingCellitem in arrPendingCells)
                                {
                                    if (string.IsNullOrEmpty(pendingCellWithoutDuplicateCells))
                                    {
                                        pendingCellWithoutDuplicateCells = pendingCellitem;
                                    }
                                    else
                                    {
                                        bool isDuplicatedCells = false;
                                        var arrPendingCellWithoutDuplicateCells = pendingCellWithoutDuplicateCells.Split(',');
                                        foreach (var duplicateItem in arrPendingCellWithoutDuplicateCells)
                                        {
                                            if (duplicateItem == pendingCellitem)
                                            {
                                                isDuplicatedCells = true;
                                            }
                                        }
                                        if (!isDuplicatedCells)
                                        {
                                            pendingCellWithoutDuplicateCells = pendingCellWithoutDuplicateCells + "," + pendingCellitem;
                                        }
                                    }
                                }
                                forecastEmployeeAssignmentViewModel.BCYRCellPending = pendingCellWithoutDuplicateCells;
                            }

                            forecastEmployeeAssignmentViewModel.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            forecastEmployeeAssignmentViewModel.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);

                            
                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }

        public List<ForecastAssignmentViewModel> GetAllBudgetData(EmployeeBudgetAssignment employeeAssignment)
        {

            string where = "";
           
            if (!string.IsNullOrEmpty(employeeAssignment.SectionId))
            {
                string tempSectionIds = "";
                if (employeeAssignment.SectionId.IndexOf(",") >= 0)
                {
                    string[] arrSectionIds = employeeAssignment.SectionId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrSectionIds.Length; i++)
                    {
                        if (tempSectionIds == "")
                        {
                            tempSectionIds = arrSectionIds[i];
                        }
                        else
                        {
                            tempSectionIds = tempSectionIds + "," + arrSectionIds[i];
                        }
                    }
                }
                else
                {
                    tempSectionIds = employeeAssignment.SectionId;
                }
                where += $" ea.SectionId In ({tempSectionIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.DepartmentId))
            {
                string tempDepartmentIds = "";
                if (employeeAssignment.DepartmentId.IndexOf(",") >= 0)
                {
                    string[] arrDepartmentIds = employeeAssignment.DepartmentId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrDepartmentIds.Length; i++)
                    {
                        if (tempDepartmentIds == "")
                        {
                            tempDepartmentIds = arrDepartmentIds[i];
                        }
                        else
                        {
                            tempDepartmentIds = tempDepartmentIds + "," + arrDepartmentIds[i];
                        }
                    }
                }
                else
                {
                    tempDepartmentIds = employeeAssignment.DepartmentId;
                }
                where += $" ea.DepartmentId In ({tempDepartmentIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.InchargeId))
            {
                string tempInchargeIdIds = "";
                if (employeeAssignment.InchargeId.IndexOf(",") >= 0)
                {
                    string[] arrInchargeIds = employeeAssignment.InchargeId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrInchargeIds.Length; i++)
                    {
                        if (tempInchargeIdIds == "")
                        {
                            tempInchargeIdIds = arrInchargeIds[i];
                        }
                        else
                        {
                            tempInchargeIdIds = tempInchargeIdIds + "," + arrInchargeIds[i];
                        }
                    }
                }
                else
                {
                    tempInchargeIdIds = employeeAssignment.InchargeId;
                }
                where += $" ea.InChargeId In ({tempInchargeIdIds}) and ";
            }

            if (!string.IsNullOrEmpty(employeeAssignment.RoleId))
            {
                string tempRoleIds = "";
                if (employeeAssignment.RoleId.IndexOf(",") >= 0)
                {
                    string[] arrRoleIdss = employeeAssignment.RoleId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrRoleIdss.Length; i++)
                    {
                        if (tempRoleIds == "")
                        {
                            tempRoleIds = arrRoleIdss[i];
                        }
                        else
                        {
                            tempRoleIds = tempRoleIds + "," + arrRoleIdss[i];
                        }
                    }
                }
                else
                {
                    tempRoleIds = employeeAssignment.RoleId;
                }
                where += $" ea.RoleId In ({tempRoleIds}) and ";
            }

            if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
            {
                string tempExplanationIds = "";
                if (employeeAssignment.ExplanationId.IndexOf(",") >= 0)
                {
                    string[] arrtempExplanationIds = employeeAssignment.ExplanationId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrtempExplanationIds.Length; i++)
                    {
                        if (tempExplanationIds == "")
                        {
                            tempExplanationIds = arrtempExplanationIds[i];
                        }
                        else
                        {
                            tempExplanationIds = tempExplanationIds + "," + arrtempExplanationIds[i];
                        }
                    }
                }
                else
                {
                    tempExplanationIds = employeeAssignment.ExplanationId;
                }
                where += $" ea.ExplanationId IN ({tempExplanationIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.CompanyId))
            {
                string tempCompanyIds = "";
                if (employeeAssignment.CompanyId.IndexOf(",") >= 0)
                {
                    string[] arrCompanyIds = employeeAssignment.CompanyId.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrCompanyIds.Length; i++)
                    {
                        if (tempCompanyIds == "")
                        {
                            tempCompanyIds = arrCompanyIds[i];
                        }
                        else
                        {
                            tempCompanyIds = tempCompanyIds + "," + arrCompanyIds[i];
                        }
                    }
                }
                else
                {
                    tempCompanyIds = employeeAssignment.CompanyId;
                }
                where += $" ea.CompanyId In ({tempCompanyIds}) and ";
            }
            if (!string.IsNullOrEmpty(employeeAssignment.Year))
            {
                where += $" ea.Year={employeeAssignment.Year} and ";
            }
            if (employeeAssignment.FirstHalfBudget)
            {
                where += $" ea.FirstHalfBudget=1 and ";
            }
            if (employeeAssignment.SecondHalfBudget)
            {
                where += $" ea.SecondHalfBudget=1 and ";
            }      

            where += " 1=1 ";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks,ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.IsActive
                            from EmployeeeBudgets ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {where}
                            order by emp.Id asc";


            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();
           
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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();                           

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }


        public int CreateApprovedAssignmentByTimestampId(EmployeeAssignment employeeAssignment,int approvedTimestampId)
        {
            int result = 0;
            string query = $@"
                                INSERT INTO ApprovedEmployeesAssignments
	                                (
		                                ApprovedTimeStampId,AssignmentId,EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId
		                                ,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,SubCode,Year,EmployeeName,IsDeleted,BCYRCellPending
	                                ) 
                                values
	                                (
		                                @approvedTimeStampId,@assignmentId,@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId
		                                ,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@subCode,@year,@employeeName,@isDeleted,@bCYRCellPending
	                                )
                            ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@approvedTimeStampId", approvedTimestampId);
                cmd.Parameters.AddWithValue("@AssignmentId", employeeAssignment.Id);                
                if (employeeAssignment.EmployeeId == null)
                {
                    cmd.Parameters.AddWithValue("@employeeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@employeeId", employeeAssignment.EmployeeId);
                }
                if (employeeAssignment.SectionId == null)
                {
                    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sectionId", employeeAssignment.SectionId);
                }
                if (employeeAssignment.DepartmentId == null)
                {
                    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@departmentId", employeeAssignment.DepartmentId);
                }
                if (employeeAssignment.InchargeId == null)
                {
                    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@inChargeId", employeeAssignment.InchargeId);
                }               
                if (employeeAssignment.RoleId == null)
                {
                    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleId", employeeAssignment.RoleId);
                }                
                if (String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@explanationId", employeeAssignment.ExplanationId);
                }                
                if (employeeAssignment.CompanyId == null)
                {
                    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@companyId", employeeAssignment.CompanyId);
                }
                cmd.Parameters.AddWithValue("@unitPrice", employeeAssignment.UnitPrice);                
                if (employeeAssignment.GradeId == null)
                {
                    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@gradeId", employeeAssignment.GradeId);
                }
                if (employeeAssignment.CreatedBy == null)
                {
                    cmd.Parameters.AddWithValue("@createdBy", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@createdBy", employeeAssignment.CreatedBy);
                }

                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                if (employeeAssignment.IsActive == null)
                {
                    cmd.Parameters.AddWithValue("@isActive", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isActive", employeeAssignment.IsActive);
                }
                if (employeeAssignment.Remarks == null)
                {
                    cmd.Parameters.AddWithValue("@remarks", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@remarks", employeeAssignment.Remarks);
                }
                if (employeeAssignment.BCYRCellPending == null)
                {
                    cmd.Parameters.AddWithValue("@bCYRCellPending", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bCYRCellPending", employeeAssignment.BCYRCellPending);
                }

                cmd.Parameters.AddWithValue("@subCode", employeeAssignment.SubCode);
                cmd.Parameters.AddWithValue("@year", employeeAssignment.Year);

                if (!string.IsNullOrEmpty(employeeAssignment.EmployeeModifiedName))
                {
                    cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeModifiedName);
                }
                else
                {
                    if (employeeAssignment.EmployeeRootName == null)
                    {
                        cmd.Parameters.AddWithValue("@employeeName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeRootName);
                    }                        
                }
                
                cmd.Parameters.AddWithValue("@isDeleted", employeeAssignment.IsDeleted);

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
        public int GetApprovedAssignmentLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from ApprovedEmployeesAssignments;";
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
        //Check if the original data is already exists!
        public int CheckForOriginalAssignmentIsExists(int assignmentId)
        {
            int result = 0;
            string query = $@"SELECT * FROM EmployeesAssignmentsOrg WHERE EmployeesAssignmentId = {assignmentId}";
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
        //update original assignment data
        public int UpdateOriginalAssignment(AssignmentHistory _assignmentHistory, string columnValue, string columnName)
        {
            int result = 0;
            //string query = $@"update EmployeesAssignmentsOrg set EmployeeId=@employeeId,  SectionId=@sectionId,DepartmentId=@departmentId,InChargeId=@inChargeId,RoleId=@roleId,ExplanationId=@explanationId,CompanyId=@companyId,UnitPrice=@unitPrice,GradeId=@gradeId,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate, Remarks=@remarks where EmployeesAssignmentId=@id";
            string query = "Update EmployeesAssignmentsOrg Set "+ columnName+"="+ columnValue+ " Where EmployeesAssignmentId="+_assignmentHistory.EmployeeAssignmentId;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@employeeId", _assignmentHistory.EmployeeId);
                //if (_assignmentHistory.SectionId == null)
                //{
                //    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@sectionId", _assignmentHistory.SectionId);
                //}
                //if (_assignmentHistory.DepartmentId == null)
                //{
                //    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@departmentId", _assignmentHistory.DepartmentId);
                //}
                //if (_assignmentHistory.InChargeId == null)
                //{
                //    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@inChargeId", _assignmentHistory.InChargeId);
                //}

                //if (_assignmentHistory.RoleId == null)
                //{
                //    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@roleId", _assignmentHistory.RoleId);
                //}

                //if (String.IsNullOrEmpty(_assignmentHistory.ExplanationId))
                //{
                //    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@explanationId", _assignmentHistory.ExplanationId);
                //}

                //if (_assignmentHistory.CompanyId == null)
                //{
                //    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@companyId", _assignmentHistory.CompanyId);
                //}

                //if (_assignmentHistory.GradeId == null)
                //{
                //    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@gradeId", _assignmentHistory.GradeId);
                //}
                //cmd.Parameters.AddWithValue("@unitPrice", _assignmentHistory.UnitPrice);
                //cmd.Parameters.AddWithValue("@updatedBy", _assignmentHistory.UpdatedBy);
                //cmd.Parameters.AddWithValue("@updatedDate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@id", _assignmentHistory.Id);
                //cmd.Parameters.AddWithValue("@remarks", _assignmentHistory.Remarks);

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
        
        //insert original data
        public int InsertOriginalAssignment(AssignmentHistory _assignmentHistory, string columnValue, string columnName)
        {
            int result = 0;
            //string query = $@"insert into EmployeesAssignmentsOrg(EmployeeId,EmployeesAssignmentId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,Remarks,Year) values(@employeeId,@employeesAssignmentId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@remarks,@year);";            
            string query = "Insert Into EmployeesAssignmentsOrg (EmployeesAssignmentId," + columnName + ",Year) Values(@employeesAssignmentId," + columnValue + ", @year) ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@employeeId", _assignmentHistory.EmployeeId);
                //if (_assignmentHistory.SectionId == null)
                //{
                //    cmd.Parameters.AddWithValue("@sectionId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@sectionId", _assignmentHistory.SectionId);
                //}                
                //if (_assignmentHistory.DepartmentId == null)
                //{
                //    cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@departmentId", _assignmentHistory.DepartmentId);
                //}
                //if (_assignmentHistory.InChargeId == null)
                //{
                //    cmd.Parameters.AddWithValue("@inChargeId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@inChargeId", _assignmentHistory.InChargeId);
                //}

                //if (_assignmentHistory.RoleId == null)
                //{
                //    cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@roleId", _assignmentHistory.RoleId);
                //}

                //if (String.IsNullOrEmpty(_assignmentHistory.ExplanationId))
                //{
                //    cmd.Parameters.AddWithValue("@explanationId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@explanationId", _assignmentHistory.ExplanationId);
                //}                
                //if (_assignmentHistory.CompanyId == null)
                //{
                //    cmd.Parameters.AddWithValue("@companyId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@companyId", _assignmentHistory.CompanyId);
                //}

                //if (_assignmentHistory.GradeId == null)
                //{
                //    cmd.Parameters.AddWithValue("@gradeId", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@gradeId", _assignmentHistory.GradeId);
                //}
                //cmd.Parameters.AddWithValue("@unitPrice", _assignmentHistory.UnitPrice);
                //cmd.Parameters.AddWithValue("@createdBy", _assignmentHistory.CreatedBy);
                //cmd.Parameters.AddWithValue("@createdDate", _assignmentHistory.CreatedDate);
                cmd.Parameters.AddWithValue("@employeesAssignmentId", _assignmentHistory.Id);
                //cmd.Parameters.AddWithValue("@remarks", _assignmentHistory.Remarks);
                cmd.Parameters.AddWithValue("@year", _assignmentHistory.Year);

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

        //Check if the original data is already exists!
        public int CheckForOriginalForecastDataIsExists(int assignmentId)
        {
            int result = 0;
            string query = $@"SELECT * FROM CostsOrg WHERE EmployeeAssignmentsId = {assignmentId}";
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
        //Check if the original data is already exists!
        public int CheckMonthIdExistsForOrgForecast(int assignmentId,int monthId)
        {
            int result = 0;
            string query = $@"SELECT * FROM CostsOrg WHERE EmployeeAssignmentsId = {assignmentId} AND MonthId={monthId}";
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

        public int RemoveAssignmentDataFromOrgTable(int assignmentId,string dbColumnName)
        {
            int result = 0;            
            string query = "update EmployeesAssignmentsOrg set "+ dbColumnName + "='' where EmployeesAssignmentId="+ assignmentId;
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
        public int RemoveForecastedDataFromOrgTable(int assignmentId, int dbColumnName)
        {
            int result = 0;            
            string query = "DELETE FROM CostsOrg WHERE EmployeeAssignmentsId="+ assignmentId + " and MonthId="+ dbColumnName;
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

        public string GetOriginalDataForPendingCells(int assignmentId,string dbColumnNameWithDbSchema,string dbColumnName)
        {

            string query = $@"select {dbColumnNameWithDbSchema}
                                from EmployeesAssignmentsOrg ea 
	                            left join Sections sec on ea.SectionId = sec.Id
                                left join Departments dep on ea.DepartmentId = dep.Id
                                left join Companies com on ea.CompanyId = com.Id
                                left join Roles rl on ea.RoleId = rl.Id
                                left join InCharges inc on ea.InChargeId = inc.Id 
                                left join Grades gd on ea.GradeId = gd.Id    
                                left join Explanations e on ea.ExplanationId = e.Id
                            where ea.EmployeesAssignmentId={assignmentId}";

            string result = "";

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

                            result = rdr[""+ dbColumnName + ""] is DBNull ? "" : rdr["" + dbColumnName + ""].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
        public decimal GetMonthWiseOriginalForecastData(int assignmentId, string dbColumnName)
        {

            string query = $@"select Points from CostsOrg ea where ea.EmployeeAssignmentsId={assignmentId} and MonthId={dbColumnName}";

            decimal result = 0;

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

                            result = rdr["Points"] is DBNull ? 0 : Convert.ToDecimal(rdr["Points"]);

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
        public bool CheckForBudgetYearIsExists(int selected_year,int select_budget_type)
        {
            bool isValid = false;
            string strWhere = "";
            if (select_budget_type == 1)
            {
                strWhere = "WHERE FirstHalfBudget=" + 1;
            }
            if (select_budget_type == 2)
            {
                if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "WHERE SecondHalfBudget=" + 1;
                }
                else
                {
                    strWhere = strWhere+" AND SecondHalfBudget=" + 1;
                }                
            }
            if (string.IsNullOrEmpty(strWhere))
            {

            }
            else
            {
                strWhere = strWhere + " AND Year=" + selected_year;
            }
            string query = "select * from EmployeeeBudgets " + strWhere;

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {

                }                
            }
            return isValid;
        }
        public int FinalizeBudgetAssignment(int selected_year, int select_budget_type)
        {
            int result = 0;
            string strWhere = "";
            if (select_budget_type == 1)
            {
                strWhere = "WHERE FirstHalfBudget=" + 1;
            }else if(select_budget_type == 2)
            {
                strWhere = "WHERE SecondHalfBudget=" + 1;
            }
            
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = strWhere + " AND Year=" + selected_year;
            }
            
            string query = "UPDATE EmployeeeBudgets SET FinalizedBudget=1 " + strWhere;

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
        public List<EmployeeBudget> GetFinalizedBudgetData(int selected_year, int select_budget_type)
        {
            string strWhere = "";
            if (select_budget_type == 1)
            {
                strWhere = "WHERE FirstHalfBudget=" + 1;
            }
            else if (select_budget_type == 2)
            {
                strWhere = "WHERE SecondHalfBudget=" + 1;
            }

            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = strWhere + " AND Year=" + selected_year;
            }
            string query = "SELECT * FROM EmployeeeBudgets " + strWhere;            

            List<EmployeeBudget> _employeeAssignments = new List<EmployeeBudget>();

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
                            EmployeeBudget _employeeBudget = new EmployeeBudget();
                            _employeeBudget.Id = rdr["Id"] is DBNull ? 0 : Convert.ToInt32(rdr["Id"]);
                            _employeeBudget.EmployeeId = rdr["EmployeeId"] is DBNull ? "" : rdr["EmployeeId"].ToString();
                            _employeeBudget.SectionId = rdr["SectionId"] is DBNull ? 0 : Convert.ToInt32(rdr["SectionId"]);
                            _employeeBudget.InchargeId = rdr["InChargeId"] is DBNull ? 0 : Convert.ToInt32(rdr["InChargeId"]);
                            _employeeBudget.DepartmentId = rdr["DepartmentId"] is DBNull ? 0 : Convert.ToInt32(rdr["DepartmentId"]);
                            _employeeBudget.RoleId = rdr["RoleId"] is DBNull ? 0 : Convert.ToInt32(rdr["RoleId"]);
                            _employeeBudget.CompanyId = rdr["CompanyId"] is DBNull ? 0 : Convert.ToInt32(rdr["CompanyId"]);                                                        
                            _employeeBudget.ExplanationId = String.IsNullOrEmpty(rdr["ExplanationId"].ToString()) ? null : rdr["ExplanationId"].ToString();
                            _employeeBudget.UnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                            _employeeBudget.GradeId = rdr["GradeId"] is DBNull ? 0 : Convert.ToInt32(rdr["GradeId"]);
                            _employeeBudget.SubCode = 0;                            
                            _employeeBudget.BCYR = false;                            
                            _employeeBudget.BCYRCell = "";

                            _employeeBudget.CreatedBy = rdr["CreatedBy"] is DBNull ? "" : rdr["CreatedBy"].ToString();                            
                            _employeeBudget.CreatedDate = DateTime.Now;
                            _employeeBudget.IsActive = "1";                            
                            _employeeBudget.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();                            
                            _employeeBudget.Year = rdr["Year"] is DBNull ? "" : rdr["Year"].ToString();
                            _employeeBudget.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();

                            _employeeAssignments.Add(_employeeBudget);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return _employeeAssignments;
        }
        public bool CheckYearIfFinalize(int year,int reqType)
        {
            bool results = false;
            string strBudetTypeTxt = "";
            if (reqType == 1)
            {
                strBudetTypeTxt = "where Year="+ year + " And FirstHalfBudget=1 And FinalizedBudget=1 ";
            }
            else
            {
                strBudetTypeTxt = "where Year="+ year + " And SecondHalfBudget=1 And FinalizedBudget=1 ";
            }
            string query = "select * from EmployeeeBudgets " + strBudetTypeTxt;

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
            }
            return results;
        }
        public void DeleteAssignment_PreviousFinalizeData(int year)
        {
            string queryForAssignment = $@"DELETE FROM EmployeesAssignments WHERE Year=@year";
            string queryForCost = $@"DELETE FROM Costs WHERE Year=@year";            

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmdForAssignment = new SqlCommand(queryForAssignment, sqlConnection);
                SqlCommand cmdForCost = new SqlCommand(queryForCost, sqlConnection);

                cmdForAssignment.Parameters.AddWithValue("@year", year);
                cmdForCost.Parameters.AddWithValue("@year", year);
                try
                {
                    cmdForAssignment.ExecuteNonQuery();
                    cmdForCost.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

            }
        }
        public bool CheckIsValidYearForImport(int year)
        {
            bool results = false;

            string query = "SELECT * FROM EmployeeeBudgets WHERE Year="+ year + " AND SecondHalfBudget=1";

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
            }
            return results;
        }
    }
}
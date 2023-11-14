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
            string query = $@"insert into EmployeesAssignments(EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,SubCode,Year,BCYR,BCYRCell,EmployeeName,DuplicateFrom,DuplicateCount,RoleChanged,UnitPriceChanged) values(@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@subCode,@year,@bCYR,@bCYRCell,@employeeName,@duplicateFrom,@duplicateCount,@roleChanged,@unitPriceChanged);";
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

                if (employeeAssignment.DuplicateFrom == null)
                {
                    cmd.Parameters.AddWithValue("@duplicateFrom", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@duplicateFrom", employeeAssignment.DuplicateFrom);
                }
                if (employeeAssignment.DuplicateCount == null)
                {
                    cmd.Parameters.AddWithValue("@duplicateCount", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@duplicateCount", employeeAssignment.DuplicateCount);
                }
                if (employeeAssignment.RoleChanged == null)
                {
                    cmd.Parameters.AddWithValue("@roleChanged", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleChanged", employeeAssignment.RoleChanged);
                }
                if (employeeAssignment.UnitPriceChanged == null)
                {
                    cmd.Parameters.AddWithValue("@unitPriceChanged", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@unitPriceChanged", employeeAssignment.UnitPriceChanged);
                }

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
        public int CreateFinalBudgetAssignment(EmployeeAssignment employeeAssignment)
        {
            int result = 0;
            string query = $@"insert into EmployeeeFinalBudgets(EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,Year,EmployeeName,DuplicateFrom,DuplicateCount,RoleChanged,UnitPriceChanged) values(@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@year,@employeeName,@duplicateFrom,@duplicateCount,@roleChanged,@unitPriceChanged);";
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
                cmd.Parameters.AddWithValue("@year", employeeAssignment.Year);
                cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeName);

                cmd.Parameters.AddWithValue("@duplicateFrom", employeeAssignment.DuplicateFrom);
                cmd.Parameters.AddWithValue("@duplicateCount", employeeAssignment.DuplicateCount);
                cmd.Parameters.AddWithValue("@roleChanged", employeeAssignment.RoleChanged);
                cmd.Parameters.AddWithValue("@unitPriceChanged", employeeAssignment.UnitPriceChanged);

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
            string query = $@"insert into EmployeeeBudgets(EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,Year,EmployeeName,FirstHalfBudget,SecondHalfBudget,FinalizedBudget,DuplicateFrom,DuplicateCount,RoleChanged,UnitPriceChanged) values(@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@year,@employeeName,@firstHalfBudget,@secondHalfBudget,@finalizedBudget,@duplicateFrom,@duplicateCount,@roleChanged,@unitPriceChanged);";
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
                //cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeModifiedName);
                cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeName);
                cmd.Parameters.AddWithValue("@year", employeeAssignment.Year);

                cmd.Parameters.AddWithValue("@firstHalfBudget", employeeAssignment.FirstHalfBudget);
                cmd.Parameters.AddWithValue("@secondHalfBudget", employeeAssignment.SecondHalfBudget);
                cmd.Parameters.AddWithValue("@finalizedBudget", employeeAssignment.FinalizedBudget);

                cmd.Parameters.AddWithValue("@duplicateFrom", employeeAssignment.DuplicateFrom);
                cmd.Parameters.AddWithValue("@duplicateCount", employeeAssignment.DuplicateCount);
                cmd.Parameters.AddWithValue("@roleChanged", employeeAssignment.RoleChanged);
                cmd.Parameters.AddWithValue("@unitPriceChanged", employeeAssignment.UnitPriceChanged);

            
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
        public int GetFinalBudgetLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from EmployeeeFinalBudgets;";
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
        public int GetAssignmentTimeStampsLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from EmployeesAssignmentsWithTimeStamps;";
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

        public List<ForecastAssignmentViewModel> GetEmployeesBudgetByDepartments_Company(int departmentId, string companyIds, int year)
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
            where += $" ea.Year={year}";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.ExplanationId,
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

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(string departmentIds, string companyIds, int year)
        {
            string where = "";
            where += $" ea.DepartmentId in({departmentIds}) and ";

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

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company_Timestamps(int departmentId, string companyIds, int year,string timestampsId)
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
            where += $" ea.TimeStampsId = ({timestampsId}) and ";
            where += $" ea.Year={year} and ";

            where += " 1=1 and ea.IsDeleted is null Or  ea.IsDeleted=0 ";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,IsDeletePending,ea.EmployeeAssignmentId
                            from EmployeesAssignmentsWithTimeStamps ea left join Sections sec on ea.SectionId = sec.Id
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
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["EmployeeAssignmentId"]);
                            forecastEmployeeAssignmentViewModel.AssignmentTimeStampId = Convert.ToInt32(rdr["AssignmentId"]);
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

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByIncharge_Company(int inchargeId, string companyIds, int year)
        {

            string where = "";
            where += $" ea.InChargeId = {inchargeId} and ";

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

        public List<ForecastDto> GetBudgetByAssignmentId(int assignmentId, string year)
        {
            List<ForecastDto> budgets = new List<ForecastDto>();
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
                            ForecastDto budget = new ForecastDto();
                            budget.ForecastId = Convert.ToInt32(rdr["Id"]);
                            budget.Year = Convert.ToInt32(rdr["Year"]);
                            budget.Month = Convert.ToInt32(rdr["MonthId"]);
                            budget.Points = Convert.ToDecimal(rdr["Points"]);

                            budgets.Add(budget);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return budgets;
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

        public List<ForecastDto> GetForecastsByTimestampAssignmentId(int assignmentId, string year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from Costs_WithAssignmentsTimeStamps where TimeStampsAssignmentId=" + assignmentId + " and Year=" + year;
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
        public List<Forecast> GetApprovedForecastdDataForReplicateBudget(int assignmentId, string year)
        {
            List<Forecast> forecasts = new List<Forecast>();
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
                            Forecast forecast = new Forecast();
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            if (rdr["ApprovedEmployeeAssignmentsId"] == DBNull.Value)
                            {
                                forecast.EmployeeAssignmentId = 0;
                            }
                            else
                            {
                                forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["ApprovedEmployeeAssignmentsId"]);
                            }
                            if (rdr["Year"] == DBNull.Value)
                            {
                                forecast.Year = 0;
                            }
                            else
                            {
                                forecast.Year = Convert.ToInt32(rdr["Year"]);
                            }
                            if (rdr["MonthId"] == DBNull.Value)
                            {
                                forecast.Month = 0;
                            }
                            else
                            {
                                forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            }
                            if (rdr["Points"] == DBNull.Value)
                            {
                                forecast.Points = 0;
                            }
                            else
                            {
                                forecast.Points = Convert.ToInt32(rdr["Points"]);
                            }
                            if (rdr["Total"] == DBNull.Value)
                            {
                                forecast.Total = 0;
                            }
                            else
                            {
                                forecast.Total = Convert.ToInt32(rdr["Total"]);
                            }
                            if (rdr["CreatedBy"] == DBNull.Value)
                            {
                                forecast.CreatedBy = null;
                            }
                            else
                            {
                                forecast.CreatedBy = rdr["CreatedBy"].ToString();
                            }

                            if (rdr["CreatedDate"] == DBNull.Value)
                            {
                                forecast.CreatedDate = DateTime.Now;
                            }
                            else
                            {
                                forecast.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            }
                            if (rdr["UpdatedDate"] == DBNull.Value)
                            {
                                forecast.UpdatedDate = DateTime.Now; ;
                            }
                            else
                            {
                                forecast.UpdatedDate = Convert.ToDateTime(rdr["UpdatedDate"]);
                            }

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
                            ,gd.GradePoints,ea.IsActive,ea.EmployeeName 'DuplicateName' 
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
                            employeeAssignmentViewModel.EmployeeName = rdr["DuplicateName"] is DBNull ? "" : rdr["DuplicateName"].ToString();

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

            string query = $@"select ea.id as AssignmentId,ep.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId, ea.EmployeeId, 
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive, cst.Points, cst.Total,ea.EmployeeName 'DuplicateName', ea.DuplicateFrom, ea.DuplicateCount, ea.RoleChanged, ea.UnitPriceChanged 
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
                            employeeAssignmentViewModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            employeeAssignmentViewModel.EmployeeName = rdr["DuplicateName"].ToString();
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

                            employeeAssignmentViewModel.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "" : rdr["DuplicateFrom"].ToString();
                            employeeAssignmentViewModel.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "" : rdr["DuplicateCount"].ToString();
                            employeeAssignmentViewModel.RoleChanged = rdr["RoleChanged"] is DBNull ? "" : rdr["RoleChanged"].ToString();
                            employeeAssignmentViewModel.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "" : rdr["UnitPriceChanged"].ToString();

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
            string results = "";

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

                            results = rdr["BCYRCell"] is DBNull ? "": rdr["BCYRCell"].ToString();                            

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return results;
        }
        public EmployeeAssignment GetBCYRCellAndPendingCellsByAssignmentId(int assignmentId)
        {
            string query = $@"select BCYRCell,BCYRCellPending from EmployeesAssignments where Id={assignmentId}";
            EmployeeAssignment _employeeAssignment = new EmployeeAssignment();

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

                            _employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            _employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return _employeeAssignment;
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

        public int UpdateBCYRCellBCYRPendingCellByAssignmentId(int assignmentId, string cell,string pendingCells)
        {
            string query = $@"update EmployeesAssignments set BCYRCell ='{cell}',BCYRCellPending ='{pendingCells}' where Id={assignmentId}";
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

            string query = $@"select ea.Id,ea.AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,ea.IsDeletePending,ea.IsAddEmployee,ea.IsDeleteEmployee,ea.IsCellWiseUpdate,ea.ApprovedCells,emp.FullName 'RootEmployeeName'
                            ,ea.IsDeleted,ea.AssignmentId 'EmployeeAssignmentIdOrg',ema.IsRowPending 'PendingRowAfterApprove', ea.DuplicateFrom, ea.DuplicateCount, ea.RoleChanged,ea.UnitPriceChanged 
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
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["Id"]);
                            forecastEmployeeAssignmentViewModel.AssignmentId = Convert.ToInt32(rdr["AssignmentId"]);
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
                            forecastEmployeeAssignmentViewModel.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "" : rdr["DuplicateFrom"].ToString();
                            forecastEmployeeAssignmentViewModel.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "" : rdr["DuplicateCount"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleChanged = rdr["RoleChanged"] is DBNull ? "" : rdr["RoleChanged"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "" : rdr["UnitPriceChanged"].ToString();

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

        public List<ExcelAssignmentDto> GetAllOriginalDataForReplciateBudget(string year, int approvedTimestampid)
        {
            string where = "";

            if (!string.IsNullOrEmpty(year))
            {
                where += $" ea.Year={year} and ";
            }
            if (!string.IsNullOrEmpty(approvedTimestampid.ToString()))
            {
                where += $" ea.ApprovedTimeStampId={approvedTimestampid} and ";
            }
            where += " 1=1 and (ema.IsRowPending is null or ema.IsRowPending =0)";

            string query = $@"select ea.Id,ea.AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.BCYR,ea.BCYRCell,ea.IsActive,ea.BCYRApproved,ea.BCYRCellApproved,ea.IsApproved,ea.BCYRCellPending
                            ,ea.IsRowPending,ea.IsDeletePending,ea.IsAddEmployee,ea.IsDeleteEmployee,ea.IsCellWiseUpdate,ea.ApprovedCells,emp.FullName 'RootEmployeeName'
                            ,ea.IsDeleted,ea.AssignmentId 'EmployeeAssignmentIdOrg',ema.IsRowPending 'PendingRowAfterApprove'
                            , ema.DuplicateFrom, ema.DuplicateCount, ema.RoleChanged,ema.UnitPriceChanged 
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


            List<ExcelAssignmentDto> excelAssignmentDtos = new List<ExcelAssignmentDto>();

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
                            ExcelAssignmentDto excelAssignmentDto = new ExcelAssignmentDto();
                            excelAssignmentDto.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            if (rdr["EmployeeId"] == DBNull.Value)
                            {
                                excelAssignmentDto.EmployeeId = null;
                            }
                            else
                            {
                                excelAssignmentDto.EmployeeId = rdr["EmployeeId"].ToString();
                            }
                            if (rdr["SectionId"] == DBNull.Value)
                            {
                                excelAssignmentDto.SectionId = null;
                            }
                            else
                            {
                                excelAssignmentDto.SectionId = Convert.ToInt32(rdr["SectionId"]);
                            }
                            if (rdr["DepartmentId"] == DBNull.Value)
                            {
                                excelAssignmentDto.DepartmentId = null;
                            }
                            else
                            {
                                excelAssignmentDto.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                            }
                            if (rdr["InchargeId"] == DBNull.Value)
                            {
                                excelAssignmentDto.InchargeId = null;
                            }
                            else
                            {
                                excelAssignmentDto.InchargeId = Convert.ToInt32(rdr["InchargeId"]);
                            }
                            if (rdr["RoleId"] == DBNull.Value)
                            {
                                excelAssignmentDto.RoleId = null;
                            }
                            else
                            {
                                excelAssignmentDto.RoleId = Convert.ToInt32(rdr["RoleId"]);
                            }

                            if (rdr["ExplanationId"] == DBNull.Value)
                            {
                                excelAssignmentDto.ExplanationId = null;
                            }
                            else
                            {
                                excelAssignmentDto.ExplanationId = Convert.ToInt32(rdr["ExplanationId"]);
                            }
                            if (rdr["CompanyId"] == DBNull.Value)
                            {
                                excelAssignmentDto.CompanyId = null;
                            }
                            else
                            {
                                excelAssignmentDto.CompanyId = Convert.ToInt32(rdr["CompanyId"]);
                            }
                            excelAssignmentDto.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]);

                            if (rdr["GradeId"] == DBNull.Value)
                            {
                                excelAssignmentDto.GradeId = null;
                            }
                            else
                            {
                                excelAssignmentDto.GradeId = Convert.ToInt32(rdr["GradeId"]);
                            }
                            if (rdr["RootEmployeeName"] == DBNull.Value)
                            {
                                excelAssignmentDto.EmployeeName = null;
                            }
                            else
                            {
                                excelAssignmentDto.EmployeeName = rdr["RootEmployeeName"].ToString();
                            }
                            excelAssignmentDto.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            excelAssignmentDto.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            excelAssignmentDto.EmployeeModifiedName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();

                            excelAssignmentDto.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "" : rdr["DuplicateFrom"].ToString();
                            excelAssignmentDto.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "" : rdr["DuplicateCount"].ToString();
                            excelAssignmentDto.RoleChanged = rdr["RoleChanged"] is DBNull ? "" : rdr["RoleChanged"].ToString();
                            excelAssignmentDto.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "" : rdr["UnitPriceChanged"].ToString();

                            excelAssignmentDtos.Add(excelAssignmentDto);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return excelAssignmentDtos;
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
                            ,ea.IsRowPending,ea.IsDeletePending, ea.DuplicateFrom, ea.DuplicateCount, ea.RoleChanged, ea.UnitPriceChanged
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
                            forecastEmployeeAssignmentViewModel.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "" : rdr["DuplicateFrom"].ToString();
                            forecastEmployeeAssignmentViewModel.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "" : rdr["DuplicateCount"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleChanged = rdr["RoleChanged"] is DBNull ? "" : rdr["RoleChanged"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "" : rdr["UnitPriceChanged"].ToString();

                            //if(forecastEmployeeAssignmentViewModel.Id== 224)
                            //{
                            //    var tepp = 1;
                            //}
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

            where += " ea.IsActive=1 and 1=1 ";

            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks,ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.IsActive, ea.DuplicateFrom, ea.DuplicateCount, ea.RoleChanged, ea.UnitPriceChanged
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
                            forecastEmployeeAssignmentViewModel.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "" : rdr["DuplicateFrom"].ToString();
                            forecastEmployeeAssignmentViewModel.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "" : rdr["DuplicateCount"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleChanged = rdr["RoleChanged"] is DBNull ? "" : rdr["RoleChanged"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "" : rdr["UnitPriceChanged"].ToString();

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
		                                ,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,SubCode,Year,EmployeeName,IsDeleted,BCYRCellPending, DuplicateFrom, DuplicateCount, RoleChanged, UnitPriceChanged
	                                ) 
                                values
	                                (
		                                @approvedTimeStampId,@assignmentId,@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId
		                                ,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@subCode,@year,@employeeName,@isDeleted,@bCYRCellPending, @duplicateFrom, @duplicateCount, @roleChanged, @unitPriceChanged
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

                if (String.IsNullOrEmpty(employeeAssignment.DuplicateFrom))
                {
                    cmd.Parameters.AddWithValue("@duplicateFrom", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@duplicateFrom", employeeAssignment.DuplicateFrom);
                }
                if (String.IsNullOrEmpty(employeeAssignment.DuplicateCount))
                {
                    cmd.Parameters.AddWithValue("@duplicateCount", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@duplicateCount", employeeAssignment.DuplicateCount);
                }
                if (String.IsNullOrEmpty(employeeAssignment.RoleChanged))
                {
                    cmd.Parameters.AddWithValue("@roleChanged", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleChanged", employeeAssignment.RoleChanged);
                }
                if (String.IsNullOrEmpty(employeeAssignment.UnitPriceChanged))
                {
                    cmd.Parameters.AddWithValue("@unitPriceChanged", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@unitPriceChanged", employeeAssignment.UnitPriceChanged);
                }

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

                            _employeeBudget.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "0" : rdr["DuplicateFrom"].ToString();
                            _employeeBudget.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "0" : rdr["DuplicateCount"].ToString();
                            _employeeBudget.RoleChanged = rdr["RoleChanged"] is DBNull ? "0" : rdr["RoleChanged"].ToString();
                            _employeeBudget.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "0" : rdr["UnitPriceChanged"].ToString();

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
        public void DeletePreviousFinalBudgetData(int year)
        {
            string queryForAssignment = $@"DELETE FROM EmployeeeFinalBudgets WHERE Year=@year";
            string queryForCost = $@"DELETE FROM FinalBudgetCosts WHERE Year=@year";

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

        public List<ForecastAssignmentViewModel> GetBudgetDataByYearAndType(int budgetYear, int budgetType)
        {
            string where = "";

            if (budgetType ==1)
            {
                where = " ea.Year=" + budgetYear + " AND ea.FirstHalfBudget=1 ";
            }
            else
            {
                where = " ea.Year=" + budgetYear + " AND ea.SecondHalfBudget=1";
            }

            string query = "";
            query = query + "SELECT ea.Id as AssignmentId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId ";
            query = query + "    , sec.Name as SectionName, ea.Remarks,ea.ExplanationId ";
            query = query + "    ,ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName ";
            query = query + "    ,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice ";
            query = query + "    ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.IsActive,emp.FullName 'RootEmployeeName' ";
            query = query + "FROM EmployeeeBudgets ea ";
            query = query + "    LEFT JOIN Sections sec on ea.SectionId = sec.Id ";
            query = query + "    LEFT JOIN Departments dep on ea.DepartmentId = dep.Id ";
            query = query + "    LEFT JOIN Companies com on ea.CompanyId = com.Id ";
            query = query + "    LEFT JOIN Roles rl on ea.RoleId = rl.Id ";
            query = query + "    LEFT JOIN InCharges inc on ea.InChargeId = inc.Id ";
            query = query + "    LEFT JOIN Grades gd on ea.GradeId = gd.Id ";
            query = query + "    LEFT JOIN Employees emp on ea.EmployeeId = emp.Id ";
            query = query + "WHERE "+ where;
            //query = query + "ORDER BY emp.Id ASC";



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
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString("N0");
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
        public List<ForecastDto> GetBudgetForecastData(int budgetAssignmentId, string year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from BudgetCosts where EmployeeBudgetId=" + budgetAssignmentId + " and Year=" + year;
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
        public List<EmployeeBudget> GetSecondHlafBudgetData(int selected_year, int select_budget_type)
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

                            _employeeBudget.DuplicateFrom = rdr["DuplicateFrom"] is DBNull ? "0" : rdr["DuplicateFrom"].ToString();
                            _employeeBudget.DuplicateCount = rdr["DuplicateCount"] is DBNull ? "0" : rdr["DuplicateCount"].ToString();
                            _employeeBudget.RoleChanged = rdr["RoleChanged"] is DBNull ? "0" : rdr["RoleChanged"].ToString();
                            _employeeBudget.UnitPriceChanged = rdr["UnitPriceChanged"] is DBNull ? "0" : rdr["UnitPriceChanged"].ToString();

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
        public int IsBudgetMatchWithAssignmentData(EmployeeBudget _employeeBudget)
        {
            string strWhere = "";
            strWhere = "where Year = " + _employeeBudget.Year + " and EmployeeId = " + _employeeBudget.EmployeeId + " ";
            if (_employeeBudget.SectionId == null || _employeeBudget.SectionId == 0)
            {
                strWhere = strWhere+ " and (SectionId=0 OR  SectionId is null) ";
            }
            else
            {
                strWhere = strWhere + " and SectionId= " + _employeeBudget.SectionId + " ";
            }
            if (_employeeBudget.DepartmentId == null || _employeeBudget.DepartmentId == 0)
            {
                strWhere = strWhere + " and (DepartmentId= 0 OR  DepartmentId is null) ";
            }
            else
            {
                strWhere = strWhere + " and DepartmentId= " + _employeeBudget.DepartmentId + " ";
            }            
            if (_employeeBudget.InchargeId == null || _employeeBudget.InchargeId == 0)
            {
                strWhere = strWhere + " and (InchargeId= 0 OR  InchargeId is null) ";
            }
            else
            {
                strWhere = strWhere + " and InchargeId= " + _employeeBudget.InchargeId + " ";
            }
            if (_employeeBudget.RoleId == null || _employeeBudget.RoleId == 0)
            {
                strWhere = strWhere + " and (RoleId= 0 OR  RoleId is null) ";
            }
            else
            {
                strWhere = strWhere + " and RoleId= " + _employeeBudget.RoleId + " ";
            }
            if (string.IsNullOrEmpty(_employeeBudget.ExplanationId))
            {
                strWhere = strWhere + " and (ExplanationId= 0 OR  ExplanationId is null) ";
            }
            else
            {
                strWhere = strWhere + " and ExplanationId= " + _employeeBudget.ExplanationId + " ";
            }
            if (_employeeBudget.CompanyId == null || _employeeBudget.CompanyId == 0)
            {
                strWhere = strWhere + " and (CompanyId= 0 OR  CompanyId is null) ";
            }
            else
            {
                strWhere = strWhere + " and CompanyId= " + _employeeBudget.CompanyId + " ";
            }
            if (_employeeBudget.UnitPrice == 0)
            {
                strWhere = strWhere + " and (UnitPrice= 0 OR  UnitPrice is null) ";
            }
            else
            {
                strWhere = strWhere + " and UnitPrice= " + _employeeBudget.UnitPrice + " ";
            }
            if (_employeeBudget.GradeId == null || _employeeBudget.GradeId == 0)
            {
                strWhere = strWhere + " and (GradeId=0 OR  GradeId is null) ";
            }
            else
            {
                strWhere = strWhere + " and GradeId= " + _employeeBudget.GradeId + " ";
            }
            string query = "";
            query = query + "select * ";
            query = query + "from EmployeesAssignments ";
            query = query + strWhere + " and IsActive= 1  ";            

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
                            result = rdr["Id"] is DBNull ? 0 : Convert.ToInt32(rdr["Id"]);
                        }
                        
                    }
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }
        public List<ForecastDto> GettForecastDataForSecondHalfBudgetByAssignmentId(int assignmentId, int year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from Costs where EmployeeAssignmentsId=" + assignmentId + " and Year=" + year;
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
        public EmployeeAssignment GetAssignmentChangedAndPendingCellNo(int assignmentId, int year)
        {
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();
            string query = "SELECT Id,BCYRCell,BCYRCellPending FROM EmployeesAssignments WHERE Year="+ year + " AND Id=" + assignmentId + " ";
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
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignment;
        }
        public decimal GetForecastOriginalPointsForBudget(int assignmentId,int monthId,int year)
        {
            decimal returnPoint = 0;
            string query = "SELECT Points FROM CostsOrg where EmployeeAssignmentsId = "+ assignmentId + " and MonthId = "+ monthId + " AND Year="+year+" ";
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
                            returnPoint = rdr["Points"] is DBNull ? 0 : Convert.ToDecimal(rdr["Points"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return returnPoint;
        }
        public ForecastTotalManMonthCostsViewModal GetTotalCalculationForManmonthAndCost(int year)
        {
            //List<ForecastTotalManMonthCostsViewModal> _forecastTotalManMonthCosts = new List<ForecastTotalManMonthCostsViewModal>();
            string query = "";
            query = query + "select c.MonthId,SUM(c.Points) as 'TotalMM' ";
            query = query + "from costs c ";
            query = query + "where c.year="+ year + " ";
            query = query + "group by c.MonthId ";

            ForecastTotalManMonthCostsViewModal _forecastTotalManMonthCost = new ForecastTotalManMonthCostsViewModal();
            decimal totalManMonth = 0;
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
                            if(Convert.ToInt32(rdr["MonthId"]) == 10)
                            {
                                _forecastTotalManMonthCost.OctTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 11)
                            {
                                _forecastTotalManMonthCost.NovTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 12)
                            {
                                _forecastTotalManMonthCost.DecTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 1)
                            {
                                _forecastTotalManMonthCost.JanTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 2)
                            {
                                _forecastTotalManMonthCost.FebTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 3)
                            {
                                _forecastTotalManMonthCost.MarTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 4)
                            {
                                _forecastTotalManMonthCost.AprTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 5)
                            {
                                _forecastTotalManMonthCost.MayTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 6)
                            {
                                _forecastTotalManMonthCost.JunTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 7)
                            {
                                _forecastTotalManMonthCost.JulTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 8)
                            {
                                _forecastTotalManMonthCost.AugTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 9)
                            {
                                _forecastTotalManMonthCost.SepTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalMM);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            _forecastTotalManMonthCost.TotalManMonth = Convert.ToDecimal(totalManMonth).ToString("0.0");
            ForecastTotalManMonthCostsViewModal _forecastCostsViewModal = new ForecastTotalManMonthCostsViewModal();
            _forecastCostsViewModal = GetTotalCosts(year);

            _forecastTotalManMonthCost.OctTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.OctTotalCosts).ToString("N0");        
            _forecastTotalManMonthCost.NovTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.NovTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.DecTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.DecTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.JanTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.JanTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.FebTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.FebTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.MarTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.MarTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.AprTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.AprTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.MayTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.MayTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.JunTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.JunTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.JulTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.JulTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.AugTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.AugTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.SepTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.SepTotalCosts).ToString("N0");
            //_forecastTotalManMonthCost.TotalCosts = _forecastCostsViewModal.TotalCosts;
            _forecastTotalManMonthCost.TotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalCosts)+ Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalCosts)+Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalCosts)+ Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalCosts)).ToString();
            _forecastTotalManMonthCost.TotalCosts = Convert.ToDecimal(_forecastTotalManMonthCost.TotalCosts).ToString("N0");
            return _forecastTotalManMonthCost;
        }
        public ForecastTotalManMonthCostsViewModal GetTotalCosts(int year)
        {
            //List<ForecastTotalManMonthCostsViewModal> _forecastTotalManMonthCosts = new List<ForecastTotalManMonthCostsViewModal>();
            string query = "";           
            query = query + "select ea.UnitPrice,c.MonthId,SUM(c.Points) as 'TPoints'";
            query = query + "from costs c ";
	        query = query + "    Inner Join EmployeesAssignments ea on c.EmployeeAssignmentsId=ea.Id ";
            query = query + "where c.year= "+ year + " ";
            query = query + "group by ea.UnitPrice,c.MonthId ";

            ForecastTotalManMonthCostsViewModal _forecastTotalManMonthCost = new ForecastTotalManMonthCostsViewModal();
            decimal totalCosts = 0;
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
                            if (Convert.ToInt32(rdr["MonthId"]) == 10)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalOctCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.OctTotalCosts))
                                {
                                    _forecastTotalManMonthCost.OctTotalCosts = totalOctCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.OctTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalCosts) + Convert.ToDecimal(totalOctCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 11)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalNovCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.NovTotalCosts))
                                {
                                    _forecastTotalManMonthCost.NovTotalCosts = totalNovCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.NovTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalCosts) + Convert.ToDecimal(totalNovCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 12)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalDecCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.DecTotalCosts))
                                {
                                    _forecastTotalManMonthCost.DecTotalCosts = totalDecCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.DecTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalCosts) + Convert.ToDecimal(totalDecCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 1)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalJanCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.JanTotalCosts))
                                {
                                    _forecastTotalManMonthCost.JanTotalCosts = totalJanCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.JanTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalCosts) + Convert.ToDecimal(totalJanCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 2)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalFebCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.FebTotalCosts))
                                {
                                    _forecastTotalManMonthCost.FebTotalCosts = totalFebCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.FebTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalCosts) + Convert.ToDecimal(totalFebCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 3)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalMarCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.MarTotalCosts))
                                {
                                    _forecastTotalManMonthCost.MarTotalCosts = totalMarCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.MarTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalCosts) + Convert.ToDecimal(totalMarCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 4)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalAprCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.AprTotalCosts))
                                {
                                    _forecastTotalManMonthCost.AprTotalCosts = totalAprCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.AprTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalCosts) + Convert.ToDecimal(totalAprCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 5)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalMayCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.MayTotalCosts))
                                {
                                    _forecastTotalManMonthCost.MayTotalCosts = totalMayCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.MayTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalCosts) + Convert.ToDecimal(totalMayCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 6)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalJunCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.JunTotalCosts))
                                {
                                    _forecastTotalManMonthCost.JunTotalCosts = totalJunCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.JunTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalCosts) + Convert.ToDecimal(totalJunCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 7)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalJulCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.JulTotalCosts))
                                {
                                    _forecastTotalManMonthCost.JulTotalCosts = totalJulCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.JulTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalCosts) + Convert.ToDecimal(totalJulCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 8)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalAugCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.AugTotalCosts))
                                {
                                    _forecastTotalManMonthCost.AugTotalCosts = totalAugCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.AugTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalCosts) + Convert.ToDecimal(totalAugCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 9)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalSepCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.SepTotalCosts))
                                {
                                    _forecastTotalManMonthCost.SepTotalCosts = totalSepCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.SepTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalCosts) + Convert.ToDecimal(totalSepCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalCosts);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            //_forecastTotalManMonthCost.TotalCosts = totalCosts.ToString();            
            return _forecastTotalManMonthCost;
        }

        public List<EmployeeAssignment> GetEmployeeNameForMenuChange(int year,int employeeId)
        {
            List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();

            string query = "";
            query = query + "SELECT ea.Id,ea.EmployeeId,ea.EmployeeName,e.FullName 'RootName' ";
            query = query + "FROM EmployeesAssignments ea ";
            query = query + "   INNER JOIN Employees e ON ea.EmployeeId=e.Id ";
            query = query + "WHERE ea.EmployeeId="+ employeeId + " AND ea.Year="+ year + " ";

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
                            employeeAssignment.EmployeeRootName = rdr["RootName"] is DBNull ? "" : rdr["RootName"].ToString();
                            employeeAssignment.EmployeeModifiedName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();

                            employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;



        }
        public List<EmployeeAssignment> GetDeletedEmployeeCount(int year, int employeeId)
        {
            List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();

            string query = "";
            query = query + "SELECT ea.Id,ea.EmployeeId,ea.EmployeeName,e.FullName 'RootName' ";
            query = query + "FROM EmployeesAssignments ea ";
            query = query + "   INNER JOIN Employees e ON ea.EmployeeId=e.Id ";
            query = query + "WHERE ea.EmployeeId=" + employeeId + " AND ea.Year=" + year + " AND ea.IsDeleted=1";

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
                            employeeAssignment.EmployeeRootName = rdr["RootName"] is DBNull ? "" : rdr["RootName"].ToString();
                            employeeAssignment.EmployeeModifiedName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();

                            employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return employeeAssignments;
        }

        public int RemoveBudgetAssignment(string budgetAssignmentId)
        {
            int result = 0;
            string query = $@"update EmployeeeBudgets set IsActive=0 where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", budgetAssignmentId);
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
        public ForecastTotalManMonthCostsViewModal GetTotalManMonthAndCostForBudgetEdit(int year,int budgetType)
        {
            //List<ForecastTotalManMonthCostsViewModal> _forecastTotalManMonthCosts = new List<ForecastTotalManMonthCostsViewModal>();
            string query = "";
            query = query + "SELECT c.MonthId,SUM(c.Points) as 'TotalMM' ";
            query = query + "FROM BudgetCosts c ";
            query = query + "    INNER JOIN EmployeeeBudgets eb ON c.EmployeeBudgetId = eb.Id ";
            if (budgetType == 1) { 
                query = query + "WHERE c.Year = "+ year + " AND eb.FirstHalfBudget = 1 ";
            }
            else
            {
                query = query + "WHERE c.Year = "+ year + " AND eb.SecondHalfBudget = 1 ";
            }
            query = query + "GROUP BY c.MonthId ";


            ForecastTotalManMonthCostsViewModal _forecastTotalManMonthCost = new ForecastTotalManMonthCostsViewModal();
            decimal totalManMonth = 0;
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
                            if (Convert.ToInt32(rdr["MonthId"]) == 10)
                            {
                                _forecastTotalManMonthCost.OctTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 11)
                            {
                                _forecastTotalManMonthCost.NovTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 12)
                            {
                                _forecastTotalManMonthCost.DecTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 1)
                            {
                                _forecastTotalManMonthCost.JanTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 2)
                            {
                                _forecastTotalManMonthCost.FebTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 3)
                            {
                                _forecastTotalManMonthCost.MarTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 4)
                            {
                                _forecastTotalManMonthCost.AprTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 5)
                            {
                                _forecastTotalManMonthCost.MayTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 6)
                            {
                                _forecastTotalManMonthCost.JunTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 7)
                            {
                                _forecastTotalManMonthCost.JulTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 8)
                            {
                                _forecastTotalManMonthCost.AugTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalMM);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 9)
                            {
                                _forecastTotalManMonthCost.SepTotalMM = rdr["TotalMM"] is DBNull ? "0.0" : Convert.ToDecimal(rdr["TotalMM"]).ToString("0.0");
                                totalManMonth = totalManMonth + Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalMM);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            _forecastTotalManMonthCost.TotalManMonth = Convert.ToDecimal(totalManMonth).ToString("0.0");
            ForecastTotalManMonthCostsViewModal _forecastCostsViewModal = new ForecastTotalManMonthCostsViewModal();
            _forecastCostsViewModal = GetTotalCostsForBudget(year,budgetType);

            _forecastTotalManMonthCost.OctTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.OctTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.NovTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.NovTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.DecTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.DecTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.JanTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.JanTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.FebTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.FebTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.MarTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.MarTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.AprTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.AprTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.MayTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.MayTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.JunTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.JunTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.JulTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.JulTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.AugTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.AugTotalCosts).ToString("N0");
            _forecastTotalManMonthCost.SepTotalCosts = Convert.ToDecimal(_forecastCostsViewModal.SepTotalCosts).ToString("N0");
            //_forecastTotalManMonthCost.TotalCosts = _forecastCostsViewModal.TotalCosts;
            _forecastTotalManMonthCost.TotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalCosts) + Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalCosts)).ToString();
            _forecastTotalManMonthCost.TotalCosts = Convert.ToDecimal(_forecastTotalManMonthCost.TotalCosts).ToString("N0");
            return _forecastTotalManMonthCost;
        }
        public ForecastTotalManMonthCostsViewModal GetTotalCostsForBudget(int year,int budgetType)
        {
            //List<ForecastTotalManMonthCostsViewModal> _forecastTotalManMonthCosts = new List<ForecastTotalManMonthCostsViewModal>();
            string query = "";
            query = query + "SELECT ea.UnitPrice,c.MonthId,SUM(c.Points) as 'TPoints' ";
            query = query + "FROM BudgetCosts c ";
            query = query + "    INNER JOIN EmployeeeBudgets ea ON c.EmployeeBudgetId = ea.Id ";
            if (budgetType == 1) { 
                query = query + "WHERE c.year = "+ year + " AND ea.FirstHalfBudget = 1 ";
            }
            else
            {
                query = query + "WHERE c.year = " + year + " AND ea.SecondHalfBudget = 1 ";
            }
            query = query + "GROUP BY ea.UnitPrice,c.MonthId ";

            ForecastTotalManMonthCostsViewModal _forecastTotalManMonthCost = new ForecastTotalManMonthCostsViewModal();
            decimal totalCosts = 0;
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
                            if (Convert.ToInt32(rdr["MonthId"]) == 10)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalOctCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.OctTotalCosts))
                                {
                                    _forecastTotalManMonthCost.OctTotalCosts = totalOctCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.OctTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalCosts) + Convert.ToDecimal(totalOctCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.OctTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 11)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalNovCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.NovTotalCosts))
                                {
                                    _forecastTotalManMonthCost.NovTotalCosts = totalNovCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.NovTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalCosts) + Convert.ToDecimal(totalNovCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.NovTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 12)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalDecCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.DecTotalCosts))
                                {
                                    _forecastTotalManMonthCost.DecTotalCosts = totalDecCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.DecTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalCosts) + Convert.ToDecimal(totalDecCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.DecTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 1)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalJanCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.JanTotalCosts))
                                {
                                    _forecastTotalManMonthCost.JanTotalCosts = totalJanCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.JanTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalCosts) + Convert.ToDecimal(totalJanCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.JanTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 2)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalFebCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.FebTotalCosts))
                                {
                                    _forecastTotalManMonthCost.FebTotalCosts = totalFebCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.FebTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalCosts) + Convert.ToDecimal(totalFebCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.FebTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 3)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalMarCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.MarTotalCosts))
                                {
                                    _forecastTotalManMonthCost.MarTotalCosts = totalMarCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.MarTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalCosts) + Convert.ToDecimal(totalMarCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.MarTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 4)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalAprCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.AprTotalCosts))
                                {
                                    _forecastTotalManMonthCost.AprTotalCosts = totalAprCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.AprTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalCosts) + Convert.ToDecimal(totalAprCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.AprTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 5)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalMayCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.MayTotalCosts))
                                {
                                    _forecastTotalManMonthCost.MayTotalCosts = totalMayCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.MayTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalCosts) + Convert.ToDecimal(totalMayCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.MayTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 6)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalJunCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.JunTotalCosts))
                                {
                                    _forecastTotalManMonthCost.JunTotalCosts = totalJunCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.JunTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalCosts) + Convert.ToDecimal(totalJunCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.JunTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 7)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalJulCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.JulTotalCosts))
                                {
                                    _forecastTotalManMonthCost.JulTotalCosts = totalJulCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.JulTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalCosts) + Convert.ToDecimal(totalJulCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.JulTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 8)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalAugCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.AugTotalCosts))
                                {
                                    _forecastTotalManMonthCost.AugTotalCosts = totalAugCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.AugTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalCosts) + Convert.ToDecimal(totalAugCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.AugTotalCosts);
                            }
                            if (Convert.ToInt32(rdr["MonthId"]) == 9)
                            {
                                decimal totalPoints = rdr["TPoints"] is DBNull ? 0 : Convert.ToDecimal(rdr["TPoints"]);
                                decimal totalUnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                                decimal totalSepCost = totalUnitPrice * totalPoints;

                                if (string.IsNullOrEmpty(_forecastTotalManMonthCost.SepTotalCosts))
                                {
                                    _forecastTotalManMonthCost.SepTotalCosts = totalSepCost.ToString();
                                }
                                else
                                {
                                    _forecastTotalManMonthCost.SepTotalCosts = (Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalCosts) + Convert.ToDecimal(totalSepCost)).ToString();
                                }
                                totalCosts = totalCosts + Convert.ToDecimal(_forecastTotalManMonthCost.SepTotalCosts);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            //_forecastTotalManMonthCost.TotalCosts = totalCosts.ToString();            
            return _forecastTotalManMonthCost;
        }

        public List<QaProportion> GetQAProportionPercentageWithEmployee(string employeeId, string year)
        {
            List<QaProportion> objQAProportions = new List<QaProportion>();

            string query = "";
            query = query + "SELECT qp.*,d.Name 'DepartmentName' ";
            query = query + "FROM QaProportions qp ";
            query = query + "    INNER JOIN Departments d ON qp.DepartmentId=d.Id ";
            query = query + "WHERE qp.EmployeeId=" + employeeId + " AND qp.Year=" + year;

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
                            QaProportion objQAProportion = new QaProportion();
                            objQAProportion.Id = Convert.ToInt32(rdr["Id"]);
                            objQAProportion.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            objQAProportion.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            objQAProportion.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();

                            objQAProportion.OctPercentage = rdr["OctPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["OctPercentage"]);
                            objQAProportion.NovPercentage = rdr["NovPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["NovPercentage"]);
                            objQAProportion.DecPercentage = rdr["DecPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["DecPercentage"]);
                            objQAProportion.JanPercentage = rdr["JanPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["JanPercentage"]);
                            objQAProportion.FebPercentage = rdr["FebPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["FebPercentage"]);
                            objQAProportion.MarPercentage = rdr["MarPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["MarPercentage"]);
                            objQAProportion.AprPercentage = rdr["AprPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["AprPercentage"]);
                            objQAProportion.MayPercentage = rdr["MayPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["MayPercentage"]);
                            objQAProportion.JunPercentage = rdr["JunPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["JunPercentage"]);
                            objQAProportion.JulPercentage = rdr["JulPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["JulPercentage"]);
                            objQAProportion.AugPercentage = rdr["AugPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["AugPercentage"]);
                            objQAProportion.SepPercentage = rdr["SepPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["SepPercentage"]);

                            objQAProportions.Add(objQAProportion);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return objQAProportions;
        }
        public List<QaProportion> GetQAProportionPercentageWithoutEmployee(string year)
        {
            List<QaProportion> objQAProportions = new List<QaProportion>();

            string query = "";
            query = query + "SELECT ap.*,d.Name 'DepartmentName' ";
            query = query + "FROM Apportionments ap ";
            query = query + "    INNER JOIN Departments d ON ap.DepartmentId=d.Id ";
            query = query + "WHERE ap.Year = " + year;

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
                            QaProportion objQAProportion = new QaProportion();
                            objQAProportion.Id = Convert.ToInt32(rdr["Id"]);
                            //objQAProportion.EmployeeId = 0;
                            objQAProportion.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            objQAProportion.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();

                            objQAProportion.OctPercentage = rdr["OctPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["OctPercentage"]);
                            objQAProportion.NovPercentage = rdr["NovPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["NovPercentage"]);
                            objQAProportion.DecPercentage = rdr["DecPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["DecPercentage"]);
                            objQAProportion.JanPercentage = rdr["JanPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["JanPercentage"]);
                            objQAProportion.FebPercentage = rdr["FebPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["FebPercentage"]);
                            objQAProportion.MarPercentage = rdr["MarPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["MarPercentage"]);
                            objQAProportion.AprPercentage = rdr["AprPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["AprPercentage"]);
                            objQAProportion.MayPercentage = rdr["MayPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["MayPercentage"]);
                            objQAProportion.JunPercentage = rdr["JunPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["JunPercentage"]);
                            objQAProportion.JulPercentage = rdr["JulPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["JulPercentage"]);
                            objQAProportion.AugPercentage = rdr["AugPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["AugPercentage"]);
                            objQAProportion.SepPercentage = rdr["SepPercentage"] is DBNull ? 0.0 : Convert.ToDouble(rdr["SepPercentage"]);

                            objQAProportions.Add(objQAProportion);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return objQAProportions;
        }

        public List<QaProportion> GetQAProportionsWithEmployee(string employeeId,string year)
        {
            List<QaProportion> objQAProportions = new List<QaProportion>();
            objQAProportions = GetQAProportionPercentageWithEmployee(employeeId,year);
            if (objQAProportions.Count > 0)
            {
                return objQAProportions;
            }
            else
            {
                objQAProportions = GetQAProportionPercentageWithoutEmployee(year);
                return objQAProportions;                
            }            
        }
        public int InsertEmployeeAssignmentsForTimeStamps(EmployeeAssignment employeeAssignment,int timeStampId)
        {
            int result = 0;
            string query = $@"insert into EmployeesAssignmentsWithTimeStamps(EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,CreatedBy,CreatedDate,IsActive,Remarks,Year,BCYR,BCYRCell,EmployeeName,TimeStampsId,EmployeeAssignmentId) values(@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@createdBy,@createdDate,@isActive,@remarks,@year,@bCYR,@bCYRCell,@employeeName,@timeStampsId,@employeeAssignmentId);";
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
                cmd.Parameters.AddWithValue("@year", employeeAssignment.Year);
                cmd.Parameters.AddWithValue("@bCYR", employeeAssignment.BCYR);
                cmd.Parameters.AddWithValue("@bCYRCell", employeeAssignment.BCYRCell);                
                cmd.Parameters.AddWithValue("@employeeName", employeeAssignment.EmployeeName);
                cmd.Parameters.AddWithValue("@timeStampsId", timeStampId);
                cmd.Parameters.AddWithValue("@employeeAssignmentId", employeeAssignment.Id);

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
        public List<EmployeeAssignment> GetEmployeesAssignmentsByYear(int year, string strUpdatedAssignmentIds)
        {
            List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();

            string query = $@"select ea.id as AssignmentId,ep.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.EmployeeName 'DuplicateName',ea.GradeId,ea.EmployeeId,ea.Year,ea.BCYR,ea.BCYRCell
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            Inner join Employees ep on ea.EmployeeId = ep.Id
                            where ea.Year={year} and ea.Id NOT IN ({strUpdatedAssignmentIds})
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
                            EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                            employeeAssignment.Id = Convert.ToInt32(rdr["AssignmentId"]);
                            employeeAssignment.EmployeeId = rdr["EmployeeId"] is DBNull ? "": rdr["EmployeeId"].ToString();
                            employeeAssignment.SectionId = rdr["SectionId"] is DBNull ? 0 : Convert.ToInt32(rdr["SectionId"]);
                            employeeAssignment.DepartmentId = rdr["DepartmentId"] is DBNull ? 0 : Convert.ToInt32(rdr["DepartmentId"]);
                            employeeAssignment.InchargeId = rdr["InchargeId"] is DBNull ? 0 : Convert.ToInt32(rdr["InchargeId"]);
                            employeeAssignment.RoleId = rdr["RoleId"] is DBNull ? 0 : Convert.ToInt32(rdr["RoleId"]);
                            employeeAssignment.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();                            
                            employeeAssignment.CompanyId = rdr["CompanyId"] is DBNull ? 0 : Convert.ToInt32(rdr["CompanyId"]);                                                                                    
                            employeeAssignment.UnitPrice = rdr["UnitPrice"] is DBNull ? 0: Convert.ToDecimal(rdr["UnitPrice"]);
                            employeeAssignment.GradeId = rdr["GradeId"] is DBNull ? 0 : Convert.ToInt32(rdr["GradeId"]);
                            employeeAssignment.IsActive = rdr["IsActive"] is DBNull ? "0" : rdr["IsActive"].ToString();
                            employeeAssignment.Remarks = rdr["Remarks"] is DBNull ? "0" : rdr["Remarks"].ToString();
                            employeeAssignment.EmployeeName = rdr["DuplicateName"] is DBNull ? "0" : rdr["DuplicateName"].ToString();
                            employeeAssignment.Year = rdr["Year"] is DBNull ? "0" : rdr["Year"].ToString();
                            employeeAssignment.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "0" : rdr["BCYRCell"].ToString();                            

                            employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return employeeAssignments;            
        }
        public List<Forecast> GetAssignmentForecastByYearAndAssignmentId(int assignmentId, int year)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "select * from Costs where EmployeeAssignmentsId=" + assignmentId + " and Year=" + year;
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
                            Forecast forecast = new Forecast();
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);
                            forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);
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



    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;
using System.Globalization;

namespace CostAllocationApp.DAL
{
    public class SalaryDAL : DbContext
    {
        public int CreateSalary(Salary salary)
        {
            int result = 0;
            string query = $@"insert into Grades(GradePoints,GradeLowPoints,GradeHighPoints,CreatedBy,CreatedDate,IsActive) values(@gradePoints,@gradeLowPoints,@gradeHighPoints,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@gradePoints", salary.SalaryGrade);
                cmd.Parameters.AddWithValue("@gradeLowPoints", salary.SalaryLowPoint);
                cmd.Parameters.AddWithValue("@gradeHighPoints", salary.SalaryHighPoint);
                cmd.Parameters.AddWithValue("@createdBy", salary.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", salary.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", salary.IsActive);
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
        public int UpdateSalary(Salary salary)
        {
            int result = 0;
            string query = $@"update Grades set GradePoints=@gradePoints,GradeLowPoints=@gradeLowPoints,GradeHighPoints=@gradeHighPoints,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", salary.Id);
                cmd.Parameters.AddWithValue("@gradePoints", salary.SalaryGrade);
                cmd.Parameters.AddWithValue("@gradeLowPoints", salary.SalaryLowPoint);
                cmd.Parameters.AddWithValue("@gradeHighPoints", salary.SalaryHighPoint);
                cmd.Parameters.AddWithValue("@updatedBy", salary.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", salary.UpdatedDate);
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
        public List<Salary> GetAllSalaryPoints()
        {
            List<Salary> salaries = new List<Salary>();
            string query = "";
            query = "SELECT * FROM Grades WHERE IsActive=1 ORDER BY len(GradePoints), GradePoints";
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
                            Salary salary = new Salary();
                            salary.Id = Convert.ToInt32(rdr["Id"]);
                            salary.SalaryGrade = rdr["GradePoints"].ToString();
                            salary.SalaryLowPoint = Convert.ToDecimal(rdr["GradeLowPoints"]);
                            salary.SalaryLowPointWithComma = Convert.ToInt32(rdr["GradeLowPoints"]).ToString("N0");
                            salary.SalaryHighPoint = Convert.ToDecimal(rdr["GradeHighPoints"]);
                            salary.SalaryHighPointWithComma = Convert.ToInt32(rdr["GradeHighPoints"]).ToString("N0");
                            salary.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            salary.CreatedBy = rdr["CreatedBy"].ToString();

                            salaries.Add(salary);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return salaries;
            }
        }
        public int RemoveSalary(int salaryIds)
        {
            int result = 0;            
            string query = $@"DELETE FROM Grades WHERE id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", salaryIds);
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
        public bool CheckGrade(Salary salary)
        {
            string query = $"select * from Grades where GradePoints=N'{salary.SalaryGrade}' or GradeLowPoints={salary.SalaryLowPoint} or GradeHighPoints={salary.SalaryHighPoint}";
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

        public int GetSalaryCountWithEmployeeAsignment(int gradeId)
        {
            string query = "select * from EmployeesAssignments where GradeId=" + gradeId;
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

        public Salary GetSalaryBySalaryId(int salaryId)
        {
            Salary salary = null;
            string query = "select * from Grades where Id = " + salaryId;
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
                            salary = new Salary();
                            salary.Id = Convert.ToInt32(rdr["Id"]);
                            salary.SalaryLowPoint = Convert.ToDecimal(rdr["GradeLowPoints"]);
                            salary.SalaryHighPoint = Convert.ToDecimal(rdr["GradeHighPoints"]);

                            salary.SalaryGrade = rdr["GradePoints"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    salary = null;
                }

                return salary;
            }
        }
        public int GetGradeIdByGradePoint(string gradePoint)
        {
            int returnGradeId = 0;
            string query = "select * from Grades where GradePoints= "+ gradePoint;
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
                            returnGradeId = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    returnGradeId = 0;
                }

                return returnGradeId;
            }
        }        
    }
}
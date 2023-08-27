using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class DepartmentWithSubCategoryDAL : DbContext
    {
        public int CreateDepartmentWithSubCategory(DepartmentWithSubCategory departmentWithSubCategory)
        {
            int result = 0;
            //string query = $@"UPDATE Departments SET SubCategoryId=@subCategoryId,UpdatedDate=@updatedDate,UpdatedBy=@updatedBy where Id In"++"";
            string query = "UPDATE Departments SET SubCategoryId="+ departmentWithSubCategory.SubCategoryId + ",UpdatedDate='"+ departmentWithSubCategory.UpdatedDate + "',UpdatedBy='"+ departmentWithSubCategory.UpdatedBy + "' where Id In ("+ departmentWithSubCategory.DepartmentId + ") ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open(); 
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                //cmd.Parameters.AddWithValue("@subCategoryId", );
                //cmd.Parameters.AddWithValue("@departmentId", );
                //cmd.Parameters.AddWithValue("@updatedDate", );
                //cmd.Parameters.AddWithValue("@updatedBy", );
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
        //public List<SubCategory> GetAllSubCategories()
        //{
        //    List<SubCategory> subCategories = new List<SubCategory>();
        //    string query = "";
        //    query = query + "SELECT d.Id 'SubCategoryId',d.SubCategoryName,s.Id 'CategoryId',s.CategoryName ,d.CreatedDate,d.CreatedBy ";
        //    query = query + "FROM SubCategories d ";
        //    query = query + "   INNER JOIN Categories s ON d.CategoryId = s.Id ";
        //    query = query + "WHERE d.IsActive=1";

        //    using (SqlConnection sqlConnection = this.GetConnection())
        //    {
        //        sqlConnection.Open();
        //        SqlCommand cmd = new SqlCommand(query, sqlConnection);
        //        try
        //        {
        //            SqlDataReader rdr = cmd.ExecuteReader();
        //            if (rdr.HasRows)
        //            {
        //                while (rdr.Read())
        //                {
        //                    SubCategory subCategory = new SubCategory();
        //                    subCategory.Id = Convert.ToInt32(rdr["SubCategoryId"]);
        //                    subCategory.CategoryId = rdr["CategoryId"].ToString();
        //                    subCategory.SubCategoryName = rdr["SubCategoryName"].ToString();
        //                    subCategory.CategoryName = rdr["CategoryName"].ToString();
        //                    subCategory.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
        //                    subCategory.CreatedBy = rdr["CreatedBy"].ToString();

        //                    subCategories.Add(subCategory);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        return subCategories;
        //    }
        //}

        //public int RemoveSubCategory(int subCategoryId)
        //{
        //    int result = 0;
        //    string query = $@"update SubCategories set isactive=0 where id=@id";
        //    using (SqlConnection sqlConnection = this.GetConnection())
        //    {
        //        sqlConnection.Open();
        //        SqlCommand cmd = new SqlCommand(query, sqlConnection);
        //        cmd.Parameters.AddWithValue("@id", subCategoryId);
        //        try
        //        {
        //            result = cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        return result;
        //    }

        //}

        public bool CheckSubCategoryIsAssignedToDepartment(string departmentId)
        {
            string query = "";
            query = query + "SELECT * ";
            query = query + "FROM Departments d ";
            query = query + "    INNER JOIN SubCategories sc ON d.SubCategoryId=sc.Id ";
            query = query + "WHERE d.Id= "+ departmentId + " ";

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
        //public Category GetCategoryByCategoryId(int categoryId)
        //{
        //    Category category = null;
        //    string query = "select * from Categories where Id = " + categoryId;

        //    using (SqlConnection sqlConnection = this.GetConnection())
        //    {
        //        sqlConnection.Open();
        //        SqlCommand cmd = new SqlCommand(query, sqlConnection);
        //        try
        //        {
        //            SqlDataReader rdr = cmd.ExecuteReader();
        //            if (rdr.HasRows)
        //            {
        //                while (rdr.Read())
        //                {
        //                    category = new Category();
        //                    category.CategoryName = rdr["CategoryName"].ToString();
        //                    category.Id = Convert.ToInt32(rdr["Id"]);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            category = null;
        //        }

        //        return category;
        //    }
        //}
        public List<SubCategory> GetSubCategoryByCategoryId(int categoryId)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            string query = "";
            query = "SELECT * FROM SubCategories WHERE CategoryId=" + categoryId;
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
        public List<Department> GetAllUnassignedDepartments()
        {
            List<Department> departments = new List<Department>();
            string query = "";
            query = "SELECT * FROM Departments WHERE SubCategoryId IS NULL OR SubCategoryId = 0 ";
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
        public List<DepartmentWithSubCategory> GetDepartmentCategoryAndSubCategory()
        {
            List<DepartmentWithSubCategory> departmentWithSubCategories = new List<DepartmentWithSubCategory>();
            string query = "";
            query = query + "SELECT D.Name 'DepartmentName',D.Id 'DepartmentId',c.CategoryName,sc.SubCategoryName,c.Id 'CategoryId',sc.Id 'SubCategoryId' ";
            query = query + "FROM Departments d ";
            query = query + "    INNER JOIN SubCategories sc ON d.SubCategoryId=sc.Id ";
            query = query + "    INNER JOIN Categories c ON sc.CategoryId=c.Id ";

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
                            DepartmentWithSubCategory departmentWithSubCategory = new DepartmentWithSubCategory();
                            departmentWithSubCategory.DepartmentId = rdr["DepartmentId"] is DBNull ? "0" : rdr["DepartmentId"].ToString();
                            departmentWithSubCategory.DepartmentName = rdr["DepartmentName"] is DBNull ? "0" : rdr["DepartmentName"].ToString();
                            departmentWithSubCategory.CategoryId = rdr["CategoryId"] is DBNull ? 0 : Convert.ToInt32(rdr["CategoryId"]);
                            departmentWithSubCategory.CategoryName = rdr["CategoryName"] is DBNull ? "0" : rdr["CategoryName"].ToString();
                            departmentWithSubCategory.SubCategoryId = rdr["SubCategoryId"] is DBNull ? "0" : rdr["SubCategoryId"].ToString();
                            departmentWithSubCategory.SubCategoryName = rdr["SubCategoryName"] is DBNull ? "0" : rdr["SubCategoryName"].ToString();

                            departmentWithSubCategories.Add(departmentWithSubCategory);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return departmentWithSubCategories;
            }

        }
    }
}
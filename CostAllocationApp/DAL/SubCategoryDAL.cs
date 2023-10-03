using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class SubCategoryDAL : DbContext
    {
        public int CreateSubCategory(SubCategory subCategory)
        {
            int result = 0;
            string query = $@"insert into SubCategories(SubCategoryName,CategoryId,CreatedBy,CreatedDate,IsActive) values(@subCategoryName,@categoryId,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@subCategoryName", subCategory.SubCategoryName);
                cmd.Parameters.AddWithValue("@categoryId", subCategory.CategoryId);
                cmd.Parameters.AddWithValue("@createdBy", subCategory.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", subCategory.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", subCategory.IsActive);
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

        public List<SubCategory> GetAllSubCategories()
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            string query = "";
            query = query + "SELECT d.Id 'SubCategoryId',d.SubCategoryName,s.Id 'CategoryId',s.CategoryName ,d.CreatedDate,d.CreatedBy ";
            query = query + "FROM SubCategories d ";
            query = query + "   INNER JOIN Categories s ON d.CategoryId = s.Id ";
            query = query + "WHERE d.IsActive=1";

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
                            subCategory.Id = Convert.ToInt32(rdr["SubCategoryId"]);
                            subCategory.CategoryId = rdr["CategoryId"].ToString();
                            subCategory.SubCategoryName = rdr["SubCategoryName"].ToString();
                            subCategory.CategoryName = rdr["CategoryName"].ToString();
                            subCategory.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            subCategory.CreatedBy = rdr["CreatedBy"].ToString();

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

        public SubCategory GetSubCategoryById(int subCategoryId)
        {
            SubCategory subCategory = new SubCategory();
            string query = "";
            query = query + "SELECT d.Id 'SubCategoryId',d.SubCategoryName,s.Id 'CategoryId',s.CategoryName ,d.CreatedDate,d.CreatedBy ";
            query = query + "FROM SubCategories d ";
            query = query + "   INNER JOIN Categories s ON d.CategoryId = s.Id ";
            query = query + "WHERE d.id="+ subCategoryId;

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
                            subCategory.Id = Convert.ToInt32(rdr["SubCategoryId"]);
                            subCategory.CategoryId = rdr["CategoryId"].ToString();
                            subCategory.SubCategoryName = rdr["SubCategoryName"].ToString();
                            subCategory.CategoryName = rdr["CategoryName"].ToString();
                            subCategory.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            subCategory.CreatedBy = rdr["CreatedBy"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return subCategory;
            }
        }

        public int RemoveSubCategory(SubCategory subCategory)
        {
            int result = 0;
            string query = $@"update SubCategories set isactive=@isactive, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isactive", subCategory.IsActive);
                cmd.Parameters.AddWithValue("@updatedBy", subCategory.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", subCategory.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", subCategory.Id);
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

        public bool CheckSubCategory(string subCategoryName)
        {
            string query = "select * from SubCategories where SubCategoryName=N'" + subCategoryName + "'";
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

        public List<SubCategory> GetSubCategoryByCategoryId(int categoryId)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            string query = "select * from SubCategories where isactive=1 and CategoryId = " + categoryId;

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
    }
}
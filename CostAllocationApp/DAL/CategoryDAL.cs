using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;


namespace CostAllocationApp.DAL
{
    public class CategoryDAL : DbContext
    {
        public int CreateCategory(Category category)
        {
            int result = 0;
            string query = $@"insert into Categories(CategoryName,CreatedBy,CreatedDate,IsActive) values(@categoryName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@createdBy", category.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", category.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", category.IsActive);
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

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "select * from Categories where IsActive=1";
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
                            category.CategoryName = rdr["CategoryName"].ToString();
                            //category.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            //category.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            //category.CreatedBy = rdr["CreatedBy"].ToString();

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

        public int RemoveCategory(Category category)
        {
            int result = 0;
            string query = $@"update Categories set isactive=@isactive, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isactive", category.IsActive);
                cmd.Parameters.AddWithValue("@updatedBy", category.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", category.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", category.Id);
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

        public int UpdateCategory(Category category)
        {
            int result = 0;
            string query = $@"update Categories set CategoryName=@categoryName, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@updatedBy", category.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", category.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", category.Id);
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

        public bool CheckCategory(string categoryName)
        {
            string query = "select * from Categories where CategoryName=N'" + categoryName + "'";
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

        public Category GetCategoryByCategoryId(int categoryId)
        {
            Category category = null;
            string query = "select * from Categories where Id = " + categoryId;
            
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
                            category = new Category();
                            category.CategoryName = rdr["CategoryName"].ToString();
                            category.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    category = null;
                }

                return category;
            }
        }
    }
}
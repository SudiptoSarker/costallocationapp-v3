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
            string query = $@"insert into Categories(CategoryName,CreatedBy,CreatedDate,IsActive,DynamicTableId) values(@categoryName,@createdBy,@createdDate,@isActive,@dynamicTableId)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@createdBy", category.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", category.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", category.IsActive);
                cmd.Parameters.AddWithValue("@dynamicTableId", category.DynamicTableId);
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

        public List<Category> GetAllCategoriesByDynamicTableId(int dynamicTableId)
        {
            List<Category> categories = new List<Category>();
            string query = ""; 
            query = query + "SELECT c.Id,c.CategoryName,c.IsActive,c.DynamicTableId,dt.CategoryTitle,dt.SubCategoryTitle,dt.DetailsTitle ";
            query = query + "FROM Categories c ";
            query = query + "    INNER JOIN DynamicTables dt ON c.DynamicTableId=dt.Id ";
            query = query + "where DynamicTableId=" + dynamicTableId;

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
                            category.SubTitleName = rdr["SubCategoryTitle"] is DBNull ? "" : rdr["SubCategoryTitle"].ToString();
                            if (!string.IsNullOrEmpty(category.SubTitleName))
                            {
                                category.IsSubTitle = true;
                            }
                            else
                            {
                                category.IsSubTitle = false;
                            }
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
            string query = $@"delete from  Categories where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
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
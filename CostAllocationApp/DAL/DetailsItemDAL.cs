using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class DetailsItemDAL : DbContext
    {
        public int CreateDetailsItem(DeatailsItem detailsItem)
        {
            int result = 0;
            string query = $@"insert into DetailsItems(DetailsItemName,SubCategoryId,CreatedBy,CreatedDate,IsActive) values(@detailsItemName,@subCategoryId,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@detailsItemName", detailsItem.DetailsItemName);
                cmd.Parameters.AddWithValue("@subCategoryId", detailsItem.SubCategoryId);
                cmd.Parameters.AddWithValue("@createdBy", detailsItem.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", detailsItem.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", detailsItem.IsActive);
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

        public DeatailsItem GetDetailsItemById(int detailsItemId)
        {
            DeatailsItem deatailsItem = null;
            string query = "select d.Id,d.DetailsItemName, d.SubCategoryId ,sb.SubCategoryName, c.CategoryName from DetailsItems d join SubCategories sb on d.SubCategoryId = sb.Id join Categories c on sb.CategoryId = c.Id  where d.id = " + detailsItemId;

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
                            deatailsItem = new DeatailsItem();
                            deatailsItem.DetailsItemName = rdr["DetailsItemName"].ToString();
                            deatailsItem.SubCategoryId = rdr["SubCategoryId"].ToString();
                            deatailsItem.SubItemName = rdr["SubCategoryName"].ToString();
                            deatailsItem.MainItemName = rdr["CategoryName"].ToString();
                            deatailsItem.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    deatailsItem = null;
                }

                return deatailsItem;
            }
        }

        public List<DeatailsItem> GetDetailsItemBySubItemsId(int subItemId)
        {
            List<DeatailsItem> deatailsItems = new List<DeatailsItem>();
            string query = "select d.Id,d.DetailsItemName, sb.SubCategoryName, c.CategoryName from DetailsItems d join SubCategories sb on d.SubCategoryId = sb.Id join Categories c on sb.CategoryId = c.Id  where d.isactive=1 and d.SubCategoryId = " + subItemId;

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
                            DeatailsItem deatailsItem = new DeatailsItem();
                            deatailsItem.DetailsItemName = rdr["DetailsItemName"].ToString();
                            deatailsItem.SubItemName = rdr["SubCategoryName"].ToString();
                            deatailsItem.MainItemName = rdr["CategoryName"].ToString();
                            deatailsItem.Id = Convert.ToInt32(rdr["Id"]);
                            deatailsItems.Add(deatailsItem);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return deatailsItems;
            }
        }

        public int RemoveDetailsItem(DeatailsItem deatailsItem)
        {
            int result = 0;
            string query = $@"delete from  DetailsItems where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", deatailsItem.Id);
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

        public int UpdateDetailsItem(DeatailsItem deatailsItem)
        {
            int result = 0;
            string query = $@"update DetailsItems set DetailsItemName=@detailsItemName, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@detailsItemName", deatailsItem.DetailsItemName);
                cmd.Parameters.AddWithValue("@updatedBy", deatailsItem.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", deatailsItem.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", deatailsItem.Id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class SectionDAL:DbContext
    {
        // Create New Section
        public int CreateSection(Section section)
        {           
            int result = 0;
            string query = $@"insert into Sections(Name,CreatedBy,CreatedDate,IsActive) values(@sectionName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query,sqlConnection);
                cmd.Parameters.AddWithValue("@sectionName",section.SectionName);
                cmd.Parameters.AddWithValue("@createdBy", section.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", section.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", section.IsActive);
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

        // Update Section
        public int UpdateSection(Section section)
        {            
            int result = 0;
            string query = "";
            query = query + "UPDATE Sections ";
            query = query + "SET Name = N'"+ section.SectionName+ "',UpdatedBy=N'"+ section.UpdatedBy+ "',UpdatedDate='" + section.UpdatedDate + "',IsActive=1";
            query = query + "WHERE Id = "+section.Id+" ";
            
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
                    return result = 0;
                }

                return result;
            }

        }

        // Get Section List
        public List<Section> GetAllSections()
        {
            List<Section> sections = new List<Section>();
            string query = "select * from Sections where isactive=1";
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
                            Section section = new Section();
                            section.Id = Convert.ToInt32(rdr["Id"]);
                            section.SectionName = rdr["Name"].ToString();
                            section.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            section.CreatedBy = rdr["CreatedBy"].ToString();

                            sections.Add(section);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return sections;
            }
        }

        // Section Deletion 
        public int RemoveSection(int sectionId)
        {
            int result = 0;            
            string query = $@"Update Sections SET IsActive=0 Where Id=@id ";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", sectionId);
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

        // Section Unique check. Response: bool
        public bool CheckSection(string sectionName)
        {
            string query = "select * from Sections where Name=N'" + sectionName + "'";
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

        /*
	    Get count of sections assigned in forecast	    
	    Response: integer count
	    */
        public int GetSectionCountWithEmployeeAsignment(int sectionId)
        {
            string query = "select * from EmployeesAssignments where SectionId=" + sectionId;
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

        // Get section name for creating the validation strings.
        public Section GetSectionBySectionId(int sectionId)
        {
            Section section = null;
            string query = "select * from Sections where Id = "+sectionId;
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
                            section = new Section();
                            section.SectionName = rdr["Name"].ToString();
                            section.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    section = null;
                }

                return section;
            }
        }

        //check if the section is unique or not
        public int GetSectionIdByName(string sectionName)
        {
            string query = "select Id,Name from Sections where Name=N'" + sectionName + "'";
            int sectionId = 0;

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
                            sectionId = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return sectionId;
            }
        }
    }
}
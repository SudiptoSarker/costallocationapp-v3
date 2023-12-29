using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;


namespace CostAllocationApp.DAL
{
    public class ExplanationDAL : DbContext
    {
        public int CreateExplanation(Explanation explanation)
        {
            int result = 0;
            string query = $@"insert into Explanations(Name,CreatedBy,CreatedDate,IsActive) values(@explanationName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@explanationName", explanation.ExplanationName);
                cmd.Parameters.AddWithValue("@createdBy", explanation.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", explanation.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", explanation.IsActive);
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
        public int UpdateExplanations(Explanation explanation)
        {
            int result = 0;
            string query = "";
            query = query + "UPDATE Explanations ";
            query = query + "SET Name = N'" + explanation.ExplanationName + "',UpdatedBy=N'" + explanation.UpdatedBy + "',UpdatedDate='" + explanation.UpdatedDate + "',IsActive=1";
            query = query + "WHERE Id = " + explanation.Id + " ";

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
        public List<Explanation> GetAllExplanations()
        {
            List<Explanation> explanations = new List<Explanation>();
            string query = "";
            query = "SELECT * FROM Explanations WHERE IsActive=1 ";
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
                            Explanation explanation = new Explanation();
                            explanation.Id = Convert.ToInt32(rdr["Id"]);
                            explanation.ExplanationName = rdr["Name"].ToString();
                            explanation.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            explanation.CreatedBy = rdr["CreatedBy"].ToString();

                            explanations.Add(explanation);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return explanations;
            }
        }
        public Explanation GetSingleExplanationByExplanationId(int id)
        {
            Explanation explanation = new Explanation();
            string query = "";
            query = "SELECT * FROM Explanations WHERE IsActive=1 and id="+id;
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
                            
                            explanation.Id = Convert.ToInt32(rdr["Id"]);
                            explanation.ExplanationName = rdr["Name"].ToString();
                            explanation.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            explanation.CreatedBy = rdr["CreatedBy"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return explanation;
            }
        }
        public int RemoveExplanations(int explanationIds)
        {
            int result = 0;            
            string query = $@"DELETE FROM Explanations WHERE id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", explanationIds);
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

        public int GetExplanationCountWithEmployeeAsignment(int explanationId)
        {
            string query = "select * from EmployeesAssignments where ExplanationId=" + explanationId;
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

        public Explanation GetExplanationByExplanationId(int explanationId)
        {
            Explanation explanation = null;
            string query = "select * from Explanations where Id = " + explanationId;
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
                            explanation = new Explanation();
                            explanation.ExplanationName = rdr["Name"].ToString();
                            explanation.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    explanation = null;
                }

                return explanation;
            }
        }
        public int GetExplanationIdByName(string inchargeName)
        {
            int inchargeId = 0;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(inchargeName))
            {
                where += $" Name =N'{inchargeName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM Explanations
                            where {where}";

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
                                inchargeId = Convert.ToInt32(rdr["Id"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Response.Write(ex);
                        HttpContext.Current.Response.End();
                    }
                }

            }

            return inchargeId;
        }
    }
}
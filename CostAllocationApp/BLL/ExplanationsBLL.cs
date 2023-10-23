using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.BLL
{
    public class ExplanationsBLL
    {
        ExplanationDAL explanationDAL = null;
        public ExplanationsBLL()
        {
            explanationDAL = new ExplanationDAL();
        }
        public int CreateExplanation(Explanation explanation)
        {
            return explanationDAL.CreateExplanation(explanation);
        }
        public int UpdateExplanations(Explanation explanation)
        {
            return explanationDAL.UpdateExplanations(explanation);
        }
        public List<Explanation> GetAllExplanations()
        {
            return explanationDAL.GetAllExplanations();
        }
        public int RemoveExplanations(int explanationIds)
        {
            return explanationDAL.RemoveExplanations(explanationIds);
        }
        public Explanation GetSingleExplanationByExplanationId(int id)
        {
            return explanationDAL.GetSingleExplanationByExplanationId(id);
        }
        public int GetExplanationCountWithEmployeeAsignment(int explanationId)
        {
            return explanationDAL.GetExplanationCountWithEmployeeAsignment(explanationId);
        }
        public Explanation GetExplanationByExplanationId(int explanationId)
        {
            return explanationDAL.GetExplanationByExplanationId(explanationId);
        }
        public int RetrieveExplanationIdByExplanationName(string explanationName, string userName)
        {
            Explanation explanation = new Explanation();
            int explanationId = 0;

            if (!string.IsNullOrEmpty(explanationName))
            {
                explanationId = explanationDAL.GetExplanationIdByName(explanationName);

                if (explanationId > 0)
                {
                    return explanationId;
                }
                else
                {
                    explanation.CreatedBy = userName;
                    explanation.CreatedDate = DateTime.Now;
                    explanation.IsActive = true;
                    explanation.ExplanationName = explanationName;

                    int result = explanationDAL.CreateExplanation(explanation);
                    explanationId = explanationDAL.GetExplanationIdByName(explanationName);
                    return explanationId;
                }
            }
            else
            {
                return explanationId;
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class SectionBLL
    {
        SectionDAL sectionDAL = null;
        public SectionBLL()
        {
            sectionDAL = new SectionDAL();
        }

        // Create New Section
        public int CreateSection(Section section)
        {            
            return sectionDAL.CreateSection(section);
        }

        // Update Section
        public int UpdateSection(Section section)
        {            
            return sectionDAL.UpdateSection(section);
        }

        // Get Section List
        public List<Section> GetAllSections()
        {
            return sectionDAL.GetAllSections();
        }

        // Section Deletion 
        public int RemoveSection(int sectionId)
        {            
            return sectionDAL.RemoveSection(sectionId);
        }

        // Section Unique check. Response: bool
        public bool CheckSection(string sectionName)
        {
            return sectionDAL.CheckSection(sectionName);
        }

        /*
	    Get count of sections assigned in forecast	    
	    Response: integer count
	    */
        public int GetSectionCountWithEmployeeAsignment(int sectionId)
        {
            return sectionDAL.GetSectionCountWithEmployeeAsignment(sectionId);
        }

        // Get section name for creating the validation strings.
        public Section GetSectionBySectionId(int sectionId)
        {
            return sectionDAL.GetSectionBySectionId(sectionId);
        }

        /*
        if the section is exists then return sectionId
        if not exists then create new section and return section id
        */
        public int RetrieveSectionIdBySectionName(string sectionName,string userName) {
            Section section = new Section();
            SectionBLL _sectionBll = new SectionBLL();
            int sectionId = 0;

            if (!string.IsNullOrEmpty(sectionName))
            {
                //check if the section is unique or not
                sectionId = sectionDAL.GetSectionIdByName(sectionName);
               

                if (sectionId > 0)
                {
                    return sectionId;
                }
                else
                {
                    section.CreatedBy = userName;
                    section.CreatedDate = DateTime.Now;
                    section.IsActive = true;
                    section.SectionName = sectionName;

                    int result = sectionDAL.CreateSection(section);
                    sectionId = sectionDAL.GetSectionIdByName(sectionName);
                    return sectionId;
                }
            }
            else
            {
                return sectionId;
            }
                     
        }
    }
}
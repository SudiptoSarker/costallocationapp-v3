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

        public int CreateSection(Section section)
        {
            return sectionDAL.CreateSection(section);
        }
        public int UpdateSection(Section section)
        {
            return sectionDAL.UpdateSection(section);
        }
        public List<Section> GetAllSections()
        {
            return sectionDAL.GetAllSections();
        }
        public int RemoveSection(int sectionId)
        {
            return sectionDAL.RemoveSection(sectionId);
        }

        public bool CheckSection(string sectionName)
        {
            return sectionDAL.CheckSection(sectionName);
        }

        public int GetSectionCountWithEmployeeAsignment(int sectionId)
        {
            return sectionDAL.GetSectionCountWithEmployeeAsignment(sectionId);
        }
        public Section GetSectionBySectionId(int sectionId)
        {
            return sectionDAL.GetSectionBySectionId(sectionId);
        }

        public int RetrieveSectionIdBySectionName(string sectionName,string userName) {
            Section section = new Section();
            SectionBLL _sectionBll = new SectionBLL();
            int sectionId = 0;

            if (!string.IsNullOrEmpty(sectionName))
            {
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
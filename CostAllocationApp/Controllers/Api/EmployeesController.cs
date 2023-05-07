using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;
using CostAllocationApp.Dtos;
using CostAllocationApp.ViewModels;


namespace CostAllocationApp.Controllers.Api
{
    public class EmployeesController : ApiController
    {
        // GET: Employees
        EmployeeAssignmentBLL employeeAssignmentBLL = null;
        
        public EmployeesController()
        {
            employeeAssignmentBLL = new EmployeeAssignmentBLL();
        }

        [HttpPost]
        public IHttpActionResult CreateAssignment(EmployeeAssignmentDTO employeeAssignmentDTO)
        {
            var session = System.Web.HttpContext.Current.Session;

            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

            int tempValue = 0;
            decimal tempUnitPrice = 0;

            #region validation of inputs
            //if (!String.IsNullOrEmpty(employeeAssignmentDTO.EmployeeId))
            //{
            //    var checkResult = employeeAssignmentBLL.CheckEmployeeName(employeeAssignmentDTO.EmployeeName.Trim());
            //    if (checkResult && employeeAssignmentDTO.SubCode == 1)
            //    {
            //        return BadRequest("Employee Already Exists");
            //    }
            //    else
            //    {
            //        employeeAssignment.EmployeeName = employeeAssignmentDTO.EmployeeName.Trim();
            //    }
                
            //}
            //else
            //{
            //    return BadRequest("Invalid Employee Name");
            //}
            if (String.IsNullOrEmpty(employeeAssignmentDTO.EmployeeId))
            {
                return BadRequest("Invalid Employee Name");
            }
            else
            {
                employeeAssignment.EmployeeId = employeeAssignmentDTO.EmployeeId;
            }
            if (String.IsNullOrEmpty(employeeAssignmentDTO.Year))
            {
                return BadRequest("Invalid Year");
            }
            else
            {
                employeeAssignment.Year = employeeAssignmentDTO.Year;
            }

            if (string.IsNullOrEmpty(employeeAssignmentDTO.SectionId))
            {
                employeeAssignment.SectionId = 0;
            }
            else
            {
                employeeAssignment.SectionId = Convert.ToInt32(employeeAssignmentDTO.SectionId);
            }
            //if (int.TryParse(employeeAssignmentDTO.SectionId, out tempValue))
            //{
            //    //if (tempValue <= 0)
            //    //{
            //    //    return BadRequest("Invalid Section Id");

            //    //}
            //    employeeAssignment.SectionId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Section Id");
            //}
            if (string.IsNullOrEmpty(employeeAssignmentDTO.DepartmentId))
            {
                employeeAssignment.DepartmentId = 0;
            }
            else
            {
                employeeAssignment.DepartmentId = Convert.ToInt32(employeeAssignmentDTO.DepartmentId);
            }
            //if (int.TryParse(employeeAssignmentDTO.DepartmentId, out tempValue))
            //{
            //    //if (tempValue <= 0)
            //    //{

            //    //    return BadRequest("Invalid Department Id");
            //    //}
            //    employeeAssignment.DepartmentId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Department Id");
            //}
            if (string.IsNullOrEmpty(employeeAssignmentDTO.InchargeId))
            {
                employeeAssignment.InchargeId = 0;
            }
            else
            {
                employeeAssignment.InchargeId = Convert.ToInt32(employeeAssignmentDTO.InchargeId);
            }
            //if (int.TryParse(employeeAssignmentDTO.InchargeId, out tempValue))
            //{
            //    //if (tempValue <= 0)
            //    //{
            //    //    return BadRequest("Invalid Incharge Id");

            //    //}
            //    employeeAssignment.InchargeId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Incharge Id");
            //}
            if (string.IsNullOrEmpty(employeeAssignmentDTO.RoleId))
            {
                employeeAssignment.RoleId = 0;
            }
            else
            {
                employeeAssignment.RoleId = Convert.ToInt32(employeeAssignmentDTO.RoleId);
            }
            //if (int.TryParse(employeeAssignmentDTO.RoleId, out tempValue))
            //{
            //    //if (tempValue <= 0)
            //    //{
            //    //    return BadRequest("Invalid Role Id");

            //    //}
            //    employeeAssignment.RoleId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Role Id");
            //}
            if (String.IsNullOrEmpty(employeeAssignmentDTO.ExplanationId))
            {
                employeeAssignment.ExplanationId=null;
            }
            else
            {
                if (int.TryParse(employeeAssignmentDTO.ExplanationId, out tempValue))
                {
                    //if (tempValue <= 0)
                    //{

                    //    return BadRequest("Invalid Explanation Id");
                    //}
                    employeeAssignment.ExplanationId = tempValue.ToString();
                }
                else
                {
                    return BadRequest("Invalid Explanation Id");
                }
            }

            if (string.IsNullOrEmpty(employeeAssignmentDTO.CompanyId))
            {
                employeeAssignment.CompanyId = 0;
            }
            else
            {
                employeeAssignment.CompanyId = Convert.ToInt32(employeeAssignmentDTO.CompanyId);
            }
            //if (int.TryParse(employeeAssignmentDTO.CompanyId, out tempValue))
            //{
            //    if (tempValue <= 0)
            //    {
            //        return BadRequest("Invalid Company Id");

            //    }
            //    employeeAssignment.CompanyId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Company Id");
            //}
            if (string.IsNullOrEmpty(employeeAssignmentDTO.UnitPrice))
            {
                employeeAssignment.UnitPrice = 0;
            }
            else
            {
                employeeAssignment.UnitPrice = Convert.ToDecimal(employeeAssignmentDTO.UnitPrice);
            }
            //if (decimal.TryParse(employeeAssignmentDTO.UnitPrice, out tempUnitPrice))
            //{
            //    if (tempValue < 0)
            //    {
            //        return BadRequest("Invalid Unit Price");

            //    }
            //    employeeAssignment.UnitPrice = tempUnitPrice;
            //}

            //else
            //{
            //    return BadRequest("Invalid Unit Price");
            //}
            if (string.IsNullOrEmpty(employeeAssignmentDTO.GradeId))
            {
                employeeAssignment.GradeId = 0;
            }
            else
            {
                employeeAssignment.GradeId = Convert.ToInt32(employeeAssignmentDTO.GradeId);
            }
            //if (int.TryParse(employeeAssignmentDTO.GradeId, out tempValue))
            //{
            //    if (tempValue <= 0)
            //    {
            //        return BadRequest("Invalid Grade Id");

            //    }
            //    employeeAssignment.GradeId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Grade Id");
            //}

            if (String.IsNullOrEmpty(employeeAssignmentDTO.Remarks))
            {
                employeeAssignmentDTO.Remarks = "";
            }
            #endregion

            //employeeAssignment.ExplanationId = employeeAssignmentDTO.ExplanationId;
            employeeAssignment.CreatedBy = session["userName"].ToString();
            employeeAssignment.CreatedDate = DateTime.Now;
            employeeAssignment.IsActive = "1";
            employeeAssignment.Remarks = employeeAssignmentDTO.Remarks.Trim();
            employeeAssignment.SubCode = employeeAssignmentDTO.SubCode;


            int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
            if (result > 0)
            {
                return Ok("Data Saved Successfully!");
            }
            else
            {
                return BadRequest("Something Went Wrong!!!");
            }
        }


        [HttpPut]
        public IHttpActionResult UpdateAssignment([FromBody]  EmployeeAssignmentDTO employeeAssignmentDTO)
        {
            var session = System.Web.HttpContext.Current.Session;
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

            int tempValue = 0;
            decimal tempUnitPrice = 0;

            #region validation of inputs
            if (int.TryParse(employeeAssignmentDTO.SectionId, out tempValue))
            {
                if (tempValue <= 0)
                {
                    return BadRequest("Invalid Section Id");

                }
                employeeAssignment.SectionId = tempValue;
            }
            else
            {
                return BadRequest("Invalid Section Id");
            }
            if (int.TryParse(employeeAssignmentDTO.DepartmentId, out tempValue))
            {
                if (tempValue <= 0)
                {

                    return BadRequest("Invalid Department Id");
                }
                employeeAssignment.DepartmentId = tempValue;
            }
            else
            {
                return BadRequest("Invalid Department Id");
            }
            if (int.TryParse(employeeAssignmentDTO.InchargeId, out tempValue))
            {
                if (tempValue <= 0)
                {
                    return BadRequest("Invalid Incharge Id");

                }
                employeeAssignment.InchargeId = tempValue;
            }
            else
            {
                return BadRequest("Invalid Incharge Id");
            }
            if (int.TryParse(employeeAssignmentDTO.RoleId, out tempValue))
            {
                if (tempValue <= 0)
                {
                    return BadRequest("Invalid Role Id");

                }
                employeeAssignment.RoleId = tempValue;
            }
            else
            {
                return BadRequest("Invalid Role Id");
            }
            if (String.IsNullOrEmpty(employeeAssignmentDTO.ExplanationId))
            {
                employeeAssignment.ExplanationId = null;
            }
            else
            {
                if (int.TryParse(employeeAssignmentDTO.ExplanationId, out tempValue))
                {
                    if (tempValue <= 0)
                    {

                        return BadRequest("Invalid Explanation Id");
                    }
                    employeeAssignment.ExplanationId = tempValue.ToString();
                }
                else
                {
                    return BadRequest("Invalid Explanation Id");
                }
            }


            if (int.TryParse(employeeAssignmentDTO.CompanyId, out tempValue))
            {
                if (tempValue <= 0)
                {
                    return BadRequest("Invalid Company Id");

                }
                employeeAssignment.CompanyId = tempValue;
            }
            else
            {
                return BadRequest("Invalid Company Id");
            }
            if (decimal.TryParse(employeeAssignmentDTO.UnitPrice, out tempUnitPrice))
            {
                if (tempValue < 0)
                {
                    return BadRequest("Invalid Unit Price");

                }
                employeeAssignment.UnitPrice = tempUnitPrice;
            }
            else
            {
                return BadRequest("Invalid Unit Price");
            }
            if (int.TryParse(employeeAssignmentDTO.GradeId, out tempValue))
            {
                if (tempValue <= 0)
                {
                    return BadRequest("Invalid Grade Id");

                }
                employeeAssignment.GradeId = tempValue;
            }
            else
            {
                return BadRequest("Invalid Grade Id");
            }

            if (String.IsNullOrEmpty(employeeAssignmentDTO.Remarks))
            {
                employeeAssignmentDTO.Remarks = "";
            }
            #endregion
            employeeAssignment.ExplanationId = employeeAssignmentDTO.ExplanationId;
            employeeAssignment.UpdatedBy = session["userName"].ToString();
            employeeAssignment.UpdatedDate = DateTime.Now;
            employeeAssignment.Id = employeeAssignmentDTO.Id;
            employeeAssignment.Remarks = employeeAssignmentDTO.Remarks.Trim();


            int result = employeeAssignmentBLL.UpdateAssignment(employeeAssignment);
            if (result > 0)
            {
                return Ok("Data Saved Successfully!");
            }
            else
            {
                return BadRequest("Something Went Wrong!!!");
            }
        }

        [HttpGet]
        public IHttpActionResult SearchAssignment(string EmployeeName="", string SectionId="", string DepartmentId="", string InchargeId="", string RoleId="", string ExplanationId="", string CompanyId="", bool Status=true)
        {

            int tempValue = 0;
            // decimal tempUnitPrice = 0;


            EmployeeAssignment employeeAssignment = new EmployeeAssignment();


            #region validation of inputs
            if (int.TryParse(SectionId, out tempValue))
            {
                if (tempValue > 0)
                {
                    employeeAssignment.SectionId = tempValue;

                }
            }
            if (int.TryParse(DepartmentId, out tempValue))
            {
                if (tempValue > 0)
                {

                    employeeAssignment.DepartmentId = tempValue;
                }
            }
            if (int.TryParse(InchargeId, out tempValue))
            {
                if (tempValue > 0)
                {
                    employeeAssignment.InchargeId = tempValue;

                }
            }
            if (int.TryParse(RoleId, out tempValue))
            {
                if (tempValue > 0)
                {
                    employeeAssignment.RoleId = tempValue;

                }
            }
            //if (int.TryParse(ExplanationId, out tempValue))
            //{
            //    if (tempValue > 0)
            //    {

            //        employeeAssignment.ExplanationId = tempValue;
            //    }

            //}

            if (int.TryParse(CompanyId, out tempValue))
            {
                if (tempValue > 0)
                {
                    employeeAssignment.CompanyId = tempValue;

                }
            }
            //if (decimal.TryParse(employeeAssignmentDTO.UnitPrice, out tempUnitPrice))
            //{
            //    if (tempValue < 0)
            //    {
            //        return BadRequest("Invalid Unit Price");

            //    }
            //    employeeAssignment.UnitPrice = tempUnitPrice;
            //}
            //else
            //{
            //    return BadRequest("Invalid Unit Price");
            //}
            //if (int.TryParse(employeeAssignmentDTO.GradeId, out tempValue))
            //{
            //    if (tempValue <= 0)
            //    {
            //        return BadRequest("Invalid Grade Id");

            //    }
            //    employeeAssignment.GradeId = tempValue;
            //}
            //else
            //{
            //    return BadRequest("Invalid Grade Id");
            //}
            #endregion

            List<EmployeeAssignmentViewModel> employeeAssignmentViewModels = employeeAssignmentBLL.SearchAssignment(employeeAssignment);

            if (employeeAssignmentViewModels.Count > 0)
            {
                return Ok(employeeAssignmentViewModels);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut]
        public IHttpActionResult RemoveAssignment(int id)
        {
            int result =  employeeAssignmentBLL.RemoveAssignment(id);
            if (result>0)
            {
                return Ok("Data Removed Successfully");
            }
            else
            {
                return BadRequest("Something Went Wrong");
            }
        }




    }
}
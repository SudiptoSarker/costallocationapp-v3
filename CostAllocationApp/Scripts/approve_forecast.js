var globalSearchObject = '';
var globalPreviousValue = '0.0';
var globalPreviousId = '';
var jss;
var globalX = 0;
var globalY = 0;
var newRowCount = 1;
var beforeChangedValue = 0;
var jssUpdatedData = [];
var jssInsertedData = [];
var allEmployeeName = [];
var allEmployeeName1 = [];
var cellwiseColorCode = [];

function LoaderShow() {
    $("#jspreadsheet").css("display", "none");
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#jspreadsheet").css("display", "block");
    $("#loading").css("display", "none");
}
function LoaderShowJexcel() {
    $("#loading").css("display", "block");
    $("#jspreadsheet").hide();  
    //$("#head_total").css("display", "none");
    
}
function LoaderHideJexcel(){
    $("#jspreadsheet").show();  
    //$("#head_total").css("display", "table !important");
    $("#loading").css("display", "none");
}

function ColumnOrder(columnNumber, orderBy) {    
   
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_p_asc').css('background-color', 'lightsteelblue');
        $('#search_p_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_p_asc').css('background-color', 'grey');
        $('#search_p_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Section(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_section_asc').css('background-color', 'lightsteelblue');
        $('#search_section_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_section_asc').css('background-color', 'grey');
        $('#search_section_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Department(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_department_asc').css('background-color', 'lightsteelblue');
        $('#search_department_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_department_asc').css('background-color', 'grey');
        $('#search_department_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_InCharge(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_incharge_asc').css('background-color', 'lightsteelblue');
        $('#search_incharge_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_incharge_asc').css('background-color', 'grey');
        $('#search_incharge_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Role(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_role_asc').css('background-color', 'lightsteelblue');
        $('#search_role_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_role_asc').css('background-color', 'grey');
        $('#search_role_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Explanation(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_explanation_asc').css('background-color', 'lightsteelblue');
        $('#search_explanation_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_explanation_asc').css('background-color', 'grey');
        $('#search_explanation_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Company(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_company_asc').css('background-color', 'lightsteelblue');
        $('#search_company_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_company_asc').css('background-color', 'grey');
        $('#search_company_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Grade(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_grade_asc').css('background-color', 'lightsteelblue');
        $('#search_grade_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_grade_asc').css('background-color', 'grey');
        $('#search_grade_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_UnitPrice(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_unit_price_asc').css('background-color', 'lightsteelblue');
        $('#search_unit_price_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_unit_price_asc').css('background-color', 'grey');
        $('#search_unit_price_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function onCancel() {
    LoaderShow();
    LoadForecastData()
}

var expanded = false;
$(function () {
    var chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#save_notifications').append(`<li>${name} ${message}</li>`);
    };

});




$(document).ready(function () {
    GetAllForecastYears();
    var year = $('#hidForecastYear').val();
    if (year.toLowerCase() != "imprt") {
        $("#jspreadsheet").hide();  
    }
    var count = 1;
    $('#employee_list').select2();

    $('#saved_approved_data').on('click', function () {
        var assignmentYear = $('#assignment_year_list').val();
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('Select valid year!!!');
            return false;
        }     
        
        // LoaderShowJexcel();        
        LoaderShow();                

        $.ajax({
            url: `/api/utilities/UpdateApprovedData`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: "assignmentYear=" + assignmentYear,
            success: function (data) {
                if(data==1){
                    alert("Operation Success.")                    
                    ShowForecastResults(assignmentYear);
                }else{
                    LoaderHide();
                    alert("There is no approved data to save!")
                }
            }
        });   
    });

    $('#approve_forecast_data').on('click', function () {
        var approveAssignmentId = $("#hidSelectedRow_AssignementId").val();
        var isDeleted = $("#hidIsRowDeleted").val();
        if (approveAssignmentId =='' || typeof approveAssignmentId === "undefined"){
            alert("There is no data to approve!");
        }else{
            var data_Info = {
                Id: approveAssignmentId
            };
            $.ajax({
                url: `/api/utilities/ApprovedForecastData`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: "assignementId=" + approveAssignmentId+"&isDeletedRow="+isDeleted,
                success: function (data) {
                    if(data==1){
                        var rowNumber = $("#hidSelectedRowNumber").val();
                        if(isDeleted =='true'){
                            SetRowColor_AfterApproved(parseInt(rowNumber)+1);
                        }else{
                            SetRowColor_ForDeletedRow(parseInt(rowNumber)+1);
                        }
                        $("#hidSelectedRow_AssignementId").val("");
                        $("#hidIsRowDeleted").val("");
                        alert("Operation Success.")
                    }
                    else{
                        alert("There is no data to approved!")
                    }
                    //_retriveddata = data;
                }
            });       
        }       
    });



    $(document).on('change', '#section_search', function () {

        var sectionId = $(this).val();

        $.getJSON(`/api/utilities/DepartmentsBySection/${sectionId}`)
            .done(function (data) {
                $('#department_search').empty();
                $('#department_search').append(`<option value=''>Select Department</option>`);
                $.each(data, function (key, item) {
                    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
                });
            });
    });
    
    $(document).on('click', '#assignment_year_data ', function () {    
        var assignmentYear = $('#assignment_year_list').val();
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('Select valid year!!!');
            return false;
        }     
        
        LoaderShowJexcel();
            
        setTimeout(function () {                                 
            ShowForecastResults(assignmentYear);
        }, 3000);

        
    });
    $(document).on('click', '#cancel_forecast_history ', function () {    
        var assignmentYear = $('#assignment_year_list').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }
        LoaderShowJexcel();            
        setTimeout(function () {                               
            ShowForecastResults(assignmentYear);
        }, 3000);
        
    });
    
    $(document).ajaxComplete(function(){
        LoaderHideJexcel();
    });

});

function ShowForecastResults(year) {
    var employeeName = $('#name_search').val();
    employeeName = "";
    var sectionId = $('#section_multi_search').val();
    sectionId = "";
    var inchargeId = $('#incharge_multi_search').val();
    inchargeId = "";
    var roleId = $('#role_multi_search').val();
    roleId = "";
    var companyId = $('#company_multi_search').val();
    companyId = "";
    var departmentId = $('#dept_multi_search').val();
    departmentId = "";
    var explanationId = $('#explanation_multi_search').val();
    explanationId = "";
    if (year == '' || year == undefined) {
        alert('select year');
        return false;
    }

    $('#cancel_forecast').css('display', 'inline-block');
    $('#save_forecast').css('display', 'inline-block');

    var sectionCheck = [];
    var departmentCheck = [];
    var inchargeCheck = [];
    var roleCheck = [];
    var explanationCheck = [];
    var companyCheck = [];

    var data_info = {
        employeeName: employeeName,
        sectionId: sectionId,
        departmentId: departmentId,
        inchargeId: inchargeId,
        roleId: roleId,
        explanationId: explanationId,
        companyId: companyId,
        status: '', year: year, timeStampId: ''
    };
    globalSearchObject = data_info;

    var _retriveddata = [];
    $.ajax({
        url: `/api/utilities/SearchForApprovalEmployee`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            _retriveddata = data;
        }
    });
    
    var sectionsForJexcel = [];
    var departmentsForJexcel = [];
    var inchargesForJexcel = [];
    var rolesForJexcel = [];
    var explanationsForJexcel = [];
    var companiesForJexcel = [];
    var gradesForJexcel = [];

    $.ajax({
        url: `/api/Sections`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                sectionsForJexcel.push({ id: value.Id, name: value.SectionName });
            });
        }
    });
    $.ajax({
        url: `/api/Departments`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {

            $.each(data, (index, value) => {
                departmentsForJexcel.push({ id: value.Id, name: value.DepartmentName });
            });
        }
    });

    $.ajax({
        url: `/api/InCharges`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                inchargesForJexcel.push({ id: value.Id, name: value.InChargeName });
            });
        }
    });
    $.ajax({
        url: `/api/Roles`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                rolesForJexcel.push({ id: value.Id, name: value.RoleName });
            });
        }
    });
    $.ajax({
        url: `/api/Explanations`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                explanationsForJexcel.push({ id: value.Id, name: value.ExplanationName });
            });
        }
    });
    $.ajax({
        url: `/api/Companies`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                companiesForJexcel.push({ id: value.Id, name: value.CompanyName });
            });
        }
    });
    $.ajax({
        url: `/api/Salaries`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                gradesForJexcel.push({ id: value.Id, name: value.SalaryGrade });
            });
        }
    });
    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }
    var w = window.innerWidth;
    var h = window.innerHeight;
    
    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        filters: true,
        tableOverflow: true,
        freezeColumns: 3,
        defaultColWidth: 50,
        // tableWidth: w - 500 + "px",
        // tableHeight: (h - 300) + "px",
        tableWidth: w-280+ "px",
        tableHeight: (h-150) + "px",
        
        columns: [
            { title: "Id", type: 'hidden', name: "Id" },
            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
            { title: "Remarks", type: "text", name: "Remarks", width: 60 },
            { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },
            { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },
            { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100 },
            { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60 },
            { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
            { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100 },
            { 
                title: "グレード(Grade)", 
                type: "dropdown", 
                source: gradesForJexcel, 
                name: "GradeId", 
                width: 60 
            },
            { title: "単価(Unit Price)", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },
            {
                title: "10月",
                type: "decimal",
                name: "OctPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "11月",
                type: "decimal",
                name: "NovPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "12月",
                type: "decimal",
                name: "DecPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "1月",
                type: "decimal",
                name: "JanPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "2月",
                type: "decimal",
                name: "FebPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "3月",
                type: "decimal",
                name: "MarPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "4月",
                type: "decimal",
                name: "AprPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "5月",
                type: "decimal",
                name: "MayPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "6月",
                type: "decimal",
                name: "JunPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "7月",
                type: "decimal",
                name: "JulPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "8月",
                type: "decimal",
                name: "AugPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "9月",
                type: "decimal",
                name: "SepPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "10月",
                type: "number",
                //readOnly: true,
                mask: "#,##0",
                name: "OctTotal",
                width: 60
            },
            {
                title: "11月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "NovTotal"
            },
            {
                title: "12月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "DecTotal"
            },
            {
                title: "1月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "JanTotal"
            },
            {
                title: "2月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "FebTotal"
            },
            {
                title: "3月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "MarTotal"
            },
            {
                title: "4月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "AprTotal"
            },
            {
                title: "5月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "MayTotal"
            },
            {
                title: "6月",
                type: "decimal",
                // readOnly: true,
                mask: "#,##0",
                name: "JunTotal"
            },
            {
                title: "7月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "JulTotal"
            },
            {
                title: "8月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "AugTotal"
            },
            {
                title: "9月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "SepTotal"
            },
            { title: "Employee Id", type: 'hidden', name: "EmployeeId" },
            { title: "BCYR", type: 'hidden', name: "BCYR" },
            { title: "BCYRCell", type: 'hidden', name: "BCYRCell" },
            { title: "BCYRApproved", type: 'hidden', name: "BCYRApproved" },
            { title: "IsActive", type: 'hidden', name: "IsActive" },
        ],
        minDimensions: [6, 10],
        columnSorting: true,
        onbeforechange: function (instance, cell, x, y, value) {

            //alert(value);
            if (x == 11) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 12) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 13) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 14) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 15) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 16) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 17) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 18) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 19) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 20) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 21) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
            if (x == 22) {
                beforeChangedValue = jss.getValueFromCoords(x, y);
            }
        },
        onchange: function (instance, cell, x, y, value) {
            //debugger;
            var checkId = jss.getValueFromCoords(0, y);
            var employeeId = jss.getValueFromCoords(35, y);

            if (checkId == null || checkId == '' || checkId == undefined) {

                var retrivedData = retrivedObject(jss.getRowData(y));
                retrivedData.assignmentId = "new-" + newRowCount;

                jssInsertedData.push(retrivedData);
                newRowCount++;
                jss.setValueFromCoords(0, y, retrivedData.assignmentId, false);
                jss.setValueFromCoords(23, y, `=K${parseInt(y) + 1}*L${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(24, y, `=K${parseInt(y) + 1}*M${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(25, y, `=K${parseInt(y) + 1}*N${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(26, y, `=K${parseInt(y) + 1}*O${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(27, y, `=K${parseInt(y) + 1}*P${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(28, y, `=K${parseInt(y) + 1}*Q${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(29, y, `=K${parseInt(y) + 1}*R${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(30, y, `=K${parseInt(y) + 1}*S${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(31, y, `=K${parseInt(y) + 1}*T${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(32, y, `=K${parseInt(y) + 1}*U${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(33, y, `=K${parseInt(y) + 1}*V${parseInt(y) + 1}`, false);
                jss.setValueFromCoords(34, y, `=K${parseInt(y) + 1}*W${parseInt(y) + 1}`, false);
            }
            else {
                var retrivedData = retrivedObject(jss.getRowData(y));
                if (retrivedData.assignmentId.toString().includes('new')) {
                    updateArrayForInsert(jssInsertedData, retrivedData);
                }
                else {
                    var dataCheck = jssUpdatedData.filter(d => d.assignmentId == retrivedData.assignmentId);
                    if (x == 2) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId+'_'+x);
                    }
                    if (x == 3) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 4) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 5) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 6) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 7) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 8) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 9) {
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 11) {
                        var octSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                octSum += parseFloat(value.childNodes[12].innerText);
                            }

                        });

                        if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
                            octSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);

                        }
                        else {

                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }

                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);

                    }
                    if (x == 12) {
                        var novSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                novSum += parseFloat(value.childNodes[13].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
                            novSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 13) {
                        var decSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                decSum += parseFloat(value.childNodes[14].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
                            decSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 14) {
                        var janSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                janSum += parseFloat(value.childNodes[15].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
                            janSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 15) {
                        var febSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                febSum += parseFloat(value.childNodes[16].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
                            febSum = 1;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 16) {
                        var marSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                marSum += parseFloat(value.childNodes[17].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
                            marSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 17) {
                        var aprSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                aprSum += parseFloat(value.childNodes[18].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
                            aprSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 18) {
                        var maySum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                maySum += parseFloat(value.childNodes[19].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 19) {
                        var junSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                junSum += parseFloat(value.childNodes[20].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
                            junSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 20) {
                        var julSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                julSum += parseFloat(value.childNodes[21].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 21) {
                        var augSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                augSum += parseFloat(value.childNodes[22].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
                            augSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    if (x == 22) {
                        var sepSum = 0;
                        $.each(jss.rows, (index, value) => {
                            if (value.childNodes[36].innerText == employeeId.toString()) {
                                sepSum += parseFloat(value.childNodes[23].innerText);
                            }

                        });
                        if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
                            sepSum = 0;
                            alert('Input not valid');
                            jss.setValueFromCoords(x, y, beforeChangedValue, false);
                        }
                        else {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {

                                updateArray(jssUpdatedData, retrivedData);

                            }
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                }

            }

        },
        oninsertrow: newRowInserted,
        ondeleterow: deleted,
        contextMenu: function (obj, x, y, e) {            
            return "";
        },
        onselection: selectionActive,   
        // onfocus: focus,     
    });

    $("#saved_approved_data").css("display", "block");
    $("#approve_forecast_data").css("display", "block");

    jss.deleteColumn(40, 15);
    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    jexcelHeadTdEmployeeName.addClass('arrow-down');
    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelFirstHeaderRow.css('top', '0px');
    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelSecondHeaderRow.css('top', '20px');

    //employee name column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {       
        $('.search_p').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_p').fadeIn("slow");
    });

    //section column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)').on('click', function () {               
        $('.search_section').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_section').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)').on('click', function () {               
        $('.search_department').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_department').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)').on('click', function () {               
        $('.search_incharge').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_incharge').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)').on('click', function () {               
        $('.search_role').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_role').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)').on('click', function () {               
        $('.search_explanation').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_explanation').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)').on('click', function () {               
        $('.search_company').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_company').fadeIn("slow");
    });
    //grade column sorting
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)').on('click', function () {               
        $('.search_grade').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_grade').fadeIn("slow");
    });
        //unit price column sorting
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)').on('click', function () {               
        $('.search_unit_price').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_unit_price').fadeIn("slow");
    });
    // $(".jexcel_content").css("max-height",window.innerHeight+200+"px !important");    
    // $("#head_total").css("width",w-300);

    var allRows = jss.getData();
    var count = 1;
    $.each(allRows, function (index,value) {
        if (value['36'] == true && value['38'] == true) {
            SetRowColor_ApprovedRow(count);
        }else if (value['36'] == true && value['38'] == false) {
            SetRowColor(count);
        }
        else {
            var columnInfo = value['37'];
            var infoArray = columnInfo.split(',');
            $.each(infoArray, function (nextedIndex, nestedValue) {              
                if (parseInt(nestedValue) == 1) {
                    jss.setStyle("B" + count, "background-color", "yellow");
                    jss.setStyle("B" + count, "color", "red");
                }

                if (parseInt(nestedValue) == 2) {
                    jss.setStyle("C" + count, "background-color", "yellow");
                    jss.setStyle("C" + count, "color", "red");
                }

                if (parseInt(nestedValue) == 3) {
                    jss.setStyle("D" + count, "background-color", "yellow");
                    jss.setStyle("D" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 4) {
                    jss.setStyle("E" + count, "background-color", "yellow");
                    jss.setStyle("E" + count, "color", "red");
                }
 
                if (parseInt(nestedValue) == 5) {
                    jss.setStyle("F" + count, "background-color", "yellow");
                    jss.setStyle("F" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 6) {
                    jss.setStyle("G" + count, "background-color", "yellow");
                    jss.setStyle("G" + count, "color", "red");
                }

                if (parseInt(nestedValue) == 7) {
                    jss.setStyle("H" + count, "background-color", "yellow");
                    jss.setStyle("H" + count, "color", "red");
                }
             
                if (parseInt(nestedValue) == 8) {
                    jss.setStyle("I" + count, "background-color", "yellow");
                    jss.setStyle("I" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 9) {
                    jss.setStyle("J" + count, "background-color", "yellow");
                    jss.setStyle("J" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 10) {
                    jss.setStyle("K" + count, "background-color", "yellow");
                    jss.setStyle("K" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 11) {
                    jss.setStyle("L" + count, "background-color", "yellow");
                    jss.setStyle("L" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 12) {
                    jss.setStyle("M" + count, "background-color", "yellow");
                    jss.setStyle("M" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 13) {
                    jss.setStyle("N" + count, "background-color", "yellow");
                    jss.setStyle("N" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 14) {
                    jss.setStyle("O" + count, "background-color", "yellow");
                    jss.setStyle("O" + count, "color", "red");
                }
             
                if (parseInt(nestedValue) == 15) {
                    jss.setStyle("P" + count, "background-color", "yellow");
                    jss.setStyle("P" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 16) {
                    jss.setStyle("Q" + count, "background-color", "yellow");
                    jss.setStyle("Q" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 17) {
                    jss.setStyle("R" + count, "background-color", "yellow");
                    jss.setStyle("R" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 18) {
                    jss.setStyle("S" + count, "background-color", "yellow");
                    jss.setStyle("S" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 19) {
                    jss.setStyle("T" + count, "background-color", "yellow");
                    jss.setStyle("T" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 20) {
                    jss.setStyle("U" + count, "background-color", "yellow");
                    jss.setStyle("U" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 21) {
                    jss.setStyle("V" + count, "background-color", "yellow");
                    jss.setStyle("V" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 22) {
                    jss.setStyle("W" + count, "background-color", "yellow");
                    jss.setStyle("W" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 23) {
                    jss.setStyle("X" + count, "background-color", "yellow");
                    jss.setStyle("X" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 24) {
                    jss.setStyle("Y" + count, "background-color", "yellow");
                    jss.setStyle("Y" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 25) {
                    jss.setStyle("Z" + count, "background-color", "yellow");
                    jss.setStyle("Z" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 26) {
                    jss.setStyle("AA" + count, "background-color", "yellow");
                    jss.setStyle("AA" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 27) {
                    jss.setStyle("AB" + count, "background-color", "yellow");
                    jss.setStyle("AB" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 28) {
                    jss.setStyle("AC" + count, "background-color", "yellow");
                    jss.setStyle("AC" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 29) {
                    jss.setStyle("AD" + count, "background-color", "yellow");
                    jss.setStyle("AD" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 30) {
                    jss.setStyle("AE" + count, "background-color", "yellow");
                    jss.setStyle("AE" + count, "color", "red");
                }
                
                if (parseInt(nestedValue) == 31) {
                    jss.setStyle("AF" + count, "background-color", "yellow");
                    jss.setStyle("AF" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 32) {
                    jss.setStyle("AG" + count, "background-color", "yellow");
                    jss.setStyle("AG" + count, "color", "red");
                }
              
                if (parseInt(nestedValue) == 33) {
                    jss.setStyle("AH" + count, "background-color", "yellow");
                    jss.setStyle("AH" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 34) {
                    jss.setStyle("AI" + count, "background-color", "yellow");
                    jss.setStyle("AI" + count, "color", "red");
                }
                if (parseInt(nestedValue) == 35) {
                    jss.setStyle("AJ" + count, "background-color", "yellow");
                    jss.setStyle("AJ" + count, "color", "red");
                }
            });
        }
        
        if (value['38'] == false && value['39'] == false) {
            DisableRow(count);
        }
        else if(value['38'] == true && value['39'] == false){
            SetRowColor_ForDeletedRow(count)
        }
        count++;
    });
}

$("#hider").hide();
$(".search_p").hide();
$(".search_section").hide();
$(".search_department").hide();
$(".search_incharge").hide();
$(".search_role").hide();
$(".search_explanation").hide();
$(".search_company").hide();
$(".search_grade").hide();
$(".search_unit_price").hide();

$("#buttonClose,#buttonClose_section,#buttonClose_department,#buttonClose_incharge,#buttonClose_role,#buttonClose_explanation,#buttonClose_company,#buttonClose_grade,#buttonClose_unit_price").click(function () {

    $("#hider").fadeOut("slow");
    $('.search_p').fadeOut("slow");
    $('.search_section').fadeOut("slow");
    $('.search_department').fadeOut("slow");
    $('.search_incharge').fadeOut("slow");
    $('.search_role').fadeOut("slow");
    $('.search_explanation').fadeOut("slow");
    $('.search_company').fadeOut("slow");
    $('.search_grade').fadeOut("slow");
    $('.search_unit_price').fadeOut("slow");
   // $('#search_p_text_box').val('');
});

var deleted = function (instance, x, y, value) {
    var assignmentIds = [];
    if (value.length > 0) {
        for (let i = 0; i < value.length; i++) {
            if (value[i][0].innerText != '' && value[i][0].innerText.toString().includes('new') == false) {
                assignmentIds.push(value[i][0].innerText);
            }

        }
        if (assignmentIds.length > 0) {
            $.ajax({
                url: `/api/utilities/ExcelDeleteAssignment/`,
                contentType: 'application/json',
                type: 'DELETE',
                async: false,
                dataType: 'json',
                data: JSON.stringify(assignmentIds),
                success: function (data) {
                    alert(data);
                }
            });
        }

    }

}


var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');   
}
var selectionActive = function(instance, x1, y1, x2, y2, origin) {
    var cellName1 = jexcel.getColumnNameFromId([x1, y1]);
    var cellName2 = jexcel.getColumnNameFromId([x2, y2]);
    
    //get cell information
    //var retrivedData = retrivedObject(jss.getRowData(y));


    var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);
    var retrivedData = retrivedObject_ApprovalData(jss.getRowData(sRows));
    $("#hidSelectedRow_AssignementId").val(retrivedData.assignmentId);
    $("#hidIsRowDeleted").val(retrivedData.isActive);
    $("#hidSelectedRowNumber").val(sRows);
    // var cellName1 = jexcel.getColumnNameFromId([x1, y1]);
    // var cellName2 = jexcel.getColumnNameFromId([x2, y2]);
    // $('#log').append('The selection from ' + cellName1 + ' to ' + cellName2 + '');
}
// var focus = function(instance) {
//     $('#log').append('The table ' + $(instance).prop('id') + ' is focus');
// }	
function updateArray(array, retrivedData) {
    var index = jssUpdatedData.findIndex(d => d.assignmentId == retrivedData.assignmentId);

    array[index].employeeId = retrivedData.employeeId;
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    array[index].explanationId = retrivedData.explanationId;
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].octPoint = retrivedData.octPoint;
    array[index].novPoint = retrivedData.novPoint;
    array[index].decPoint = retrivedData.decPoint;
    array[index].janPoint = retrivedData.janPoint;
    array[index].febPoint = retrivedData.febPoint;
    array[index].marPoint = retrivedData.marPoint;
    array[index].aprPoint = retrivedData.aprPoint;
    array[index].mayPoint = retrivedData.mayPoint;
    array[index].junPoint = retrivedData.junPoint;
    array[index].julPoint = retrivedData.julPoint;
    array[index].augPoint = retrivedData.augPoint;
    array[index].sepPoint = retrivedData.sepPoint;
    array[index].year = retrivedData.year;
}

function updateArrayForInsert(array, retrivedData) {
    var index = array.findIndex(d => d.assignmentId == retrivedData.assignmentId);
    array[index].employeeId = retrivedData.employeeId;
    array[index].remarks = retrivedData.remarks;
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    array[index].explanationId = retrivedData.explanationId;
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].octPoint = retrivedData.octPoint;
    array[index].novPoint = retrivedData.novPoint;
    array[index].decPoint = retrivedData.decPoint;
    array[index].janPoint = retrivedData.janPoint;
    array[index].febPoint = retrivedData.febPoint;
    array[index].marPoint = retrivedData.marPoint;
    array[index].aprPoint = retrivedData.aprPoint;
    array[index].mayPoint = retrivedData.mayPoint;
    array[index].junPoint = retrivedData.junPoint;
    array[index].julPoint = retrivedData.julPoint;
    array[index].augPoint = retrivedData.augPoint;
    array[index].sepPoint = retrivedData.sepPoint;
    array[index].year = retrivedData.year;
}

function retrivedObject(rowData) {
    return {
        assignmentId: rowData[0],
        employeeName: rowData[1],
        remarks: rowData[2],
        employeeId: rowData[35],
        sectionId: rowData[3],
        departmentId: rowData[4],
        inchargeId: rowData[5],
        roleId: rowData[6],
        explanationId: rowData[7],
        companyId: rowData[8],
        gradeId: rowData[9],
        unitPrice: parseFloat(rowData[10]),
        octPoint: parseFloat(rowData[11]),
        novPoint: parseFloat(rowData[12]),
        decPoint: parseFloat(rowData[13]),
        janPoint: parseFloat(rowData[14]),
        febPoint: parseFloat(rowData[15]),
        marPoint: parseFloat(rowData[16]),
        aprPoint: parseFloat(rowData[17]),
        mayPoint: parseFloat(rowData[18]),
        junPoint: parseFloat(rowData[19]),
        julPoint: parseFloat(rowData[20]),
        augPoint: parseFloat(rowData[21]),
        sepPoint: parseFloat(rowData[22]),
        year: document.getElementById('assignment_year_list').value,
        bcyr: rowData[36],
        bCYRApproved: rowData[37]
    };
}

function retrivedObject_ApprovalData(rowData) {
    return {
        assignmentId: rowData[0],
        employeeName: rowData[1],
        remarks: rowData[2],
        employeeId: rowData[35],
        sectionId: rowData[3],
        departmentId: rowData[4],
        inchargeId: rowData[5],
        roleId: rowData[6],
        explanationId: rowData[7],
        companyId: rowData[8],
        gradeId: rowData[9],
        unitPrice: parseFloat(rowData[10]),
        octPoint: parseFloat(rowData[11]),
        novPoint: parseFloat(rowData[12]),
        decPoint: parseFloat(rowData[13]),
        janPoint: parseFloat(rowData[14]),
        febPoint: parseFloat(rowData[15]),
        marPoint: parseFloat(rowData[16]),
        aprPoint: parseFloat(rowData[17]),
        mayPoint: parseFloat(rowData[18]),
        junPoint: parseFloat(rowData[19]),
        julPoint: parseFloat(rowData[20]),
        augPoint: parseFloat(rowData[21]),
        sepPoint: parseFloat(rowData[22]),
        year: document.getElementById('assignment_year_list').value,
        bcyr: rowData[36],
        bCYRApproved: rowData[37],
        isActive: rowData[39]
    };
}

function DeleteRecords() {
    $.getJSON(`/api/utilities/DeleteAssignments/`)
        .done(function (data) {
            //$('#department_search').empty();
            //$('#department_search').append(`<option value=''>Select Department</option>`);
            //$.each(data, function (key, item) {
            //    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
            //});
        });
}


//employee insert
function InsertEmployee() {
    var apiurl = "/api/utilities/CreateEmployee/";
    let employeeName = $("#employee_name").val();
    if (employeeName == "" || employeeName == null || employeeName == undefined) {
        $(".employee_err").show();
        return false;
    } else {
        $(".employee_err").hide();
        var data = {
            FullName: employeeName.trim()
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result > 0) {
                    jss.setValueFromCoords(1, globalY, data.FullName, false);
                    jss.setValueFromCoords(34, globalY, result, false);
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('Data Save Successfully!');
                    $('#employee_name').val('');
                    $('#jexcel_add_employee_modal').modal('hide');
                }

                //GetEmployeeList();
            },
            error: function (result) {
                alert(result.responseJSON.Message);
            }
        });
    }
}

//Get employee list
function GetEmployeeList() {
    $('#employee_list').empty();
    $.getJSON('/api/utilities/EmployeeList/')
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#employee_list').append(`<option value='${item.Id}'>${item.FullName}</option>`);
            });
        });
}

/***************************\                           
  Add Employee through Modal                      
\***************************/
function AddEmployee() {
    var employeeId = $('#employee_list').val();
    var employeeName = $('#employee_list').find("option:selected").text();
    jss.setValueFromCoords(1, globalY, employeeName, false);
    jss.setValueFromCoords(35, globalY, employeeId, false);
    $('#jexcel_add_employee_modal').modal('hide');
}
function UpdateForecast(){   
    $("#update_forecast").modal("hide");
    $("#jspreadsheet").hide();
    // $("#head_total").hide();
    LoaderShow(); 
    
    var userName = '';

    $.ajax({
        url: `/Registration/GetSession/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userName = data;
        }
    });    

    var updateMessage = "";
    var insertMessage = "";

    if (jssUpdatedData.length > 0) {
        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getDate();
        var year = dateObj.getUTCFullYear();

        var timestamp = `${year}${month}${day}_`;
        var promptValue = prompt("History Save As", '');

        if (promptValue == null || promptValue == undefined || promptValue == "") {
            return false;
        }
        else {
            updateMessage = "Successfully data updated";
            $.ajax({
                url: `/api/utilities/UpdateForecastData`,
                contentType: 'application/json',
                type: 'POST',
                async: true,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue, CellInfo:cellwiseColorCode }),
                success: function (data) {
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been updated by ', userName);
                    });        
                    $("#jspreadsheet").show();
                    //$("#head_total").show();
                    LoaderHide();             
                }
            });
            jssUpdatedData = [];
        }
    }
    else {
        $("#jspreadsheet").show();        
        //$("#head_total").show();
        LoaderHide();    
        //alert('No data found!');
        updateMessage = ""
    }

    if (jssInsertedData.length > 0) {
        var elementIndex = jssInsertedData.findIndex(object => {
            return object.employeeName.toLowerCase() == 'total';
        });
        if (elementIndex >= 0) {
            jssInsertedData.splice(elementIndex, 1);
        }

        insertMessage = "Successfully data inserted.";
        $.ajax({
            url: `/api/utilities/ExcelAssignment/`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify(jssInsertedData),
            success: function (data) {
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been inserted by ', userName);
                });
                $("#jspreadsheet").show();
                //$("#head_total").show();
                LoaderHide(); 
            }
        });
        jssInsertedData = [];
        newRowCount = 1;
    } 
    if(updateMessage =="" && insertMessage==""){
        $("#header_show").html("");
        $("#update_forecast").modal("show");
        $("#save_modal_header").html("There is nothing to save!");
        $("#back_button_show").css("display", "none");
        $("#save_btn_modal").css("display", "none");

        $("#close_save_modal").css("display", "block");
    }
    else if(updateMessage !="" && insertMessage !=""){
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("Operation Success.");
    }
    else if(updateMessage !=""){
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("Operation Success.");
    }
    else if(insertMessage !=""){
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("Operation Success.");
    }
}
function CompareUpdatedData() {
    
   
    if (jssUpdatedData.length > 0) {
        

        $.ajax({
            url: `/api/utilities/GetMatchedRows`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
            success: function (data) {
                $('#display_matched_rows table tbody').empty();
                $.each(data, function (index, element) {
                    $('#display_matched_rows table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                });
            }
        });
    }
}
function ImportCSVFile() {
    var userName = '';


    $.ajax({
        url: `/Registration/GetSession/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userName = data;
        }
    });

    $.ajax({
        url: `/Forecasts/Index/`,
        contentType: 'application/json',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: JSON.stringify(jssInsertedData),
        success: function (data) {
            var chat = $.connection.chatHub;
            $.connection.hub.start();
            // Start the connection.
            $.connection.hub.start().done(function () {
                chat.server.send('data has been inserted by ', userName);
            });
        }
    });
}

function GetAllForecastYears() {
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        //data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            $('#assignment_year_list').append(`<option value=''>年度データーの選択</option>`);
            $('#select_year_to_import').append(`<option value=''>select year</option>`);
            $('#replicate_from').append(`<option value=''>select year</option>`);
            //var count =1;
            $.each(data, function (index, element) {
                // if(count==1){
                //     $("#hidDefaultForecastYear").val(element.Year)
                // }
                $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
                $('#select_year_to_import').append(`<option value='${element.Year}'>${element.Year}</option>`);
                $('#replicate_from').append(`<option value='${element.Year}'>${element.Year}</option>`);
                //count++;
            });
        }
    });
}



function CheckForecastYear(){
    //var year = $('#assignment_year_list').find(":selected").val();
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!="" && typeof year != "undefined"){
        // $('#inputState').val(parseInt(year)+1);
        $('#select_import_year').val(parseInt(year)+1);
    }
}
// function ValidateYear(){
//     $("#csv_import_modal").modal("hide");
//     LoaderShow();
//     // var selectedYear = $('#inputState').find(":selected").val();
//     var selectedYear = $('#select_import_year').find(":selected").val();
//     if(selectedYear =="" || typeof selectedYear === "undefined"){
//         alert("please select year!");
//         return false;
//     }
    
// }
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        $('#duplciateYear').val(parseInt(year)+1);
        $('#replicate_from').val(year);
    }else{
        $('#duplciateYear').val('');
        $('#replicate_from').val('');
    }
}
function DuplicateForecast(){    
    var insertYear  = $('#duplciateYear').find(":selected").val();
    var copyYear = $('#replicate_from').find(":selected").val();

    if(copyYear!="" && insertYear!=""){
        $("#replicate_from_previous_year").modal("hide");
        $("#loading").css("display", "block");
        //LoaderShow();
        $.ajax({
            url: `/api/utilities/DuplicateForecastYear`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: "copyYear=" + copyYear+"&insertYear="+insertYear,
            success: function (data) {               
                LoaderHide();
                window.location.reload();
                alert("data has been replicated.")
            }
        });
    }else{
        alert("please select From and To year!");
        return false;
    }
}
function validate(){
    var selectedYear = $('#select_import_year').find(":selected").val();
    var import_file = $('#import_file_excel').val();
   
    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("please select year!");
        return false;
    }else if(import_file =="" || typeof import_file === "undefined"){
        alert("please select import file!");
        return false;
    }else { 
        return true; 
    }
}

$('#frm_import_year_data').submit(validate);
function SetRowColor(insertedRowNumber){
    jss.setStyle("A"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("A"+insertedRowNumber,"color", "red");
    jss.setStyle("B"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("B"+insertedRowNumber,"color", "red");
    jss.setStyle("C"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("C"+insertedRowNumber,"color", "red");
    jss.setStyle("D"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("D"+insertedRowNumber,"color", "red");
    jss.setStyle("E"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("E"+insertedRowNumber,"color", "red");
    jss.setStyle("F"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("F"+insertedRowNumber,"color", "red");
    jss.setStyle("G"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("G"+insertedRowNumber,"color", "red");
    jss.setStyle("H"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("H"+insertedRowNumber,"color", "red");
    jss.setStyle("I"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("I"+insertedRowNumber,"color", "red");
    jss.setStyle("J"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("J"+insertedRowNumber,"color", "red");
    jss.setStyle("K"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("K"+insertedRowNumber,"color", "red");
    jss.setStyle("L"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("L"+insertedRowNumber,"color", "red");
    jss.setStyle("M"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("M"+insertedRowNumber,"color", "red");
    jss.setStyle("N"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("N"+insertedRowNumber,"color", "red");
    jss.setStyle("O"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("O"+insertedRowNumber,"color", "red");
    jss.setStyle("P"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("P"+insertedRowNumber,"color", "red");
    jss.setStyle("Q"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("Q"+insertedRowNumber,"color", "red");
    jss.setStyle("R"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("R"+insertedRowNumber,"color", "red");
    jss.setStyle("S"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("S"+insertedRowNumber,"color", "red");
    jss.setStyle("T"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("T"+insertedRowNumber,"color", "red");
    jss.setStyle("U"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("U"+insertedRowNumber,"color", "red");
    jss.setStyle("V"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("V"+insertedRowNumber,"color", "red");
    jss.setStyle("W"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("W"+insertedRowNumber,"color", "red");
    jss.setStyle("X"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("X"+insertedRowNumber,"color", "red");
    jss.setStyle("Y"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("Y"+insertedRowNumber,"color", "red");
    jss.setStyle("Z"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("Z"+insertedRowNumber,"color", "red");
    jss.setStyle("AA"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AA"+insertedRowNumber,"color", "red");
    jss.setStyle("AB"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AB"+insertedRowNumber,"color", "red");
    jss.setStyle("AC"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AC"+insertedRowNumber,"color", "red");
    jss.setStyle("AD"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AD"+insertedRowNumber,"color", "red");
    jss.setStyle("AE"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AE"+insertedRowNumber,"color", "red");
    jss.setStyle("AF"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AF"+insertedRowNumber,"color", "red");
    jss.setStyle("AG"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AG"+insertedRowNumber,"color", "red");
    jss.setStyle("AH"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AH"+insertedRowNumber,"color", "red");
    jss.setStyle("AI"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AI"+insertedRowNumber,"color", "red");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AJ"+insertedRowNumber,"color", "red");
}

function SetRowColor_AfterApproved(insertedRowNumber){
    jss.setStyle("A"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("A"+insertedRowNumber,"color", "red");
    jss.setStyle("B" + insertedRowNumber, "background-color", "LightBlue");
    //jss.setStyle("B" + insertedRowNumber, "color", "red");
    jss.setStyle("C"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("C"+insertedRowNumber,"color", "red");
    jss.setStyle("D"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("D"+insertedRowNumber,"color", "red");
    jss.setStyle("E"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("E"+insertedRowNumber,"color", "red");
    jss.setStyle("F"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("F"+insertedRowNumber,"color", "red");
    jss.setStyle("G"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("G"+insertedRowNumber,"color", "red");
    jss.setStyle("H"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("H"+insertedRowNumber,"color", "red");
    jss.setStyle("I"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("I"+insertedRowNumber,"color", "red");
    jss.setStyle("J"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("J"+insertedRowNumber,"color", "red");
    jss.setStyle("K"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("K"+insertedRowNumber,"color", "red");
    jss.setStyle("L"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("L"+insertedRowNumber,"color", "red");
    jss.setStyle("M"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("M"+insertedRowNumber,"color", "red");
    jss.setStyle("N"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("N"+insertedRowNumber,"color", "red");
    jss.setStyle("O"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("O"+insertedRowNumber,"color", "red");
    jss.setStyle("P"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("P"+insertedRowNumber,"color", "red");
    jss.setStyle("Q"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("Q"+insertedRowNumber,"color", "red");
    jss.setStyle("R"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("R"+insertedRowNumber,"color", "red");
    jss.setStyle("S"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("S"+insertedRowNumber,"color", "red");
    jss.setStyle("T"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("T"+insertedRowNumber,"color", "red");
    jss.setStyle("U"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("U"+insertedRowNumber,"color", "red");
    jss.setStyle("V"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("V"+insertedRowNumber,"color", "red");
    jss.setStyle("W"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("W"+insertedRowNumber,"color", "red");
    jss.setStyle("X"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("X"+insertedRowNumber,"color", "red");
    jss.setStyle("Y"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("Y"+insertedRowNumber,"color", "red");
    jss.setStyle("Z"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("Z"+insertedRowNumber,"color", "red");
    jss.setStyle("AA"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AA"+insertedRowNumber,"color", "red");
    jss.setStyle("AB"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AB"+insertedRowNumber,"color", "red");
    jss.setStyle("AC"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AC"+insertedRowNumber,"color", "red");
    jss.setStyle("AD"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AD"+insertedRowNumber,"color", "red");
    jss.setStyle("AE"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AE"+insertedRowNumber,"color", "red");
    jss.setStyle("AF"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AF"+insertedRowNumber,"color", "red");
    jss.setStyle("AG"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AG"+insertedRowNumber,"color", "red");
    jss.setStyle("AH"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AH"+insertedRowNumber,"color", "red");
    jss.setStyle("AI"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AI"+insertedRowNumber,"color", "red");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle("AJ"+insertedRowNumber,"color", "red");
}
function SetRowColor_ForDeletedRow(insertedRowNumber){  
    $(jss.getCell("A" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("A"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("A" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("A"+insertedRowNumber,"color", "black");

    $(jss.getCell("B" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("B" + insertedRowNumber, "background-color", "#ffcccc");
    $(jss.getCell("B" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("B" + insertedRowNumber, "color", "black");

    $(jss.getCell("C" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("C"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("C" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("C"+insertedRowNumber,"color", "black");

    $(jss.getCell("D" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("D"+insertedRowNumber,"background-color", "Pink");
    $(jss.getCell("D" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("D"+insertedRowNumber,"color", "black");

    $(jss.getCell("E" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("E"+insertedRowNumber,"background-color", "Pink");
    $(jss.getCell("E" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("E"+insertedRowNumber,"color", "black");

    $(jss.getCell("F" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("F"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("F" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("F"+insertedRowNumber,"color", "black");

    $(jss.getCell("G" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("G"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("G" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("G"+insertedRowNumber,"color", "black");

    $(jss.getCell("H" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("H"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("H" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("H"+insertedRowNumber,"color", "black");

    $(jss.getCell("I" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("I"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("I" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("I"+insertedRowNumber,"color", "black");

    $(jss.getCell("J" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("J"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("J" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("J"+insertedRowNumber,"color", "black");

    $(jss.getCell("K" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("K"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("K" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("K"+insertedRowNumber,"color", "black");

    $(jss.getCell("L" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("L"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("L" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("L"+insertedRowNumber,"color", "black");

    $(jss.getCell("M" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("M"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("M" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("M"+insertedRowNumber,"color", "black");

    $(jss.getCell("N" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("N"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("N" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("N"+insertedRowNumber,"color", "black");

    $(jss.getCell("O" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("O"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("O" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("O"+insertedRowNumber,"color", "black");

    $(jss.getCell("P" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("P"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("P" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("P"+insertedRowNumber,"color", "black");

    $(jss.getCell("Q" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("Q"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("Q" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("Q"+insertedRowNumber,"color", "black");
    
    $(jss.getCell("R" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("R"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("R" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("R"+insertedRowNumber,"color", "black");

    $(jss.getCell("S" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("S"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("S" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("S"+insertedRowNumber,"color", "black");

    $(jss.getCell("T" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("T"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("T" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("T"+insertedRowNumber,"color", "black");

    $(jss.getCell("U" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("U"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("U" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("U"+insertedRowNumber,"color", "black");

    $(jss.getCell("V" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("V"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("V" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("V"+insertedRowNumber,"color", "black");

    $(jss.getCell("W" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("W"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("W" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("W"+insertedRowNumber,"color", "black");

    $(jss.getCell("X" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("X"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("X" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("X"+insertedRowNumber,"color", "black");

    $(jss.getCell("Y" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("Y"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("Y" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("Y"+insertedRowNumber,"color", "black");

    $(jss.getCell("Z" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("Z"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("Z" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("Z"+insertedRowNumber,"color", "black");

    $(jss.getCell("AA" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AA"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AA" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AA"+insertedRowNumber,"color", "black");

    $(jss.getCell("AB" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AB"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AB" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AB"+insertedRowNumber,"color", "black");

    $(jss.getCell("AC" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AC"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AC" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AC"+insertedRowNumber,"color", "black");

    $(jss.getCell("AD" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AD"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AD" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AD"+insertedRowNumber,"color", "black");

    $(jss.getCell("AE" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AE"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AE" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AE"+insertedRowNumber,"color", "black");

    $(jss.getCell("AF" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AF"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AF" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AF"+insertedRowNumber,"color", "black");

    $(jss.getCell("AG" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AG"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AG" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AG"+insertedRowNumber,"color", "black");

    $(jss.getCell("AH" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AH"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AH" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AH"+insertedRowNumber,"color", "black");

    $(jss.getCell("AI" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AI"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AI" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AI"+insertedRowNumber,"color", "black");

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"color", "black");
}
function SetRowColor_AfterSaved(insertedRowNumber){
    jss.setStyle("A"+insertedRowNumber,"background-color", "white");
    jss.setStyle("A"+insertedRowNumber,"color", "black");
    jss.setStyle("B" + insertedRowNumber, "background-color", "white");
    jss.setStyle("B" + insertedRowNumber, "color", "black");
    jss.setStyle("C"+insertedRowNumber,"background-color", "white");
    jss.setStyle("C"+insertedRowNumber,"color", "black");
    jss.setStyle("D"+insertedRowNumber,"background-color", "white");
    jss.setStyle("D"+insertedRowNumber,"color", "black");
    jss.setStyle("E"+insertedRowNumber,"background-color", "white");
    jss.setStyle("E"+insertedRowNumber,"color", "black");
    jss.setStyle("F"+insertedRowNumber,"background-color", "white");
    jss.setStyle("F"+insertedRowNumber,"color", "black");
    jss.setStyle("G"+insertedRowNumber,"background-color", "white");
    jss.setStyle("G"+insertedRowNumber,"color", "black");
    jss.setStyle("H"+insertedRowNumber,"background-color", "white");
    jss.setStyle("H"+insertedRowNumber,"color", "black");
    jss.setStyle("I"+insertedRowNumber,"background-color", "white");
    jss.setStyle("I"+insertedRowNumber,"color", "black");
    jss.setStyle("J"+insertedRowNumber,"background-color", "white");
    jss.setStyle("J"+insertedRowNumber,"color", "black");
    jss.setStyle("K"+insertedRowNumber,"background-color", "white");
    jss.setStyle("K"+insertedRowNumber,"color", "black");
    jss.setStyle("L"+insertedRowNumber,"background-color", "white");
    jss.setStyle("L"+insertedRowNumber,"color", "black");
    jss.setStyle("M"+insertedRowNumber,"background-color", "white");
    jss.setStyle("M"+insertedRowNumber,"color", "black");
    jss.setStyle("N"+insertedRowNumber,"background-color", "white");
    jss.setStyle("N"+insertedRowNumber,"color", "black");
    jss.setStyle("O"+insertedRowNumber,"background-color", "white");
    jss.setStyle("O"+insertedRowNumber,"color", "black");
    jss.setStyle("P"+insertedRowNumber,"background-color", "white");
    jss.setStyle("P"+insertedRowNumber,"color", "black");
    jss.setStyle("Q"+insertedRowNumber,"background-color", "white");
    jss.setStyle("Q"+insertedRowNumber,"color", "black");
    jss.setStyle("R"+insertedRowNumber,"background-color", "white");
    jss.setStyle("R"+insertedRowNumber,"color", "black");
    jss.setStyle("S"+insertedRowNumber,"background-color", "white");
    jss.setStyle("S"+insertedRowNumber,"color", "black");
    jss.setStyle("T"+insertedRowNumber,"background-color", "white");
    jss.setStyle("T"+insertedRowNumber,"color", "black");
    jss.setStyle("U"+insertedRowNumber,"background-color", "white");
    jss.setStyle("U"+insertedRowNumber,"color", "black");
    jss.setStyle("V"+insertedRowNumber,"background-color", "white");
    jss.setStyle("V"+insertedRowNumber,"color", "black");
    jss.setStyle("W"+insertedRowNumber,"background-color", "white");
    jss.setStyle("W"+insertedRowNumber,"color", "black");
    jss.setStyle("X"+insertedRowNumber,"background-color", "white");
    jss.setStyle("X"+insertedRowNumber,"color", "black");
    jss.setStyle("Y"+insertedRowNumber,"background-color", "white");
    jss.setStyle("Y"+insertedRowNumber,"color", "black");
    jss.setStyle("Z"+insertedRowNumber,"background-color", "white");
    jss.setStyle("Z"+insertedRowNumber,"color", "black");
    jss.setStyle("AA"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AA"+insertedRowNumber,"color", "black");
    jss.setStyle("AB"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AB"+insertedRowNumber,"color", "black");
    jss.setStyle("AC"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AC"+insertedRowNumber,"color", "black");
    jss.setStyle("AD"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AD"+insertedRowNumber,"color", "black");
    jss.setStyle("AE"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AE"+insertedRowNumber,"color", "black");
    jss.setStyle("AF"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AF"+insertedRowNumber,"color", "black");
    jss.setStyle("AG"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AG"+insertedRowNumber,"color", "black");
    jss.setStyle("AH"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AH"+insertedRowNumber,"color", "black");
    jss.setStyle("AI"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AI"+insertedRowNumber,"color", "black");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AJ"+insertedRowNumber,"color", "black");
}
function SetRowColor_ApprovedRow(insertedRowNumber){
    jss.setStyle("A"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("A"+insertedRowNumber,"color", "red");
    jss.setStyle("B" + insertedRowNumber, "background-color", "LightBlue");
    jss.setStyle("B" + insertedRowNumber, "color", "red");
    jss.setStyle("C"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("C"+insertedRowNumber,"color", "red");
    jss.setStyle("D"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("D"+insertedRowNumber,"color", "red");
    jss.setStyle("E"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("E"+insertedRowNumber,"color", "red");
    jss.setStyle("F"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("F"+insertedRowNumber,"color", "red");
    jss.setStyle("G"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("G"+insertedRowNumber,"color", "red");
    jss.setStyle("H"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("H"+insertedRowNumber,"color", "red");
    jss.setStyle("I"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("I"+insertedRowNumber,"color", "red");
    jss.setStyle("J"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("J"+insertedRowNumber,"color", "red");
    jss.setStyle("K"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("K"+insertedRowNumber,"color", "red");
    jss.setStyle("L"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("L"+insertedRowNumber,"color", "red");
    jss.setStyle("M"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("M"+insertedRowNumber,"color", "red");
    jss.setStyle("N"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("N"+insertedRowNumber,"color", "red");
    jss.setStyle("O"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("O"+insertedRowNumber,"color", "red");
    jss.setStyle("P"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("P"+insertedRowNumber,"color", "red");
    jss.setStyle("Q"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("Q"+insertedRowNumber,"color", "red");
    jss.setStyle("R"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("R"+insertedRowNumber,"color", "red");
    jss.setStyle("S"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("S"+insertedRowNumber,"color", "red");
    jss.setStyle("T"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("T"+insertedRowNumber,"color", "red");
    jss.setStyle("U"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("U"+insertedRowNumber,"color", "red");
    jss.setStyle("V"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("V"+insertedRowNumber,"color", "red");
    jss.setStyle("W"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("W"+insertedRowNumber,"color", "red");
    jss.setStyle("X"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("X"+insertedRowNumber,"color", "red");
    jss.setStyle("Y"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("Y"+insertedRowNumber,"color", "red");
    jss.setStyle("Z"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("Z"+insertedRowNumber,"color", "red");
    jss.setStyle("AA"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AA"+insertedRowNumber,"color", "red");
    jss.setStyle("AB"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AB"+insertedRowNumber,"color", "red");
    jss.setStyle("AC"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AC"+insertedRowNumber,"color", "red");
    jss.setStyle("AD"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AD"+insertedRowNumber,"color", "red");
    jss.setStyle("AE"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AE"+insertedRowNumber,"color", "red");
    jss.setStyle("AF"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AF"+insertedRowNumber,"color", "red");
    jss.setStyle("AG"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AG"+insertedRowNumber,"color", "red");
    jss.setStyle("AH"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AH"+insertedRowNumber,"color", "red");
    jss.setStyle("AI"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AI"+insertedRowNumber,"color", "red");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AJ"+insertedRowNumber,"color", "red");
}
function DisableRow(rowNumber) {

    jss.setStyle("A" + rowNumber, "background-color", "gray");
    jss.setStyle("A" + rowNumber, "color", "black");
    $(jss.getCell("A" + (rowNumber))).addClass('readonly');

    jss.setStyle("B" + rowNumber, "background-color", "gray");
    jss.setStyle("B" + rowNumber, "color", "black");
    $(jss.getCell("B" + (rowNumber))).addClass('readonly');

    jss.setStyle("C" + rowNumber, "background-color", "gray");
    jss.setStyle("C" + rowNumber, "color", "black");
    $(jss.getCell("C" + (rowNumber))).addClass('readonly');

    jss.setStyle("D" + rowNumber, "background-color", "gray");
    jss.setStyle("D" + rowNumber, "color", "black");
    $(jss.getCell("D" + (rowNumber))).addClass('readonly');

    jss.setStyle("E" + rowNumber, "background-color", "gray");
    jss.setStyle("E" + rowNumber, "color", "black");
    $(jss.getCell("E" + (rowNumber))).addClass('readonly');

    jss.setStyle("F" + rowNumber, "background-color", "gray");
    jss.setStyle("F" + rowNumber, "color", "black");
    $(jss.getCell("F" + (rowNumber))).addClass('readonly');

    jss.setStyle("G" + rowNumber, "background-color", "gray");
    jss.setStyle("G" + rowNumber, "color", "black");
    $(jss.getCell("G" + (rowNumber))).addClass('readonly');

    jss.setStyle("H" + rowNumber, "background-color", "gray");
    jss.setStyle("H" + rowNumber, "color", "black");
    $(jss.getCell("H" + (rowNumber))).addClass('readonly');

    jss.setStyle("I" + rowNumber, "background-color", "gray");
    jss.setStyle("I" + rowNumber, "color", "black");
    $(jss.getCell("I" + (rowNumber))).addClass('readonly');

    jss.setStyle("J" + rowNumber, "background-color", "gray");
    jss.setStyle("J" + rowNumber, "color", "black");
    $(jss.getCell("J" + (rowNumber))).addClass('readonly');

    jss.setStyle("K" + rowNumber, "background-color", "gray");
    jss.setStyle("K" + rowNumber, "color", "black");
    $(jss.getCell("K" + (rowNumber))).addClass('readonly');

    jss.setStyle("L" + rowNumber, "background-color", "gray");
    jss.setStyle("L" + rowNumber, "color", "black");
    $(jss.getCell("L" + (rowNumber))).addClass('readonly');

    jss.setStyle("M" + rowNumber, "background-color", "gray");
    jss.setStyle("M" + rowNumber, "color", "black");
    $(jss.getCell("M" + (rowNumber))).addClass('readonly');

    jss.setStyle("N" + rowNumber, "background-color", "gray");
    jss.setStyle("N" + rowNumber, "color", "black");
    $(jss.getCell("N" + (rowNumber))).addClass('readonly');

    jss.setStyle("O" + rowNumber, "background-color", "gray");
    jss.setStyle("O" + rowNumber, "color", "black");
    $(jss.getCell("O" + (rowNumber))).addClass('readonly');

    jss.setStyle("P" + rowNumber, "background-color", "gray");
    jss.setStyle("P" + rowNumber, "color", "black");
    $(jss.getCell("P" + (rowNumber))).addClass('readonly');

    jss.setStyle("Q" + rowNumber, "background-color", "gray");
    jss.setStyle("Q" + rowNumber, "color", "black");
    $(jss.getCell("Q" + (rowNumber))).addClass('readonly');

    jss.setStyle("R" + rowNumber, "background-color", "gray");
    jss.setStyle("R" + rowNumber, "color", "black");
    $(jss.getCell("R" + (rowNumber))).addClass('readonly');

    jss.setStyle("S" + rowNumber, "background-color", "gray");
    jss.setStyle("S" + rowNumber, "color", "black");
    $(jss.getCell("S" + (rowNumber))).addClass('readonly');

    jss.setStyle("T" + rowNumber, "background-color", "gray");
    jss.setStyle("T" + rowNumber, "color", "black");
    $(jss.getCell("T" + (rowNumber))).addClass('readonly');

    jss.setStyle("U" + rowNumber, "background-color", "gray");
    jss.setStyle("U" + rowNumber, "color", "black");
    $(jss.getCell("U" + (rowNumber))).addClass('readonly');

    jss.setStyle("V" + rowNumber, "background-color", "gray");
    jss.setStyle("V" + rowNumber, "color", "black");
    $(jss.getCell("V" + (rowNumber))).addClass('readonly');

    jss.setStyle("W" + rowNumber, "background-color", "gray");
    jss.setStyle("W" + rowNumber, "color", "black");
    $(jss.getCell("W" + (rowNumber))).addClass('readonly');

    jss.setStyle("X" + rowNumber, "background-color", "gray");
    jss.setStyle("X" + rowNumber, "color", "black");
    jss.setStyle("Y" + rowNumber, "background-color", "gray");
    jss.setStyle("Y" + rowNumber, "color", "black");
    jss.setStyle("Z" + rowNumber, "background-color", "gray");
    jss.setStyle("Z" + rowNumber, "color", "black");
    jss.setStyle("AA" + rowNumber, "background-color", "gray");
    jss.setStyle("AA" + rowNumber, "color", "black");
    jss.setStyle("AB" + rowNumber, "background-color", "gray");
    jss.setStyle("AB" + rowNumber, "color", "black");
    jss.setStyle("AC" + rowNumber, "background-color", "gray");
    jss.setStyle("AC" + rowNumber, "color", "black");
    jss.setStyle("AD" + rowNumber, "background-color", "gray");
    jss.setStyle("AD" + rowNumber, "color", "black");
    jss.setStyle("AE" + rowNumber, "background-color", "gray");
    jss.setStyle("AE" + rowNumber, "color", "black");
    jss.setStyle("AF" + rowNumber, "background-color", "gray");
    jss.setStyle("AF" + rowNumber, "color", "black");
    jss.setStyle("AG" + rowNumber, "background-color", "gray");
    jss.setStyle("AG" + rowNumber, "color", "black");
    jss.setStyle("AH" + rowNumber, "background-color", "gray");
    jss.setStyle("AH" + rowNumber, "color", "black");
    jss.setStyle("AI" + rowNumber, "background-color", "gray");
    jss.setStyle("AI" + rowNumber, "color", "black");
    jss.setStyle("AJ" + rowNumber, "background-color", "gray");
    jss.setStyle("AJ" + rowNumber, "color", "black");
}

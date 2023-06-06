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
var cellwiseColorCodeForInsert = [];
var changeCount = 0;
var newRowChangeEventFlag = false;

function LoaderShow() {
    $("#forecast_table_wrapper").css("display", "none");
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#forecast_table_wrapper").css("display", "block");
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
        //var assignmentYear = $("#hidDefaultForecastYear").val();
        //$('#assignment_year_list').val(assignmentYear);  
        $("#jspreadsheet").hide();  
    }
    var count = 1;


    $('#employee_list').select2();

 

    



    //$.connection.chatHub.disconnected(function () {
    //    setTimeout(function () {
    //        $.connection.hub.start();
    //    }, 3000); // Restart connection after 3 seconds.
    //});    

    $('#update_forecast_history').on('click', function () {

        var employeeCount = 0;
        var rowCount = 0;
        $.ajax({
            url: `/api/utilities/GetUserLogs/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                employeeCount = data.length;
                console.log(employeeCount);
            }
        });


        if (jssInsertedData.length > 0 || jssUpdatedData.length > 0) {
            $("#save_modal_header").html("年度データー(Emp. Assignments)");
            $("#back_button_show").css("display", "block");
            $("#save_btn_modal").css("display", "block");
            $("#close_save_modal").css("display", "none");
        } else {
            $("#update_forecast").modal("show");
            $("#save_modal_header").html("There is nothing to save!");
            $("#back_button_show").css("display", "none");
            $("#save_btn_modal").css("display", "none");

            $("#close_save_modal").css("display", "block");
        }



        if (jssUpdatedData.length > 0) {

            $.ajax({
                url: `/api/utilities/GetMatchedRowNumber/`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
                success: function (data) {
                    rowCount = data;
                    console.log(rowCount);
                    $.ajax({
                        url: `/api/utilities/GetMatchedUserNames/`,
                        contentType: 'application/json',
                        type: 'POST',
                        async: false,
                        dataType: 'json',
                        data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
                        success: function (userNames) {
                            $('#user_names').empty();
                            $('#user_names').append(userNames);
                        }
                    });
                    $('#row_count').empty();
                    $('#row_count').append(data);
                }
            });
        }


        if (employeeCount == 1) {
            $('#header_show').css('display', 'none');
            $('#back_button_show').css('display', 'none');
        }
        else {
            if (rowCount == 0) {
                $('#header_show').css('display', 'none');
                $('#back_button_show').css('display', 'none');
            }
            else {
                $('#header_show').css('display', 'block');
                $('#back_button_show').css('display', 'block');
            }
         
        }
      
        $('#update_forecast').modal('show');
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
            // var assignmentYear = $('#assignment_year_list').val();
            // if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            //     alert('Select valid year!!!');
            //     return false;
            // }            
            ShowForecastResults(assignmentYear);
        }, 3000);

        
    });
    $(document).on('click', '#cancel_forecast_history ', function () {    
        var assignmentYear = $('#assignment_year_list').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }

        console.log("assignmentYear: "+assignmentYear);

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
    //LoaderShow();
    //debugger;

    //$("#loading").css("display", "block");
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

    // var year = $('#hidForecastYear').val();

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
        url: `/api/utilities/SearchForecastEmployee`,
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
            { title: "IsActive", type: 'hidden', name: "IsActive" },
            { title: "BCYRApproved", type: 'hidden', name: "BCYRApproved" },
            { title: "BCYRCellApproved", type: 'hidden', name: "BCYRCellApproved" },
            { title: "IsApproved", type: 'hidden', name: "IsApproved" },
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
                    updateArrayForInsert(jssInsertedData, retrivedData, x,y, cell, value, beforeChangedValue);
                }
                else {
                    var dataCheck = jssUpdatedData.filter(d => d.assignmentId == retrivedData.assignmentId);
                    //console.log(checkId);

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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText=='false') {
                                //console.log(value);
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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
                            if (value.childNodes[36].innerText == employeeId.toString() && value.childNodes[39].innerText == 'false') {
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

                    console.log(cellwiseColorCode);
                }

            }

        },
        oninsertrow: newRowInserted,
        //ondeleterow: deleted,
        contextMenu: function (obj, x, y, e) {
            var items = [];

            // Insert new row
            //if (obj.options.allowInsertRow == true) {

            //    items.push({
            //        title: '新しい行を挿入する (New Row)',
            //        onclick: function () {
            //            obj.insertRow(1, parseInt(y));
            //        }
            //    });
            //}

            items.push({
                title: '要員を追加 (Add Emp)',
                onclick: function () {
                    obj.insertRow(1, parseInt(y));
                    var insertedRowNumber = parseInt(obj.getSelectedRows(true)) + 2;
                    
                    setTimeout(function () {
                        SetRowColor(insertedRowNumber);
                        console.log(insertedRowNumber);
                        jss.setValueFromCoords(36, (insertedRowNumber - 1), true, false);

                        $('#jexcel_add_employee_modal').modal('show');
                        globalY = parseInt(y) + 1;
                        GetEmployeeList();
                    },1000);
                    
                    
                }
            });
            items.push({
                title: '要員のコピー（単価変更）(unit price)',
                onclick: function () {
                    debugger;
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));

                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {
                                
                                if (isNaN(x[0])) {
                                    allSpecificObjectsCount++;
                                    allSameEmployeeId.push(x[0]);
                                }

                            }
                        }
                        var allSameEmployeeIdSplitted = [];
                        for (var i = 0; i < allSameEmployeeId.length; i++) {
                            var singleNewEmployeeId = allSameEmployeeId[i].split('-');
                            allSameEmployeeIdSplitted.push(parseInt(singleNewEmployeeId[1]));
                        }
                       

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeIdSplitted);

                        for (let x = 0; x < allData.length; x++) {
                            //debugger;
                            if (allData[x][0] == 'new-'+minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            //console.log(x);
                            if (x[0] == 'new-'+minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})`;
                                break;
                            }
                        }



                    }
                    else {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[0])) {
                                    allSameEmployeeId.push(x[0]);
                                }

                            }
                        }

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);

                        for (let x = 0; x < allData.length; x++) {
                            //debugger;
                            if (allData[x][0] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;

                        for (let x of allData) {
                            //console.log(x);
                            if (x[0] == minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})`;
                                break;
                            }
                        }


                    }
                    
                    
                    
                    obj.setValueFromCoords(1, nextRow, newCountedEmployeeName, false);
                    allSameEmployeeId = [];


                    obj.setValueFromCoords(35, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(2, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(3, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(4, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(5, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(6, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(7, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(8, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(9, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(10, nextRow, retrivedData.unitPrice, false);

                    // color row....
                    jss.setStyle("B" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("B" + (nextRow + 1), "color", "red");

                    jss.setStyle("J" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("J" + (nextRow + 1), "color", "red");

                    jss.setStyle("K" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("K" + (nextRow + 1), "color", "red");

                    // disable section....
                    $(obj.getCell("D" + (nextRow + 1))).addClass('readonly');
                    // disable department....
                    $(obj.getCell("E" + (nextRow + 1))).addClass('readonly');
                    // disable incharge....
                    $(obj.getCell("F" + (nextRow + 1))).addClass('readonly');
                    // disable role....
                    $(obj.getCell("G" + (nextRow + 1))).addClass('readonly');
                    // disable role....
                    $(obj.getCell("I" + (nextRow + 1))).addClass('readonly');

                    obj.setValueFromCoords(36, nextRow, false, false);
                    obj.setValueFromCoords(37, nextRow, `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`, false);


                    obj.setValueFromCoords(11, nextRow, '0.0', false);
                    obj.setValueFromCoords(12, nextRow, '0.0', false);
                    obj.setValueFromCoords(13, nextRow, '0.0', false);
                    obj.setValueFromCoords(14, nextRow, '0.0', false);
                    obj.setValueFromCoords(15, nextRow, '0.0', false);
                    obj.setValueFromCoords(16, nextRow, '0.0', false);
                    obj.setValueFromCoords(17, nextRow, '0.0', false);
                    obj.setValueFromCoords(18, nextRow, '0.0', false);
                    obj.setValueFromCoords(19, nextRow, '0.0', false);
                    obj.setValueFromCoords(20, nextRow, '0.0', false);
                    obj.setValueFromCoords(21, nextRow, '0.0', false);
                    obj.setValueFromCoords(22, nextRow, '0.0', false);

                    jss.setValueFromCoords(23, nextRow, `=K${nextRow + 1}*L${nextRow + 1}`, false);
                    jss.setValueFromCoords(24, nextRow, `=K${nextRow + 1}*M${nextRow + 1}`, false);
                    jss.setValueFromCoords(25, nextRow, `=K${nextRow + 1}*N${nextRow + 1}`, false);
                    jss.setValueFromCoords(26, nextRow, `=K${nextRow + 1}*O${nextRow + 1}`, false);
                    jss.setValueFromCoords(27, nextRow, `=K${nextRow + 1}*P${nextRow + 1}`, false);
                    jss.setValueFromCoords(28, nextRow, `=K${nextRow + 1}*Q${nextRow + 1}`, false);
                    jss.setValueFromCoords(29, nextRow, `=K${nextRow + 1}*R${nextRow + 1}`, false);
                    jss.setValueFromCoords(30, nextRow, `=K${nextRow + 1}*S${nextRow + 1}`, false);
                    jss.setValueFromCoords(31, nextRow, `=K${nextRow + 1}*T${nextRow + 1}`, false);
                    jss.setValueFromCoords(32, nextRow, `=K${nextRow + 1}*U${nextRow + 1}`, false);
                    jss.setValueFromCoords(33, nextRow, `=K${nextRow + 1}*V${nextRow + 1}`, false);
                    jss.setValueFromCoords(34, nextRow, `=K${nextRow + 1}*W${nextRow + 1}`, false);
                


                    newRowCount++;
                    newRowChangeEventFlag = false;
                }
               
            });
            items.push({
                title: '要員のコピー（役割変更）(role)',
                onclick: function () {
                    debugger;
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {

                                if (isNaN(x[0])) {
                                    allSpecificObjectsCount++;
                                    allSameEmployeeId.push(x[0]);
                                }

                            }
                        }
                        var allSameEmployeeIdSplitted = [];
                        for (var i = 0; i < allSameEmployeeId.length; i++) {
                            var singleNewEmployeeId = allSameEmployeeId[i].split('-');
                            allSameEmployeeIdSplitted.push(parseInt(singleNewEmployeeId[1]));
                        }


                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeIdSplitted);

                        for (let x = 0; x < allData.length; x++) {
                            //debugger;
                            if (allData[x][0] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;


                        for (let x of allData) {
                            //console.log(x);
                            if (x[0] == 'new-' + minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})*`;
                                break;
                            }
                        }
                    }
                    else {
                        newEmployeeId = "new-" + newRowCount;

                        var allSpecificObjectsCount = 0;
                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[0])) {
                                    allSameEmployeeId.push(x[0]);
                                }
                            }
                        }

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);


                        for (let x = 0; x < allData.length; x++) {
                            //debugger;
                            if (allData[x][0] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;

                        for (let x of allData) {
                            //console.log(x);
                            if (x[0] == minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})*`;
                                break;
                            }
                        }
                    }
                    
                    obj.setValueFromCoords(1, nextRow, newCountedEmployeeName, false);
                    allSameEmployeeId = [];

                   

                    obj.setValueFromCoords(35, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(2, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(3, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(4, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(5, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(6, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(7, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(8, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(9, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(10, nextRow, retrivedData.unitPrice, false);


                    // color row....
                    jss.setStyle("B" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("B" + (nextRow + 1), "color", "red");

                    jss.setStyle("D" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("D" + (nextRow + 1), "color", "red");


                    jss.setStyle("E" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("E" + (nextRow + 1), "color", "red");

                    jss.setStyle("F" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("F" + (nextRow + 1), "color", "red");

                    jss.setStyle("G" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("G" + (nextRow + 1), "color", "red");

                    jss.setStyle("I" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("I" + (nextRow + 1), "color", "red");


                    // disable grade and unit price....
                    $(obj.getCell("J" + (nextRow + 1))).addClass('readonly');
                    $(obj.getCell("K" + (nextRow + 1))).addClass('readonly');




                    obj.setValueFromCoords(36, nextRow, false, false);
                    obj.setValueFromCoords(37, nextRow, `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`, false);

                    obj.setValueFromCoords(11, nextRow, '0.0', false);
                    obj.setValueFromCoords(12, nextRow, '0.0', false);
                    obj.setValueFromCoords(13, nextRow, '0.0', false);
                    obj.setValueFromCoords(14, nextRow, '0.0', false);
                    obj.setValueFromCoords(15, nextRow, '0.0', false);
                    obj.setValueFromCoords(16, nextRow, '0.0', false);
                    obj.setValueFromCoords(17, nextRow, '0.0', false);
                    obj.setValueFromCoords(18, nextRow, '0.0', false);
                    obj.setValueFromCoords(19, nextRow, '0.0', false);
                    obj.setValueFromCoords(20, nextRow, '0.0', false);
                    obj.setValueFromCoords(21, nextRow, '0.0', false);
                    obj.setValueFromCoords(22, nextRow, '0.0', false);

                    jss.setValueFromCoords(23, nextRow, `=K${nextRow + 1}*L${nextRow + 1}`, false);
                    jss.setValueFromCoords(24, nextRow, `=K${nextRow + 1}*M${nextRow + 1}`, false);
                    jss.setValueFromCoords(25, nextRow, `=K${nextRow + 1}*N${nextRow + 1}`, false);
                    jss.setValueFromCoords(26, nextRow, `=K${nextRow + 1}*O${nextRow + 1}`, false);
                    jss.setValueFromCoords(27, nextRow, `=K${nextRow + 1}*P${nextRow + 1}`, false);
                    jss.setValueFromCoords(28, nextRow, `=K${nextRow + 1}*Q${nextRow + 1}`, false);
                    jss.setValueFromCoords(29, nextRow, `=K${nextRow + 1}*R${nextRow + 1}`, false);
                    jss.setValueFromCoords(30, nextRow, `=K${nextRow + 1}*S${nextRow + 1}`, false);
                    jss.setValueFromCoords(31, nextRow, `=K${nextRow + 1}*T${nextRow + 1}`, false);
                    jss.setValueFromCoords(32, nextRow, `=K${nextRow + 1}*U${nextRow + 1}`, false);
                    jss.setValueFromCoords(33, nextRow, `=K${nextRow + 1}*V${nextRow + 1}`, false);
                    jss.setValueFromCoords(34, nextRow, `=K${nextRow + 1}*W${nextRow + 1}`, false);

                    newRowCount++;
                    newRowChangeEventFlag = false;

                }
            });
            items.push({
                title: '要員のコピー（役割・単価変更）(role/unit)',
                onclick: function () {
                    //debugger;
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));

                    if (retrivedData.assignmentId.toString().includes('new')) {
                    //if (Assignmentid.tostring().include) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {

                                if (isNaN(x[0])) {
                                    allSpecificObjectsCount++;
                                    allSameEmployeeId.push(x[0]);
                                }

                            }
                        }
                        var allSameEmployeeIdSplitted = [];
                        for (var i = 0; i < allSameEmployeeId.length; i++) {
                            var singleNewEmployeeId = allSameEmployeeId[i].split('-');
                            allSameEmployeeIdSplitted.push(parseInt(singleNewEmployeeId[1]));
                        }


                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeIdSplitted);

                        for (let x = 0; x < allData.length; x++) {
                            //debugger;
                            if (allData[x][0] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            //console.log(x);
                            if (x[0] == 'new-' + minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})**`;
                                break;
                            }
                        }
                    } else {
                        newEmployeeId = "new-" + newRowCount;

                        //debugger;
                        var allSpecificObjectsCount = 0;
                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[0])) {
                                    allSameEmployeeId.push(x[0]);
                                }
                            }
                        }
                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);

                        for (let x = 0; x < allData.length; x++) {
                            //debugger;
                            if (allData[x][0] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            //console.log(x);
                            if (x[0] == minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})**`;
                                break;
                            }
                        }
                    }

                   
                    obj.setValueFromCoords(1, nextRow, newCountedEmployeeName, false);
                    allSameEmployeeId = [];

                    jss.setStyle("B" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("B" + (nextRow + 1), "color", "red");

                    jss.setStyle("D" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("D" + (nextRow + 1), "color", "red");


                    jss.setStyle("E" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("E" + (nextRow + 1), "color", "red");

                    jss.setStyle("F" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("F" + (nextRow + 1), "color", "red");

                    jss.setStyle("G" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("G" + (nextRow + 1), "color", "red");

                    jss.setStyle("I" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("I" + (nextRow + 1), "color", "red");

                    jss.setStyle("J" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("J" + (nextRow + 1), "color", "red");

                    jss.setStyle("K" + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle("K" + (nextRow + 1), "color", "red");



                    obj.setValueFromCoords(35, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(2, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(3, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(4, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(5, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(6, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(7, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(8, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(9, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(10, nextRow, retrivedData.unitPrice, false);



                    obj.setValueFromCoords(36, nextRow, false, false);
                    obj.setValueFromCoords(37, nextRow, `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`, false);


                    obj.setValueFromCoords(11, nextRow, '0.0', false);
                    obj.setValueFromCoords(12, nextRow, '0.0', false);
                    obj.setValueFromCoords(13, nextRow, '0.0', false);
                    obj.setValueFromCoords(14, nextRow, '0.0', false);
                    obj.setValueFromCoords(15, nextRow, '0.0', false);
                    obj.setValueFromCoords(16, nextRow, '0.0', false);
                    obj.setValueFromCoords(17, nextRow, '0.0', false);
                    obj.setValueFromCoords(18, nextRow, '0.0', false);
                    obj.setValueFromCoords(19, nextRow, '0.0', false);
                    obj.setValueFromCoords(20, nextRow, '0.0', false);
                    obj.setValueFromCoords(21, nextRow, '0.0', false);
                    obj.setValueFromCoords(22, nextRow, '0.0', false);

                    jss.setValueFromCoords(23, nextRow, `=K${nextRow + 1}*L${nextRow + 1}`, false);
                    jss.setValueFromCoords(24, nextRow, `=K${nextRow + 1}*M${nextRow + 1}`, false);
                    jss.setValueFromCoords(25, nextRow, `=K${nextRow + 1}*N${nextRow + 1}`, false);
                    jss.setValueFromCoords(26, nextRow, `=K${nextRow + 1}*O${nextRow + 1}`, false);
                    jss.setValueFromCoords(27, nextRow, `=K${nextRow + 1}*P${nextRow + 1}`, false);
                    jss.setValueFromCoords(28, nextRow, `=K${nextRow + 1}*Q${nextRow + 1}`, false);
                    jss.setValueFromCoords(29, nextRow, `=K${nextRow + 1}*R${nextRow + 1}`, false);
                    jss.setValueFromCoords(30, nextRow, `=K${nextRow + 1}*S${nextRow + 1}`, false);
                    jss.setValueFromCoords(31, nextRow, `=K${nextRow + 1}*T${nextRow + 1}`, false);
                    jss.setValueFromCoords(32, nextRow, `=K${nextRow + 1}*U${nextRow + 1}`, false);
                    jss.setValueFromCoords(33, nextRow, `=K${nextRow + 1}*V${nextRow + 1}`, false);
                    jss.setValueFromCoords(34, nextRow, `=K${nextRow + 1}*W${nextRow + 1}`, false);

                    newRowCount++;
                    newRowChangeEventFlag = false;
                }
            });

            items.push({
                title: '選択した要員の削除 (delete)',
                onclick: function () {
                    var value = obj.getSelectedRows();
                    console.log(value);
                    var assignmentIds = [];
                    if (value.length > 0) {
                        for (let i = 0; i < value.length; i++) {
                            if (value[i].childNodes[1].innerText != '' && value[i].childNodes[1].innerText.toString().includes('new') == false) {
                                assignmentIds.push(value[i].childNodes[1].innerText);
                                DisableRow(parseInt(value[i].childNodes[0].innerText));
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
            });

            return items;
        }
    });

    $("#update_forecast_history").css("display", "block");
    $("#cancel_forecast_history").css("display", "block");

    jss.deleteColumn(42, 15);
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
        //allEmployeeName = [];
        //var data = jss.getData();
        //for (var i = 0; i < jss.getData().length; i++) {
        //    allEmployeeName.push(data[i][1]);
        //}

        //var allEmployeeName = allEmployeeName.filter(function (value, index, array) {
        //    return array.indexOf(value) === index;
        //});
        //allEmployeeName.sort();
        //$('#search_p_search').empty();
        //allEmployeeName1 = [];
        //$.each(allEmployeeName, function (index, value) {
        //    $('#search_p_search').append(`<li><input type='checkbox' name='employeename' value='${value}'> ${value}</li>`);
        //    allEmployeeName1.push(value);
        //});
        //console.log(allEmployeeName);

        $("#hider").fadeIn("slow");
        $('.search_p').fadeIn("slow");
        //$('#filter_modal').modal('show');
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
        if (value['36'] == true && value['39'] == true) {
            SetRowColor_ApprovedRow(count);
        }else if (value['36'] == true && value['39'] == false) {
            SetRowColor(count);
        }
        else {
            var isApprovedCells = value['41'];
            var columnInfo = value['37'];
            var infoArray = columnInfo.split(',');
            console.log(infoArray);
            $.each(infoArray, function (nextedIndex, nestedValue) {        
                if( value['0'] ==   54)      
                {
                    console.log("test");
                }
                
                if (parseInt(nestedValue) == 1) {
                    // jss.setStyle("B" + count, "background-color", "yellow");
                    // jss.setStyle("B" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("B" + count, "background-color", "red");
                        jss.setStyle("B" + count, "color", "black");
                    }else{
                        jss.setStyle("B" + count, "background-color", "yellow");
                        jss.setStyle("B" + count, "color", "red");
                    }                    
                }
                
                if (parseInt(nestedValue) == 2) {
                    // jss.setStyle("C" + count, "background-color", "yellow");
                    // jss.setStyle("C" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("C" + count, "background-color", "red");
                        jss.setStyle("C" + count, "color", "black");
                    }else{
                        jss.setStyle("C" + count, "background-color", "yellow");
                        jss.setStyle("C" + count, "color", "red");
                    }  
                }
                
                if (parseInt(nestedValue) == 3) {
                    // jss.setStyle("D" + count, "background-color", "yellow");
                    // jss.setStyle("D" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("D" + count, "background-color", "red");
                        jss.setStyle("D" + count, "color", "black");
                    }else{
                        jss.setStyle("D" + count, "background-color", "yellow");
                        jss.setStyle("D" + count, "color", "red");
                    }                      
                }
                
                if (parseInt(nestedValue) == 4) {
                    // jss.setStyle("E" + count, "background-color", "yellow");
                    // jss.setStyle("E" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("E" + count, "background-color", "red");
                        jss.setStyle("E" + count, "color", "black");
                    }else{
                        jss.setStyle("E" + count, "background-color", "yellow");
                        jss.setStyle("E" + count, "color", "red");
                    } 
                }
                
                if (parseInt(nestedValue) == 5) {
                    // jss.setStyle("F" + count, "background-color", "yellow");
                    // jss.setStyle("F" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("F" + count, "background-color", "red");
                        jss.setStyle("F" + count, "color", "black");
                    }else{
                        jss.setStyle("F" + count, "background-color", "yellow");
                        jss.setStyle("F" + count, "color", "red");
                    } 
                }
                
                if (parseInt(nestedValue) == 6) {
                    // jss.setStyle("G" + count, "background-color", "yellow");
                    // jss.setStyle("G" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("G" + count, "background-color", "red");
                        jss.setStyle("G" + count, "color", "black");
                    }else{
                        jss.setStyle("G" + count, "background-color", "yellow");
                        jss.setStyle("G" + count, "color", "red");
                    }                   
                }
                
                if (parseInt(nestedValue) == 7) {
                    // jss.setStyle("H" + count, "background-color", "yellow");
                    // jss.setStyle("H" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("H" + count, "background-color", "red");
                        jss.setStyle("H" + count, "color", "black");
                    }else{
                        jss.setStyle("H" + count, "background-color", "yellow");
                        jss.setStyle("H" + count, "color", "red");
                    } 
                }
                
                if (parseInt(nestedValue) == 8) {
                    // jss.setStyle("I" + count, "background-color", "yellow");
                    // jss.setStyle("I" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("I" + count, "background-color", "red");
                        jss.setStyle("I" + count, "color", "black");
                    }else{
                        jss.setStyle("I" + count, "background-color", "yellow");
                        jss.setStyle("I" + count, "color", "red");
                    } 
                }
                
                if (parseInt(nestedValue) == 9) {
                    // jss.setStyle("J" + count, "background-color", "yellow");
                    // jss.setStyle("J" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("J" + count, "background-color", "red");
                        jss.setStyle("J" + count, "color", "black");
                    }else{
                        jss.setStyle("J" + count, "background-color", "yellow");
                        jss.setStyle("J" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 10) {
                    // jss.setStyle("K" + count, "background-color", "yellow");
                    // jss.setStyle("K" + count, "color", "red");

                    if(isApprovedCells == true){
                        jss.setStyle("K" + count, "background-color", "red");
                        jss.setStyle("K" + count, "color", "black");
                    }else{
                        jss.setStyle("K" + count, "background-color", "yellow");
                        jss.setStyle("K" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 11) {
                    // jss.setStyle("L" + count, "background-color", "yellow");
                    // jss.setStyle("L" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("L" + count, "background-color", "red");
                        jss.setStyle("L" + count, "color", "black");
                    }else{
                        jss.setStyle("L" + count, "background-color", "yellow");
                        jss.setStyle("L" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 12) {
                    // jss.setStyle("M" + count, "background-color", "yellow");
                    // jss.setStyle("M" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("M" + count, "background-color", "red");
                        jss.setStyle("M" + count, "color", "black");
                    }else{
                        jss.setStyle("M" + count, "background-color", "yellow");
                        jss.setStyle("M" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 13) {
                    // jss.setStyle("N" + count, "background-color", "yellow");
                    // jss.setStyle("N" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("N" + count, "background-color", "red");
                        jss.setStyle("N" + count, "color", "black");
                    }else{
                        jss.setStyle("N" + count, "background-color", "yellow");
                        jss.setStyle("N" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 14) {
                    // jss.setStyle("O" + count, "background-color", "yellow");
                    // jss.setStyle("O" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("O" + count, "background-color", "red");
                        jss.setStyle("O" + count, "color", "black");
                    }else{
                        jss.setStyle("O" + count, "background-color", "yellow");
                        jss.setStyle("O" + count, "color", "red");
                    }
                }  
                          
                if (parseInt(nestedValue) == 15) {
                    // jss.setStyle("P" + count, "background-color", "yellow");
                    // jss.setStyle("P" + count, "color", "red"); 
                    if(isApprovedCells == true){
                        jss.setStyle("P" + count, "background-color", "red");
                        jss.setStyle("P" + count, "color", "black");
                    }else{
                        jss.setStyle("P" + count, "background-color", "yellow");
                        jss.setStyle("P" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 16) {
                    // jss.setStyle("Q" + count, "background-color", "yellow");
                    // jss.setStyle("Q" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("Q" + count, "background-color", "red");
                        jss.setStyle("Q" + count, "color", "black");
                    }else{
                        jss.setStyle("Q" + count, "background-color", "yellow");
                        jss.setStyle("Q" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 17) {
                    // jss.setStyle("R" + count, "background-color", "yellow");
                    // jss.setStyle("R" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("R" + count, "background-color", "red");
                        jss.setStyle("R" + count, "color", "black");
                    }else{
                        jss.setStyle("R" + count, "background-color", "yellow");
                        jss.setStyle("R" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 18) {
                    // jss.setStyle("S" + count, "background-color", "yellow");
                    // jss.setStyle("S" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("S" + count, "background-color", "red");
                        jss.setStyle("S" + count, "color", "black");
                    }else{
                        jss.setStyle("S" + count, "background-color", "yellow");
                        jss.setStyle("S" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 19) {
                    // jss.setStyle("T" + count, "background-color", "yellow");
                    // jss.setStyle("T" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("T" + count, "background-color", "red");
                        jss.setStyle("T" + count, "color", "black");
                    }else{
                        jss.setStyle("T" + count, "background-color", "yellow");
                        jss.setStyle("T" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 20) {
                    // jss.setStyle("U" + count, "background-color", "yellow");
                    // jss.setStyle("U" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("U" + count, "background-color", "red");
                        jss.setStyle("U" + count, "color", "black");
                    }else{
                        jss.setStyle("U" + count, "background-color", "yellow");
                        jss.setStyle("U" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 21) {
                    // jss.setStyle("V" + count, "background-color", "yellow");
                    // jss.setStyle("V" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("V" + count, "background-color", "red");
                        jss.setStyle("V" + count, "color", "black");
                    }else{
                        jss.setStyle("V" + count, "background-color", "yellow");
                        jss.setStyle("V" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 22) {
                    // jss.setStyle("W" + count, "background-color", "yellow");
                    // jss.setStyle("W" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("W" + count, "background-color", "red");
                        jss.setStyle("W" + count, "color", "black");
                    }else{
                        jss.setStyle("W" + count, "background-color", "yellow");
                        jss.setStyle("W" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 23) {
                    jss.setStyle("X" + count, "background-color", "yellow");
                    jss.setStyle("X" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("X" + count, "background-color", "red");
                        jss.setStyle("X" + count, "color", "black");
                    }else{
                        jss.setStyle("X" + count, "background-color", "yellow");
                        jss.setStyle("X" + count, "color", "red");
                    }
                }
               
                if (parseInt(nestedValue) == 24) {
                    // jss.setStyle("Y" + count, "background-color", "yellow");
                    // jss.setStyle("Y" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("Y" + count, "background-color", "red");
                        jss.setStyle("Y" + count, "color", "black");
                    }else{
                        jss.setStyle("Y" + count, "background-color", "yellow");
                        jss.setStyle("Y" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 25) {
                    // jss.setStyle("Z" + count, "background-color", "yellow");
                    // jss.setStyle("Z" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("Z" + count, "background-color", "red");
                        jss.setStyle("Z" + count, "color", "black");
                    }else{
                        jss.setStyle("Z" + count, "background-color", "yellow");
                        jss.setStyle("Z" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 26) {
                    // jss.setStyle("AA" + count, "background-color", "yellow");
                    // jss.setStyle("AA" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AA" + count, "background-color", "red");
                        jss.setStyle("AA" + count, "color", "black");
                    }else{
                        jss.setStyle("AA" + count, "background-color", "yellow");
                        jss.setStyle("AA" + count, "color", "red");
                    }
                }
               
                if (parseInt(nestedValue) == 27) {
                    // jss.setStyle("AB" + count, "background-color", "yellow");
                    // jss.setStyle("AB" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AB" + count, "background-color", "red");
                        jss.setStyle("AB" + count, "color", "black");
                    }else{
                        jss.setStyle("AB" + count, "background-color", "yellow");
                        jss.setStyle("AB" + count, "color", "red");
                    }
                }
               
                if (parseInt(nestedValue) == 28) {
                    // jss.setStyle("AC" + count, "background-color", "yellow");
                    // jss.setStyle("AC" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AC" + count, "background-color", "red");
                        jss.setStyle("AC" + count, "color", "black");
                    }else{
                        jss.setStyle("AC" + count, "background-color", "yellow");
                        jss.setStyle("AC" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 29) {
                    // jss.setStyle("AD" + count, "background-color", "yellow");
                    // jss.setStyle("AD" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AD" + count, "background-color", "red");
                        jss.setStyle("AD" + count, "color", "black");
                    }else{
                        jss.setStyle("AD" + count, "background-color", "yellow");
                        jss.setStyle("AD" + count, "color", "red");
                    }
                }
               
                if (parseInt(nestedValue) == 30) {
                    // jss.setStyle("AE" + count, "background-color", "yellow");
                    // jss.setStyle("AE" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AE" + count, "background-color", "red");
                        jss.setStyle("AE" + count, "color", "black");
                    }else{
                        jss.setStyle("AE" + count, "background-color", "yellow");
                        jss.setStyle("AE" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 31) {
                    // jss.setStyle("AF" + count, "background-color", "yellow");
                    // jss.setStyle("AF" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AF" + count, "background-color", "red");
                        jss.setStyle("AF" + count, "color", "black");
                    }else{
                        jss.setStyle("AF" + count, "background-color", "yellow");
                        jss.setStyle("AF" + count, "color", "red");
                    }
                }
                
                if (parseInt(nestedValue) == 32) {
                    // jss.setStyle("AG" + count, "background-color", "yellow");
                    // jss.setStyle("AG" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AG" + count, "background-color", "red");
                        jss.setStyle("AG" + count, "color", "black");
                    }else{
                        jss.setStyle("AG" + count, "background-color", "yellow");
                        jss.setStyle("AG" + count, "color", "red");
                    }
                }
               
                if (parseInt(nestedValue) == 33) {
                    // jss.setStyle("AH" + count, "background-color", "yellow");
                    // jss.setStyle("AH" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AH" + count, "background-color", "red");
                        jss.setStyle("AH" + count, "color", "black");
                    }else{
                        jss.setStyle("AH" + count, "background-color", "yellow");
                        jss.setStyle("AH" + count, "color", "red");
                    }
                }
               
                if (parseInt(nestedValue) == 34) {
                    // jss.setStyle("AI" + count, "background-color", "yellow");
                    // jss.setStyle("AI" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AI" + count, "background-color", "red");
                        jss.setStyle("AI" + count, "color", "black");
                    }else{
                        jss.setStyle("AI" + count, "background-color", "yellow");
                        jss.setStyle("AI" + count, "color", "red");
                    }                    
                }
                
                if (parseInt(nestedValue) == 35) {
                    // jss.setStyle("AJ" + count, "background-color", "yellow");
                    // jss.setStyle("AJ" + count, "color", "red");
                    if(isApprovedCells == true){
                        jss.setStyle("AJ" + count, "background-color", "red");
                        jss.setStyle("AJ" + count, "color", "black");
                    }else{
                        jss.setStyle("AJ" + count, "background-color", "yellow");
                        jss.setStyle("AJ" + count, "color", "red");
                    }
                }
            });

            //approved cells color
            var approvedCells = value['40'];
            var arrApprovedCells = approvedCells.split(',');
            $.each(arrApprovedCells, function (nextedIndex, nestedValue2) {              
                if (parseInt(nestedValue2) == 1) {
                    jss.setStyle("B" + count, "background-color", "LightBlue");
                    jss.setStyle("B" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 2) {
                    jss.setStyle("C" + count, "background-color", "LightBlue");
                    jss.setStyle("C" + count, "color", "red");
                }

                if (parseInt(nestedValue2) == 3) {
                    jss.setStyle("D" + count, "background-color", "LightBlue");
                    jss.setStyle("D" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 4) {
                    jss.setStyle("E" + count, "background-color", "LightBlue");
                    jss.setStyle("E" + count, "color", "red");
                }
 
                if (parseInt(nestedValue2) == 5) {
                    jss.setStyle("F" + count, "background-color", "LightBlue");
                    jss.setStyle("F" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 6) {
                    jss.setStyle("G" + count, "background-color", "LightBlue");
                    jss.setStyle("G" + count, "color", "red");
                }

                if (parseInt(nestedValue2) == 7) {
                    jss.setStyle("H" + count, "background-color", "LightBlue");
                    jss.setStyle("H" + count, "color", "red");
                }
             
                if (parseInt(nestedValue2) == 8) {
                    jss.setStyle("I" + count, "background-color", "LightBlue");
                    jss.setStyle("I" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 9) {
                    jss.setStyle("J" + count, "background-color", "LightBlue");
                    jss.setStyle("J" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 10) {
                    jss.setStyle("K" + count, "background-color", "LightBlue");
                    jss.setStyle("K" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 11) {
                    jss.setStyle("L" + count, "background-color", "LightBlue");
                    jss.setStyle("L" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 12) {
                    jss.setStyle("M" + count, "background-color", "LightBlue");
                    jss.setStyle("M" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 13) {
                    jss.setStyle("N" + count, "background-color", "LightBlue");
                    jss.setStyle("N" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 14) {
                    jss.setStyle("O" + count, "background-color", "LightBlue");
                    jss.setStyle("O" + count, "color", "red");
                }
             
                if (parseInt(nestedValue2) == 15) {
                    jss.setStyle("P" + count, "background-color", "LightBlue");
                    jss.setStyle("P" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 16) {
                    jss.setStyle("Q" + count, "background-color", "LightBlue");
                    jss.setStyle("Q" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 17) {
                    jss.setStyle("R" + count, "background-color", "LightBlue");
                    jss.setStyle("R" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 18) {
                    jss.setStyle("S" + count, "background-color", "LightBlue");
                    jss.setStyle("S" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 19) {
                    jss.setStyle("T" + count, "background-color", "LightBlue");
                    jss.setStyle("T" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 20) {
                    jss.setStyle("U" + count, "background-color", "LightBlue");
                    jss.setStyle("U" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 21) {
                    jss.setStyle("V" + count, "background-color", "LightBlue");
                    jss.setStyle("V" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 22) {
                    jss.setStyle("W" + count, "background-color", "LightBlue");
                    jss.setStyle("W" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 23) {
                    jss.setStyle("X" + count, "background-color", "LightBlue");
                    jss.setStyle("X" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 24) {
                    jss.setStyle("Y" + count, "background-color", "LightBlue");
                    jss.setStyle("Y" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 25) {
                    jss.setStyle("Z" + count, "background-color", "LightBlue");
                    jss.setStyle("Z" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 26) {
                    jss.setStyle("AA" + count, "background-color", "LightBlue");
                    jss.setStyle("AA" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 27) {
                    jss.setStyle("AB" + count, "background-color", "LightBlue");
                    jss.setStyle("AB" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 28) {
                    jss.setStyle("AC" + count, "background-color", "LightBlue");
                    jss.setStyle("AC" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 29) {
                    jss.setStyle("AD" + count, "background-color", "LightBlue");
                    jss.setStyle("AD" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 30) {
                    jss.setStyle("AE" + count, "background-color", "LightBlue");
                    jss.setStyle("AE" + count, "color", "red");
                }
                
                if (parseInt(nestedValue2) == 31) {
                    jss.setStyle("AF" + count, "background-color", "LightBlue");
                    jss.setStyle("AF" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 32) {
                    jss.setStyle("AG" + count, "background-color", "LightBlue");
                    jss.setStyle("AG" + count, "color", "red");
                }
              
                if (parseInt(nestedValue2) == 33) {
                    jss.setStyle("AH" + count, "background-color", "LightBlue");
                    jss.setStyle("AH" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 34) {
                    jss.setStyle("AI" + count, "background-color", "LightBlue");
                    jss.setStyle("AI" + count, "color", "red");
                }
                if (parseInt(nestedValue2) == 35) {
                    jss.setStyle("AJ" + count, "background-color", "LightBlue");
                    jss.setStyle("AJ" + count, "color", "red");
                }
            });
            
        }

        if (value['38'] == false && value['39'] == false) {
            DisableRow(count);
        }
        else if(value['38'] == false && value['39'] == true){
            SetRowColor_ForDeletedRow(count)
        }        
        count++;
    });
}

//$('#search_p_text_box').on('keyup', function () {
//    var name = $(this).val();
//    console.log(allEmployeeName1);
//    if (allEmployeeName1.length > 0) {
//        var data = allEmployeeName1.filter(employeeName => employeeName.toLowerCase().includes(name.toLowerCase()));

//        data.sort();

//        $('#search_p_search').empty();
//        $.each(data, function (index, value) {
//            $('#search_p_search').append(`<li><input type='checkbox' name='employeename' value='${value}'> ${value}</li>`);
//        });
//    }
//});

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

//var deleted = function (instance, x, y, value) {
//    var assignmentIds = [];
//    if (value.length > 0) {
//        for (let i = 0; i < value.length; i++) {
//            if (value[i][0].innerText != '' && value[i][0].innerText.toString().includes('new') == false) {
//                assignmentIds.push(value[i][0].innerText);
//            }

//        }
//        if (assignmentIds.length > 0) {
//            $.ajax({
//                url: `/api/utilities/ExcelDeleteAssignment/`,
//                contentType: 'application/json',
//                type: 'DELETE',
//                async: false,
//                dataType: 'json',
//                data: JSON.stringify(assignmentIds),
//                success: function (data) {
//                    alert(data);
//                }
//            });
//        }

//    }

//}


var newRowInserted = function (instance, x, y, newRow) {
    console.log(jss.getData(false));
    var totalRow = jss.getData(false);
    console.log(`A${totalRow.length - 2}`);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');
    //console.log(newRow.options);
    //var sectionCell = newRow[0][2];
    //$(sectionCell).append();

}


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

function updateArrayForInsert(array, retrivedData, x,y, cell, value, beforeChangedValue) {
    var index = array.findIndex(d => d.assignmentId == retrivedData.assignmentId);
    array[index].employeeId = retrivedData.employeeId;
    array[index].employeeName = retrivedData.employeeName;
    
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    if (x == 2) {
        array[index].remarks = retrivedData.remarks;
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }
    }
    if (x == 7) {
        array[index].explanationId = retrivedData.explanationId;
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }
    }
    if (x == 11) {
        //debugger;
        var octSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                //console.log(value);
                octSum += parseFloat(value.childNodes[12].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
            octSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);

        }
        else {
            array[index].octPoint = retrivedData.octPoint;
        }

        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37,y);
            currentValue += ',new-x_'+x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }


    }

    if (x == 12) {
        var novSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                novSum += parseFloat(value.childNodes[13].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
            novSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].novPoint = retrivedData.novPoint;
        }

        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }


    }
    if (x == 13) {
        var decSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                decSum += parseFloat(value.childNodes[14].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
            decSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].decPoint = retrivedData.decPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 14) {
        var janSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                janSum += parseFloat(value.childNodes[15].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
            janSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].janPoint = retrivedData.janPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 15) {
        var febSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                febSum += parseFloat(value.childNodes[16].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
            febSum = 1;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].febPoint = retrivedData.febPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    
    if (x == 16) {
        var marSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                marSum += parseFloat(value.childNodes[17].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
            marSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].marPoint = retrivedData.marPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 17) {
        var aprSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                aprSum += parseFloat(value.childNodes[18].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
            aprSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].aprPoint = retrivedData.aprPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 18) {
        var maySum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                maySum += parseFloat(value.childNodes[19].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].mayPoint = retrivedData.mayPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 19) {
        var junSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                junSum += parseFloat(value.childNodes[20].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
            junSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].junPoint = retrivedData.junPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 20) {
        var julSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                julSum += parseFloat(value.childNodes[21].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].julPoint = retrivedData.julPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 21) {
        var augSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString() && value.childNodes[39].innerText == 'false') {
                augSum += parseFloat(value.childNodes[22].innerText);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
            augSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].augPoint = retrivedData.augPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 22) {
        var sepSum = 0;
        $.each(jss.rows, (index, value) => {
            if (value.childNodes[36].innerText == retrivedData.employeeId.toString()) {
                sepSum += parseFloat(value.childNodes[23].innerText);
            }

        });
        if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
            sepSum = 0;
            alert('Input not valid');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].sepPoint = retrivedData.sepPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    
    
    array[index].year = retrivedData.year;
    array[index].bcyr= retrivedData.bcyr;
    array[index].bCYRCell= retrivedData.bCYRCell;
}

function retrivedObject(rowData) {
    //debugger;
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
        bCYRCell: rowData[37],
        bCYRApproved: rowData[39],
        bCYRCellApproved: rowData[40],
        isApproved: rowData[41]
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
                    // jss.setValueFromCoords(34, globalY, result, false);
                    jss.setValueFromCoords(35, globalY, result, false);
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
    var promptValue = prompt("History Save As", '');
    $("#timeStamp_ForUpdateData").val('');
    if (promptValue == null || promptValue == undefined || promptValue == "") {
        return false;
    }else{
        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getDate();
        var year = dateObj.getUTCFullYear();
        var miliSeconds= dateObj.getMilliseconds();    
        var timestamp = `${year}${month}${day}${miliSeconds}_`; 
        
        if (jssUpdatedData.length > 0) {            
            updateMessage = "Successfully data updated";
            $.ajax({
                url: `/api/utilities/UpdateForecastData`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue, CellInfo:cellwiseColorCode }),
                success: function (data) {
                    $("#timeStamp_ForUpdateData").val(data);
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
            console.log(jssInsertedData);
            var update_timeStampId = $("#timeStamp_ForUpdateData").val();

            $.ajax({
                url: `/api/utilities/ExcelAssignment/`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                //data: JSON.stringify(jssInsertedData),
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssInsertedData, HistoryName: timestamp + promptValue, CellInfo:cellwiseColorCode,TimeStampId:update_timeStampId }),
                success: function (data) {
                    $("#timeStamp_ForUpdateData").val('');
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
    }
    
    if(updateMessage =="" && insertMessage==""){
        //alert("There is nothing to save!");
        //update_forecast
        //forecast_save_confirm_text
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
                    console.log(element);
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
    }else { return true; }
    // var cnv = $('#countryName').val();
    // if (!$.trim(cnv)) {
    //     alert('Country Name is required!');
    //     return false;
    // } else { return true; }
}

$('#frm_import_year_data').submit(validate);
function SetRowColor(insertedRowNumber){
    console.log("insertedRowNumber: "+insertedRowNumber);

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
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
var deletedExistingRowIds = [];

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
    
}
function LoaderHideJexcel(){
    $("#jspreadsheet").show();  
    $("#loading").css("display", "none");
}

//shorting columns
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
    //import budget selction menu
    $('#select_import_year').on('change', function() {
        var selectedBudgetYear = this.value;
        if (selectedBudgetYear != '' && selectedBudgetYear != null || selectedBudgetYear != undefined) {
            //check the selected year is valid for import the excel.
            CheckIsValidYearForImport(selectedBudgetYear);       
        }    
    });

    //check import year validation
    function CheckIsValidYearForImport(selectedBudgetYear){
        $.ajax({
            url: `/api/utilities/CheckIsValidYearForImport/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "select_year_type=" + selectedBudgetYear,
            success: function (data) {
                if(data == true){
                    CreateBudgetTypeWithYear(selectedBudgetYear);              
                }else{
                    $('#select_budget_type').empty();
                    $('#select_import_year').val('');
                    alert("selected year is not valid to import!");
                }
            }
        });
    }

    //check replicate year validation
    function CheckIsValidYearForReplicate(selectedBudgetYear){
        $.ajax({
            url: `/api/utilities/CheckIsValidYearForImport/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "select_year_type=" + selectedBudgetYear,
            success: function (data) {
                if(data == true){
                    ReplicateBudgetFromPreviousYearData(selectedBudgetYear);              
                }else{
                    $('#select_duplicate_budget_type').empty();
                    $('#duplciateYear').val('');
                    alert("selected year is not valid to replicate!");
                }
            }
        });
    }

    //create budget type dropdown and set as html
    function CreateBudgetTypeWithYear(selectedBudgetYear){
        $.ajax({
            url: `/api/utilities/CheckBudgetWithYear/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "BudgetYear=" + selectedBudgetYear,
            success: function (data) {
                $('#select_budget_type').empty();
                                               
                $('#select_budget_type').append(`<option value="">select type</option>`);
                //create fist half budget dropdown                    
                if(data.FirstHalfFinalize){
                    $('#select_budget_type').append(`<option value="1" disabled style='color:red;'>${selectedBudgetYear} Initial Budget Created</option>`);
                }else if(data.FirstHalfBudget){
                    $('#select_budget_type').append(`<option value="1" disabled style='color:orange;'>${selectedBudgetYear} Initial Budget Created But Not Finalize</option>`);
                }else if(!data.FirstHalfBudget){
                    $('#select_budget_type').append(`<option value="1">${selectedBudgetYear} Initial Budget</option>`);
                }  

                //create second half budget dropdown
                if(data.SecondHalfFinalize){
                    $('#select_budget_type').append(`<option value="2" disabled style='color:red;'>${selectedBudgetYear} 2nd Half Budget Created</option>`);
                }else if(data.SecondHalfBudget){
                    $('#select_budget_type').append(`<option value="2" disabled style='color:orange;'>${selectedBudgetYear} 2nd Half Budget Created But Not Finalize</option>`);
                }else if(!data.SecondHalfBudget){
                    if(data.FirstHalfBudget){
                        $('#select_budget_type').append(`<option value="2">${selectedBudgetYear} 2nd Half Budget</option>`);
                    }else{
                        $('#select_budget_type').append(`<option value="2" disabled style='color:gray;'>${selectedBudgetYear} 2nd Half Budget</option>`);
                    }                                               
                }  
            }
        });
    }

    //replicate budget from selected year
    function ReplicateBudgetFromPreviousYearData(selectedBudgetYear){
        //get budget initial and 2nd half data if exists
        $.ajax({
            url: `/api/utilities/CheckBudgetWithYear/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "BudgetYear=" + selectedBudgetYear,
            success: function (data) {
                $('#select_duplicate_budget_type').empty();
                                               
                $('#select_duplicate_budget_type').append(`<option value="">select type</option>`);
                //create fist half budget dropdown                    
                if(data.FirstHalfFinalize){
                    $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:red;'>${selectedBudgetYear} Initial Budget Created</option>`);
                }else if(data.FirstHalfBudget){
                    $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:orange;'>${selectedBudgetYear} Initial Budget Created But Not Finalize</option>`);
                }else if(!data.FirstHalfBudget){
                    $('#select_duplicate_budget_type').append(`<option value="1">${selectedBudgetYear} Initial Budget</option>`);
                }  

                //create second half budget dropdown
                if(data.SecondHalfFinalize){
                    $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:red;'>${selectedBudgetYear} 2nd Half Budget Created</option>`);
                }else if(data.SecondHalfBudget){
                    $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:orange;'>${selectedBudgetYear} 2nd Half Budget Created But Not Finalize</option>`);
                }else if(!data.SecondHalfBudget){                         
                    if(data.FirstHalfBudget){
                        $('#select_duplicate_budget_type').append(`<option value="2">${selectedBudgetYear} 2nd Half Budget</option>`);
                    }else{
                        $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:gray;'>${selectedBudgetYear} 2nd Half Budget</option>`);
                    }                        
                }  
            }
        });
    }

    //duplicate budget selction menu
    $('#duplciateYear').on('change', function() {
        var selectedBudgetYear = this.value;
    
        if (selectedBudgetYear != '' && selectedBudgetYear != null || selectedBudgetYear != undefined) {
            //check the selected year is valid for replicate data
	        CheckIsValidYearForReplicate(selectedBudgetYear);             
        }    
    });

    //From date on change
    $('#duplicate_from').on('change', function() {
        var selectedBudgetYear = this.value;
        if (selectedBudgetYear != '' && selectedBudgetYear != null && selectedBudgetYear != undefined) {
            $("#duplciateYear").prop('disabled', false);
            $('#select_duplicate_budget_type').empty();
            SelectDuplicateBudgetYearAndType();            
        }        
        else{
            $("#duplciateYear").prop('disabled', true);
            $('#select_duplicate_budget_type').empty();
        }
    });

    GetAllBudgetYear();
    GetAllFinalizeYear();

    var year = $('#hidForecastYear').val();
    $("#jspreadsheet").hide();      
    var count = 1;

    $('#employee_list').select2();    

    //finalize the budget data.
    $('#budget_finalize').on('click', function () {
        var selected_year_for_finalize_budget = $("#budget_years").val();

        if (selected_year_for_finalize_budget == null || selected_year_for_finalize_budget == undefined || selected_year_for_finalize_budget == "") {
            alert("please 年度を選択してください!");
        }
        else{
            $.ajax({
                url: `/api/utilities/FinalizeBudgetAssignment`,
                contentType: 'application/json',
                type: 'GET',
                async: true,
                dataType: 'json',
                data: "year=" + selected_year_for_finalize_budget,
                success: function (data) {        
                    alert("保存されました");
                    $("#save_bedget").prop("disabled",true);
                    $("#budget_finalize").prop("disabled",true); 
                }
            });
        }       
    });

    //search for section1
    $(document).on('change', '#section_search', function () {
        var sectionId = $(this).val();
        $.getJSON(`/api/utilities/DepartmentsBySection/${sectionId}`)
            .done(function (data) {
                $('#department_search').empty();
                $('#department_search').append(`<option value=''>部署を選択</option>`);
                $.each(data, function (key, item) {
                    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
                });
            });
    });
    
    //show budget data.
    $(document).on('click', '#search_budget ', function () {           
        var assignmentYear = $('#budget_years').val();        
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }     
        
        LoaderShowJexcel();
            
        setTimeout(function () {                                
            ShowBedgetResults(assignmentYear);
        }, 3000);
    });

    //refresh the page table data
    $(document).on('click', '#cancele_all_changed_budget ', function () {    
        var assignmentYear = $('#budget_years').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }

        deletedExistingRowIds = [];
        LoaderShowJexcel();            
        setTimeout(function () {                               
            ShowBedgetResults(assignmentYear);
        }, 3000);
        
    });
    
    $(document).ajaxComplete(function(){
        LoaderHideJexcel();
    });

});

//show budget data
function ShowBedgetResults(year) {
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
        alert('予算年度を選択');
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
        url: `/api/utilities/GetAllBudgetData`,
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
    
    if (_retriveddata != "" && _retriveddata != null && _retriveddata != undefined) {
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
                    width: 60,
                    filter: (instance, cell, c, r, source) => {
                        
                        let row = parseInt(r);
                        let column = parseInt(c) - 1;
                        
                        var value1 = instance.jexcel.getValueFromCoords(column, row);
                        if (parseInt(value1) != 3) {
                            return [];
                        }
                        else {
                            return gradesForJexcel;
                        }
                    },
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
                    readOnly: true,
                    mask: "#,##0",
                    name: "OctTotal",
                    width: 60
                },
                {
                    title: "11月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "NovTotal"
                },
                {
                    title: "12月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "DecTotal"
                },
                {
                    title: "1月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JanTotal"
                },
                {
                    title: "2月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "FebTotal"
                },
                {
                    title: "3月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "MarTotal"
                },
                {
                    title: "4月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "AprTotal"
                },
                {
                    title: "5月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "MayTotal"
                },
                {
                    title: "6月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JunTotal"
                },
                {
                    title: "7月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JulTotal"
                },
                {
                    title: "8月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "AugTotal"
                },
                {
                    title: "9月",
                    type: "decimal",
                    readOnly: true,
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
                { title: "BCYRCellPending", type: 'hidden', name: "BCYRCellPending" },

                { title: "IsRowPending", type: 'hidden', name: "IsRowPending" },
                { title: "IsDeletePending", type: 'hidden', name: "IsDeletePending" },
                { title: "RowType", type: 'hidden', name: "RowType" }
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
            //onafterchanges: function () {
            //},
            onchange: function (instance, cell, x, y, value) {            
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
                    
                        if (x == 2) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }                        
                            cellwiseColorCode.push(retrivedData.assignmentId+'_'+x);
                        }
                        
                        if (x == 3) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }                    

                        if (x == 4) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }
                        
                        if (x == 5) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }                        
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 6) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 7) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 8) {                    
                            var rowNumber = parseInt(y) + 1;
                            if (parseInt(value) !== 3) {
                                var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                                element[0].cells[10].innerText = '';
                                $(jss.getCell("J" + rowNumber)).addClass('readonly');
                                $(jss.getCell("J" + rowNumber)).css('color', 'black');
                                $(jss.getCell("J" + rowNumber)).css('background-color', 'white');
                            }
                            else {
                                $(jss.getCell("J" + rowNumber)).removeClass('readonly');
                                $(jss.getCell("J" + rowNumber)).css('color', 'black');
                                $(jss.getCell("J" + rowNumber)).css('background-color', 'white'); 
                            }

                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x); 
                        }

                        if (x == 9) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 10) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 11) {                        
                            var octSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    octSum += parseFloat(parseFloat(dataValue[11]));
                                }

                            });

                            if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
                                octSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }                    

                        if (x == 12) {                        
                            var novSum = 0;

                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    novSum += parseFloat(dataValue[12]);
                                }

                            });

                            if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
                                novSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 13) {
                            var decSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    decSum += parseFloat(dataValue[13]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
                                decSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 14) {                        
                            var janSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    janSum += parseFloat(dataValue[14]);
                                }
                            });
                            if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
                                janSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 15) {                        
                            var febSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    febSum += parseFloat(dataValue[15]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
                                febSum = 1;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 16) {                    
                            var marSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    marSum += parseFloat(dataValue[16]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
                                marSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 17) {                        
                            var aprSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    aprSum += parseFloat(dataValue[17]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
                                aprSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 18) {                        
                            var maySum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    maySum += parseFloat(dataValue[18]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 19) {                        
                            var junSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    junSum += parseFloat(dataValue[19]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
                                junSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 20) {                        
                            var julSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    julSum += parseFloat(dataValue[20]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }
                        
                        if (x == 21) {                        
                            var augSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    augSum += parseFloat(dataValue[21]);
                                }

                            });
                            if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
                                augSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 22) {                        
                            var sepSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[35].toString() == employeeId.toString() && dataValue[38] == true) {
                                    sepSum += parseFloat(dataValue[22]);
                                }

                            });
                            if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
                                sepSum = 0;
                                alert('入力値が不正です');
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
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }                                        
                    }

                }

            },
            oninsertrow: newRowInserted,
            //ondeleterow: deleted,
            contextMenu: function (obj, x, y, e) {
                var items = [];
                        
                return items;
            }
        });
    }else{
        $('#jspreadsheet').html("No Budget assign for this year!");
    }

    $("#save_bedget").css("display", "block");
    $("#cancele_all_changed_budget").css("display", "block");
    $("#budget_finalize").css("display", "block");
    
    jss.deleteColumn(46, 19);
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

    var allRows = jss.getData();
    var count = 1;
    $.each(allRows, function (index,value) {
        if (value['36'] == true && value['39'] == false) {            
            SetColorCommonRow(count,"yellow","red","newrow");
        }
        else {
            var isApprovedCells = value['41'];
            var columnInfo = value['37'];
            var infoArray = columnInfo.split(',');
            $.each(infoArray, function (nextedIndex, nestedValue) {        
                
                if (parseInt(nestedValue) == 1) {
                    jss.setStyle("B" + count, "background-color", "yellow");
                    jss.setStyle("B" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("B" + count, "background-color", "red");
                    //     jss.setStyle("B" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("B" + count, "background-color", "yellow");
                    //     jss.setStyle("B" + count, "color", "red");
                    // }                    
                }
                
                if (parseInt(nestedValue) == 2) {
                    jss.setStyle("C" + count, "background-color", "yellow");
                    jss.setStyle("C" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("C" + count, "background-color", "red");
                    //     jss.setStyle("C" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("C" + count, "background-color", "yellow");
                    //     jss.setStyle("C" + count, "color", "red");
                    // }  
                }
                
                if (parseInt(nestedValue) == 3) {
                    jss.setStyle("D" + count, "background-color", "yellow");
                    jss.setStyle("D" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("D" + count, "background-color", "red");
                    //     jss.setStyle("D" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("D" + count, "background-color", "yellow");
                    //     jss.setStyle("D" + count, "color", "red");
                    // }                      
                }
                
                if (parseInt(nestedValue) == 4) {
                    jss.setStyle("E" + count, "background-color", "yellow");
                    jss.setStyle("E" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("E" + count, "background-color", "red");
                    //     jss.setStyle("E" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("E" + count, "background-color", "yellow");
                    //     jss.setStyle("E" + count, "color", "red");
                    // } 
                }
                
                if (parseInt(nestedValue) == 5) {
                    jss.setStyle("F" + count, "background-color", "yellow");
                    jss.setStyle("F" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("F" + count, "background-color", "red");
                    //     jss.setStyle("F" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("F" + count, "background-color", "yellow");
                    //     jss.setStyle("F" + count, "color", "red");
                    // } 
                }
                
                if (parseInt(nestedValue) == 6) {
                    jss.setStyle("G" + count, "background-color", "yellow");
                    jss.setStyle("G" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("G" + count, "background-color", "red");
                    //     jss.setStyle("G" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("G" + count, "background-color", "yellow");
                    //     jss.setStyle("G" + count, "color", "red");
                    // }                   
                }
                
                if (parseInt(nestedValue) == 7) {
                    jss.setStyle("H" + count, "background-color", "yellow");
                    jss.setStyle("H" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("H" + count, "background-color", "red");
                    //     jss.setStyle("H" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("H" + count, "background-color", "yellow");
                    //     jss.setStyle("H" + count, "color", "red");
                    // } 
                }
                
                if (parseInt(nestedValue) == 8) {
                    jss.setStyle("I" + count, "background-color", "yellow");
                    jss.setStyle("I" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("I" + count, "background-color", "red");
                    //     jss.setStyle("I" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("I" + count, "background-color", "yellow");
                    //     jss.setStyle("I" + count, "color", "red");
                    // } 
                }
                
                if (parseInt(nestedValue) == 9) {
                    jss.setStyle("J" + count, "background-color", "yellow");
                    jss.setStyle("J" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("J" + count, "background-color", "red");
                    //     jss.setStyle("J" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("J" + count, "background-color", "yellow");
                    //     jss.setStyle("J" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 10) {
                    jss.setStyle("K" + count, "background-color", "yellow");
                    jss.setStyle("K" + count, "color", "red");

                    // if(isApprovedCells == true){
                    //     jss.setStyle("K" + count, "background-color", "red");
                    //     jss.setStyle("K" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("K" + count, "background-color", "yellow");
                    //     jss.setStyle("K" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 11) {
                    jss.setStyle("L" + count, "background-color", "yellow");
                    jss.setStyle("L" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("L" + count, "background-color", "red");
                    //     jss.setStyle("L" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("L" + count, "background-color", "yellow");
                    //     jss.setStyle("L" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 12) {
                    jss.setStyle("M" + count, "background-color", "yellow");
                    jss.setStyle("M" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("M" + count, "background-color", "red");
                    //     jss.setStyle("M" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("M" + count, "background-color", "yellow");
                    //     jss.setStyle("M" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 13) {
                    jss.setStyle("N" + count, "background-color", "yellow");
                    jss.setStyle("N" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("N" + count, "background-color", "red");
                    //     jss.setStyle("N" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("N" + count, "background-color", "yellow");
                    //     jss.setStyle("N" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 14) {
                    jss.setStyle("O" + count, "background-color", "yellow");
                    jss.setStyle("O" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("O" + count, "background-color", "red");
                    //     jss.setStyle("O" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("O" + count, "background-color", "yellow");
                    //     jss.setStyle("O" + count, "color", "red");
                    // }
                }  
                          
                if (parseInt(nestedValue) == 15) {
                    jss.setStyle("P" + count, "background-color", "yellow");
                    jss.setStyle("P" + count, "color", "red"); 
                    // if(isApprovedCells == true){
                    //     jss.setStyle("P" + count, "background-color", "red");
                    //     jss.setStyle("P" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("P" + count, "background-color", "yellow");
                    //     jss.setStyle("P" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 16) {
                    jss.setStyle("Q" + count, "background-color", "yellow");
                    jss.setStyle("Q" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("Q" + count, "background-color", "red");
                    //     jss.setStyle("Q" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("Q" + count, "background-color", "yellow");
                    //     jss.setStyle("Q" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 17) {
                    jss.setStyle("R" + count, "background-color", "yellow");
                    jss.setStyle("R" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("R" + count, "background-color", "red");
                    //     jss.setStyle("R" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("R" + count, "background-color", "yellow");
                    //     jss.setStyle("R" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 18) {
                    jss.setStyle("S" + count, "background-color", "yellow");
                    jss.setStyle("S" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("S" + count, "background-color", "red");
                    //     jss.setStyle("S" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("S" + count, "background-color", "yellow");
                    //     jss.setStyle("S" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 19) {
                    jss.setStyle("T" + count, "background-color", "yellow");
                    jss.setStyle("T" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("T" + count, "background-color", "red");
                    //     jss.setStyle("T" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("T" + count, "background-color", "yellow");
                    //     jss.setStyle("T" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 20) {
                    jss.setStyle("U" + count, "background-color", "yellow");
                    jss.setStyle("U" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("U" + count, "background-color", "red");
                    //     jss.setStyle("U" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("U" + count, "background-color", "yellow");
                    //     jss.setStyle("U" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 21) {
                    jss.setStyle("V" + count, "background-color", "yellow");
                    jss.setStyle("V" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("V" + count, "background-color", "red");
                    //     jss.setStyle("V" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("V" + count, "background-color", "yellow");
                    //     jss.setStyle("V" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 22) {
                    jss.setStyle("W" + count, "background-color", "yellow");
                    jss.setStyle("W" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("W" + count, "background-color", "red");
                    //     jss.setStyle("W" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("W" + count, "background-color", "yellow");
                    //     jss.setStyle("W" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 23) {
                    jss.setStyle("X" + count, "background-color", "yellow");
                    jss.setStyle("X" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("X" + count, "background-color", "red");
                    //     jss.setStyle("X" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("X" + count, "background-color", "yellow");
                    //     jss.setStyle("X" + count, "color", "red");
                    // }
                }
               
                if (parseInt(nestedValue) == 24) {
                    jss.setStyle("Y" + count, "background-color", "yellow");
                    jss.setStyle("Y" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("Y" + count, "background-color", "red");
                    //     jss.setStyle("Y" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("Y" + count, "background-color", "yellow");
                    //     jss.setStyle("Y" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 25) {
                    jss.setStyle("Z" + count, "background-color", "yellow");
                    jss.setStyle("Z" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("Z" + count, "background-color", "red");
                    //     jss.setStyle("Z" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("Z" + count, "background-color", "yellow");
                    //     jss.setStyle("Z" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 26) {
                    jss.setStyle("AA" + count, "background-color", "yellow");
                    jss.setStyle("AA" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AA" + count, "background-color", "red");
                    //     jss.setStyle("AA" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AA" + count, "background-color", "yellow");
                    //     jss.setStyle("AA" + count, "color", "red");
                    // }
                }
               
                if (parseInt(nestedValue) == 27) {
                    jss.setStyle("AB" + count, "background-color", "yellow");
                    jss.setStyle("AB" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AB" + count, "background-color", "red");
                    //     jss.setStyle("AB" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AB" + count, "background-color", "yellow");
                    //     jss.setStyle("AB" + count, "color", "red");
                    // }
                }
               
                if (parseInt(nestedValue) == 28) {
                    jss.setStyle("AC" + count, "background-color", "yellow");
                    jss.setStyle("AC" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AC" + count, "background-color", "red");
                    //     jss.setStyle("AC" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AC" + count, "background-color", "yellow");
                    //     jss.setStyle("AC" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 29) {
                    jss.setStyle("AD" + count, "background-color", "yellow");
                    jss.setStyle("AD" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AD" + count, "background-color", "red");
                    //     jss.setStyle("AD" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AD" + count, "background-color", "yellow");
                    //     jss.setStyle("AD" + count, "color", "red");
                    // }
                }
               
                if (parseInt(nestedValue) == 30) {
                    jss.setStyle("AE" + count, "background-color", "yellow");
                    jss.setStyle("AE" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AE" + count, "background-color", "red");
                    //     jss.setStyle("AE" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AE" + count, "background-color", "yellow");
                    //     jss.setStyle("AE" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 31) {
                    jss.setStyle("AF" + count, "background-color", "yellow");
                    jss.setStyle("AF" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AF" + count, "background-color", "red");
                    //     jss.setStyle("AF" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AF" + count, "background-color", "yellow");
                    //     jss.setStyle("AF" + count, "color", "red");
                    // }
                }
                
                if (parseInt(nestedValue) == 32) {
                    jss.setStyle("AG" + count, "background-color", "yellow");
                    jss.setStyle("AG" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AG" + count, "background-color", "red");
                    //     jss.setStyle("AG" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AG" + count, "background-color", "yellow");
                    //     jss.setStyle("AG" + count, "color", "red");
                    // }
                }
               
                if (parseInt(nestedValue) == 33) {
                    jss.setStyle("AH" + count, "background-color", "yellow");
                    jss.setStyle("AH" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AH" + count, "background-color", "red");
                    //     jss.setStyle("AH" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AH" + count, "background-color", "yellow");
                    //     jss.setStyle("AH" + count, "color", "red");
                    // }
                }
               
                if (parseInt(nestedValue) == 34) {
                    jss.setStyle("AI" + count, "background-color", "yellow");
                    jss.setStyle("AI" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AI" + count, "background-color", "red");
                    //     jss.setStyle("AI" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AI" + count, "background-color", "yellow");
                    //     jss.setStyle("AI" + count, "color", "red");
                    // }                    
                }
                
                if (parseInt(nestedValue) == 35) {
                    jss.setStyle("AJ" + count, "background-color", "yellow");
                    jss.setStyle("AJ" + count, "color", "red");
                    // if(isApprovedCells == true){
                    //     jss.setStyle("AJ" + count, "background-color", "red");
                    //     jss.setStyle("AJ" + count, "color", "black");
                    // }else{
                    //     jss.setStyle("AJ" + count, "background-color", "yellow");
                    //     jss.setStyle("AJ" + count, "color", "red");
                    // }
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
            
            //pending cells color
            var bCYRCellPending = value['42'];
            var arrBCYRCellPending = bCYRCellPending.split(',');
            $.each(arrBCYRCellPending, function (nextedIndex, nestedValue2) {              
                if (parseInt(nestedValue2) == 1) {
                    jss.setStyle("B" + count, "background-color", "red");
                    jss.setStyle("B" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 2) {
                    jss.setStyle("C" + count, "background-color", "red");
                    jss.setStyle("C" + count, "color", "black");
                }

                if (parseInt(nestedValue2) == 3) {
                    jss.setStyle("D" + count, "background-color", "red");
                    jss.setStyle("D" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 4) {
                    jss.setStyle("E" + count, "background-color", "red");
                    jss.setStyle("E" + count, "color", "black");
                }
 
                if (parseInt(nestedValue2) == 5) {
                    jss.setStyle("F" + count, "background-color", "red");
                    jss.setStyle("F" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 6) {
                    jss.setStyle("G" + count, "background-color", "red");
                    jss.setStyle("G" + count, "color", "black");
                }

                if (parseInt(nestedValue2) == 7) {
                    jss.setStyle("H" + count, "background-color", "red");
                    jss.setStyle("H" + count, "color", "black");
                }
             
                if (parseInt(nestedValue2) == 8) {
                    jss.setStyle("I" + count, "background-color", "red");
                    jss.setStyle("I" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 9) {
                    jss.setStyle("J" + count, "background-color", "red");
                    jss.setStyle("J" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 10) {
                    jss.setStyle("K" + count, "background-color", "red");
                    jss.setStyle("K" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 11) {
                    jss.setStyle("L" + count, "background-color", "red");
                    jss.setStyle("L" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 12) {
                    jss.setStyle("M" + count, "background-color", "red");
                    jss.setStyle("M" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 13) {
                    jss.setStyle("N" + count, "background-color", "red");
                    jss.setStyle("N" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 14) {
                    jss.setStyle("O" + count, "background-color", "red");
                    jss.setStyle("O" + count, "color", "black");
                }
             
                if (parseInt(nestedValue2) == 15) {
                    jss.setStyle("P" + count, "background-color", "red");
                    jss.setStyle("P" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 16) {
                    jss.setStyle("Q" + count, "background-color", "red");
                    jss.setStyle("Q" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 17) {
                    jss.setStyle("R" + count, "background-color", "red");
                    jss.setStyle("R" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 18) {
                    jss.setStyle("S" + count, "background-color", "red");
                    jss.setStyle("S" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 19) {
                    jss.setStyle("T" + count, "background-color", "red");
                    jss.setStyle("T" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 20) {
                    jss.setStyle("U" + count, "background-color", "red");
                    jss.setStyle("U" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 21) {
                    jss.setStyle("V" + count, "background-color", "red");
                    jss.setStyle("V" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 22) {
                    jss.setStyle("W" + count, "background-color", "red");
                    jss.setStyle("W" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 23) {
                    jss.setStyle("X" + count, "background-color", "red");
                    jss.setStyle("X" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 24) {
                    jss.setStyle("Y" + count, "background-color", "red");
                    jss.setStyle("Y" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 25) {
                    jss.setStyle("Z" + count, "background-color", "red");
                    jss.setStyle("Z" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 26) {
                    jss.setStyle("AA" + count, "background-color", "red");
                    jss.setStyle("AA" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 27) {
                    jss.setStyle("AB" + count, "background-color", "red");
                    jss.setStyle("AB" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 28) {
                    jss.setStyle("AC" + count, "background-color", "red");
                    jss.setStyle("AC" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 29) {
                    jss.setStyle("AD" + count, "background-color", "red");
                    jss.setStyle("AD" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 30) {
                    jss.setStyle("AE" + count, "background-color", "red");
                    jss.setStyle("AE" + count, "color", "black");
                }
                
                if (parseInt(nestedValue2) == 31) {
                    jss.setStyle("AF" + count, "background-color", "red");
                    jss.setStyle("AF" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 32) {
                    jss.setStyle("AG" + count, "background-color", "red");
                    jss.setStyle("AG" + count, "color", "black");
                }
              
                if (parseInt(nestedValue2) == 33) {
                    jss.setStyle("AH" + count, "background-color", "red");
                    jss.setStyle("AH" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 34) {
                    jss.setStyle("AI" + count, "background-color", "red");
                    jss.setStyle("AI" + count, "color", "black");
                }
                if (parseInt(nestedValue2) == 35) {
                    jss.setStyle("AJ" + count, "background-color", "red");
                    jss.setStyle("AJ" + count, "color", "black");
                }
            });
        }       
        if (value['38'] == false && value['39'] == false && value['44'] == false) {
            //DisableRow(count);
            SetColorCommonRow(count,"gray","black","deleted");
        }
        else if(value['43'] == true || value['44'] == true){
            SetColorCommonRow(count,"red","black","editable");
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

//modal button close functions
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

var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');    
}

//update jexcel cell data to an array
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

//update jexcel add employee row to the array
function updateArrayForInsert(array, retrivedData, x,y, cell, value, beforeChangedValue) {
    var index = array.findIndex(d => d.assignmentId == retrivedData.assignmentId);
    array[index].assignmentId = retrivedData.assignmentId;
    array[index].employeeId = retrivedData.employeeId;
    array[index].employeeName = retrivedData.employeeName;
    
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].rowType = retrivedData.rowType;
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
        var octSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                octSum += parseFloat(dataValue[11]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
            octSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                novSum += parseFloat(dataValue[12]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
            novSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                decSum += parseFloat(dataValue[13]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
            decSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                janSum += parseFloat(dataValue[14]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
            janSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                febSum += parseFloat(dataValue[15]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
            febSum = 1;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                marSum += parseFloat(dataValue[16]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
            marSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                aprSum += parseFloat(dataValue[17]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
            aprSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                maySum += parseFloat(dataValue[18]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                junSum += parseFloat(dataValue[19]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
            junSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                julSum += parseFloat(dataValue[20]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                augSum += parseFloat(dataValue[21]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
            augSum = 0;
            alert('入力値が不正です');
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
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
                sepSum += parseFloat(dataValue[22]);
            }

        });
        if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
            sepSum = 0;
            alert('入力値が不正です');
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
    
    if(array[index].bCYRCell.length <= retrivedData.bCYRCell.length){
        array[index].bCYRCell= retrivedData.bCYRCell;  
    }            
}

// set information to the object for retrive
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
        year: document.getElementById('selected_budget_year').value,
        bcyr: rowData[36],
        bCYRCell: rowData[37],
        isActive: rowData[38],
        bCYRApproved: rowData[39],
        bCYRCellApproved: rowData[40],
        isApproved: rowData[41],
        bCYRCellPending: rowData[42],
        isRowPending: rowData[43],
        isDeletePending: rowData[44],
        rowType: rowData[45],
    };
}

//delete the row data
function DeleteRecords() {
    $.getJSON(`/api/utilities/DeleteAssignments/`)
        .done(function (data) {
            //$('#department_search').empty();
            //$('#department_search').append(`<option value=''>部署を選択</option>`);
            //$.each(data, function (key, item) {
            //    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
            //});
        });
}


//add/register new employee
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
                    console.log("result: "+result);
                    console.log("globalY: "+globalY);

                    jss.setValueFromCoords(35, globalY, result, false);
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('データが保存されました!');
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

//updating the budget infromations
function UpdateBudget() {    
    $("#update_forecast").modal("hide");
    $("#jspreadsheet").hide();    
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
    var promptValue = "";
    
    var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getDate();
        var year = dateObj.getUTCFullYear();
        var miliSeconds = dateObj.getMilliseconds();
        var timestamp = `${year}${month}${day}${miliSeconds}_`;

        if (jssUpdatedData.length > 0) {           
                updateMessage = "Successfully data updated";
                $.ajax({
                    url: `/api/utilities/UpdateBudgetData`,
                    contentType: 'application/json',
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode }),
                    success: function (data) {
                        var year = $("#budget_years").val();
                        ShowBedgetResults(year);

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
            alert("There is no data to update.");
            $("#jspreadsheet").show();
            //$("#head_total").show();
            LoaderHide();
            //alert('追加、修正していないデータがありません!');
            updateMessage = ""
        }

        if (jssInsertedData.length > 0) {
            var elementIndex = jssInsertedData.findIndex(object => {
                return object.employeeName.toLowerCase() == 'total';
            });
            if (elementIndex >= 0) {
                jssInsertedData.splice(elementIndex, 1);
            }

            
            var update_timeStampId = $("#timeStamp_ForUpdateData").val();
            
                insertMessage = "Successfully data inserted.";
                $.ajax({
                    url: `/api/utilities/ExcelAssignment/`,
                    contentType: 'application/json',
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    //data: JSON.stringify(jssInsertedData),
                    data: JSON.stringify({ ForecastUpdateHistoryDtos: jssInsertedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode, TimeStampId: update_timeStampId }),
                    success: function (data) {
                        var allJexcelData = jss.getData();
                        for (let i = 0; i < data.length; i++) {

                            $.each(allJexcelData, (index, dataValue) => {
                                if (data[i].assignmentId == dataValue[0]) {
                                    jss.setValueFromCoords(0, index, data[i].returnedId, false);
                                }

                            });
                        }

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

        if (deletedExistingRowIds.length > 0) {
            var year = $("#budget_years").val();
            $.ajax({
                url: `/api/utilities/ExcelDeleteAssignment/`,
                contentType: 'application/json',
                type: 'DELETE',
                async: false,
                dataType: 'json',
                //data: JSON.stringify(deletedExistingRowIds),
                data: JSON.stringify({ ForecastUpdateHistoryDtos: "", HistoryName: timestamp + promptValue,TimeStampId: update_timeStampId,DeletedRowIds: deletedExistingRowIds,Year:year}),
                success: function (data) {
                    alert(data);
                }
            });

            $("#timeStamp_ForUpdateData").val('');
            var chat = $.connection.chatHub;
            $.connection.hub.start();
            // Start the connection.
            $.connection.hub.start().done(function () {
                chat.server.send('data has been deleted by ', userName);
            });
            $("#jspreadsheet").show();
            //$("#head_total").show();
            LoaderHide();
            deletedExistingRowIds = [];
        }

    if (updateMessage == "" && insertMessage == "") {
        $("#header_show").html("");
        $("#update_forecast").modal("show");
        $("#save_modal_header").html("変更されていないので、保存できません");
        $("#back_button_show").css("display", "none");
        $("#save_btn_modal").css("display", "none");

        $("#close_save_modal").css("display", "block");
    }
    else if (updateMessage != "" && insertMessage != "") {
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
    else if (updateMessage != "") {
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
    else if (insertMessage != "") {
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
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

/*
    author: sudipto.
    import data from excel file.
*/
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
            $.connection.hub.start().done(function () {
                chat.server.send('data has been inserted by ', userName);
            });
        }
    });
}

/*
    author: sudipto.
    get all the forecasted year. and build the year dropdown.
*/
function GetAllBudgetYear() {
    $.ajax({
        url: `/api/utilities/GetBudgetYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#budget_years').append(`<option value=''>予算年度を選択</option>`);
            $.each(data, function (index, element) {
                $('#budget_years').append(`<option value='${element.Year}_1'>${element.Year}年初期</option>`);
                if(element.SecondHalfBudget){
                    $('#budget_years').append(`<option value='${element.Year}_2'>${element.Year}年下半期</option>`);
                }else{
                    $('#budget_years').append(`<option value='${element.Year}_2' disabled style='gray;'>${element.Year}年下半期</option>`);
                }
            });
        }
    });
}

/*
    author: sudipto.
    get all finalize year list.
*/
function GetAllFinalizeYear() {
    $.ajax({
        url: `/api/utilities/GetBudgetFinalizeYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            if(data==''){                                
                $("#duplciateYear").prop('disabled', true);
                $('#select_duplicate_budget_type').empty();                
            }else{
                $("#duplciateYear").prop('disabled', true);
                $('#select_duplicate_budget_type').empty();  

                $('#duplicate_from').append(`<option value=''>予算年度を選択</option>`);
                $.each(data, function (index, element) {
                    $('#duplicate_from').append(`<option value='${element.Year}'>${element.Year}</option>`);                
                });
            }            
        }
    });
}

//auto select csv improt year and budget type when modal is open.
function SelectImportBudgetYearAndType(){
    $.ajax({
        url: `/api/utilities/GetImportYearAndBudgetType`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            //auto select the year
            $('#select_import_year').val(data.Year);

            //auto select the budget type
            $('#select_budget_type').empty();
            $('#select_budget_type').append(`<option value="">select type</option>`);

            //create fist half budget dropdown            
            if(data.FirstHalfFinalize){
                $('#select_budget_type').append(`<option value="1" disabled style='color:red;'>${data.Year} Initial Budget Created</option>`);
            }else if(data.FirstHalfBudget){
                $('#select_budget_type').append(`<option value="1" disabled style='color:orange;'>${data.Year} Initial Budget Created But Not Finalize</option>`);
            }else if(!data.FirstHalfBudget){
                $('#select_budget_type').append(`<option value="1" selected >${data.Year} Initial Budget</option>`);
            }  
            
            //create second half budget dropdown
            if(data.SecondHalfFinalize){
                $('#select_budget_type').append(`<option value="2" disabled style='color:red;'>${data.Year} 2nd Half Budget Created</option>`);
            }else if(data.SecondHalfBudget){
                $('#select_budget_type').append(`<option value="2" disabled style='color:orange;'>${data.Year} 2nd Half Budget Created But Not Finalize</option>`);
            }else if(!data.SecondHalfBudget){
                if(data.FirstHalfBudget){
                    $('#select_budget_type').append(`<option value="2" selected>${data.Year} 2nd Half Budget</option>`);
                }else{
                    $('#select_budget_type').append(`<option value="2" disabled style='color:gray;'>${data.Year} 2nd Half Budget</option>`);
                }                                               
            }       
        }
    });
}

//auto select duplicatedata year and budget type when modal is open.
function SelectDuplicateBudgetYearAndType(){
    $.ajax({
        url: `/api/utilities/GetImportYearAndBudgetType`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            //auto select the year
            $('#duplciateYear').val(data.Year);

            //auto select the budget type
            $('#select_duplicate_budget_type').empty();
            $('#select_duplicate_budget_type').append(`<option value="">select type</option>`);

            //create fist half budget dropdown            
            if(data.FirstHalfFinalize){
                $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:red;'>${data.Year} Initial Budget Created</option>`);
            }else if(data.FirstHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:orange;'>${data.Year} Initial Budget Created But Not Finalize</option>`);
            }else if(!data.FirstHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="1" selected >${data.Year} Initial Budget</option>`);
            }  
            
            //create second half budget dropdown
            if(data.SecondHalfFinalize){
                $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:red;'>${data.Year} 2nd Half Budget Created</option>`);
            }else if(data.SecondHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:orange;'>${data.Year} 2nd Half Budget Created But Not Finalize</option>`);
            }else if(!data.SecondHalfBudget){
                if(data.FirstHalfBudget){
                    $('#select_duplicate_budget_type').append(`<option value="2" selected>${data.Year} 2nd Half Budget</option>`);
                }else{
                    $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:gray;'>${data.Year} 2nd Half Budget</option>`);
                }                                               
            }       
        }
    });
}

/*
    author: sudipto.
    validate replicate data. 
*/
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        // $('#duplciateYear').val(parseInt(year)+1);
        $('#replicate_from').val(year);
    }else{
        // $('#duplciateYear').val('');
        $('#replicate_from').val('');
    }
}

/*
    author: sudipto.
    replicate the budget data from previous year budget.
*/
function DuplicateBudget(){    
    $("#validation_message").html("");

    var fromDate = $('#duplicate_from').val();
    var toDate  = $('#duplciateYear').val();
    var budgetType  = $('#select_duplicate_budget_type').val();
 
    if (fromDate == null || fromDate == undefined || fromDate == "") {
        alert("Please select from date!")
        return false;
    }
    if (toDate == null || toDate == undefined || toDate == "") {
        alert("Please select to date!")
        return false;
    }
    if (budgetType == null || budgetType == undefined || budgetType == "") {
        alert("Please select to budget type!")
        return false;
    }
    
    if(fromDate!="" && toDate!=""){
        $("#replicate_from_previous_year").modal("hide");
        $("#loading").css("display", "block");
        $.ajax({
            url: `/api/utilities/DuplicateForecastYear`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: "copyYear=" + fromDate+"&insertYear="+toDate+"&budgetType="+budgetType,
            success: function (data) {                       
                if(parseInt(data)==5){
                    $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Data has already imported to " + toDate + ".Please chooose another year to import data..</span>");                        
                }
                else if(parseInt(data)==6){
                    $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>"+fromDate+" has no data to copy!</span>");                        
                }
                else{
                    $("#validation_message").html("<span id='validation_message_success' style='margin-left: 28px;'>Data has successfully replicated to "+toDate+".</span>");                        
                    // if(parseInt(data)>0){
                    //     $("#validation_message").html("<span id='validation_message_success' style='margin-left: 28px;'>インポートデータは正常に処理されました "+toDate+".</span>");                        
                    // }else{
                    //     $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Failed to Replicate the data!</span>");                        
                    // }
                }
                LoaderHide();
                //window.location.reload();                
            }
        });
    }else{
        $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Failed to Replicate the data!</span>");                        
        return false;
    }
}

/*
    author: sudipto.
    budget validation and submit the import file to server.
*/
function validate(){
    // var selectedYear = $('#select_import_year').find(":selected").val();
    var selectedYear = $('#select_import_year').val();
    var import_file = $('#import_file_excel').val();
   
    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("please 年度を選択してください!");
        return false;
    }else if(import_file =="" || typeof import_file === "undefined"){
        alert("please select import file!");
        return false;
    }else { return true; }
}

$('#frm_import_year_data').submit(validate);

/*
    author: sudipto.
    date:17July23.
    approve,not approved and deleted rows color code functions.
*/
function SetColorCommonRow(rowNumber,backgroundColor,textColor,requestType){         
    if(requestType != "deleted"){
        $(jss.getCell("A" + (rowNumber))).removeClass('readonly');
    }    
    jss.setStyle("A"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("A"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("A" + (rowNumber))).addClass('readonly');
    }    

    if(requestType != "deleted"){
        $(jss.getCell("B" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("B"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("B"+rowNumber,"color", textColor);    
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("B" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("C" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("C"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("C"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("C" + (rowNumber))).addClass('readonly');
    }


    if(requestType != "deleted"){
        $(jss.getCell("D" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("D"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("D"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("D" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("E" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("E"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("E"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("E" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("F" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("F"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("F"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("F" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("G" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("G"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("G"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("G" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("H" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("H"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("H"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("H" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("I" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("I"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("I"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("I" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("J" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("J"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("J"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("J" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("K" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("K"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("K"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("K" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("L" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("L"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("L"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("L" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("M" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("M"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("M"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("M" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("N" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("N"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("N"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("N" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("O" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("O"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("O"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("O" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("P" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("P"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("P"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("P" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("Q" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("Q"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Q"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("Q" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("R" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("R"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("R"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("R" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("S" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("S"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("S"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("S" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("T" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("T"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("T"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("T" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("U" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("U"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("U"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("U" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("V" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("V"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("V"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("V" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("W" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("W"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("W"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("W" + (rowNumber))).addClass('readonly');
    }

    $(jss.getCell("X" + (rowNumber))).removeClass('readonly');
    jss.setStyle("X"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("X"+rowNumber,"color", textColor);
    $(jss.getCell("X" + (rowNumber))).addClass('readonly');

    $(jss.getCell("Y" + (rowNumber))).removeClass('readonly');
    jss.setStyle("Y"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Y"+rowNumber,"color", textColor);
    $(jss.getCell("Y" + (rowNumber))).addClass('readonly');

    $(jss.getCell("Z" + (rowNumber))).removeClass('readonly');
    jss.setStyle("Z"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Z"+rowNumber,"color", textColor);
    $(jss.getCell("Z" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AA" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AA"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AA"+rowNumber,"color", textColor);
    $(jss.getCell("AA" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AB" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AB"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AB"+rowNumber,"color", textColor);
    $(jss.getCell("AB" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AC" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AC"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AC"+rowNumber,"color", textColor);
    $(jss.getCell("AC" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AD" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AD"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AD"+rowNumber,"color", textColor);
    $(jss.getCell("AD" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AE" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AE"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AE"+rowNumber,"color", textColor);
    $(jss.getCell("AE" + (rowNumber))).addClass('readonly');
    
    $(jss.getCell("AF" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AF"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AF"+rowNumber,"color", textColor);
    $(jss.getCell("AF" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AG" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AG"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AG"+rowNumber,"color", textColor);
    $(jss.getCell("AG" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AH" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AH"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AH"+rowNumber,"color", textColor);
    $(jss.getCell("AH" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AI" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AI"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AI"+rowNumber,"color", textColor);
    $(jss.getCell("AI" + (rowNumber))).addClass('readonly');

    // $(jss.getCell("AJ" + (rowNumber))).removeClass('readonly');
    // jss.setStyle("AJ"+rowNumber,"background-color", backgroundColor);
    // jss.setStyle("AJ"+rowNumber,"color", textColor);
    // $(jss.getCell("AJ" + (rowNumber))).addClass('readonly');
}

/*
    author: sudipto
    after cell change, store that cell information into hidden field.
*/
function CheckIfAlreadyExists(selectedCells,assignmentId){
    var isCellExists = false;    

	if (previousChangedCells != '' && previousChangedCells != null && previousChangedCells != undefined){
        var arrPreviousCells = previousChangedCells.split(",");                                
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");
            if(arrNestedCells[0] == assignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }      
        })        
    } 
    return isCellExists;  
}

//store the changed data
function StoreChangeCellData(selectedCells,assignmentId){
	var changedCellStored = "";
    var isCellExists = false;    

	if (previousChangedCells != '' && previousChangedCells != null && previousChangedCells != undefined){

        var arrPreviousCells = previousChangedCells.split(",");                                
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");  
            if(arrNestedCells[0] == assignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }   

            if(changedCellStored==""){
                changedCellStored = arrNestedCells[0]+"_"+arrNestedCells[1];
            }else{
                var arrChangedCellStored = changedCellStored.split(",");
                var isExists =false;

                $.each(arrChangedCellStored, function (nextedIndex, nestedValue2){
                    var arrNestedValue2 = nestedValue2.split("_");
                    if(arrNestedValue2[0] == arrNestedCells[0] && arrNestedValue2[1] == arrNestedCells[1]){
                        isExists = true;
                    }      
                })   
                if(!isExists){
                    changedCellStored = changedCellStored +","+arrNestedCells[0]+"_"+arrNestedCells[1];
                }                
            } 
        })

        if(!isCellExists){
            if(changedCellStored == ""){
                changedCellStored = assignmentId+"_"+selectedCells;     
            }else{
                changedCellStored = changedCellStored +","+assignmentId+"_"+selectedCells;     
            }
        }
    }
    else{
        changedCellStored = assignmentId+"_"+selectedCells;     
    }	
}

//cell wise color function
function SetColorForCells(strBackgroundColor, strTextColor, CellPosition) {
	jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
}

//cell wise color function
function SetColorForCostsCells(strBackgroundColor,strTextColor,CellPosition){
	$(jss.getCell(CellPosition)).removeClass('readonly');
    jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
    $(jss.getCell(CellPosition)).addClass('readonly');
}

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

function ClearnAllJexcelData(){
    globalSearchObject = '';
    globalPreviousValue = '0.0';
    globalPreviousId = '';
    jss;
    globalX = 0;
    globalY = 0;
    newRowCount = 1;
    beforeChangedValue = 0;
    jssUpdatedData = [];
    jssInsertedData = [];
    allEmployeeName = [];
    allEmployeeName1 = [];
    cellwiseColorCode = [];
    cellwiseColorCodeForInsert = [];
    changeCount = 0;
    newRowChangeEventFlag = false;
    deletedExistingRowIds = [];
}
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
$(document).ready(function () {

});
function ShowForecastResults(year,strPageType) {
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
        alert('年度を選択してください');
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
        url: `/api/utilities/GetAllAssignmentData`,
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
    var _retriveTotal = [];        
    //get total man month
    $.ajax({
        url: `/api/utilities/GetTotalCalculationForManmonthAndCost`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "year=" + year,
        success: function (data) {
            _retriveTotal = data;           
        }
    });


    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }
    var w = window.innerWidth;
    var h = window.innerHeight;
    
    var yearHeaderTitleForPoints = "";
    var yearHeaderTitleForCosts = "";
    yearHeaderTitleForPoints = "FY"+year+" 見通し";
    yearHeaderTitleForCosts = "FY"+year+" コスト見通し";
    
    var octSumFormula = "=SUM(L3:L13)";
    var novSumFormula = "=SUM(M3:M13)";
    var octTotalPoints = "";
    octTotalPoints = "<label id='oct_total_points'>"+_retriveTotal.OctTotalMM+"</label>"

    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        filters: true,
        allowComments:true,
        tableOverflow: true,
        freezeColumns: 3,
        //defaultColWidth: 75,
        tableWidth: w-280+ "px",
        tableHeight: (h-150) + "px",           
        minDimensions: [6, 10],
        columnSorting: true,
        oninsertrow: newRowInserted,

        nestedHeaders:[
            [
                {
                    title: '',
                    colspan: '10',
                },
                //month wise total points
                {
                    title: _retriveTotal.OctTotalMM,                    
                    type: "decimal",
                    name: "octSumFormula",
                    mask: '#.##,0',
                    decimal: '.'                                       
                },
                {
                    title: _retriveTotal.NovTotalMM,                    
                    type: "decimal",
                    name: "=SUM(M3:B3)",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.DecTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.JanTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.FebTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.MarTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.AprTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.MayTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.JunTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.JulTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.AugTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.SepTotalMM,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.TotalManMonth,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                //month wise total cost
                {
                    title: _retriveTotal.OctTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.NovTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.DecTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.JanTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.FebTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.MarTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.AprTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.MayTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.JunTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.JulTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.AugTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.SepTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },

                {
                    title: _retriveTotal.TotalCosts,
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
            ],
            [
                {
                    title: '',
                    colspan: '9',
                },
                {
                    title: '',
                    colspan: '1',
                },
                {
                    title: yearHeaderTitleForPoints,
                    colspan: '12',
                },
                
                {
                    title: '',
                    colspan: '1',
                },{
                    title: yearHeaderTitleForCosts,
                    colspan: '12',
                },
                {
                    title: '',
                    colspan: '1',
                },
            ],
        ],        
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
                    
                    var element = $(`.jexcel > tbody > tr:nth-of-type(${row+1})`);
                    var companyName = element[0].cells[9].innerText;
                    console.log('company filter:' + companyName);
                    if (companyName.toLowerCase() !== "mw") {
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
            //cost
            {
                title: "計画工数",
                type: "decimal",
                name: "TotalManMonth",
                mask: '#.##,0',
                decimal: '.',
                backgroundColor:"#f46e42",                
                readOnly: true,      
            },
            {
                title: "10月",
                type: "number",
                readOnly: true,
                mask: "#,##0",
                name: "OctTotal",
                width: 75
            },
            {
                title: "11月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "NovTotal",
                width: 75
            },
            {
                title: "12月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "DecTotal",
                width: 75
            },            
            {
                title: "1月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "JanTotal",
                width: 75
            },
            {
                title: "2月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "FebTotal",
                width: 75
            },
            {
                title: "3月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "MarTotal",
                width: 75
            },
            {
                title: "4月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "AprTotal",
                width: 75
            },
            {
                title: "5月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "MayTotal",
                width: 75
            },
            {
                title: "6月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "JunTotal",
                width: 75
            },
            {
                title: "7月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "JulTotal",
                width: 75
            },
            {
                title: "8月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "AugTotal",
                width: 75
            },
            {
                title: "9月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "SepTotal",
                width: 75
            },
            {
                title: "実績・見通し",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "TotalCost",
                width: 75
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
            { title: "RowType", type: 'hidden', name: "RowType" },

            { title: "DuplicateFrom", type: 'text', name: "DuplicateFrom" },
            { title: "DuplicateCount", type: 'text', name: "DuplicateCount" },
            { title: "RoleChanged", type: 'text', name: "RoleChanged" },
            { title: "UnitPriceChanged", type: 'text', name: "UnitPriceChanged" },
            
            
        ],        
        
        onbeforechange: function (instance, cell, x, y, value) {
            console.log("onbeforechange");
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
            var checkId = jss.getValueFromCoords(0, y);
            var employeeId = jss.getValueFromCoords(37, y);            

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
                console.log("changed");
                var retrivedData = retrivedObject(jss.getRowData(y));
                if (retrivedData.assignmentId.toString().includes('new')) {
                    updateArrayForInsert(jssInsertedData, retrivedData, x,y, cell, value, beforeChangedValue);
                }
                else {
                    var dataCheck = jssUpdatedData.filter(d => d.assignmentId == retrivedData.assignmentId);
                    console.log(jssUpdatedData);
                    if (retrivedData.companyId != 3) {
                        retrivedData.gradeId = '';
                    }
                    
                    var isUnapprovedDeletedRow = retrivedData.isDeletePending;                                                        
                    
                    if(isUnapprovedDeletedRow){
                        var isCellAlreadyChanged = false;
                        if(!isCellAlreadyChanged){                                
                            SetColorForCells("white","black","B"+(parseInt(y)+1))                                
                        }
                    }

                    if (x == 2) {
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
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
                    else{                         
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(2,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","C"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 3) {
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
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
                    else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(3,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","D"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 4) {
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(4,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","E"+(parseInt(y)+1))                                
                            }
                        }
                    }
                    
                    if (x == 5) {
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(5,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","F"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 6) {
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(6,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","G"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 7) {
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(7,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","H"+(parseInt(y)+1))                                
                            }
                        }
                    }
                    // for company
                    if (x == 8) {                        ;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var rowNumber = parseInt(y) + 1;
                        var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                        console.log(element);
                        var companyName = element[0].cells[9].innerText;
                        console.log('copany ' + companyName);
                        if (companyName.toLowerCase() !== 'mw') {
                            element[0].cells[10].innerText = '';
                            $(jss.getCell("J" + rowNumber)).addClass('readonly');
                            $(jss.getCell("J" + rowNumber)).css('color', 'black');
                            $(jss.getCell("J" + rowNumber)).css('background-color', 'white');
                            jss.setValueFromCoords(10, parseInt(y), 0, false);
                            
                        }
                        else {
                            $(jss.getCell("J" + rowNumber)).removeClass('readonly');
                            $(jss.getCell("J" + rowNumber)).css('color', 'black');
                            $(jss.getCell("J" + rowNumber)).css('background-color', 'white'); 
                            jss.setValueFromCoords(10, parseInt(y), 0, false);
                        }

                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                            console.log('clouser');
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x); 
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(8,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","I"+(parseInt(y)+1))                                
                            }
                        }
                    }
                    // for grade
                    if (x == 9) {
                        debugger;
                        var rowNumber = parseInt(y) + 1;
                        var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                        var companyName = element[0].cells[9].innerText;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }


                        var cellValue = jss.getValueFromCoords(8, y);
                        if (companyName.toLowerCase() == 'mw') {
                            $.ajax({
                                url: '/api/Salaries?salaryGradeId=' + value,
                                contentType: 'application/json',
                                type: 'GET',
                                async: false,
                                dataType: 'json',
                                success: function (salary) {
                                    jss.setValueFromCoords(10, parseInt(y), salary.SalaryLowPoint, false);
                                }
                            });
                        }

                        if (dataCheck.length == 0) {
                           jssUpdatedData.push(retrivedData);
                        }
                        else {
                           updateArray(jssUpdatedData, retrivedData);
                        }
                       

                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(9,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","J"+(parseInt(y)+1))                                
                            }
                        }
                    }
                    // for unit price
                    if (x == 10) {
                                debugger;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheck.length == 0) {
                            jssUpdatedData.push(retrivedData);
                        }
                        else {
                            updateArray(jssUpdatedData, retrivedData);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(10,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","K"+(parseInt(y)+1))                                
                            }
                        }
                    }
                    

                    if (x == 11) {
                        var octPointsSum = 0;
                        var octCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var octSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {  
                            if (dataValue[11] != "" && dataValue[11] != null && dataValue[11] != undefined) {
                                var octPointPerRow = 0.0;
                                octPointPerRow = parseFloat(dataValue[11]).toFixed(1);
                                octPointsSum += parseFloat(octPointPerRow);                            
                                octCostSum = parseFloat(octCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[11]);     
                            }

                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                octSum += parseFloat(parseFloat(dataValue[11]));
                            }
                        });       

                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[2].innerText= parseFloat(octPointsSum).toFixed(1);

                        octCostSum = new Intl.NumberFormat().format(octCostSum)
                        element[0].cells[15].innerText= octCostSum;                        

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);  
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(11,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","L"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 12) {
                        var novPointsSum = 0;
                        var novCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var novSum = 0;

                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[12] != "" && dataValue[12] != null && dataValue[12] != undefined) {
                                var novPointPerRow = 0.0;
                                novPointPerRow = parseFloat(dataValue[12]).toFixed(1);
                                novPointsSum += parseFloat(novPointPerRow);   

                                novCostSum = parseFloat(novCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[12]);   
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                novSum += parseFloat(dataValue[12]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[3].innerText= novPointsSum.toFixed(1);
                        novCostSum = new Intl.NumberFormat().format(novCostSum)
                        element[0].cells[16].innerText= novCostSum;

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(12,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","M"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 13) {
                        var decPointsSum = 0;
                        var decCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var decSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[13] != "" && dataValue[13] != null && dataValue[13] != undefined){
                                var decPointPerRow = 0.0;
                                decPointPerRow = parseFloat(dataValue[13]).toFixed(1);
                                decPointsSum += parseFloat(decPointPerRow); 
                                
                                decCostSum = parseFloat(decCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[13]);   
                            }                            
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                decSum += parseFloat(dataValue[13]);
                            }

                        });

                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[4].innerText= decPointsSum.toFixed(1);
                        decCostSum = new Intl.NumberFormat().format(decCostSum)
                        element[0].cells[17].innerText= decCostSum;  

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(13,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","N"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 14) {
                        var janPointsSum = 0;
                        var janCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var janSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[14] != "" && dataValue[14] != null && dataValue[14] != undefined){
                                var janPointPerRow = 0.0;
                                janPointPerRow = parseFloat(dataValue[14]).toFixed(1);
                                janPointsSum += parseFloat(janPointPerRow); 

                                janCostSum = parseFloat(janCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[14]);   
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                janSum += parseFloat(dataValue[14]);
                            }
                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[5].innerText= janPointsSum.toFixed(1);
                        janCostSum = new Intl.NumberFormat().format(janCostSum)
                        element[0].cells[18].innerText= janCostSum;

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(14,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","O"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 15) {
                        var febPointsSum = 0;
                        var febCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var febSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[15] != "" && dataValue[15] != null && dataValue[15] != undefined){
                                var febPointPerRow = 0.0;
                                febPointPerRow = parseFloat(dataValue[15]).toFixed(1);
                                febPointsSum += parseFloat(febPointPerRow); 
                                
                                febCostSum = parseFloat(febCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[15]);   
                            }
                            
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                febSum += parseFloat(dataValue[15]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[6].innerText= febPointsSum.toFixed(1);
                        febCostSum = new Intl.NumberFormat().format(febCostSum)
                        element[0].cells[19].innerText= febCostSum; 

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(15,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","P"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 16) {
                        var marPointsSum = 0;
                        var marCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var marSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[16] != "" && dataValue[16] != null && dataValue[16] != undefined){
                                var marPointPerRow = 0.0;
                                marPointPerRow = parseFloat(dataValue[16]).toFixed(1);
                                marPointsSum += parseFloat(marPointPerRow); 
                                
                                marCostSum = parseFloat(marCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[16]);   
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                marSum += parseFloat(dataValue[16]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[7].innerText= marPointsSum.toFixed(1);
                        marCostSum = new Intl.NumberFormat().format(marCostSum)
                        element[0].cells[20].innerText= marCostSum; 

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(16,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","Q"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 17) {
                        var aprPointsSum = 0;
                        var aprCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var aprSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[17] != "" && dataValue[17] != null && dataValue[17] != undefined){
                                var aprPointPerRow = 0.0;
                                aprPointPerRow = parseFloat(dataValue[17]).toFixed(1);
                                aprPointsSum += parseFloat(aprPointPerRow); 
                                
                                aprCostSum = parseFloat(aprCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[17]);
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                aprSum += parseFloat(dataValue[17]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[8].innerText= aprPointsSum.toFixed(1);
                        aprCostSum = new Intl.NumberFormat().format(aprCostSum)
                        element[0].cells[21].innerText= aprCostSum;

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(17,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","R"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 18) {
                        var mayPointsSum = 0;
                        var mayCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var maySum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[18] != "" && dataValue[18] != null && dataValue[18] != undefined){
                                var mayPointPerRow = 0.0;
                                mayPointPerRow = parseFloat(dataValue[18]).toFixed(1);
                                mayPointsSum += parseFloat(mayPointPerRow); 

                                mayCostSum = parseFloat(mayCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[18]); 
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                maySum += parseFloat(dataValue[18]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[9].innerText= mayPointsSum.toFixed(1);
                        mayCostSum = new Intl.NumberFormat().format(mayCostSum)
                        element[0].cells[22].innerText= mayCostSum;

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(18,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","S"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 19) {
                        var junPointsSum = 0;
                        var junCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var junSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[19] != "" && dataValue[19] != null && dataValue[19] != undefined){
                                var junPointPerRow = 0.0;
                                junPointPerRow = parseFloat(dataValue[19]).toFixed(1);
                                junPointsSum += parseFloat(junPointPerRow); 

                                junCostSum = parseFloat(junCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[19]); 
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                junSum += parseFloat(dataValue[19]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[10].innerText= junPointsSum.toFixed(1);
                        junCostSum = new Intl.NumberFormat().format(junCostSum)
                        element[0].cells[23].innerText= junCostSum;     

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(19,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","T"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 20) {
                        var julPointsSum = 0;
                        var julCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var julSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[20] != "" && dataValue[20] != null && dataValue[20] != undefined){
                                var julPointPerRow = 0.0;
                                julPointPerRow = parseFloat(dataValue[20]).toFixed(1);
                                julPointsSum += parseFloat(julPointPerRow); 

                                julCostSum = parseFloat(julCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[20]); 
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                julSum += parseFloat(dataValue[20]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[11].innerText= julPointsSum.toFixed(1);
                        julCostSum = new Intl.NumberFormat().format(julCostSum)
                        element[0].cells[24].innerText= julCostSum;  

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(20,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","U"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 21) {
                        var augPointsSum = 0;
                        var augCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var augSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[21] != "" && dataValue[21] != null && dataValue[21] != undefined){
                                var augPointPerRow = 0.0;
                                augPointPerRow = parseFloat(dataValue[21]).toFixed(1);
                                augPointsSum += parseFloat(augPointPerRow); 

                                augCostSum = parseFloat(augCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[21]); 
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                augSum += parseFloat(dataValue[21]);
                            }

                        });
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[12].innerText= augPointsSum.toFixed(1);
                        augCostSum = new Intl.NumberFormat().format(augCostSum)
                        element[0].cells[25].innerText= augCostSum;           

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(21,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","V"+(parseInt(y)+1))                                
                            }
                        }
                    }

                    if (x == 22) {
                        var sepPointsSum = 0;
                        var sepCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var sepSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[22] != "" && dataValue[22] != null && dataValue[22] != undefined){
                                var sepPointPerRow = 0.0;
                                sepPointPerRow = parseFloat(dataValue[22]).toFixed(1);
                                sepPointsSum += parseFloat(sepPointPerRow); 

                                sepCostSum = parseFloat(sepCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[22]); 
                            }
                            if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                sepSum += parseFloat(dataValue[22]);
                            }

                        });  
                        var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                        element[0].cells[13].innerText= sepPointsSum.toFixed(1);
                        sepCostSum = new Intl.NumberFormat().format(sepCostSum)
                        element[0].cells[26].innerText= sepCostSum;   

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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(22,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white","black","W"+(parseInt(y)+1))                                
                            }
                        }
                    }
                    
                    if(isUnapprovedDeletedRow){
                        var isCellAlreadyChanged = false;
                        if(!isCellAlreadyChanged){                                
                            SetColorForCostsCells("white","black","X"+(parseInt(y)+1));                               
                            SetColorForCostsCells("white","black","Y"+(parseInt(y)+1));                               
                            SetColorForCostsCells("white","black","Z"+(parseInt(y)+1));                               
                            SetColorForCostsCells("white","black","AA"+(parseInt(y)+1));    
                            SetColorForCostsCells("white","black","AB"+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black","AC"+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black","AD"+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black","AE"+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black","AF"+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black","AG"+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black","AH"+(parseInt(y)+1));                                                            
                            SetColorForCostsCells("white","black","AI"+(parseInt(y)+1));                                
                        }
                    }
                }

            }

        },        
        //ondeleterow: deleted,
        contextMenu: function (obj, x, y, e) {
            var items = [];           
            items.push({
                title: '要員を追加 (Add Emp)',
                onclick: function () {
                    obj.insertRow(1, parseInt(y));
                    var insertedRowNumber = parseInt(obj.getSelectedRows(true)) + 2;
                    
                    setTimeout(function () {
                        SetColorCommonRow(parseInt(y)+2,"yellow","red","newrow");  
                        jss.setValueFromCoords(38, (insertedRowNumber - 1), true, false);

                        $('#jexcel_add_employee_modal').modal('show');
                        globalY = parseInt(y) + 1;
                        GetEmployeeList();
                    },1000);
                    
                    
                }
            });
            items.push({
                title: '要員のコピー（単価変更）(unit price)',
                onclick: function () {
                    var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                    if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                        return false;
                    }

                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";
                    var activeEmployeeCount =0;
                    var masterEmployeeName = "";
                    var inactiveEmployeeCount = 0;
                    var _duplicateFrom = "";
                    var _duplicateCount = "";
                    var _roleChanged = "";
                    var _unitPriceChanged = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    _duplicateFrom = retrivedData.assignmentId.toString();
                    if (retrivedData.assignmentId.toString().includes('new')) {                        
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[37] == retrivedData.employeeId) {
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
                            if (allData[x][0] == 'new-'+minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
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
                            if (x[37] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[0])) {
                                    allSameEmployeeId.push(x[0]);
                                }

                            }
                        }

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][0] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;

                        for (let x of allData) {                            
                            if(parseInt(x[37]) == parseInt(retrivedData.employeeId)){
                                activeEmployeeCount = activeEmployeeCount+1;
                            }                          
                        }

                        $.ajax({
                            url: `/api/utilities/GetEmployeeNameForMenuChange`,
                            contentType: 'application/json',
                            type: 'GET',
                            async: false,
                            dataType: 'json',
                            data: "employeeAssignmentId=" + retrivedData.assignmentId+"&employeeId="+retrivedData.employeeId+"&menuType=unit"+"&year="+retrivedData.year,
                            success: function (data) { 
                                masterEmployeeName = data.EmployeeName;
                                inactiveEmployeeCount = data.EmployeeCount;
                            }
                        });                        
                    }    


                    _unitPriceChanged = '1';
                    _roleChanged = '0';
                    _duplicateCount = (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1);
                    //newCountedEmployeeName = masterEmployeeName + " (" + (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1) + ")";
                    obj.setValueFromCoords(1, nextRow, retrivedData.employeeName, false);
                    allSameEmployeeId = [];

                    obj.setValueFromCoords(48, nextRow, _duplicateFrom, false);
                    obj.setValueFromCoords(49, nextRow, _duplicateCount, false);
                    obj.setValueFromCoords(50, nextRow, _roleChanged, false);
                    obj.setValueFromCoords(51, nextRow, _unitPriceChanged, false);
                    obj.setValueFromCoords(37, nextRow, retrivedData.employeeId, false);
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
                    
                    obj.setValueFromCoords(38, nextRow, false, false);
                    obj.setValueFromCoords(39, nextRow, `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`, false);
                    obj.setValueFromCoords(40, nextRow, true, false);
                    obj.setValueFromCoords(47, nextRow, `unit_${retrivedData.assignmentId}_${y}`, false);

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
                    var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                    if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                        return false;
                    }
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";
                    var activeEmployeeCount =0;
                    var masterEmployeeName = "";
                    var inactiveEmployeeCount = 0;
                    var _duplicateFrom = "";
                    var _duplicateCount = "";
                    var _roleChanged = "";
                    var _unitPriceChanged = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    _duplicateFrom = retrivedData.assignmentId.toString();
                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[37] == retrivedData.employeeId) {

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
                            if (allData[x][0] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;


                        for (let x of allData) {
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
                            if (x[37] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[0])) {
                                    allSameEmployeeId.push(x[0]);
                                }
                            }
                        }

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);


                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][0] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;

                        for (let x of allData) {
                            if(parseInt(x[37]) == parseInt(retrivedData.employeeId)){
                                activeEmployeeCount = activeEmployeeCount+1;
                            }  
                        }

                        $.ajax({
                            url: `/api/utilities/GetEmployeeNameForMenuChange`,
                            contentType: 'application/json',
                            type: 'GET',
                            async: false,
                            dataType: 'json',
                            data: "employeeAssignmentId=" + retrivedData.assignmentId+"&employeeId="+retrivedData.employeeId+"&menuType=unit"+"&year="+retrivedData.year,
                            success: function (data) { 
                                masterEmployeeName = data.EmployeeName;
                                inactiveEmployeeCount = data.EmployeeCount;
                            }
                        });
                    }

                    _unitPriceChanged = '0';
                    _roleChanged = '1';
                    _duplicateCount = (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1);
                    //newCountedEmployeeName =   masterEmployeeName +" ("+(parseInt(activeEmployeeCount)+parseInt(inactiveEmployeeCount)+1)+")*";
                    obj.setValueFromCoords(1, nextRow, retrivedData.employeeName, false);
                    allSameEmployeeId = [];                   

                    obj.setValueFromCoords(48, nextRow, _duplicateFrom, false);
                    obj.setValueFromCoords(49, nextRow, _duplicateCount, false);
                    obj.setValueFromCoords(50, nextRow, _roleChanged, false);
                    obj.setValueFromCoords(51, nextRow, _unitPriceChanged, false);
                    obj.setValueFromCoords(37, nextRow, retrivedData.employeeId, false);
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

                    obj.setValueFromCoords(38, nextRow, false, false);
                    obj.setValueFromCoords(39, nextRow, `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`, false);
                    obj.setValueFromCoords(40, nextRow, true, false);
                    obj.setValueFromCoords(47, nextRow, `role_${retrivedData.assignmentId}_${y}`, false);


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
                    var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                    if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                        return false;
                    }
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";
                    var activeEmployeeCount =0;
                    var masterEmployeeName = "";
                    var inactiveEmployeeCount = 0;
                    var _duplicateFrom = "";
                    var _duplicateCount = "";
                    var _roleChanged = "";
                    var _unitPriceChanged = "";
                    
                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    _duplicateFrom = retrivedData.assignmentId.toString();

                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[37] == retrivedData.employeeId) {

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
                            if (allData[x][0] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            if (x[0] == 'new-' + minAssignmentNumber) {
                                newCountedEmployeeName = x[1] + ` (${allSpecificObjectsCount + 1})**`;
                                break;
                            }
                        }
                    } else {
                        newEmployeeId = "new-" + newRowCount;

                        var allSpecificObjectsCount = 0;
                        for (let x of allData) {
                            if (x[37] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[0])) {
                                    allSameEmployeeId.push(x[0]);
                                }
                            }
                        }
                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][0] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            if(parseInt(x[37]) == parseInt(retrivedData.employeeId)){
                                activeEmployeeCount = activeEmployeeCount+1;
                            }  
                        }
                        $.ajax({
                            url: `/api/utilities/GetEmployeeNameForMenuChange`,
                            contentType: 'application/json',
                            type: 'GET',
                            async: false,
                            dataType: 'json',
                            data: "employeeAssignmentId=" + retrivedData.assignmentId+"&employeeId="+retrivedData.employeeId+"&menuType=unit"+"&year="+retrivedData.year,
                            success: function (data) { 
                                masterEmployeeName = data.EmployeeName;
                                inactiveEmployeeCount = data.EmployeeCount;
                            }
                        });
                    }

                    _unitPriceChanged = '1';
                    _roleChanged = '1';
                    _duplicateCount = (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1);
                    //newCountedEmployeeName =   masterEmployeeName +" ("+(parseInt(activeEmployeeCount)+parseInt(inactiveEmployeeCount)+1)+")**";
                    obj.setValueFromCoords(1, nextRow, retrivedData.employeeName, false);
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

                    obj.setValueFromCoords(48, nextRow, _duplicateFrom, false);
                    obj.setValueFromCoords(49, nextRow, _duplicateCount, false);
                    obj.setValueFromCoords(50, nextRow, _roleChanged, false);
                    obj.setValueFromCoords(51, nextRow, _unitPriceChanged, false);

                    obj.setValueFromCoords(37, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(2, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(3, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(4, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(5, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(6, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(7, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(8, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(9, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(10, nextRow, retrivedData.unitPrice, false);

                    obj.setValueFromCoords(38, nextRow, false, false);
                    obj.setValueFromCoords(39, nextRow, `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`, false);
                    obj.setValueFromCoords(40, nextRow, true, false);
                    obj.setValueFromCoords(47, nextRow, `both_${retrivedData.assignmentId}_${y}`, false);

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
                    var assignementId = jss.getValueFromCoords(0, y);
                    var name = jss.getValueFromCoords(1, y);                                       
                    if(parseInt(assignementId) >0){
                        deletedExistingRowIds.push(assignementId);                                
                        SetColorCommonRow(parseInt(y)+1,"gray","black","deleted");                        
                    }else{
                        alert(name +" は保存されていないため、削除できません")  
                    }      
                }
            });

            return items;
        }
    });

    $("#update_forecast_history").css("display", "block");
    $("#cancel_forecast_history").css("display", "block");

    //delete unwanted column from jexcel table
    jss.deleteColumn(52, 23);

    //get the header for shorting the column wise 
    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)');
    jexcelHeadTdEmployeeName.addClass('arrow-down');

    //year title header postion freezed/fixed 
    var yearTitleHeader = $('.jexcel > thead > tr:nth-of-type(2) > td');
    yearTitleHeader.css('position', 'sticky');
    yearTitleHeader.css('top', '0px');

    //title header postion freezed/fixed 
    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(3) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelFirstHeaderRow.css('top', '0px');

    //search header postion freezed/fixed
    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(4) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelSecondHeaderRow.css('top', '20px');

    //employee name column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)').on('click', function () {       
        $('.search_p').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_p').fadeIn("slow");
    });

    //section column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(5)').on('click', function () {               
        $('.search_section').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_section').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(6)').on('click', function () {               
        $('.search_department').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_department').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)').on('click', function () {               
        $('.search_incharge').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_incharge').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)').on('click', function () {               
        $('.search_role').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_role').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)').on('click', function () {               
        $('.search_explanation').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_explanation').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)').on('click', function () {               
        $('.search_company').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_company').fadeIn("slow");
    });
    //grade column sorting
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)').on('click', function () {               
        $('.search_grade').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_grade').fadeIn("slow");
    });
        //unit price column sorting
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)').on('click', function () {               
        $('.search_unit_price').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_unit_price').fadeIn("slow");
    });
    
    var allRows = jss.getData();
    var count = 1;
    $.each(allRows, function (index,value) {
        if (value['38'] == true && value['41'] == false) {            
            SetColorCommonRow(count,"yellow","red","newrow");
        }
        else {
            var isApprovedCells = value['43'];            
            var columnInfo = value['39'];
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

            var approvedCells = value['42'];
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
            var bCYRCellPending = value['44'];
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
        if (value['40'] == false && value['41'] == false && value['46'] == false) {
            SetColorCommonRow(count,"gray","black","deleted");
        }
        else if(value['45'] == true || value['46'] == true){
            SetColorCommonRow(count,"red","black","editable");
        }        
        count++;
    });
}

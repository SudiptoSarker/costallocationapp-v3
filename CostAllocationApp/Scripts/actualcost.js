﻿var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];
const channel = new BroadcastChannel("actualCost");
var year = "";

$(document).ready(function(){    
    $(".sorting_custom_modal").css("display", "block");
    
    $("#hider").hide();
    $(".employee_sorting").hide();
    $(".section_sorting").hide();
    $(".department_sorting").hide();
    $(".incharge_sorting").hide();
    $(".role_sorting").hide();
    $(".explanation_sorting").hide();
    $(".company_sorting").hide();
    $(".grade_sorting").hide();
    $(".unit_sorting").hide();

    channel.addEventListener("message", e => {
        LoadJexcel();
    });

    //year list
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#assignment_year').empty();
            $('#assignment_year').append(`<option value=''>年度データーの選択</option>`);
            $.each(data, function (index, element) {
                $('#assignment_year').append(`<option value='${element.Year}'>${element.Year}</option>`);
            });
        }
    });

    //call jexcel function
    $('#actual_cost').on('click', function () {
        LoadJexcel();
    });

    //shorting modal close
    $("#buttonClose,#buttonClose_section,#buttonClose_department,#buttonClose_incharge,#buttonClose_role,#buttonClose_explanation,#buttonClose_company,#buttonClose_grade,#buttonClose_unit_price").click(function () {
        $("#hider").fadeOut("slow");
        $('.employee_sorting').fadeOut("slow");
        $('.section_sorting').fadeOut("slow");
        $('.department_sorting').fadeOut("slow");
        $('.incharge_sorting').fadeOut("slow");
        $('.role_sorting').fadeOut("slow");
        $('.explanation_sorting').fadeOut("slow");
        $('.company_sorting').fadeOut("slow");
        $('.grade_sorting').fadeOut("slow");
        $('.unit_sorting').fadeOut("slow");
    });
        
});

//show actual cost and forecast data into 
function ActualCostJexcel(){
    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }

    var sectionsForJexcel = [];
    var departmentsForJexcel = [];
    var inchargesForJexcel = [];
    var rolesForJexcel = [];
    var explanationsForJexcel = [];
    var companiesForJexcel = [];

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

    var w = window.innerWidth;
    var h = window.innerHeight;
    console.log(h);
    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        filters: true,
        tableOverflow: true,
        freezeColumns: 3,
        defaultColWidth: 50,
        tableWidth: w-280+ "px",
        tableHeight: (h-150) + "px",

        columns: [
            { title: "Assignment Id", type: 'hidden', name: "AssignmentId" },
            { 
                title: "要員", 
                type: "text", 
                name: "EmployeeName", 
                width: 150,
                readOnly: true
            },
            { 
                title: "注記", 
                type: "text", 
                name: "Remarks", 
                width: 60,
                readOnly: true
            },
            { 
                title: "区分", 
                type: "dropdown", 
                source: sectionsForJexcel, 
                name: "SectionId", 
                width: 100,
                readOnly: true 
            },
            { title: "部署", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100,readOnly: true },
            { title: "担当作業", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100,readOnly: true },
            { title: "役割", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60,readOnly: true },
            { title: "説明", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150,readOnly: true },
            { title: "会社", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100,readOnly: true },            
            {
                title: "10月 実績",
                type: "decimal",
                name: "OctCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "11月 実績",
                type: "decimal",
                name: "NovCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "12月 実績",
                type: "decimal",
                name: "DecCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "1月 実績",
                type: "decimal",
                name: "JanCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "2月 実績",
                type: "decimal",
                name: "FebCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "3月 実績",
                type: "decimal",
                name: "MarCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "4月 実績",
                type: "decimal",
                name: "AprCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "5月 実績",
                type: "decimal",
                name: "MayCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "6月 実績",
                type: "decimal",
                name: "JunCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "7月 実績",
                type: "decimal",
                name: "JulCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "8月 実績",
                type: "decimal",
                name: "AugCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: "9月 実績",
                type: "decimal",
                name: "SepCost",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
        ],
        columnSorting: false,
        contextMenu: function (obj, x, y, e) {
        }
    });
    jss.deleteColumn(21, 4);
        
    //sort arrow on load: employee
    var employee_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    employee_asc_arow.addClass('arrow-down');
    var employee_header = $('.jexcel > thead > tr:nth-of-type(1) > td');
    employee_header.css('position', 'sticky');
    employee_header.css('top', '0px');

    //sort arrow on load: section
    var section_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
    section_asc_arow.addClass('arrow-down');
    var section_header = $('.jexcel > thead > tr:nth-of-type(4) > td');
    section_header.css('position', 'sticky');
    section_header.css('top', '0px');
    
    //sort arrow on load: department
    var dept_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
    dept_asc_arow.addClass('arrow-down');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');
   
    //sort employee
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {                         
        $('.employee_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.employee_sorting').fadeIn("slow");
    });

    // //section column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)').on('click', function () {  
        $('.section_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.section_sorting').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)').on('click', function () {     
        $('.department_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.department_sorting').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)').on('click', function () {  
        $('.incharge_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.incharge_sorting').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)').on('click', function () {         
        $('.role_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.role_sorting').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)').on('click', function () {    
        $('.explanation_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.explanation_sorting').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)').on('click', function () {     
        $('.company_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.company_sorting').fadeIn("slow");
    });     
    var user_role = $("#user_role").val();
    if(user_role == "admin" || user_role == "editor"){
        var octElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
        octElement.append('<button id="oct_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var novElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
        novElement.append('<button id="nov_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var decElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(13)');
        decElement.append('<button id="dec_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var janElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(14)');
        janElement.append('<button id="jan_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var febElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(15)');
        febElement.append('<button id="feb_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var marElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(16)');
        marElement.append('<button id="mar_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var aprElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(17)');
        aprElement.append('<button id="apr_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var mayElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(18)');
        mayElement.append('<button id="may_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var junElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(19)');
        junElement.append('<button id="jun_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var julElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(20)');
        julElement.append('<button id="jul_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var augElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(21)');
        augElement.append('<button id="aug_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');

        var sepElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(22)');
        sepElement.append('<button id="sep_btn"  style="display:inline-block;margin-left: 10px; padding: 0px 5px;"><i class="fa fa-caret-right" aria-hidden="true"></i></button>');
    }
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {
        $('.search_p').css('display', 'block');
        $("#hider").fadeIn("slow");
        $('.search_p').fadeIn("slow");
    });

   
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11) #oct_btn').on('click', function () {
        $('#oct_btn').empty();
        $('#oct_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=10&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12) #nov_btn').on('click', function () {
        $('#nov_btn').empty();
        $('#nov_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=11&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(13) #dec_btn').on('click', function () {
        $('#dec_btn').empty();
        $('#dec_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=12&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(14) #jan_btn').on('click', function () {
        $('#jan_btn').empty();
        $('#jan_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=1&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(15) #feb_btn').on('click', function () {
        $('#feb_btn').empty();
        $('#feb_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=2&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(16) #mar_btn').on('click', function () {
        $('#mar_btn').empty();
        $('#mar_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=3&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(17) #apr_btn').on('click', function () {
        $('#apr_btn').empty();
        $('#apr_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=4&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(18) #may_btn').on('click', function () {
        $('#may_btn').empty();
        $('#may_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=5&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(19) #jun_btn').on('click', function () {
        $('#jun_btn').empty();
        $('#jun_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=6&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(20) #jul_btn').on('click', function () {
        $('#jul_btn').empty();
        $('#jul_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=7&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(21) #aug_btn').on('click', function () {
        $('#aug_btn').empty();
        $('#aug_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=8&year=' + year, '_blank');
    });
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(22) #sep_btn').on('click', function () {
        $('#sep_btn').empty();
        $('#sep_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
        window.open('/Forecasts/ActualCostConfirm?month=9&year=' + year, '_blank');
    });
    
    var octTotalSum = 0;
    var novTotalSum = 0;
    var decTotalSum = 0;
    var janTotalSum = 0;
    var febTotalSum = 0;
    var marTotalSum = 0;
    var aprTotalSum = 0;
    var mayTotalSum = 0;
    var junTotalSum = 0;
    var julTotalSum = 0;
    var augTotalSum = 0;
    var sepTotalSum = 0;
    
    var allData = jss.getData();
    console.log(allData);
    for (let i = 0; i < allData.length-1; i++) {
        octTotalSum += allData[i][9] == '' ? 0 : parseFloat(allData[i][9]);
        novTotalSum += allData[i][10] == '' ? 0 : parseFloat(allData[i][10]);
        decTotalSum += allData[i][11] == '' ? 0 : parseFloat(allData[i][11]);
        janTotalSum += allData[i][12] == '' ? 0 : parseFloat(allData[i][12]);
        febTotalSum += allData[i][13] == '' ? 0 : parseFloat(allData[i][13]);
        marTotalSum += allData[i][14] == '' ? 0 : parseFloat(allData[i][14]);
        aprTotalSum += allData[i][15] == '' ? 0 : parseFloat(allData[i][15]);
        mayTotalSum += allData[i][16] == '' ? 0 : parseFloat(allData[i][16]);
        junTotalSum += allData[i][17] == '' ? 0 : parseFloat(allData[i][17]);
        julTotalSum += allData[i][18] == '' ? 0 : parseFloat(allData[i][18]);
        augTotalSum += allData[i][19] == '' ? 0 : parseFloat(allData[i][19]);
        sepTotalSum += allData[i][20] == '' ? 0 : parseFloat(allData[i][20]);

        if (i == allData.length - 2) {
            if (octTotalSum > 0) {
                $('#oct_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#oct_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (novTotalSum > 0) {
                $('#nov_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#nov_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (decTotalSum > 0) {
                $('#dec_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#dec_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (janTotalSum > 0) {
                $('#jan_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#jan_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (febTotalSum > 0) {
                $('#feb_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#feb_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (marTotalSum > 0) {
                $('#mar_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#mar_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (aprTotalSum > 0) {
                $('#apr_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#apr_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (mayTotalSum > 0) {
                $('#may_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#may_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (junTotalSum > 0) {
                $('#jun_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#jun_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (julTotalSum > 0) {
                $('#jul_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#jul_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (augTotalSum > 0) {
                $('#aug_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#aug_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
            if (sepTotalSum > 0) {
                $('#sep_btn').html('<i class="fa fa-times" aria-hidden="true"></i>');
            } else {
                $('#sep_btn').html('<i class="fa fa-caret-right" aria-hidden="true"></i>');
            }
        }
    } 
}

//actual cost show in jexcel
function LoadJexcel() {
    year = $('#assignment_year').val();

    if (year == null || year == '' || year == undefined) {
        alert('年度を選択してください!!!');
        return false;
    }
    LoaderShow();

    $.ajax({
        url: '/Registration/GetUserRole',
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            console.log(data)
            if (parseInt(data) === 1 || parseInt(data) === 2) {
                userRoleflag = false;
            }
            else {
                userRoleflag = true;
            }
        }
    });
    $.ajax({
        url: `/api/utilities/GetAssignmentsByYear?year=${year}`,
        contentType: 'application/json',
        type: 'GET',
        async: true,
        dataType: 'json',
        success: function (data) {                
            _retriveddata = data;
            ActualCostJexcel();
            LoaderHide();
        }
    });
}

//show loader
function LoaderShow() {
    $("#jspreadsheet").hide();     
    $("#loading").css("display", "block");
}

//hide loader
function LoaderHide() {
    $("#jspreadsheet").show(); 
    $("#loading").css("display", "none");
}

function ShowAllSortingAscIcon(){
    //sort arrow on load: employee
    var employee_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    employee_asc_arow.addClass('arrow-down');
    var employee_header = $('.jexcel > thead > tr:nth-of-type(1) > td');
    employee_header.css('position', 'sticky');
    employee_header.css('top', '0px');
    $('#search_p_asc').css('background-color', 'lightsteelblue');
    $('#search_p_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: section
    var section_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
    section_asc_arow.addClass('arrow-down');
    var section_header = $('.jexcel > thead > tr:nth-of-type(4) > td');
    section_header.css('position', 'sticky');
    section_header.css('top', '0px');
    $('#search_section_asc').css('background-color', 'lightsteelblue');
    $('#search_section_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: department
    var dept_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
    dept_asc_arow.addClass('arrow-down');
    $('#search_department_asc').css('background-color', 'lightsteelblue');
    $('#search_department_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');
    $('#search_incharge_asc').css('background-color', 'lightsteelblue');
    $('#search_incharge_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');
    $('#search_role_asc').css('background-color', 'lightsteelblue');
    $('#search_role_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');
    $('#search_explanation_asc').css('background-color', 'lightsteelblue');
    $('#search_explanation_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');
    $('#search_company_asc').css('background-color', 'lightsteelblue');
    $('#search_company_desc').css('background-color', 'lightsteelblue');
}

//shorting column functions
function ColumnOrder(columnNumber, orderBy,trType,tdType,sortIconIdAsc,sortIconIdDesc) {    
    jss.orderBy(columnNumber, orderBy);    
    ShowAllSortingAscIcon();

    var column_sort_icon = "";
    if (parseInt(orderBy) == 0) {
        $('#'+sortIconIdAsc).css('background-color', 'grey');
        $('#'+sortIconIdDesc).css('background-color', 'lightsteelblue');

        column_sort_icon = $('.jexcel > thead > tr:nth-of-type('+trType+') > td:nth-of-type('+tdType+')');
        column_sort_icon.addClass('arrow-down');                       
    }
    if (parseInt(orderBy) == 1) {
        $('#'+sortIconIdAsc).css('background-color', 'lightsteelblue');
        $('#'+sortIconIdDesc).css('background-color', 'grey');
        column_sort_icon = $('.jexcel > thead > tr:nth-of-type('+trType+') > td:nth-of-type('+tdType+')');
        column_sort_icon.addClass('arrow-down');      
    }        
}
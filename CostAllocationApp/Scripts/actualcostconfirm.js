﻿var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];
var distributionCount = 0;

const channel = new BroadcastChannel("actualCost");
var queryStrings = getUrlVars();
var year = queryStrings['year'];

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

$(document).ready(function(){
    $(".sorting_custom_modal").css("display", "block");

    //distribute actual cost
    $('#distribute').on('click', function () {
        distributionCount++;

        var _newEmployeeGroupList = [];
        var _uniqueEmnployeeIdList = [];
        var _actualCostFlag = 0;
        var _actualCostAmount = 0;  
        var totalManmonth = 0;      
        var allData = jss.getData();
        var _allRows = $(`.jexcel > tbody > tr`);

        for (var i = 0; i < allData.length; i++) {
            if (_uniqueEmnployeeIdList.length > 0) {
                if (!_uniqueEmnployeeIdList.includes(allData[i][14])) {
                    _uniqueEmnployeeIdList.push(allData[i][14]);
                }
            }
            else {
                _uniqueEmnployeeIdList.push(allData[i][14]);
            }
        }

        if (_uniqueEmnployeeIdList.length > 0) {

            for (var j = 0; j < _uniqueEmnployeeIdList.length; j++) {
                _actualCostCount = 0;
                _actualCostAmount = 0;
                _actualCostFlag = 0;
                totalManmonth = 0;
                for (var k = 0; k < allData.length; k++) {
                    if (allData[k][14] == _uniqueEmnployeeIdList[j]) {

                        if (allData[k][13] > 0) {
                            _actualCostAmount = parseFloat(allData[k][13]);
                        }
                        var _changedValue = allData[k][15];
                        if (parseInt(_changedValue) == 1) {
                            _actualCostFlag++;
                        }
                        totalManmonth+= parseFloat(allData[k][10]);
                        _newEmployeeGroupList.push({
                            assignmentId: allData[k][0],
                            manMonth: allData[k][10],
                            actualCost: allData[k][13]
                        });
                    }
                }
                if (_actualCostFlag > 0) {
                    for (var l = 0; l < _newEmployeeGroupList.length; l++) {

                        var mm = parseFloat(_newEmployeeGroupList[l].manMonth);
                        var ac = _actualCostAmount;
                        var newCost = (mm * ac) / totalManmonth;
                        if (isNaN(newCost)) {
                            newCost = 0;
                        }
                        newCost = newCost.toFixed(2);
                        for (var m = 0; m < _allRows.length; m++) {
                            if (parseInt(_newEmployeeGroupList[l].assignmentId) == parseInt(_allRows[m].cells[1].innerText)) {                                
                                jss.setValueFromCoords(13, parseInt(_allRows[m].cells[0].dataset.y), newCost, false);
                                jss.setValueFromCoords(15, parseInt(_allRows[m].cells[0].dataset.y), '', false);
                            }
                        }
                    }
                }
                _newEmployeeGroupList = [];
            }
        }

       
    });
    
    //save actual cost
    $('#create_actual_cost').on('click', function () {

        if (distributionCount == 0) {
            alert("最初に配布してください!");
            return false;
        }

        var _uniqueEmnployeeIdList = [];
        var _allData = jss.getData(false);
        var _newEmployeeGroupList = [];
        var saveFlag = true;
        for (var i = 0; i < _allData.length; i++) {
            if (_uniqueEmnployeeIdList.length > 0) {
                if (!_uniqueEmnployeeIdList.includes(_allData[i][14])) {
                    _uniqueEmnployeeIdList.push(_allData[i][14]);
                }
            }
            else {
                _uniqueEmnployeeIdList.push(_allData[i][14]);
            }
        }

        for (var e = 0; e < _uniqueEmnployeeIdList.length; e++) {
            _newEmployeeGroupList = [];
            var employeeId = _uniqueEmnployeeIdList[e];
            $.each(_allData, (index, value) => {
                if (employeeId == value[14]) {
                    _newEmployeeGroupList.push({ manMonth : value[10],actualCost : value[13] });
                }
            });

            if (_newEmployeeGroupList.length > 1) {
                var employeeCostCount = 0;
                $.each(_newEmployeeGroupList, (index, value) => {
                    if (parseFloat(value.actualCost) > 0) {
                        employeeCostCount++;
                    }
                });

                if (employeeCostCount > 0) {
                    $.each(_newEmployeeGroupList, (index, value) => {
                        if (parseFloat(value.manMonth) > 0) {
                            if (parseFloat(value.actualCost) <= 0) {
                                saveFlag = false;
                            }
                        }
                        
                    });
                }
                
            }
        }

        if (saveFlag == false) {
            alert("最初に配布してください!");
            return false;
        }

        var queryStrings = getUrlVars();
        var dataToSend = [];
        if (jss != undefined) {
            $.each(_allData, function (index, value) {
                var obj = {
                    assignmentId: value[0],
                    manHour: parseFloat(value[12]),
                    actualCostAmount: parseFloat(value[13])
                };

                dataToSend.push(obj);
            });

            console.log(dataToSend);
            $.ajax({
                url: `/api/utilities/CreateActualCost`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({
                    ActualCosts: dataToSend,
                    Year: queryStrings['year'],
                    Month: queryStrings['month'],
                }),
                success: function (data) {
                    alert("保存されました.");
                    channel.postMessage('done');
                    window.close();
                }
            });
        }
        else {
            alert('追加、修正していないデータがありません!');
        }
    });

    //return to actual cost page
    $('#cancel_actual_cost').on('click', function () {
        channel.postMessage('done');
        window.close();
    });

    //sorting modal close
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

    // loading jexcel
    {               
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
            url: `/api/utilities/GetActualCostConfirmData?year=${year}&monthId=${queryStrings['month']}`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            success: function (data) {                                        
                _retriveddata = data;
                ShowActualCostConfrimJexcel();
                LoaderHide();                    
            }
        });
    }
});

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

//actual cost confirm jexcel table
function ShowActualCostConfrimJexcel(){
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
        tableWidth: w - 280 + "px",
        tableHeight: (h - 150) + "px",


        columns: [
            { title: "Assignment Id", type: 'hidden', name: "Id" },
            { title: "要員", type: "text", name: "EmployeeName", width: 150, readOnly: true },
            { title: "注記", type: "text", name: "Remarks", width: 60, readOnly: true },
            { title: "区分", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100, readOnly: true },
            { title: "部署", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100, readOnly: true },
            { title: "担当作業", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100, readOnly: true },
            { title: "役割", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60, readOnly: true },
            { title: "説明", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150, readOnly: true },
            { title: "会社", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100, readOnly: true },
            
            {
                title: `${queryStrings['month']}月単価(uc)`,
                type: "decimal",
                name: "UnitPrice",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: `${queryStrings['month']}月工数(mm)`,
                type: "decimal",
                name: "ForecastedPoints",
                width: 100,
                readOnly: true
            },
            {
                title: `${queryStrings['month']}月予定額(forcasted)`,
                type: "decimal",
                name: "ForecastedTotal",
                mask: "#,##0",
                width: 100,
                readOnly: true
            },
            {
                title: `${queryStrings['month']}月実工数(amm)`,
                type: "decimal",
                name: "ManMonth",
                width: 100,
            },
            {
                title: `${queryStrings['month']}月実績(ac)`,
                type: "decimal",
                name: "ActualCostAmount",
                mask: "#,##0",
                width: 100,
            },
            { title: "Employee Id", type: 'hidden', name: "EmployeeId" },
            { title: "IsChanged", type: 'hidden', name: "IsChanged" },
        ],
        minDimensions: [6, 10],
        columnSorting: false,
        contextMenu: function (obj, x, y, e) {
        },
        onchange: function (instance, cell, x, y, value) {
            if (parseInt(x) == 13 && value > 0) {

                var _allData = jss.getData();
                var employeeId = jss.getValueFromCoords(14, y);
                var employeeCostCount = 0;
                $.each(_allData, (index, value) => {
                    if (employeeId == value[14]) {
                        if (parseFloat(value[13]) > 0) {
                            employeeCostCount++;
                        }
                    }
                });
                if (employeeCostCount == 1) {
                    jss.setValueFromCoords(15, y, 1, false);
                }
                else {
                    if (distributionCount == 0) {
                        alert('同一情報の要員が複製されています');
                        jss.setValueFromCoords(13, y, 0, false);
                    }                   
                }
            }            
        }
    });
    jss.deleteColumn(16, 22);

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
}

//get the url variables 
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

//on page load show soring icon as a asc icon
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
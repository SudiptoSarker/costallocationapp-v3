﻿var globalSearchObject = '';
var globalPreviousValue = '0.0';
var globalPreviousId = '';
var jss;
var globalX = 0;
var globalY = 0;
var newRowCount = 1;
var beforeChangedValue = 0;
var jssUpdatedData = [];
var jssInsertedData = [];

function DismissOtherDropdown(requestType) {
    var section_display = "";
    if (requestType.toLowerCase() != "section") {
        section_display = $('#sectionChks');
        if (section_display.css("display") == "block") {
            let checkboxes = document.getElementById("sectionChks");
            checkboxes.style.display = "none";
            expanded = false;
        }
    }

    if (requestType.toLowerCase() != "dept") {
        section_display = "";
        section_display = $('#departmentChks');
        if (section_display.css("display") == "block") {
            let checkboxes = document.getElementById("departmentChks");
            checkboxes.style.display = "none";
            expanded = false;
        }
    }

    if (requestType.toLowerCase() != "incharge") {
        section_display = "";
        section_display = $('#inchargeChks');
        if (section_display.css("display") == "block") {
            let checkboxes = document.getElementById("inchargeChks");
            checkboxes.style.display = "none";
            expanded = false;
        }
    }

    if (requestType.toLowerCase() != "role") {
        section_display = "";
        section_display = $('#RoleChks');
        if (section_display.css("display") == "block") {
            let checkboxes = document.getElementById("RoleChks");
            checkboxes.style.display = "none";
            expanded = false;
        }
    }

    if (requestType.toLowerCase() != "explanation") {
        section_display = "";
        section_display = $('#ExplanationChks');
        if (section_display.css("display") == "block") {
            let checkboxes = document.getElementById("ExplanationChks");
            checkboxes.style.display = "none";
            expanded = false;
        }
    }

    if (requestType.toLowerCase() != "company") {
        section_display = "";
        section_display = $('#CompanyChks');
        if (section_display.css("display") == "block") {
            let checkboxes = document.getElementById("CompanyChks");
            checkboxes.style.display = "none";
            expanded = false;
        }
    }
}
//var expanded = false;
function SectionCheck() {
    DismissOtherDropdown("section");

    var checkboxes = document.getElementById("sectionChks");
    if (!expanded) {
        checkboxes.style.display = "block";
        //$(this).find("#sectionChks").slideToggle("fast");
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}
function DepartmentCheck() {
    DismissOtherDropdown("dept");
    // section_display = $('#sectionChks');
    // if (section_display.css("display") == "block") {
    //     let checkboxes = document.getElementById("sectionChks");
    //     checkboxes.style.display = "none";
    //     expanded = false;
    // }

    var checkboxes = document.getElementById("departmentChks");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}
function InchargeCheck() {
    DismissOtherDropdown("incharge");
    var checkboxes = document.getElementById("inchargeChks");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}
function RoleCheck() {
    DismissOtherDropdown("role");
    var checkboxes = document.getElementById("RoleChks");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}
function ExplanationCheck() {
    DismissOtherDropdown("explanation");
    var checkboxes = document.getElementById("ExplanationChks");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}
function CompanyCheck() {
    DismissOtherDropdown("company");
    var checkboxes = document.getElementById("CompanyChks");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}

function LoadGradeValue(sel) {
    var _rowId = $("#add_name_row_no_hidden").val();
    var _companyName = $('#company_row_' + _rowId).find(":selected").text();
    var _unitPrice = $("#add_name_unit_price_hidden").val();

    $.ajax({
        url: `/api/utilities/CompareGrade/${_unitPrice}`,
        type: 'GET',
        dataType: 'json',
        //data: {
        //    unitPrice: _unitPrice
        //},
        success: function (data) {
            $('#grade_edit_hidden').val(data.Id);
            //if (_companyName.toLowerCase == 'mw') {
            if (_companyName.toLowerCase().indexOf("mw") > 0) {
                //$('#grade_new_hidden').val(data.Id);
                $('#grade_row_' + _rowId).val(data.SalaryGrade);
            } else {
                $('#grade_row_' + _rowId).val('');
            }
        },
        error: function () {
            $('#grade_row_' + _rowId).val('');
        }
    });
}

function NameListSort(sort_asc, sort_desc) {
    var nameAsc = $('#' + sort_asc).css('display');
    if (nameAsc == 'inline-block') {
        $('#' + sort_asc).css('display', 'none');
        $('#' + sort_desc).css('display', 'inline-block');
    }
    else {
        $('#' + sort_asc).css('display', 'inline-block');
        $('#' + sort_desc).css('display', 'none');
    }
}
function NameListSort_OnChange() {
    $('#name_asc').css('display', 'inline-block');
    $('#name_desc').css('display', 'none');

    $('#section_asc').css('display', 'inline-block');
    $('#section_desc').css('display', 'none');

    $('#department_asc').css('display', 'inline-block');
    $('#department_desc').css('display', 'none');

    $('#incharge_asc').css('display', 'inline-block');
    $('#incharge_desc').css('display', 'none');

    $('#role_asc').css('display', 'inline-block');
    $('#role_desc').css('display', 'none');

    $('#explanation_asc').css('display', 'inline-block');
    $('#explanation_desc').css('display', 'none');

    $('#company_asc').css('display', 'inline-block');
    $('#company_desc').css('display', 'none');

    $('#grade_asc').css('display', 'inline-block');
    $('#grade_desc').css('display', 'none');

    $('#unit_asc').css('display', 'inline-block');
    $('#unit_desc').css('display', 'none');
}

function NameList_DatatableLoad(data) {
    var tempCompanyName = "";
    var tempSubCode = "";
    var redMark = "";

    $('#name-list').DataTable({
        destroy: true,
        data: data,
        ordering: true,
        orderCellsTop: true,
        pageLength: 100,
        searching: false,
        bLengthChange: false,
        columns: [
            //{
            //    data: 'MarkedAsRed',
            //    render: function (markedAsRed) {
            //        redMark = markedAsRed;
            //        return null;
            //    }
            //},
            {
                data: 'EmployeeNameWithCodeRemarks',
                render: function (employeeNameWithCodeRemarks) {
                    var splittedString = employeeNameWithCodeRemarks.split('$');
                    if (splittedString[2] == 'true') {
                        return `<span style='color:red;' class='namelist_addname' onClick="loadSingleAssignmentDataForExistingEmployee('${splittedString[0]}')" data-toggle="modal" data-target="#modal_add_name">${splittedString[1]}</span>`;
                    }
                    else {
                        return `<span class='namelist_addname' onClick="loadSingleAssignmentDataForExistingEmployee('${splittedString[0]}')" data-toggle="modal" data-target="#modal_add_name">${splittedString[1]}</span>`;
                    }

                }
            },
            {
                data: 'SectionName'
            },
            {
                data: 'DepartmentName'
            },
            {
                data: 'InchargeName'
            },
            {
                data: 'RoleName'
            },
            {
                data: 'ExplanationName'
            },
            {
                data: 'CompanyName',
                render: function (companyName) {
                    tempCompanyName = companyName;
                    return companyName;
                }
            },
            {
                data: 'GradePoint',
                render: function (grade) {
                    if (tempCompanyName.toLowerCase() == "mw") {
                        return grade;
                    } else {
                        return "<span style='display:none;'>" + grade + "</span>";
                    }
                }
            },
            {
                data: 'UnitPrice'
            },
            {
                data: 'Id',
                render: function (Id) {
                    if (tempCompanyName.toLowerCase() == 'mw') {
                        return `<td class='namelist_td Action'><a href="javascript:void(0);" id='edit_button' onClick="loadSingleAssignmentData(${Id})" data-toggle="modal" data-target="#modal_edit_name">Edit</a><a id='delete_button' href='javascript:void();' data-toggle='modal' data-target='#namelist_delete' onClick="loadAssignmentRowData(${Id})" assignment_id='${Id}'>Inactive</a></td>`;
                    } else {
                        return `<td class='namelist_td Action'><a href="javascript:void(0);" id='edit_button' onClick="loadSingleAssignmentData(${Id})" data-toggle="modal" data-target="#modal_edit_name">Edit</a><a id='delete_button' href='javascript:void();' data-toggle='modal' data-target='#namelist_delete' onClick="loadAssignmentRowData(${Id})" assignment_id='${Id}'>Inactive</a></td>`;
                    }
                }
            }
        ]
    });
}

//$("#section_registration").on('hide', function () {
//    window.location.reload();
//});

function RetainRegistrationValue() {
    let name = $("#hid_name").val();
    let memo = $("#hid_memo").val(memo);
    let sectionId = $("#hid_sectionId").val(sectionId);
    let departmentId = $("#hid_departmentId").val(departmentId);
    let inchargeId = $("#hid_inchargeId").val(inchargeId);
    let roleId = $("#hid_roleId").val(roleId);
    let explantionId = $("#hid_explanationId").val(explantionId);
    let companyid = $("#hid_companyId").val();
    let companyName = $("#hid_companyName").val();
    let grade = $("#hid_grade").val();
    //let unitPrice = $("#hid_gradeId").val(unitPrice);
    let unitPrice = $("#hid_unitPrice").val();

    $("#identity_new").val(name);
    $("#memo_new").val(memo);
    // $('#section_new').find(":selected").val();
    // $('#department_new').find(":selected").val();
    // $('#incharge_new').find(":selected").val();
    // $('#role_new').find(":selected").val();
    // $('#explanation_new').find(":selected").val();
    // $('#company_new').find(":selected").val();
    if (companyName.toLowerCase() == 'mw') {
        $("#grade_new").val(grade);
    } else {
        $("#grade_new").val('');
    }

    $("#unitprice_new").val(unitPrice);

}


$('#department_modal_href').click(function () {
    $.getJSON('/api/sections/')
        .done(function (data) {
            $('#section_list').empty();
            $('#section_list').append(`<option value=''>Select Section</option>`)
            $.each(data, function (key, item) {
                $('#section_list').append(`<option value='${item.Id}'>&nbsp;&nbsp; ${item.SectionName}</option>`)
            });
        });
});

function ForecastSearchDropdownInLoad() {
    $.getJSON('/api/sections/')
        .done(function (data) {
            $('#sectionChks').empty();
            $('#sectionChks').append(`<label for='chk_sec_all'><input id="chk_sec_all" type="checkbox" checked data-checkwhat="chkSelect" value='sec_all'/>All</label>`)
            $.each(data, function (key, item) {
                $('#sectionChks').append(`<label for='section_checkbox_${item.Id}'><input class='section_checkbox' id="section_checkbox_${item.Id}"  type="checkbox" checked value='${item.Id}'/>${item.SectionName}</label>`)
            });
        });
    $.getJSON('/api/Departments/')
        .done(function (data) {
            $('#departmentChks').empty();
            $('#departmentChks').append(`<label for='chk_dept_all'><input id="chk_dept_all" type="checkbox" checked value='dept_all'/>All</label>`)
            $.each(data, function (key, item) {
                $('#departmentChks').append(`<label for='department_checkbox_${item.Id}'><input class='department_checkbox' id="department_checkbox_${item.Id}" type="checkbox" checked value='${item.Id}'/>${item.DepartmentName}</label>`)
            });
        });
    $.getJSON('/api/InCharges/')
        .done(function (data) {
            $('#inchargeChks').empty();
            $('#inchargeChks').append(`<label for='chk_incharge_all'><input id="chk_incharge_all" type="checkbox" checked value='incharge_all'/>All</label>`);
            $.each(data, function (key, item) {
                $('#inchargeChks').append(`<label for='incharge_checkbox_${item.Id}'><input class='incharge_checkbox' id="incharge_checkbox_${item.Id}" type="checkbox" checked value='${item.Id}'/>${item.InChargeName}</label>`)
            });
        });
    $.getJSON('/api/Roles/')
        .done(function (data) {
            $('#RoleChks').empty();
            $('#RoleChks').append(`<label for='chk_role_all'><input id="chk_role_all" type="checkbox" checked value='role_all'/>All</label>`);
            $.each(data, function (key, item) {
                $('#RoleChks').append(`<label for='role_checkbox_${item.Id}'><input class='role_checkbox' id="role_checkbox_${item.Id}" type="checkbox" checked value='${item.Id}'/>${item.RoleName}</label>`)
            });
        });
    $.getJSON('/api/Explanations/')
        .done(function (data) {
            $('#ExplanationChks').empty();
            $('#ExplanationChks').append(`<label for='chk_explanation_all'><input id="chk_explanation_all" type="checkbox" checked value='explanation_all'/>All</label><br>`);
            $.each(data, function (key, item) {
                $('#ExplanationChks').append(`<label for='explanation_checkbox_${item.Id}'><input class='explanation_checkbox' id="explanation_checkbox_${item.Id}" type="checkbox" checked value='${item.Id}'/>${item.ExplanationName}</label><br>`);

            });
        });
    $.getJSON('/api/Companies/')
        .done(function (data) {
            $('#CompanyChks').empty();
            $('#CompanyChks').append(`<label for='chk_comopany_all'><input id="chk_comopany_all" type="checkbox" checked value='comopany_all'/>All</label>`);
            $.each(data, function (key, item) {
                $('#CompanyChks').append(`<label for='comopany_checkbox_${item.Id}'><input class='comopany_checkbox' id="comopany_checkbox_${item.Id}" type="checkbox" checked value='${item.Id}'/>${item.CompanyName}</label>`)
            });
        });
}
function CheckPreviousManMonthIdValuePoint(e) {
    let clickedId = e.id;
    console.log("clickedId: " + clickedId);
    if (globalPreviousId == '') {
        globalPreviousId = clickedId;
    }

    if (globalPreviousId == clickedId) {
        let pointValue = $("#" + e.id).val();
        if ((isNaN(pointValue) || pointValue == undefined || pointValue == '') && globalPreviousValue > 0) {
            globalPreviousValue = globalPreviousValue;
        } else {
            globalPreviousValue = $("#" + e.id).val();
        }

        if (e.value <= 0) {
            //globalPreviousValue='';
            $("#" + e.id).val('');
        }
    } else {

        let pointValue = $("#" + globalPreviousId).val();
        if ((isNaN(pointValue) || pointValue == undefined || pointValue == '') && globalPreviousValue > 0) {
            globalPreviousValue = globalPreviousValue;
        } else {
            globalPreviousValue = $("#" + globalPreviousId).val();
        }

        if (e.value <= 0) {
            //globalPreviousValue='';
            $("#" + e.id).val('');
        }
        globalPreviousId = clickedId;

    }
    console.log("clickedId: " + clickedId);
    console.log("globalPreviousId: " + globalPreviousId);
    console.log("globalPreviousValue: " + globalPreviousValue);
}

function checkPoint_click(e) {
    console.log("click: " + globalPreviousValue);
    let pointValue = $("#" + e.id).val();
    let comparePreviousGlobalValue = parseFloat(globalPreviousValue);
    if ((isNaN(pointValue) || pointValue == undefined || pointValue == '') && comparePreviousGlobalValue > 0) {
        globalPreviousValue = globalPreviousValue;
    }
    else if (comparePreviousGlobalValue > 0) {
        globalPreviousValue = globalPreviousValue;
    }
    else {
        //globalPreviousValue = $("#" + e.id).val();
        globalPreviousValue = '0.0';
    }

    if (e.value <= 0) {
        //globalPreviousValue='';
        $("#" + e.id).val('');
        // $("#" + e.id).val('0.0');
    }
}

function checkPoint_onmouseover(e) {
    let pointValue = $("#" + e.id).val();
    if ((isNaN(pointValue) || pointValue == undefined || pointValue == '') && globalPreviousValue > 0) {
        globalPreviousValue = globalPreviousValue;
    } else {
        globalPreviousValue = $("#" + e.id).val();
    }

    console.log(globalPreviousValue);
}
function checkPoint(element) {

    var pointValue = $(element).val();
    if (isNaN(pointValue) || pointValue == undefined || pointValue == '') {
        $(element).val(globalPreviousValue);
    }
    else {
        if ((pointValue > 1 || pointValue < 0)) {
            alert('total month point can not be grater than 1 Or less than 0');
            $(element).val(globalPreviousValue);
        }
        else {
            $(element).val(pointValue);
        }
    }


    var totalMonthPoint = 0;
    var sameNameTr = [];
    var tr = element.closest('tr');
    //var trId = $(tr).data('rowid');
    var trName = $(tr).find('td').eq(0).children('span').data('name');
    var allTr = $('#forecast_table > tbody > tr');
    //var monthNumber = $(element).data('month');
    var columnIndex = $(element).parent().index();

    $.each(allTr, function (index, value) {
        var tempTrName = $(value).find('td').eq(0).children('span').data('name');
        if (tempTrName == trName) {
            var monthPoint = $(value).find('td').eq(columnIndex).children('input').val();
            if (monthPoint == '' || monthPoint == NaN) {
                totalMonthPoint += 0;
            }
            else {
                totalMonthPoint += parseFloat(monthPoint);
            }
            sameNameTr.push(value);
        }
    });
    if (totalMonthPoint > 1) {
        alert('total month point can not be grater than 1');
        $(element).val(globalPreviousValue);
    }
}
function LoaderShow() {
    $("#forecast_table_wrapper").css("display", "none");
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#forecast_table_wrapper").css("display", "block");
    $("#loading").css("display", "none");
}

function LoadForecastData() {
    var employeeName = $('#name_search').val();
    var sectionId = $('#section_multi_search').val();
    var inchargeId = $('#incharge_multi_search').val();
    var roleId = $('#role_multi_search').val();
    var companyId = $('#company_multi_search').val();
    var departmentId = $('#dept_multi_search').val();
    var explanationId = $('#explanation_multi_search').val();
    var year = $('#period_multi_search').val();

    var allocations = [];

    $.getJSON('/api/Explanations/')
        .done(function (data) {
            allocations = data;
        });

    console.log("saved-3");
    if (globalSearchObject == '') {
        return;
    }
    else {
        $.ajax({
            url: `/api/utilities/SearchForecastEmployee`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            //data: globalSearchObject,
            data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
            success: function (data) {
                LoaderHide();
                $('#forecast_table>thead').empty();
                $('#forecast_table>tbody').empty();
                $('#forecast_table>thead').append(`
                    <tr>
                        <th id="forecast_name">Name </th>
                        <th id="forecast_section">Section </th>
                        <th id="forecast_department">Department </th>
                        <th id="forecast_incharge">In-Charge </th>
                        <th id="forecast_role">Role </th>
                        <th id="forecast_explanation">Explanation </th>
                        <th id="forecast_company">Company Name </i></th>
                        <th id="forecast_grade">Grade </th>
                        <th id="forecast_unitprice"><span>Unit Price</span> </th>
                        <th>10月</th>
                        <th>11月</th>
                        <th>12月</th>
                        <th>1月</th>
                        <th>2月</th>
                        <th>3月</th>
                        <th>4月</th>
                        <th>5月</th>
                        <th>6月</th>
                        <th>7月</th>
                        <th>8月</th>
                        <th>9月</th>
                        <th>Oct</th>
                        <th>Nov</th>
                        <th>Dec</th>
                        <th>Jan</th>
                        <th>Feb</th>
                        <th>Mar</th>
                        <th>Apr</th>
                        <th>May</th>
                        <th>Jun</th>
                        <th>Jul</th>
                        <th>Aug</th>
                        <th>Sep</th>
                    </tr>
                `);
                //Forecast_DatatableLoad(data);
                var _id = 0;
                var _companyName = '';
                var _oct = 0;
                var _octTotal = 0;

                $('#forecast_table').DataTable({
                    destroy: true,
                    data: data,
                    searching: false,
                    bLengthChange: false,
                    autoWidth: false,
                    pageLength: 100,
                    columns: [
                        {
                            data: 'EmployeeNameWithCodeRemarks',
                            render: function (employeeNameWithCodeRemarks) {
                                var splittedString = employeeNameWithCodeRemarks.split('$');
                                _id = parseInt(splittedString[3]); // id
                                if (splittedString[2] == 'true') {
                                    return `<span title='initial' data-name='${splittedString[0]}' style='color:red;'> ${splittedString[1]}</span> <input type='hidden' id='row_id_${_id}' value='${_id}'/>`;
                                } else {
                                    return `<span title='initial' data-name='${splittedString[0]}'> ${splittedString[1]}</span> <input type='hidden' id='row_id_${_id}' value='${_id}'/>`;
                                }

                            }
                        },
                        {
                            data: 'SectionName',
                            render: function (sectionName) {
                                return `<td title='initial'>${sectionName}</td>`;
                            }
                        },
                        {
                            data: 'DepartmentName',
                            render: function (departmentName) {
                                return `<td title='initial'>${departmentName}</td>`;
                            }
                        },
                        {
                            data: 'InchargeName',
                            render: function (inchargeName) {
                                return `<td title='initial'>${inchargeName}</td>`;
                            }
                        },
                        {
                            data: 'RoleName',
                            render: function (roleName) {
                                return `<td title='initial'>${roleName}</td>`;
                            }
                        },
                        {
                            data: 'ExplanationName',
                            render: function (explanationName) {
                                return `<span title='initial' class='forecast_explanation'>${explanationName}</span>`;
                            }
                        },
                        {
                            data: 'CompanyName',
                            render: function (companyName) {
                                _companyName = companyName;
                                return `<td title='initial'>${companyName}</td>`;
                            }
                        },
                        {
                            data: 'GradePoint',
                            render: function (gradePoint) {

                                if (_companyName.toLowerCase() == 'mw') {
                                    return `<td title='initial'>${gradePoint}</td>`;
                                }
                                else {
                                    return `<td title='initial'></td>`;
                                }

                            }
                        },
                        {
                            data: 'UnitPrice',
                            render: function (unitPrice) {
                                return `<span id='up_${_id}' data-unitprice=${unitPrice}> ${unitPrice}</span>`;
                            }
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='oct_${_id}'  onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)"  data-month='10' value='${_forecast[0].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='oct_${_id}'  onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)"  data-month='10' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='nov_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='11' value='${_forecast[1].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='nov_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='11' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='dec_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='12' value='${_forecast[2].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='dec_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='12' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='jan_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='1' value='${_forecast[3].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='jan_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='1' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='feb_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='2' value='${_forecast[4].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='feb_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='2' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='mar_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='3' value='${_forecast[5].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='mar_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='3' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='apr_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='4' value='${_forecast[6].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='apr_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='4' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='may_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='5' value='${_forecast[7].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='may_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='5' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='jun_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='6' value='${_forecast[8].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='jun_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='6' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='jul_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='7' value='${_forecast[9].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='jul_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='7' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='aug_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='8' value='${_forecast[10].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='aug_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='8' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='sep_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='9' value='${_forecast[11].Points.toFixed(1)}' class='input_month'/></td>`;
                                } else {
                                    return `<td><input type='text' id='sep_${_id}' onfocus ="checkPoint_onmouseover(this);" onclick="checkPoint_click(this)" onChange="checkPoint(this)" data-month='9' value='0.0' class='input_month'/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='oct_output_${_id}' value='${_forecast[0].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='oct_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='nov_output_${_id}' value='${_forecast[1].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='nov_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='dec_output_${_id}' value='${_forecast[2].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='dec_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='jan_output_${_id}' value='${_forecast[3].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='jan_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='feb_output_${_id}' value='${_forecast[4].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='feb_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='mar_output_${_id}' value='${_forecast[5].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='mar_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='apr_output_${_id}' value='${_forecast[6].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='apr_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='may_output_${_id}' value='${_forecast[7].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='may_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='jun_output_${_id}' value='${_forecast[8].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='jun_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='jul_output_${_id}' value='${_forecast[9].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='jul_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='aug_output_${_id}' value='${_forecast[10].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='aug_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                        {
                            data: 'forecasts',
                            render: function (_forecast) {

                                if (_forecast.length > 0) {
                                    return `<td><input type='text' id='sep_output_${_id}' value='${_forecast[11].Total}' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                } else {
                                    return `<td><input type='text' id='sep_output_${_id}' value='0' style='background-color:#E6E6E3;text-align:right;' readonly/></td>`;
                                }

                            },
                            orderable: false
                        },
                    ],
                });

            },
            error: function () {
                $('#add_name_table_1 tbody').empty();
            }
        });
    }
}

function onCancel() {
    LoaderShow();
    LoadForecastData()
}

function onSave(e) {

    e.preventDefault();
    //$.when(ForecastDataSave()).then(LoadForecastData());
    ForecastDataSave();

    setTimeout(() => {
        LoadForecastData();
        ToastMessageSuccess("Data Saved Successfully");
    }
        , 5000);
    //LoadForecastData();
    //LoadForecastData(); 
    //LoadForecastData();
}

function ForecastDataSave() {
    var saveFlag = false;
    const rows = document.querySelectorAll("#forecast_table > tbody > tr");
    if (rows.length <= 0) {
        alert('No Rows Selected');
        return false;
    }
    LoaderShow();

    $.each(rows, function (index, data) {
        var rowId = $(this).closest('tr').find('td').eq(0).children('input').val();
        var year = $('#period_search').find(":selected").val();
        var assignmentId = $('#row_id_' + rowId).val();

        var oct_point = $('#oct_' + rowId).val().replace(/,/g, '');
        var oct_output = $('#oct_output_' + rowId).val().replace(/,/g, '');

        var nov_point = $('#nov_' + rowId).val().replace(/,/g, '');
        var nov_output = $('#nov_output_' + rowId).val().replace(/,/g, '');


        var dec_point = $('#dec_' + rowId).val().replace(/,/g, '');
        var dec_output = $('#dec_output_' + rowId).val().replace(/,/g, '');

        var jan_point = $('#jan_' + rowId).val().replace(/,/g, '');
        var jan_output = $('#jan_output_' + rowId).val().replace(/,/g, '');

        var feb_point = $('#feb_' + rowId).val().replace(/,/g, '');
        var feb_output = $('#feb_output_' + rowId).val().replace(/,/g, '');

        var mar_point = $('#mar_' + rowId).val().replace(/,/g, '');
        var mar_output = $('#mar_output_' + rowId).val().replace(/,/g, '');

        var apr_point = $('#apr_' + rowId).val().replace(/,/g, '');
        var apr_output = $('#apr_output_' + rowId).val().replace(/,/g, '');

        var may_point = $('#may_' + rowId).val().replace(/,/g, '');
        var may_output = $('#may_output_' + rowId).val().replace(/,/g, '');

        var jun_point = $('#jun_' + rowId).val().replace(/,/g, '');
        var jun_output = $('#jun_output_' + rowId).val().replace(/,/g, '');

        var jul_point = $('#jul_' + rowId).val().replace(/,/g, '');
        var jul_output = $('#jul_output_' + rowId).val().replace(/,/g, '');

        var aug_point = $('#aug_' + rowId).val().replace(/,/g, '');
        var aug_output = $('#aug_output_' + rowId).val().replace(/,/g, '');

        var sep_point = $('#sep_' + rowId).val().replace(/,/g, '');
        var sep_output = $('#sep_output_' + rowId).val().replace(/,/g, '');

        var data = `10_${oct_point}_${oct_output},11_${nov_point}_${nov_output},12_${dec_point}_${dec_output},1_${jan_point}_${jan_output},2_${feb_point}_${feb_output},3_${mar_point}_${mar_output},4_${apr_point}_${apr_output},5_${may_point}_${may_output},6_${jun_point}_${jun_output},7_${jul_point}_${jul_output},8_${aug_point}_${aug_output},9_${sep_point}_${sep_output}`;

        $.ajax({
            url: '/api/Forecasts',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: {
                data: data,
                year: year,
                assignmentId: assignmentId
            },
            success: function (data) {
                saveFlag = data;
                console.log("saved-1");
            },
            error: function (data) {
                $("#loading").css("display", "none");
                saveFlag = false;
                alert("Error please try again");
            }
        });
    });

    //ToastMessageSuccess("Data Saved Successfully");
}

var expanded = false;
$(document).on("click", function (event) {
    var $trigger = $(".forecast_multiselect");
    //alert("trigger: "+$trigger);
    if ($trigger !== event.target && !$trigger.has(event.target).length) {
        $(".commonselect").slideUp("fast");
        expanded = false;
    }
});

$(document).ready(function () {
    GetAllForecastYears();
    var year = $('#hidForecastYear').val();
    if (year.toLowerCase() != "imprt") {
        //var assignmentYear = $('#assignment_year_list').find(":selected").val();
        var assignmentYear = $("#hidDefaultForecastYear").val();
        $('#assignment_year_list').val(assignmentYear);
        ShowForecastResults(assignmentYear);
    }

    var count = 1;
    $('#forecast_search tbody tr:eq(0) th').each(function () {
        if (count == 1) {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="Search ' + title + '" class="" Id="name_search"/>');
        }
        count = count + 1;
    });
    // //section multi search
    $('#section_multi_search').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        nonSelectedText: 'select 区分(section)',
    });
    //company multi search
    $('#company_multi_search').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        nonSelectedText: 'select 会社(company)',
    });
    //department multi search
    $('#dept_multi_search').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        nonSelectedText: 'select 企画/開発(dept.)',
    });
    //allocation multi search
    // $('#allocation_multi_search').multiselect({
    //     includeSelectAllOption: true,
    //     enableFiltering: true,
    //     nonSelectedText: 'select 配置作成(allocation)',
    // });
    //allocation multi search
    //$('#period_multi_search').multiselect({
    //    includeSelectAllOption: true,
    //    enableFiltering: true,
    //    nonSelectedText: 'select 期間(period)',
    //});
    //incharge multi search
    $('#incharge_multi_search').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        nonSelectedText: 'select 担当(in-chrg)',
    });
    //role multi search
    $('#role_multi_search').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        nonSelectedText: 'select 役割(role)',
    });
    //explanation multi search
    $('#explanation_multi_search').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        nonSelectedText: 'select 説明 (exp.)',
    });



    $('#forecast_name').click(function () {
        NameListSort("name_asc", "name_desc");
    });
    $('#forecast_section').click(function () {
        NameListSort("section_asc", "section_desc");
    });
    $('#forecast_department').click(function () {
        NameListSort("department_asc", "department_desc");
    });
    $('#forecast_incharge').click(function () {
        NameListSort("incharge_asc", "incharge_desc");
    });
    $('#forecast_role').click(function () {
        NameListSort("role_asc", "role_desc");
    });
    $('#forecast_explanation').click(function () {
        NameListSort("explanation_asc", "explanation_desc");
    });
    $('#forecast_company').click(function () {
        NameListSort("company_asc", "company_desc");
    });
    $('#forecast_grade').click(function () {
        NameListSort("grade_asc", "grade_desc");
    });
    $('#forecast_unitprice').click(function () {
        NameListSort("unit_asc", "unit_desc");
    });

    // multiple search by section
    $(document).on('click', '#sectionChks input[type="checkbox"]', function () {
        let isSectionAllChk = $("#chk_sec_all").is(':checked');
        var isDepartmentAllCheck = $("#chk_dept_all").is(':checked');
        var isInChargeAllCheck = $("#chk_incharge_all").is(':checked');
        var isRoleAllCheck = $("#chk_role_all").is(':checked');
        var isExplanationAllCheck = $("#chk_explanation_all").is(':checked');
        var isCompanytAllCheck = $("#chk_comopany_all").is(':checked');

        var clicked_checkbox = $(this).attr("id")

        if (clicked_checkbox.toLowerCase() == "chk_sec_all") {
            $(".section_checkbox").prop("checked", isSectionAllChk);
        } else {
            $("#chk_sec_all").prop("checked", false);
        }


        clicked_checkbox = "";

        var employeeName = "";
        var sectionCheck = [];
        var departmentCheck = [];
        var inchargeCheck = [];
        var roleCheck = [];
        var explanationCheck = [];
        var companyCheck = [];

        employeeName = $('#name_search').val();
        var sectionCheckedBoxes = $('#sectionChks input[type="checkbox"]:checked');
        var departmentCheckedBoxes = $('#departmentChks input[type="checkbox"]:checked');
        var inchargeCheckedBoxes = $('#inchargeChks input[type="checkbox"]:checked');
        var roleCheckedBoxes = $('#RoleChks input[type="checkbox"]:checked');
        var explanationCheckedBoxes = $('#ExplanationChks input[type="checkbox"]:checked');
        var companyCheckedBoxes = $('#CompanyChks input[type="checkbox"]:checked');

        $("#hidSectionId").val('');
        if (!isSectionAllChk) {
            $.each(sectionCheckedBoxes, function (index, item) {
                sectionCheck.push(item.value);
            });
        }
        $("#hidSectionId").val(sectionCheck);

        $("#hidDepartmentId").val('');
        if (!isDepartmentAllCheck) {
            $.each(departmentCheckedBoxes, function (index, item) {
                departmentCheck.push(item.value);
            });
        }
        $("#hidDepartmentId").val(departmentCheck);

        $("#hidInChargeId").val('');
        if (!isInChargeAllCheck) {
            $.each(inchargeCheckedBoxes, function (index, item) {
                inchargeCheck.push(item.value);
            });
        }
        $("#hidInChargeId").val(inchargeCheck);

        $("#hidRoleId").val('');
        if (!isRoleAllCheck) {
            $.each(roleCheckedBoxes, function (index, item) {
                roleCheck.push(item.value);
            });
        }
        $("#hidRoleId").val(roleCheck);

        $("#hidExplanationId").val('');
        if (!isExplanationAllCheck) {
            $.each(explanationCheckedBoxes, function (index, item) {
                explanationCheck.push(item.value);
            });
        }
        $("#hidExplanationId").val(explanationCheck);

        $("#hidCompanyid").val('');
        if (!isCompanytAllCheck) {
            $.each(companyCheckedBoxes, function (index, item) {
                companyCheck.push(item.value);
            });
        }
        $("#hidCompanyid").val(companyCheck);

        //var sectionSearchId = "";
        //if (!isSectionAllChk) {
        //    $.each(sectionCheckedBoxes, function(index, item) {
        //        if(sectionSearchId == ""){
        //            sectionSearchId = item.value;
        //        }
        //        else{
        //            sectionSearchId = sectionSearchId + ","+item.value;
        //        }
        //    });
        //}
        //$("#hidSectionId").val(sectionSearchId);
    });

    // multiple search by departments
    $(document).on('click', '#departmentChks input[type="checkbox"]', function () {
        let isSectionAllChk = $("#chk_sec_all").is(':checked');
        var isDepartmentAllCheck = $("#chk_dept_all").is(':checked');
        var isInChargeAllCheck = $("#chk_incharge_all").is(':checked');
        var isRoleAllCheck = $("#chk_role_all").is(':checked');
        var isExplanationAllCheck = $("#chk_explanation_all").is(':checked');
        var isCompanytAllCheck = $("#chk_comopany_all").is(':checked');

        var clicked_checkbox = $(this).attr("id")

        if (clicked_checkbox.toLowerCase() == "chk_dept_all") {
            $(".department_checkbox").prop("checked", isDepartmentAllCheck);
        } else {
            $("#chk_dept_all").prop("checked", false);
        }

        var employeeName = "";
        var sectionCheck = [];
        var departmentCheck = [];
        var inchargeCheck = [];
        var roleCheck = [];
        var explanationCheck = [];
        var companyCheck = [];



        employeeName = $('#name_search').val();
        var sectionCheckedBoxes = $('#sectionChks input[type="checkbox"]:checked');
        var departmentCheckedBoxes = $('#departmentChks input[type="checkbox"]:checked');
        var inchargeCheckedBoxes = $('#inchargeChks input[type="checkbox"]:checked');
        var roleCheckedBoxes = $('#RoleChks input[type="checkbox"]:checked');
        var explanationCheckedBoxes = $('#ExplanationChks input[type="checkbox"]:checked');
        var companyCheckedBoxes = $('#CompanyChks input[type="checkbox"]:checked');

        $("#hidSectionId").val('');
        if (!isSectionAllChk) {
            $.each(sectionCheckedBoxes, function (index, item) {
                sectionCheck.push(item.value);
            });
        }
        $("#hidSectionId").val(sectionCheck);

        $("#hidDepartmentId").val('');
        if (!isDepartmentAllCheck) {
            $.each(departmentCheckedBoxes, function (index, item) {
                departmentCheck.push(item.value);
            });
        }
        $("#hidDepartmentId").val(departmentCheck);

        $("#hidInChargeId").val('');
        if (!isInChargeAllCheck) {
            $.each(inchargeCheckedBoxes, function (index, item) {
                inchargeCheck.push(item.value);
            });
        }
        $("#hidInChargeId").val(inchargeCheck);

        $("#hidRoleId").val('');
        if (!isRoleAllCheck) {
            $.each(roleCheckedBoxes, function (index, item) {
                roleCheck.push(item.value);
            });
        }
        $("#hidRoleId").val(roleCheck);

        $("#hidExplanationId").val('');
        if (!isExplanationAllCheck) {
            $.each(explanationCheckedBoxes, function (index, item) {
                explanationCheck.push(item.value);
            });
        }
        $("#hidExplanationId").val(explanationCheck);

        $("#hidCompanyid").val('');
        if (!isCompanytAllCheck) {
            $.each(companyCheckedBoxes, function (index, item) {
                companyCheck.push(item.value);
            });
        }
        $("#hidCompanyid").val(companyCheck);

        //var departmentIds = "";
        //$("#hidDepartmentId").val('');
        //if (!isDepartmentAllCheck) {
        //    $.each(departmentCheckedBoxes, function(index, item) {
        //        //sectionCheck.push(item.value);
        //        if(departmentIds == ""){
        //            departmentIds = item.value;
        //        }
        //        else{
        //            departmentIds = departmentIds + ","+item.value;
        //        }
        //    });
        //}
        //$("#hidDepartmentId").val(departmentIds);
    });

    // multiple search by incharges
    $(document).on('click', '#inchargeChks input[type="checkbox"]', function () {
        let isSectionAllChk = $("#chk_sec_all").is(':checked');
        var isDepartmentAllCheck = $("#chk_dept_all").is(':checked');
        var isInChargeAllCheck = $("#chk_incharge_all").is(':checked');
        var isRoleAllCheck = $("#chk_role_all").is(':checked');
        var isExplanationAllCheck = $("#chk_explanation_all").is(':checked');
        var isCompanytAllCheck = $("#chk_comopany_all").is(':checked');

        var clicked_checkbox = $(this).attr("id")

        if (clicked_checkbox.toLowerCase() == "chk_incharge_all") {
            $(".incharge_checkbox").prop("checked", isInChargeAllCheck);
        } else {
            $("#chk_incharge_all").prop("checked", false);
        }

        var employeeName = "";
        var sectionCheck = [];
        var departmentCheck = [];
        var inchargeCheck = [];
        var roleCheck = [];
        var explanationCheck = [];
        var companyCheck = [];



        employeeName = $('#name_search').val();
        var sectionCheckedBoxes = $('#sectionChks input[type="checkbox"]:checked');
        var departmentCheckedBoxes = $('#departmentChks input[type="checkbox"]:checked');
        var inchargeCheckedBoxes = $('#inchargeChks input[type="checkbox"]:checked');
        var roleCheckedBoxes = $('#RoleChks input[type="checkbox"]:checked');
        var explanationCheckedBoxes = $('#ExplanationChks input[type="checkbox"]:checked');
        var companyCheckedBoxes = $('#CompanyChks input[type="checkbox"]:checked');

        $("#hidSectionId").val('');
        if (!isSectionAllChk) {
            $.each(sectionCheckedBoxes, function (index, item) {
                sectionCheck.push(item.value);
            });
        }
        $("#hidSectionId").val(sectionCheck);

        $("#hidDepartmentId").val('');
        if (!isDepartmentAllCheck) {
            $.each(departmentCheckedBoxes, function (index, item) {
                departmentCheck.push(item.value);
            });
        }
        $("#hidDepartmentId").val(departmentCheck);

        $("#hidInChargeId").val('');
        if (!isInChargeAllCheck) {
            $.each(inchargeCheckedBoxes, function (index, item) {
                inchargeCheck.push(item.value);
            });
        }
        $("#hidInChargeId").val(inchargeCheck);

        $("#hidRoleId").val('');
        if (!isRoleAllCheck) {
            $.each(roleCheckedBoxes, function (index, item) {
                roleCheck.push(item.value);
            });
        }
        $("#hidRoleId").val(roleCheck);

        $("#hidExplanationId").val('');
        if (!isExplanationAllCheck) {
            $.each(explanationCheckedBoxes, function (index, item) {
                explanationCheck.push(item.value);
            });
        }
        $("#hidExplanationId").val(explanationCheck);

        $("#hidCompanyid").val('');
        if (!isCompanytAllCheck) {
            $.each(companyCheckedBoxes, function (index, item) {
                companyCheck.push(item.value);
            });
        }
        $("#hidCompanyid").val(companyCheck);

        //var inchargeIds = "";
        //$("#hidInChargeId").val('');
        //if (!isInChargeAllCheck) {
        //    $.each(inchargeCheckedBoxes, function(index, item) {
        //        //sectionCheck.push(item.value);
        //        if(inchargeIds == ""){
        //            inchargeIds = item.value;
        //        }
        //        else{
        //            inchargeIds = inchargeIds + ","+item.value;
        //        }
        //    });
        //}
        //$("#hidInChargeId").val(inchargeIds);
    });

    // multiple search by roles
    $(document).on('click', '#RoleChks input[type="checkbox"]', function () {
        let isSectionAllChk = $("#chk_sec_all").is(':checked');
        var isDepartmentAllCheck = $("#chk_dept_all").is(':checked');
        var isInChargeAllCheck = $("#chk_incharge_all").is(':checked');
        var isRoleAllCheck = $("#chk_role_all").is(':checked');
        var isExplanationAllCheck = $("#chk_explanation_all").is(':checked');
        var isCompanytAllCheck = $("#chk_comopany_all").is(':checked');

        var clicked_checkbox = $(this).attr("id")

        if (clicked_checkbox.toLowerCase() == "chk_role_all") {
            $(".role_checkbox").prop("checked", isRoleAllCheck);
        } else {
            $("#chk_role_all").prop("checked", false);
        }


        var employeeName = "";
        var sectionCheck = [];
        var departmentCheck = [];
        var inchargeCheck = [];
        var roleCheck = [];
        var explanationCheck = [];
        var companyCheck = [];



        employeeName = $('#name_search').val();
        var sectionCheckedBoxes = $('#sectionChks input[type="checkbox"]:checked');
        var departmentCheckedBoxes = $('#departmentChks input[type="checkbox"]:checked');
        var inchargeCheckedBoxes = $('#inchargeChks input[type="checkbox"]:checked');
        var roleCheckedBoxes = $('#RoleChks input[type="checkbox"]:checked');
        var explanationCheckedBoxes = $('#ExplanationChks input[type="checkbox"]:checked');
        var companyCheckedBoxes = $('#CompanyChks input[type="checkbox"]:checked');

        $("#hidSectionId").val('');
        if (!isSectionAllChk) {
            $.each(sectionCheckedBoxes, function (index, item) {
                sectionCheck.push(item.value);
            });
        }
        $("#hidSectionId").val(sectionCheck);

        $("#hidDepartmentId").val('');
        if (!isDepartmentAllCheck) {
            $.each(departmentCheckedBoxes, function (index, item) {
                departmentCheck.push(item.value);
            });
        }
        $("#hidDepartmentId").val(departmentCheck);

        $("#hidInChargeId").val('');
        if (!isInChargeAllCheck) {
            $.each(inchargeCheckedBoxes, function (index, item) {
                inchargeCheck.push(item.value);
            });
        }
        $("#hidInChargeId").val(inchargeCheck);

        $("#hidRoleId").val('');
        if (!isRoleAllCheck) {
            $.each(roleCheckedBoxes, function (index, item) {
                roleCheck.push(item.value);
            });
        }
        $("#hidRoleId").val(roleCheck);

        $("#hidExplanationId").val('');
        if (!isExplanationAllCheck) {
            $.each(explanationCheckedBoxes, function (index, item) {
                explanationCheck.push(item.value);
            });
        }
        $("#hidExplanationId").val(explanationCheck);

        $("#hidCompanyid").val('');
        if (!isCompanytAllCheck) {
            $.each(companyCheckedBoxes, function (index, item) {
                companyCheck.push(item.value);
            });
        }
        $("#hidCompanyid").val(companyCheck);

        //var roleIds = "";
        //$("#hidRoleId").val('');
        //if (!isRoleAllCheck) {
        //    $.each(roleCheckedBoxes, function(index, item) {
        //        //sectionCheck.push(item.value);
        //        if(roleIds == ""){
        //            roleIds = item.value;
        //        }
        //        else{
        //            roleIds = roleIds + ","+item.value;
        //        }
        //    });
        //}
        //$("#hidRoleId").val(roleIds);

    });

    // multiple search by explanations
    $(document).on('click', '#ExplanationChks input[type="checkbox"]', function () {
        let isSectionAllChk = $("#chk_sec_all").is(':checked');
        var isDepartmentAllCheck = $("#chk_dept_all").is(':checked');
        var isInChargeAllCheck = $("#chk_incharge_all").is(':checked');
        var isRoleAllCheck = $("#chk_role_all").is(':checked');
        var isExplanationAllCheck = $("#chk_explanation_all").is(':checked');
        var isCompanytAllCheck = $("#chk_comopany_all").is(':checked');

        var clicked_checkbox = $(this).attr("id")

        if (clicked_checkbox.toLowerCase() == "chk_explanation_all") {
            $(".explanation_checkbox").prop("checked", isExplanationAllCheck);
        } else {
            $("#chk_explanation_all").prop("checked", false);
        }

        var employeeName = "";
        var sectionCheck = [];
        var departmentCheck = [];
        var inchargeCheck = [];
        var roleCheck = [];
        var explanationCheck = [];
        var companyCheck = [];



        employeeName = $('#name_search').val();
        var sectionCheckedBoxes = $('#sectionChks input[type="checkbox"]:checked');
        var departmentCheckedBoxes = $('#departmentChks input[type="checkbox"]:checked');
        var inchargeCheckedBoxes = $('#inchargeChks input[type="checkbox"]:checked');
        var roleCheckedBoxes = $('#RoleChks input[type="checkbox"]:checked');
        var explanationCheckedBoxes = $('#ExplanationChks input[type="checkbox"]:checked');
        var companyCheckedBoxes = $('#CompanyChks input[type="checkbox"]:checked');

        $("#hidSectionId").val('');
        if (!isSectionAllChk) {
            $.each(sectionCheckedBoxes, function (index, item) {
                sectionCheck.push(item.value);
            });
        }
        $("#hidSectionId").val(sectionCheck);

        $("#hidDepartmentId").val('');
        if (!isDepartmentAllCheck) {
            $.each(departmentCheckedBoxes, function (index, item) {
                departmentCheck.push(item.value);
            });
        }
        $("#hidDepartmentId").val(departmentCheck);

        $("#hidInChargeId").val('');
        if (!isInChargeAllCheck) {
            $.each(inchargeCheckedBoxes, function (index, item) {
                inchargeCheck.push(item.value);
            });
        }
        $("#hidInChargeId").val(inchargeCheck);

        $("#hidRoleId").val('');
        if (!isRoleAllCheck) {
            $.each(roleCheckedBoxes, function (index, item) {
                roleCheck.push(item.value);
            });
        }
        $("#hidRoleId").val(roleCheck);

        $("#hidExplanationId").val('');
        if (!isExplanationAllCheck) {
            $.each(explanationCheckedBoxes, function (index, item) {
                explanationCheck.push(item.value);
            });
        }
        $("#hidExplanationId").val(explanationCheck);

        $("#hidCompanyid").val('');
        if (!isCompanytAllCheck) {
            $.each(companyCheckedBoxes, function (index, item) {
                companyCheck.push(item.value);
            });
        }
        $("#hidCompanyid").val(companyCheck);

        //var explanationIds = "";
        //$("#hidExplanationId").val('');
        //if (!isExplanationAllCheck) {
        //    $.each(explanationCheckedBoxes, function(index, item) {
        //        //sectionCheck.push(item.value);
        //        if(explanationIds == ""){
        //            explanationIds = item.value;
        //        }
        //        else{
        //            explanationIds = explanationIds + ","+item.value;
        //        }
        //    });
        //}
        //$("#hidExplanationId").val(explanationIds);
    });

    // multiple search by companies
    $(document).on('click', '#CompanyChks input[type="checkbox"]', function () {
        let isSectionAllChk = $("#chk_sec_all").is(':checked');
        var isDepartmentAllCheck = $("#chk_dept_all").is(':checked');
        var isInChargeAllCheck = $("#chk_incharge_all").is(':checked');
        var isRoleAllCheck = $("#chk_role_all").is(':checked');
        var isExplanationAllCheck = $("#chk_explanation_all").is(':checked');
        var isCompanytAllCheck = $("#chk_comopany_all").is(':checked');

        var clicked_checkbox = $(this).attr("id")

        if (clicked_checkbox.toLowerCase() == "chk_comopany_all") {
            $(".comopany_checkbox").prop("checked", isCompanytAllCheck);
        } else {
            $("#chk_comopany_all").prop("checked", false);
        }

        var employeeName = "";
        var sectionCheck = [];
        var departmentCheck = [];
        var inchargeCheck = [];
        var roleCheck = [];
        var explanationCheck = [];
        var companyCheck = [];



        employeeName = $('#name_search').val();
        var sectionCheckedBoxes = $('#sectionChks input[type="checkbox"]:checked');
        var departmentCheckedBoxes = $('#departmentChks input[type="checkbox"]:checked');
        var inchargeCheckedBoxes = $('#inchargeChks input[type="checkbox"]:checked');
        var roleCheckedBoxes = $('#RoleChks input[type="checkbox"]:checked');
        var explanationCheckedBoxes = $('#ExplanationChks input[type="checkbox"]:checked');
        var companyCheckedBoxes = $('#CompanyChks input[type="checkbox"]:checked');

        $("#hidSectionId").val('');
        if (!isSectionAllChk) {
            $.each(sectionCheckedBoxes, function (index, item) {
                sectionCheck.push(item.value);
            });
        }
        $("#hidSectionId").val(sectionCheck);

        $("#hidDepartmentId").val('');
        if (!isDepartmentAllCheck) {
            $.each(departmentCheckedBoxes, function (index, item) {
                departmentCheck.push(item.value);
            });
        }
        $("#hidDepartmentId").val(departmentCheck);

        $("#hidInChargeId").val('');
        if (!isInChargeAllCheck) {
            $.each(inchargeCheckedBoxes, function (index, item) {
                inchargeCheck.push(item.value);
            });
        }
        $("#hidInChargeId").val(inchargeCheck);

        $("#hidRoleId").val('');
        if (!isRoleAllCheck) {
            $.each(roleCheckedBoxes, function (index, item) {
                roleCheck.push(item.value);
            });
        }
        $("#hidRoleId").val(roleCheck);

        $("#hidExplanationId").val('');
        if (!isExplanationAllCheck) {
            $.each(explanationCheckedBoxes, function (index, item) {
                explanationCheck.push(item.value);
            });
        }
        $("#hidExplanationId").val(explanationCheck);

        $("#hidCompanyid").val('');
        if (!isCompanytAllCheck) {
            $.each(companyCheckedBoxes, function (index, item) {
                companyCheck.push(item.value);
            });
        }
        $("#hidCompanyid").val(companyCheck);

        //var companyIds = "";
        //$("#hidCompanyid").val('');
        //if (!isCompanytAllCheck) {
        //    $.each(companyCheckedBoxes, function(index, item) {
        //        //sectionCheck.push(item.value);
        //        if(companyIds == ""){
        //            companyIds = item.value;
        //        }
        //        else{
        //            companyIds = companyIds + ","+item.value;
        //        }
        //    });
        //}
        //$("#hidCompanyid").val(companyIds);
    });

    $('#employee_list').select2();


    // var expanded = false;
    // $(document).on("click", function (event) {
    //     var $trigger = $(".forecast_multiselect");
    //     //alert("trigger: "+$trigger);
    //     if ($trigger !== event.target && !$trigger.has(event.target).length) {
    //         $(".commonselect").slideUp("fast");
    //     }
    // });

    // ForecastSearchDropdownInLoad();
    GetListDropdownValue();
    // $('.container').blur(function (e) {
    //     $('.commonselect').fadeOut(100);
    // });
    
    $('#forecast_table').on('change', '.input_month', function () {

        //var rowId = parseInt($(this).closest('tr').data('rowid'));
        var rowId = $(this).closest('tr').find('td').eq(0).children('input').val();
        var month = parseInt($(this).data('month'));
        var unitPrice = parseFloat($('#up_' + rowId).data('unitprice').replace(/,/g, ''));
        var pointValue = parseFloat($(this).val());
        let result = 0;
        switch (month) {
            case 1:
                result = unitPrice * pointValue;
                $('#jan_output_' + rowId).val(result.toLocaleString());
                break;
            case 2:
                result = unitPrice * pointValue;
                $('#feb_output_' + rowId).val(result.toLocaleString());
                break;
            case 3:
                result = unitPrice * pointValue;
                $('#mar_output_' + rowId).val(result.toLocaleString());
                break;
            case 4:
                result = unitPrice * pointValue;
                $('#apr_output_' + rowId).val(result.toLocaleString());
                break;
            case 5:
                result = unitPrice * pointValue;
                $('#may_output_' + rowId).val(result.toLocaleString());
                break;
            case 6:
                result = unitPrice * pointValue;
                $('#jun_output_' + rowId).val(result.toLocaleString());
                break;
            case 7:
                result = unitPrice * pointValue;
                $('#jul_output_' + rowId).val(result.toLocaleString());
                break;
            case 8:
                result = unitPrice * pointValue;
                $('#aug_output_' + rowId).val(result.toLocaleString());
                break;
            case 9:
                result = unitPrice * pointValue;
                $('#sep_output_' + rowId).val(result.toLocaleString());
                break;
            case 10:
                result = unitPrice * pointValue;
                $('#oct_output_' + rowId).val(result.toLocaleString());
                break;
            case 11:
                result = unitPrice * pointValue;
                $('#nov_output_' + rowId).val(result.toLocaleString());
                break;
            case 12:
                result = unitPrice * pointValue;
                $('#dec_output_' + rowId).val(result.toLocaleString());
                break;

        }


    });

    function ShowForecastResults(year) {
        LoaderShow();
        
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

        if (_retriveddata.length > 0) {
            var headCount = _retriveddata.find(x => x.EmployeeName == 'Head Count');
            var total = _retriveddata.find(x => x.EmployeeName == 'Total');
            $("#head_total").css("display", "inline-table");
            $('#head_total tbody').append(`<tr>
                    <td>Head Count</td>
                    <td>${headCount.OctPoints}</td>
                    <td>${headCount.NovPoints}</td>
                    <td>${headCount.DecPoints}</td>
                    <td>${headCount.JanPoints}</td>
                    <td>${headCount.FebPoints}</td>
                    <td>${headCount.MarPoints}</td>
                    <td>${headCount.AprPoints}</td>
                    <td>${headCount.MayPoints}</td>
                    <td>${headCount.JunPoints}</td>
                    <td>${headCount.JulPoints}</td>
                    <td>${headCount.AugPoints}</td>
                    <td>${headCount.SepPoints}</td>
                </tr>`);

            $('#head_total tbody').append(`<tr>
                <td>Total</td>
                <td>${headCount.OctPoints}</td>
                <td>${headCount.NovPoints}</td>
                <td>${headCount.DecPoints}</td>
                <td>${headCount.JanPoints}</td>
                <td>${headCount.FebPoints}</td>
                <td>${headCount.MarPoints}</td>
                <td>${headCount.AprPoints}</td>
                <td>${headCount.MayPoints}</td>
                <td>${headCount.JunPoints}</td>
                <td>${headCount.JulPoints}</td>
                <td>${headCount.AugPoints}</td>
                <td>${headCount.SepPoints}</td>
            </tr>`);

            _retriveddata = _retriveddata.filter(d => d.EmployeeId!==0);
        }

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
                                        
        jss = $('#jspreadsheet').jspreadsheet({
            data: _retriveddata,
            filters: true,
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
                { title: "グレード(Grade)", type: "dropdown", source: gradesForJexcel, name: "GradeId", width: 60  },
                { title: "単価(Unit Price)", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },
                {
                    title: "10月",
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal:'.'
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
            ],
            minDimensions: [6, 10],
            columnSorting: true,
            onbeforechange: function (instance, cell, x, y, value) {
                
                //alert(value);
                if (x==11) {
                    beforeChangedValue = jss.getValueFromCoords(x,y);
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
                        //console.log(checkId);

                        if (x == 2) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 3) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 4) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 5) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 6) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 7) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 8) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                        }
                        if (x == 9) {
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
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
                        }
                    }

                }

            },
            oninsertrow: newRowInserted,
            ondeleterow: deleted,
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
                    title: '要員を追加する(emp add)',
                    onclick: function () {
                        obj.insertRow(1, parseInt(y));
                        $('#jexcel_add_employee_modal').modal('show');
                        globalY = parseInt(y) + 1;

                        //console.log("y: "+y);
                        //console.log("globalY: "+globalY);

                        GetEmployeeList();
                    }
                });
                items.push({
                    title: '同じ要員を複製する(emp duplication)',
                    onclick: function () {
                        //debugger;
                        var allData = jss.getData();
                        let nextRow = parseInt(y) + 1;

                        obj.insertRow(1, parseInt(y));

                        var retrivedData = retrivedObject(jss.getRowData(y));
                        
                        retrivedData.assignmentId = "new-" + newRowCount;
                        debugger;
                        var allSpecificObjectsCount = 0;
                        for (let x of allData) {
                            //console.log(x);
                            if (x[35] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                            }
                        }

                        //var firstIndex = retrivedData.employeeName.indexOf('(');
                        var lastIndex = retrivedData.employeeName.lastIndexOf(' ');
                        var totalLength = retrivedData.employeeName.length;
                        console.log("retrivedData1: "+retrivedData.employeeName);
                        console.log("totalLength: "+totalLength);
                        //console.log("firstIndex: "+firstIndex);

                        if(lastIndex<0){
                            obj.setValueFromCoords(1, nextRow, retrivedData.employeeName + ` ${allSpecificObjectsCount+1}`, false);
                        }
                        else{
                            var empNumber = retrivedData.employeeName.substring(lastIndex, (totalLength - 1)).trim();
                            if(isNaN(empNumber)){
                                obj.setValueFromCoords(1, nextRow, retrivedData.employeeName + ` ${allSpecificObjectsCount+1}`, false);
                            }
                            else{
                                retrivedData.employeeName = retrivedData.employeeName.slice(0, -(totalLength - lastIndex));
                                obj.setValueFromCoords(1, nextRow, retrivedData.employeeName + ` ${allSpecificObjectsCount+1}`, false);
                                console.log("retrivedData2: "+retrivedData.employeeName);
                               
                            }
                        }
                       
                        
                        
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
                    }
                });


                if (obj.options.allowDeleteRow == true) {
                    items.push({
                        title: '選択した要員を削除(selected emp delete)',
                        onclick: function () {
                            obj.deleteRow(obj.getSelectedRows().length ? undefined : parseInt(y));
                        }
                    });
                }
                
                return items;
            }
        });
        
        $("#update_forecast_history").css("display", "block");

        jss.deleteColumn(36, 15);
        LoaderHide();
    }

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
            year: 2023
        };
    }

    //$.connection.chatHub.disconnected(function () {
    //    setTimeout(function () {
    //        $.connection.hub.start();
    //    }, 3000); // Restart connection after 3 seconds.
    //});

    $('#update_forecast_history').on('click', function () {
        if (jssUpdatedData.length > 0) {
            $.ajax({
            url: `/api/utilities/GetMatchedRowNumber/`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
            success: function (data) {
                $('#row_count').empty();
                $('#row_count').append(data);
                }
            });
        }
        $('#update_forecast').modal('show');
    });

    // $('#update_forecast_history').on('click', function () {
        
                  
    //     console.log(jssInsertedData);    
    //     var dateObj = new Date();
    //     var month = dateObj.getUTCMonth() + 1; //months from 1-12
    //     var day = dateObj.getDate();
    //     var year = dateObj.getUTCFullYear();

    //     var timestamp = `${year}${month}${day}_`;
    //     var promptValue = prompt("History Save As", timestamp);
    //     if (promptValue == null || promptValue == undefined || promptValue == "") {
    //         return false;
    //     }
    //     else {
    //         if (jssInsertedData.length > 0) {
    //             var elementIndex = jssInsertedData.findIndex(object => {

    //                 return object.employeeName.toLowerCase() == 'total';

    //             });
    //             if (elementIndex >= 0) {
    //                 jssInsertedData.splice(elementIndex, 1);
    //             }

    //         }
    //         if (jssInsertedData.length > 0) {
    //             //console.log('clicked');

    //             $.ajax({
    //                 url: `/api/utilities/ExcelAssignment/`,
    //                 contentType: 'application/json',
    //                 type: 'POST',
    //                 async: false,
    //                 dataType: 'json',
    //                 data: JSON.stringify(jssInsertedData),
    //                 success: function (data) {
    //                     var chat = $.connection.chatHub;
    //                     $.connection.hub.start();
    //                     // Start the connection.
    //                     $.connection.hub.start().done(function () {
    //                         chat.server.send('data has been inserted by', 'user');
    //                     });
    //                 }
    //             });
    //             jssInsertedData = [];
    //             newRowCount = 1;
    //         }


    //         if (jssUpdatedData.length > 0) {
    //             console.log(jssUpdatedData);
    //             $.ajax({
    //                 url: `/api/utilities/CreateHistory`,
    //                 contentType: 'application/json',
    //                 type: 'POST',
    //                 async: false,
    //                 dataType: 'json',
    //                 data: JSON.stringify({ ForecastUpdateHistoryDtos:jssUpdatedData, HistoryName: promptValue }),
    //                 success: function (data) {
    //                     var chat = $.connection.chatHub;
    //                     $.connection.hub.start();
    //                     // Start the connection.
    //                     $.connection.hub.start().done(function () {
    //                         chat.server.send('data has been updated by', 'user');
    //                     });
    //                 }
    //             });
    //             jssUpdatedData = [];
    //         }
    //     }
    //     $("#loading").css("display", "none");


    // });

    $(function () {
        var chat = $.connection.chatHub;
        $.connection.hub.start();
        chat.client.addNewMessageToPage = function (name, message) {
            // Add the message to the page.
            $('#save_notifications').append(`<li>${name} ${message}</li>`);
        };

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
    
    $(document).on('change', '#assignment_year_list', function () {
        var assignmentYear = $("#hidDefaultForecastYear").val();
        ShowForecastResults(assignmentYear);
    });

});

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

function GetListDropdownValue() {
    $.getJSON('/api/sections/')
        .done(function (data) {
            $('#section_multi_search').empty();
            $.each(data, function (key, item) {
                $('#section_multi_search').append(`<option class='section_checkbox' id="section_checkbox_${item.Id}" value='${item.Id}' >${item.SectionName}</option>`)
            });
            $('#section_multi_search').multiselect('rebuild');
        });
    // var tempDepartmentId = $("#hid_departmentId").val();
    // $.getJSON('/api/Departments/')

    //     .done(function (data) {
    //         $('#dept_multi_search').empty();
    //         $.each(data, function (key, item) {
    //             if(item.Id == tempDepartmentId){
    //                 $('#dept_multi_search').attr("disabled", true); 
    //                 $('#dept_multi_search').append(`<option selected readonly class='department_checkbox' id="department_checkbox_${item.Id}" value='${item.Id}'>${item.DepartmentName}</option>`)   
    //             }                
    //         });
    //         $('#dept_multi_search').multiselect('rebuild');
    //     });    
    //department multi select dropdown 
    $.getJSON('/api/Departments/')
        .done(function (data) {
            $('#dept_multi_search').empty();
            $.each(data, function (key, item) {
                $('#dept_multi_search').append(`<option class='department_checkbox' id="department_checkbox_${item.Id}" value='${item.Id}'>${item.DepartmentName}</option>`)
            });
            $('#dept_multi_search').multiselect('rebuild');
        });
    // incharge multi select dropdown 
    $.getJSON('/api/InCharges/')
        .done(function (data) {
            $('#incharge_multi_search').empty();
            $.each(data, function (key, item) {
                $('#incharge_multi_search').append(`<option class='incharge_checkbox' id="incharge_checkbox_${item.Id}" value='${item.Id}'>${item.InChargeName}</option>`)
            });
            $('#incharge_multi_search').multiselect('rebuild');
        });
    //Role multi select dropdown 
    $.getJSON('/api/Roles/')
        .done(function (data) {
            $('#role_multi_search').empty();
            $.each(data, function (key, item) {
                $('#role_multi_search').append(`<option class='role_checkbox' id="role_checkbox_${item.Id}" value='${item.Id}'>${item.RoleName}</option>`)
            });
            $('#role_multi_search').multiselect('rebuild');
        });

    //explanation multi search dropdown
    $.getJSON('/api/Explanations/')
        .done(function (data) {
            $('#explanation_multi_search').empty();
            $.each(data, function (key, item) {
                $('#explanation_multi_search').append(`<option class='explanation_checkbox' id="explanation_checkbox_${item.Id}" value='${item.Id}'>${item.ExplanationName}</option>`)
            });
            $('#explanation_multi_search').multiselect('rebuild');
        });
    $.getJSON('/api/Companies/')
        .done(function (data) {
            $('#company_multi_search').empty();
            $.each(data, function (key, item) {
                $('#company_multi_search').append(`<option class='comopany_checkbox' id="comopany_checkbox_${item.Id}" value='${item.Id}'>${item.CompanyName}</option>`)
            });
            $('#company_multi_search').multiselect('rebuild');
        });

    //$('#period_multi_search').empty();
    //$('#period_multi_search').append(`<option class='period_checkbox' id="period_checkbox" value='2023'>2023</option>`)
    //$('#period_multi_search').multiselect('rebuild');
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
    $('#update_forecast').modal('hide');
    $("#loading").css("display", "block"); 

    console.log(jssInsertedData); 
    var dateObj = new Date();
    var month = dateObj.getUTCMonth() + 1; //months from 1-12
    var day = dateObj.getDate();
    var year = dateObj.getUTCFullYear();

    var timestamp = `${year}${month}${day}_`;
    //var promptValue = prompt("History Save As", timestamp);
    
    var promptValue = "nothing";
    if (promptValue == null || promptValue == undefined || promptValue == "") {
        return false;
    }
    else {
        if (jssInsertedData.length > 0) {
            var elementIndex = jssInsertedData.findIndex(object => {

                return object.employeeName.toLowerCase() == 'total';

            });
            if (elementIndex >= 0) {
                jssInsertedData.splice(elementIndex, 1);
            }

        }
        if (jssInsertedData.length > 0) {
            //console.log('clicked');

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
                        chat.server.send('data has been inserted', '');
                    });
                }
            });
            jssInsertedData = [];
            newRowCount = 1;
        }

        
        if (jssUpdatedData.length > 0) {
            console.log(jssUpdatedData);
            $.ajax({
                // url: `/api/utilities/CreateHistory`,
                url: `/api/utilities/UpdateForecastData`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: promptValue }),
                success: function (data) {
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been updated','');
                    });
                }
            });
            jssUpdatedData = [];
        }
    }
    $("#loading").css("display", "none");

}
function CompareUpdatedData() {
    
    //window.location.replace("/Forecasts/GetHistories");
    if (jssUpdatedData.length > 0) {
        $('#display_matched_rows').css('display', 'block');

        $.ajax({
            // url: `/api/utilities/CreateHistory`,
            url: `/api/utilities/GetMatchedRows`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
            success: function (data) {
                $('#display_matched_rows table tbody').empty();
                console.log(data);
                $.each(data, function (index, element) {
                    console.log(element.CreatedBy);
                    $('#display_matched_rows table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                });
            }
        });
    }
}
function ImportCSVFile(){
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
                chat.server.send('data has been inserted by', 'user');
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
            $('#assignment_year_list').append(`<option value=''>select year</option>`);
            var count =1;
            $.each(data, function (index, element) {
                if(count==1){
                    $("#hidDefaultForecastYear").val(element.Year)
                }
                $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
                count++;
            });
        }
    });
}

function CheckForecastYear(){
    var year = $('#assignment_year_list').find(":selected").val();
    if(year!=""){
        $('#inputState').val(parseInt(year)+1);
    }
}
function ValidateYear(){
    var selectedYear = $('#inputState').find(":selected").val();
    if(selectedYear ==""){
        return false;
    }
    
}
function CheckDuplicateYear(){
    var year = $('#assignment_year_list').find(":selected").val();
    if(year!=""){
        $('#duplciateYear').val(parseInt(year)+1);
    }
}
function DuplicateForecast(){
    var insertYear  = $('#duplciateYear').find(":selected").val();
    var copyYear = $('#assignment_year_list').find(":selected").val();

    if(copyYear!="" && insertYear!=""){
        $.ajax({
            url: `/api/utilities/DuplicateForecastYear`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "copyYear=" + copyYear+"&insertYear="+insertYear,
            success: function (data) {
                // $('#assignment_year_list').append(`<option value=''>select year</option>`);
                // $.each(data, function (index, element) {
                //     $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
                // });
            }
        });
    }else{
        alert("please select year!");
        return false;
    }
}
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
    $("#head_total").css("display", "none");
    
}
function LoaderHideJexcel(){
    $("#jspreadsheet").show();        
    $("#head_total").css("display", "table !important");
    $("#loading").css("display", "none");
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
        var assignmentYear = $("#hidDefaultForecastYear").val();
        $('#assignment_year_list').val(assignmentYear);      
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
    
    $(document).on('click', '#assignment_year_data', function () {
        LoaderShowJexcel();
        setTimeout(function () {
            
            var assignmentYear = $('#assignment_year_list').val();
            if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
                alert('Select valid year!!!');
                return false;
            }
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
    //LoaderHide();
    if (_retriveddata.length > 0) {
        var headCount = _retriveddata.find(x => x.EmployeeName == 'Head Count');
        var total = _retriveddata.find(x => x.EmployeeName == 'Total');
        $("#head_total").css("display", "inline-table");
        $('#head_total tbody').empty();
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

        _retriveddata = _retriveddata.filter(d => d.EmployeeId !== 0);
    }

    //return false;   

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
        tableOverflow: true,
        tableWidth: window.innerWidth - 300 + 'px',
        freezeColumns: 3,
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
            { title: "グレード(Grade)", type: "dropdown", source: gradesForJexcel, name: "GradeId", width: 60 },
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
                    console.log("retrivedData1: " + retrivedData.employeeName);
                    console.log("totalLength: " + totalLength);
                    //console.log("firstIndex: "+firstIndex);

                    if (lastIndex < 0) {
                        obj.setValueFromCoords(1, nextRow, retrivedData.employeeName + ` ${allSpecificObjectsCount + 1}`, false);
                    }
                    else {
                        var empNumber = retrivedData.employeeName.substring(lastIndex, (totalLength - 1)).trim();
                        if (isNaN(empNumber)) {
                            obj.setValueFromCoords(1, nextRow, retrivedData.employeeName + ` ${allSpecificObjectsCount + 1}`, false);
                        }
                        else {
                            retrivedData.employeeName = retrivedData.employeeName.slice(0, -(totalLength - lastIndex));
                            obj.setValueFromCoords(1, nextRow, retrivedData.employeeName + ` ${allSpecificObjectsCount + 1}`, false);
                            console.log("retrivedData2: " + retrivedData.employeeName);

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
    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    jexcelHeadTdEmployeeName.addClass('arrow-down');
    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelFirstHeaderRow.css('top', '0px');
    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelSecondHeaderRow.css('top', '20px');
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
        year: document.getElementById('assignment_year_list').value
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
    $("#head_total").hide();
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
            $.ajax({
                url: `/api/utilities/UpdateForecastData`,
                contentType: 'application/json',
                type: 'POST',
                async: true,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue }),
                success: function (data) {
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been updated by ', userName);
                    });        
                    $("#jspreadsheet").show();
                    $("#head_total").show();
                    LoaderHide();             
                }
            });
            jssUpdatedData = [];
        }
    }
    else {
        $("#jspreadsheet").show();
        $("#head_total").show();
        LoaderHide();    
        alert('No data to update!!!');
    }
    if (jssInsertedData.length > 0) {
        var elementIndex = jssInsertedData.findIndex(object => {
            return object.employeeName.toLowerCase() == 'total';
        });
        if (elementIndex >= 0) {
            jssInsertedData.splice(elementIndex, 1);
        }


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
                $("#head_total").show();
                LoaderHide(); 
            }
        });
        jssInsertedData = [];
        newRowCount = 1;
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
            $('#assignment_year_list').append(`<option value=''>select year</option>`);
            $('#select_year_to_import').append(`<option value=''>select year</option>`);
            $('#replicate_from').append(`<option value=''>select year</option>`);
            var count =1;
            $.each(data, function (index, element) {
                if(count==1){
                    $("#hidDefaultForecastYear").val(element.Year)
                }
                $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
                $('#select_year_to_import').append(`<option value='${element.Year}'>${element.Year}</option>`);
                $('#replicate_from').append(`<option value='${element.Year}'>${element.Year}</option>`);
                count++;
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
function ValidateYear(){
    $("#csv_import_modal").modal("hide");
    LoaderShow();
    // var selectedYear = $('#inputState').find(":selected").val();
    var selectedYear = $('#select_import_year').find(":selected").val();
    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("please select year!");
        return false;
    }
    
}
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

var jss;
var jss_1;
var _retriveddata;
var _retriveddata_1;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];
var beforeChangedValue = 0;
var loadFlag = 0;
var loadFlag1 = 0;



const channel = new BroadcastChannel("actualCost");

function retrivedObject(rowData) {
    return {
        employeeId: rowData[0],
        employeeName: rowData[1],
        DepartmentId: rowData[2],
        octPoint: parseFloat(rowData[3]),
        novPoint: parseFloat(rowData[4]),
        decPoint: parseFloat(rowData[5]),
        janPoint: parseFloat(rowData[6]),
        febPoint: parseFloat(rowData[7]),
        marPoint: parseFloat(rowData[8]),
        aprPoint: parseFloat(rowData[9]),
        mayPoint: parseFloat(rowData[10]),
        junPoint: parseFloat(rowData[11]),
        julPoint: parseFloat(rowData[12]),
        augPoint: parseFloat(rowData[13]),
        sepPoint: parseFloat(rowData[14])
    };
}

function find_duplicate_in_array(arra1) {
    var object = {};
    var result = [];

    arra1.forEach(function (item) {
        if (!object[item])
            object[item] = 0;
        object[item] += 1;
    })

    for (var prop in object) {
        if (object[prop] >= 2) {
            result.push(prop);
        }
    }

    return result;

}

function LoadJexcel() {
    var year = $('#assignment_year').val();

    if (year == null || year == '' || year == undefined) {
        alert('Select Year!!!');
        return false;
    }
    //LoaderShow();
    setTimeout(function () {
        $.ajax({
            url: '/Registration/GetUserRole',
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                if (parseInt(data) === 1 || parseInt(data) === 2) {
                    userRoleflag = false;
                }
                else {
                    userRoleflag = true;
                }
            }
        });
        // 1st jexcel

        {
        //LoaderHide();

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
            url: `/api/Departments`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {

                $.each(data, (index, value) => {
                    if (value.DepartmentName !== '品証') {
                        departmentsForJexcel.push({ id: value.Id, name: value.DepartmentName });
                    }

                });
            }
        });


        jss = $('#jspreadsheet').jspreadsheet({
            data: _retriveddata,
            filters: true,
            tableOverflow: true,
            freezeColumns: 3,
            defaultColWidth: 50,
            tableWidth: (window.innerWidth - 300) + "px",
            tableHeight: (window.innerHeight - 300) + "px",

            columns: [
                { title: "Employee Id", type: 'text', name: "EmployeeId", type: 'hidden' },

                { title: "要員名(Emp)", type: 'text', name: "EmployeeName", width: 100 },

                { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },

                { title: "10月", type: "decimal", name: "OctPercentage", mask: "#,## %", width: 100 },

                { title: "11月", type: "decimal", name: "NovPercentage", mask: "#.## %", width: 100 },

                { title: "12月", type: "decimal", name: "DecPercentage", mask: "#.## %", width: 100 },

                { title: "1月", type: "decimal", name: "JanPercentage", mask: "#.## %", width: 100 },

                { title: "2月", type: "decimal", name: "FebPercentage", mask: "#.## %", width: 100 },

                { title: "3月", type: "decimal", name: "MarPercentage", mask: "#.## %", width: 100 },

                { title: "4月", type: "decimal", name: "AprPercentage", mask: "#.## %", width: 100 },

                { title: "5月", type: "decimal", name: "MayPercentage", mask: "#.## %", width: 100 },

                { title: "6月", type: "decimal", name: "JunPercentage", mask: "#.## %", width: 100 },

                { title: "7月", type: "decimal", name: "JulPercentage", mask: "#.## %", width: 100 },

                { title: "8月", type: "decimal", name: "AugPercentage", mask: "#.## %", width: 100 },

                { title: "9月", type: "decimal", name: "SepPercentage", mask: "#.## %", width: 100 },

                { title: "Id", type: 'text', name: "Id", type: 'hidden' },

            ],
            columnSorting: true,
            contextMenu: function (obj, x, y, e) {
                var items = [];
                var nextRow = parseInt(y) + 1;
                items.push({
                    title: 'duplicate',
                    onclick: () => {
                        obj.insertRow(1, parseInt(y));
                        var retrivedData = retrivedObject(jss.getRowData(y));

                        obj.setValueFromCoords(0, nextRow, retrivedData.employeeId, false);
                        obj.setValueFromCoords(1, nextRow, retrivedData.employeeName, false);
                        obj.setValueFromCoords(2, nextRow, null, false);
                        obj.setValueFromCoords(3, nextRow, 0, false);
                        obj.setValueFromCoords(4, nextRow, 0, false);
                        obj.setValueFromCoords(5, nextRow, 0, false);
                        obj.setValueFromCoords(6, nextRow, 0, false);
                        obj.setValueFromCoords(7, nextRow, 0, false);
                        obj.setValueFromCoords(8, nextRow, 0, false);
                        obj.setValueFromCoords(9, nextRow, 0, false);
                        obj.setValueFromCoords(10, nextRow, 0, false);
                        obj.setValueFromCoords(11, nextRow, 0, false);
                        obj.setValueFromCoords(12, nextRow, 0, false);
                        obj.setValueFromCoords(13, nextRow, 0, false);
                        obj.setValueFromCoords(14, nextRow, 0, false);
                    }
                });

                return items;
            },
            onbeforechange: function (instance, cell, x, y, value) {

                //alert(value);
                if (x == 3) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 4) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 5) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 6) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 7) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 8) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 9) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 10) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
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
            },
            onchange: (instance, cell, x, y, value) => {
                //if (x == 2) {
                //    debugger;
                //    var deptCount = 0;
                //    var employeeId = jss.getValueFromCoords(0,y);
                //    $.each(jss.getData(), (index, itemValue) => {
                //        //if (itemValue[2] != null || itemValue[2] != '' || itemValue[2] != undefined) {
                //            if (employeeId != null && (itemValue[0].toString() == employeeId.toString())) {
                //                if (itemValue[2].toString() == value.toString()) {
                //                    deptCount++;
                //                }
                //            }
                //        //}
                //    });

                //    if (deptCount > 1) {
                //        alert('Duplicate Department');
                //        var rowNumber = parseInt(y) + 1;
                //        var element = $(`#jspreadsheet .jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                //        element[0].cells[3].innerText = '';
                //        console.log(element[0]);
                //        return false;
                //    }
                //}
                if (x == 3) {
                    var octSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            octSum += parseFloat(parseFloat(dataValue[3]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || octSum > 100) {
                        octSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 4) {
                    var novSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            novSum += parseFloat(parseFloat(dataValue[4]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || novSum > 100) {
                        novSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 5) {
                    var decSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            decSum += parseFloat(parseFloat(dataValue[5]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || decSum > 100) {
                        decSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 6) {
                    var janSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            janSum += parseFloat(parseFloat(dataValue[6]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || janSum > 100) {
                        janSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 7) {
                    var febSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            febSum += parseFloat(parseFloat(dataValue[7]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || febSum > 100) {
                        febSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 8) {
                    var marSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            marSum += parseFloat(parseFloat(dataValue[8]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || marSum > 100) {
                        marSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 9) {
                    var aprSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            aprSum += parseFloat(parseFloat(dataValue[9]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || aprSum > 100) {
                        aprSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 10) {
                    var maySum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            maySum += parseFloat(parseFloat(dataValue[10]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || maySum > 100) {
                        maySum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 11) {
                    var junSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            junSum += parseFloat(parseFloat(dataValue[11]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || junSum > 100) {
                        junSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 12) {
                    var julSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            julSum += parseFloat(parseFloat(dataValue[12]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || julSum > 100) {
                        julSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 13) {
                    var augSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            augSum += parseFloat(parseFloat(dataValue[13]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || augSum > 100) {
                        augSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
                if (x == 14) {
                    var sepSum = 0;
                    var employeeId = jss.getValueFromCoords(0, y);
                    $.each(jss.getData(), (index, dataValue) => {
                        if (dataValue[0].toString() == employeeId.toString()) {
                            sepSum += parseFloat(parseFloat(dataValue[14]));
                        }

                    });

                    if (isNaN(value) || parseFloat(value) < 0 || sepSum > 100) {
                        sepSum = 0;
                        alert('Input not valid');
                        jss.setValueFromCoords(x, y, beforeChangedValue, false);

                    }
                    //else {

                    //    if (dataCheck.length == 0) {
                    //        jssUpdatedData.push(retrivedData);
                    //    }
                    //    else {
                    //        updateArray(jssUpdatedData, retrivedData);
                    //    }

                    //}
                }
            }
        });
        //jss.deleteColumn(21, 4);

    } 


        //$.ajax({
        //    url: `/api/utilities/QaAssignmentTotal?year=${year}`,
        //    contentType: 'application/json',
        //    type: 'GET',
        //    async: false,
        //    dataType: 'json',
        //    success: function (assignmentData) {
        //        $.each(assignmentData, (index, itemValue) => {
        //            $('#qa_assignments').append(`<tr><td>${itemValue.EmployeeName}</td><td>${itemValue.OctTotal}</td><td>${itemValue.NovTotal}</td><td>${itemValue.DecTotal}</td><td>${itemValue.JanTotal}</td><td>${itemValue.FebTotal}</td><td>${itemValue.MarTotal}</td><td>${itemValue.AprTotal}</td><td>${itemValue.MayTotal}</td><td>${itemValue.JunTotal}</td><td>${itemValue.JulTotal}</td><td>${itemValue.AugTotal}</td><td>${itemValue.SepTotal}</td></tr>`);
        //        });
        //    }
        //});



    }, 3000);




}

function LoadJexcel1() {
    // another jexcel table
    var year = $('#assignment_year').val();

    if (year == null || year == '' || year == undefined) {
        alert('Select Year!!!');
        return false;
    }
   // LoaderShow();
    //$.ajax({
    //    url: `/api/utilities/CreateApportionment?year=${year}`,
    //    contentType: 'application/json',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    success: function (data1) 
    {
            //LoaderHide();
            //_retriveddata_1 = data1;

            if (jss_1 != undefined) {
                jss_1.destroy();
                $('#jspreadsheet_1').empty();
            }

            jss_1 = $('#jspreadsheet_1').jspreadsheet({
                data: _retriveddata_1,
                //filters: true,
                tableOverflow: true,
                //freezeColumns: 3,
                defaultColWidth: 100,
                tableWidth: (window.innerWidth - 300) + "px",
                tableHeight: (window.innerHeight - 300) + "px",
                columns: [
                    { title: "Department Id", type: 'hidden', name: "DepartmentId" },
                    { title: "部門名 (Dept.)", type: 'text', name: "DepartmentName" },

                    { title: "10月 (QA ratio)", type: "decimal", name: "OctPercentage", mask: "#,## %", width: 100 },

                    { title: "11月 (QA ratio)", type: "decimal", name: "NovPercentage", mask: "#.## %", width: 100 },

                    { title: "12月 (QA ratio)", type: "decimal", name: "DecPercentage", mask: "#.## %", width: 100 },

                    { title: "1月 (QA ratio)", type: "decimal", name: "JanPercentage", mask: "#.## %", width: 100 },

                    { title: "2月 (QA ratio)", type: "decimal", name: "FebPercentage", mask: "#.## %", width: 100 },

                    { title: "3月 (QA ratio)", type: "decimal", name: "MarPercentage", mask: "#.## %", width: 100 },


                    { title: "4月 (QA ratio)", type: "decimal", name: "AprPercentage", mask: "#.## %", width: 100 },

                    { title: "5月 (QA ratio)", type: "decimal", name: "MayPercentage", mask: "#.## %", width: 100 },

                    { title: "6月 (QA ratio)", type: "decimal", name: "JunPercentage", mask: "#.## %", width: 100 },

                    { title: "7月 (QA ratio)", type: "decimal", name: "JulPercentage", mask: "#.## %", width: 100 },

                    { title: "8月 (QA ratio)", type: "decimal", name: "AugPercentage", mask: "#.## %", width: 100 },

                    { title: "9月 (QA ratio)", type: "decimal", name: "SepPercentage", mask: "#.## %", width: 100 },
                    { title: "Id", type: 'hidden', name: "Id" },
                ],
                //onchange: function (instance, cell, x, y, value) {
                //    var count = 0;
                //    var allPercentage = jss.getData();
                //    $.each(allPercentage, function (index, value) {
                //        count += value[x];
                //    });

                //    if (count > 100 || count < 0) {
                //        alert("invalid value!");
                //        jss.setValueFromCoords(x, y, 0, false);
                //    }
                //},
            });
    }
    //});
}

function LoaderShow() {
    $("#jspreadsheet").hide();
    $("#loading").css("display", "block");
}
function LoaderHide() {
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
$(document).ready(function () {

    
    // LoaderHide();


    $('#employee_wise_save_button').on('click', () => {
       
        let duplicateEmployeeId = '';
        let duplicateEmployeeName = '';

        let flag = true;
        let flag1 = true;
        if (jss != undefined) {
            $.each(jss.getData(), (index, itemValue) => {
                if (itemValue[2] == null || itemValue[2] == '' || itemValue[2] == undefined) {
                    alert('invalid department!');
                    flag = false;
                    return false;
                }
            });
            if (flag) {
                let employeeIds = [];
                $.each(jss.getData(), (index, itemValue) => {
                    employeeIds.push(parseInt(itemValue[0]));
                });
                let uniqueEmployeeIds = employeeIds.filter((value, index, array)=> {
                    return array.indexOf(value) === index;
                });

                let departmentIds = [];
                for (let i = 0; i < uniqueEmployeeIds.length;i++) {
                    $.each(jss.getData(), (index1, itemValue1) => {
                        if (uniqueEmployeeIds[i].toString() == itemValue1[0].toString()) {
                            departmentIds.push(parseInt(itemValue1[2]));
                        }
                        
                    });
                    // find duplicate departments...
                    let duplicateElements = find_duplicate_in_array(departmentIds);
                    if (duplicateElements.length > 0) {
                        
                        departmentIds = [];
                        duplicateEmployeeId = uniqueEmployeeIds[i];
                        flag1 = false;
                        break;
                    }
                    else {
                        departmentIds = [];
                        var oct_sum = 0;
                        var nov_sum = 0;
                        var dec_sum = 0;
                        var jan_sum = 0;
                        var feb_sum = 0;
                        var mar_sum = 0;
                        var apr_sum = 0;
                        var may_sum = 0;
                        var jun_sum = 0;
                        var jul_sum = 0;
                        var aug_sum = 0;
                        var sep_sum = 0;

                        $.each(jss.getData(), (index1, itemValue1) => {
                            if (uniqueEmployeeIds[i].toString() == itemValue1[0].toString()) {
                                oct_sum += parseFloat(itemValue1[3]);
                                nov_sum += parseFloat(itemValue1[4]);
                                dec_sum += parseFloat(itemValue1[5]);
                                jan_sum += parseFloat(itemValue1[6]);
                                feb_sum += parseFloat(itemValue1[7]);
                                mar_sum += parseFloat(itemValue1[8]);
                                apr_sum += parseFloat(itemValue1[9]);
                                may_sum += parseFloat(itemValue1[10]);
                                jun_sum += parseFloat(itemValue1[11]);
                                jul_sum += parseFloat(itemValue1[12]);
                                aug_sum += parseFloat(itemValue1[13]);
                                sep_sum += parseFloat(itemValue1[14]);
                            }

                        }); // end of each...
                        
                        if (oct_sum !=100) {
                            alert('Invalid Month (Oct) Value ' + oct_sum);
                            return false;
                        }
                        if (nov_sum != 100) {
                            alert('Invalid Month (Nov) Value ' + nov_sum);
                            return false;
                        }
                        if (dec_sum != 100) {
                            alert('Invalid Month (Dec) Value ' + dec_sum);
                            return false;
                        }
                        if (jan_sum != 100) {
                            alert('Invalid Month (Jan) Value ' + jan_sum);
                            return false;
                        }
                        if (feb_sum != 100) {
                            alert('Invalid Month (Feb) Value ' + feb_sum);
                            return false;
                        }
                        if (mar_sum != 100) {
                            alert('Invalid Month (Mar) Value ' + mar_sum);
                            return false;
                        }
                        if (apr_sum != 100) {
                            alert('Invalid Month (Apr) Value ' + apr_sum);
                            return false;
                        }
                        if (may_sum != 100) {
                            alert('Invalid Month (May) Value ' + may_sum);
                            return false;
                        }
                        if (jun_sum != 100) {
                            alert('Invalid Month (Jun) Value ' + jun_sum);
                            return false;
                        }
                        if (jul_sum != 100) {
                            alert('Invalid Month (Jul) Value ' + jul_sum);
                            return false;
                        }
                        if (aug_sum != 100) {
                            alert('Invalid Month (Aug) Value ' + aug_sum);
                            return false;
                        }
                        if (sep_sum != 100) {
                            alert('Invalid Month (Sep) Value ' + sep_sum);
                            return false;
                        }

                    }
                }

                if (flag1) {
                    var year = $('#assignment_year').val();
                    let employeeWiseProportionObjectList = [];
                    let employeeWiseProportionList = jss.getData();
                    if (employeeWiseProportionList.length > 0) {
                        $.each(employeeWiseProportionList, (index, singleItemValue) => {
                            employeeWiseProportionObjectList.push({
                                EmployeeId: singleItemValue[0],
                                DepartmentId: singleItemValue[2],
                                OctPercentage: singleItemValue[3],
                                NovPercentage: singleItemValue[4],
                                DecPercentage: singleItemValue[5],
                                JanPercentage: singleItemValue[6],
                                FebPercentage: singleItemValue[7],
                                MarPercentage: singleItemValue[8],
                                AprPercentage: singleItemValue[9],
                                MayPercentage: singleItemValue[10],
                                JunPercentage: singleItemValue[11],
                                JulPercentage: singleItemValue[12],
                                AugPercentage: singleItemValue[13],
                                SepPercentage: singleItemValue[14],
                                Id: singleItemValue[15]
                            });
                        });

                        $.ajax({
                            url: `/api/utilities/CreateQaProportion`,
                            contentType: 'application/json',
                            type: 'POST',
                            async: false,
                            dataType: 'json',
                            data: JSON.stringify({ QaProportionViewModels: employeeWiseProportionObjectList, Year: year }),
                            success: function (data) {
                                //$("#timeStamp_ForUpdateData").val(data);
                                //var chat = $.connection.chatHub;
                                //$.connection.hub.start();
                                // Start the connection.
                                //$.connection.hub.start().done(function () {
                                //    chat.server.send('data has been updated by ', userName);
                                //});
                                //$("#jspreadsheet").show();
                                //$("#head_total").show();
                                employeeWiseProportionObjectList = [];
                                alert(data);
                                LoaderHide();
                            }
                        });
                    } else {
                        alert('No data found!');
                        return false;
                    }
                }
                else {
                    var allArrayData = jss.getData();
                    for (var i = 0; i < allArrayData.length; i++) {
                        if (allArrayData[i][0].toString() == duplicateEmployeeId.toString()) {
                            duplicateEmployeeName = allArrayData[i][1];
                            break;
                        }
                        
                    }
                    alert('duplicate departments found for ' + duplicateEmployeeName);
                    return false;
                }
            }
           
           
        }
        else {
            alert('No table found!');
            return false;
        }

    });



    $('#department_wise_save_button').click(function () {
        var dataToSend = [];
        var oct_sum = 0;
        var nov_sum = 0;
        var dec_sum = 0;
        var jan_sum = 0;
        var feb_sum = 0;
        var mar_sum = 0;
        var apr_sum = 0;
        var may_sum = 0;
        var jun_sum = 0;
        var jul_sum = 0;
        var aug_sum = 0;
        var sep_sum = 0;

        var year = $('#assignment_year').val();

        if (jss_1 != undefined) {
            var data = jss_1.getData(false);

            for (let i = 0; i < data.length; i++) {
                oct_sum += parseFloat(data[i][2]);
                nov_sum += parseFloat(data[i][3]);
                dec_sum += parseFloat(data[i][4]);
                jan_sum += parseFloat(data[i][5]);
                feb_sum += parseFloat(data[i][6]);
                mar_sum += parseFloat(data[i][7]);
                apr_sum += parseFloat(data[i][8]);
                may_sum += parseFloat(data[i][9]);
                jun_sum += parseFloat(data[i][10]);
                jul_sum += parseFloat(data[i][11]);
                aug_sum += parseFloat(data[i][12]);
                sep_sum += parseFloat(data[i][13]);
            }
            if (oct_sum < 0 || oct_sum > 100) {
                alert('Invalid Department(Oct) Value ' + oct_sum);
                dataToSend = [];
                return false;
            }
            if (nov_sum < 0 || nov_sum> 100) {
                alert('Invalid Department(Nov) Value ' + nov_sum);
                dataToSend = [];
                return false;
            }
            if (dec_sum < 0 || dec_sum> 100) {
                alert('Invalid Department(Dec) Value ' + dec_sum);
                dataToSend = [];
                return false;
            }
            if (jan_sum < 0 || jan_sum> 100) {
                alert('Invalid Department(Jan) Value ' + jan_sum);
                dataToSend = [];
                return false;
            }
            if (feb_sum < 0 || feb_sum> 100) {
                alert('Invalid Department(Feb) Value ' + feb_sum);
                dataToSend = [];
                return false;
            }
            if (mar_sum < 0 || feb_sum> 100) {
                alert('Invalid Department(Mar) Value ' + mar_sum);
                dataToSend = [];
                return false;
            }
            if (apr_sum < 0 || apr_sum> 100) {
                alert('Invalid Department(Apr) Value ' + apr_sum);
                dataToSend = [];
                return false;
            }
            if (may_sum < 0 || may_sum> 100) {
                alert('Invalid Department(May) Value ' + may_sum);
                dataToSend = [];
                return false;
            }
            if (jun_sum < 0 || jun_sum> 100) {
                alert('Invalid Department(Jun) Value ' + jun_sum);
                dataToSend = [];
                return false;
            }
            if (jul_sum < 0 || jul_sum > 100) {
                alert('Invalid Department(Jul) Value ' + jul_sum);
                dataToSend = [];
                return false;
            }
            if (aug_sum < 0 || aug_sum> 100) {
                alert('Invalid Department(Aug) Value ' + aug_sum);
                dataToSend = [];
                return false;
            }
            if (sep_sum < 0 || sep_sum> 100) {
                alert('Invalid Department(Sep) Value ' + sep_sum);
                dataToSend = [];
                return false;
            }

            $.each(data, function (index, value) {
                var obj = {
                    departmentId: value[0],
                    octPercentage: parseFloat(value[2]),
                    novPercentage: parseFloat(value[3]),
                    decPercentage: parseFloat(value[4]),
                    janPercentage: parseFloat(value[5]),
                    febPercentage: parseFloat(value[6]),
                    marPercentage: parseFloat(value[7]),
                    aprPercentage: parseFloat(value[8]),
                    mayPercentage: parseFloat(value[9]),
                    junPercentage: parseFloat(value[10]),
                    julPercentage: parseFloat(value[11]),
                    augPercentage: parseFloat(value[12]),
                    sepPercentage: parseFloat(value[13]),
                    id: value[14]
                };

                dataToSend.push(obj);
            });

            $.ajax({
                url: `/api/utilities/CreateApportionment`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({
                    Apportionments: dataToSend,
                    Year: year,
                }),
                success: function (data) {
                    alert("保存されました.");
                }
            });
        }
        else {
            alert('No Data Found!');
        }
    });




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

    $('#actual_cost').on('click', function () {
        
        var year = $('#assignment_year').val();

        $.ajax({
            url: `/api/utilities/QaProportion?year=${year}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                $('#merged_employee_from_qc').empty();
                $('#merged_employee_from_qc').append(`<option value=''></option>`);
                $.each(data, function (index, element) {
                    $('#merged_employee_from_qc').append(`<option value='${element.EmployeeId}_${element.EmployeeName}'>${element.EmployeeName}</option>`);
                });
            }
        });

        $.ajax({
            url: `/api/utilities/GetFilteredDepartments`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                $('#department_list').empty();
                $('#department_list').append(`<option value=''></option>`);
                $.each(data, function (index, element) {
                    $('#department_list').append(`<option value='${element.Id}_${element.DepartmentName}'>${element.DepartmentName}</option>`);
                });
            }
        });

        _retriveddata = [];
        _retriveddata_1 = [];
        $.ajax({
            url: `/api/utilities/QaProportionDataByYear?year=${year}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                _retriveddata = data;
            }
        });

        $.ajax({
            url: `/api/utilities/CreateApportionment?year=${year}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                _retriveddata_1 = data;
            }
        });

        LoadJexcel();
        LoadJexcel1();
    });

    $('#add_button').on('click', function () {
        var datas = $('#merged_employee_from_qc').val();
        var year = $('#assignment_year').val();
        if (loadFlag==0) {
            _retriveddata = [];
            $.ajax({
                url: `/api/utilities/QaProportionDataByYear?year=${year}`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                success: function (data) {
                    _retriveddata = data;
                }
            });
            loadFlag = 1;
        }
        
        
        $.each(datas, function (index, itemValue) {
            var splittedString = itemValue.split('_');
            _retriveddata.push({
                EmployeeId: splittedString[0],
                EmployeeName: splittedString[1],
                DepartmentId: null,
                OctPercentage: 0,
                NovPercentage: 0,
                DecPercentage: 0,
                JanPercentage: 0,
                FebPercentage: 0,
                MarPercentage: 0,
                AprPercentage: 0,
                MayPercentage: 0,
                JunPercentage: 0,
                JulPercentage: 0,
                AugPercentage: 0,
                SepPercentage: 0,
                Id: 0
               

            });
        });
        LoadJexcel();
    });
    
    $('#department_list_add_button').on('click', function () {
        var duplicateDepartments = [];
        var departmentList = $('#department_list').val();
        var year = $('#assignment_year').val();
        if (loadFlag1==0) {
            _retriveddata_1 = [];
            $.ajax({
                url: `/api/utilities/CreateApportionment?year=${year}`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                success: function (data) {
                    _retriveddata_1 = data;
                }
            });
            loadFlag1 = 1;
        }
        console.log(_retriveddata_1);
        $.each(departmentList, function (index, itemValue) {
            let pushFlag = true;
            var splittedString = itemValue.split('_');

            for (let i = 0; i < _retriveddata_1.length; i++) {
                if (_retriveddata_1[i].DepartmentId.toString() == splittedString[0].toString()) {
                    pushFlag = false;
                    duplicateDepartments.push(splittedString[1]);
                    break;
                }
            }

            if (pushFlag) {
                _retriveddata_1.push({
                    DepartmentId: splittedString[0],
                    DepartmentName: splittedString[1],
                    OctPercentage: 0,
                    NovPercentage: 0,
                    DecPercentage: 0,
                    JanPercentage: 0,
                    FebPercentage: 0,
                    MarPercentage: 0,
                    AprPercentage: 0,
                    MayPercentage: 0,
                    JunPercentage: 0,
                    JulPercentage: 0,
                    AugPercentage: 0,
                    SepPercentage: 0,
                    Id: 0


                });
            }

        });
        if (duplicateDepartments.length > 0) {
            var duplicateMessege = '';
            $.each(duplicateDepartments, function (duplicateIndex, duplicateValue) {
                duplicateMessege += duplicateValue+'\n'; 
            });
            duplicateMessege += 'department\'s are already exists!';
            alert(duplicateMessege);
        }
        LoadJexcel1();
    });

    $('#merged_employee_from_qc').select2({ placeholder: "要員の選択", });
    $('#department_list').select2({ placeholder: "部署を選択 (Select Department)", });

    $("#hider").hide();
    $(".search_p").hide();

    $("#buttonClose").click(function () {

        $("#hider").fadeOut("slow");
        $('.search_p').fadeOut("slow");
        // $('#search_p_text_box').val('');
    });


});
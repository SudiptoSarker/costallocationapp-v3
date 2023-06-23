var jss;
var jss_1;
var _retriveddata;
var _retriveddata_1;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];
var beforeChangedValue = 0;


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

function LoadJexcel() {
    var year = $('#assignment_year').val();

    if (year == null || year == '' || year == undefined) {
        alert('Select Year!!!');
        return false;
    }
    LoaderShow();
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
        LoaderHide();

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

                { title: "Employee Name", type: 'text', name: "EmployeeName", width: 100 },

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
    LoaderShow();
    //$.ajax({
    //    url: `/api/utilities/CreateApportionment?year=${year}`,
    //    contentType: 'application/json',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    success: function (data1) 
    {
            LoaderHide();
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
                    { title: "Department Name", type: 'text', name: "DepartmentName" },

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

        let flag = true;
        if (jss != undefined) {
            $.each(jss.getData(), (index, itemValue) => {
                if (itemValue[2] == null || itemValue[2] == '' || itemValue[2] == undefined) {
                    alert('invalid department!');
                    flag = false;
                    return false;
                }
            });
            if (flag) {
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
           
           
        }
        else {
            alert('No table found!');
            return false;
        }

    });



    $('#department_wise_save_button').click(function () {
        var dataToSend = [];
        var year = $('#assignment_year').val();

        if (jss_1 != undefined) {
            var data = jss_1.getData(false);
            $.each(data, function (index, value) {
                var obj = {
                    departmentId: value[0],
                    octPercentage: parseFloat(value[2]),
                    novPercentage: parseFloat(value[3]),
                    decPercentage: parseFloat(value[4]),
                    janPercentage: parseFloat(value[5]),
                    bebPercentage: parseFloat(value[6]),
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
                    alert("Operation Success.");
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
                console.log(data);
            }
        });

        LoadJexcel();
        LoadJexcel1();
    });

    $('#add_button').on('click', function () {
        var datas = $('#merged_employee_from_qc').val();
        var year = $('#assignment_year').val();
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
        var departmentList = $('#department_list').val();
        var year = $('#assignment_year').val();
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
        $.each(departmentList, function (index, itemValue) {
            var splittedString = itemValue.split('_');
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
        });
        LoadJexcel1();
    });

    $('#merged_employee_from_qc').select2({ placeholder: "Select Employee", });
    $('#department_list').select2({ placeholder: "Select Department", });

    $("#hider").hide();
    $(".search_p").hide();

    $("#buttonClose").click(function () {

        $("#hider").fadeOut("slow");
        $('.search_p').fadeOut("slow");
        // $('#search_p_text_box').val('');
    });


});
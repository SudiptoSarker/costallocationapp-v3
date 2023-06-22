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
                        obj.setValueFromCoords(2, nextRow, retrivedData.departmentId, false);
                        obj.setValueFromCoords(3, nextRow, retrivedData.octPoint, false);
                        obj.setValueFromCoords(4, nextRow, retrivedData.novPoint, false);
                        obj.setValueFromCoords(5, nextRow, retrivedData.decPoint, false);
                        obj.setValueFromCoords(6, nextRow, retrivedData.janPoint, false);
                        obj.setValueFromCoords(7, nextRow, retrivedData.febPoint, false);
                        obj.setValueFromCoords(8, nextRow, retrivedData.marPoint, false);
                        obj.setValueFromCoords(9, nextRow, retrivedData.aprPoint, false);
                        obj.setValueFromCoords(10, nextRow, retrivedData.mayPoint, false);
                        obj.setValueFromCoords(11, nextRow, retrivedData.junPoint, false);
                        obj.setValueFromCoords(12, nextRow, retrivedData.julPoint, false);
                        obj.setValueFromCoords(13, nextRow, retrivedData.augPoint, false);
                        obj.setValueFromCoords(14, nextRow, retrivedData.sepPoint, false);
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
                if (x == 5) {
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
                if (x == 6) {
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
                if (x == 7) {
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
                if (x == 8) {
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
                if (x == 9) {
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
                if (x == 10) {
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
                if (x == 11) {
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
                if (x == 12) {
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
                if (x == 13) {
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
                if (x == 14) {
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
            }
        });
        //jss.deleteColumn(21, 4);

    } 


        $.ajax({
            url: `/api/utilities/QaAssignmentTotal?year=${year}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (assignmentData) {
                $.each(assignmentData, (index, itemValue) => {
                    $('#qa_assignments').append(`<tr><td>${itemValue.EmployeeName}</td><td>${itemValue.OctTotal}</td><td>${itemValue.NovTotal}</td><td>${itemValue.DecTotal}</td><td>${itemValue.JanTotal}</td><td>${itemValue.FebTotal}</td><td>${itemValue.MarTotal}</td><td>${itemValue.AprTotal}</td><td>${itemValue.MayTotal}</td><td>${itemValue.JunTotal}</td><td>${itemValue.JulTotal}</td><td>${itemValue.AugTotal}</td><td>${itemValue.SepTotal}</td></tr>`);
                });
            }
        });



    }, 3000);




}

function LoadJexcel1() {
    // another jexcel table
    var year = $('#assignment_year').val();

    if (year == null || year == '' || year == undefined) {
        alert('Select Year!!!');
        return false;
    }

    $.ajax({
        url: `/api/utilities/CreateApportionment?year=${year}`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data1) {
            LoaderHide();
            _retriveddata_1 = data1;

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
    });
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

        if (jss != undefined) {
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
                    sepPercentage: parseFloat(value[13])
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

    $('#merged_employee_from_qc').select2({ placeholder: "Select Employee",});

    $("#hider").hide();
    $(".search_p").hide();

    $("#buttonClose").click(function () {

        $("#hider").fadeOut("slow");
        $('.search_p').fadeOut("slow");
        // $('#search_p_text_box').val('');
    });


    //$('#create_actual_cost').on('click', function () {
    //    var dataToSend = [];
    //    var year = $('#assignment_year').val();

    //    var oct_flag = $('#oct_chk').is(":checked");
    //    var nov_flag = $('#nov_chk').is(":checked");
    //    var dec_flag = $('#dec_chk').is(":checked");
    //    var jan_flag = $('#jan_chk').is(":checked");
    //    var feb_flag = $('#feb_chk').is(":checked");
    //    var mar_flag = $('#mar_chk').is(":checked");
    //    var apr_flag = $('#apr_chk').is(":checked");
    //    var may_flag = $('#may_chk').is(":checked");
    //    var jun_flag = $('#jun_chk').is(":checked");
    //    var jul_flag = $('#jul_chk').is(":checked");
    //    var aug_flag = $('#aug_chk').is(":checked");
    //    var sep_flag = $('#sep_chk').is(":checked");

    //    if (jss != undefined) {
    //        var data = jss.getData(false);
    //        $.each(data, function (index, value) {
    //            var obj = {
    //                assignmentId: value[0],
    //                octCost: parseFloat(value[9]),
    //                novCost: parseFloat(value[10]),
    //                decCost: parseFloat(value[11]),
    //                janCost: parseFloat(value[12]),
    //                febCost: parseFloat(value[13]),
    //                marCost: parseFloat(value[14]),
    //                aprCost: parseFloat(value[15]),
    //                mayCost: parseFloat(value[16]),
    //                junCost: parseFloat(value[17]),
    //                julCost: parseFloat(value[18]),
    //                augCost: parseFloat(value[19]),
    //                sepCost: parseFloat(value[20])
    //            };

    //            dataToSend.push(obj);
    //        });

    //        $.ajax({
    //            url: `/api/utilities/CreateActualCost`,
    //            contentType: 'application/json',
    //            type: 'POST',
    //            async: false,
    //            dataType: 'json',
    //            data: JSON.stringify({
    //                ActualCosts: dataToSend,
    //                Year: year,
    //                OctFlag: oct_flag,
    //                NovFlag: nov_flag,
    //                DecFlag: dec_flag,
    //                JanFlag: jan_flag,
    //                FebFlag: feb_flag,
    //                MarFlag: mar_flag,
    //                AprFlag: apr_flag,
    //                MayFlag: may_flag,
    //                JunFlag: jun_flag,
    //                JulFlag: jul_flag,
    //                AugFlag: aug_flag,
    //                SepFlag: sep_flag,
    //            }),
    //            success: function (data) {
    //                alert("Operation Success.");
    //            }
    //        });
    //    }
    //    else {
    //        alert('No Data Found!');
    //    }
    //});
});
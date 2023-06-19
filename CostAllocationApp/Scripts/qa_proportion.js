var jss;
var jss_1;
var _retriveddata;
var _retriveddata_1;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];


const channel = new BroadcastChannel("actualCost");



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

        $.ajax({
            url: `/api/utilities/QaProportion?year=${year}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                LoaderHide();
                _retriveddata = data;

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
              
  
                var w = window.innerWidth;
                var h = window.innerHeight;
                jss = $('#jspreadsheet').jspreadsheet({
                    data: _retriveddata,
                    filters: true,
                    tableOverflow: true,
                    freezeColumns: 3,
                    defaultColWidth: 50,
                     //tableWidth: w - 500 + "px",
                     //tableHeight: (h - 300) + "px",
                    tableWidth: w - 300 + "px",
                    tableHeight: (h - 300) + "px",

                    columns: [
                        
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

                    ],
                    columnSorting: true,
                    contextMenu: function (obj, x, y, e) {

                    }
                });
                jss.deleteColumn(21, 4);

              

            }
        });

        // another jexcel table

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
    //channel.addEventListener("message", e => {
    //    LoadJexcel();
    //});
    
    // LoaderHide();
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
        LoadJexcel();
    });



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
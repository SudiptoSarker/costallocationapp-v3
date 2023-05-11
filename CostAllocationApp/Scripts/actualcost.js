var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];

function LoaderShow() {
    $("#actual_cost_table_header").hide();     
    $("#jspreadsheet").hide();     
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#actual_cost_table_header").show(); 
    $("#jspreadsheet").show(); 
    $("#loading").css("display", "none");
}
function ColumnOrder(columnNumber,orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy==0) {
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
function GetEmployeeName() {
    var data = [];
    $("#search_p_search input:checkbox[name=employeename]:checked").each(function () {
        data.push($(this).val());
    });

    jss.search(data[0]);
    $("#hider").fadeOut("slow");
    $('.search_p').fadeOut("slow");
    $('#search_p_text_box').val('');
}
// $("#actual_cost_table_header").hide();
$(document).ready(function () {
    $("#actual_cost_table_header").hide();     
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
                    console.log(data)
                    if (data === 1 || data === 2) {
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
                async: false,
                dataType: 'json',
                success: function (data) {
                    //console.log(data);
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
                        //filters: true,
                        tableOverflow: true,
                        //tableWidth: window.innerWidth - 300 + 'px',
                        freezeColumns: 3,
                        
                        defaultColWidth: 50,
                        // tableWidth: w - 500 + "px",
                        // tableHeight: (h - 300) + "px",
                        tableWidth: w-300+ "px",
                        tableHeight: (h-300) + "px",

                        columns: [
                            { title: "Assignment Id", type: 'hidden', name: "AssignmentId" },
                            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
                            { title: "Remarks", type: "text", name: "Remarks", width: 60 },
                            { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },
                            { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },
                            { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100 },
                            { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60 },
                            { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
                            { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100 },
                            //{ type: 'number', title:'Price', mask:'$ #.##0,00', decimal:',' }
                            {
                                title: "10月",
                                type: "decimal",
                                name: "OctCost",
                                mask: "#,##0",
                                //decimal:'.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "11月",
                                type: "decimal",
                                name: "NovCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "12月",
                                type: "decimal",
                                name: "DecCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "1月",
                                type: "decimal",
                                name: "JanCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "2月",
                                type: "decimal",
                                name: "FebCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "3月",
                                type: "decimal",
                                name: "MarCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "4月",
                                type: "decimal",
                                name: "AprCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "5月",
                                type: "decimal",
                                name: "MayCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "6月",
                                type: "decimal",
                                name: "JunCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "7月",
                                type: "decimal",
                                name: "JulCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "8月",
                                type: "decimal",
                                name: "AugCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                            {
                                title: "9月",
                                type: "decimal",
                                name: "SepCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            },
                        ],
                        columnSorting: true,
                        contextMenu: function (obj, x, y, e) {

                        }
                    });
                    jss.deleteColumn(21, 4);
                    //jss.hideIndex();
                    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
                    jexcelHeadTdEmployeeName.addClass('arrow-down');
                    //var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
                    //jexcelFirstHeaderRow.css('position', 'sticky');
                    //jexcelFirstHeaderRow.css('top', '0px');
                    //var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
                    //jexcelSecondHeaderRow.css('position', 'sticky');
                    //jexcelSecondHeaderRow.css('top', '20px');

                    var octElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
                    octElement.append('<input type="checkbox" id="oct_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var novElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
                    novElement.append('<input type="checkbox" id="nov_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var decElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(13)');
                    decElement.append('<input type="checkbox" id="dec_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var janElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(14)');
                    janElement.append('<input type="checkbox" id="jan_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var febElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(15)');
                    febElement.append('<input type="checkbox" id="feb_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var marElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(16)');
                    marElement.append('<input type="checkbox" id="mar_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var aprElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(17)');
                    aprElement.append('<input type="checkbox" id="apr_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var mayElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(18)');
                    mayElement.append('<input type="checkbox" id="may_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var junElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(19)');
                    junElement.append('<input type="checkbox" id="jun_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var julElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(20)');
                    julElement.append('<input type="checkbox" id="jul_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var augElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(21)');
                    augElement.append('<input type="checkbox" id="aug_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    var sepElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(22)');
                    sepElement.append('<input type="checkbox" id="sep_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {
                        $('.search_p').css('display','block');
                        allEmployeeName = [];
                        var data = jss.getData();
                        for (var i = 0; i < jss.getData().length; i++) {
                            allEmployeeName.push(data[i][1]);
                        }
                       
                        var allEmployeeName = allEmployeeName.filter(function (value, index, array) {
                            return array.indexOf(value) === index;
                        });
                        allEmployeeName.sort();
                        $('#search_p_search').empty();
                        allEmployeeName1 = [];
                        $.each(allEmployeeName, function (index, value) {
                            $('#search_p_search').append(`<li><input type='checkbox' name='employeename' value='${value}'> ${value}</li>`);
                            allEmployeeName1.push(value);
                        });
                        //console.log(allEmployeeName);

                        $("#hider").fadeIn("slow");
                        $('.search_p').fadeIn("slow");
                        //$('#filter_modal').modal('show');
                    });
                }
            });
        }, 3000);

        
    });

    $('#search_p_text_box').on('keyup',function () {
        var name = $(this).val();
        console.log(allEmployeeName1);
        if (allEmployeeName1.length > 0) {
            var data = allEmployeeName1.filter(employeeName => employeeName.toLowerCase().includes(name.toLowerCase()));

            data.sort();

            $('#search_p_search').empty();
            $.each(data, function (index, value) {
                $('#search_p_search').append(`<li><input type='checkbox' name='employeename' value='${value}'> ${value}</li>`);
            });
        }
    });

    $("#hider").hide();
    $(".search_p").hide();

    $("#buttonClose").click(function () {

        $("#hider").fadeOut("slow");
        $('.search_p').fadeOut("slow");
        $('#search_p_text_box').val('');
    });


    $('#create_actual_cost').on('click', function () {
        var dataToSend = [];
        var year = $('#assignment_year').val();

        var oct_flag = $('#oct_chk').is(":checked");
        var nov_flag = $('#nov_chk').is(":checked");
        var dec_flag = $('#dec_chk').is(":checked");
        var jan_flag = $('#jan_chk').is(":checked");
        var feb_flag = $('#feb_chk').is(":checked");
        var mar_flag = $('#mar_chk').is(":checked");
        var apr_flag = $('#apr_chk').is(":checked");
        var may_flag = $('#may_chk').is(":checked");
        var jun_flag = $('#jun_chk').is(":checked");
        var jul_flag = $('#jul_chk').is(":checked");
        var aug_flag = $('#aug_chk').is(":checked");
        var sep_flag = $('#sep_chk').is(":checked");

        if (jss != undefined) {
            var data = jss.getData(false);
            $.each(data, function (index,value) {
                var obj = {
                    assignmentId: value[0],
                    octCost: parseFloat(value[9]),
                    novCost: parseFloat(value[10]),
                    decCost: parseFloat(value[11]),
                    janCost: parseFloat(value[12]),
                    febCost: parseFloat(value[13]),
                    marCost: parseFloat(value[14]),
                    aprCost: parseFloat(value[15]),
                    mayCost: parseFloat(value[16]),
                    junCost: parseFloat(value[17]),
                    julCost: parseFloat(value[18]),
                    augCost: parseFloat(value[19]),
                    sepCost: parseFloat(value[20])
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
                    Year: year,
                    OctFlag: oct_flag,
                    NovFlag: nov_flag,
                    DecFlag: dec_flag,
                    JanFlag: jan_flag,
                    FebFlag: feb_flag,
                    MarFlag: mar_flag,
                    AprFlag: apr_flag,
                    MayFlag: may_flag,
                    JunFlag: jun_flag,
                    JulFlag: jul_flag,
                    AugFlag: aug_flag,
                    SepFlag: sep_flag,
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
});
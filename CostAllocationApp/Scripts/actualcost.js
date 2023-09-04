var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];


const channel = new BroadcastChannel("actualCost");




function LoadJexcel() {
    var year = $('#assignment_year').val();

    if (year == null || year == '' || year == undefined) {
        alert('年度を選択してください!!!');
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
                    filters: true,
                    tableOverflow: true,
                    freezeColumns: 3,
                    defaultColWidth: 50,
                    // tableWidth: w - 500 + "px",
                    // tableHeight: (h - 300) + "px",
                    tableWidth: w-280+ "px",
                    tableHeight: (h-150) + "px",

                    columns: [
                        { title: "Assignment Id", type: 'hidden', name: "AssignmentId" },
                        { 
                            title: "要員(Employee)", 
                            type: "text", 
                            name: "EmployeeName", 
                            width: 150,
                            readOnly: true
                        },
                        { 
                            title: "Remarks", 
                            type: "text", 
                            name: "Remarks", 
                            width: 60,
                            readOnly: true
                        },
                        { 
                            title: "区分(Section)", 
                            type: "dropdown", 
                            source: sectionsForJexcel, 
                            name: "SectionId", 
                            width: 100,
                            readOnly: true 
                        },
                        { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100,readOnly: true },
                        { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100,readOnly: true },
                        { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60,readOnly: true },
                        { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150,readOnly: true },
                        { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100,readOnly: true },
                        //{ type: 'number', title:'Price', mask:'$ #.##0,00', decimal:',' }
                        {
                            title: "10月 実績",
                            type: "decimal",
                            name: "OctCost",
                            mask: "#,##0",
                            //decimal:'.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "11月 実績",
                            type: "decimal",
                            name: "NovCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "12月 実績",
                            type: "decimal",
                            name: "DecCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "1月 実績",
                            type: "decimal",
                            name: "JanCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "2月 実績",
                            type: "decimal",
                            name: "FebCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "3月 実績",
                            type: "decimal",
                            name: "MarCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "4月 実績",
                            type: "decimal",
                            name: "AprCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "5月 実績",
                            type: "decimal",
                            name: "MayCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "6月 実績",
                            type: "decimal",
                            name: "JunCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "7月 実績",
                            type: "decimal",
                            name: "JulCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "8月 実績",
                            type: "decimal",
                            name: "AugCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
                        },
                        {
                            title: "9月 実績",
                            type: "decimal",
                            name: "SepCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                            readOnly: true
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
                var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
                jexcelFirstHeaderRow.css('position', 'sticky');
                jexcelFirstHeaderRow.css('top', '0px');
                var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
                jexcelSecondHeaderRow.css('position', 'sticky');
                jexcelSecondHeaderRow.css('top', '20px');

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
$(document).ready(function () {
    channel.addEventListener("message", e => {
        LoadJexcel();
    });

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
                    alert("保存されました.");
                }
            });
        }
        else {
            alert('追加、修正していないデータがありません!');
        }
    });
});
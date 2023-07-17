var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];

const channel = new BroadcastChannel("actualCost");

function LoaderShow() {
    $("#jspreadsheet").hide();
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#jspreadsheet").show();
    $("#loading").css("display", "none");
}



$(document).ready(function () {
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

    // loading jexcel
    {
        var queryStrings = getUrlVars();
        //var year = $('#assignment_year').val();
        var year = queryStrings['year'];

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
                        tableWidth: w-280+ "px",
                        tableHeight: (h-150) + "px",

                        
                        columns: [
                            { title: "Assignment Id", type: 'hidden', name: "Id" },
                            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150,readOnly: true },
                            { title: "Remarks", type: "text", name: "Remarks", width: 60,readOnly: true },
                            { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100,readOnly: true },
                            { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100,readOnly: true },
                            { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100,readOnly: true },
                            { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60,readOnly: true },
                            { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150,readOnly: true },
                            { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100,readOnly: true },
                            //{ type: 'number', title:'Price', mask:'$ #.##0,00', decimal:',' }
                            {
                                title: `${queryStrings['month']}月単価(uc)`,
                                type: "decimal",
                                name: "UnitPrice",
                                mask: "#,##0",
                                //decimal:'.',
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月工数(mm)`,
                                type: "decimal",
                                name: "ForecastedPoints",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月予定額(forcasted)`,
                                type: "decimal",
                                name: "ForecastedTotal",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月実工数(amm)`,
                                type: "decimal",
                                name: "ManMonth",
                                //mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                //readOnly: true
                            },
                             {
                                 title: `${queryStrings['month']}月実績(ac)`,
                                type: "decimal",
                                 name: "ActualCostAmount",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                //readOnly: userRoleflag
                            }
                        ],
                        minDimensions: [6, 10],
                        columnSorting: true,
                        contextMenu: function (obj, x, y, e) {

                        }
                    });
                    jss.deleteColumn(14, 20);
                    //jss.hideIndex();
                    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
                    jexcelHeadTdEmployeeName.addClass('arrow-down');
                    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
                    jexcelFirstHeaderRow.css('position', 'sticky');
                    jexcelFirstHeaderRow.css('top', '0px');
                    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
                    jexcelSecondHeaderRow.css('position', 'sticky');
                    jexcelSecondHeaderRow.css('top', '20px');

                    //var octElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
                    //octElement.append('<input type="checkbox" id="oct_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    //var ActualCostAmountElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(15)');
                    //ActualCostAmountElement.append('<input type="checkbox" id="ActualCostAmount_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {
                        $('.search_p').css('display', 'block');
                        $("#hider").fadeIn("slow");
                        $('.search_p').fadeIn("slow");
                    });

                    
                }
            });
        }, 3000);


    }


    $('#create_actual_cost').on('click', function () {
        var queryStrings = getUrlVars();
        var dataToSend = [];
        //var year = $('#assignment_year').val();

        if (jss != undefined) {
            var data = jss.getData(false);
            $.each(data, function (index, value) {
                var obj = {
                    assignmentId: value[0],
                    manHour: parseFloat(value[12]),
                    actualCostAmount :  parseFloat(value[13])
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
                    alert("Operation Success.");
                    channel.postMessage('done');
                    window.close();
                }
            });
        }
        else {
            alert('No Data Found!');
        }
    });

    $('#cancel_actual_cost').on('click', function () {
        channel.postMessage('done');
        window.close();
    });


});

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
var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];


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
        var year = 2023;

        if (year == null || year == '' || year == undefined) {
            alert('Select Year!!!');
            return false;
        }
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
                    //LoaderHide();
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
                        tableWidth: w - 300 + "px",
                        tableHeight: (h - 300) + "px",

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
                                title: `${queryStrings['month']}月 Unit Price`,
                                type: "decimal",
                                name: "OctCost",
                                mask: "#,##0",
                                //decimal:'.',
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月 Man Month (forecast)`,
                                type: "decimal",
                                name: "NovCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月 Cost (forecast)`,
                                type: "decimal",
                                name: "DecCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月 Man Month`,
                                type: "decimal",
                                name: "JanCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                //readOnly: true
                            },
                             {
                                title: `${queryStrings['month']}月 Actual Cost`,
                                type: "decimal",
                                name: "FebCost",
                                mask: "#,##0",
                                //decimal: '.',
                                width: 100,
                                readOnly: userRoleflag
                            }
                        ],
                        columnSorting: true,
                        contextMenu: function (obj, x, y, e) {

                        }
                    });
                    jss.deleteColumn(13, 20);
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

                    //var novElement = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
                    //novElement.append('<input type="checkbox" id="nov_chk"  style="display:inline-block;margin-left: 10px;"/>');

                    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {
                        $('.search_p').css('display', 'block');
                        $("#hider").fadeIn("slow");
                        $('.search_p').fadeIn("slow");
                    });

                    
                }
            });
        }, 3000);


    }

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
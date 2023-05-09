﻿var jss;
var _retriveddata;
var userRoleflag;

function LoaderShow() {
    $("#jspreadsheet").hide(); 
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#jspreadsheet").show(); 
    $("#loading").css("display", "none");
}
$(document).ready(function () {
    LoaderHide();
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#assignment_year').empty();
            $('#assignment_year').append(`<option value=''>select year</option>`);
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


                    jss = $('#jspreadsheet').jspreadsheet({
                        data: _retriveddata,
                        filters: true,
                        tableOverflow: true,
                        tableWidth: window.innerWidth - 300 + 'px',
                        freezeColumns: 3,
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
                    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
                    jexcelFirstHeaderRow.css('position', 'sticky');
                    jexcelFirstHeaderRow.css('top', '0px');
                    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
                    jexcelSecondHeaderRow.css('position', 'sticky');
                    jexcelSecondHeaderRow.css('top', '20px');

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

                }
            });
        }, 3000);

        
    });

    //jss.doubleClickControls = function (e) {
    //    // Jexcel is selected
    //    if (jexcel.current) {
    //        // Corner action
    //        if (e.target.classList.contains('jexcel_corner')) {
    //            // Any selected cells
    //            if (jexcel.current.highlighted.length > 0) {
    //                // Copy from this
    //                var x1 = jexcel.current.highlighted[0].getAttribute('data-x');
    //                var y1 = parseInt(jexcel.current.highlighted[jexcel.current.highlighted.length - 1].getAttribute('data-y')) + 1;
    //                // Until this
    //                var x2 = jexcel.current.highlighted[jexcel.current.highlighted.length - 1].getAttribute('data-x');
    //                var y2 = jexcel.current.records.length - 1
    //                // Execute copy
    //                jexcel.current.copyData(jexcel.current.records[y1][x1], jexcel.current.records[y2][x2]);
    //            }
    //        } else if (e.target.classList.contains('jexcel_column_filter')) {
    //            // Column
    //            var columnId = e.target.getAttribute('data-x');
    //            // Open filter
    //            jexcel.current.openFilter(columnId);

    //        } else {
    //            // Get table
    //            var jexcelTable = jexcel.getElement(e.target);

    //            // Double click over header
    //            if (jexcelTable[1] == 1 && jexcel.current.options.columnSorting == true) {
    //                // Check valid column header coords
    //                //var columnId = e.target.getAttribute('data-x');
    //                //if (columnId) {
    //                //    jexcel.current.orderBy(columnId);
    //                //}
    //                alert('clicked');
    //            }

    //            // Double click over body
    //            if (jexcelTable[1] == 2 && jexcel.current.options.editable == true) {
    //                if (!jexcel.current.edition) {
    //                    var getCellCoords = function (element) {
    //                        if (element.parentNode) {
    //                            var x = element.getAttribute('data-x');
    //                            var y = element.getAttribute('data-y');
    //                            if (x && y) {
    //                                return element;
    //                            } else {
    //                                return getCellCoords(element.parentNode);
    //                            }
    //                        }
    //                    }
    //                    var cell = getCellCoords(e.target);
    //                    if (cell && cell.classList.contains('highlight')) {
    //                        jexcel.current.openEditor(cell);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    $('#create_actual_cost').on('click', function () {
        var dataToSend = [];
        var year = $('#assignment_year').val();

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

            
            $.ajax({
                url: `/api/utilities/CreateActualCost`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ActualCosts: dataToSend, Year: year }),
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
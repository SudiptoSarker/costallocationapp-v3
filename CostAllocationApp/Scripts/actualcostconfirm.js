var jss;
var _retriveddata;
var userRoleflag;
var allEmployeeName = [];
var allEmployeeName1 = [];
var distributeFlag = false;

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

    $('#distribute').on('click', function () {
        var _newEmployeeGroupList = [];
        var _uniqueEmnployeeIdList = [];
        var _actualCostCount = 0;
        var _actualCostAmount = 0;
        var allData = jss.getData();
        var _allRows = $(`.jexcel > tbody > tr`);

        for (var i = 0; i < allData.length; i++) {
            if (_uniqueEmnployeeIdList.length > 0) {
                if (!_uniqueEmnployeeIdList.includes(allData[i][19])) {
                    _uniqueEmnployeeIdList.push(allData[i][19]);
                }
            }
            else {
                _uniqueEmnployeeIdList.push(allData[i][19]);
            }
        }

        if (_uniqueEmnployeeIdList.length > 0) {
            for (var j = 0; j < _uniqueEmnployeeIdList.length; j++) {
                _actualCostCount = 0;
                _actualCostAmount =0;
                for (var k = 0; k < allData.length; k++) {
                    if (allData[k][19] == _uniqueEmnployeeIdList[j]) {

                        if (allData[k][18] > 0) {
                            _actualCostAmount = parseFloat(allData[k][18]);
                            _actualCostCount++;
                        }
                        if (_actualCostCount > 1) {
                            alert('Duplicate actual cost found!');
                            distributeFlag = false;
                            return;
                        }
                        _newEmployeeGroupList.push({
                            assignmentId: allData[k][0],
                            manMonth: allData[k][15],
                            actualCost: allData[k][18]
                        });


                    }
                }
                //debugger;
                //console.log(jss.options.rows);
                //console.log(_allRows);
                for (var l = 0; l < _newEmployeeGroupList.length; l++) {

                    var mm = parseFloat(_newEmployeeGroupList[l].manMonth);
                    var ac = _actualCostAmount;
                    var newCost = mm * ac;

                    for (var m = 0; m < _allRows.length; m++) {
                        if (parseInt(_newEmployeeGroupList[l].assignmentId) ==  parseInt(_allRows[m].cells[1].innerText)) {
                            jss.setValueFromCoords(18, parseInt(_allRows[m].cells[0].dataset.y), newCost, false);
                        }
                    }

                }

                _newEmployeeGroupList = [];
            }
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

    // loading jexcel
    {
        var queryStrings = getUrlVars();
        //var year = $('#assignment_year').val();
        var year = queryStrings['year'];

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
                            { title: "Db Id", type: 'text', name: "Id", width: 100, readOnly: true },
                            { title: "Duplicate From", type: 'text', name: "DuplicateFrom", width: 100, readOnly: true  },
                            { title: "Duplicate Count", type: 'text', name: "DuplicateCount",width: 100,readOnly: true  },
                            { title: "Role Changed", type: 'text', name: "RoleChanged",width: 100,readOnly: true  },
                            { title: "Unit Price Changed", type: 'text', name: "UnitPriceChanged",width: 100,readOnly: true  },
                            {
                                title: `${queryStrings['month']}月単価(uc)`,
                                type: "decimal",
                                name: "UnitPrice",
                                mask: "#,##0",
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月工数(mm)`,
                                type: "decimal",
                                name: "ForecastedPoints",
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月予定額(forcasted)`,
                                type: "decimal",
                                name: "ForecastedTotal",
                                mask: "#,##0",
                                width: 100,
                                readOnly: true
                            },
                            {
                                title: `${queryStrings['month']}月実工数(amm)`,
                                type: "decimal",
                                name: "ManMonth",
                                width: 100,
                            },
                             {
                                title: `${queryStrings['month']}月実績(ac)`,
                                type: "decimal",
                                name: "ActualCostAmount",
                                mask: "#,##0",
                                width: 100,
                            },
                            { title: "Employee Id", type: 'hidden', name: "EmployeeId" },
                        ],
                        minDimensions: [6, 10],
                        columnSorting: true,
                        contextMenu: function (obj, x, y, e) {

                        }
                    });
                    jss.deleteColumn(20, 26);
                    //jss.hideIndex();
                    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
                    jexcelHeadTdEmployeeName.addClass('arrow-down');
                    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
                    jexcelFirstHeaderRow.css('position', 'sticky');
                    jexcelFirstHeaderRow.css('top', '0px');
                    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
                    jexcelSecondHeaderRow.css('position', 'sticky');
                    jexcelSecondHeaderRow.css('top', '20px');


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

        if (distributeFlag==false) {
            alert("Please distribute first!");
            return false;
        }

        var queryStrings = getUrlVars();
        var dataToSend = [];
        //var year = $('#assignment_year').val();

        if (jss != undefined) {
            var data = jss.getData(false);
            $.each(data, function (index, value) {
                var obj = {
                    assignmentId: value[0],
                    manHour: parseFloat(value[17]),
                    actualCostAmount :  parseFloat(value[18])
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
                    alert("保存されました.");
                    channel.postMessage('done');
                    window.close();
                }
            });
        }
        else {
            alert('追加、修正していないデータがありません!');
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
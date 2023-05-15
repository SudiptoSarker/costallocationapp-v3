var jss;
var _retriveddata;

function LoaderShow() {
    //$("#actual_cost_table_header").hide();
    $("#jspreadsheet").hide();
    $("#loading").css("display", "block");
}
function LoaderHide() {
    //$("#actual_cost_table_header").show();
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

    $('#apportionment_button').click(function () {
        var year = $('#assignment_year').val();

        if (year == null || year == '' || year == undefined) {
            alert('Select Year!!!');
            return false;
        }

        LoaderShow();

        setTimeout(function () {
            $.ajax({
                url: `/api/utilities/CreateApportionment?year=${year}`,
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

                    jss = $('#jspreadsheet').jspreadsheet({
                        data: _retriveddata,
                        //filters: true,
                        //tableOverflow: true,
                        //freezeColumns: 3,
                        defaultColWidth: 100,
                        tableWidth: (window.innerWidth - 300) + "px",
                        tableHeight: (window.innerHeight - 300) + "px",
                        columns: [
                            { title: "Department Id", type: 'hidden', name: "DepartmentId" },
                            { title: "Department Name", type: 'text', name: "DepartmentName" },

                            { title: "10月 (actual cost)", type: "decimal", name: "OctActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "10月 (QA ratio)", type: "decimal", name: "OctPercentage", mask: "#.## %", width: 100 },
                            { title: "10月 (total)", type: "decimal", name: "OctResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "11月 (actual cost)", type: "decimal", name: "NovActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "11月 (QA ratio)", type: "decimal", name: "NovPercentage", mask: "#.## %", width: 100 },
                            { title: "11月 (total)", type: "decimal", name: "NovResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "12月 (actual cost)", type: "decimal", name: "DecActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "12月 (QA ratio)", type: "decimal", name: "DecPercentage", mask: "#.## %", width: 100 },
                            { title: "12月 (total)", type: "decimal", name: "DecResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "1月 (actual cost)", type: "decimal", name: "JanActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "1月 (QA ratio)", type: "decimal", name: "JanPercentage", mask: "#.## %", width: 100 },
                            { title: "1月 (total)", type: "decimal", name: "JanResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "2月 (actual cost)", type: "decimal", name: "FebActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "2月 (QA ratio)", type: "decimal", name: "FebPercentage", mask: "#.## %", width: 100 },
                            { title: "2月 (total)", type: "decimal", name: "FebResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "3月 (actual cost)", type: "decimal", name: "MarActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "3月 (QA ratio)", type: "decimal", name: "MarPercentage", mask: "#.## %", width: 100 },
                            { title: "3月 (total)", type: "decimal", name: "MarResult", mask: "#,##0", width: 100, readOnly: true },


                            { title: "4月 (actual cost)", type: "decimal", name: "AprActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "4月 (QA ratio)", type: "decimal", name: "AprPercentage", mask: "#.## %", width: 100 },
                            { title: "4月 (total)", type: "decimal", name: "AprResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "5月 (actual cost)", type: "decimal", name: "MayActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "5月 (QA ratio)", type: "decimal", name: "MayPercentage", mask: "#.## %", width: 100 },
                            { title: "5月 (total)", type: "decimal", name: "MayResult", mask: "#,##0", width: 100, readOnly: true },


                            { title: "6月 (actual cost)", type: "decimal", name: "JunActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "6月 (QA ratio)", type: "decimal", name: "JunPercentage", mask: "#.## %", width: 100 },
                            { title: "6月 (total)", type: "decimal", name: "JunResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "7月 (actual cost)", type: "decimal", name: "JulActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "7月 (QA ratio)", type: "decimal", name: "JulPercentage", mask: "#.## %", width: 100 },
                            { title: "7月 (total)", type: "decimal", name: "JulResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "8月 (actual cost)", type: "decimal", name: "AugActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "8月 (QA ratio)", type: "decimal", name: "AugPercentage", mask: "#.## %", width: 100 },
                            { title: "8月 (total)", type: "decimal", name: "AugResult", mask: "#,##0", width: 100, readOnly: true },

                            { title: "9月 (actual cost)", type: "decimal", name: "SepActualCost", mask: "#,##0", width: 100, readOnly: true },
                            { title: "9月 (QA ratio)", type: "decimal", name: "SepPercentage", mask: "#.## %", width: 100 },
                            { title: "9月 (total)", type: "decimal", name: "SepResult", mask: "#,##0", width: 100, readOnly: true },

                        ],
                    });
                }
            });
        }, 3000);
    });

    $('#apportionment_save_button').click(function () {
        var dataToSend = [];
        var year = $('#assignment_year').val();

        if (jss != undefined) {
            var data = jss.getData(false);
            $.each(data, function (index, value) {
                var obj = {
                    departmentId: value[0],
                    octPercentage: parseFloat(value[3]),
                    novPercentage: parseFloat(value[6]),
                    decPercentage: parseFloat(value[9]),
                    janPercentage: parseFloat(value[12]),
                    bebPercentage: parseFloat(value[15]),
                    marPercentage: parseFloat(value[18]),
                    aprPercentage: parseFloat(value[21]),
                    mayPercentage: parseFloat(value[24]),
                    junPercentage: parseFloat(value[27]),
                    julPercentage: parseFloat(value[30]),
                    augPercentage: parseFloat(value[33]),
                    sepPercentage: parseFloat(value[36])
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

});
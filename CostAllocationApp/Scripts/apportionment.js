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
        var _retriveddata = [];
        if (year == null || year == '' || year == undefined) {
            alert('Select Year!!!');
            return false;
        }

        LoaderShow();

        setTimeout(function () {


            $.ajax({
                url: `/api/utilities/SearchForecastEmployee`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: "employeeName=&sectionId=&departmentId=&inchargeId=&roleId=&explanationId=&companyId=&status=&year=" + year + "&timeStampId=",
                success: function (data) {
                    _retriveddata = data;
                }
            });
            if (_retriveddata.length > 0) {
                var headCount = _retriveddata.find(x => x.EmployeeName == 'Head Count');
                var total = _retriveddata.find(x => x.EmployeeName == 'Total');
                $("#head_total").css("display", "inline-table");
                $('#head_total tbody').empty();
                $('#head_total tbody').append(`<tr>
                     <td>Head Count</td>
                     <td>${headCount.OctPoints}</td>
                     <td>${headCount.NovPoints}</td>
                     <td>${headCount.DecPoints}</td>
                     <td>${headCount.JanPoints}</td>
                     <td>${headCount.FebPoints}</td>
                     <td>${headCount.MarPoints}</td>
                     <td>${headCount.AprPoints}</td>
                     <td>${headCount.MayPoints}</td>
                     <td>${headCount.JunPoints}</td>
                     <td>${headCount.JulPoints}</td>
                     <td>${headCount.AugPoints}</td>
                     <td>${headCount.SepPoints}</td>
                 </tr>`);

                $('#head_total tbody').append(`<tr>
                 <td>Total</td>
                 <td>${headCount.OctPoints}</td>
                 <td>${headCount.NovPoints}</td>
                 <td>${headCount.DecPoints}</td>
                 <td>${headCount.JanPoints}</td>
                 <td>${headCount.FebPoints}</td>
                 <td>${headCount.MarPoints}</td>
                 <td>${headCount.AprPoints}</td>
                 <td>${headCount.MayPoints}</td>
                 <td>${headCount.JunPoints}</td>
                 <td>${headCount.JulPoints}</td>
                 <td>${headCount.AugPoints}</td>
                 <td>${headCount.SepPoints}</td>
             </tr>`);

                _retriveddata = _retriveddata.filter(d => d.EmployeeId !== 0);
            }



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
                        onchange: function (instance, cell, x, y, value) {
                            var count = 0;
                            var allPercentage = jss.getData();
                            $.each(allPercentage, function (index, value) {
                                count += value[x];
                            });

                            if (count > 100 || count < 0) {
                                alert("invalid value!");
                                jss.setValueFromCoords(x, y, 0, false);
                            }
                        },
                    });
                    jss.deleteColumn(14, 20);
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

            console.log(dataToSend);
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
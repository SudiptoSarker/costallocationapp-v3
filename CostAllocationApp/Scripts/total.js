var companyList = [];
var totalList = [];
var latestFiscalYear = 0;

$(document).ready(function () {
    var companies;

    $.ajax({
        url: `/api/Companies/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#companies').empty();
            $.each(data, function (key, item) {
                companyList.push(item);
                $('#companies').append(`<option value="${item.Id}">${item.CompanyName}</option>`);
            });
            companies = $('#companies').filterMultiSelect();
        }
    });

    

    $('#total_btn').on('click', () => {
        debugger;
        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);
        var selectedCompanyArray = [];
        for (var i = 0; i < companyArray.companies.length; i++) {
            for (var j = 0; j < companyList.length; j++) {
                if (companyArray.companies[i].toString() == companyList[j].Id.toString()) {
                    selectedCompanyArray.push(companyList[j]);
                    continue;
                }
            }
        }
        $.ajax({
            url: `/api/utilities/GetTotal?companiIds=${companyArray.companies.join(',')}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                totalList = data;
                console.log(totalList);
            }
        });

        $.ajax({
            url: `/api/utilities/LatestFiscalYear`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                latestFiscalYear = data;
            }
        });

        $('#total_table').empty();
        $('#total_table').append(`<thead><tr><th>開発区分</th><th>費用</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${latestFiscalYear}計</th><th>上期</th><th>下期 </th></tr></thead>`);
        $('#total_table').append('<tbody>');
        var othersDepartmentTotal = [];
        if (totalList.length > 0) {
            for (var k = 0; k < totalList.length; k++) {
                if (totalList[k].DepartmentName == "導入") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td class="text-right">${totalList[k].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                }
                else if (totalList[k].DepartmentName == "運用保守") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td class="text-right">${totalList[k].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

                }
                else if (totalList[k].DepartmentName == "New BLEND") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td class="text-right">${totalList[k].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                }
                else if (totalList[k].DepartmentName == "移行") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td class="text-right">${totalList[k].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                }
                else if (totalList[k].DepartmentName == "自治体") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td class="text-right">${totalList[k].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalList[k].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                }
                else {
                    othersDepartmentTotal.push(totalList[k]);
                }
            }
            if (othersDepartmentTotal.length > 0) {
                $('#total_table').append(`
                                <tr data-indentity='1'>
                                <td rowspan="${othersDepartmentTotal.length+1}">その他 </th>
                                <td data-deptid='${othersDepartmentTotal[0].DepartmentId}'><i class="fa fa-plus inner-expand" aria-hidden="true"></i> ${othersDepartmentTotal[0].DepartmentName}</td>
                                <td class="text-right">${othersDepartmentTotal[0].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[0].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                for (var l = 1; l < othersDepartmentTotal.length; l++) {
                    $('#total_table').append(`
                                <tr data-indentity='2'>
                                <td data-deptid='${othersDepartmentTotal[l].DepartmentId}'><i class="fa fa-plus inner-expand" aria-hidden="true"></i> ${othersDepartmentTotal[l].DepartmentName}</td>
                                <td class="text-right">${othersDepartmentTotal[l].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[l].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                }
            }
        }
        
        $('#total_table').append('</tbody>');

    });

    $(document).on('click', '.expand', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);

        var departmentId = $(closestRow[0].cells[1]).attr('data-deptid');


        $.ajax({
            url: `/api/utilities/GetTotalWithQA/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: { companiIds: companyArray.companies.join(','), departmentId: departmentId },
            success: function (data) {
                dataForExpandedRow = data;
                console.log(dataForExpandedRow);
            }
        });

        $(closestRow[0].cells[2]).html(dataForExpandedRow[0].OctCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[3]).html(dataForExpandedRow[0].NovCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[4]).html(dataForExpandedRow[0].DecCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[5]).html(dataForExpandedRow[0].JanCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[6]).html(dataForExpandedRow[0].FebCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[7]).html(dataForExpandedRow[0].MarCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[8]).html(dataForExpandedRow[0].AprCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[9]).html(dataForExpandedRow[0].MayCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JunCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[11]).html(dataForExpandedRow[0].JulCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[12]).html(dataForExpandedRow[0].AugCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[13]).html(dataForExpandedRow[0].SepCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[14]).html(dataForExpandedRow[0].RowTotal[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[15]).html(dataForExpandedRow[0].FirstSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[16]).html(dataForExpandedRow[0].SecondSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

        $(closestRow[0].cells[0]).attr('rowspan',3);
        closestRow.after(`
                                <tr>
                                <td>QA</th>
                                <td class="text-right">${ dataForExpandedRow[0].OctCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].NovCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].DecCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].JanCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].FebCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].MarCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].AprCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].MayCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].JunCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].JulCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].AugCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].SepCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].RowTotal[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].FirstSlot[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].SecondSlot[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                </tr>`);

        closestRow.next().after(`
                                <tr>
                                <td>Total</th>
                                <td class="text-right">${ dataForExpandedRow[0].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                <td class="text-right">${ dataForExpandedRow[0].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") }</td>
                                </tr>`);


        $(this).removeClass('fa fa-plus expand');
        $(this).addClass('fa fa-minus closed');
    });

    $(document).on('click', '.closed', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        $(closestRow[0].cells[0]).removeAttr('rowspan');
        $(this).removeClass('fa fa-minus closed');
        $(this).addClass('fa fa-plus expand');
        closestRow.next().remove();
        closestRow.next().remove();

        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);

        var departmentId = $(closestRow[0].cells[1]).attr('data-deptid');

        $.ajax({
            url: `/api/utilities/GetTotalWithQA/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: { companiIds: companyArray.companies.join(','), departmentId: departmentId },
            success: function (data) {
                dataForExpandedRow = data;
            }
        });

        $(closestRow[0].cells[2]).html(dataForExpandedRow[0].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[3]).html(dataForExpandedRow[0].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[4]).html(dataForExpandedRow[0].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[5]).html(dataForExpandedRow[0].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[6]).html(dataForExpandedRow[0].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[7]).html(dataForExpandedRow[0].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[8]).html(dataForExpandedRow[0].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[9]).html(dataForExpandedRow[0].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[11]).html(dataForExpandedRow[0].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[12]).html(dataForExpandedRow[0].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[13]).html(dataForExpandedRow[0].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[14]).html(dataForExpandedRow[0].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[15]).html(dataForExpandedRow[0].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $(closestRow[0].cells[16]).html(dataForExpandedRow[0].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

    });

    $(document).on('click', '.inner-expand', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);

        var departmentId = '';
        var identity = $(closestRow[0]).attr('data-indentity');

        if (parseInt(identity) == 1) {

            departmentId = $(closestRow[0].cells[1]).attr('data-deptid');
            $.ajax({
                url: `/api/utilities/GetTotalWithQA/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: { companiIds: companyArray.companies.join(','), departmentId: departmentId },
                success: function (data) {
                    dataForExpandedRow = data;
                    console.log(dataForExpandedRow);
                }
            });


            $(closestRow[0].cells[2]).html(dataForExpandedRow[0].OctCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(dataForExpandedRow[0].NovCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(dataForExpandedRow[0].DecCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(dataForExpandedRow[0].JanCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(dataForExpandedRow[0].FebCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(dataForExpandedRow[0].MarCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(dataForExpandedRow[0].AprCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(dataForExpandedRow[0].MayCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JunCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(dataForExpandedRow[0].JulCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(dataForExpandedRow[0].AugCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(dataForExpandedRow[0].SepCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[14]).html(dataForExpandedRow[0].RowTotal[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[15]).html(dataForExpandedRow[0].FirstSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[16]).html(dataForExpandedRow[0].SecondSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));





        }
        else {
            departmentId = $(closestRow[0].cells[0]).attr('data-deptid');

            $.ajax({
                url: `/api/utilities/GetTotalWithQA/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: { companiIds: companyArray.companies.join(','), departmentId: departmentId },
                success: function (data) {
                    dataForExpandedRow = data;
                    console.log(dataForExpandedRow);
                }
            });

            $(closestRow[0].cells[1]).html(dataForExpandedRow[0].OctCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[2]).html(dataForExpandedRow[0].NovCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(dataForExpandedRow[0].DecCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(dataForExpandedRow[0].JanCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(dataForExpandedRow[0].FebCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(dataForExpandedRow[0].MarCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(dataForExpandedRow[0].AprCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(dataForExpandedRow[0].MayCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(dataForExpandedRow[0].JunCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JulCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(dataForExpandedRow[0].AugCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(dataForExpandedRow[0].SepCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(dataForExpandedRow[0].RowTotal[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[14]).html(dataForExpandedRow[0].FirstSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[15]).html(dataForExpandedRow[0].SecondSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));



        }


        closestRow.after(`
                                <tr>
                                <td>QA</th>
                                <td class="text-right">${ dataForExpandedRow[0].OctCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].NovCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].DecCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JanCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].FebCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].MarCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].AprCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].MayCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JunCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JulCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].AugCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].SepCost[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].RowTotal[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].FirstSlot[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].SecondSlot[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

        closestRow.next().after(`
                                <tr>
                                <td>Total</th>
                                <td class="text-right">${ dataForExpandedRow[0].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${ dataForExpandedRow[0].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

        $(this).removeClass('fa fa-plus inner-expand');
        $(this).addClass('fa fa-minus inner-closed');



    });

    $(document).on('click', '.inner-closed', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        var identity = $(closestRow[0]).attr('data-indentity');
        var departmentId = '';


        $(this).removeClass('fa fa-minus closed');
        $(this).addClass('fa fa-plus expand');
        closestRow.next().remove();
        closestRow.next().remove();

        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);




        if (parseInt(identity) == 1) {
            departmentId = $(closestRow[0].cells[1]).attr('data-deptid');
            $.ajax({
                url: `/api/utilities/GetTotalWithQA/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: { companiIds: companyArray.companies.join(','), departmentId: departmentId },
                success: function (data) {
                    dataForExpandedRow = data;
                }
            });

            $(closestRow[0].cells[2]).html(dataForExpandedRow[0].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(dataForExpandedRow[0].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(dataForExpandedRow[0].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(dataForExpandedRow[0].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(dataForExpandedRow[0].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(dataForExpandedRow[0].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(dataForExpandedRow[0].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(dataForExpandedRow[0].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(dataForExpandedRow[0].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(dataForExpandedRow[0].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(dataForExpandedRow[0].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[14]).html(dataForExpandedRow[0].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[15]).html(dataForExpandedRow[0].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[16]).html(dataForExpandedRow[0].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        }
        else {
            departmentId = $(closestRow[0].cells[0]).attr('data-deptid');

            $.ajax({
                url: `/api/utilities/GetTotalWithQA/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: { companiIds: companyArray.companies.join(','), departmentId: departmentId },
                success: function (data) {
                    dataForExpandedRow = data;
                }
            });

            $(closestRow[0].cells[1]).html(dataForExpandedRow[0].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[2]).html(dataForExpandedRow[0].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(dataForExpandedRow[0].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(dataForExpandedRow[0].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(dataForExpandedRow[0].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(dataForExpandedRow[0].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(dataForExpandedRow[0].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(dataForExpandedRow[0].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(dataForExpandedRow[0].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(dataForExpandedRow[0].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(dataForExpandedRow[0].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(dataForExpandedRow[0].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[14]).html(dataForExpandedRow[0].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[15]).html(dataForExpandedRow[0].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

        }



        





    });

});
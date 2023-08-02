var companyList = [];
var totalList = [];
$.getJSON('/api/Companies/')
    .done(function (data) {
        $('#companies').empty();
        $.each(data, function (key, item) {
            companyList.push(item);
            $('#companies').append(`<option value="${item.Id}">${item.CompanyName}</option>`);
        });
    });



$(document).ready(function () {

    var companies = $('#companies').filterMultiSelect();

    $('#total_btn').on('click',  ()=> {
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

        $('#total_table').empty();
        $('#total_table').append(`<thead><tr><th>Field-1</th><th>Field-2</th><th>10</th><th>11</th><th>12</th><th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>Row Total</th></tr></thead>`);
        $('#total_table').append('<tbody>');
        var othersDepartmentTotal = [];
        if (totalList.length > 0) {
            for (var k = 0; k < totalList.length; k++) {
                if (totalList[k].DepartmentName == "DX事業本部") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td>${totalList[k].OctCost}</td>
                                <td>${totalList[k].NovCost}</td>
                                <td>${totalList[k].DecCost}</td>
                                <td>${totalList[k].JanCost}</td>
                                <td>${totalList[k].FebCost}</td>
                                <td>${totalList[k].MarCost}</td>
                                <td>${totalList[k].AprCost}</td>
                                <td>${totalList[k].MayCost}</td>
                                <td>${totalList[k].JunCost}</td>
                                <td>${totalList[k].JulCost}</td>
                                <td>${totalList[k].AugCost}</td>
                                <td>${totalList[k].SepCost}</td>
                                <td>${totalList[k].RowTotal}</td>
                                </tr>`);
                }
                else if (totalList[k].DepartmentName == "New BLEND") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td>${totalList[k].OctCost}</td>
                                <td>${totalList[k].NovCost}</td>
                                <td>${totalList[k].DecCost}</td>
                                <td>${totalList[k].JanCost}</td>
                                <td>${totalList[k].FebCost}</td>
                                <td>${totalList[k].MarCost}</td>
                                <td>${totalList[k].AprCost}</td>
                                <td>${totalList[k].MayCost}</td>
                                <td>${totalList[k].JunCost}</td>
                                <td>${totalList[k].JulCost}</td>
                                <td>${totalList[k].AugCost}</td>
                                <td>${totalList[k].SepCost}</td>
                                <td>${totalList[k].RowTotal}</td>
                                </tr>`);

                }
                else if (totalList[k].DepartmentName == "インテグレーション") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td>${totalList[k].OctCost}</td>
                                <td>${totalList[k].NovCost}</td>
                                <td>${totalList[k].DecCost}</td>
                                <td>${totalList[k].JanCost}</td>
                                <td>${totalList[k].FebCost}</td>
                                <td>${totalList[k].MarCost}</td>
                                <td>${totalList[k].AprCost}</td>
                                <td>${totalList[k].MayCost}</td>
                                <td>${totalList[k].JunCost}</td>
                                <td>${totalList[k].JulCost}</td>
                                <td>${totalList[k].AugCost}</td>
                                <td>${totalList[k].SepCost}</td>
                                <td>${totalList[k].RowTotal}</td>
                                </tr>`);
                }
                else if (totalList[k].DepartmentName == "インフラ") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td>${totalList[k].OctCost}</td>
                                <td>${totalList[k].NovCost}</td>
                                <td>${totalList[k].DecCost}</td>
                                <td>${totalList[k].JanCost}</td>
                                <td>${totalList[k].FebCost}</td>
                                <td>${totalList[k].MarCost}</td>
                                <td>${totalList[k].AprCost}</td>
                                <td>${totalList[k].MayCost}</td>
                                <td>${totalList[k].JunCost}</td>
                                <td>${totalList[k].JulCost}</td>
                                <td>${totalList[k].AugCost}</td>
                                <td>${totalList[k].SepCost}</td>
                                <td>${totalList[k].RowTotal}</td>
                                </tr>`);
                }
                else if (totalList[k].DepartmentName == "カスタマーサポート本部") {
                    $('#total_table').append(`
                                <tr data-count='${k + 1}'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>${totalList[k].DepartmentName}</th>
                                <td>${totalList[k].OctCost}</td>
                                <td>${totalList[k].NovCost}</td>
                                <td>${totalList[k].DecCost}</td>
                                <td>${totalList[k].JanCost}</td>
                                <td>${totalList[k].FebCost}</td>
                                <td>${totalList[k].MarCost}</td>
                                <td>${totalList[k].AprCost}</td>
                                <td>${totalList[k].MayCost}</td>
                                <td>${totalList[k].JunCost}</td>
                                <td>${totalList[k].JulCost}</td>
                                <td>${totalList[k].AugCost}</td>
                                <td>${totalList[k].SepCost}</td>
                                <td>${totalList[k].RowTotal}</td>
                                </tr>`);
                }
                else {
                    othersDepartmentTotal.push(totalList[k]);
                }
            }
            if (othersDepartmentTotal.length > 0) {
                $('#total_table').append(`
                                <tr data-count='6'>
                                <td rowspan="${othersDepartmentTotal.length+1}">その他 </th>
                                <td data-deptid='${othersDepartmentTotal[0].DepartmentId}'>${othersDepartmentTotal[0].DepartmentName}</th>
                                <td>${othersDepartmentTotal[0].OctCost}</td>
                                <td>${othersDepartmentTotal[0].NovCost}</td>
                                <td>${othersDepartmentTotal[0].DecCost}</td>
                                <td>${othersDepartmentTotal[0].JanCost}</td>
                                <td>${othersDepartmentTotal[0].FebCost}</td>
                                <td>${othersDepartmentTotal[0].MarCost}</td>
                                <td>${othersDepartmentTotal[0].AprCost}</td>
                                <td>${othersDepartmentTotal[0].MayCost}</td>
                                <td>${othersDepartmentTotal[0].JunCost}</td>
                                <td>${othersDepartmentTotal[0].JulCost}</td>
                                <td>${othersDepartmentTotal[0].AugCost}</td>
                                <td>${othersDepartmentTotal[0].SepCost}</td>
                                <td>${othersDepartmentTotal[0].RowTotal}</td>
                                </tr>`);
                for (var l = 0; l < othersDepartmentTotal.length; l++) {
                    $('#total_table').append(`
                                <tr>
                                <td data-deptid='${othersDepartmentTotal[l].DepartmentId}'>${othersDepartmentTotal[l].DepartmentName}</th>
                                <td>${othersDepartmentTotal[l].OctCost}</td>
                                <td>${othersDepartmentTotal[l].NovCost}</td>
                                <td>${othersDepartmentTotal[l].DecCost}</td>
                                <td>${othersDepartmentTotal[l].JanCost}</td>
                                <td>${othersDepartmentTotal[l].FebCost}</td>
                                <td>${othersDepartmentTotal[l].MarCost}</td>
                                <td>${othersDepartmentTotal[l].AprCost}</td>
                                <td>${othersDepartmentTotal[l].MayCost}</td>
                                <td>${othersDepartmentTotal[l].JunCost}</td>
                                <td>${othersDepartmentTotal[l].JulCost}</td>
                                <td>${othersDepartmentTotal[l].AugCost}</td>
                                <td>${othersDepartmentTotal[l].SepCost}</td>
                                <td>${othersDepartmentTotal[l].RowTotal}</td>
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

        $(closestRow[0].cells[2]).html(dataForExpandedRow[0].OctCost[0]);
        $(closestRow[0].cells[3]).html(dataForExpandedRow[0].NovCost[0]);
        $(closestRow[0].cells[4]).html(dataForExpandedRow[0].DecCost[0]);
        $(closestRow[0].cells[5]).html(dataForExpandedRow[0].JanCost[0]);
        $(closestRow[0].cells[6]).html(dataForExpandedRow[0].FebCost[0]);
        $(closestRow[0].cells[7]).html(dataForExpandedRow[0].MarCost[0]);
        $(closestRow[0].cells[8]).html(dataForExpandedRow[0].AprCost[0]);
        $(closestRow[0].cells[9]).html(dataForExpandedRow[0].MayCost[0]);
        $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JunCost[0]);
        $(closestRow[0].cells[11]).html(dataForExpandedRow[0].JulCost[0]);
        $(closestRow[0].cells[12]).html(dataForExpandedRow[0].AugCost[0]);
        $(closestRow[0].cells[13]).html(dataForExpandedRow[0].SepCost[0]);
        $(closestRow[0].cells[14]).html(dataForExpandedRow[0].RowTotal[0]);

        $(closestRow[0].cells[0]).attr('rowspan',3);
        closestRow.after(`
                                <tr>
                                <td>QA</th>
                                <td>${ dataForExpandedRow[0].OctCost[1] }</td>
                                <td>${ dataForExpandedRow[0].NovCost[1] }</td>
                                <td>${ dataForExpandedRow[0].DecCost[1] }</td>
                                <td>${ dataForExpandedRow[0].JanCost[1] }</td>
                                <td>${ dataForExpandedRow[0].FebCost[1] }</td>
                                <td>${ dataForExpandedRow[0].MarCost[1] }</td>
                                <td>${ dataForExpandedRow[0].AprCost[1] }</td>
                                <td>${ dataForExpandedRow[0].MayCost[1] }</td>
                                <td>${ dataForExpandedRow[0].JunCost[1] }</td>
                                <td>${ dataForExpandedRow[0].JulCost[1] }</td>
                                <td>${ dataForExpandedRow[0].AugCost[1] }</td>
                                <td>${ dataForExpandedRow[0].SepCost[1]}</td>
                                <td>${ dataForExpandedRow[0].RowTotal[1] }</td>
                                </tr>`);

        closestRow.next().after(`
                                <tr>
                                <td>Total</th>
                                <td>${ dataForExpandedRow[0].OctCost[2]}</td>
                                <td>${ dataForExpandedRow[0].NovCost[2]}</td>
                                <td>${ dataForExpandedRow[0].DecCost[2]}</td>
                                <td>${ dataForExpandedRow[0].JanCost[2]}</td>
                                <td>${ dataForExpandedRow[0].FebCost[2]}</td>
                                <td>${ dataForExpandedRow[0].MarCost[2]}</td>
                                <td>${ dataForExpandedRow[0].AprCost[2]}</td>
                                <td>${ dataForExpandedRow[0].MayCost[2]}</td>
                                <td>${ dataForExpandedRow[0].JunCost[2]}</td>
                                <td>${ dataForExpandedRow[0].JulCost[2]}</td>
                                <td>${ dataForExpandedRow[0].AugCost[2]}</td>
                                <td>${ dataForExpandedRow[0].SepCost[2] }</td>
                                <td>${ dataForExpandedRow[0].RowTotal[2] }</td>
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

        $(closestRow[0].cells[2]).html(dataForExpandedRow[0].OctCost[2]);
        $(closestRow[0].cells[3]).html(dataForExpandedRow[0].NovCost[2]);
        $(closestRow[0].cells[4]).html(dataForExpandedRow[0].DecCost[2]);
        $(closestRow[0].cells[5]).html(dataForExpandedRow[0].JanCost[2]);
        $(closestRow[0].cells[6]).html(dataForExpandedRow[0].FebCost[2]);
        $(closestRow[0].cells[7]).html(dataForExpandedRow[0].MarCost[2]);
        $(closestRow[0].cells[8]).html(dataForExpandedRow[0].AprCost[2]);
        $(closestRow[0].cells[9]).html(dataForExpandedRow[0].MayCost[2]);
        $(closestRow[0].cells[10]).html(dataForExpandedRow[0].JunCost[2]);
        $(closestRow[0].cells[11]).html(dataForExpandedRow[0].JulCost[2]);
        $(closestRow[0].cells[12]).html(dataForExpandedRow[0].AugCost[2]);
        $(closestRow[0].cells[13]).html(dataForExpandedRow[0].SepCost[2]);
        $(closestRow[0].cells[14]).html(dataForExpandedRow[0].RowTotal[2]);

    });

});
var companyList = [];
var totalList = [];
var totalListForBudget = [];
var latestFiscalYear = 0;
var othersDepartmentTotal = [];
var othersDepartmentTotalForBudget = [];
var othersDepartmentTotalForDifference = [];
var differenceTable = [];
var headCountList = [];
var uniqueCategoryList = [];



// for regular
var _octOtherTotal = 0;
var _novOtherTotal = 0;
var _decOtherTotal = 0;
var _janOtherTotal = 0;
var _febOtherTotal = 0;
var _marOtherTotal = 0;
var _aprOtherTotal = 0;
var _mayOtherTotal = 0;
var _junOtherTotal = 0;
var _julOtherTotal = 0;
var _augOtherTotal = 0;
var _sepOtherTotal = 0;
var _otherRowTotal = 0;
var _otherFisrtHalf = 0;
var _otherSecondHalf = 0;

//for budget
var _octOtherTotalBudget = 0;
var _novOtherTotalBudget = 0;
var _decOtherTotalBudget = 0;
var _janOtherTotalBudget = 0;
var _febOtherTotalBudget = 0;
var _marOtherTotalBudget = 0;
var _aprOtherTotalBudget = 0;
var _mayOtherTotalBudget = 0;
var _junOtherTotalBudget = 0;
var _julOtherTotalBudget = 0;
var _augOtherTotalBudget = 0;
var _sepOtherTotalBudget = 0;
var _otherRowTotalBudget = 0;
var _otherFisrtHalfBudget = 0;
var _otherSecondHalfBudget = 0;

// for difference
var _octOtherTotalDifference = 0;
var _novOtherTotalDifference = 0;
var _decOtherTotalDifference = 0;
var _janOtherTotalDifference = 0;
var _febOtherTotalDifference = 0;
var _marOtherTotalDifference = 0;
var _aprOtherTotalDifference = 0;
var _mayOtherTotalDifference = 0;
var _junOtherTotalDifference = 0;
var _julOtherTotalDifference = 0;
var _augOtherTotalDifference = 0;
var _sepOtherTotalDifference = 0;
var _otherRowTotalDifference = 0;
var _otherFirstHalfDifference = 0;
var _otherSecondHalfDifference = 0;

// for regular
var _octTotal = 0;
var _novTotal = 0;
var _decTotal = 0;
var _janTotal = 0;
var _febTotal = 0;
var _marTotal = 0;
var _aprTotal = 0;
var _mayTotal = 0;
var _junTotal = 0;
var _julTotal = 0;
var _augTotal = 0;
var _sepTotal = 0;
var _rowTotal = 0;
var _firstHalf = 0;
var _secondHalf = 0;

// for budget
var _octTotalBudget = 0;
var _novTotalBudget = 0;
var _decTotalBudget = 0;
var _janTotalBudget = 0;
var _febTotalBudget = 0;
var _marTotalBudget = 0;
var _aprTotalBudget = 0;
var _mayTotalBudget = 0;
var _junTotalBudget = 0;
var _julTotalBudget = 0;
var _augTotalBudget = 0;
var _sepTotalBudget = 0;
var _rowTotalBudget = 0;
var _firstHalfBudget = 0;
var _secondHalfBudget = 0;

// for difference
var _octTotalDifference = 0;
var _novTotalDifference = 0;
var _decTotalDifference = 0;
var _janTotalDifference = 0;
var _febTotalDifference = 0;
var _marTotalDifference = 0;
var _aprTotalDifference = 0;
var _mayTotalDifference = 0;
var _junTotalDifference = 0;
var _julTotalDifference = 0;
var _augTotalDifference = 0;
var _sepTotalDifference = 0;
var _rowTotalDifference = 0;
var _firstHalfDifference = 0;
var _secondHalfDifference = 0;



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

        totalList = [];
        totalListForBudget = [];
        latestFiscalYear = 0;
        othersDepartmentTotal = [];
        othersDepartmentTotalForBudget = [];
        othersDepartmentTotalForDifference = [];
        differenceTable = [];
        headCountList = [];
        uniqueCategoryList = [];


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
                differenceTable = structuredClone(totalList);
                //differenceTable = totalList.map(function (arr) {
                //    return arr.slice();
                //});
                //console.log(totalList);
            }
        });

        $.ajax({
            url: `/api/utilities/GetInitialBudgetForTotal?companiIds=${companyArray.companies.join(',')}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                totalListForBudget = data;
            }
        });

        $.ajax({
            url: `/api/utilities/GetHeadCount?companiIds=${companyArray.companies.join(',')}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                headCountList = data;
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

        // code for total table
        {
                $('#p-total').remove();
                $('#total_table').before('<p class="font-weight-bold" id="p-total" style="margin-top:20px;"><u>集計 (Total):</u></p>');
                $('#total_table').empty();
                $('#total_table').append(`<thead><tr><th>開発区分</th><th>費用</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${latestFiscalYear}計</th><th>上期</th><th>下期 </th></tr></thead>`);
                $('#total_table').append('<tbody>');
                if (totalList.length > 0) {

                    _octTotal = 0;
                    _novTotal = 0;
                    _decTotal = 0;
                    _janTotal = 0;
                    _febTotal = 0;
                    _marTotal = 0;
                    _aprTotal = 0;
                    _mayTotal = 0;
                    _junTotal = 0;
                    _julTotal = 0;
                    _augTotal = 0;
                    _sepTotal = 0;
                    _rowTotal = 0;
                    _firstHalf = 0;
                    _secondHalf = 0;


                    for (var k = 0; k < totalList.length; k++) {

                        _octTotal += parseFloat(totalList[k].OctCost);
                        _novTotal += parseFloat(totalList[k].NovCost);
                        _decTotal += parseFloat(totalList[k].DecCost);
                        _janTotal += parseFloat(totalList[k].JanCost);
                        _febTotal += parseFloat(totalList[k].FebCost);
                        _marTotal += parseFloat(totalList[k].MarCost);
                        _aprTotal += parseFloat(totalList[k].AprCost);
                        _mayTotal += parseFloat(totalList[k].MayCost);
                        _junTotal += parseFloat(totalList[k].JunCost);
                        _julTotal += parseFloat(totalList[k].JulCost);
                        _augTotal += parseFloat(totalList[k].AugCost);
                        _sepTotal += parseFloat(totalList[k].SepCost);
                        _rowTotal += parseFloat(totalList[k].RowTotal);
                        _firstHalf += parseFloat(totalList[k].FirstSlot);
                        _secondHalf += parseFloat(totalList[k].SecondSlot);


                        if (totalList[k].DepartmentName == "導入") {
                            $('#total_table').append(`
                                <tr data-indentity='0'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>Total</th>
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
                                <tr data-indentity='0'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>Total</th>
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
                                <tr data-indentity='0'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>Total</th>
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
                                <tr data-indentity='0'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>Total</th>
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
                                <tr data-indentity='0'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> ${totalList[k].DepartmentName}</th>
                                <td data-deptid='${totalList[k].DepartmentId}'>Total</th>
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

                        _octOtherTotal = 0;
                        _novOtherTotal = 0;
                        _decOtherTotal = 0;
                        _janOtherTotal = 0;
                        _febOtherTotal = 0;
                        _marOtherTotal = 0;
                        _aprOtherTotal = 0;
                        _mayOtherTotal = 0;
                        _junOtherTotal = 0;
                        _julOtherTotal = 0;
                        _augOtherTotal = 0;
                        _sepOtherTotal = 0;
                        _otherRowTotal = 0;
                        _otherFisrtHalf = 0;
                        _otherSecondHalf = 0;

                        console.log(othersDepartmentTotal);

                        //debugger;
                        for (var l = 0; l < othersDepartmentTotal.length; l++) {
                            _octOtherTotal += parseFloat(othersDepartmentTotal[l].OctCost);
                            _novOtherTotal += parseFloat(othersDepartmentTotal[l].NovCost);
                            _decOtherTotal += parseFloat(othersDepartmentTotal[l].DecCost);
                            _janOtherTotal += parseFloat(othersDepartmentTotal[l].JanCost);
                            _febOtherTotal += parseFloat(othersDepartmentTotal[l].FebCost);
                            _marOtherTotal += parseFloat(othersDepartmentTotal[l].MarCost);
                            _otherFisrtHalf += parseFloat(othersDepartmentTotal[l].OctCost) + parseFloat(othersDepartmentTotal[l].NovCost) + parseFloat(othersDepartmentTotal[l].DecCost) + parseFloat(othersDepartmentTotal[l].JanCost) + parseFloat(othersDepartmentTotal[l].FebCost) + parseFloat(othersDepartmentTotal[l].MarCost);
                            _aprOtherTotal += parseFloat(othersDepartmentTotal[l].AprCost);
                            _mayOtherTotal += parseFloat(othersDepartmentTotal[l].MayCost);
                            _junOtherTotal += parseFloat(othersDepartmentTotal[l].JunCost);
                            _julOtherTotal += parseFloat(othersDepartmentTotal[l].JulCost);
                            _augOtherTotal += parseFloat(othersDepartmentTotal[l].AugCost);
                            _sepOtherTotal += parseFloat(othersDepartmentTotal[l].SepCost);
                            _otherSecondHalf += parseFloat(othersDepartmentTotal[l].AprCost) + parseFloat(othersDepartmentTotal[l].MayCost) + parseFloat(othersDepartmentTotal[l].JunCost) + parseFloat(othersDepartmentTotal[l].JulCost) + parseFloat(othersDepartmentTotal[l].AugCost) + parseFloat(othersDepartmentTotal[l].SepCost);

                        }
                        _otherRowTotal += _otherFisrtHalf + _otherSecondHalf;

                        $('#total_table').append(`
                                <tr data-indentity='1'>
                                <td><i class="fa fa-plus expand" aria-hidden="true"></i> その他 </th>
                                <td>Total</td>
                                <td class="text-right">${_octOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_otherRowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_otherFisrtHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_otherSecondHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);


                    }

                }

                $('#total_table').append(`
                                <tr data-indentity='2'>
                                <td colspan="2" class="text-center">Total</td>
                                <td class="text-right">${_octTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_rowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_firstHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_secondHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

                $('#total_table').append('</tbody>');
            }

        // code for total budget table
        {
                $('#p-initial-budget').remove();
                $('#total_budget_table').before('<p class="font-weight-bold" id="p-initial-budget"><u>初期年度予算 (Initial Budget):</u></p>');
                $('#total_budget_table').empty();
                $('#total_budget_table').append(`<thead><tr><th>開発区分</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${latestFiscalYear}計</th><th>上期</th><th>下期 </th></tr></thead>`);
                $('#total_budget_table').append('<tbody>');
                if (totalListForBudget.length > 0) {

                    _octTotalBudget = 0;
                    _novTotalBudget = 0;
                    _decTotalBudget = 0;
                    _janTotalBudget = 0;
                    _febTotalBudget = 0;
                    _marTotalBudget = 0;
                    _aprTotalBudget = 0;
                    _mayTotalBudget = 0;
                    _junTotalBudget = 0;
                    _julTotalBudget = 0;
                    _augTotalBudget = 0;
                    _sepTotalBudget = 0;
                    _rowTotalBudget = 0;
                    _firstHalfBudget = 0;
                    _secondHalfBudget = 0;


                    for (var a = 0; a < totalListForBudget.length; a++) {

                        // calculate each department with 1st table data.
                        {
                            var singleDepartment = null;
                            for (var z = 0; z < differenceTable.length; z++) {
                                if (differenceTable[z].DepartmentId == totalListForBudget[a].DepartmentId) {
                                    singleDepartment = differenceTable[z];
                                    singleDepartment.OctCost = parseFloat(singleDepartment.OctCost) - parseFloat(totalListForBudget[a].OctCost);
                                    singleDepartment.NovCost = parseFloat(singleDepartment.NovCost) - parseFloat(totalListForBudget[a].NovCost);
                                    singleDepartment.DecCost = parseFloat(singleDepartment.DecCost) - parseFloat(totalListForBudget[a].DecCost);
                                    singleDepartment.JanCost = parseFloat(singleDepartment.JanCost) - parseFloat(totalListForBudget[a].JanCost);
                                    singleDepartment.FebCost = parseFloat(singleDepartment.FebCost) - parseFloat(totalListForBudget[a].FebCost);
                                    singleDepartment.MarCost = parseFloat(singleDepartment.MarCost) - parseFloat(totalListForBudget[a].MarCost);
                                    singleDepartment.AprCost = parseFloat(singleDepartment.AprCost) - parseFloat(totalListForBudget[a].AprCost);
                                    singleDepartment.MayCost = parseFloat(singleDepartment.MayCost) - parseFloat(totalListForBudget[a].MayCost);
                                    singleDepartment.JunCost = parseFloat(singleDepartment.JunCost) - parseFloat(totalListForBudget[a].JunCost);
                                    singleDepartment.JulCost = parseFloat(singleDepartment.JulCost) - parseFloat(totalListForBudget[a].JulCost);
                                    singleDepartment.AugCost = parseFloat(singleDepartment.AugCost) - parseFloat(totalListForBudget[a].AugCost);
                                    singleDepartment.SepCost = parseFloat(singleDepartment.SepCost) - parseFloat(totalListForBudget[a].SepCost);
                                    singleDepartment.RowTotal[0] = parseFloat(singleDepartment.RowTotal[0]) - parseFloat(totalListForBudget[a].RowTotal[0]);
                                    singleDepartment.FirstSlot[0] = parseFloat(singleDepartment.FirstSlot[0]) - parseFloat(totalListForBudget[a].FirstSlot[0]);
                                    singleDepartment.SecondSlot[0] = parseFloat(singleDepartment.SecondSlot[0]) - parseFloat(totalListForBudget[a].SecondSlot[0]);

                                }
                            }


                        }
                        _octTotalBudget += parseFloat(totalListForBudget[a].OctCost);
                        _novTotalBudget += parseFloat(totalListForBudget[a].NovCost);
                        _decTotalBudget += parseFloat(totalListForBudget[a].DecCost);
                        _janTotalBudget += parseFloat(totalListForBudget[a].JanCost);
                        _febTotalBudget += parseFloat(totalListForBudget[a].FebCost);
                        _marTotalBudget += parseFloat(totalListForBudget[a].MarCost);
                        _aprTotalBudget += parseFloat(totalListForBudget[a].AprCost);
                        _mayTotalBudget += parseFloat(totalListForBudget[a].MayCost);
                        _junTotalBudget += parseFloat(totalListForBudget[a].JunCost);
                        _julTotalBudget += parseFloat(totalListForBudget[a].JulCost);
                        _augTotalBudget += parseFloat(totalListForBudget[a].AugCost);
                        _sepTotalBudget += parseFloat(totalListForBudget[a].SepCost);
                        _rowTotalBudget += parseFloat(totalListForBudget[a].RowTotal);
                        _firstHalfBudget += parseFloat(totalListForBudget[a].FirstSlot);
                        _secondHalfBudget += parseFloat(totalListForBudget[a].SecondSlot);


                        if (totalListForBudget[a].DepartmentName == "導入") {
                            $('#total_budget_table').append(`
                                <tr>
                                <td>${totalListForBudget[a].DepartmentName}</th>
                                <td class="text-right">${totalListForBudget[a].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else if (totalListForBudget[a].DepartmentName == "運用保守") {
                            $('#total_budget_table').append(`
                                <tr>
                                <td>${totalListForBudget[a].DepartmentName}</th>
                                <td class="text-right">${totalListForBudget[a].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

                        }
                        else if (totalListForBudget[a].DepartmentName == "New BLEND") {
                            $('#total_budget_table').append(`
                                <tr>
                                <td>${totalListForBudget[a].DepartmentName}</th>
                                <td class="text-right">${totalListForBudget[a].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else if (totalListForBudget[a].DepartmentName == "移行") {
                            $('#total_budget_table').append(`
                                <tr>
                                <td>${totalListForBudget[a].DepartmentName}</th>
                                <td class="text-right">${totalListForBudget[a].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else if (totalListForBudget[a].DepartmentName == "自治体") {
                            $('#total_budget_table').append(`
                                <tr>
                                <td>${totalListForBudget[a].DepartmentName}</th>
                                <td class="text-right">${totalListForBudget[a].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${totalListForBudget[a].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else {
                            othersDepartmentTotalForBudget.push(totalListForBudget[a]);
                        }
                    }
                    if (othersDepartmentTotalForBudget.length > 0) {

                        _octOtherTotalBudget = 0;
                        _novOtherTotalBudget = 0;
                        _decOtherTotalBudget = 0;
                        _janOtherTotalBudget = 0;
                        _febOtherTotalBudget = 0;
                        _marOtherTotalBudget = 0;
                        _aprOtherTotalBudget = 0;
                        _mayOtherTotalBudget = 0;
                        _junOtherTotalBudget = 0;
                        _julOtherTotalBudget = 0;
                        _augOtherTotalBudget = 0;
                        _sepOtherTotalBudget = 0;
                        _otherRowTotalBudget = 0;
                        _otherFisrtHalfBudget = 0;
                        _otherSecondHalfBudget = 0;


                        for (var b = 0; b < othersDepartmentTotalForBudget.length; b++) {
                            _octOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].OctCost);
                            _novOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].NovCost);
                            _decOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].DecCost);
                            _janOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].JanCost);
                            _febOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].FebCost);
                            _marOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].MarCost);
                            _otherFisrtHalfBudget += parseFloat(othersDepartmentTotalForBudget[b].OctCost) + parseFloat(othersDepartmentTotalForBudget[b].NovCost) + parseFloat(othersDepartmentTotalForBudget[b].DecCost) + parseFloat(othersDepartmentTotalForBudget[b].JanCost) + parseFloat(othersDepartmentTotalForBudget[b].FebCost) + parseFloat(othersDepartmentTotalForBudget[b].MarCost);
                            _aprOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].AprCost);
                            _mayOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].MayCost);
                            _junOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].JunCost);
                            _julOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].JulCost);
                            _augOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].AugCost);
                            _sepOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].SepCost);
                            _otherSecondHalfBudget += parseFloat(othersDepartmentTotalForBudget[b].AprCost) + parseFloat(othersDepartmentTotalForBudget[b].MayCost) + parseFloat(othersDepartmentTotalForBudget[b].JunCost) + parseFloat(othersDepartmentTotalForBudget[b].JulCost) + parseFloat(othersDepartmentTotalForBudget[b].AugCost) + parseFloat(othersDepartmentTotalForBudget[b].SepCost);

                        }
                        _otherRowTotalBudget += _otherFisrtHalfBudget + _otherSecondHalfBudget;

                        $('#total_budget_table').append(`
                                <tr>
                                <td>その他 </th>
                                <td class="text-right">${_octOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepOtherTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_otherRowTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_otherFisrtHalfBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_otherSecondHalfBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);


                    }

                }

                $('#total_budget_table').append(`
                                <tr>
                                <td class="text-center">Total</td>
                                <td class="text-right">${_octTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_rowTotalBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_firstHalfBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_secondHalfBudget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

                $('#total_budget_table').append('</tbody>');
            }
        // code for difference table
        {
                $('#p-difference').remove();
                $('#difference_table').before('<p class="font-weight-bold" id="p-difference"><u>差分 (Difference):</u></p>');
                $('#difference_table').empty();
                $('#difference_table').append(`<thead><tr><th>開発区分</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${latestFiscalYear}計</th><th>上期</th><th>下期 </th></tr></thead>`);
                $('#difference_table').append('<tbody>');
                if (differenceTable.length > 0) {

                    _octTotalDifference = 0;
                    _novTotalDifference = 0;
                    _decTotalDifference = 0;
                    _janTotalDifference = 0;
                    _febTotalDifference = 0;
                    _marTotalDifference = 0;
                    _aprTotalDifference = 0;
                    _mayTotalDifference = 0;
                    _junTotalDifference = 0;
                    _julTotalDifference = 0;
                    _augTotalDifference = 0;
                    _sepTotalDifference = 0;
                    _rowTotalDifference = 0;
                    _firstHalfDifference = 0;
                    _secondHalfDifference = 0;


                    for (var d = 0; d < differenceTable.length; d++) {
                        _octTotalDifference += parseFloat(differenceTable[d].OctCost);
                        _novTotalDifference += parseFloat(differenceTable[d].NovCost);
                        _decTotalDifference += parseFloat(differenceTable[d].DecCost);
                        _janTotalDifference += parseFloat(differenceTable[d].JanCost);
                        _febTotalDifference += parseFloat(differenceTable[d].FebCost);
                        _marTotalDifference += parseFloat(differenceTable[d].MarCost);
                        _aprTotalDifference += parseFloat(differenceTable[d].AprCost);
                        _mayTotalDifference += parseFloat(differenceTable[d].MayCost);
                        _junTotalDifference += parseFloat(differenceTable[d].JunCost);
                        _julTotalDifference += parseFloat(differenceTable[d].JulCost);
                        _augTotalDifference += parseFloat(differenceTable[d].AugCost);
                        _sepTotalDifference += parseFloat(differenceTable[d].SepCost);
                        _rowTotalDifference += parseFloat(differenceTable[d].RowTotal);
                        _firstHalfDifference += parseFloat(differenceTable[d].FirstSlot);
                        _secondHalfDifference += parseFloat(differenceTable[d].SecondSlot);


                        if (differenceTable[d].DepartmentName == "導入") {
                            $('#difference_table').append(`
                                <tr>
                                <td>${differenceTable[d].DepartmentName}</th>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].OctCost}">${differenceTable[d].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].NovCost}">${differenceTable[d].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].DecCost}">${differenceTable[d].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JanCost}">${differenceTable[d].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FebCost}">${differenceTable[d].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MarCost}">${differenceTable[d].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AprCost}">${differenceTable[d].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MayCost}">${differenceTable[d].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JunCost}">${differenceTable[d].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JulCost}">${differenceTable[d].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AugCost}">${differenceTable[d].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SepCost}">${differenceTable[d].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].RowTotal}">${differenceTable[d].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FirstSlot}">${differenceTable[d].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SecondSlot}">${differenceTable[d].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else if (differenceTable[d].DepartmentName == "運用保守") {
                            $('#difference_table').append(`
                                <tr>
                                <td>${differenceTable[d].DepartmentName}</th>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].OctCost}">${differenceTable[d].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].NovCost}">${differenceTable[d].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].DecCost}">${differenceTable[d].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JanCost}">${differenceTable[d].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FebCost}">${differenceTable[d].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MarCost}">${differenceTable[d].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AprCost}">${differenceTable[d].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MayCost}">${differenceTable[d].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JunCost}">${differenceTable[d].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JulCost}">${differenceTable[d].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AugCost}">${differenceTable[d].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SepCost}">${differenceTable[d].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].RowTotal}">${differenceTable[d].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FirstSlot}">${differenceTable[d].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SecondSlot}">${differenceTable[d].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

                        }
                        else if (differenceTable[d].DepartmentName == "New BLEND") {
                            $('#difference_table').append(`
                                <tr>
                                <td>${differenceTable[d].DepartmentName}</th>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].OctCost}">${differenceTable[d].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].NovCost}">${differenceTable[d].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].DecCost}">${differenceTable[d].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JanCost}">${differenceTable[d].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FebCost}">${differenceTable[d].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MarCost}">${differenceTable[d].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AprCost}">${differenceTable[d].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MayCost}">${differenceTable[d].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JunCost}">${differenceTable[d].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JulCost}">${differenceTable[d].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AugCost}">${differenceTable[d].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SepCost}">${differenceTable[d].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].RowTotal}">${differenceTable[d].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FirstSlot}">${differenceTable[d].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SecondSlot}">${differenceTable[d].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else if (differenceTable[d].DepartmentName == "移行") {
                            $('#difference_table').append(`
                                <tr>
                                <td>${differenceTable[d].DepartmentName}</th>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].OctCost}">${differenceTable[d].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].NovCost}">${differenceTable[d].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].DecCost}">${differenceTable[d].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JanCost}">${differenceTable[d].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FebCost}">${differenceTable[d].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MarCost}">${differenceTable[d].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AprCost}">${differenceTable[d].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MayCost}">${differenceTable[d].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JunCost}">${differenceTable[d].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JulCost}">${differenceTable[d].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AugCost}">${differenceTable[d].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SepCost}">${differenceTable[d].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].RowTotal}">${differenceTable[d].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FirstSlot}">${differenceTable[d].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SecondSlot}">${differenceTable[d].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else if (differenceTable[d].DepartmentName == "自治体") {
                            $('#difference_table').append(`
                                <tr>
                                <td>${differenceTable[d].DepartmentName}</th>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].OctCost}">${differenceTable[d].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].NovCost}">${differenceTable[d].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].DecCost}">${differenceTable[d].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JanCost}">${differenceTable[d].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FebCost}">${differenceTable[d].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MarCost}">${differenceTable[d].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AprCost}">${differenceTable[d].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].MayCost}">${differenceTable[d].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JunCost}">${differenceTable[d].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].JulCost}">${differenceTable[d].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].AugCost}">${differenceTable[d].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SepCost}">${differenceTable[d].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].RowTotal}">${differenceTable[d].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].FirstSlot}">${differenceTable[d].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${differenceTable[d].SecondSlot}">${differenceTable[d].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
                        }
                        else {
                            othersDepartmentTotalForDifference.push(differenceTable[d]);
                        }
                    }
                    if (othersDepartmentTotalForDifference.length > 0) {

                        _octOtherTotalDifference = 0;
                        _novOtherTotalDifference = 0;
                        _decOtherTotalDifference = 0;
                        _janOtherTotalDifference = 0;
                        _febOtherTotalDifference = 0;
                        _marOtherTotalDifference = 0;
                        _aprOtherTotalDifference = 0;
                        _mayOtherTotalDifference = 0;
                        _junOtherTotalDifference = 0;
                        _julOtherTotalDifference = 0;
                        _augOtherTotalDifference = 0;
                        _sepOtherTotalDifference = 0;
                        _otherRowTotalDifference = 0;
                        _otherFirstHalfDifference = 0;
                        _otherSecondHalfDifference = 0;


                        for (var e = 0; e < othersDepartmentTotalForDifference.length; e++) {
                            _octOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].OctCost);
                            _novOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].NovCost);
                            _decOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].DecCost);
                            _janOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].JanCost);
                            _febOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].FebCost);
                            _marOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].MarCost);
                            _otherFirstHalfDifference += parseFloat(othersDepartmentTotalForDifference[e].OctCost) + parseFloat(othersDepartmentTotalForDifference[e].NovCost) + parseFloat(othersDepartmentTotalForDifference[e].DecCost) + parseFloat(othersDepartmentTotalForDifference[e].JanCost) + parseFloat(othersDepartmentTotalForDifference[e].FebCost) + parseFloat(othersDepartmentTotalForDifference[e].MarCost);
                            _aprOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].AprCost);
                            _mayOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].MayCost);
                            _junOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].JunCost);
                            _julOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].JulCost);
                            _augOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].AugCost);
                            _sepOtherTotalDifference += parseFloat(othersDepartmentTotalForDifference[e].SepCost);
                            _otherSecondHalfDifference += parseFloat(othersDepartmentTotalForDifference[e].AprCost) + parseFloat(othersDepartmentTotalForDifference[e].MayCost) + parseFloat(othersDepartmentTotalForDifference[e].JunCost) + parseFloat(othersDepartmentTotalForDifference[e].JulCost) + parseFloat(othersDepartmentTotalForDifference[e].AugCost) + parseFloat(othersDepartmentTotalForDifference[e].SepCost);

                        }
                        _otherRowTotalDifference += _otherFirstHalfDifference + _otherSecondHalfDifference;

                        $('#difference_table').append(`
                                <tr>
                                <td>その他 </th>
                                <td class="text-right" data-monetary-amount="${_octOtherTotalDifference}">${_octOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_novOtherTotalDifference}">${_novOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_decOtherTotalDifference}">${_decOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_janOtherTotalDifference}">${_janOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_febOtherTotalDifference}">${_febOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_marOtherTotalDifference}">${_marOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_aprOtherTotalDifference}">${_aprOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_mayOtherTotalDifference}">${_mayOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_junOtherTotalDifference}">${_junOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_julOtherTotalDifference}">${_julOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_augOtherTotalDifference}">${_augOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_sepOtherTotalDifference}">${_sepOtherTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_otherRowTotalDifference}">${_otherRowTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_otherFirstHalfDifference}">${_otherFirstHalfDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_otherSecondHalfDifference}">${_otherSecondHalfDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);


                    }

                }

                $('#difference_table').append(`
                                <tr>
                                <td class="text-center">Total</td>
                                <td class="text-right" data-monetary-amount="${_octTotalDifference}">${_octTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_novTotalDifference}">${_novTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_decTotalDifference}">${_decTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_janTotalDifference}">${_janTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_febTotalDifference}">${_febTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_marTotalDifference}">${_marTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_aprTotalDifference}">${_aprTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_mayTotalDifference}">${_mayTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_junTotalDifference}">${_junTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_julTotalDifference}">${_julTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_augTotalDifference}">${_augTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_sepTotalDifference}">${_sepTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_rowTotalDifference}">${_rowTotalDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_firstHalfDifference}">${_firstHalfDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right" data-monetary-amount="${_secondHalfDifference}">${_secondHalfDifference.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

                $('#difference_table').append('</tbody>');
            }

        // code for head count table
        {
            // get all the unique categories
            for (var g = 0; g < headCountList.length; g++) {
                if (uniqueCategoryList.length == 0) {
                    uniqueCategoryList.push(headCountList[g].CategoryName.toString());
                }
                else {
                    if (!uniqueCategoryList.includes(headCountList[g].CategoryName.toString())) {
                        uniqueCategoryList.push(headCountList[g].CategoryName.toString());
                    }
                }
       
            }

            console.log(uniqueCategoryList);
            var _octCount = 0, _novCount = 0, _decCount = 0, _janCount = 0, _febCount = 0, _marCount = 0, _aprCount = 0, _mayCount = 0, _junCount = 0, _julCount = 0, _augCount = 0, _sepCount = 0;
            var _octCountTotal = 0, _novCountTotal = 0, _decCountTotal = 0, _janCountTotal = 0, _febCountTotal = 0, _marCountTotal = 0, _aprCountTotal = 0, _mayCountTotal = 0, _junCountTotal = 0, _julCountTotal = 0, _augCountTotal = 0, _sepCountTotal = 0;

            $('#p-headcount').remove();
            $('#headcount_table').before('<p class="font-weight-bold" id="p-headcount"><u>要員数 (Head Count):</u></p>');
            $('#headcount_table').empty();
            $('#headcount_table').append(`<thead><tr><th>departments</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th></tr></thead>`);
            $('#headcount_table').append('<tbody>');
            for (var f = 0; f < uniqueCategoryList.length; f++) {
                _octCount = 0, _novCount = 0, _decCount = 0, _janCount = 0, _febCount = 0, _marCount = 0, _aprCount = 0, _mayCount = 0, _junCount = 0, _julCount = 0, _augCount = 0, _sepCount = 0;
                for (var h = 0; h < headCountList.length; h++) {
                    if (headCountList[h].CategoryName.toString() == uniqueCategoryList[f]) {
                        _octCount += parseFloat(headCountList[h].OctCount);
                        _novCount += parseFloat(headCountList[h].NovCount);
                        _decCount += parseFloat(headCountList[h].DecCount);
                        _janCount += parseFloat(headCountList[h].JanCount);
                        _febCount += parseFloat(headCountList[h].FebCount);
                        _marCount += parseFloat(headCountList[h].MarCount);
                        _aprCount += parseFloat(headCountList[h].AprCount);
                        _mayCount += parseFloat(headCountList[h].MayCount);
                        _junCount += parseFloat(headCountList[h].JunCount);
                        _julCount += parseFloat(headCountList[h].JulCount);
                        _augCount += parseFloat(headCountList[h].AugCount);
                        _sepCount += parseFloat(headCountList[h].SepCount);
                    }
                }

                // sum of all head counts
                _octCountTotal += _octCount;
                _novCountTotal += _novCount;
                _decCountTotal += _decCount;
                _janCountTotal += _janCount;
                _febCountTotal += _febCount;
                _marCountTotal += _marCount;
                _aprCountTotal += _aprCount;
                _mayCountTotal += _mayCount;
                _junCountTotal += _junCount;
                _julCountTotal += _julCount;
                _augCountTotal += _augCount;
                _sepCountTotal += _sepCount;

                $('#headcount_table').append(`
                                <tr data-category='${uniqueCategoryList[f]}'>
                                <td><i class="fa fa-plus expand-count" aria-hidden="true"></i> ${uniqueCategoryList[f]}</th>
                                <td class="text-right">${_octCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
            }

            $('#headcount_table').append(`
                                <tr>
                                <td class="text-center">Total</th>
                                <td class="text-right">${_octCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepCountTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

            $('#headcount_table').append('</tbody>');
        }

    });

    $(document).on('click', '.expand-count-inner', function () {
        var closestRow = $(this).closest('tr');
        var categoryName = $(closestRow).attr('data-category');
        var subCategoryName = $(closestRow[0].cells[0]).attr('data-subcategory');
        // filtering department by category and subcategory
        var filteredDepartmentList = [];

        for (var u = 0; u < headCountList.length; u++) {
            if ((headCountList[u].CategoryName == categoryName) && (headCountList[u].SubCategoryName == subCategoryName)) {
                filteredDepartmentList.push(headCountList[u]);
            }

        }

        if (filteredDepartmentList.length > 0) {

            for (var v = 0; v < filteredDepartmentList.length; v++) {

                closestRow.after(`
                                <tr data-category='${filteredDepartmentList[v].CategoryName}'>
                                <td style='padding-left:40px !important;' data-subcategory='${filteredDepartmentList[v].SubCategoryName}'>${filteredDepartmentList[v].DepartmentName}</th>
                                <td class="text-right" data-isdepartment='yes'>${filteredDepartmentList[v].OctCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].NovCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].DecCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].JanCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].FebCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].MarCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].AprCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].MayCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].JunCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].JulCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].AugCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${filteredDepartmentList[v].SepCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
            }
        }

        $(this).removeClass('fa fa-plus expand-count-inner');
        $(this).addClass('fa fa-minus expand-count-inner-close');


    });

    $(document).on('click', '.expand-count-inner-close', function () {
        var closestRow = $(this).closest('tr');
        var categoryName = $(closestRow).attr('data-category');
        var subCategoryName = $(closestRow[0].cells[0]).attr('data-subcategory');

            $('#headcount_table tr').each(function (index, value) {
                if (index > 0) {
                    var category = $(value).attr('data-category');
                    var subCategory = $(value.cells[0]).attr('data-subcategory');
                    var isdepartment = $(value.cells[1]).attr('data-isdepartment');

                    if ((category == categoryName) && (subCategory == subCategoryName) && (isdepartment=='yes')) {
                        $(value).remove();
                    }
                }

            });


        $(this).removeClass('fa fa-minus expand-count-inner-close');
        $(this).addClass('fa fa-plus expand-count-inner');



    });

    $(document).on('click', '.expand-count', function () {
        var closestRow = $(this).closest('tr');
        var categoryName = $(closestRow).attr('data-category');
        
        var uniqueSubCategoryList = [];
        for (var s = 0; s < headCountList.length; s++) {
            if (headCountList[s].CategoryName == categoryName) {
                if (uniqueSubCategoryList.length == 0) {
                    uniqueSubCategoryList.push(headCountList[s].SubCategoryName.toString());
                }
                else {
                    if (!uniqueSubCategoryList.includes(headCountList[s].SubCategoryName.toString())) {
                        uniqueSubCategoryList.push(headCountList[s].SubCategoryName.toString());
                    }
                }
            }
           
        }
        //console.log(uniqueSubCategoryList);
        if (uniqueSubCategoryList.length > 0) {
            //debugger;
            let _octCount = 0, _novCount = 0, _decCount = 0, _janCount = 0, _febCount = 0, _marCount = 0, _aprCount = 0, _mayCount = 0, _junCount = 0, _julCount = 0, _augCount = 0, _sepCount = 0;
            for (var q = 0; q < uniqueSubCategoryList.length; q++) {
                _octCount = 0, _novCount = 0, _decCount = 0, _janCount = 0, _febCount = 0, _marCount = 0, _aprCount = 0, _mayCount = 0, _junCount = 0, _julCount = 0, _augCount = 0, _sepCount = 0;
                for (var r = 0; r < headCountList.length; r++) {
                    if ((headCountList[r].SubCategoryName.toString() == uniqueSubCategoryList[q]) && (headCountList[r].CategoryName.toString() == categoryName)) {
                        _octCount += parseFloat(headCountList[r].OctCount);
                        _novCount += parseFloat(headCountList[r].NovCount);
                        _decCount += parseFloat(headCountList[r].DecCount);
                        _janCount += parseFloat(headCountList[r].JanCount);
                        _febCount += parseFloat(headCountList[r].FebCount);
                        _marCount += parseFloat(headCountList[r].MarCount);
                        _aprCount += parseFloat(headCountList[r].AprCount);
                        _mayCount += parseFloat(headCountList[r].MayCount);
                        _junCount += parseFloat(headCountList[r].JunCount);
                        _julCount += parseFloat(headCountList[r].JulCount);
                        _augCount += parseFloat(headCountList[r].AugCount);
                        _sepCount += parseFloat(headCountList[r].SepCount);
                    }
                }

                closestRow.after(`
                                <tr data-category='${categoryName}'>
                                <td style='padding-left:20px !important;' data-subcategory='${uniqueSubCategoryList[q]}'><i class="fa fa-plus expand-count-inner" aria-hidden="true"></i> ${uniqueSubCategoryList[q]}</th>
                                <td class="text-right">${_octCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepCount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
            }

        }
        
        
        $(this).removeClass('fa fa-plus expand-count');
        $(this).addClass('fa fa-minus expand-count-close');

    });

    $(document).on('click', '.expand-count-close', function () {
        var closestRow = $(this).closest('tr');
        var categoryName = $(closestRow).attr('data-category');

        var uniqueSubCategoryList = [];
        for (var w = 0; w < headCountList.length; w++) {
            if (headCountList[w].CategoryName == categoryName) {
                if (uniqueSubCategoryList.length == 0) {
                    uniqueSubCategoryList.push(headCountList[w].SubCategoryName.toString());
                }
                else {
                    if (!uniqueSubCategoryList.includes(headCountList[w].SubCategoryName.toString())) {
                        uniqueSubCategoryList.push(headCountList[w].SubCategoryName.toString());
                    }
                }
            }
        }

        for (var z = 0; z < uniqueSubCategoryList.length; z++) {
            $('#headcount_table tr').each(function (index, value) {
                if (index > 0) {
                    var category = $(value).attr('data-category');
                    var subCategory = $(value.cells[0]).attr('data-subcategory');

                    if ((category == categoryName) && (subCategory == uniqueSubCategoryList[z])) {
                        $(value).remove();
                    }
                }

            });
        }

        $(this).removeClass('fa fa-minus expand-count-close');
        $(this).addClass('fa fa-plus expand-count');



    });

    $(document).on('click', '.expand', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);

        var departmentId = $(closestRow[0].cells[1]).attr('data-deptid');
        var identity = $(closestRow[0]).attr('data-indentity');


        if (identity == 1) {
            console.log(othersDepartmentTotal);
            closestRow.next().remove();
            $(closestRow[0].cells[0]).attr('rowspan', othersDepartmentTotal.length);
            $(closestRow[0].cells[1]).html(othersDepartmentTotal[0].DepartmentName);
            $(closestRow[0].cells[2]).html(othersDepartmentTotal[0].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(othersDepartmentTotal[0].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(othersDepartmentTotal[0].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(othersDepartmentTotal[0].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(othersDepartmentTotal[0].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(othersDepartmentTotal[0].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(othersDepartmentTotal[0].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(othersDepartmentTotal[0].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(othersDepartmentTotal[0].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(othersDepartmentTotal[0].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(othersDepartmentTotal[0].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(othersDepartmentTotal[0].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[14]).html(othersDepartmentTotal[0].RowTotal[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[15]).html(othersDepartmentTotal[0].FirstSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[16]).html(othersDepartmentTotal[0].SecondSlot[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

            for (var k = 1; k < othersDepartmentTotal.length; k++) {
                $('#total_table').append(`
                                <tr>
                                <td>${othersDepartmentTotal[k].DepartmentName}</td>
                                <td class="text-right">${othersDepartmentTotal[k].OctCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].NovCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].DecCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].JanCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].FebCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].MarCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].AprCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].MayCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].JunCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].JulCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].AugCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].SepCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].RowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].FirstSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${othersDepartmentTotal[k].SecondSlot.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
            }

            $('#total_table').append(`
                                <tr data-indentity='2'>
                                <td colspan="2" class="text-center">Total</td>
                                <td class="text-right">${_octTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_rowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_firstHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_secondHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);


        }
        else {

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

            $(closestRow[0].cells[1]).html(dataForExpandedRow[0].DepartmentName);
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

            $(closestRow[0].cells[0]).attr('rowspan', 3);

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

        }
        $(this).removeClass('fa fa-plus expand');
        $(this).addClass('fa fa-minus closed');

    });

    $(document).on('click', '.closed', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        
        $(this).removeClass('fa fa-minus closed');
        $(this).addClass('fa fa-plus expand');


        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);

        var departmentId = $(closestRow[0].cells[1]).attr('data-deptid');
        var identity = $(closestRow[0]).attr('data-indentity');

        if (identity == 1) {
            $(closestRow[0].cells[0]).attr('rowspan', '');
            $(closestRow[0].cells[1]).html('Total');
            $(closestRow[0].cells[2]).html(_octOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(_novOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(_decOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(_janOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(_febOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(_marOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(_aprOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(_mayOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(_junOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(_julOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(_augOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(_sepOtherTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[14]).html(_otherRowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[15]).html(_otherFisrtHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[16]).html(_otherSecondHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

            for (var i = 0; i < othersDepartmentTotal.length; i++) {
                closestRow.next().remove();
            }
            debugger;

            $('#total_table').append(`
                                <tr data-indentity='2'>
                                <td colspan="2" class="text-center">Total</td>
                                <td class="text-right">${_octTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_novTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_decTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_janTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_febTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_marTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_aprTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_mayTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_junTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_julTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_augTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_sepTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_rowTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_firstHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                <td class="text-right">${_secondHalf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);

        }
        else {
            $(closestRow[0].cells[0]).removeAttr('rowspan');
            closestRow.next().remove();
            closestRow.next().remove();

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

            $(closestRow[0].cells[1]).html('Total');
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

      
    });

    // $.ajax({
    //     // url: `/api/utilities/GetYearFromHistory`,
    //     //url: `/api/utilities/GetAssignmentYearList`,
    //     url: `/api/utilities/GetForecatYear`,
    //     contentType: 'application/json',
    //     type: 'GET',
    //     async: false,
    //     dataType: 'json',
    //     success: function (data) {
    //         $('#total_year_list').empty();
    //         $('#total_year_list').append('<option value="">年度データーの選択</option>');
    //         $.each(data, function (index, value) {
    //             $('#total_year_list').append(`<option value="${value}">${value}</option>`);
    //         });
    //     }
    // });

    //cascading edit history time stamp dropdown
    $(document).on('change', '#total_year_list', function () {
        var year = $('#total_year_list').val();        
        $.ajax({
            url: `/api/utilities/GetTimeStamps`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: { year: year },
            success: function (data) {
                
                if (data == "" || data == null || data == undefined) {          
                    console.log("empty")          
                    $('#edit_history_time_stamp').empty().append('<option value="">タイムスタンプの選択</option>');      
                }else{
                    console.log("not empty")          
                    $('#edit_history_time_stamp').empty();
                    $('#edit_history_time_stamp').append('<option value="">タイムスタンプの選択</option>');                
                    $.each(data, function (index, element) {
                        $('#edit_history_time_stamp').append(`<option value="${element.Id}">${element.TimeStamp}</option>`);                    
                    });
                }                
            }
        });
    });   

    GetAllForecastYears();
    function GetAllForecastYears() {
        $.ajax({
            url: `/api/utilities/GetForecatYear`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                $('#total_year_list').append(`<option value=''>年度データーの選択</option>`);
                $.each(data, function (index, element) {
                    $('#total_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
                });
            }
        });
    }
});
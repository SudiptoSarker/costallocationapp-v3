var companyList = [];
var totalList = [];
var totalListForBudget = [];
var latestFiscalYear = 0;
var othersDepartmentTotal = [];
var othersDepartmentTotalForBudget = [];



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
                //console.log(totalList);
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


                    for (var l = 0; l < othersDepartmentTotal.length; l++) {
                        _octOtherTotal += parseFloat(othersDepartmentTotal[l].OctCost);
                        _novOtherTotal += parseFloat(othersDepartmentTotal[l].NovCost);
                        _decOtherTotal += parseFloat(othersDepartmentTotal[l].DecCost);
                        _janOtherTotal += parseFloat(othersDepartmentTotal[l].JanCost);
                        _febOtherTotal += parseFloat(othersDepartmentTotal[l].FebCost);
                        _marOtherTotal += parseFloat(othersDepartmentTotal[l].MarCost);
                        _otherFisrtHalf += _octOtherTotal + _novOtherTotal + _decOtherTotal + _janOtherTotal + _febOtherTotal + _marOtherTotal;
                        _aprOtherTotal += parseFloat(othersDepartmentTotal[l].AprCost);
                        _mayOtherTotal += parseFloat(othersDepartmentTotal[l].MayCost);
                        _junOtherTotal += parseFloat(othersDepartmentTotal[l].JunCost);
                        _julOtherTotal += parseFloat(othersDepartmentTotal[l].JulCost);
                        _augOtherTotal += parseFloat(othersDepartmentTotal[l].AugCost);
                        _sepOtherTotal += parseFloat(othersDepartmentTotal[l].SepCost);
                        _otherSecondHalf += _aprOtherTotal + _mayOtherTotal + _junOtherTotal + _julOtherTotal + _augOtherTotal + _sepOtherTotal;
                        _otherRowTotal += _otherFisrtHalf + _otherSecondHalf;
                    }

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
            $('#total_budget_table').empty();
            $('#total_budget_table').append(`<thead><tr><th>開発区分</th><th>費用</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${latestFiscalYear}計</th><th>上期</th><th>下期 </th></tr></thead>`);
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
                                <td>Total</th>
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
                                <td>Total</th>
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
                                <td>Total</th>
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
                                <td>Total</th>
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
                                <td>Total</th>
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
                        _otherFisrtHalfBudget += _octOtherTotalBudget + _novOtherTotalBudget + _decOtherTotalBudget + _janOtherTotalBudget + _febOtherTotalBudget + _marOtherTotalBudget;
                        _aprOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].AprCost);
                        _mayOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].MayCost);
                        _junOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].JunCost);
                        _julOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].JulCost);
                        _augOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].AugCost);
                        _sepOtherTotalBudget += parseFloat(othersDepartmentTotalForBudget[b].SepCost);
                        _otherSecondHalfBudget += _aprOtherTotalBudget + _mayOtherTotalBudget + _junOtherTotalBudget + _julOtherTotalBudget + _augOtherTotalBudget + _sepOtherTotalBudget;
                        _otherRowTotalBudget += _otherFisrtHalfBudget + _otherSecondHalfBudget;
                    }

                    $('#total_budget_table').append(`
                                <tr>
                                <td>その他 </th>
                                <td>Total</td>
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
                                <td colspan="2" class="text-center">Total</td>
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
        





    });

    $(document).on('click', '.expand', function () {
        var dataForExpandedRow = [];
        var closestRow = $(this).closest('tr');
        var companyValues = companies.getSelectedOptionsAsJson(includeDisabled = false);
        var companyArray = JSON.parse(companyValues);

        var departmentId = $(closestRow[0].cells[1]).attr('data-deptid');
        var identity = $(closestRow[0]).attr('data-indentity');


        if (identity == 1) {
            closestRow.next().remove();
            $(closestRow[0].cells[0]).attr('rowspan', othersDepartmentTotal.length);
            $(closestRow[0].cells[1]).html(othersDepartmentTotal[0].DepartmentName);
            $(closestRow[0].cells[2]).html(othersDepartmentTotal[0].OctCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[3]).html(othersDepartmentTotal[0].NovCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[4]).html(othersDepartmentTotal[0].DecCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[5]).html(othersDepartmentTotal[0].JanCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[6]).html(othersDepartmentTotal[0].FebCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[7]).html(othersDepartmentTotal[0].MarCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[8]).html(othersDepartmentTotal[0].AprCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[9]).html(othersDepartmentTotal[0].MayCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[10]).html(othersDepartmentTotal[0].JunCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[11]).html(othersDepartmentTotal[0].JulCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[12]).html(othersDepartmentTotal[0].AugCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $(closestRow[0].cells[13]).html(othersDepartmentTotal[0].SepCost[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
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



});
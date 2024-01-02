var methodList = [];
var tableRowList = [];

$(document).ready(function () {
    $('#company_list').multiselect({
        allSelectedText: '全て',
        maxHeight: 200,
        includeSelectAllOption: true,
        nonSelectedText: 'すべて選択',
        nSelectedText  : "選ばれた"
    })
        .multiselect('selectAll', true).multiselect('updateButtonText');
    GetCompanyList();

    var uniqueTableList = [];
    var selected_compannies = "";
    var selected_year = "";
    var timeStampId = "";

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
                    $('#edit_history_time_stamp').empty().append('<option value="">タイムスタンプの選択</option>');
                } else {
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


    // when search button is clicked.
    $('#show_dynamic_tables').on('click', () => {
        selected_compannies = $("#company_list").val();
        selected_year = $("#total_year_list").val();
        timeStampId = $("#edit_history_time_stamp").val();


        if (selected_compannies == "" || selected_compannies == null || selected_compannies == undefined) {
            alert("会社を選択してください");
        } else if (selected_year == "" || selected_year == null || selected_year == undefined) {
            alert("年を選択してください");
        }
        else if (timeStampId == "" || timeStampId == null || timeStampId == undefined) {
            alert("タイムスタンプを選択してください");
        }
        else {
            LoaderShow();
            methodList = [];
            $.ajax({
                url: `/api/utilities/GetMethodList/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                success: function (data) {
                    methodList = data;
                }
            });

            $.ajax({
                url: `/api/utilities/GetDynamicSettings/`,
                contentType: 'application/json',
                type: 'GET',
                async: true,
                dataType: 'json',
                success: function (data) {

                    CreateDynamicSettingTablesWithCalculations(data, selected_compannies, selected_year, timeStampId);
                    LoaderHide();
                    ColorNegativeValue();
                }
            });
        }
    });

    // click on main item expand icon.
    $(document).on('click', '.expand-main', function () {

        var tableId = $(this).closest('table').attr('id');
        var mainItemName = $(this).closest('tr').data('item');
        $(`#${tableId} tbody tr`).filter(function (index) {
            var returnFlag = false;
            var dataAttr = $(this).data('item');
            if (dataAttr.includes(mainItemName + "_") && !dataAttr.includes('&')) {
                returnFlag = true;
            }
            return returnFlag;
        }).show();

        $(this).removeClass('fa fa-plus expand-main');
        $(this).addClass('fa fa-minus close-main');

    });

    // click on main item close icon.
    $(document).on('click', '.close-main', function () {

        var tableId = $(this).closest('table').attr('id');
        var mainItemName = $(this).closest('tr').data('item');
        $(`#${tableId} tbody tr`).filter(function (index) {
            return $(this).data('item').includes(mainItemName + '_');
        }).hide();

        $(`#${tableId} tbody i.${mainItemName}subicon`).removeClass('fa fa-minus');
        $(`#${tableId} tbody i.${mainItemName}subicon`).addClass('fa fa-plus');

        $(this).removeClass('fa fa-minus close-main');
        $(this).addClass('fa fa-plus expand-main');


    });

    // click on sub item expand icon.
    $(document).on('click', '.expand-sub', function () {

        var tableId = $(this).closest('table').attr('id');
        var mainSubItemName = $(this).closest('tr').data('item');
        $(`#${tableId} tbody tr`).filter(function (index) {
            var returnFlag = false;
            var dataAttr = $(this).data('item');
            if (dataAttr == mainSubItemName + '_&') {
                returnFlag = true;
            }
            return returnFlag;
        }).show();

        $(this).removeClass('fa fa-plus expand-sub');
        $(this).addClass('fa fa-minus close-sub');

    });

    // click on sub item close icon.
    $(document).on('click', '.close-sub', function () {

        var tableId = $(this).closest('table').attr('id');
        var mainSubItemName = $(this).closest('tr').data('item');
        $(`#${tableId} tbody tr`).filter(function (index) {
            var returnFlag = false;
            var dataAttr = $(this).data('item');
            if (dataAttr == mainSubItemName + '_&') {
                returnFlag = true;
            }
            return returnFlag;
        }).hide();

        $(this).removeClass('fa fa-minus close-sub');
        $(this).addClass('fa fa-plus expand-sub');


    });
});

// loader show function
function LoaderShow() {
    $("#table_container").css("display", "none");
    $("#loading").css("display", "block");
}
// loaded hide function
function LoaderHide() {
    $("#table_container").css("display", "block");
    $("#loading").css("display", "none");
}

// get company list functin
function GetCompanyList() {
    $.ajax({
        url: `/api/Companies/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#company_list').empty();
            $.each(data, function (key, item) {
                $('#company_list').append(`<option value="${item.Id}">${item.CompanyName}</option>`);
            });
            $("#company_list").multiselect('destroy');
            $('#company_list').multiselect({
                allSelectedText: '全て',
                maxHeight: 200,
                includeSelectAllOption: true,
                nonSelectedText: 'すべて選択',
                nSelectedText  : "選ばれた"
            })
        }
    });
}

// making all the negetive value as red color function
function ColorNegativeValue() {
    var allTableData = $('#table_container table tbody td');
    $.each(allTableData, (index, value) => {
        if (value.innerText.includes('-')) {
            var colorValue = ReplaceComma(value.innerText);
            if ($.isNumeric(value.innerText)) {

                $(value).css('color', 'red');
            }

        }
    });
}

// replace comma from numbers.
function ReplaceComma(returnValue) {

    if (returnValue.includes(',')) {
        returnValue = returnValue.replace(',', '');
    }
    if (returnValue.includes(',')) {
        returnValue = returnValue.replace(',', '');
    }
    if (returnValue.includes(',')) {
        returnValue = returnValue.replace(',', '');
    }

    return returnValue;
}

// getting forcasted year function
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

// dynamic table will view into page by this method.
function CreateDynamicSettingTablesWithCalculations(dynamicSettings, selected_compannies, selected_year, timeStampId) {
    for (var i = 0; i < methodList.length; i++) {

        for (var j = 0; j < dynamicSettings.length; j++) {
            if (parseInt(methodList[i].Id) == parseInt(dynamicSettings[j].MethodId)) {
                dynamicSettings[j].MethodName = methodList[i].MethodName;
                dynamicSettings[j].Dependency = methodList[i].Dependency;
                dynamicSettings[j].Syntex = methodList[i].Syntex;
            }
        }
    }

    uniqueTableList = [];
    for (var m = 0; m < dynamicSettings.length; m++) {
        if (uniqueTableList.length == 0) {
            uniqueTableList.push(dynamicSettings[m].DynamicTableTitle + '_' + dynamicSettings[m].DynamicTableId);
        }
        else {
            if (!uniqueTableList.includes(dynamicSettings[m].DynamicTableTitle + '_' + dynamicSettings[m].DynamicTableId)) {
                uniqueTableList.push(dynamicSettings[m].DynamicTableTitle + '_' + dynamicSettings[m].DynamicTableId);
            }
        }
    }

    $('#table_container').empty();
    tableRowList = [];
    var titleColumnsCount = 0;
    for (var n = 0; n < uniqueTableList.length; n++) {
        var _splittedValue = uniqueTableList[n].split('_');
        titleColumnsCount = 0;


        $.ajax({
            url: `/api/utilities/GetTableWiseTotal?tableId=${_splittedValue[1]}&companiIds=${selected_compannies}&year=${selected_year}&timestampsId=${timeStampId}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (tableObject) {

                var tableInfo = tableObject.dynamicTable;
                var tableData = tableObject.data;

                if (tableInfo.CategoryTitle !== "") {
                    titleColumnsCount++;
                }
                if (tableInfo.SubCategoryTitle !== "") {
                    titleColumnsCount++;
                }
                if (tableInfo.DetailsTitle !== "") {
                    titleColumnsCount++;
                }

                $('#table_container').append(`<p class="font-weight-bold" style="margin-top:20px;"><u>${_splittedValue[0]}</u></p>`);
                $('#table_container').append(`<table id="table_${n}" class="generated_table" data-dt='${titleColumnsCount}'></table>`);
                if (titleColumnsCount == 1) {
                    $(`#table_${n}`).append(`<thead><tr><th>${tableInfo.CategoryTitle}</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${selected_year}計</th><th>上期</th><th>下期 </th></tr></thead><tbody></tbody>`);
                    var _tr = '';
                    $.each(tableData, (index, mainItem) => {
                        var direction = '';
                        var icon = '';
                        if (mainItem.MainItems.length > 0) {
                            icon = '<i class="fa fa-plus expand-main" aria-hidden="true"></i>';
                        }
                        if (mainItem.MethodId == 5 || mainItem.MethodId == 6 || mainItem.MethodId == 7 || mainItem.MethodId == 8 || mainItem.MethodId == 12 || mainItem.MethodId == 13 || mainItem.MethodId == 14 || mainItem.MethodId == 15) {
                            direction = 'text-center';
                        }
                        else {
                            direction = 'text-right';
                        }
                        _tr += `
                                    <tr data-item='${mainItem.MainItemName}'>
                                    <td>${icon} ${mainItem.MainItemName}</td>
                                    <td class='${direction}'>${Math.round(mainItem.OctVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.NovVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.DecVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JanVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.FebVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.MarVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.AprVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.MayVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JunVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JulVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.AugVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.SepVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.Total).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.FirstHalf).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.SecondHalf).toLocaleString('en-US')}</td>
                                    </tr>
                                    `;
                        if (mainItem.MainItems.length > 0) {
                            $.each(mainItem.MainItems, (index1, nestedMainItem) => {

                                if (nestedMainItem.MethodId == 5 || nestedMainItem.MethodId == 6 || nestedMainItem.MethodId == 7 || nestedMainItem.MethodId == 8 || nestedMainItem.MethodId == 12 || nestedMainItem.MethodId == 13 || nestedMainItem.MethodId == 14 || nestedMainItem.MethodId == 15) {
                                    direction = 'text-center';
                                }
                                else {
                                    direction = 'text-right';
                                }

                                _tr += `
                                    <tr class='hidden-row' data-item='${mainItem.MainItemName}_${mainItem.MainItemName}'>
                                    <td></td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.OctVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.NovVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.DecVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.JanVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.FebVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.MarVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.AprVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.MayVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.JunVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.JulVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.AugVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.SepVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.Total).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.FirstHalf).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(nestedMainItem.SecondHalf).toLocaleString('en-US')}</td>
                                    </tr>
                                    `;
                            });
                        }
                    });
                    $(`#table_${n} tbody`).empty();
                    $(`#table_${n} tbody`).append(_tr);

                }
                if (titleColumnsCount == 2) {
                    $(`#table_${n}`).append(`<thead><tr><th>${tableInfo.CategoryTitle}</th><th>${tableInfo.SubCategoryTitle}</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${selected_year}計</th><th>上期</th><th>下期 </th></tr></thead><tbody></tbody>`);
                    var _tr = '';
                    $.each(tableData, (index, mainItem) => {
                        var direction = '';
                        if (mainItem.MethodId == 5 || mainItem.MethodId == 6 || mainItem.MethodId == 7 || mainItem.MethodId == 8 || mainItem.MethodId == 12 || mainItem.MethodId == 13 || mainItem.MethodId == 14 || mainItem.MethodId == 15) {
                            direction = 'text-center';
                        }
                        else {
                            direction = 'text-right';
                        }
                        _tr += `
                                    <tr data-item='${mainItem.MainItemName}'>
                                    <td><i class="fa fa-plus expand-main" aria-hidden="true"></i> ${mainItem.MainItemName}</td>
                                    <td></td>
                                    <td class='${direction}'>${Math.round(mainItem.OctVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.NovVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.DecVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JanVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.FebVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.MarVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.AprVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.MayVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JunVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JulVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.AugVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.SepVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.Total).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.FirstHalf).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.SecondHalf).toLocaleString('en-US')}</td>
                                    </tr>
                                    `;
                        if (mainItem.SubItems != null) {
                            $.each(mainItem.SubItems, (index1, subItem) => {
                                if (subItem.MethodId == 5 || subItem.MethodId == 6 || subItem.MethodId == 7 || subItem.MethodId == 8 || subItem.MethodId == 12 || subItem.MethodId == 13 || subItem.MethodId == 14 || subItem.MethodId == 15) {
                                    direction = 'text-center';
                                }
                                else {
                                    direction = 'text-right';
                                }
                                _tr += `
                                    <tr class='hidden-row' data-item='${mainItem.MainItemName}_${subItem.SubItemName}'>
                                    <td></td>
                                    <td>${subItem.SubItemName}</td>
                                    <td class='${direction}'>${Math.round(subItem.OctVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.NovVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.DecVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.JanVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.FebVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.MarVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.AprVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.MayVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.JunVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.JulVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.AugVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.SepVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.Total).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.FirstHalf).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.SecondHalf).toLocaleString('en-US')}</td>
                                    </tr>
                                    `;
                            });
                        }
                    });
                    $(`#table_${n} tbody`).empty();
                    $(`#table_${n} tbody`).append(_tr);
                }
                if (titleColumnsCount == 3) {
                    $(`#table_${n}`).append(`<thead><tr><th>${tableInfo.CategoryTitle}</th><th>${tableInfo.SubCategoryTitle}</th><th>${tableInfo.DetailsTitle}</th><th>10月</th><th>11月</th><th>12月</th><th>1月</th><th>2月</th><th>3月</th><th>4月</th><th>5月</th><th>6月</th><th>7月</th><th>8月</th><th>9月</th><th>FY${selected_year}計</th><th>上期</th><th>下期 </th></tr></thead><tbody></tbody>`);

                    var _tr = '';
                    $.each(tableData, (index, mainItem) => {
                        var direction = '';
                        if (mainItem.MethodId == 5 || mainItem.MethodId == 6 || mainItem.MethodId == 7 || mainItem.MethodId == 8 || mainItem.MethodId == 12 || mainItem.MethodId == 13 || mainItem.MethodId == 14 || mainItem.MethodId == 15) {
                            direction = 'text-center';
                        }
                        else {
                            direction = 'text-right';
                        }
                        _tr += `
                                    <tr data-item='${mainItem.MainItemName}'>
                                    <td><i class="fa fa-plus expand-main" aria-hidden="true"></i> ${mainItem.MainItemName}</td>
                                    <td></td>
                                    <td></td>
                                    <td class='${direction}'>${Math.round(mainItem.OctVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.NovVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.DecVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JanVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.FebVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.MarVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.AprVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.MayVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JunVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.JulVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.AugVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.SepVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.Total).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.FirstHalf).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(mainItem.SecondHalf).toLocaleString('en-US')}</td>
                                    </tr>
                                    `;
                        if (mainItem.SubItems != null) {
                            $.each(mainItem.SubItems, (index1, subItem) => {
                                if (subItem.MethodId == 5 || subItem.MethodId == 6 || subItem.MethodId == 7 || subItem.MethodId == 8 || subItem.MethodId == 12 || subItem.MethodId == 13 || subItem.MethodId == 14 || subItem.MethodId == 15) {
                                    direction = 'text-center';
                                }
                                else {
                                    direction = 'text-right';
                                }
                                _tr += `
                                    <tr class='hidden-row' data-item='${mainItem.MainItemName}_${subItem.SubItemName}'>
                                    <td></td>
                                    <td><i class="fa fa-plus expand-sub ${mainItem.MainItemName}subicon" aria-hidden="true"></i> ${subItem.SubItemName}</td>
                                    <td></td>
                                    <td class='${direction}'>${Math.round(subItem.OctVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.NovVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.DecVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.JanVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.FebVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.MarVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.AprVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.MayVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.JunVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.JulVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.AugVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.SepVal).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.Total).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.FirstHalf).toLocaleString('en-US')}</td>
                                    <td class='${direction}'>${Math.round(subItem.SecondHalf).toLocaleString('en-US')}</td>
                                    </tr>
                                    `;


                                if (subItem.DetailsItems != null) {
                                    $.each(subItem.DetailsItems, (index1, detailsItem) => {
                                        if (detailsItem.MethodId == 5 || detailsItem.MethodId == 6 || detailsItem.MethodId == 7 || detailsItem.MethodId == 8 || detailsItem.MethodId == 12 || detailsItem.MethodId == 13 || detailsItem.MethodId == 14 || detailsItem.MethodId == 15) {
                                            direction = 'text-center';
                                        }
                                        else {
                                            direction = 'text-right';
                                        }
                                        _tr += `
                                                <tr class='hidden-row' data-item='${mainItem.MainItemName}_${subItem.SubItemName}_&'>
                                                <td></td>
                                                <td></td>
                                                <td>${detailsItem.DetailsItemName}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.OctVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.NovVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.DecVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.JanVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.FebVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.MarVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.AprVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.MayVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.JunVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.JulVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.AugVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.SepVal).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.Total).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.FirstHalf).toLocaleString('en-US')}</td>
                                                <td class='${direction}'>${Math.round(detailsItem.SecondHalf).toLocaleString('en-US')}</td>
                                                </tr>
                                                `;
                                    });
                                }


                            });
                        }
                    });

                    $(`#table_${n} tbody`).empty();
                    $(`#table_${n} tbody`).append(_tr);
                }

            }
        });

    }


}

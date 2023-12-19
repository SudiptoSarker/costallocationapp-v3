var methodList = [];
var tableRowList = [];
$(document).ready(function () {


    function LoaderShow() {
        $("#table_container").css("display", "none");
        $("#loading").css("display", "block");
    }
    function LoaderHide() {
        $("#table_container").css("display", "block");
        $("#loading").css("display", "none");
    }

    $('#company_list').multiselect({
        allSelectedText: 'All',
        maxHeight: 200,
        includeSelectAllOption: true
    })
    .multiselect('selectAll', true).multiselect('updateButtonText');
    GetCompanyList();    
    
    function GetCompanyList(){
        $.ajax({
            url: `/api/Companies/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                $('#company_list').empty();            
                $.each(data, function (key, item) {
                    //companyList.push(item);
                    $('#company_list').append(`<option value="${item.Id}">${item.CompanyName}</option>`);
                });
                $("#company_list").multiselect('destroy');
                $('#company_list').multiselect({
                    allSelectedText: 'All',
                    maxHeight: 200,
                    includeSelectAllOption: true
                })
                // .multiselect('selectAll', true).multiselect('updateButtonText');
            }
        });
    }
    var dynamicSettings = [];
    var uniqueTableList = [];
    var selected_compannies = "";
    var selected_year = "";
    var timeStampId = "";    
    
    function ReplaceComma_2(returnValue){
         //var returnValue  = 0;
         //returnValue = differenceValue;
        if(returnValue.includes(',')){
             returnValue = returnValue.replace(',', '');
         }
         if(returnValue.includes(',')){
             returnValue = returnValue.replace(',', '');
         }
         if(returnValue.includes(',')){
             returnValue = returnValue.replace(',', '');
         }        
        return returnValue;
     }

    function ColorNegativeValue_2(){
        var allTableData = $('#table_container table tbody td');
                    
        $.each(allTableData, (index,value) => {
            if (value.innerText.includes('-')) {
                //if (isNaN(value.innerText)) {
                var colorValue = ReplaceComma_2(value.innerText);                            
                if ($.isNumeric(colorValue)){                            
                    $(value).css('color', 'red');
                }
                
            }
        });
    }
    

    $('#show_dynamic_tables').on('click', () => {                
        selected_compannies = $("#company_list").val();
        selected_year = $("#total_year_list").val();
        timeStampId = $("#edit_history_time_stamp").val();        


        if (selected_compannies == "" || selected_compannies == null || selected_compannies == undefined) {
            alert("会社を選択してください");
        }else if(selected_year == "" || selected_year == null || selected_year == undefined){
            alert("年を選択してください");
        }
        else if(timeStampId == "" || timeStampId == null || timeStampId == undefined){
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
                    dynamicSettings = data;                    
                    
                    CreateDynamicSettingTablesWithCalculations();
                    LoaderHide();
                    ColorNegativeValue_2();
                }
            });            
        }   
    });

    function CreateDynamicSettingTablesWithCalculations(){
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
        var tableId = 0;
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
                    console.log(tableData);

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
                            var icon = '';
                            if (mainItem.MainItems.length > 0) {
                                icon = '<i class="fa fa-plus expand-main" aria-hidden="true"></i>';
                            }
                            _tr += `
                                    <tr data-item='${mainItem.MainItemName}'>
                                    <td>${icon} ${mainItem.MainItemName}</td>
                                    <td>${mainItem.OctVal}</td>
                                    <td>${mainItem.NovVal}</td>
                                    <td>${mainItem.DecVal}</td>
                                    <td>${mainItem.JanVal}</td>
                                    <td>${mainItem.FebVal}</td>
                                    <td>${mainItem.MarVal}</td>
                                    <td>${mainItem.AprVal}</td>
                                    <td>${mainItem.MayVal}</td>
                                    <td>${mainItem.JunVal}</td>
                                    <td>${mainItem.JulVal}</td>
                                    <td>${mainItem.AugVal}</td>
                                    <td>${mainItem.SepVal}</td>
                                    <td>${mainItem.Total}</td>
                                    <td>${mainItem.FirstHalf}</td>
                                    <td>${mainItem.SecondHalf}</td>
                                    </tr>
                                    `;
                            if (mainItem.MainItems.length > 0) {
                                $.each(mainItem.MainItems, (index1, nestedMainItem) => {
                                    _tr += `
                                    <tr class='hidden-row' data-item='${mainItem.MainItemName}_${mainItem.MainItemName}'>
                                    <td></td>
                                    <td>${nestedMainItem.OctVal}</td>
                                    <td>${nestedMainItem.NovVal}</td>
                                    <td>${nestedMainItem.DecVal}</td>
                                    <td>${nestedMainItem.JanVal}</td>
                                    <td>${nestedMainItem.FebVal}</td>
                                    <td>${nestedMainItem.MarVal}</td>
                                    <td>${nestedMainItem.AprVal}</td>
                                    <td>${nestedMainItem.MayVal}</td>
                                    <td>${nestedMainItem.JunVal}</td>
                                    <td>${nestedMainItem.JulVal}</td>
                                    <td>${nestedMainItem.AugVal}</td>
                                    <td>${nestedMainItem.SepVal}</td>
                                    <td>${nestedMainItem.Total}</td>
                                    <td>${nestedMainItem.FirstHalf}</td>
                                    <td>${nestedMainItem.SecondHalf}</td>
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
                            _tr += `
                                    <tr data-item='${mainItem.MainItemName}'>
                                    <td><i class="fa fa-plus expand-main" aria-hidden="true"></i> ${mainItem.MainItemName}</td>
                                    <td></td>
                                    <td>${mainItem.OctVal}</td>
                                    <td>${mainItem.NovVal}</td>
                                    <td>${mainItem.DecVal}</td>
                                    <td>${mainItem.JanVal}</td>
                                    <td>${mainItem.FebVal}</td>
                                    <td>${mainItem.MarVal}</td>
                                    <td>${mainItem.AprVal}</td>
                                    <td>${mainItem.MayVal}</td>
                                    <td>${mainItem.JunVal}</td>
                                    <td>${mainItem.JulVal}</td>
                                    <td>${mainItem.AugVal}</td>
                                    <td>${mainItem.SepVal}</td>
                                    <td>${mainItem.Total}</td>
                                    <td>${mainItem.FirstHalf}</td>
                                    <td>${mainItem.SecondHalf}</td>
                                    </tr>
                                    `;
                            if (mainItem.SubItems!=null) {
                                $.each(mainItem.SubItems, (index1,subItem) => {
                                    _tr += `
                                    <tr class='hidden-row' data-item='${mainItem.MainItemName}_${subItem.SubItemName}'>
                                    <td></td>
                                    <td>${subItem.SubItemName}</td>
                                    <td>${subItem.OctVal}</td>
                                    <td>${subItem.NovVal}</td>
                                    <td>${subItem.DecVal}</td>
                                    <td>${subItem.JanVal}</td>
                                    <td>${subItem.FebVal}</td>
                                    <td>${subItem.MarVal}</td>
                                    <td>${subItem.AprVal}</td>
                                    <td>${subItem.MayVal}</td>
                                    <td>${subItem.JunVal}</td>
                                    <td>${subItem.JulVal}</td>
                                    <td>${subItem.AugVal}</td>
                                    <td>${subItem.SepVal}</td>
                                    <td>${subItem.Total}</td>
                                    <td>${subItem.FirstHalf}</td>
                                    <td>${subItem.SecondHalf}</td>
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
                            _tr += `
                                    <tr data-item='${mainItem.MainItemName}'>
                                    <td><i class="fa fa-plus expand-main" aria-hidden="true"></i> ${mainItem.MainItemName}</td>
                                    <td></td>
                                    <td></td>
                                    <td>${mainItem.OctVal}</td>
                                    <td>${mainItem.NovVal}</td>
                                    <td>${mainItem.DecVal}</td>
                                    <td>${mainItem.JanVal}</td>
                                    <td>${mainItem.FebVal}</td>
                                    <td>${mainItem.MarVal}</td>
                                    <td>${mainItem.AprVal}</td>
                                    <td>${mainItem.MayVal}</td>
                                    <td>${mainItem.JunVal}</td>
                                    <td>${mainItem.JulVal}</td>
                                    <td>${mainItem.AugVal}</td>
                                    <td>${mainItem.SepVal}</td>
                                    <td>${mainItem.Total}</td>
                                    <td>${mainItem.FirstHalf}</td>
                                    <td>${mainItem.SecondHalf}</td>
                                    </tr>
                                    `;
                            if (mainItem.SubItems != null) {
                                $.each(mainItem.SubItems, (index1, subItem) => {
                                    _tr += `
                                    <tr class='hidden-row' data-item='${mainItem.MainItemName}_${subItem.SubItemName}'>
                                    <td></td>
                                    <td><i class="fa fa-plus expand-sub ${mainItem.MainItemName}subicon" aria-hidden="true"></i> ${subItem.SubItemName}</td>
                                    <td></td>
                                    <td>${subItem.OctVal}</td>
                                    <td>${subItem.NovVal}</td>
                                    <td>${subItem.DecVal}</td>
                                    <td>${subItem.JanVal}</td>
                                    <td>${subItem.FebVal}</td>
                                    <td>${subItem.MarVal}</td>
                                    <td>${subItem.AprVal}</td>
                                    <td>${subItem.MayVal}</td>
                                    <td>${subItem.JunVal}</td>
                                    <td>${subItem.JulVal}</td>
                                    <td>${subItem.AugVal}</td>
                                    <td>${subItem.SepVal}</td>
                                    <td>${subItem.Total}</td>
                                    <td>${subItem.FirstHalf}</td>
                                    <td>${subItem.SecondHalf}</td>
                                    </tr>
                                    `;


                                    if (subItem.DetailsItems != null) {
                                        $.each(subItem.DetailsItems, (index1, detailsItem) => {
                                            _tr += `
                                                <tr class='hidden-row' data-item='${mainItem.MainItemName}_${subItem.SubItemName}_&'>
                                                <td></td>
                                                <td></td>
                                                <td>${detailsItem.DetailsItemName}</td>
                                                <td>${detailsItem.OctVal}</td>
                                                <td>${detailsItem.NovVal}</td>
                                                <td>${detailsItem.DecVal}</td>
                                                <td>${detailsItem.JanVal}</td>
                                                <td>${detailsItem.FebVal}</td>
                                                <td>${detailsItem.MarVal}</td>
                                                <td>${detailsItem.AprVal}</td>
                                                <td>${detailsItem.MayVal}</td>
                                                <td>${detailsItem.JunVal}</td>
                                                <td>${detailsItem.JulVal}</td>
                                                <td>${detailsItem.AugVal}</td>
                                                <td>${detailsItem.SepVal}</td>
                                                <td>${detailsItem.Total}</td>
                                                <td>${detailsItem.FirstHalf}</td>
                                                <td>${detailsItem.SecondHalf}</td>
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

    $(document).on('click', '.close-main', function () {

        var tableId = $(this).closest('table').attr('id');
        var mainItemName = $(this).closest('tr').data('item');
        $(`#${tableId} tbody tr`).filter(function (index) {
            return $(this).data('item').includes(mainItemName+'_');
        }).hide();

        $(`#${tableId} tbody i.${mainItemName}subicon`).removeClass('fa fa-minus');
        $(`#${tableId} tbody i.${mainItemName}subicon`).addClass('fa fa-plus');

        $(this).removeClass('fa fa-minus close-main');
        $(this).addClass('fa fa-plus expand-main');
        

    });

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


    function GetDynamicCostTables(selected_compannies,selected_year,strTableType,timeStampId){        
        var strTotalTalbe = "";    
        if (strTableType == "" || strTableType == null || strTableType == undefined) {
            alert("invalid request!");
        }else{
            var apiurl = "";
            apiurl = "/api/Utilities/GetDynamicCostTalbes?companiIds=" + selected_compannies+"&year="+selected_year+"&strTableType="+strTableType+"&timestampsId="+timeStampId;
                        
            $.ajax({                
                url: apiurl,                
                type: 'GET',
                async: false,
                dataType: 'html',
                success: function (data) {
                    if (data == "" || data == null || data == undefined) {
                        alert("table not found")
                    }else{
                        strTotalTalbe = data;
                    }
                }
            });
        } 
        
        return strTotalTalbe
    }

    function GetHeadCount_Headerpart(selected_compannies,selected_year,strTableType,timeStampId){        
        var strTotalTalbe = "";    
        if (strTableType == "" || strTableType == null || strTableType == undefined) {
            alert("invalid request!");
        }else{
            var apiurl = "";
            apiurl = "/api/Utilities/GetHeadCount_Headerpart?companiIds=" + selected_compannies+"&year="+selected_year+"&strTableType="+strTableType+"&timestampsId="+timeStampId;
                        
            $.ajax({                
                url: apiurl,                
                type: 'GET',
                async: false,
                dataType: 'html',
                success: function (data) {
                    if (data == "" || data == null || data == undefined) {
                        alert("table not found")
                    }else{
                        strTotalTalbe = data;
                    }
                }
            });
        } 
        
        return strTotalTalbe
    }
    function GetTotalTableHeaderPart(main_header,sub_header,detial_header,tableTitle,year){
        var strTableHeader = "";            
        //strTableHeader = "<p class'font-weight-bold' id='p-total' style='margin-top:20px;'><u>" + tableTitle + ":</u></p>";
        strTableHeader = strTableHeader + "<thead>";
        strTableHeader = strTableHeader + "	<tr>";
        if (!string.IsNullOrEmpty(main_header))
        {
            strTableHeader = strTableHeader + "<th>"+ main_header + "</th>";
        }
        if (!string.IsNullOrEmpty(sub_header))
        {
            strTableHeader = strTableHeader + "<th>" + sub_header + "</th>";
        }
        if (!string.IsNullOrEmpty(detial_header))
        {
            strTableHeader = strTableHeader + "<th>" + detial_header + "</th>";
        }
        strTableHeader = strTableHeader + "		<th>10月</th>";
        strTableHeader = strTableHeader + "		<th>11月</th>";
        strTableHeader = strTableHeader + "		<th>12月</th>";
        strTableHeader = strTableHeader + "		<th>1月</th>";
        strTableHeader = strTableHeader + "		<th>2月</th>";
        strTableHeader = strTableHeader + "		<th>3月</th>";
        strTableHeader = strTableHeader + "		<th>4月</th>";
        strTableHeader = strTableHeader + "		<th>5月</th>";
        strTableHeader = strTableHeader + "		<th>6月</th>";
        strTableHeader = strTableHeader + "		<th>7月</th>";
        strTableHeader = strTableHeader + "		<th>8月</th>";
        strTableHeader = strTableHeader + "		<th>9月</th>";

        strTableHeader = strTableHeader + "		<th>FY$"+ year + "計</th>";
        strTableHeader = strTableHeader + "		<th>上期</th>";
        strTableHeader = strTableHeader + "		<th>下期 </th>";
        strTableHeader = strTableHeader + "	</tr>";
        strTableHeader = strTableHeader + "</thead>";

        return strTableHeader;
    }

    //get table title
    function GetDynamicTableTitleByPosition(tablePosition){
        var strTotalTalbe = "";        

        $.ajax({            
            url: "/api/Utilities/GetDynamicTableTitleByPosition?tablePosition=" + tablePosition,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                if (data == "" || data == null || data == undefined) {
                    alert("table not found")
                }else{
                    strTotalTalbe = data;                    
                }
            }
        });
        return strTotalTalbe
    }
    //company multi select: end
});


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
    
    function ReplaceComma(returnValue){
        //var returnValue  = 0;
        //returnValue = differenceValue;

        if(returnValue.includes(',')){
            returnValue = returnValue.replace(',', '');
        }
        if(returnValue.includes(',')){
            returnValue = returnValue.replace(',', '');
        }
        if(returnValue.includes(',')){
            returnValue = returnValue.replace(',', '');
        }        

        return returnValue;
    }

    function ColorNegativeValue(){
        var allTableData = $('#table_container table tbody td');
                    
        $.each(allTableData, (index,value) => {
            if (value.innerText.includes('-')) {
                //if (isNaN(value.innerText)) {
                var colorValue = ReplaceComma(value.innerText);                            
                if ($.isNumeric(colorValue)){                            
                    $(value).css('color', 'red');
                }
                
            }
        });
    }

    var companies;

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

        ColorNegativeValue();
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

        ColorNegativeValue();
        $(this).removeClass('fa fa-minus expand-count-inner-close');
        $(this).addClass('fa fa-plus expand-count-inner');



    });

    $(document).on('click', '.expand-count', function () {        
        var closestRow = $(this).closest('tr');
        var categoryName = $(closestRow).attr('data-category');
        var filteredRows = [];

        var splittedValue = categoryName.split('_');
        var dynamicTitleCount = parseInt(splittedValue[2]);
        $.each(tableRowList, (index,tableData) => {

            if (tableData.tableTitle == splittedValue[1]) {
                $.each(tableData.rowData, (index, row) => {
                    if (row.mainTitle == splittedValue[0]) {
                        filteredRows.push(row);
                    }
                });
            }
        });
        $(closestRow[0].cells[0]).attr('rowspan', filteredRows.length);
        for (var i = 1; i < filteredRows.length; i++) {
            if (dynamicTitleCount == 1) {

                closestRow.after(`
                                    <tr data-categiry='${categoryName}'>                                    
                                    <td class="text-right">${filteredRows[i].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                        
                                    
                                    <td class="text-right">${filteredRows[i].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                            
                                    <td class="text-right">${filteredRows[i].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                        
                                </tr>`);
            }
            if (dynamicTitleCount == 2) {
                closestRow.after(`
                                    <tr data-categiry='${categoryName}'>
                                    <td>${filteredRows[i].subTitle}</td>                                    
                                    <td class="text-right">${filteredRows[i].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                                                                                                                                
                                    <td class="text-right">${filteredRows[i].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                    <td class="text-right">${filteredRows[i].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                    <td class="text-right">${filteredRows[i].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                    <td class="text-right">${filteredRows[i].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                    <td class="text-right">${filteredRows[i].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                
                                    <td class="text-right">${filteredRows[i].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                    
                                    <td class="text-right">${filteredRows[i].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
            }
            if (dynamicTitleCount == 3) {
                closestRow.after(`
                                    <tr data-categiry='${categoryName}'>
                                    <td>${filteredRows[i].subTitle}</td>
                                    <td>${filteredRows[i].detailsTitle}</td>
                                    <td class="text-right">${Number(filteredRows[i].OctCost[2])}</td>
                                    <td class="text-right">${filteredRows[i].OctCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].NovCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].DecCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].JanCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].FebCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].MarCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].AprCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].MayCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].JunCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                
                                    <td class="text-right">${filteredRows[i].JulCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].AugCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].SepCost[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                    
                                    <td class="text-right">${filteredRows[i].RowTotal[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].FirstSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>                                    
                                    <td class="text-right">${filteredRows[i].SecondSlot[2].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
                                </tr>`);
            }
        }

        ColorNegativeValue();
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

        ColorNegativeValue();
        $(this).removeClass('fa fa-minus expand-count-close');
        $(this).addClass('fa fa-plus expand-count');
    });

    //$(document).on('click', '.expand', function () {
    //    var closestRow = $(this).closest('tr');
    //    var categoryName = $(closestRow).attr('data-category');
    //    var filteredRows = [];

    //    var splittedValue = categoryName.split('_');
    //    var dynamicTitleCount = parseInt(splittedValue[2]);
    //    $.each(tableRowList, (index, tableData) => {

    //        if (tableData.tableTitle == splittedValue[1]) {
    //            $.each(tableData.rowData, (index, row) => {
    //                if (row.mainTitle == splittedValue[0]) {
    //                    filteredRows.push(row);
    //                }
    //            });
    //        }
    //    });
    //    $(closestRow[0].cells[0]).attr('rowspan', filteredRows.length);
    //    for (var i = 1; i < filteredRows.length; i++) {
    //        if (dynamicTitleCount == 1) {

    //            closestRow.after(`
    //                                <tr data-categiry='${categoryName}'>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].OctCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].NovCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].DecCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].JanCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].FebCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].MarCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].AprCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].MayCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].JunCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].JulCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].AugCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].SepCost[2]).toLocaleString('en-US')}</td>
                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].RowTotal[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].FirstSlot[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].SecondSlot[2]).toLocaleString('en-US')}</td>
    //                            </tr>`);
    //        }
    //        if (dynamicTitleCount == 2) {
    //            closestRow.after(`
    //                                <tr data-categiry='${categoryName}'>
    //                                <td>${filteredRows[i].subTitle}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].OctCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].NovCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].DecCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].JanCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].FebCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].MarCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].AprCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].MayCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].JunCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].JulCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].AugCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].SepCost[2]).toLocaleString('en-US')}</td>                                    
                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].RowTotal[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].FirstSlot[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].SecondSlot[2]).toLocaleString('en-US')}</td>
    //                            </tr>`);
    //        }
    //        if (dynamicTitleCount == 3) {
    //            closestRow.after(`
    //                                <tr data-categiry='${categoryName}'>
    //                                <td>${filteredRows[i].subTitle}</td>
    //                                <td>${filteredRows[i].detailsTitle}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].OctCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].NovCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].DecCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].JanCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].FebCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].MarCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].AprCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].MayCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].JunCost[2]).toLocaleString('en-US')}</td>
    //                                <td class="text-right">${Math.round(filteredRows[i].JulCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].AugCost[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].SepCost[2]).toLocaleString('en-US')}</td>                                    
                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].RowTotal[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].FirstSlot[2]).toLocaleString('en-US')}</td>                                    
    //                                <td class="text-right">${Math.round(filteredRows[i].SecondSlot[2]).toLocaleString('en-US')}</td>
    //                            </tr>`);
    //        }
    //    }

    //    ColorNegativeValue();
        
    //    $(this).removeClass('fa fa-plus expand');
    //    $(this).addClass('fa fa-minus closed');

    //});

    //$(document).on('click', '.closed', function () {
    //    $(this).removeClass('fa fa-minus closed');
    //    $(this).addClass('fa fa-plus expand');


    //    var closestRow = $(this).closest('tr');
    //    var categoryName = $(closestRow).attr('data-category');
    //    var filteredRows = [];

    //    var splittedValue = categoryName.split('_');
    //    var dynamicTitleCount = parseInt(splittedValue[2]);
    //    $.each(tableRowList, (index, tableData) => {

    //        if (tableData.tableTitle == splittedValue[1]) {
    //            $.each(tableData.rowData, (index, row) => {
    //                if (row.mainTitle == splittedValue[0]) {
    //                    filteredRows.push(row);
    //                }
    //            });
    //        }
    //    });
    //    var rowSpanNumber = $(closestRow[0].cells[0]).attr('rowspan');

    //    $(closestRow[0].cells[0]).attr('rowspan', '');

    //    for (var i = 1; i < rowSpanNumber; i++) {
    //        closestRow.next().remove();
    //    }




      
    //});

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
                    $('#edit_history_time_stamp').empty().append('<option value="">タイムスタンプの選択</option>');      
                }else{
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
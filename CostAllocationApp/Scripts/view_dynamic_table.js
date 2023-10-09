$(document).ready(function () {    
    $(".data_for_dropdown").select2();    
    //get table lsit and show it to the dropdown
    GetDynamicTables();    

    function GetDynamicTables() {
        $.getJSON('/api/Utilities/GetDynamicTables')
            .done(function (data) {           
                $('#table_list').empty();
                $('#table_list').append(`<option value=''>クリックして集計表を選択</option>`);
                $.each(data, function (key, item) {
                    $('#table_list').append(`<option value='${item.Id}'>${item.TableName}</option>`);
                });
            });
    }    
    function GetDynamicTablesByTableId(tableId){
        var tableMainItemTitle="";
        var tableSubItemTitle = "";
        var tableDetialItemTitle = "";
        var resultsItem = "";

        $.ajax({
            url: `/api/utilities/GetDynamicTableById/${tableId}`,
            type: 'Get',
            async: false,
            dataType: 'json',
            success: function (data) {
                if (data.CategoryTitle != '' && data.CategoryTitle != null && data.CategoryTitle != undefined) {
                    tableMainItemTitle =  data.CategoryTitle;     
                    resultsItem = tableMainItemTitle;
                    if (data.SubCategoryTitle != '' && data.SubCategoryTitle != null && data.SubCategoryTitle != undefined) {
                        tableSubItemTitle =  data.SubCategoryTitle;       
                        resultsItem = resultsItem+"##"+tableSubItemTitle;
                        if (data.DetailsTitle != '' && data.DetailsTitle != null && data.DetailsTitle != undefined){
                            tableDetialItemTitle = data.DetailsTitle;
                            resultsItem = resultsItem+"##"+tableDetialItemTitle;
                        }                        
                    }                                  
                }                
            },
            error: function (data) {
            }
        });

        return resultsItem;
    }
    //show table details
    $(document).on('click', '#search_dynamic_table ', function () {
        var tableId = $("#table_list").val();
        if (tableId == '' || tableId == null || tableId == undefined) {
            $(".total_menu_list_tbl").hide();  
            alert('テーブルを選択してください!!!');        
            return false;
        }else{ 
            $('#total_menu_list_tbody').empty();    
            var resultsItem = GetDynamicTablesByTableId(tableId);
            GetDynamicSettings(tableId,resultsItem);
            $('.data_for_dropdown').select2();
            $(".total_menu_list_tbl").show();              
        }
    });
    
    //get dynamic table settings by table id
    function GetDynamicSettings(dynamicTableId,resultsItem) {            
        $.ajax({
            url: `/api/utilities/GetDynamicSettingsByDynamicTableId?dynamicTableId=${dynamicTableId}`,
            type: 'Get',
            dataType: 'json',
            success: function (data) {         
                var tableheaderStartHtml = "<tr>"       
                var tableheaderEndHtml = "</tr>"
                var tableheaderHtml = ""

                var arrResultsItem = resultsItem.split('##');
                //alert(resultsItem);
                //alert(arrResultsItem.length);
                //return false;
                var count =1;                
                if(parseInt(data.length)>0){                        
                    $.each(data, function (key, item) {
                        var startTR = "";
                        var endTR = "";
                        var checkItem = "";
                        var mainItem = "";
                        var subItem = "";
                        var detailItem = "";
                        var methodList = "";
                        var dataForList = "";
                        var totalListItem = "";                        
                        var settingColCount = 0;

                        $("#main_item_settings").val('');
                        $("#sub_item_settings").val('');
                        $("#detail_item_settings").val('');

                        
                        startTR = "<tr data-count='1' class='setting_tbl_tr'>";
                        endTR = "</tr>";        
                        if (item.Id != '' && item.Id != null && item.Id != undefined){
                            settingColCount = parseInt(settingColCount)+1;

                            if(parseInt(count) ==1){
                                tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>選択​</th>";
                            }                            
                            checkItem = `<td class='setting_items_td'><input  type='checkbox' value='${item.Id}' class='setting_tbl_chk'></td>`;                                               
                        }

                        var tempCatId = 0;
                        if (arrResultsItem[0] != '' && arrResultsItem[0] != null && arrResultsItem[0] != undefined){                            
                            if (item.CategoryId != '' && item.CategoryId != null && item.CategoryId != undefined){
                                tempCatId = item.CategoryId;
                            }

                            settingColCount = parseInt(settingColCount)+1;
                            $("#main_item_settings").val(1);
                            if(parseInt(count) ==1){                                
                                tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>"+arrResultsItem[0]+"</th>";
                            }                            
                            mainItem = GetMainItemList(tempCatId);
                        }

                        var tempSubCatId = 0;
                        if (arrResultsItem[1] != '' && arrResultsItem[1] != null && arrResultsItem[1] != undefined){                            
                            if (item.SubCategoryId != '' && item.SubCategoryId != null && item.SubCategoryId != undefined){
                                tempSubCatId = item.SubCategoryId;
                            }

                            settingColCount = parseInt(settingColCount)+1;
                            $("#sub_item_settings").val(1);
                            if(parseInt(count) ==1){
                                tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>"+arrResultsItem[1]+"</th>";
                            }                            
                            subItem = GetSubItemList(tempCatId,tempSubCatId);  
                        }                             

                        var tempDetailId = 0;
                        if (arrResultsItem[2] != '' && arrResultsItem[2] != null && arrResultsItem[2] != undefined){
                            if (item.DetailsId != '' && item.DetailsId != null && item.DetailsId != undefined){
                                tempDetailId = item.DetailsId;
                            }

                            $("#detail_item_settings").val(1);
                            settingColCount = parseInt(settingColCount)+1;
                            if(parseInt(count) == 1){                                
                                tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>"+arrResultsItem[2]+"</th>";
                            }                            
                            detailItem = GetDetailItemList(tempSubCatId,tempDetailId);   
                        }                        
                        
                        methodList = GetMethodList(item.MethodId,count);
                                            
                        dataForList = dataForList +" <td class='setting_items_td data_for'>";                             
                        dataForList = dataForList +"<select class='data_for_dropdown' multiple='multiple' id='data_for_id_"+count+"'>";                                            
                        dataForList = dataForList +"</select>   ";                      
                        dataForList = dataForList +" </td>";
                        
                        var dependency = "";
                        if(parseInt(item.MethodId) == 1 || parseInt(item.MethodId)==3 || parseInt(item.MethodId)==5 || parseInt(item.MethodId)==6 || parseInt(item.MethodId)==8){
                            dependency = "dp";
                        }else{
                            dependency = "in";
                        }                                        
                        totalListItem = ""        
                        totalListItem = startTR+""+checkItem+""+mainItem+""+subItem+""+detailItem+""+methodList+""+dataForList+""+endTR;
                        
                        if(parseInt(count) == 1){
                            settingColCount = parseInt(settingColCount)+2;
                            tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>集計方式​(methods)</th>";
                            tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>集計データ​(data)</th>";

                            tableheaderHtml = tableheaderStartHtml+""+tableheaderHtml+""+tableheaderEndHtml;
                            $('#total_menu_list_thead').empty().append(`${tableheaderHtml}`);  
                            $("#setting_column_count").val(settingColCount);
                        }

                        $('#total_menu_list_tbody').append(`${totalListItem}`);  
                        
                        var optionDataFor = "";
                        optionDataFor = DataForDropdown(item.ParameterId,dependency,count);                    
                        $('.setting_items_td').find('#data_for_id_'+count).empty().append(optionDataFor);
                        $(".data_for_dropdown").select2();
                        
                        count = count +1;
                    });
                }
                else{ 
                    var startTR = "";
                    var endTR = "";
                    var checkItem = "";
                    var mainItem = "";
                    var subItem = "";
                    var detailItem = "";
                    var methodList = "";
                    var dataForList = "";
                    var totalListItem = "";                        
                    var settingColCount = 0;

                    $("#main_item_settings").val('');
                    $("#sub_item_settings").val('');
                    $("#detail_item_settings").val('');

                    
                    startTR = "<tr data-count='1' class='setting_tbl_tr'>";
                    endTR = "</tr>";
                    tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>選択​</th>";        
                    checkItem = `<td class='setting_items_td'><input  type='checkbox' value='' class='setting_tbl_chk'></td>`;   
                    settingColCount = parseInt(settingColCount)+1;                                                                

                    var tempCatId = 0;
                    if (arrResultsItem[0] != '' && arrResultsItem[0] != null && arrResultsItem[0] != undefined){                            
                        settingColCount = parseInt(settingColCount)+1;
                        tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>"+arrResultsItem[0]+"</th>";
                        $("#main_item_settings").val(1);                                                    
                        mainItem = GetMainItemList(tempCatId);
                    }

                    var tempSubCatId = 0;
                    if (arrResultsItem[1] != '' && arrResultsItem[1] != null && arrResultsItem[1] != undefined){                                                    
                        settingColCount = parseInt(settingColCount)+1;                        
                        tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>"+arrResultsItem[1]+"</th>";                            
                        $("#sub_item_settings").val(1);
                        subItem = GetSubItemList(tempCatId,tempSubCatId);  
                    }                             

                    var tempDetailId = 0;
                    if (arrResultsItem[2] != '' && arrResultsItem[2] != null && arrResultsItem[2] != undefined){                        
                        $("#detail_item_settings").val(1);
                        settingColCount = parseInt(settingColCount)+1;
                        tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>"+arrResultsItem[2]+"</th>";                            
                        detailItem = GetDetailItemList(tempSubCatId,tempDetailId);   
                    }                        
                    var tempMethodId=0;
                    methodList = GetMethodList(tempMethodId,count);
                                        
                    dataForList = dataForList +" <td class='setting_items_td data_for'>";                             
                    dataForList = dataForList +"<select class='data_for_dropdown' multiple='multiple' id='data_for_id_"+count+"'>";                                            
                    dataForList = dataForList +"</select>   ";                      
                    dataForList = dataForList +" </td>";
                    
                    var dependency = "";                                                            
                    totalListItem = ""        
                    totalListItem = startTR+""+checkItem+""+mainItem+""+subItem+""+detailItem+""+methodList+""+dataForList+""+endTR;
                    
                    settingColCount = parseInt(settingColCount)+2;
                    tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>集計方式​(methods)</th>";
                    tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>集計データ​(data)</th>";

                    tableheaderHtml = tableheaderStartHtml+""+tableheaderHtml+""+tableheaderEndHtml;
                    $('#total_menu_list_thead').empty().append(`${tableheaderHtml}`);  
                    $("#setting_column_count").val(settingColCount);

                    $('#total_menu_list_tbody').append(`${totalListItem}`);  
                    
                    var optionDataFor = "";
                    $('.setting_items_td').find('#data_for_id_'+count).empty().append(optionDataFor);
                    $(".data_for_dropdown").select2(); 
                                       
                    // var startTR = "";
                    // var endTR = "";
                    // var checkItem = "";
                    // var mainItem = "";
                    // var subItem = "";
                    // var detailItem = "";
                    // var methodList = "";
                    // var dataForList = "";
                    // var totalListItem = "";
                    // tableheaderHtml = "";

                    // tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>選択​</th>";
                    // tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>大項目​(main)</th>";
                    // tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>中項目​(sub)</th>";
                    // tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>小項目​(detail)</th>";
                    // tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>集計方式​(methods)</th>";
                    // tableheaderHtml = tableheaderHtml +"<th class='total_tbl_header'>集計データ​(data)</th>";

                    // tableheaderHtml = tableheaderStartHtml+""+tableheaderHtml+""+tableheaderEndHtml;
                    // $('#total_menu_list_thead').empty().append(`${tableheaderHtml}`);                      
                    // $("#setting_column_count").val(6);

                    // $("#main_item_settings").val(1);
                    // $("#sub_item_settings").val(1);
                    // $("#detail_item_settings").val(1);

                    // startTR = "<tr data-count='1' class='setting_tbl_tr'>";
                    // endTR = "</tr>";        
                    
                    // checkItem = `<td class='setting_items_td'><input  type='checkbox' value='' class='setting_tbl_chk'></td>`;                       
                    // mainItem = GetMainItemList(0);                 
                    // subItem = GetSubItemList(0,0);                            
                    // detailItem = GetDetailItemList(0,0); 
                    // methodList = GetMethodList(0,1);
                                        
                    // dataForList = dataForList +" <td class='setting_items_td data_for'>";                             
                    // dataForList = dataForList +"<select class='data_for_dropdown' multiple='multiple' id='data_for_id_"+count+"'>";                                            
                    // dataForList = dataForList +"</select>   ";                      
                    // dataForList = dataForList +" </td>";
                    
                    // var dependency = "";                                                         
                    // totalListItem = ""        
                    // totalListItem = startTR+""+checkItem+""+mainItem+""+subItem+""+detailItem+""+methodList+""+dataForList+""+endTR;
                    // $('#total_menu_list_tbody').append(`${totalListItem}`);  
                    
                    // var optionDataFor = "";
                    // $('.setting_items_td').find('#data_for_id_'+count).empty().append(optionDataFor);
                    // $(".data_for_dropdown").select2();                                        
                }
            },
            error: function (data) {
            }
        });
    }

    //get main item list
    function GetMainItemList(mainItemId){        
        var tableId = $("#table_list").val();
        var mainItem = "";
        $.ajax({
            url: `/api/utilities/GetCategories?dynamicTableId=${tableId}`,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                
                mainItem = mainItem +" <td class='setting_items_td'>";
                mainItem = mainItem +"    <select class='main_item_dropdown'>";
                mainItem = mainItem +"      <option value=''>設定する大項目を入力</option>";
                $.each(data, function (key, item) {
                    if(parseInt(mainItemId) == parseInt(item.Id)){
                        mainItem = mainItem +`<option value='${item.Id}' selected>${item.CategoryName}</option>`;
                    }else{
                        mainItem = mainItem +`<option value='${item.Id}'>${item.CategoryName}</option>`;
                    }                   
                });
                mainItem = mainItem +"    </select>";
                mainItem = mainItem +" </td>"; 
                
                
            },
            error: function (data) {
            }
        });

        return mainItem;
    }

    //get sub item list
    function GetSubItemList(categoryId,subCategoryId){
        var subItem = "";
        if(parseInt(categoryId)==0){
            subItem = subItem +" <td class='setting_items_td'>";
            subItem = subItem +"    <select class='sub_item_dropdown'> ";
            subItem = subItem +"    </select>";
            subItem = subItem +" </td>";
        }else{
            $.ajax({
                url: `/api/utilities/GetSubCategoriesByCategory?categoryId=${categoryId}`,
                type: 'Get',
                dataType: 'json',
                async: false,
                success: function (data) { 
                    if(parseInt(subCategoryId) ==0)  {
                        subItem = subItem +"      <option value=''>select sub item</option>";                                                                   
                    }else{
                        subItem = subItem +" <td class='setting_items_td'>";
                        subItem = subItem +"    <select class='sub_item_dropdown'> ";
                        subItem = subItem +"      <option value=''>select sub item</option>";                                                                   
                    }               
                    
                    $.each(data, function (key, item) { 
                        if(subCategoryId ==0)  {
                            subItem = subItem +`<option value='${item.Id}'>${item.SubCategoryName}</option>`;
                        }else{
                            if (subCategoryId != '' && subCategoryId != null && subCategoryId != undefined){
                                if(parseInt(subCategoryId) == parseInt(item.Id)){
                                    subItem = subItem +`<option value='${item.Id}' selected>${item.SubCategoryName}</option>`;
                                }else{
                                    subItem = subItem +`<option value='${item.Id}'>${item.SubCategoryName}</option>`;
                                }                         
                            }
                        }                    
                    });  
                    if(parseInt(subCategoryId) !=0)  {                    
                        subItem = subItem +"    </select>";
                        subItem = subItem +" </td>";                                                                
                    } 
                            
                },
                error: function (data) {
                }
            });
        }
        return subItem;
    }

    //get sub item list
    function GetDetailItemList(subItemId,detailItemId){        
        var detailItem = "";       
        if(parseInt(subItemId)==0){
            detailItem = detailItem +" <td class='setting_items_td'>";
            detailItem = detailItem +"    <select class='detail_item_dropdown'> ";
            detailItem = detailItem +"    </select>";
            detailItem = detailItem +" </td>"; 
        }else{        
            $.ajax({
                url: `/api/utilities/GetDetailsItemBySubItemsId?subItemId=${subItemId}`,
                type: 'Get',
                dataType: 'json',
                async: false,
                success: function (data) {                                
                    if(parseInt(detailItemId) ==0)  {
                        detailItem = detailItem +"      <option value=''>select detail item</option>";                                                                  
                    }else{
                        detailItem = detailItem +" <td class='setting_items_td'>";
                        detailItem = detailItem +"    <select class='detail_item_dropdown'> ";
                        detailItem = detailItem +"      <option value=''>select detail item</option>";                                                                    
                    }    

                    $.each(data, function (key, item) {   
                        if(parseInt(detailItemId) ==0){
                            detailItem = detailItem +`<option value='${item.Id}'>${item.DetailsItemName}</option>`;
                        }else{
                            if (detailItemId != '' && detailItemId != null && detailItemId != undefined){
                                if(parseInt(detailItemId) == parseInt(item.Id)){
                                    detailItem = detailItem +`<option value='${item.Id}' selected>${item.DetailsItemName}</option>`;
                                }else{
                                    detailItem = detailItem +`<option value='${item.Id}'>${item.DetailsItemName}</option>`;
                                }                         
                            }  
                        }
                                                        
                    });  
                    if(parseInt(detailItemId) !=0){
                        detailItem = detailItem +"    </select>";
                        detailItem = detailItem +" </td>";               
                    }                
                },
                error: function (data) {
                }
            });
        }
        return detailItem;
    }

    //get method list
    function GetMethodList(methodId,rowCount){
        var methodList = "";        
        $.ajax({
            url: '/api/utilities/GetMethodList/',
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                methodList = methodList +" <td class='setting_items_td'>";
                methodList = methodList +"    <select class='method_dropdown'> ";
                methodList = methodList +"      <option value=''>select method</option>";
                $.each(data, function (key, item) {
                    if(parseInt(methodId) == parseInt(item.Id)){
                        methodList = methodList +`<option value='${item.Id}'data-dependency=${item.Dependency} row-count=${rowCount} selected>${item.MethodName}</option>`;
                    }else{
                        methodList = methodList +`<option value='${item.Id}'data-dependency=${item.Dependency} row-count=${rowCount} >${item.MethodName}</option>`;
                    }                      
                });
                methodList = methodList +"    </select>";
                methodList = methodList +" </td>";
            },
            error: function (data) {
            }
        }); 
        return methodList;
    }

    function DataForDropdown(parameterIds,dependency,indexCount){       
        var dataForList = "";
        // pull data for dependency
        var arrParameters = parameterIds.split(",");    
        if (dependency == "dp") {
            $.ajax({
                url: `/api/Departments`,
                type: 'Get',
                async:false,
                dataType: 'json',
                success: function (data) {                                                         
                    $.each(data, function (key, item) {    
                        var isDepartmentSelected = false;

                        for (i = 0; i < arrParameters.length; ++i) {
                            if(parseInt(item.Id) == parseInt(arrParameters[i])){
                                isDepartmentSelected = true;
                            }
                        }    
                        if(isDepartmentSelected){
                            dataForList = dataForList +`<option value='${item.Id}' selected>${item.DepartmentName}</option>`;
                        }else{
                            dataForList = dataForList +`<option value='${item.Id}'>${item.DepartmentName}</option>`;
                        }                        
                    });                       
                },
                error: function (data) {
                }
            });
        }
        if (dependency == "in") {
            $.ajax({
                url: `/api/InCharges`,
                type: 'Get',
                async:false,
                dataType: 'json',
                success: function (data) {                     
                    $.each(data, function (key, item) {
                        var isInchageSelected = false;
                        for (i = 0; i < arrParameters.length; ++i) {
                            if(parseInt(item.Id) == parseInt(arrParameters[i])){
                                isInchageSelected = true;
                            }
                        }  
                        if(isInchageSelected){
                            dataForList = dataForList +`<option value='${item.Id}' selected>${item.InChargeName}</option>`;
                        }else{
                            dataForList = dataForList +`<option value='${item.Id}'>${item.InChargeName}</option>`;
                        } 
                    });                        
                },
                error: function (data) {
                }
            });
        }
        return dataForList;
    }

    //get data for list, when method is changed
    $(document).on('change', '.method_dropdown', function () {

        // var optionDataFor = "";
        // optionDataFor = DataForDropdown(item.ParameterId,dependency,count);                    
        // $('.setting_items_td').find('#data_for_id_'+count).empty().append(optionDataFor);
        // $(".data_for_dropdown").select2();

        
        var data_for_options = "";
        var methodId = $(this).val();    
        var dependency = $('option:selected', this).attr('data-dependency');        
        var row_count = $('option:selected', this).attr('row-count');                

        if (methodId == "" || methodId == undefined || methodId == null) {
            return;
        }
        else {                        
            if (dependency == "dp") {
                $.ajax({
                    url: `/api/Departments`,
                    type: 'Get',
                    async:false,
                    dataType: 'json',
                    success: function (data) {    
                        data_for_options ="";
                        data_for_options = "<option value=''>select item</option>";
    
                        $.each(data, function (key, item) {                        
                            data_for_options = data_for_options +`<option value='${item.Id}'>${item.DepartmentName}</option>`;
                        });                          
                    },
                    error: function (data) {
                    }
                });
            }
            if (dependency == "in") {
                $.ajax({
                    url: `/api/InCharges`,
                    type: 'Get',
                    async:false,
                    dataType: 'json',
                    success: function (data) {
                        data_for_options ="";
                        data_for_options = "<option value=''>select item</option>";
                        $.each(data, function (key, item) {
                            data_for_options = data_for_options +`<option value='${item.Id}'>${item.InChargeName}</option>`;
                        });
                    },
                    error: function (data) {
                    }
                });
            }
    
            $(this).closest('tr').find('.data_for_dropdown').empty().append(data_for_options);
            $(this).closest('tr').find('.data_for_dropdown').select2();
        }
    });   
    
    $(document).on('change', '#table_list', function () {
        $(".total_menu_list_tbl").hide();    
    });

    //add new row for table settings
    $(document).on('click', '.list_table_add_btn ', function () {
        //var row_count = $('option:selected', this).attr('row-count');       

        var $lastRow = $("#total_menu_list_tbody").find("tr").last();        
        var $newRow = $lastRow.clone();

        var setting_col_count = $("#setting_column_count").val();

        $($newRow[0].cells[parseInt(setting_col_count)-1]).empty();
        $($newRow[0].cells[parseInt(setting_col_count)-1]).append(`<select class='data_for_dropdown' multiple='multiple' id=''></select>`);
        
        $newRow.find(".setting_tbl_chk").val('');   
        $newRow.find(".main_item_dropdown").val('');
        $newRow.find(".sub_item_dropdown").val('');
        $newRow.find(".detail_item_dropdown").val('');
        $newRow.find(".method_dropdown").val('');
        $lastRow.after($newRow);




        // var $tr = $(this).closest('.item_row');
        // var $clone = $tr.clone();
        // $($clone[0].cells[4]).empty();
        // $($clone[0].cells[4]).append(`<select class="data_for_dropdown" name="data_for_list_dropdown_for_setting" id="data_for_list_dropdown_for_setting${++globalCount}" multiple="multiple"></select>`);
        // $clone[0].dataset.count = ++globalCount;
        // $clone.find(':text').val('');
        // $tr.after($clone);

        // if(parseInt($lastRow.length)>0){            
        // }     
    });

    //delete button clicked event
    $(document).on('click', '.list_table_delete_btn ', function () {
        var tableId = $("#table_list").val();        
        if (tableId == '' || tableId == null || tableId == undefined) {   
            $(".total_menu_list_tbl").hide();  
            alert('テーブルを選択してください!!!');        
            return false;                 
        }else{
            var checkedId = $('.setting_tbl_chk:checkbox:checked').val();
            if (checkedId == '' || checkedId == null || checkedId == undefined) {            
                $("#delete_settings_table").modal("hide");
                alert('設定を選択してください!!!');        
                return false;
            }else{
                $("#delete_settings_table").modal("show");
            } 
        }        
    });

    //delete settings for the selected table
    $(document).on('click', '.confrim_del_btn ', function () {
        var tableId = $("#table_list").val();    
        var resultsItem = GetDynamicTablesByTableId(tableId);

        var checkedId = $('.setting_tbl_chk:checkbox:checked').val();
        var settingIds = "";
        var val = [];
        $('.setting_tbl_chk:checkbox:checked').each(function(i){
            val[i] = $(this).val();
            if (settingIds == '' || settingIds == null || settingIds == undefined) {
                settingIds = val[i];
            }else{
                settingIds = settingIds+","+val[i];
            }             
        });  
                
        var apiurl = "/api/Utilities/DeleteDynamicTableSettings?tableId=" + tableId+"&settingIds="+settingIds;
        
    
        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            success: function (data) {                
                ToastMessageSuccess(data);                                
                $('#total_menu_list_tbody').empty();      
                GetDynamicSettings(tableId,resultsItem);
                $("#delete_settings_table").modal("hide");
            },
            error: function (data) {
                ToastMessageFailed(data); 
            }
        });
    
        $('#delete_dynamic_table').modal('toggle');
    }); 

    $(document).on('click', '.list_table_edit_btn ', function () {
        var tableSettingsParameters = "";
        //tableSettingsParameters = "settingsId_mainItemId_subItemId_detailItemId_methodId_parameterList(1,2,3)_insertType(update/insert)###settingsId_mainItemId_subItemId_detailItemId_methodId_parameterList(1#2#3#)_insertType(update/insert)"
        
        var is_main_item_colmn = $("#main_item_settings").val();
        var is_sub_item_colmn = $("#sub_item_settings").val();
        var is_detail_item_colmn = $("#detail_item_settings").val();

        var isValidRequest = true;
        $('.setting_tbl_tr').each(function(i){
            var settingsId = "";
            var mainItemId = "";
            var subItemId  = "";
            var detailItemId = "";
            var methodId = "";
            var paramterIds = "";
            var insertType = "";            
                        
            var settingsId = $(this).find(".setting_tbl_chk").val();
            if (settingsId == '' || settingsId == null || settingsId == undefined) {
                insertType = "insert"
                settingsId = 0;
            }else{
                insertType = "update"
            }
            
            if (is_main_item_colmn != '' && is_main_item_colmn != null && is_main_item_colmn != undefined) {         
                if(parseInt(is_main_item_colmn) == 1){
                    var mainItemId = $(this).find(".main_item_dropdown").val();
                    if (mainItemId == '' || mainItemId == null || mainItemId == undefined) {                
                        mainItemId = 0;
                        isValidRequest = false;
                    }
                }
            }

            if (is_sub_item_colmn != '' && is_sub_item_colmn != null && is_sub_item_colmn != undefined) {         
                if(parseInt(is_sub_item_colmn) == 1){
                    var subItemId = $(this).find(".sub_item_dropdown").val();
                    if (subItemId == '' || subItemId == null || subItemId == undefined) {                
                        subItemId = 0;
                        isValidRequest = false;
                    }
                }
            }
            
            if (is_detail_item_colmn != '' && is_detail_item_colmn != null && is_detail_item_colmn != undefined) {         
                if(parseInt(is_detail_item_colmn) == 1){
                    var detailItemId = $(this).find(".detail_item_dropdown").val();
                    if (detailItemId == '' || detailItemId == null || detailItemId == undefined) {                
                        detailItemId = 0;
                        isValidRequest = false;
                    }
                }
            }
            
            var methodId = $(this).find(".method_dropdown").val();
            if (methodId == '' || methodId == null || methodId == undefined) {                
                methodId = 0;
                isValidRequest = false;
            }
            var paramterIds = $(this).find(".data_for_dropdown").val();
            if (paramterIds == '' || paramterIds == null || paramterIds == undefined) {                
                paramterIds = 0;
                isValidRequest = false;
            }                        
            
            if (tableSettingsParameters == '' || tableSettingsParameters == null || tableSettingsParameters == undefined){
                tableSettingsParameters =settingsId+"_"+mainItemId+"_"+subItemId+"_"+detailItemId+"_"+methodId+"_"+paramterIds+"_"+insertType;
            }else{
                tableSettingsParameters = tableSettingsParameters+ "-"+settingsId+"_"+mainItemId+"_"+subItemId+"_"+detailItemId+"_"+methodId+"_"+paramterIds+"_"+insertType;
            }                        
        })
        if(isValidRequest){
            UpdateInsertSettings(tableSettingsParameters);
        }else{
            alert("すべてのフィールドを選択してください");
            return false;
        }        
    });

    //udpate insert dynamic table settings.
    function UpdateInsertSettings(tableSettingsParameters) {       
        var tableId = $("#table_list").val();  
        var resultsItem = GetDynamicTablesByTableId(tableId);
        if (tableSettingsParameters != '' && tableSettingsParameters != null && tableSettingsParameters != undefined){
            var apiurl = "/api/Utilities/InsertUpdateDynamicSettings?tableSettingsParameters=" + tableSettingsParameters+"&tableId="+tableId;
            
            $.ajax({
                url: apiurl,
                type: 'POST',
                dataType: 'json',
                success: function (data) {                
                    ToastMessageSuccess(data);                                
                    $('#total_menu_list_tbody').empty();      
                    GetDynamicSettings(tableId,resultsItem);
                    $("#delete_settings_table").modal("hide");
                },
                error: function (data) {
                    ToastMessageFailed(data); 
                }
            });            
        }else{
        }        
    }  

    //cascading dropdown for main item 
    $(document).on('change', '.main_item_dropdown', function () {
        var categoryId = $(this).val();  
        var subItemHtml = GetSubItemList(categoryId,0);
        $(this).closest('tr').find('.sub_item_dropdown').empty().append(subItemHtml);
        $(this).closest('tr').find('.detail_item_dropdown').empty();
    }); 

    //cascading dropdown for sub item 
    $(document).on('change', '.sub_item_dropdown', function () {
        var subItemId = $(this).val();  
        var detailItemHtml = GetDetailItemList(subItemId,0);        
        $(this).closest('tr').find('.detail_item_dropdown').empty().append(detailItemHtml);        
    }); 

});

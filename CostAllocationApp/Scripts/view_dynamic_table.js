$(document).ready(function () {
    // $('.selectpicker').selectpicker();
    // $(".data_for_dropdown").select2();
    // $('.data_for_multiselect').multiselect();

    html = $('#total_menu_setting_items_tbl').html();
    //get table lsit and show it to the dropdown
    GetDynamicTables();
    //GetDynamicTablesForSetting();

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

    //show table details
    $(document).on('click', '#search_dynamic_table ', function () {
        var tableId = $("#table_list").val();
        if (tableId == '' || tableId == null || tableId == undefined) {
            $(".total_menu_list_tbl").hide();  
            alert('テーブルを選択してください!!!');        
            return false;
        }else{ 
            $('#total_menu_list_tbody').empty();      
            GetDynamicSettings(tableId);
            $(".total_menu_list_tbl").show();  
            $(".data_for_dropdown").select2();            
        }
    });


    //create new row and concate
    $(document).on('click', '#item_row_add ', function () {
        debugger;
        var $tr = $(this).closest('.item_row');
        var $clone = $tr.clone();
        $($clone[0].cells[4]).empty();
        $($clone[0].cells[4]).append(`<select class="data_for_dropdown" name="data_for_list_dropdown_for_setting" id="data_for_list_dropdown_for_setting${++globalCount}" multiple="multiple"></select>`);
        $clone[0].dataset.count = ++globalCount;
        $clone.find(':text').val('');
        $tr.after($clone);
        $clone.find('.setting_plus_icon').hide();    
        $clone.find('.setting_minus_icon').show();    
    });
    
    //get dynamic table settings by table id
    function GetDynamicSettings(dynamicTableId) {            
        $.ajax({
            url: `/api/utilities/GetDynamicSettingsByDynamicTableId?dynamicTableId=${dynamicTableId}`,
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                //$('#setting_list_body').empty();
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
                    
                    startTR = "<tr data-count='1'>";
                    endTR = "</tr>";        
                    
                    checkItem = `<td class='setting_items_td'><input  type='checkbox' value='${item.Id}'></td>`;                       
                    mainItem = GetMainItemList(item.CategoryId);                 
                    subItem = GetSubItemList(item.CategoryId,item.SubCategoryId);            
                    detailItem = GetDetailItemList(item.SubCategoryId,item.DetailsId); 
                    methodList = GetMethodList(item.MethodId);
                    
                    dataForList = "";
                    dataForList = dataForList +" <td class='setting_items_td'>";                    
                    dataForList = dataForList +"<select class='data_for_dropdown' multiple='multiple'>";                        
                    // dataForList = dataForList +"<option value='cheese'>Cheese</option>";    
                    // dataForList = dataForList +"<option value='tomatoes'>Tomatoes</option>";    
                    dataForList = dataForList +"</select>   ";                      
                    dataForList = dataForList +" </td>";
                    
                    totalListItem = ""        
                    totalListItem = startTR+""+checkItem+""+mainItem+""+subItem+""+detailItem+""+methodList+""+dataForList+""+endTR;

                    $('#total_menu_list_tbody').append(`${totalListItem}`);  
                    //GetTotalMenuListHtml(item.Id,item.CategoryId,item.SubCategoryId,item.DetailsId,item.MethodId);
                    //$('#setting_list_body').append(`<tr><td>${item.DynamicTableName}</td><td>${item.CategoryName}</td><td>${item.SubCategoryName}</td><td>${item.DetailsItemName}</td><td>${item.MethodName}</td><td>${item.CommaSeperatedParameterName}</td></tr>`);
                });
            },
            error: function (data) {
            }
        });
    }

    //create total menu dynamic list
    function GetTotalMenuListHtml(settingId,mainItemId,subItemId,detailItemId,methodId){
        var startTR = "";
        var endTR = "";
        var checkItem = "";
        var mainItem = "";
        var subItem = "";
        var detailItem = "";
        var methodList = "";
        var dataForList = "";
        var totalListItem = "";
        
        startTR = "<tr data-count='1'>";
        endTR = "</tr>";        

        checkItem = "<td class='setting_items_td'><input  type='checkbox' value=''></td>";   

        mainItem = GetMainItemList();                 
        subItem = GetSubItemList(1);            
        detailItem = GetDetailItemList(1); 
        methodList = GetMethodList();
        
        dataForList = "";
        dataForList = dataForList +" <td class='setting_items_td'>";
        dataForList = dataForList +"    <select class='select' multiple>";
        dataForList = dataForList +"        <option value='1'>One</option>";
        dataForList = dataForList +"        <option value='2'>Two</option>";
        dataForList = dataForList +"        <option value='3'>Three</option>";
        dataForList = dataForList +"    </select>";
        //dataForList = dataForList +"    <select class='data_for_dropdown'> ";
        // dataForList = dataForList +"      <option value=''>select data</option>";
        // dataForList = dataForList +"      <option value='9'>data-1</option>";
        // dataForList = dataForList +"      <option value='10'>data-2</option>";
        
        //dataForList = dataForList +"    </select>";
        dataForList = dataForList +" </td>";
        
        totalListItem = ""        
        totalListItem = startTR+""+checkItem+""+mainItem+""+subItem+""+detailItem+""+methodList+""+dataForList+""+endTR;

        $('#total_menu_list_tbody').append(`${totalListItem}`);  
        //return totalListItem;
    }

    //get main item list
    function GetMainItemList(mainItemId){
        var mainItem = "";
        $.ajax({
            url: '/api/utilities/GetCategories/',
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                
                mainItem = mainItem +" <td class='setting_items_td'>";
                mainItem = mainItem +"    <select class='main_item_dropdown'>";
                mainItem = mainItem +"      <option value='0'>設定する大項目を入力</option>";
                $.each(data, function (key, item) {
                    if (mainItemId != '' && mainItemId != null && mainItemId != undefined) {
                        if(parseInt(mainItemId) == parseInt(item.Id)){
                            mainItem = mainItem +`<option value='${item.Id}' selected>${item.CategoryName}</option>`;
                        }else{
                            mainItem = mainItem +`<option value='${item.Id}'>${item.CategoryName}</option>`;
                        }                        
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

        $.ajax({
            url: `/api/utilities/GetSubCategoriesByCategory?categoryId=${categoryId}`,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {                
                subItem = subItem +" <td class='setting_items_td'>";
                subItem = subItem +"    <select class='sub_item_dropdown'> ";
                subItem = subItem +"      <option value=''>select sub item</option>";                                                                   
                $.each(data, function (key, item) {   
                    if (subCategoryId != '' && subCategoryId != null && subCategoryId != undefined){
                        if(parseInt(subCategoryId) == parseInt(item.Id)){
                            subItem = subItem +`<option value='${item.Id}' selected>${item.SubCategoryName}</option>`;
                        }else{
                            subItem = subItem +`<option value='${item.Id}'>${item.SubCategoryName}</option>`;
                        } 
                        
                    }
                });  
                subItem = subItem +"    </select>";
                subItem = subItem +" </td>";               
            },
            error: function (data) {
            }
        });
        return subItem;
    }

    //get sub item list
    function GetDetailItemList(subItemId,detailItemId){        
        var detailItem = "";       
        $.ajax({
            url: `/api/utilities/GetDetailsItemBySubItemsId?subItemId=${subItemId}`,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {                
                detailItem = detailItem +" <td class='setting_items_td'>";
                detailItem = detailItem +"    <select class='detail_item_dropdown'> ";
                detailItem = detailItem +"      <option value=''>select detail item</option>";                                                                   
                $.each(data, function (key, item) {   
                    if (detailItemId != '' && detailItemId != null && detailItemId != undefined){
                        if(parseInt(detailItemId) == parseInt(item.Id)){
                            detailItem = detailItem +`<option value='${item.Id}' selected>${item.DetailsItemName}</option>`;
                        }else{
                            detailItem = detailItem +`<option value='${item.Id}'>${item.DetailsItemName}</option>`;
                        }                         
                    }                                     
                });  
                detailItem = detailItem +"    </select>";
                detailItem = detailItem +" </td>";               
            },
            error: function (data) {
            }
        });
        return detailItem;
    }

    //get method list
    function GetMethodList(methodId){
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
                    if (methodId != '' && methodId != null && methodId != undefined){
                        if(parseInt(methodId) == parseInt(item.Id)){
                            methodList = methodList +`<option value='${item.Id}'data-dependency=${item.Dependency} selected>${item.MethodName}</option>`;
                        }else{
                            methodList = methodList +`<option value='${item.Id}'data-dependency=${item.Dependency}>${item.MethodName}</option>`;
                        }                         
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

    //get data for list, when method is changed
    $(document).on('change', '.method_dropdown', function () {
        var data_for_options = "";
        var methodId = $(this).val();    
        var dependency = $('option:selected', this).attr('data-dependency');
        
        if (methodId == "" || methodId == undefined || methodId == null) {
            return;
        }
        else {
            // pull data for dependency
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
});

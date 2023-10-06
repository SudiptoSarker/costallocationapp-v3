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
                    
                    startTR = "<tr data-count='1' class='setting_tbl_tr'>";
                    endTR = "</tr>";        
                    
                    checkItem = `<td class='setting_items_td'><input  type='checkbox' value='${item.Id}' class='setting_tbl_chk'></td>`;                       
                    mainItem = GetMainItemList(item.CategoryId);                 
                    subItem = GetSubItemList(item.CategoryId,item.SubCategoryId);            
                    detailItem = GetDetailItemList(item.SubCategoryId,item.DetailsId); 
                    methodList = GetMethodList(item.MethodId);
                    
                    dataForList = "";
                    dataForList = dataForList +" <td class='setting_items_td data_for'>";         
                    // dataForList = dataForList +"    <span>one</span><span>two</span>";
                    dataForList = dataForList +"<select class='data_for_dropdown' multiple='multiple'>";                    
                    //dataForList = dataForList +"<select class='data_for_dropdown' multiple='multiple'>";                        
                    //dataForList = dataForList +"<option value='cheese'>Cheese,butter,red</option>";    
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
        dataForList = dataForList +"    <span>one</span><span>two</span>";
        // dataForList = dataForList +"    <select class='select' multiple>";
        // dataForList = dataForList +"        <option value='1'>One</option>";
        // dataForList = dataForList +"        <option value='2'>Two</option>";
        // dataForList = dataForList +"        <option value='3'>Three</option>";
        // dataForList = dataForList +"    </select>";
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
                mainItem = mainItem +"      <option value=''>設定する大項目を入力</option>";
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
    
    $(document).on('change', '#table_list', function () {
        $(".total_menu_list_tbl").hide();    
    });

    //add new row for table settings
    $(document).on('click', '.list_table_add_btn ', function () {

        var $lastRow = $("#total_menu_setting_items_tbl").find("tr").last();
        var $newRow = $lastRow.clone(); 
        $newRow.find(".setting_tbl_chk").val('');   
        $newRow.find(".main_item_dropdown").val('');
        $newRow.find(".sub_item_dropdown").val('');
        $newRow.find(".detail_item_dropdown").val('');
        $newRow.find(".method_dropdown").val('');
        $newRow.find(".data_for_dropdown").val('');
        $lastRow.after($newRow);
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
                GetDynamicSettings(tableId);
                $("#delete_settings_table").modal("hide");
            },
            error: function (data) {
                ToastMessageFailed(data); 
            }
        });
    
        $('#delete_dynamic_table').modal('toggle');
    });    
    $(document).on('click', '.list_table_edit_btn ', function () {
        //alert("operation success.");

        var tableSettingsParameters = "";
        //tableSettingsParameters = "settingsId_mainItemId_subItemId_detailItemId_methodId_parameterList(1,2,3)_insertType(update/insert)###settingsId_mainItemId_subItemId_detailItemId_methodId_parameterList(1#2#3#)_insertType(update/insert)"
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

            var mainItemId = $(this).find(".main_item_dropdown").val();
            if (mainItemId == '' || mainItemId == null || mainItemId == undefined) {                
                mainItemId = 0;
            }

            var subItemId = $(this).find(".sub_item_dropdown").val();
            if (subItemId == '' || subItemId == null || subItemId == undefined) {                
                subItemId = 0;
            }
            var detailItemId = $(this).find(".detail_item_dropdown").val();
            if (detailItemId == '' || detailItemId == null || detailItemId == undefined) {                
                detailItemId = 0;
            }
            var methodId = $(this).find(".method_dropdown").val();
            if (methodId == '' || methodId == null || methodId == undefined) {                
                methodId = 0;
            }
            var paramterIds = $(this).find(".data_for_dropdown").val();
            if (paramterIds == '' || paramterIds == null || paramterIds == undefined) {                
                paramterIds = 0;
            }
            if (tableSettingsParameters == '' || tableSettingsParameters == null || tableSettingsParameters == undefined){
                tableSettingsParameters =settingsId+"_"+mainItemId+"_"+subItemId+"_"+detailItemId+"_"+methodId+"_"+paramterIds+"_"+insertType;
            }else{
                tableSettingsParameters = tableSettingsParameters+ "---"+settingsId+"_"+mainItemId+"_"+subItemId+"_"+detailItemId+"_"+methodId+"_"+paramterIds+"_"+insertType;
            }            
        })
        UpdateInsertSettings(tableSettingsParameters);
    });
    //udpate insert dynamic table settings.
    function UpdateInsertSettings(tableSettingsParameters) {       
        alert("out: "+tableSettingsParameters)                                 
        if (tableSettingsParameters != '' && tableSettingsParameters != null && tableSettingsParameters != undefined){
            var apiurl = "/api/Utilities/InsertUpdateDynamicSettings?tableSettingsParameters=" + tableSettingsParameters;
            
            $.ajax({
                url: apiurl,
                type: 'POST',
                dataType: 'json',
                success: function (data) {                
                    ToastMessageSuccess(data);                                
                    $('#total_menu_list_tbody').empty();      
                    GetDynamicSettings(tableId);
                    $("#delete_settings_table").modal("hide");
                },
                error: function (data) {
                    ToastMessageFailed(data); 
                }
            });            
        }else{
        }        
    }    
});

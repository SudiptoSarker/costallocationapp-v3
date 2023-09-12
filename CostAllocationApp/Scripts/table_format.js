
$(document).ready(function () {
    //get table lsit and setting list on page load. 
    GetDynamicTables();
    GetDynamicTablesForSetting();
});

function InsertDynamicTables() {
    var apiurl = "/api/Utilities/CreateDynamicTable";
    var tableName = $("#table_name").val();
    var tableTitle = $("#table_title").val();
    var tablePosition = $("#table_position").val();
    var isValid = true;

    if (tableName == "") {
        isValid = false;
        $("#table_name_warning_msg").show();
        $("#table_name").focus();        
    }else{
        $("#table_name_warning_msg").hide();
    }
    if (tableTitle == "") {
        if(isValid){
            $("#table_title").focus();   
        }        
        isValid = false;
        $("#table_title_warning_msg").show();        
    }else{         
        $("#table_title_warning_msg").hide();
    }
    if (tablePosition == "") {
        if(isValid){
            $("#table_position").focus();   
        }  
        isValid = false;
        $("#table_position_warning_msg").show();
    }else{
        $("#table_position_warning_msg").hide();
    }


    if(isValid) {
        $("#table_name_warning_msg").hide();
        $("#table_title_warning_msg").hide();
        $("#table_position_warning_msg").hide();

        var data = {
            TableName: tableName,
            TableTitle: tableTitle,
            TablePosition: tablePosition,
            IsActive: true,
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                if(data==1){
                    alert("同一データが登録済みです.");
                }else{
                    $("#page_load_after_modal_close").val("yes");
                    $("#table_name").val('');
                    $("#table_title").val('');
                    $("#table_position").val('');
                    ToastMessageSuccess(data);
                    GetDynamicTables();
                    GetDynamicTablesForSetting();
                }                
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }
}

function InsertDynamicSetting() {
    var dynamicTableId = $('#dynamic_table_list_for_setting').val();
    var mainItemId = $('#main_item').val();
    var subItemId = $('#sub_item').val();
    var subItemId = $('#sub_item').val();
    var methodId = $('#method_list_dropdown').val();
    var dependencyId = $('#data_for_list_dropdown_for_setting').val();

    $.ajax({
        url: '/api/utilities/CreateDynamicSetting/',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: {
            DynamicTableId: dynamicTableId,
            CategoryId: mainItemId,
            SubCategoryId: subItemId,
            DetailsId: '0',
            MethodId: methodId,
            ParameterId: dependencyId
        },
        success: function (data) {
            ToastMessageSuccess(data);
            GetDynamicSettings();
        }
    });
}
function GetDynamicTables() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#dynamic_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#dynamic_list_tbody').append(`<tr><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td><td><label id="dynamic_table_delete"><a id="dynamic_table_delete_link" href="javascript:void();" data-toggle="modal" data-target="#delete_dynamic_table" onClick="DeleteDynmaicTalbe(${item.Id})">削除</a></label><label id="dynamic_table_edit_label"><a id="dynamic_table_edit_link" href="javascript:void();" data-toggle="modal" data-target="#edit_dynamic_table_modal" onClick="GetDynamicTalbeById(${item.Id})">編集</a></label></td></tr>`);
            });
        });
}
function GetDynamicTalbeById(dynamicTableId){    
    $.getJSON(`/api/utilities/GetDynamicTableById/${dynamicTableId}`)
        .done(function (data) {
            $("#table_id_for_edit").val(data.Id);
            $("#table_name_edit").val(data.TableName);
            $("#table_title_edit").val(data.TableTitle);
            $("#table_position_edit").val(data.TablePosition);    
        });
}

//edit dynamic table by table id
$(document).on('click', '#update_dynamic_table ', function () { 
    var apiurl = "/api/Utilities/UpdateDynamicTable";
    var id = $("#table_id_for_edit").val();
    var tableName = $("#table_name_edit").val();
    var tableTitle = $("#table_title_edit").val();
    var tablePosition = $("#table_position_edit").val();
    if (tableName == "") {
        alert("please enter table name!")
        return false;
    }
    if (tableTitle == "") {
        alert("please enter table title!")
        return false;
    }
    else {        
        var data = {
            Id:id,
            TableName: tableName,
            TableTitle: tableTitle,
            TablePosition: tablePosition,
            IsActive: true,
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                if(data==1){
                    alert("同一データが登録済みです.");
                }else{
                    ToastMessageSuccess(data);
                    GetDynamicTables();
                    GetDynamicTablesForSetting();
                    $('#edit_dynamic_table_modal').modal('toggle');
                }                
            },
            error: function (data) {
                ToastMessageFailed(data);
                $('#edit_dynamic_table_modal').modal('toggle');
            }
        });
    }  
    
    
});

//update main item
$(document).on('click', '#update_main_item ', function () {     
});
//update sub item
$(document).on('click', '#update_sub_item ', function () {     
});
//update detail item
$(document).on('click', '#update_detail_item ', function () {     
});
//update table setting
$(document).on('click', '#udpate_tbl_setting ', function () {     
});

function DeleteDynmaicTalbe(dynamicTableId){
    $("#table_id_for_delete").val(dynamicTableId);
}

//delete dynamic table by table id
$(document).on('click', '#delete_table_link ', function () { 

    var apiurl = "/api/Utilities/InactiveDynamicTable";
    var id = $("#table_id_for_delete").val();
    var tableName ="";
    var tableTitle ="";
    var tablePosition ="";

    var data = {
        Id:id,
        TableName: tableName,
        TableTitle: tableTitle,
        TablePosition: tablePosition,
        IsActive: false,
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);
            GetDynamicTables();
            GetDynamicTablesForSetting();
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
    
    $('#delete_dynamic_table').modal('toggle');
});
//delete dynamic table by table id
$(document).on('click', '#delete_setting_link ', function () { 

    var apiurl = "/api/Utilities/InactiveDynamicTable";
    var id = $("#table_id_for_delete").val();
    var tableName ="";
    var tableTitle ="";
    var tablePosition ="";

    var data = {
        Id:id,
        TableName: tableName,
        TableTitle: tableTitle,
        TablePosition: tablePosition,
        IsActive: false,
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);
            GetDynamicTables();
            GetDynamicTablesForSetting();
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
    
    $('#delete_dynamic_table').modal('toggle');
});

function GetDynamicTablesForSetting() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#dynamic_table_list_for_setting').empty();
            $('#dynamic_table_list_for_setting').append(`<option value=''>Select Tables</option>`)
            $.each(data, function (key, item) {
                $('#dynamic_table_list_for_setting').append(`<option value='${item.Id}'>${item.TableName}</option>`)
            });
        });
}

function DynamicTableSetting(){
    var tableId = $("#dynamic_table_list_for_setting").val();
    if (tableId == '' || tableId == null || tableId == undefined) {
        $("#total_menu_settings").hide();
        alert('テーブルを選択してください!!!');        
        return false;
    } else {
        GetDynamicSettings();
        $("#total_menu_settings").show();

        // pull data for main item
        $.ajax({
            url: '/api/utilities/GetCategories/',
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                $('#main_item').empty();
                $('#main_item').append(`<option value=''>Select Item</option>`);
                $.each(data, function (key, item) {
                    $('#main_item').append(`<option value='${item.Id}'>${item.CategoryName}</option>`);
                });
                $('#main_item').append(`<option disabled="disabled">---------</option>`);
                $('#main_item').append(`<option value='main'>Add New</option>`);
            },
            error: function (data) {
            }
        }); 

        // pull method list
        $.ajax({
            url: '/api/utilities/GetMethodList/',
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                $('#method_list_dropdown').empty();
                $('#method_list_dropdown').append(`<option value=''>Select Item</option>`);
                console.log(data);
                $.each(data, function (key, item) {
                    $('#method_list_dropdown').append(`<option value='${item.Id}'data-dependency=${item.Dependency}>${item.MethodName}</option>`);
                });
            },
            error: function (data) {
            }
        }); 


    }         
}

$(document).on('change', '.main_item_dropdown', function () {
    var mainItem = $(this).val();
    console.log("mainitem: "+mainItem);
    if (mainItem == "" || mainItem == undefined || mainItem == null) {
        return;
    } else if (mainItem == "main") {
        console.log("mainitem2: "+mainItem);
        $('#add_main_item_modal').modal('toggle');
    }
    else {
        console.log("mainitem3: "+mainItem);
        // pull data for sub item
        $.ajax({
            url: `/api/utilities/GetSubCategoriesByCategory?categoryId=${mainItem}`,
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                //$('#sub_item').empty();
                let sub_item_options = "";
                sub_item_options = "<option value=''>select sub item</option>";
                $.each(data, function (key, item) {                    
                    sub_item_options = sub_item_options +`<option value='${item.Id}'>${item.SubCategoryName}</option>`;
                    //$('#sub_item').append(`<option value='${item.Id}'>${item.SubCategoryName}</option>`);
                });
                $(this).closest('tr').find('.sub_item_dropdown').empty().append(sub_item_options); 

                $('#sub_item').append(`<option disabled="disabled">---------</option>`);
                $('#sub_item').append(`<option value='sub'>Add New</option>`);
            },
            error: function (data) {
            }
        }); 
    }
});
$(document).on('change', '.method_dropdown', function () {
    var methodId = $(this).val();    
    var dependency = $('option:selected', this).attr('data-dependency');
    console.log(dependency);
    if (methodId == "" || methodId == undefined || methodId == null) {
        return;
    }
    else {
        // pull data for dependency
        if (dependency=="dp") {
            $.ajax({
                url: `/api/Departments`,
                type: 'Get',
                dataType: 'json',
                success: function (data) {
                    // $('#data_for_list_dropdown_for_setting').empty();
                    // $('#data_for_list_dropdown_for_setting').append(`<option value=''>Select Item</option>`);
                    let data_for_options = "";
                    data_for_options = "<option value=''>select item</option>";

                    $.each(data, function (key, item) {
                        //$('#data_for_list_dropdown_for_setting').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
                        data_for_options = data_for_options +`<option value='${item.Id}'>${item.DepartmentName}</option>`;
                    });      
                    console.log("dpt: "+data_for_options);
                    $(this).closest('tr').find('.data_for_dropdown').empty().append(data_for_options);                                   
                },
                error: function (data) {
                }
            });
        }
        if (dependency == "in") {
            $.ajax({
                url: `/api/InCharges`,
                type: 'Get',
                dataType: 'json',
                success: function (data) {
                    // $('#data_for_list_dropdown_for_setting').empty();
                    // $('#data_for_list_dropdown_for_setting').append(`<option value=''>Select Item</option>`);
                    let data_for_options = "";
                    data_for_options = "<option value=''>select item</option>";
                    $.each(data, function (key, item) {
                        //$('#data_for_list_dropdown_for_setting').append(`<option value='${item.Id}'>${item.InChargeName}</option>`);
                        data_for_options = data_for_options +`<option value='${item.Id}'>${item.InChargeName}</option>`;
                    });
                    console.log("int: "+data_for_options);
                    $(this).closest('tr').find('.data_for_dropdown').empty().append(data_for_options);         
                },
                error: function (data) {
                }
            });
        }
        
    }
});
$(document).on('change', '.sub_item_dropdown', function () {
    var subItem = $(this).val();        
if (subItem == '' || subItem == null || subItem == undefined) {
	
}else{

	if(subItem=="sub"){        
		$('#add_sub_item_modal').modal('toggle');
	}else{
		let sub_item_options = "";
		sub_item_options = "<option value=''>select sub item</option>";
		sub_item_options = sub_item_options +"<option value='10'>detail-item-12</option>";
		sub_item_options = sub_item_options +"<option value='11'>detail-item-13</option>";
		sub_item_options = sub_item_options +"<option disabled='disabled'>---------</option>";
		sub_item_options = sub_item_options +"<option value='detail'>Add New</option>";
        $(this).closest('tr').find('.detail_item_dropdown').empty().append(sub_item_options);
        

		// $(this).closest('tr').find('.detail_item_dropdown').empty().append
		// (
		// 	`<option value="">select detail item</option>`,
		// 	`<option value="10">detail-item-12</option>`,
		// 	`<option value="11">detail-item-13</option>`,
		// 	`<option disabled='disabled'>---------</option>`,
		// 	`<option value='detail'>Add New</option>`
		// );                        
	}
}  
});
$(document).on('change', '.detail_item_dropdown', function () {
    var detailItem = $(this).val();    
    if(detailItem=="detail"){        
        $('#add_detail_item_modal').modal('toggle');
    }
});

//create new row and concate
$(document).on('click', '#item_row_add ', function () { 
    var $tr    = $(this).closest('.item_row');
    var $clone = $tr.clone();
    $clone.find(':text').val('');
    $tr.after($clone);
    $clone.find('.setting_plus_icon').hide();    
    $clone.find('.setting_minus_icon').show();    
});

//remove rows
$(document).on('click', '.setting_minus_icon ', function () { 
    $(this).closest("tr").remove();
});

//get setting list value
function GetAllSettingValue() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#setting_list_body').empty();
            $.each(data, function (key, item) {
                $('#setting_list_body').append(`<tr><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td><td><label id="dynamic_table_delete"><a id="dynamic_table_delete_link" href="javascript:void();" data-toggle="modal" data-target="#delete_dynamic_table" onClick="DeleteDynmaicTalbe(${item.Id})">削除</a></label><label id="dynamic_table_edit_label"><a id="dynamic_table_edit_link" href="javascript:void();" data-toggle="modal" data-target="#edit_dynamic_table_modal" onClick="GetDynamicTalbeById(${item.Id})">編集</a></label></td></tr>`);                
            });
        });
}

//clear dynamic table input fields
$(document).on('click', '#clear_input_frm ', function () { 
    $("#table_name").val('');
    $("#table_title").val('');
    $("#table_position").val('');
    $("#table_name_warning_msg").hide();
    $("#table_title_warning_msg").hide();
    $("#table_position_warning_msg").hide();    
});
//clear dynamic table input fields
$(document).on('click', '#clear_setting_form ', function () { 
    $('#total_menu_setting_items_tbl').load(' #total_menu_setting_items_tbl')
});

function GetDynamicSettings() {
    var dynamicTableId = $('#dynamic_table_list_for_setting').val();

    $.ajax({
        url: `/api/utilities/GetDynamicSettingsByDynamicTableId?dynamicTableId=${dynamicTableId}`,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('#setting_list_body').empty();
            $.each(data, function (key, item) {
                $('#setting_list_body').append(`<tr><td>${item.DynamicTableName}</td><td>${item.CategoryName}</td><td>${item.SubCategoryName}</td><td>n/a</td><td>${item.MethodName}</td><td>${item.ParameterId}</td></tr>`);
            });
        },
        error: function (data) {
        }
    });
}
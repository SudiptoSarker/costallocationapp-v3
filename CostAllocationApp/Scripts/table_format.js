
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
    }else{
        $("#total_menu_settings").show();
    }         
}

$(document).on('change', '.main_item_dropdown', function () {
    var mainItem =  $(this).val();    
    if(mainItem=="main"){        
        $('#add_main_item_modal').modal('toggle');        
    }
});
$(document).on('change', '.sub_item_dropdown', function () {
    var subItem = $(this).val();    
    if(subItem=="sub"){        
        $('#add_sub_item_modal').modal('toggle');
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
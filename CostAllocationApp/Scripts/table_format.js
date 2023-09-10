function InsertDynamicTables() {
    var apiurl = "/api/Utilities/CreateDynamicTable";
    var tableName = $("#table_name").val();
    var tableTitle = $("#table_title").val();
    var tablePosition = $("#table_position").val();
    if (tableName == "") {
        $("#table_name_err").show();
        return false;
    }
    if (tableTitle == "") {
        $("#table_title_err").show();
        return false;
    }
    else {
        $("#table_name_err").hide();
        $("#table_title_err").hide();
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
                }                
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }
}

function GetDynamicTables() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#dynamic_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#dynamic_list_tbody').append(`<tr><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td><td><label id="dynamic_table_delete"><a id="dynamic_table_delete_link" href="javascript:void();" data-toggle="modal" data-target="#delete_dynamic_table" onClick="DeleteDynmaicTalbe(${item.Id})">削除</a></label><label id="dynamic_table_edit_label"><a id="dynamic_table_edit_link" href="javascript:void();" data-toggle="modal" data-target="#edit_dynamic_table_modal" onClick="GetDynamicTalbeById(${item.Id})">編集</a></label></td></tr>`);
                // $('#dynamic_list_tbody').append(`<tr><td>${item.Id}</td><td>${item.TableTitle}</td></tr>`);
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
$(document).on('click', '#edit_dynamic_table_link ', function () { 

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

function DeleteDynmaicTalbe(dynamicTableId){
    $("#table_id_for_delete").val(dynamicTableId);
}
//delete dynamic table by table id
$(document).on('click', '#delete_dynamic_table_link ', function () { 

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
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
    
    $('#delete_dynamic_table').modal('toggle');
});
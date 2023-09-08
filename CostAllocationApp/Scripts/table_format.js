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
                $("#page_load_after_modal_close").val("yes");
                $("#table_name").val('');
                $("#table_title").val('');
                $("#table_position").val('');
                ToastMessageSuccess(data);
                GetDynamicTables();
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
                $('#dynamic_list_tbody').append(`<tr><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td><td><button>remove<button></td></tr>`);
            });
        });
}
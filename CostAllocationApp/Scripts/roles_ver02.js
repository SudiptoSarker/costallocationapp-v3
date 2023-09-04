/***************************\                           
    Roles In-Active/Remove 
    Also,shows that in how many projec that Roles is assigned                
\***************************/
function onRoleInactiveClick() {
    let roleIds = GetCheckedIds("role_list_tbody");
    var apiurl = '/api/utilities/RoleCount?roleIds=' + roleIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.role_count').empty();
            $.each(data, function (key, item) {
                $('.role_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}

$(document).ready(function () {
    /***************************\                           
        Show Roles list on page load           
    \***************************/
    GetRoleList();
    
    /***************************\                           
        Roles Delete/Remove Confirm Button           
    \***************************/	
    $('#role_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("role_list_tbody");

        id = id.slice(0, -1);
        $.ajax({
            url: '/api/Roles?roleIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);                
                GetRoleList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#inactive_role_modal').modal('toggle');

    });

    /***************************\                           
        Check if the Roles is checked for delete/remove
    \***************************/
    $('#role_inactive_btn').on('click', function (event) {

        let id = GetCheckedIds("role_list_tbody");
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
    });
});

/***************************\                           
    Roles Insertion function. 
\***************************/                                    
function InsertRoles() {
    var apiurl = "/api/Roles/";
    let roleName = $("#role_name").val().trim();
    /***************************\                           
        check roles input field is empty or not. if empty then show error message.
    \***************************/
    if (roleName == "") {
        $(".role_name_err").show();
        return false;
    }
    else {
        $(".role_name_err").hide();
        var data = {
            RoleName: roleName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#role_name").val('');
                ToastMessageSuccess(data);                
                $('#section-name').val('');
                GetRoleList();                
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

/***************************\                           
    Get all the Roles list from database.
\***************************/
function GetRoleList(){
    $.getJSON('/api/Roles/')
    .done(function (data) {
        $('#role_list_tbody').empty();
        $.each(data, function (key, item) {
            // Add a list item for the product.
            $('#role_list_tbody').append(`<tr><td><input type="checkbox" class="role_list_chk" data-id='${item.Id}' /></td><td>${item.RoleName}</td></tr>`);
        });
    });
}
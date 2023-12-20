$(document).ready(function () {
    /***************************\                           
        Show Roles list on page load           
    \***************************/
    GetRoleList();

    //show add modal
    $(".add_role_btn").on("click",function(event){     
        $('#role_name').val('');   
        $('#add_role_modal').modal('show');
    })

    //clear input fields for add modal
    $("#undo_role_add_btn").on("click",function(event){        
        $('#role_name').val('');
    })

    //clear input fields for edit modal
    $("#undo_role_edit_btn").on("click",function(event){        
        $('#role_name_edit').val('');
    })
    
    //insert roles 
    $("#roles_reg_save_btn").on("click",function(event){       
        let role_name = $("#role_name").val().trim();        
        if (role_name == "") {
            alert("役割を入力してください");
            return false;
        }

        //call insert roles functions
        UpdateInsertRoles(role_name,0,false);
    })

    //show update/edit modal
    $(".edit_role_btn").on("click",function(event){          
        let id = GetCheckedIds("role_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("役割が選択されていません");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集の場合、複数の選択はできません");
            return false;
        }else{   
            FillTheEditModal(arrIds[0]);

            $('#edit_role_modal').modal('show');
        }        
    })

    //update roles
    $("#roles_reg_save_btn_edit").on("click",function(event){           
        var roleName = $("#role_name_edit").val();   
        var roleId= $("#edit_role_id").val();   

        if (roleName == '' || roleName == null || roleName == undefined){
            alert("役割を入力してください");
            return false;
        }
        else{
            //call update roles functions
            UpdateInsertRoles(roleName,roleId,true);
        }        
    })
    
    /***************************\                           
        Roles Delete/Remove Confirm Button           
    \***************************/	
    $('#role_del_confirm').on('click', function (event) {
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
                ToastMessageFailed("操作が失敗しました");
            }
        });

        $('#delete_role_modal').modal('toggle');

    });

    /***************************\                           
        Check if the Roles is checked for delete/remove
    \***************************/
    $('.delete_role_btn').on('click', function (event) {

        let id = GetCheckedIds("role_list_tbody");
        if (id == "") {
            alert("役割が選択されていません");
            return false;
        }else{
            RoleWithAssignment();
            $('#delete_role_modal').modal('show');
        }
    });
});

//get roles details by roles id
function FillTheEditModal(roleId){            
    var apiurl = `/api/utilities/GetRoleNameByRoleId`;
    $.ajax({
        url: apiurl,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "roleId=" + roleId,
        success: function (data) { 
            $("#role_name_edit").val(data.RoleName);   
            $("#edit_role_id").val(data.Id);   
        }
    });            
}

/***************************\                           
Delete confirmation function and shows the role assignemnts with the selected roles        
\***************************/	
function RoleWithAssignment() {
    let roleIds = GetCheckedIds("role_list_tbody");
    var apiurl = '/api/utilities/RoleCount?roleIds=' + roleIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.del_confirm_warning').empty();
            $.each(data, function (key, item) {
                $('.del_confirm_warning').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}


/***************************\                           
    Roles Update/Insert
\***************************/                                    
function UpdateInsertRoles(roleName,roleId,isUpdate){        
    var apiurl = "/api/Roles/";
    var data = {
        Id:roleId,
        RoleName: roleName,
        IsUpdate:isUpdate
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);                
            GetRoleList();                

            if(isUpdate){
                $("#edit_role_modal").modal("hide");
            }else{
                $("#add_role_modal").modal("hide");                
            }            
        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });

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
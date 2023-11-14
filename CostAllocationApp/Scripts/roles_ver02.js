$(document).ready(function () {
    /***************************\                           
        Show Roles list on page load           
    \***************************/
    GetRoleList();

    //clear input fields
    $(".add_role_btn").on("click",function(event){     
        $('#role_name').val('');   
        $('#add_role_modal').modal('show');
    })
    $("#undo_role_add_btn").on("click",function(event){        
        $('#role_name').val('');
    })
    $("#undo_role_edit_btn").on("click",function(event){        
        $('#role_name_edit').val('');
    })
    
    //add role
    $("#roles_reg_save_btn").on("click",function(event){       
        let role_name = $("#role_name").val().trim();        
        if (role_name == "") {
            alert("please enter role!");
            return false;
        }

        UpdateInsertRoles(role_name,0,false);
    })

    //edit role
    $(".edit_role_btn").on("click",function(event){          
        let id = GetCheckedIds("role_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集するセクションにチェックを入れてください");
            return false;
        }else{   
            FillTheEditModal(arrIds[0]);

            $('#edit_role_modal').modal('show');
        }        
    })

    //edit from modal
    $("#roles_reg_save_btn_edit").on("click",function(event){   
        
        var roleName = $("#role_name_edit").val();   
        var roleId= $("#edit_role_id").val();   

        if (roleName == '' || roleName == null || roleName == undefined){
            alert("please enter role name!");
            return false;
        }
        else{
            UpdateInsertRoles(roleName,roleId,true);
        }        
    })

    //get section details by section id
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
                ToastMessageFailed(data);
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
            alert("ファイルが削除されたことを確認してください");
            return false;
        }else{
            RoleWithAssignment();
            $('#delete_role_modal').modal('show');
        }
    });

    
    /***************************\                           
        Roles In-Active/Remove 
        Also,shows that in how many projec that Roles is assigned                
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
        Roles Insertion function. 
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
});


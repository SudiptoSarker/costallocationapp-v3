/***************************\                           
    Company In-Active/Remove     
\***************************/
function onCompanyInactiveClick() {
    let companyIds = GetCheckedIds("company_list_tbody");
    var apiurl = '/api/utilities/CompanyCount?companyIds=' + companyIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.company_count').empty();
            $.each(data, function (key, item) {
                $('.company_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}

$(document).ready(function () {
    $(".add_master_btn").on("click",function(event){        
        $('#add_master_modal').modal('show');
    })
    $("#company_reg_save_btn").on("click",function(event){       
        let company_name = $("#company_name").val().trim();        
        if (company_name == "") {
            alert("please enter company!");
            return false;
        }

        UpdateInsertCompanies(company_name,0,false);
    })
    //edit incharge
    $(".edit_master_btn").on("click",function(event){          
        let id = GetCheckedIds("company_list_tbody");
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

            $('#edit_master_modal').modal('show');
        }        
    })
    function FillTheEditModal(companyId){            
        var apiurl = `/api/utilities/GetCompanyByCompanyId`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "companyId=" + companyId,
            success: function (data) { 
                $("#company_name_edit").val(data.CompanyName);   
                $("#edit_role_id").val(data.Id);   
            }
        });            
    }
    //edit from modal
    $("#company_reg_save_btn_edit").on("click",function(event){   
        
        var roleName = $("#company_name_edit").val();   
        var roleId= $("#edit_role_id").val();   

        if (roleName == '' || roleName == null || roleName == undefined){
            alert("please enter role name!");
            return false;
        }
        else{            
            UpdateInsertCompanies(roleName,roleId,true);
        }        
    })

    /***************************\                           
        Company Insertion function. 
    \***************************/ 
    function UpdateInsertCompanies(companyName,companyId,isUpdate) {
        
        var apiurl = "/api/Companies/";
        var data = {
            Id:companyId,
            CompanyName: companyName,
            IsUpdate:isUpdate
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                ToastMessageSuccess(data);
                GetCompanyList();

                if(isUpdate){
                    $("#edit_master_modal").modal("hide");
                }else{
                    $("#add_master_modal").modal("hide");                    
                }
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }
    /***************************\                           
        Show Company list on page load           
    \***************************/
    GetCompanyList();    

    /***************************\                           
        Company Delete/Remove Confirm Button           
    \***************************/	
    $('#company_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("company_list_tbody");
        id = id.slice(0, -1);

        $.ajax({
            url: '/api/Companies?companyIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetCompanyList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#delete_master_modal').modal('toggle');

    });

    /***************************\                           
        Check if the Company is checked for delete/remove
    \***************************/
    $('.delete_master_btn').on('click', function (event) {
        let id = GetCheckedIds("company_list_tbody");
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }else{
            onCompanyInactiveClick();
            $('#delete_master_modal').modal('show');
        }
    });
    
});

/***************************\                           
    Get all the Company list from database.
\***************************/
function GetCompanyList(){
    $.getJSON('/api/Companies/')
    .done(function (data) {
        $('#company_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#company_list_tbody').append(`<tr><td><input type="checkbox" class="company_list_chk" data-id='${item.Id}' /></td><td>${item.CompanyName}</td></tr>`);
        });
    });
}
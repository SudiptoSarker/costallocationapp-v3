$(document).ready(function () {
    /***************************\                           
        Show Company list on page load           
    \***************************/
    GetCompanyList();    

    //Show Add Modal
    $(".add_master_btn").on("click",function(event){ 
        $("#company_name").val('');
        $('#add_master_modal').modal('show');
    })

    //Clear add modal input fields
    $("#undo_company_add_btn").on("click",function(event){        
        $("#company_name").val('');
    })

    //Clear edit modal input fields
    $("#undo_company_edit_btn").on("click",function(event){        
        $("#company_name_edit").val('');
    })

    //Add Company
    $("#company_reg_save_btn").on("click",function(event){       
        let company_name = $("#company_name").val().trim();        
        if (company_name == "") {
            alert("会社名を入力してください");
            return false;
        }

        //call add function
        UpdateInsertCompanies(company_name,0,false);
    })

    //edit Company
    $(".edit_master_btn").on("click",function(event){          
        let id = GetCheckedIds("company_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("会社名が選択されていません");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集の場合、複数の選択はできません");
            return false;
        }else{   
            FillTheEditModal(arrIds[0]);

            $('#edit_master_modal').modal('show');
        }        
    })
        
    //edit from modal
    $("#company_reg_save_btn_edit").on("click",function(event){   
        
        var roleName = $("#company_name_edit").val();   
        var roleId= $("#edit_role_id").val();   

        if (roleName == '' || roleName == null || roleName == undefined){
            alert("会社名を入力してください");
            return false;
        }
        else{            
            UpdateInsertCompanies(roleName,roleId,true);
        }        
    })
    
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
            alert("会社名が選択されていません");
            return false;
        }else{
            CompanyWithAssignments();
            $('#delete_master_modal').modal('show');
        }
    });
});

/***************************\                           
Delete confirmation function and shows the Company assignemnts with the selected Company        
\***************************/	
function CompanyWithAssignments() {
    let companyIds = GetCheckedIds("company_list_tbody");
    var apiurl = '/api/utilities/CompanyCount?companyIds=' + companyIds;
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

//get compnay details by company id and fill up the edit modal
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

/***************************\                           
    Company Add / Update
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
            ToastMessageFailed(data.responseJSON.Message);
        }
    });
}

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
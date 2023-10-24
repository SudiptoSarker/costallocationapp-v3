/***************************\                           
    Explanation In-Active/Remove     
\***************************/
function onExplanationInactiveClick() {
    let explanationIds = GetCheckedIds("explanations_list_tbody");
    var apiurl = '/api/utilities/ExplanationCount?roleIds=' + explanationIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.explanation_count').empty();
            $.each(data, function (key, item) {
                $('.explanation_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}

$(document).ready(function () {
    $(".add_explanation_btn").on("click",function(event){        
        $('#add_explanation_modal').modal('show');
    })
   
    $("#exp_reg_save_btn").on("click",function(event){       
        let explanation_name = $("#explanation_name").val().trim();        
        if (explanation_name == "") {
            alert("please enter explanation name!");
            return false;
        }

        UpdateInsertExplanations(explanation_name,0,false);
    })

    //edit incharge
    $(".edit_explanation_btn").on("click",function(event){          
        let id = GetCheckedIds("explanations_list_tbody");
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

            $('#edit_explanation_modal').modal('show');
        }        
    })
    function FillTheEditModal(explanationId){            
        var apiurl = `/api/utilities/GetExplanationNameByExplanationId`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "explanationId=" + explanationId,
            success: function (data) { 
                $("#explanation_name_edit").val(data.ExplanationName);   
                $("#hid_explanation_id").val(data.Id);   
            }
        });            
    }
    //edit from modal
    $("#exp_reg_save_btn_edit").on("click",function(event){   
        
        var explanation_name = $("#explanation_name_edit").val();   
        var explanation_id= $("#hid_explanation_id").val();   

        if (explanation_name == '' || explanation_name == null || explanation_name == undefined){
            alert("please enter explanation name!");
            return false;
        }
        else{
            UpdateInsertExplanations(explanation_name,explanation_id,true);
        }        
    })
    /***************************\                           
        Show Explanation list on page load           
    \***************************/
    GetExplanationList();    

    /***************************\                           
        Explanation Delete/Remove Confirm Button           
    \***************************/	
    $('#explanations_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("explanations_list_tbody");
        id = id.slice(0, -1);
        
        $.ajax({
            url: '/api/Explanations?explanationIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetExplanationList();               
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
        $('#inactive_explanation_modal').modal('toggle');
    });

    /***************************\                           
        Check if the Explanation is checked for delete/remove
    \***************************/
    $('.delete_role_btn').on('click', function (event) {
        let id = GetCheckedIds("explanations_list_tbody");        
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }else{
            onExplanationInactiveClick();
            $('#inactive_explanation_modal').modal('show');
        }
    });

});

/***************************\                           
    Explanation Insertion function. 
\***************************/ 
function UpdateInsertExplanations(explanationName,explanationId,isUpdate) {
    var apiurl = "/api/Explanations/";
    var data = {
        Id:explanationId,
        ExplanationName: explanationName,
        IsUpdate:isUpdate
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);
            GetExplanationList();     
            
            if(isUpdate){
                $("#edit_explanation_modal").modal('hide');
            }else{
                $("#add_explanation_modal").modal('hide');
            }            
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
}

/***************************\                           
    Get all the Explanation list from database.
\***************************/
function GetExplanationList(){
    $.getJSON('/api/Explanations/')
    .done(function (data) {
        $('#explanations_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#explanations_list_tbody').append(`<tr><td><input type="checkbox" class="explanation_list_chk" data-id='${item.Id}' /></td><td>${item.ExplanationName}</td></tr>`);
        });
    });
} 
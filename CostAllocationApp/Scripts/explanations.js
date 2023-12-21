$(document).ready(function () {
    // Show Add Explanation Modal
    $(".add_explanation_btn").on("click",function(event){      
        $("#explanation_name").val('');  
        $('#add_explanation_modal').modal('show');
    })

    //clear modal modal input field
    $("#undo_explation_add_btn").on("click",function(event){        
        $("#explanation_name").val('');
    })

    //clear edit modal input field. 
    $("#undo_explation_edit_btn").on("click",function(event){        
        $("#explanation_name_edit").val('');
    })
   
    //insert explations
    $("#exp_reg_save_btn").on("click",function(event){       
        let explanation_name = $("#explanation_name").val().trim();        
        if (explanation_name == "") {
            alert("説明文を入力してください");
            return false;
        }
        
        //call insert explanation function
        UpdateInsertExplanations(explanation_name,0,false);
    })
    
    //edit incharge
    $(".edit_explanation_btn").on("click",function(event){          
        let id = GetCheckedIds("explanations_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("説明文が選択されていません");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集の場合、複数の選択はできません");
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
            alert("説明文を入力してください");
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
                ToastMessageFailed("操作が失敗しました");
            }
        });
        $('#inactive_explanation_modal').modal('toggle');
    });

    /***************************\                           
        Check if the Explanation is checked for delete/remove
    \***************************/
    $('.delete_explanation_btn').on('click', function (event) {
        let id = GetCheckedIds("explanations_list_tbody");        
        if (id == "") {
            alert("説明文が選択されていません");
            return false;
        }else{
            ExplanationWithAssignment();
            $('#inactive_explanation_modal').modal('show');
        }
    });

});

/***************************\                           
Delete confirmation function and shows the Explanation assignemnts with the selected Explanation        
\***************************/	
function ExplanationWithAssignment() {
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

/***************************\                           
    Explanation Update / Insert
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
            ToastMessageFailed("操作が失敗しました");
        }
    });
}
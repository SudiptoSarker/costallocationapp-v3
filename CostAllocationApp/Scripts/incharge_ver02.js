/***************************\                           
    In-Charge In-Active/Remove 
    Also,shows that in how many projec that In-Charge is assigned                
\***************************/
function onInchargeInactiveClick() {
    let inChargeIds = GetCheckedIds("incharge_list_tbody");    
    var apiurl = '/api/utilities/InChargeCount?inChargeIds=' + inChargeIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.incharge_count').empty();
            $.each(data, function (key, item) {
                $('.incharge_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}

$(document).ready(function () {  
    
    $(".add_inchar_btn").on("click",function(event){        
        $('#add_incharge_modal').modal('show');
    })

    $("#undo_incharge_btn_edit").on("click",function(event){        
        $("#in_charge_name_edit").val('');
    })
    $("#undo_incharge_btn_add").on("click",function(event){        
        $("#in_charge_name").val('');
    })

    $("#incharge_reg_save_btn").on("click",function(event){         
        let inchargeName = $("#in_charge_name").val().trim();        
        if (inchargeName == "") {
            alert("please enter incharge!");
            return false;
        }

        UpdateInsertInCharge(inchargeName,0,false);
    })
    //edit incharge
    $(".edit_inchar_btn").on("click",function(event){          
        let id = GetCheckedIds("incharge_list_tbody");
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

            $('#edit_incharge_modal').modal('show');
        }        
    })
    //get section details by section id
    function FillTheEditModal(inchargeId){            
        var apiurl = `/api/utilities/GetInchargeNameByInchargeId`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "inchargeId=" + inchargeId,
            success: function (data) { 
                $("#in_charge_name_edit").val(data.InChargeName);   
                $("#edit_incharge_id").val(data.Id);   
            }
        });            
    }
    /***************************\                           
        Show In-Charge list on page load           
    \***************************/	   
    GetInchargeList();

    /***************************\                           
        In-Charge Delete/Remove Confirm Button           
    \***************************/	
    $('#incharge_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("incharge_list_tbody");

        id = id.slice(0, -1);
        $.ajax({
            url: '/api/InCharges?inChargeIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetInchargeList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#del_incharge_modal').modal('toggle');

    });

    /***************************\                           
        Check if the In-Charge is checked for delete/remove
    \***************************/
    $('.delete_inchar_btn').on('click', function (event) {

        let id = GetCheckedIds("incharge_list_tbody");
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        } 
        else{
            onInchargeInactiveClick();
            $('#del_incharge_modal').modal('show');
        }
    });
});

/***************************\                           
    In-Charge Insertion function. 
\***************************/
function UpdateInsertInCharge(in_charge_name,incharegeId,isUpdate) {
    var apiurl = "/api/incharges/";

    var data = {
        Id:incharegeId,
        InChargeName: in_charge_name,
        IsUpdate:isUpdate
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data)
            GetInchargeList();
            
            if(isUpdate){
                $("#edit_incharge_modal").modal("hide");
            }else{
                $("#add_incharge_modal").modal("hide");                
            }
        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });
}

/***************************\                           
    Get all the In-Charge list from database.
\***************************/
function GetInchargeList(){
    $.getJSON('/api/InCharges/')
    .done(function (data) {
        $('#incharge_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#incharge_list_tbody').append(`<tr><td><input type="checkbox" class="in_charge_list_chk" data-id='${item.Id}' /></td><td>${item.InChargeName}</td></tr>`);
        });
    });
}

//edit from modal
$("#incharge_reg_save_btn_edit").on("click",function(event){        
    var inchargeName = $("#in_charge_name_edit").val();   
    var incharegeId= $("#edit_incharge_id").val();   

    if (inchargeName == '' || inchargeName == null || inchargeName == undefined){
        alert("please enter incharge name!");
        return false;
    }
    else{
        UpdateInsertInCharge(inchargeName,incharegeId,true);
    }        
})
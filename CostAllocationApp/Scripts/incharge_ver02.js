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
    $('#incharge_inactive_btn').on('click', function (event) {

        let id = GetCheckedIds("incharge_list_tbody");
        if (id == "") {
            alert("Please check first to delete.");
            return false;
        } else {

        }
    });
});

/***************************\                           
    In-Charge Insertion function. 
\***************************/
function InsertInCharge() {
    var apiurl = "/api/incharges/";
    let in_charge_name = $("#in_charge_name").val().trim();

    /***************************\                           
        check In-Charge input field is empty or not. if empty then show error message.
    \***************************/
    if (in_charge_name == "") {
        $(".incharge_name_err").show();
        return false;
    }
    else {
        $(".incharge_name_err").hide();
        var data = {
            InChargeName: in_charge_name
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#in_charge_name").val('');
                ToastMessageSuccess(data)
                $('#section-name').val('');

                GetInchargeList();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
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
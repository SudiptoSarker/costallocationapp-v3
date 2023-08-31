/***************************\                           
    Section In-Active/Remove 
    Also,shows that in how many projec that section is assigned                
\***************************/	
function onSectionInactiveClick() {
    let sectionIds = GetCheckedIds("section_list_tbody");
    var apiurl = '/api/utilities/SectionCount?sectionIds=' + sectionIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.section_count').empty();
            $.each(data, function (key, item) {
                $('.section_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });  
}


$(document).ready(function () {

    /***************************\                           
        Show section list on page load           
    \***************************/	
    GetSectionList();

    /***************************\                           
        Section Delete/Remove Confirm Button           
    \***************************/	
    $('#section_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("section_list_tbody");

        var sectionWarningTxt = $("#section_warning_text").val();
        $("#section_warning").html(sectionWarningTxt);
        var tempVal = $("#section_warning").html();
        //alert(tempVal)
       

        id = id.slice(0, -1);
        $.ajax({
            url: '/api/sections?sectionIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetSectionList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
        $('#delete_section').modal('toggle');
    });
});

/***************************\                           
    Check if the section is checked for delete/remove
\***************************/
$('#section_inactive_btn').on('click', function (event) {
    let id = GetCheckedIds("section_list_tbody");
    if (id == "") {
        alert("Please check first to delete.");
        return false;
    }
});

/***************************\                           
    Section Insertion is done by this function. 
\***************************/
function InsertSection() {
    var apiurl = "/api/sections/";
    let sectionName = $("#section-name").val().trim();
    
    if (sectionName == "") {
        $(".section_name_err").show();
        return false;
    } else {
        $(".section_name_err").hide();
        var data = {
            SectionName: sectionName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess(data);

                $('#section-name').val('');
                GetSectionList();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

/***************************\                           
    Get all the section list from database.
\***************************/
function GetSectionList() {
    $.getJSON('/api/sections/')
        .done(function (data) {
            $('#section_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#section_list_tbody').append(`<tr><td><input type="checkbox" class="section_list_chk" onclick="GetCheckedIds(${item.Id});" data-id='${item.Id}' /></td><td>${item.SectionName}</td></tr>`);
            });
        });
}
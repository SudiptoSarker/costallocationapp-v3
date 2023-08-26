
$(document).ready(function () {
    /***************************\                           
        Show Category list on page load           
    \***************************/	
    GetCategoryList();

    /***************************\                           
        Category Delete/Remove Confirm Button           
    \***************************/	
    $('#category_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("category_list_tbody");

        var sectionWarningTxt = $("#category_warning_text").val();
        $("#category_warning").html(sectionWarningTxt);
        var tempVal = $("#category_warning").html();
        //alert(tempVal)
       

        id = id.slice(0, -1);
        $.ajax({
            url: '/api/Category?CategoryId=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetCategoryList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
        $('#delete_category').modal('toggle');
    });
});

/***************************\                           
    Check if the category is checked for delete/remove
\***************************/
$('#category_inactive_btn').on('click', function (event) {
    let id = GetCheckedIds("category_list_tbody");
    if (id == "") {
        alert("Please check first to delete.");
        return false;
    }
});

/***************************\                           
    category Insertion is done by this function. 
\***************************/
function InsertCategory() {
    var apiurl = "/api/Category/";
    let categoryName = $("#section-name").val().trim();
    
    if (categoryName == "") {
        $(".category_name_err").show();
        return false;
    } else {
        $(".category_name_err").hide();
        var data = {
            CategoryName: categoryName
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
                GetCategoryList();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

/***************************\                           
    Get all the category list from database.
\***************************/
function GetCategoryList() {
    $.getJSON('/api/Category/')
        .done(function (data) {
            $('#category_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#category_list_tbody').append(`<tr><td><input type="checkbox" class="section_list_chk" onclick="GetCheckedIds(${item.Id});" data-id='${item.Id}' /></td><td>${item.CategoryName}</td></tr>`);
            });
        });
}
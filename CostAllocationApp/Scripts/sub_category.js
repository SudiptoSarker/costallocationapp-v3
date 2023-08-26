/***************************\                           
    Department In-Active/Remove 
    Also,shows that in how many projec that department is assigned                
\***************************/	
function onDepartmentInactiveClick() {
    // let departmentIds = GetCheckedIds("subcategory_list_tbody");    
    // var apiurl = '/api/utilities/DepartmentCount?departmentIds=' + departmentIds;
    // $.ajax({
    //     url: apiurl,
    //     type: 'Get',
    //     dataType: 'json',
    //     success: function (data) {
    //         $('.department_count').empty();
    //         $.each(data, function (key, item) {
    //             $('.department_count').append(`<li class='text-info'>${item}</li>`);
    //         });
    //     },
    //     error: function (data) {
    //     }
    // });  
}

$(document).ready(function () {   
    /***************************\                           
        Show department list on page load           
    \***************************/	 
    GetSubCategory();
    GetCategoryList();

    /***************************\                           
        Department Delete/Remove Confirm Button           
    \***************************/	
    $('#department_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("subcategory_list_tbody");
        id = id.slice(0, -1);

        $.ajax({
            url: '/api/SubCategory?subCategoryId=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetSubCategory();
            },
            error: function (data) {
                ToastMessageFailed(data);

            }
        });

        $('#inactive_department_modal').modal('toggle');

    });
    /***************************\                           
        Check if the department is checked for delete/remove
    \***************************/
    $('#department_inactive_btn').on('click', function (event) {

        let id = GetCheckedIds("subcategory_list_tbody");        
        if (id == "") {
            alert("Please check first to delete.");
            return false;
        }
    });
});

/***************************\                           
    SubCategory Insertion function. 
\***************************/
function InsertSubCategory() {
    var apiurl = "/api/SubCategory/";
    let subCategoryName = $("#subCategory_name").val().trim();
    let categoryId = $("#category_list").val().trim();

    let isValidRequest = true;

    /***************************\                           
        check department input field is empty or not. if empty then show error message.
    \***************************/
    if (subCategoryName == "") {
        $(".sub_category_name_err").show();
        isValidRequest = false;
    } else {
        $(".sub_category_name_err").hide();
    }
    
    if (categoryId == "" || categoryId < 0) {
        $("#section_ist_error").show();
        isValidRequest = false;
    } else {
        $("#section_ist_error").hide();
    }

    if (isValidRequest) {
        var data = {
            SubCategoryName: subCategoryName,
            CategoryId: categoryId
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#subCategory_name").val('');
                $("#category_list").val('');

                ToastMessageSuccess(data);
                $('#section-name').val('');
                GetSubCategory();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

/***************************\                           
    Get all the SubCategory list from database.
\***************************/
function GetSubCategory(){
    $.getJSON('/api/SubCategory/')
    .done(function (data) {
        $('#subcategory_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#subcategory_list_tbody').append(`<tr><td><input type="checkbox" class="department_list_chk" data-id='${item.Id}' /></td><td>${item.SubCategoryName}</td><td>${item.CategoryName}</td></tr>`);
        });
    });
}
/***************************\                           
    Get all the SubCategory list from database.
\***************************/
function GetCategoryList(){
    $.getJSON('/api/Category/')
    .done(function (data) {
        $('#category_list').empty();
        $('#category_list').append(`<option value='-1'>Select Category</option>`)
        $.each(data, function (key, item) {
            $('#category_list').append(`<option value='${item.Id}'>${item.CategoryName}</option>`)
        });
    });
}
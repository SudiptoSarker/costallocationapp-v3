var deptList = [];

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
    GetDepartmentListWithSubCategory()

    //$(document).on('change', '#sub_category_list', function () {
    function GetDepartmentListWithSubCategory(){
        let sub_categoryId = 10;//$("#sub_category_list").val().trim();
        if (sub_categoryId == '' || sub_categoryId == null || sub_categoryId == undefined) {
            alert('Select sub category!!!');
            return false;
        } 

        $.ajax({
            url: `/api/utilities/GetAllUnAssignedDepartments/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "sub_categoryId=" + sub_categoryId,                
            success: function (data) {
                $('#unassigned_department_list').empty();
                $.each(data, function (key, item) {
                    $('#unassigned_department_list').append(`<option value="${item.Id}">${item.DepartmentName}</option>`);
                });
                $('#unassigned_department_list').selectpicker();
            }
        });
    }            
    
    $('#dept_reg_save_btn').on('click', function (event) { 

        var selectedDepartmentIds = $("#unassigned_department_list").val();    
        var departmentIds = "";
        $.each(selectedDepartmentIds, function (key, item) {
            if(departmentIds==""){
                departmentIds = item;
            }else{
                departmentIds = departmentIds+","+item;
            }
            
        });
        console.log(departmentIds);   
        
        var apiurl = "/api/DepartmentWithSubCategory/";
        let subCategoryId = $("#sub_category_list").val();
        let isValidRequest = true;
        
        if (subCategoryId == "") {
            $(".sub_category_name_err").show();
            isValidRequest = false;
        } else {
            $(".sub_category_name_err").hide();
        }

        if (isValidRequest) {
            var data = {
                DepartmentId: departmentIds,
                SubCategoryId: subCategoryId
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
                    GetDepartmentCategoryAndSubcategory();
                },
                error: function (data) {
                    alert(data.responseJSON.Message);
                }
            });
        }

    });


    //);

    /***************************\                           
        Show department list on page load           
    \***************************/	 
    GetDepartmentCategoryAndSubcategory();
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
                GetDepartmentCategoryAndSubcategory();
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
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
    });
});


/***************************\                           
    Get all the SubCategory list from database.
\***************************/
function GetDepartmentCategoryAndSubcategory(){
    $.getJSON('/api/DepartmentWithSubCategory/')
    .done(function (data) {
        $('#subcategory_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#subcategory_list_tbody').append(`<tr><td><input type="checkbox" class="department_list_chk" data-id='${item.Id}' /></td><td>${item.CategoryName}</td><td>${item.SubCategoryName}</td><td>${item.DepartmentName}</td></tr>`);
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

$(document).on('change', '#category_list', function () {
    
    let categoryId = $("#category_list").val().trim();
    if (categoryId == '' || categoryId == null || categoryId == undefined) {
        alert('Select category!!!');
        return false;
    } 

    $.getJSON(`/api/utilities/GetSubCategoryByCategoryId/${categoryId}`)
        .done(function (data) {
            $('#sub_category_list').empty();
            $('#sub_category_list').append(`<option value=''>Select Sub-Category</option>`);
            $.each(data, function (key, item) {
                $('#sub_category_list').append(`<option value='${item.Id}'>${item.SubCategoryName}</option>`);
            });
        });
});

  /***************************\                           
    SubCategory Insertion function. 
    \***************************/
    function InsertDeparytmentSubCategory() {
        var departmentValues = tempDepartments.getSelectedOptionsAsJson(includeDisabled = false);    
        console.log("d: "+departmentValues);
        return false;

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
                    GetDepartmentCategoryAndSubcategory();
                },
                error: function (data) {
                    alert(data.responseJSON.Message);
                }
            });
        }
    }
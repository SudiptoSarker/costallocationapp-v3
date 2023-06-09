﻿/***************************\                           
    Department In-Active/Remove 
    Also,shows that in how many projec that department is assigned                
\***************************/	
function onDepartmentInactiveClick() {
    let departmentIds = GetCheckedIds("department_list_tbody");    
    var apiurl = '/api/utilities/DepartmentCount?departmentIds=' + departmentIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.department_count').empty();
            $.each(data, function (key, item) {
                $('.department_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });  
}

$(document).ready(function () {   
    /***************************\                           
        Show department list on page load           
    \***************************/	 
    GetDepartments();
    

    /***************************\                           
        Department Delete/Remove Confirm Button           
    \***************************/	
    $('#department_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("department_list_tbody");
        id = id.slice(0, -1);

        $.ajax({
            url: '/api/departments?departmentIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetDepartments();
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

        let id = GetCheckedIds("department_list_tbody");        
        if (id == "") {
            alert("Please check first to delete.");
            return false;
        }
    });
});

/***************************\                           
    Department Insertion function. 
\***************************/
function InsertDepartment() {
    var apiurl = "/api/Departments/";
    let departmentName = $("#department_name").val().trim();

    let isValidRequest = true;

    /***************************\                           
        check department input field is empty or not. if empty then show error message.
    \***************************/
    if (departmentName == "") {
        $(".department_name_err").show();
        isValidRequest = false;
    } else {
        $(".department_name_err").hide();
    }
       
    if (isValidRequest) {
        var data = {
            DepartmentName: departmentName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#department_name").val('');
                $("#section_list").val('');

                ToastMessageSuccess(data);
                $('#section-name').val('');
                GetDepartments();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

/***************************\                           
    Get all the department list from database.
\***************************/
function GetDepartments(){
    $.getJSON('/api/departments/')
    .done(function (data) {
        $('#department_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#department_list_tbody').append(`<tr><td><input type="checkbox" class="department_list_chk" data-id='${item.Id}' /></td><td>${item.DepartmentName}</td></tr>`);
        });
    });
}
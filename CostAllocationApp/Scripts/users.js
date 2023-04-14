
$(document).ready(function () {
    $.getJSON('/api/Departments/')
    .done(function(data) {
        $('#userDepartment').append(`<option value=''>Select Department</option>`);
        $.each(data, function(key, item) {                    
            $('#userDepartment').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
        });
    });    
    // ("#userDepartment").select2();

    $('#example').DataTable();
    //------------------Employee Master----------------------//
    //show employee list on page load
    GetUserList();

    /***************************\                           
     Name Registration: get all the employees.                
    \***************************/
    //GetEmployeeListWithDropdownSearch();

    /***************************\                           
        Name Registration: namelist dropdown using select2 plugin.                
    \***************************/
    //$("#employee_list").select2();

});

//search by employee name
$(document).on('change', '#search_users', function () {
    GetEmployeeSearchResults();
});

//employee insert
function CreateUserName() {
    let userName = $("#userName").val().trim();
    let userTitle = $("#userTitle").val().trim();
    let departmentId = $("#userDepartment").val().trim();
    let userEmail = $("#userEmail").val().trim();
    let userPass = $("#userPass").val().trim();

    if (userName == "") {
        alert("please enter user name")
        return false;
    }
    if (userTitle == "") {
        alert("please enter title")
        return false;
    }
    if (userPass == "") {
        alert("please enter user password")
        return false;
    }

    var data = {
        UserName: userName,
        UserTitle: userTitle,
        DepartmentId: departmentId,
        Email: userEmail,
        Password: userPass
    };
    var apiurl = "/api/utilities/CreateUserName/";    
    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (result) {
            if (result > 0) {
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess('Data Save Successfully!');

                $('#userName').val('');
                GetUserList();
            }

        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });    
}

//Get employee list
function GetUserList() {
    $.getJSON('/api/utilities/GetUserList/')
    .done(function (data) {
        ShowUserList_Datatable(data);
    });

    
}

/***************************\                           
 Showing namelist using datatable.                        
\***************************/
function ShowUserList_Datatable(data){	
    $('#employeeList_datatable').DataTable({
        destroy: true,
        data: data,
        ordering: false,
        orderCellsTop: false,
        pageLength: 100,
        searching: false,
        bLengthChange: false,    
        //dom: 'lifrtip',
        columns: [                        
            {
                data: 'UserName'
            },
            {
                data: 'UserTitle'
            },
            {
                data: 'DepartmentName'
            },
            {
                data: 'Email'
            },
            {
                data: 'Password'
            }
        ]
    });
}

// $(function () {

//     var employeeContextMenu = $("#employeeContextMenu");

//     $("body").on("contextmenu", "#employeeList_datatable tbody tr", function (e) {
//         employeeContextMenu.css({
//             display:'block',
//             left: e.pageX-230,
//             top: e.pageY-25
//         });
//         $('#employee_id_hidden').val($(this)[0].cells[0].innerText);
//         $('#employee_name_edit').val($(this)[0].cells[1].innerText);
//         //debugger;
//         return false;
//     });

//     $('html').click(function () {
//         employeeContextMenu.hide();
//     });

//     $("#employeeContextMenu li a").click(function (e) {
//         var f = $(this);
//         var elementText = f[0].innerText;
//         if (elementText.toLowerCase() == 'edit') {
//             $('#edit_employee_modal').modal();
//         }
//         if (elementText.toLowerCase() == 'inactive') {
//             $('#inactive_employee_modal').modal();
//         }
        
//         //debugger;
//     });
// });

//employee update
function UpdateEmployee() {
    var apiurl = "/api/utilities/UpdateEmployee/";
    let employeeName = $("#employee_name_edit").val().trim();
    let employeeId = $('#employee_id_hidden').val();
    if (employeeName == "") {
        $(".employee_err_edit").show();
        return false;
    } else {
        $(".employee_err_edit").hide();
        var data = {
            Id: employeeId,
            FullName: employeeName
        };

        $.ajax({
            url: apiurl,
            type: 'PUT',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess(data);
                $('#edit_employee_modal').modal('hide');
                GetUserList();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}


// inactive employee
function InactiveEmployee() {
    var apiurl = "/api/utilities/InactiveEmployee/";
    let employeeId = $('#employee_id_hidden').val();
    var data = {
        Id: employeeId
    };
    $.ajax({
        url: apiurl,
        type: 'DELETE',
        dataType: 'json',
        data: data,
        success: function (data) {
            $("#page_load_after_modal_close").val("yes");
            ToastMessageSuccess(data);
            $('#inactive_employee_modal').modal('hide');
            GetUserList();
            
            
        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });
}
function GetEmployeeSearchResults() {
    var employeeName = $('#search_users').val();
    if (employeeName == '') {
        employeeName = 'all';
    }
    $.ajax({
        url: `/api/utilities/EmployeeListByNameFilter/${employeeName}`,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowUserList_Datatable(data)
        },
        error: function () {            
        }
    });
}
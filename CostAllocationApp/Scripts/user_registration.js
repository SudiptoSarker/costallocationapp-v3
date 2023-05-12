$.getJSON('/api/Departments/')
.done(function(data) {
    $('#userDepartment').append(`<option value=''>Select Department</option>`);
    $.each(data, function(key, item) {                    
        $('#userDepartment').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
    });
    });  

//clear entry field
function ClearEntry(){
    $("#userName").val('');
    $("#userTitle").val('');
    $("#userDepartment").val('');
    $("#userEmail").val('');
    $("#userPass").val('');
    $("#hid_userRole").val('');
}
//user create
function UserRegistration() {
    let userName = $("#userName").val().trim();
    let userTitle = $("#userTitle").val().trim();
    let departmentId = $("#userDepartment").val().trim();
    let userEmail = $("#userEmail").val().trim();
    let userPass = $("#userPass").val().trim();
    let userRoleId = $("#hid_userRole").val().trim();

    if (userName == "") {
        alert("please enter user name")
        return false;
    }
    if (userTitle == "") {
        alert("please enter title")
        return false;
    }
    if (departmentId == "") {
        alert("please select departmnet")
        return false;
    }
    
    if (userPass == "") {
        alert("please enter user password")
        return false;
    }
    if (userEmail == "") {
        alert("please enter email")
        return false;
    }
    var data = {
        UserName: userName,
        UserTitle: userTitle,
        DepartmentId: departmentId,
        Email: userEmail,
        Password: userPass,
        UserRoleId: userRoleId,
    };
    var apiurl = "/api/utilities/CreateUserName/";    
    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (result) {
            if (result > 0) {
                ClearEntry();
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess('Registration is completed, please wait for the approval!');

                $('#userName').val('');
                //GetUserList();
            }
            $('#register_modal').modal('hide');
        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });    
}
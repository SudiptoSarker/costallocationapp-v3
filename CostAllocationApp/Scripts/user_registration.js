$(document).ready(function () {        
    $('#department_list').select2();      

    $.getJSON('/api/Departments/')
    .done(function(data) {
        $('#department_list').append(`<option value=''>部署を選択</option>`);
        $.each(data, function(key, item) {                    
            $('#department_list').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
        });
    });  

    //clear input field value
    $(document).on('click','.user_edit_undo_btn',function(){
        ClearEntry();
    });  
    function ClearEntry(){
        $("#user_name_reg").val('');
        $("#reg_user_title").val('');        
        $("#department_list").val('').trigger('change');  
        $("#reg_user_email").val('');
        $("#reg_user_pass").val('');
    }
    //create new user
    $(document).on('click','#register_user',function(){
        let userName = $("#user_name_reg").val().trim();
        let userTitle = $("#reg_user_title").val().trim();
        let departmentId = $("#department_list").val().trim();
        let userEmail = $("#reg_user_email").val().trim();
        let userPass = $("#reg_user_pass").val().trim();
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
                    //ToastMessageSuccess('Registration is completed, please wait for the approval!');                
                    $('#user_name_reg').val('');
                    //GetUserList();
                    $('#register_modal').modal('hide');
                    $('#register_modal').on('hidden.bs.modal', function () {
                        // Load up a new modal...
                        $('#registration_success').modal('show')
                    })
                }
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });    
    });
});
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

    var userName = "";
    var userTitle = "";
    var departmentId = "";
    var userEmail = "";
    var userPass = "";
    var userRoleId = "";

    function ValidateRegistration(){
        var isValid = true;
        userName = $("#user_name_reg").val();
        userTitle = $("#reg_user_title").val();
        departmentId = $("#department_list").val();
        userEmail = $("#reg_user_email").val();
        userPass = $("#reg_user_pass").val();
        userRoleId = $("#hid_userRole").val();


        var isUserNameEmpty = false;
        var isUserTitleEmpty = false;
        var isUserDeptEmpty = false;
        var isUserEmailEmpty = false;
        var isUserPassEmpty = false;

        if (userName == '' || userName == null || userName == undefined){            
            isUserNameEmpty = true;
            $("#name_lbl").css("display", "block");                   
            isValid =  false;
        }else{
            isUserNameEmpty = false;
            $("#name_lbl").css("display", "none");
        }

        if (userTitle == '' || userTitle == null || userTitle == undefined){            
            isUserTitleEmpty = true;
            $("#title_lbl").css("display", "block");
            isValid =  false;
        }else{
            isUserTitleEmpty = false;
            $("#title_lbl").css("display", "none");
        }

        if (departmentId == '' || departmentId == null || departmentId == undefined){  
            isUserDeptEmpty = true;          
            $("#department_lbl").css("display", "block");
            isValid =  false;
        }else{
            isUserDeptEmpty = false;
            $("#department_lbl").css("display", "none");
        }

        if (userEmail == '' || userEmail == null || userEmail == undefined){ 
            isUserEmailEmpty = true;           
            $("#email_lbl").css("display", "block");
            isValid =  false;
        }else{
            var isValidEmail = isEmail(userEmail);
            if(!isValidEmail){
                isUserEmailEmpty = true;           
                $("#email_lbl").css("display", "block");
                isValid =  false;
            }else{
                isUserEmailEmpty = false;
                $("#email_lbl").css("display", "none");
            }            
        }

        if (userPass == '' || userPass == null || userPass == undefined){     
            isUserPassEmpty = true;       
            $("#pass_lbl").css("display", "block");
            isValid =  false;
        }else{
            isUserPassEmpty = false;
            $("#pass_lbl").css("display", "none");
        }

        if(isUserNameEmpty){
            $("#user_name_reg").focus();
        }
        else if(isUserTitleEmpty){
            $("#reg_user_title").focus();
        }
        else if(isUserDeptEmpty){
            $("#department_list").focus();
        }
        else if(isUserEmailEmpty){
            $("#reg_user_email").focus();
        }
        else if(isUserPassEmpty){
            $("#reg_user_pass").focus();
        }

        return isValid;
    }
    //email validation
    function isEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }
    //password validation
    $(".toggle-password").click(function() {
        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });

    $(document).on('click','.user_edit_update_btn',function(){  
        var isValid = false;
        isValid = ValidateRegistration();

        if(isValid){
            $("#register_modal").modal('show');
        }else{
            $("#register_modal").modal('hide');
        }
    }); 
    //create new user
    $(document).on('click','#register_user',function(){
        userName = userName.trim();
        userTitle = userTitle.trim();
        departmentId = departmentId.trim();
        userEmail = userEmail.trim();
        userPass = userPass.trim();
        userRoleId = userRoleId.trim();    
              
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
                        alert("Registration is completed.Please login.");
                        window.location.href = "/Registration/Login";

                        // Load up a new modal...
                        //$('#registration_success').modal('show')
                    })
                }
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });    
    });
});
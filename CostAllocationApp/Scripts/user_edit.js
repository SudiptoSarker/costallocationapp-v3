$(document).ready(function () {    
    //get user role type on page load 
    var userRoleType = "";
    userRoleType = GetUserRoleType();
    
    if(userRoleType !=''){
        if(userRoleType=='editor'){
            $(".user_name_list_header").text("Editor's Information - 編集者情報");
        }else if(userRoleType=='visitor'){
            $(".user_name_list_header").text("Visitor's Information - 来場者情報");
        }
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
    
    //admin information shows
    $.getJSON('/api/utilities/GetOnlyAdmin/')
        .done(function (data) {
            $('#admin_table tbody').empty();
            $('#admin_table tbody').append(`<tr><td>${data.UserName}</td><td>${data.UserRoleName}</td><td>${data.UserTitle}</td><td>${data.DepartmentName}</td><td>${data.Email}</td><td>${data.Password}</td><td><button class="btn btn-info user_edit_button" user_name='${data.UserName}' user_status='active' >編集</button></td></tr>`);
        });
      
    //show employee list on page load
    GetUserList();

    $('#user_role_list').select2();       
    $('#user_department_edit').select2();  
    $('#user_status_edit').select2();  

    LoadModalDynamicValues();

    //show user edit modal and load the informations
    $(document).on('click', '.user_edit_button', function () {            
        var userName = $(this).attr("user_name");
        var user_status = $(this).attr("user_status");        
        
        var user_status_val = 0;
        if(user_status=="active"){        
            user_status_val =1;
        }else if(user_status=="inactive"){            
            user_status_val =0;
        }else{
            user_status_val =3;
        }

        $.getJSON('/api/utilities/GetSingleUserInfo?userName=' + userName)
            .done(function (data) {
                $('#user_id_hidden').val(data.Id);
                $('#edit_user_name').val(data.UserName);
                $('#edit_user_title').val(data.UserTitle);                                
                $("#user_department_edit").val(data.DepartmentId).trigger('change');   
                
                if(user_status=="waiting"){                                        
                    $("#user_role_list").val(-1).trigger('change');   
                }else{                                        
                    $("#user_role_list").val(data.UserRoleId).trigger('change');   
                } 

                $('#edit_user_email').val(data.Email); 
                 
                $("#user_status_edit").val(user_status_val).trigger('change');                   

                $('#edit_user_email').val(data.Email);                
                $('#edit_user_pass').val(data.Password);
            }); 
    
        $('#modal_update_user').modal('show');
    });        

    //update user informations
    $(document).on('click', '.user_edit_update_btn', function () {            
        let userId = $("#user_id_hidden").val();
        let userName = $("#edit_user_name").val();        
        let userTitle = $("#edit_user_title").val();
        let departmentId = $("#user_department_edit").val();
        let userEmail = $("#edit_user_email").val();
        let userPass = $("#edit_user_pass").val();
        let userRoleId = $("#user_role_list").val();
        let userStatus = $("#user_status_edit").val();
        let isActive = true;

        if (userId == '' || userId == null || userId == undefined){
            alert("user id is empty!")
            return false;
        }else{
            userId = userId.trim();
        }        
        if (userName == '' || userName == null || userName == undefined){
            alert("利用者名を入力してください")
            return false;
        }else{
            userName = userName.trim();
        }
        if (userTitle == '' || userTitle == null || userTitle == undefined){
            alert("利用者の役職を入力してください");
            return false;
        }else{
            userTitle = userTitle.trim();
        }
        if (departmentId == '' || departmentId == null || departmentId == undefined){
            alert("利用者の部署名を選択してください");
            return false;
        }else{
            departmentId = departmentId.trim();
        }
        if (userPass == '' || userPass == null || userPass == undefined){
            alert("利用者のパスワードを入力してください");
            return false;
        }else{
            userPass = userPass.trim();
        }
        if (userRoleId == '' || userRoleId == null || userRoleId == undefined || userRoleId == "-1"){
            alert("利用者の役割を選択してください");
            return false;
        }else{
            userRoleId = userRoleId.trim();
        }
        if (userStatus == '' || userStatus == null || userStatus == undefined || userStatus == "-1"){
            alert("利用者のステータスを選択してください");
            return false;
        }else{
            userStatus = userStatus.trim();
        }
        
        if(userStatus==3){
            userRoleId = 0;
        }else if(userStatus==0){
            isActive = false;
        }

        var data = {
            Id: userId,
            UserName: userName,
            UserTitle: userTitle,
            DepartmentId: departmentId,
            Email: userEmail,
            Password: userPass,
            UserRoleId: userRoleId,
            IsActive: isActive,
        };
        var apiurl = "/api/utilities/UpdateUserName/";

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result > 0) {
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('データが保存されました!');
                    $('#modal_update_user').modal('hide');
                    $('#userName').val('');
                    GetUserList();

                    $.getJSON('/api/utilities/GetOnlyAdmin/')
                        .done(function (data) {
                            var user_status;
                            if((data.UserRoleId ==1 || data.UserRoleId ==2 || data.UserRoleId ==3 ) && data.IsActive){
                                user_status = "active";
                            }
                            else if((data.UserRoleId ==1 || data.UserRoleId ==2 || data.UserRoleId ==3 ) && !data.IsActive){
                                user_status = "inactive";
                            }else{
                                user_status = "waiting";
                            }
                            $('#admin_table tbody').empty();
                            $('#admin_table tbody').append(`<tr><td>${data.UserName}</td><td>${data.UserRoleName}</td><td>${data.UserTitle}</td><td>${data.DepartmentName}</td><td>${data.Email}</td><td>${data.Password}</td><td><button class="btn btn-info user_edit_button" user_name='${data.UserName}' user_status='${user_status}'>編集</button></td></tr>`);
                        }); 
                }
                location.reload();
            },
            error: function (data) {
                alert("失敗した");
            }
        });
    });   
    
    //clear user update modal input field clean
    $(document).on('click', '.user_edit_undo_btn', function (){
        ClearEntry();
    });  
    
    // Search user by input box
    $(document).on('click', '#filterEmp', function () {
        var selectSearchOption = $("input[name='searchUser']:checked");
        if (selectSearchOption.length > 0) {
            let searchOption = selectSearchOption.val();
            let searchBy = $('#inputEmpName').val();
            GetSearchUserList(searchOption, searchBy);
        }
    });

    // Sort user list
    $(document).on('click', '#sortUserBtn', function () {
        var selectOrderOption = $("input[name='orderUser']:checked");
        if (selectOrderOption.length > 0) {
            let orderOption = selectOrderOption.val();
            $.getJSON('/api/utilities/GetUserList?orderBy=' + orderOption + "&orderType=" + "asc")
                .done(function (data) {
                    ShowUserList_Datatable(data);
                })
                .fail(function () {
                    ToastMessage_Warning("検索条件に合ったデータはありません");
                    console.log("error");
                }); 
        }
    });

    // Filter User list
    $(document).on('click', '#filterUserBtn', function () {
        var selectedFilterRole = $('#searchRole').val();
        var selectedFilterTitle = $('#searchTitle').val();
        var selectedFilterDepartment = $('#searchDepartment').val();
        var selectedFilterStatus = $('#searchStatus').val();
        let filterArray = [];
        if (selectedFilterRole != '') filterArray.push("filterRole="+selectedFilterRole);
        if (selectedFilterTitle != '') filterArray.push("filterTitle=" +selectedFilterTitle);
        if (selectedFilterDepartment != '') filterArray.push("filterDepartment=" +selectedFilterDepartment);
        if (selectedFilterStatus != '') filterArray.push("filterStatus=" + selectedFilterStatus);
        let filterString = filterArray.join("&");

        $.getJSON('/api/utilities/GetFilterUserList?' + filterString)
            .done(function (data) {
                ShowUserList_Datatable(data);
            })
            .fail(function () {
                ToastMessage_Warning("追加、修正していないデータがありません");
                console.log("error");
            });    
    });

    //status change info save to hidden field.
    var changeUserName = "";
    var changeUserRole = "";
    var changeUserStatus = "";

    $(document).on('change', '.change_status', function () {
        $('#user_status_modal').modal('show');

        changeUserName = $(this).closest('tr').find('td:eq(0)').text();
        changeUserRole = $(this).closest('tr').find('td:eq(1)').text();
        changeUserStatus =  $(this).val();

        $("#hid_changed_user_name").val(changeUserName);
        $("#hid_changed_user_role").val(changeUserRole);
        $("#hid_changed_user_status").val(changeUserStatus);
        
    });
});

//get user role 
function GetUserRoleType(){        
    $.ajax({
        url: `/Registration/GetUserRoleType/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userRoleType = data;
        }
    });

    return userRoleType;
}

//get user information for user edit modal
function LoadModalDynamicValues(){
    $.getJSON('/api/Departments/')
    .done(function(data) {
        $('#user_department_edit').append(`<option value=''>部署を選択</option>`);
        $('#searchDepartment').append(`<option value=''>部署を選択</option>`);
        $.each(data, function(key, item) {                    
            $('#user_department_edit').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
            $('#searchDepartment').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
        });
    });   

    $.getJSON('/api/utilities/GetAllUserRoles/')
    .done(function (data) {
        $('#user_role_list').append(`<option value=''>Select Role</option>`);
        $('#searchRole').append(`<option value=''>役割の選択</option>`);
        $.each(data, function (key, item) {              
            $('#user_role_list').append(`<option value='${item.Id}'>${item.Role}</option>`);
            $('#searchRole').append(`<option value='${item.Id}'>${item.Role}</option>`);
        });
    }); 

    $.getJSON('/api/utilities/GetUserList/')
        .done(function (data) {
            let userTitleList = [];
            $('#searchTitle').append(`<option value=''>役職の選択</option>`);
            $.each(data, function (key, value) {
                userTitleList.push(value.UserTitle);
            });
            newUserTitleList = userTitleList.filter(function (elem, index, self) {
                                return index === self.indexOf(elem);
                            });
            $.each(newUserTitleList, function (key, value) {
                if (value !== "") {
                    $('#searchTitle').append(`<option value='${value}'>${value}</option>`);
                }                    
            });

            $('#searchStatus').append(`<option value=''>ステータスの選択</option>`);
            $('#searchStatus').append(`<option value='1'>有効(Active)</option>`);
            $('#searchStatus').append(`<option value='3'>承認待ち(waiting)</option>`);
            $('#searchStatus').append(`<option value='0'>無効(Inactive)</option>`);
    }); 
}

//clean user inputs
function ClearEntry(){
    $("#edit_user_name").val('');
    $("#edit_user_title").val('');        
    $("#user_department_edit").val('').trigger('change');  
    $("#edit_user_email").val('');
    $("#edit_user_pass").val('');
    $("#user_role_list").val('').trigger('change');  
    $("#user_status_edit").val('').trigger('change');  
}

//Get searched user list
function GetSearchUserList(searchOption, searchBy) {
    $.getJSON('/api/utilities/GetSearchUserList?searchOption=' + searchOption + "&searchBy=" + searchBy)
        .done(function (data) {
            ShowUserList_Datatable(data);
        })
        .fail(function () {
            ToastMessage_Warning("検索条件に合ったデータはありません");
            console.log("error");
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
 Employee list datatable      
\***************************/
function ShowUserList_Datatable(data) {   
    var user_name;
    var user_status;

    var selectPageLength = $("select[name='employeeList_datatable_length']").val();
    selectPageLength = selectPageLength != null ? selectPageLength : 10;

    var custome_table = $('#user_list').DataTable({                             
        destroy: true,
        data: data,
        ordering: false,
        orderCellsTop: false,
        pageLength: selectPageLength,
        searching: true,
        columns: [  
            {
                data: 'UserName',
                render: function (data) {
                    user_name = data;
                    return data;
                }
            },
            {
                data: 'UserRoleName'
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
                data: 'Status',
                render: function (data) {
                    var role_and_status = data.split("_");

                    var strDropdown = "";
                    var statusText = ""
                    strDropdown = "<select class='change_status'>";
                    if(role_and_status[0].toLowerCase() == 'valid'){
                        if(role_and_status[1].toLowerCase() == 'true'){
                            user_status = "active";
                            strDropdown = strDropdown+"<option selected='selected' value='1'>有効(Active)</option>";
                            strDropdown = strDropdown+"<option value='3'>承認待ち(waiting)</option>";
                            strDropdown = strDropdown+"<option value='0'>無効(Inactive)</option>";
                            statusText = "<span style='color:green;'>有効(Active)</span>";
                        }else{
                            user_status = "inactive";
                            strDropdown = strDropdown+"<option value='1'>有効(Active)</option>";
                            strDropdown = strDropdown+"<option value='3'>承認待ち(waiting)</option>";
                            strDropdown = strDropdown+"<option selected='selected' value='0'>無効(Inactive)</option>";
                            statusText = "<span style='color:red;'>無効(Inactive)</span>";
                        }
                    }else{
                        if(role_and_status[1].toLowerCase() == 'true'){
                            user_status = "waiting";
                            strDropdown = strDropdown+"<option value='1'>有効(Active)</option>";
                            strDropdown = strDropdown+"<option selected='selected' value='3'>承認待ち(waiting)</option>";
                            strDropdown = strDropdown+"<option value='0'>無効(Inactive)</option>";
                            statusText = "<span style='color:darkorange;' class='blink_me'>承認待ち(waiting)</span>";
                        }else{
                            user_status = "inactive";
                            strDropdown = strDropdown+"<option value='1'>有効(Active)</option>";
                            strDropdown = strDropdown+"<option value='3'>承認待ち(waiting)</option>";
                            strDropdown = strDropdown+"<option selected='selected' value='0'>無効(Inactive)</option>";
                            statusText = "<span style='color:red;'>無効(Inactive)</span>";
                        }                        
                    }                    
                    strDropdown = strDropdown +"</select>";

                    return statusText;
                }
            },
            {
                data: 'Password'
            },
            {
                render: function () {
                    return `<button class="btn btn-info user_edit_button" user_name='${user_name}' user_status='${user_status}'>編集</button>`;
                }
            }
        ]
        ,
        initComplete: function () {
            var r = $('#user_list tfoot tr');
            $('#user_list thead').append(r);            
        },         
    });

    $('#search_user_name').on( 'keyup', function () {
        custome_table.search( this.value ).draw();
    } );
    $('#search_user_role').on( 'keyup', function () {
        custome_table.search( this.value ).draw();
    } );
    $('#search_user_title').on( 'keyup', function () {
        custome_table.search( this.value ).draw();
    } );
    $('#search_user_department').on( 'keyup', function () {
        custome_table.search( this.value ).draw();
    } );
    $('#search_user_email').on( 'keyup', function () {
        custome_table.search( this.value ).draw();
    } );
    $('#search_user_status').on( 'keyup', function () {
        custome_table.search( this.value ).draw();
    } );         
}
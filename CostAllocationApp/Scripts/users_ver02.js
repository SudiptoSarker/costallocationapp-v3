$(document).ready(function () {    
    $('#user_role_list').select2();       
    $('#user_department_edit').select2();  
    $('#user_status_edit').select2();  

    LoadModalDynamicValues();

    $(document).on('click', '.user_edit_button', function () {    
        $('#user_role_list').select2();
        $('#user_status_edit').select2();

        var userName = $(this).attr("user_name");
        var user_status = $(this).attr("user_status");        


        if(user_status=="active"){        
            $("#user_status_edit").val(1);             
        }else if(user_status=="inactive"){            
            $("#user_status_edit").val(0);                         
        }else{
            $("#user_status_edit").val(3);                                     
        }


        $.getJSON('/api/utilities/GetSingleUserInfo?userName=' + userName)
            .done(function (data) {
                $('#user_id_hidden').val(data.Id);
                $('#edit_user_name').val(data.UserName);
                $('#edit_user_title').val(data.UserTitle);                
                $("#user_department_edit").select2("val", data.DepartmentId);            
                if(user_status=="waiting"){                    
                    $("#user_role_list").select2("val", -1);
                }else{                    
                    $("#user_role_list").select2("val", data.UserRoleId);                
                }                

                $('#edit_user_email').val(data.Email);                
                $('#edit_user_pass').val(data.Password);
            }); 
    
        $('#modal_update_user').modal('show');
    });
      
    function LoadModalDynamicValues(){
        $.getJSON('/api/Departments/')
        .done(function(data) {
            $('#user_department_edit').append(`<option value=''>部署を選択</option>`);
            $.each(data, function(key, item) {                    
                $('#user_department_edit').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
            });
            });   

        $.getJSON('/api/utilities/GetAllUserRoles/')
        .done(function (data) {
            $('#user_role_list').append(`<option value=''>Select Role</option>`);
            $.each(data, function (key, item) {
                $('#user_role_list').append(`<option value='${item.Id}'>${item.Role}</option>`);
            });


            // $('#user_status_edit').append(`<option value=''>select status</option>`);
            // $('#user_status_edit').append(`<option value='1'>有効(Active)</option>`);
            // $('#user_status_edit').append(`<option value='3'>承認待ち(waiting)</option>`);
            // $('#user_status_edit').append(`<option value='0'>無効(Inactive)</option>`);
        });         
    }

    $(document).on('click', '.user_edit_update_btn', function () {    
        

        let userId = $("#user_id_hidden").val().trim();
        let userName = $("#edit_user_name").val().trim();        
        let userTitle = $("#edit_user_title").val().trim();
        let departmentId = $("#user_department_edit").val().trim();
        let userEmail = $("#edit_user_email").val().trim();
        let userPass = $("#edit_user_pass").val().trim();
        let userRoleId = $("#user_role_list").val().trim();
        let userStatus = $("#user_status_edit").val().trim();
        let isActive = true;

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
        if (userRoleId == "" || userRoleId == "-1") {
            alert("please select user role")
            return false;
        }
        if (userStatus == "" || userStatus == "-1") {
            alert("please select user status")
            return false;
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
                            //UserRoleId: "1"
                            //IsActive: true
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
                            $('#admin_table tbody').append(`<tr><td>${data.UserName}</td><td>${data.UserRoleName}</td><td>${data.UserTitle}</td><td>${data.DepartmentName}</td><td>${data.Email}</td><td>${data.Password}</td><td><button class="btn btn-info user_edit_button" onclick="UpdateUserModal('${data.UserName}','${user_status}')">編集</button></td></tr>`);
                        }); 
                }
                location.reload();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    });    
});

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

$(document).on('click', '#btn_status_change', function () {    
    $.getJSON('/api/utilities/UpdateUserStatus?changeUserName=' + changeUserName+"&changeUserRole="+changeUserRole+"&changeUserStatus="+changeUserStatus)
        .done(function (data) {
            location.reload();
            $('#user_status_modal').modal('hide');
        }); 
});

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
function ShowUserList_Datatable(data) {
    // var table = $('#employeeList_datatable').DataTable({ 
    //     initComplete: function () {
    //         this.api().columns().every( function () {
    //             var column = this;
    //             var select = $('<select><option value=""></option></select>')
    //                 .appendTo( $(column.footer()).empty() )
    //                 .on( 'change', function () {
    //                     var val = $.fn.dataTable.util.escapeRegex(
    //                         $(this).val()
    //                     );

    //                     column
    //                         .search( val ? '^'+val+'$' : '', true, false )
    //                         .draw();
    //                 } );

    //             column.data().unique().sort().each( function ( d, j ) {
    //                 select.append( '<option value="'+d+'">'+d+'</option>' )
    //             } );
    //         } );
    //     },
    //     responsive: true,
    //     "processing": true,
    //     "pageLength": 25,
    //     "bFilter":   false,

    //     "columnDefs": [
    //         {
    //             "targets": [ -1 ],
    //             "visible": false,
    //             "searchable": false
    //         }

    //     ],


    //     //This adds the Bootstrap alert class, if there is one in the last column
    //     "createdRow": function( row, data, dataIndex ) {

    //         /*console.log(data);*/

    //         if ( data[data.length-1] != '' ) {
    //             $(row).addClass( data[data.length-1] );
    //         }
    //     }




    // });

    // new $.fn.dataTable.FixedHeader( table );
    // $('#loader').hide();
    // $('#lookupTable').show();

    var user_name;
    var user_status;

    //var custome_table = $('#employeeList_datatable').DataTable();
    
    // #myInput is a <input type="text"> element    

    var custome_table = $('#employeeList_datatable').DataTable({                               
        // destroy: true,
        // data: data,
        // ordering: true,
        // orderCellsTop: true,
        // pageLength: 100,
        // filter: true,
        // bLengthChange: true,    
        // searching: true, 
        // paging: true, 
        // info: false,

        destroy: true,
        data: data,
        ordering: true,
        orderCellsTop: false,
        pageLength: 10,
        searching: true,
        //bLengthChange: false,  
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
                    return `<button class="btn btn-info user_edit_button" onclick="UpdateUserModal('${user_name}','${user_status}')">編集</button>`;
                }
            }
        ]
        ,
        initComplete: function () {
            var r = $('#employeeList_datatable tfoot tr');
            $('#employeeList_datatable thead').append(r);
            // Apply the search
            // this.api()
            //     .columns()
            //     .every(function () {
            //         var that = this; 
            //         $('input', this.footer()).on('keyup change clear', function () {
            //             if (that.search() !== this.value) {
            //                 that.search(this.value).draw();
            //             }
            //         });
            //     });
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
    

    // $('#employeeList_datatable').DataTable({                               
    //     // destroy: true,
    //     // data: data,
    //     // ordering: true,
    //     // orderCellsTop: true,
    //     // pageLength: 100,
    //     // filter: true,
    //     // bLengthChange: true,    
    //     // searching: true, 
    //     // paging: true, 
    //     // info: false,

    //     destroy: true,
    //     data: data,
    //     ordering: true,
    //     orderCellsTop: false,
    //     pageLength: 100,
    //     searching: true,
    //     //bLengthChange: false,  
    //     columns: [  
    //         {
    //             data: 'UserName',
    //             render: function (data) {
    //                 user_name = data;
    //                 return data;
    //             }
    //         },
    //         {
    //             data: 'UserRoleName'
    //         },
    //         {
    //             data: 'UserTitle'
    //         },
    //         {
    //             data: 'DepartmentName'
    //         },
    //         {
    //             data: 'Email'
    //         },
    //         {
    //             data: 'Status',
    //             render: function (data) {
    //                 var role_and_status = data.split("_");

    //                 var strDropdown = "";
    //                 var statusText = ""
    //                 strDropdown = "<select class='change_status'>";
    //                 if(role_and_status[0].toLowerCase() == 'valid'){
    //                     if(role_and_status[1].toLowerCase() == 'true'){
    //                         user_status = "active";
    //                         strDropdown = strDropdown+"<option selected='selected' value='1'>有効(Active)</option>";
    //                         strDropdown = strDropdown+"<option value='3'>承認待ち(waiting)</option>";
    //                         strDropdown = strDropdown+"<option value='0'>無効(Inactive)</option>";
    //                         statusText = "<span style='color:green;'>有効(Active)</span>";
    //                     }else{
    //                         user_status = "inactive";
    //                         strDropdown = strDropdown+"<option value='1'>有効(Active)</option>";
    //                         strDropdown = strDropdown+"<option value='3'>承認待ち(waiting)</option>";
    //                         strDropdown = strDropdown+"<option selected='selected' value='0'>無効(Inactive)</option>";
    //                         statusText = "<span style='color:red;'>無効(Inactive)</span>";
    //                     }
    //                 }else{
    //                     if(role_and_status[1].toLowerCase() == 'true'){
    //                         user_status = "waiting";
    //                         strDropdown = strDropdown+"<option value='1'>有効(Active)</option>";
    //                         strDropdown = strDropdown+"<option selected='selected' value='3'>承認待ち(waiting)</option>";
    //                         strDropdown = strDropdown+"<option value='0'>無効(Inactive)</option>";
    //                         statusText = "<span style='color:darkorange;' class='blink_me'>承認待ち(waiting)</span>";
    //                     }else{
    //                         user_status = "inactive";
    //                         strDropdown = strDropdown+"<option value='1'>有効(Active)</option>";
    //                         strDropdown = strDropdown+"<option value='3'>承認待ち(waiting)</option>";
    //                         strDropdown = strDropdown+"<option selected='selected' value='0'>無効(Inactive)</option>";
    //                         statusText = "<span style='color:red;'>無効(Inactive)</span>";
    //                     }                        
    //                 }                    
    //                 strDropdown = strDropdown +"</select>";

    //                 return statusText;
    //             }
    //         },
    //         {
    //             data: 'Password'
    //         },
    //         {
    //             render: function () {
    //                 return `<button class="btn btn-info user_edit_button" onclick="UpdateUserModal('${user_name}','${user_status}')">編集</button>`;
    //             }
    //         }
    //     ]
    //     // ,
    //     // initComplete: function () {
    //     //     var r = $('#employeeList_datatable tfoot tr');
    //     //     $('#employeeList_datatable thead').append(r);
    //     //     // Apply the search
    //     //     // this.api()
    //     //     //     .columns()
    //     //     //     .every(function () {
    //     //     //         var that = this; 
    //     //     //         $('input', this.footer()).on('keyup change clear', function () {
    //     //     //             if (that.search() !== this.value) {
    //     //     //                 that.search(this.value).draw();
    //     //     //             }
    //     //     //         });
    //     //     //     });
    //     // },         
    // });   
}

$(document).ready(function () {  
    $('#employeeList_datatable tfoot th').each(function () {
        var title = $(this).text();
        if(title == 'user name'){
            $(this).html('<input class="user_search" id="search_user_name" type="text"  placeholder="Search ' + title + '" />');
        }
        if(title == 'role'){
            $(this).html('<input class="user_search" id="search_user_role" type="text"  placeholder="Search ' + title + '" />');
        }
        if(title == 'title'){
            $(this).html('<input class="user_search" id="search_user_title" type="text"  placeholder="Search ' + title + '" />');
        }
        if(title == 'department'){
            $(this).html('<input class="user_search" id="search_user_department" type="text"  placeholder="Search ' + title + '" />');
        }
        if(title == 'email'){
            $(this).html('<input class="user_search" id="search_user_email" type="text"  placeholder="Search ' + title + '" />');
        }
        if(title == 'status'){
            $(this).html('<input class="user_search" id="search_user_status" type="text"  placeholder="Search ' + title + '" />');
        }
        //$(this).html('<input class="user_search" id="" type="text"  placeholder="Search ' + title + '" />');
    });

    $.getJSON('/api/utilities/GetOnlyAdmin/')
        .done(function (data) {
            $('#admin_table tbody').empty();
            $('#admin_table tbody').append(`<tr><td>${data.UserName}</td><td>${data.UserRoleName}</td><td>${data.UserTitle}</td><td>${data.DepartmentName}</td><td>${data.Email}</td><td>${data.Password}</td><td><button class="btn btn-info user_edit_button" user_name='${data.UserName}' user_status='active' >編集</button></td></tr>`);
        }); 

    
    // ("#userDepartment").select2();

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


function UpdateUserModal(userName,user_status) {
    console.log("user_status: "+user_status);
    if(user_status=="active"){        
        $('#userStatus').val(1);
    }else if(user_status=="inactive"){
        $('#userStatus').val(0);
    }else{
        $('#userStatus').val(3);
    }
    $.getJSON('/api/utilities/GetSingleUserInfo?userName=' + userName)
        .done(function (data) {
            $('#user_id_hidden').val(data.Id);
            $('#userName').val(data.UserName);
            $('#userTitle').val(data.UserTitle);
            $('#userDepartment').val(data.DepartmentId);
            $('#userRole').val(data.UserRoleId);
            if(user_status=="waiting"){
                $('#userRole').val(-1);
            }else{
                $('#userRole').val(data.UserRoleId);
            }
            $('#userEmail').val(data.Email);
            $('#userPass').val(data.Password);
        }); 

    $('#modal_update_user').modal('show');
}

//employee insert
function CreateUserName() {
    let userName = $("#userName").val().trim();
    let userTitle = $("#userTitle").val().trim();
    let departmentId = $("#userDepartment").val().trim();
    let userEmail = $("#userEmail").val().trim();
    let userPass = $("#userPass").val().trim();
    let userRoleId = $("#userRole").val().trim();

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
    if (userRoleId == "") {
        alert("please select user role")
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
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess('データが保存されました!');

                $('#userName').val('');
                GetUserList();
            }

        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });    
}


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
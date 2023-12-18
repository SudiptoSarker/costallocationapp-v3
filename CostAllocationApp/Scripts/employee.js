
$(document).ready(function () {
    $(".add_master_btn").on("click",function(event){        
        $("#employee_name_add").val('');
        $('#add_master_modal').modal('show');
    })
    $("#employee_add_undo").on("click",function(event){        
        $("#employee_name_add").val('');        
    })
    $("#employee_edit_undo").on("click",function(event){        
        $("#employee_name_edit").val('');
    })
    $("#employee_add_save").on("click",function(event){       
        var roleName = $("#employee_name_add").val();           

        if (roleName == '' || roleName == null || roleName == undefined){
            alert("要員名を入力してください");
            return false;
        }
        else{            
            UpdateInsertEmployee(roleName,0,false);
        }   
    })
    //edit from modal
    $("#employee_edit_save").on("click",function(event){   
        
        var roleName = $("#employee_name_edit").val();   
        var roleId= $("#employee_id_for_edit").val();   

        if (roleName == '' || roleName == null || roleName == undefined){
            alert("要員名を入力してください");
            return false;
        }
        else{            
            UpdateInsertEmployee(roleName,roleId,true);
        }        
    })
    //employee insert
    function UpdateInsertEmployee(employeeName,employeeId,isUpdate) {
        var apiurl = "/api/utilities/CreateEmployee/";
        var data = {
            Id:employeeId,
            FullName: employeeName,
            IsUpdate:isUpdate
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result > 0) {
                    ToastMessageSuccess('データが保存されました!');
                    GetEmployeeList();
                    
                    if(isUpdate){
                        $('#edit_master_modal').modal('hide');
                    }else{
                        $('#add_master_modal').modal('hide');
                    }
                }

            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }

    /***************************\                           
        Check if the employee is checked for delete/remove
    \***************************/
    $('.delete_master_btn').on('click', function (event) {        
        var checkedVals = $('.employee_id:checkbox:checked').map(function() {
            return this.value;
        }).get();
        var id  = checkedVals.join(",");
        
        if (id == "") {
            alert("要員名が選択されていません");
            return false;
        }else{
            $('#delete_master_modal').modal('show');
        }
    });

    /***************************\                           
        Employee Delete/Remove Confirm Button           
    \***************************/	
    $('#modal_delete_btn').on('click', function (event) {
        event.preventDefault();
        var checkedVals = $('.employee_id:checkbox:checked').map(function() {
            return this.value;
        }).get();
        var id  = checkedVals.join(",");

        $.ajax({
            url: '/api/Employees?employeeIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetEmployeeList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#delete_master_modal').modal('toggle');

    });

    //edit employee
    $(".edit_master_btn").on("click",function(event){   

        var checkedVals = $('.employee_id:checkbox:checked').map(function() {
            return this.value;
        }).get();
        var id  = checkedVals.join(",");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("要員名が選択されていません");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集の場合、複数の選択はできません");
            return false;
        }else{   
            FillTheEditModal(arrIds[0]);

            $('#edit_master_modal').modal('show');
        }        
    })
    function FillTheEditModal(employeeId){            
        var apiurl = `/api/utilities/GetEmployeeById`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "employeeId=" + employeeId,
            success: function (data) { 
                $("#employee_name_edit").val(data.FullName);   
                $("#employee_id_for_edit").val(data.Id);   
            }
        });            
    }

    $('#example').DataTable();
    //------------------Employee Master----------------------//
    //show employee list on page load
    GetEmployeeList();

    /***************************\                           
     Name Registration: get all the employees.                
    \***************************/
    //GetEmployeeListWithDropdownSearch();

    /***************************\                           
        Name Registration: namelist dropdown using select2 plugin.                
    \***************************/
    //$("#employee_list").select2();

});

// Clear the filter
$(document).on('click', '#clearFilter', function () {
    window.location.reload();
});

//search by employee name
$(document).on('change', '#name_search', function () {
    var employeeName = $('#name_search').val();
    GetEmployeeSearchResults(employeeName);
});

//filter by emoloyee name
$(document).on('click', '#filterEmp', function () {
    var employeeName = $('#inputEmpName').val();
    GetEmployeeSearchResults(employeeName);    

    if ($("#employeelist_tbody").hasClass("dataTables_empty")) {
        alert("empty!");
    }
});

//order the employee list
$(document).on('click', '#orderEmp', function () {
    var selectOrder = $("input[name='sortEmp']:checked");
    console.log(selectOrder.val());
    if (selectOrder.length > 0) {
        let orderBy = selectOrder.val();
        GetOrderedEmployeeList(orderBy);
    }
});

//Get ordered Employee list
function GetOrderedEmployeeList(orderBy) {
    $.getJSON('/api/utilities/EmployeeList?orderby=' + orderBy)
        .done(function (data) {
            ShowNameList_Datatable(data);
        });
}


//Get employee list
function GetEmployeeList() {
    $.getJSON('/api/utilities/EmployeeList/')
    .done(function (data) {
        ShowNameList_Datatable(data);
    });

    
}

/***************************\                           
 Showing namelist using datatable.                        
\***************************/
function ShowNameList_Datatable(data) {	
    var selectPageLength = $("select[name='employeeList_datatable_length']").val();
    selectPageLength = selectPageLength != null ? selectPageLength : 10;

    $('#employeeList_datatable').DataTable({
        destroy: true,
        data: data,
        ordering: false,
        orderCellsTop: true,
        pageLength: selectPageLength,
        searching: false,
        //searching: true,
        // bLengthChange: false,    
        //dom: 'lifrtip',
        language: {
            "emptyTable": "検索条件に合ったデータはありません"
        },
        columns: [
            { data: "Id",
                render: function (data,type,row) {
                return '<input type="checkbox" value="'+data+'" class="employee_id">';                        
                } 
            },
            {
                data: 'Id',                
            },
            {
                data: 'FullName'
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
                GetEmployeeList();
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
            GetEmployeeList();
            
            
        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });
}
function GetEmployeeSearchResults(employeeName = "") {
    //var employeeName = $('#name_search').val();
    if (employeeName == '') {
        employeeName = 'all';
    }
    $.ajax({
        url: `/api/utilities/EmployeeListByNameFilter/${employeeName}`,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowNameList_Datatable(data)
        },
        error: function () {            
        }
    });
}
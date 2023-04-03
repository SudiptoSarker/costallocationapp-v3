
$(document).ready(function () {
    //------------------Employee Master----------------------//

    //show employee list on page load
    GetEmployeeList();

});

//employee insert
function InsertEmployee() {
    var apiurl = "/api/utilities/CreateEmployee/";
    let employeeName = $("#employee-name").val().trim();
    if (employeeName == "") {
        $(".employee_err").show();
        return false;
    } else {
        $(".employee_err").hide();
        var data = {
            FullName: employeeName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result > 0) {
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('Data Save Successfully!');

                    $('#employee-name').val('');
                    GetEmployeeList();
                }

            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

//Get employee list
function GetEmployeeList() {
    // $('#employee_list_tbody').empty();
    $.getJSON('/api/utilities/EmployeeList/')
    .done(function (data) {
        ShowNameList_Datatable(data);
        // $.each(data, function (key, item) {
        //     $('#employee_list_tbody').append(`<tr><td>${item.Id}</td><td>${item.FullName}</td></tr>`);
        // });
    });

    
}

/***************************\                           
 Showing namelist using datatable.                        
\***************************/
function ShowNameList_Datatable(data){	
    $('#employee_table').DataTable({
        destroy: true,
        data: data,
        ordering: true,
        orderCellsTop: true,
        pageLength: 10,
        searching: false,
        bLengthChange: false,    
        dom: 'lifrtip',
        columns: [            
            {
                data: 'Id'
            },
            {
                data: 'FullName'
            }
        ]
    });
}

$(function () {

    var employeeContextMenu = $("#employeeContextMenu");

    $("body").on("contextmenu", "#employee_table tbody tr", function (e) {
        employeeContextMenu.css({
            display:'block',
            left: e.pageX-230,
            top: e.pageY-25
        });
        $('#employee_id_hidden').val($(this)[0].cells[0].innerText);
        $('#employee_name_edit').val($(this)[0].cells[1].innerText);
        //debugger;
        return false;
    });

    $('html').click(function () {
        employeeContextMenu.hide();
    });

    $("#employeeContextMenu li a").click(function (e) {
        var f = $(this);
        var elementText = f[0].innerText;
        if (elementText.toLowerCase() == 'edit') {
            $('#edit_employee_modal').modal();
        }
        if (elementText.toLowerCase() == 'inactive') {
            $('#inactive_employee_modal').modal();
        }
        
        //debugger;
    });
});

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
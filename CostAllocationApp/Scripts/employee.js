
$(document).ready(function () {
    //------------------Employee Master----------------------//

    //show employee list on page load
    GetEmployeeList();

    //inactive section
    //$('#section_inactive_confirm_btn').on('click', function (event) {
    //    event.preventDefault();
    //    let id = GetCheckedIds("section_list_tbody");

    //    var sectionWarningTxt = $("#section_warning_text").val();
    //    $("#section_warning").html(sectionWarningTxt);
    //    var tempVal = $("#section_warning").html();
    //    //alert(tempVal)


    //    id = id.slice(0, -1);
    //    $.ajax({
    //        url: '/api/sections?sectionIds=' + id,
    //        type: 'DELETE',
    //        success: function (data) {
    //            ToastMessageSuccess(data);
    //            GetSectionList();
    //        },
    //        error: function (data) {
    //            ToastMessageFailed(data);
    //        }
    //    });
    //    $('#delete_section').modal('toggle');
    //});
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
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess(data);

                $('#employee-name').val('');
                GetEmployeeList();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}

//Get employee list
function GetEmployeeList() {
    $.getJSON('/api/utilities/EmployeeList/')
        .done(function (data) {
            $('#employee_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#employee_list_tbody').append(`<tr><td>${item.Id}</td><td>${item.FullName}</td></tr>`);
            });
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
        //debugger;
        return false;
    });

    $('html').click(function () {
        employeeContextMenu.hide();
    });

    $("#employeeContextMenu li a").click(function (e) {
        var f = $(this);
        //debugger;
    });

});
//---------------modal insert--------------------//
function AddDivision() {
    var apiurl = "/api/sections/";
    let sectionName = $("#section-name").val().trim();
    if (sectionName == "") {
        $(".division-name-error").show();
        return false;

    }
    else {

        var data = {
            SectionName: sectionName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");

                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/sections/')
                    .done(function (data) {
                        $('#section_list_tbody').empty();
                        $.each(data, function (key, item) {
                            // Add a list item for the product.
                            $('#section_list_tbody').append(`<tr><td><input type="checkbox" class="section_list_chk" data-id='${item.Id}' /></td><td>${item.SectionName}</td></tr>`);
                        });
                    });
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}
function AddDepartment() {
    var apiurl = "/api/Departments/";
    let departmentName = $("#department_name").val().trim();
    let sectionId = $("#section_list").val().trim();

    let isValidRequest = true;

    if (departmentName == "") {
        $(".department-name-error").show();
        isValidRequest = false;
    } else {
        $(".department-name-error").hide();
    }
    if (sectionId == "" || sectionId < 0) {
        $("#section_ist_error").show();
        isValidRequest = false;
    } else {
        $("#section_ist_error").hide();
    }

    if (isValidRequest) {
        var data = {
            DepartmentName: departmentName,
            SectionId: sectionId
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

                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/departments/')
                    .done(function (data) {
                        $('#department_list_tbody').empty();
                        $.each(data, function (key, item) {
                            // Add a list item for the product.
                            $('#department_list_tbody').append(`<tr><td><input type="checkbox" class="department_list_chk" data-id='${item.Id}' /></td><td>${item.DepartmentName}</td><td>${item.SectionName}</td></tr>`);
                        });
                    });
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}
function AddInCharge() {
    var apiurl = "/api/incharges/";
    let in_charge_name = $("#in_charge_name").val().trim();
    if (in_charge_name == "") {
        $(".department-name-error").show();
        return false;
    }
    else {
        $(".department-name-error").hide();
        var data = {
            InChargeName: in_charge_name
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#in_charge_name").val('');
                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/incharges/')
                    .done(function (data) {
                        $('#incharge_list_tbody').empty();
                        $.each(data, function (key, item) {
                            $('#incharge_list_tbody').append(`<tr><td><input type="checkbox" class="in_charge_list_chk" data-id='${item.Id}' /></td><td>${item.InChargeName}</td></tr>`);
                        });
                    });
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}
function AddRoles() {
    var apiurl = "/api/Roles/";
    let roleName = $("#role_name").val().trim();
    if (roleName == "") {
        $(".department-name-error").show();
        return false;
    }
    else {
        $(".department-name-error").hide();
        var data = {
            RoleName: roleName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#role_name").val('');
                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/Roles/')
                    .done(function (data) {
                        $('#incharge_list_tbody').empty();
                        $.each(data, function (key, item) {
                            // Add a list item for the product.
                            $('#incharge_list_tbody').append(`<tr><td><input type="checkbox" class="in_charge_list_chk" data-id='${item.Id}' /></td><td>${item.RoleName}</td></tr>`);
                        });
                    });
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}
function AddExplanations() {
    var apiurl = "/api/Explanations/";
    let explanationName = $("#explanation_name").val().trim();
    if (explanationName == "") {
        $(".department-name-error").show();
        return false;
    }
    else {
        $(".department-name-error").hide();
        var data = {
            ExplanationName: explanationName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#explanation_name").val('');

                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/Explanations/')
                    .done(function (data) {
                        $('#incharge_list_tbody').empty();
                        $.each(data, function (key, item) {
                            // Add a list item for the product.
                            $('#incharge_list_tbody').append(`<tr><td><input type="checkbox" class="in_charge_list_chk" data-id='${item.Id}' /></td><td>${item.ExplanationName}</td></tr>`);
                        });
                    });
            },
            error: function () {
                alert("Error please try again");
            }
        });
    }
}
function AddCompany() {
    var apiurl = "/api/Companies/";
    let companyName = $("#companyName").val().trim();
    if (companyName == "") {
        $(".department-name-error").show();
        return false;
    }
    else {
        $(".department-name-error").hide();
        var data = {
            CompanyName: companyName
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                $("#companyName").val('');
                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/companies/')
                    .done(function (data) {
                        $('#incharge_list_tbody').empty();
                        $.each(data, function (key, item) {
                            // Add a list item for the product.
                            $('#incharge_list_tbody').append(`<tr><td><input type="checkbox" class="in_charge_list_chk" data-id='${item.Id}' /></td><td>${item.CompanyName}</td></tr>`);
                        });
                    });
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}
function AddSalary() {
    var apiurl = "/api/Salaries/";
    let lowUnitPrice = $("#lowUnitPrice").val().trim();
    let highUnitPrice = $("#hightUnitPrice").val().trim();
    let gradePoints = $("#gradePoints").val().trim();

    let isValidRequest = true;
    if (lowUnitPrice == "") {
        $("#lowPrice").show();
        isValidRequest = false;
    }
    else {
        $("#lowPrice").hide();
    }
    if (highUnitPrice == "") {
        $("#highPrice").show();
        isValidRequest = false;
    } else {
        $("#highPrice").hide();
    }
    if (gradePoints == "") {
        $("#salaryGradePoints").show();
        isValidRequest = false;
    } else {
        $("#salaryGradePoints").hide();
    }

    if (isValidRequest) {
        var data = {
            SalaryLowPoint: lowUnitPrice,
            SalaryHighPoint: highUnitPrice,
            SalaryGrade: gradePoints
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {

                Command: toastr["success"](data, "Success")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $('#section-name').val('');

                $.getJSON('/api/Salaries/')
                    .done(function (data) {
                        $('#incharge_list_tbody').empty();
                        $.each(data, function (key, item) {
                            $('#incharge_list_tbody').append(`<tr><td><input type="checkbox" class="in_charge_list_chk" data-id='${item.Id}' /></td><td>${item.SalaryLowPointWithComma} ～ ${item.SalaryHighPointWithComma}</td><td>${item.SalaryGrade}</td></tr>`);
                        });
                    });
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
}
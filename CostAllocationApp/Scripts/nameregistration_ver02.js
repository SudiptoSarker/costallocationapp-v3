/***************************\                           
 Name Registration              
\***************************/
$(document).ready(function () {
    FillDropdownOfNameRegistration();    
});

/***************************\                           
 Name Registration: Employee list                
\***************************/
function GetEmployeeList() {
    $('#employee_list').append(`<option value=''>Select Employee</option>`);
    $.getJSON('/api/utilities/EmployeeList/')
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#employee_list').append(`<option value='${item.Id}'>${item.FullName}</option>`);
            });
        });
}

/***************************\                           
 Name Registration: Fill all the dropdown when page load.                      
\***************************/	
function FillDropdownOfNameRegistration() {

    SetRegistrationValueInHiddenField();

    /***************************\                           
     Name Registration: get all the employees.                
    \***************************/	
    GetEmployeeList();                
    
    /***************************\                           
     Name Registration: namelist dropdown using select2 plugin.                
    \***************************/
    $("#employee_list").select2();

    /***************************\                           
     Name Registration: get section list.                
    \***************************/
    $.getJSON('/api/sections/')
    .done(function (data) {
        $('#section_new').empty();
        $('#section_new').append(`<option value=''>Select Section</option>`);
        var sectionId = $("#hid_sectionId").val();
        $.each(data, function (key, item) {
            if (sectionId == item.Id) {
                $('#section_new').append(`<option selected value='${item.Id}'>${item.SectionName}</option>`);
            } else {
                $('#section_new').append(`<option value='${item.Id}'>${item.SectionName}</option>`);
            }
        });
    });
    
    /***************************\                           
     Name Registration: section dropdown using select2 plugin.                  
    \***************************/
    $("#section_new").select2();          
    
    /***************************\                           
     Name Registration: get department list.                
    \***************************/
    $.getJSON('/api/Departments/')
    .done(function(data) {
        $('#department_new').append(`<option value=''>Select Department</option>`);
        $.each(data, function(key, item) {                    
            $('#department_new').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`)
        });
    });                    
    
    /***************************\                           
     Name Registration: department dropdown using select2 plugin.                  
    \***************************/
    $("#department_new").select2();

    /***************************\                           
     Name Registration: get incharge list.                
    \***************************/
    $.getJSON('/api/incharges/')
    .done(function (data) {
        $('#incharge_new').empty();
        $('#incharge_new').append(`<option value=''>Select In-Charge</option>`);
        var inchargeId = $("#hid_inchargeId").val();
        $.each(data, function (key, item) {
            if (inchargeId == item.Id) {
                $('#incharge_new').append(`<option selected value='${item.Id}'>${item.InChargeName}</option>`);
            } else {
                $('#incharge_new').append(`<option value='${item.Id}'>${item.InChargeName}</option>`);
            }
        });
    });
    
    /***************************\                           
     Name Registration: incharge dropdown using select2 plugin.                  
    \***************************/
    $("#incharge_new").select2();

    /***************************\                           
     Name Registration: get role list.                
    \***************************/
    $.getJSON('/api/Roles/')
    .done(function (data) {
        $('#role_new').empty();
        $('#role_new').append(`<option value=''>Select Role</option>`);
        var roleId = $("#hid_roleId").val();
        $.each(data, function (key, item) {
            if (roleId == item.Id) {
                $('#role_new').append(`<option selected value='${item.Id}'>${item.RoleName}</option>`);
            } else {
                $('#role_new').append(`<option value='${item.Id}'>${item.RoleName}</option>`);
            }
        });
    });
    
    /***************************\                           
     Name Registration: role dropdown using select2 plugin.                  
    \***************************/
    $("#role_new").select2();

    /***************************\                           
     Name Registration: get Explanations list.                
    \***************************/
    $.getJSON('/api/Explanations/')
    .done(function (data) {
        $('#explanation_new').empty();
        $('#explanation_new').append(`<option value=''>Select Explanation</option>`);
        var explantionId = $("#hid_explanationId").val();
        $.each(data, function (key, item) {
            if (explantionId == item.Id) {
                $('#explanation_new').append(`<option selected value='${item.Id}'>${item.ExplanationName}</option>`);
            } else {
                $('#explanation_new').append(`<option value='${item.Id}'>${item.ExplanationName}</option>`);
            }
        });
    });
    
    /***************************\                           
     Name Registration: Explanations dropdown using select2 plugin.                  
    \***************************/
    $("#explanation_new").select2();

    /***************************\                           
     Name Registration: get Company list.                
    \***************************/
    $.getJSON('/api/Companies/')
    .done(function (data) {
        $('#company_new').empty();
        $('#company_new').append(`<option value=''>Select Company</option>`);
        var companyid = $("#hid_companyId").val();
        $.each(data, function (key, item) {
            if (companyid == item.Id) {
                $('#company_new').append(`<option selected value='${item.Id}'>${item.CompanyName}</option>`);
            } else {
                $('#company_new').append(`<option value='${item.Id}'>${item.CompanyName}</option>`);
            }
        });
    });
    
    /***************************\                           
     Name Registration: Company dropdown using select2 plugin.                  
    \***************************/
    $("#company_new").select2();
    
    /***************************\                           
     Name Registration: year dropdown using select2 plugin.                  
    \***************************/
    $("#assign_year").select2();
}

$('#section_registration').on('hidden.bs.modal', function () {
    var isPageLoad = $("#page_load_after_modal_close").val();
    if (isPageLoad == "yes") {
        //SetRegistrationValueInHiddenField();
        //window.location.reload(true);
        //RetainRegistrationValue()
        FillDropdownOfNameRegistration();
    }
})
$('#department_registration').on('hidden.bs.modal', function () {
    var isPageLoad = $("#page_load_after_modal_close").val();
    if (isPageLoad == "yes") {
        //SetRegistrationValueInHiddenField();
        //window.location.reload(true);
        //RetainRegistrationValue()
        FillDropdownOfNameRegistration();
    }
})
$('#in-charge_registration').on('hidden.bs.modal', function () {
    var isPageLoad = $("#page_load_after_modal_close").val();
    if (isPageLoad == "yes") {
        //SetRegistrationValueInHiddenField();
        //window.location.reload(true);
        //RetainRegistrationValue();
        FillDropdownOfNameRegistration();
    }
})
$('#role_registration_modal').on('hidden.bs.modal', function () {
    var isPageLoad = $("#page_load_after_modal_close").val();
    if (isPageLoad == "yes") {
        //SetRegistrationValueInHiddenField();
        //window.location.reload(true);
        //RetainRegistrationValue();
        FillDropdownOfNameRegistration();
    }
})
$('#explanation_modal').on('hidden.bs.modal', function () {
    var isPageLoad = $("#page_load_after_modal_close").val();
    if (isPageLoad == "yes") {
        //SetRegistrationValueInHiddenField();
        //window.location.reload(true);
        //RetainRegistrationValue()
        FillDropdownOfNameRegistration();
    }
})
$('#company_reg_modal').on('hidden.bs.modal', function () {
    var isPageLoad = $("#page_load_after_modal_close").val();
    if (isPageLoad == "yes") {
        //window.location.reload(true);
        //RetainRegistrationValue()
        FillDropdownOfNameRegistration();
    }
})
function SetRegistrationValueInHiddenField() {
    let name = $("#identity_new").val();
    let employeeId = $('#employee_list').find(":selected").val();

    let memo = $("#memo_new").val();
    let sectionId = $('#section_new').find(":selected").val();
    console.log("set: sectionId: " + sectionId);
    let departmentId = $('#department_new').find(":selected").val();
    let inchargeId = $('#incharge_new').find(":selected").val();
    let roleId = $('#role_new').find(":selected").val();
    let explantionId = $('#explanation_new').find(":selected").val();
    let companyid = $('#company_new').find(":selected").val();
    let companyName = $('#company_new').find(":selected").text();
    let grade = $("#grade_new").val();
    let unitPrice = $("#unitprice_new").val();

    $("#hid_name").val(name);
    $("#hid_memo").val(memo);
    $("#hid_sectionId").val(sectionId);

    $("#hid_departmentId").val(departmentId);
    $("#hid_inchargeId").val(inchargeId);
    $("#hid_roleId").val(roleId);
    $("#hid_explanationId").val(explantionId);
    $("#hid_companyId").val(companyid);
    $("#hid_companyName").val(companyName);
    $("#hid_grade").val(grade);
    //$("#hid_gradeId").val(unitPrice);
    $("#hid_unitPrice").val(unitPrice);
}
//Grade point with unit price value change
$('#unitprice_new').on('change', function () {
    var _unitPrice = $(this).val();
    var _companyName = $("#company_new option:selected").text();

    $.ajax({
        url: `/api/utilities/CompareGrade/${_unitPrice}`,
        type: 'GET',
        dataType: 'json',
        //data: {
        //    unitPrice: _unitPrice
        //},
        success: function (data) {
            if (_companyName.toLowerCase() == "mw") {
                $('#grade_new_hidden').val(data.Id);
                $('#grade_new').val(data.SalaryGrade);
            } else {
                $('#grade_new_hidden').val(data.Id);
                $('#grade_new').val('');
            }
        },
        error: function () {
            $('#grade_new_hidden').val('');
            $('#grade_new').val('');
        }
    });
});

//fill grade point with company change
$('#company_new').on('change', function () {
    var _companyName = $("#company_new option:selected").text();
    var _unitPrice = $("#unitprice_new").val();
    console.log("_unitPrice: " + _unitPrice);

    $.ajax({
        url: `/api/utilities/CompareGrade/${_unitPrice}`,
        type: 'GET',
        dataType: 'json',
        //data: {
        //    unitPrice: _unitPrice
        //},
        success: function (data) {
            $('#grade_edit_hidden').val(data.Id);
            if (_companyName.toLowerCase() == "mw") {
                $('#grade_new_hidden').val(data.Id);
                $('#grade_new').val(data.SalaryGrade);
            } else {
                $('#grade_new').val('');
            }
        },
        error: function () {
            $('#grade_new_hidden').val('');
            $('#grade_new').val('');
            $('#grade_edit_hidden').val('');
        }
    });
});

//cancel button click: close tab
$('#add_name_cancel').click(function () {
    window.top.close();
});

/***************************\                           
 Assign Employee: Save Button Clicked. Employee is assigned using this function.                      
\***************************/
$('#add_name_add_new').on('click', function () {
    var employeeId = $('#employee_list').find(":selected").val();
    var sectionId = $('#section_new').find(":selected").val();
    var inchargeId = $('#incharge_new').find(":selected").val();
    var departmentId = $('#department_new').find(":selected").val();
    var roleId = $('#role_new').find(":selected").val();
    var companyId = $('#company_new').find(":selected").val();
    var explanationId = $('#explanation_new').find(":selected").val();
    var unitPrice = $('#unitprice_new').val();
    var gradeId = $('#grade_new_hidden').val();
    var remarks = $('#memo_new').val();
    var year = $('#assign_year').find(":selected").val();

    var data = {
        EmployeeId: employeeId,
        SectionId: sectionId,
        DepartmentId: departmentId,
        InchargeId: inchargeId,
        RoleId: roleId,
        ExplanationId: explanationId,
        CompanyId: companyId,
        UnitPrice: unitPrice,
        GradeId: gradeId,
        Remarks: remarks,
        SubCode: 1,
        Year: year
    };

    console.log(data);

    $.ajax({
        url: '/api/Employees',
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
            alert('Success');
            location.reload();
        },
        error: function (data) {
            alert(data.responseJSON.Message);
            //$('#modal_add_name_new').modal('toggle');
        }
    });



});

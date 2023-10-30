﻿$(document).ready(function () {
    $(".add_master_btn").on("click",function(event){        
        $('#add_master_modal').modal('show');
    })
   
    $("#salary_save_btn").on("click",function(event){ 
        let lowUnitPrice = $("#unit_price_from").val().trim();
        let highUnitPrice = $("#unit_price_to").val().trim();
        let gradePoints = $("#grae_point").val().trim();        
       
        if (lowUnitPrice == "" || lowUnitPrice == null || lowUnitPrice == undefined) {
            alert("please enter initial unit!");
            return false;
        }
        else if (highUnitPrice == "" || highUnitPrice == null || highUnitPrice == undefined) {
            alert("please enter target unit!");
            return false;
        }
        else if (gradePoints == "" || gradePoints == null || gradePoints == undefined) {
            alert("please enter grade point!");
            return false;
        }
        UpdateInsertGrade(lowUnitPrice,highUnitPrice,gradePoints,0,false);
    })
    //edit from modal
    $("#salary_edit_btn").on("click",function(event){   
            
        let lowUnitPrice = $("#unit_price_from_edit").val().trim();
        let highUnitPrice = $("#unit_price_to_edit").val().trim();
        let gradePoints = $("#grae_point_edit").val().trim();        
        let gradeId = $("#gradeId_edit").val().trim();

        if (lowUnitPrice == "" || lowUnitPrice == null || lowUnitPrice == undefined) {
            alert("please enter initial unit!");
            return false;
        }
        else if (highUnitPrice == "" || highUnitPrice == null || highUnitPrice == undefined) {
            alert("please enter target unit!");
            return false;
        }
        else if (gradePoints == "" || gradePoints == null || gradePoints == undefined) {
            alert("please enter grade point!");
            return false;
        }
        UpdateInsertGrade(lowUnitPrice,highUnitPrice,gradePoints,gradeId,true);       
    })
    //edit incharge
    $(".edit_master_btn").on("click",function(event){          
        let id = GetCheckedIds("salary_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集するセクションにチェックを入れてください");
            return false;
        }else{   
            FillTheEditModal(arrIds[0]);

            $('#edit_master_modal').modal('show');
        }        
    })

    function FillTheEditModal(salaryId){            
        var apiurl = `/api/utilities/GetSalaryBySalaryId`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "salaryId=" + salaryId,
            success: function (data) { 
                $("#unit_price_from_edit").val(data.SalaryLowPoint);   
                $("#unit_price_to_edit").val(data.SalaryHighPoint);   
                $("#grae_point_edit").val(data.SalaryGrade);   

                $("#gradeId_edit").val(data.Id);   
            }
        });            
    }
    
    //edit from modal
    $("#exp_reg_save_btn_edit").on("click",function(event){   
        
        var explanation_name = $("#explanation_name_edit").val();   
        var explanation_id= $("#hid_explanation_id").val();   

        if (explanation_name == '' || explanation_name == null || explanation_name == undefined){
            alert("please enter explanation name!");
            return false;
        }
        else{
            UpdateInsertExplanations(explanation_name,explanation_id,true);
        }        
    })

     /***************************\                           
        Check if the grade is checked for delete/remove
    \***************************/
    $('.delete_master_btn').on('click', function (event) {

        let id = GetCheckedIds("salary_list_tbody");
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }else{
            onSalaryInactiveClick();
            $('#inactive_salary_modal').modal('show');
        }
    });

    GetSalaries();

    $('#salary_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("salary_list_tbody");
        id = id.slice(0, -1);        
        $.ajax({
            url: '/api/Salaries?salaryIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetSalaries();                
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#inactive_salary_modal').modal('toggle');
    });


    $(document).on('change', '#gradePoints', function () {        
        var gradePoint = $("#gradePoints").val();
        var apiurl = '/api/utilities/IsGradeExists?gradePoint=' + gradePoint;
        $.ajax({
            url: apiurl,
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                if(parseInt(data)>0){
                    $("#salary_registration").text('更新​ (Update)');
                    
                }else{
                    $("#salary_registration").text('保存（save）');                    
                }

                $("#onchange_gradeId").val(data);                
            },
            error: function (data) {
            }
        });
    });
});

function onSalaryInactiveClick() {
    let salaryIds = GetCheckedIds("salary_list_tbody");
    var apiurl = '/api/utilities/SalaryCount?salaryIds=' + salaryIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.salary_count').empty();
            $.each(data, function (key, item) {
                $('.salary_count').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}


function GetSalaries(){
    $.getJSON('/api/Salaries/')
    .done(function (data) {
        $('#salary_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#salary_list_tbody').append(`<tr><td><input type="checkbox" class="salary_list_chk" data-id='${item.Id}' /></td><td>${item.SalaryLowPointWithComma} ～ ${item.SalaryHighPointWithComma}</td><td>${item.SalaryGrade}</td></tr>`);
        });
    });
}    

function UpdateInsertGrade(lowUnitPrice,highUnitPrice,gradePoints,gradeId,isUpdate) {
    var apiurl = "/api/Salaries/";
        
    var data = {
        Id:gradeId,
        SalaryLowPoint: lowUnitPrice,
        SalaryHighPoint: highUnitPrice,
        SalaryGrade: gradePoints,
        IsUpdate: isUpdate            
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);
            GetSalaries();    
            if(isUpdate){
                $("#edit_master_modal").modal("hide");
            }else{
                $("#add_master_modal").modal("hide");
            }            
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
}
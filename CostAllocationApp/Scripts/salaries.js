$(document).ready(function () {
    $(".add_salary_btn").on("click",function(event){        
        $('#add_salary_modal').modal('show');
    })
   
    $("#exp_reg_save_btn").on("click",function(event){       
        let explanation_name = $("#explanation_name").val().trim();        
        if (explanation_name == "") {
            alert("please enter explanation name!");
            return false;
        }

        UpdateInsertExplanations(explanation_name,0,false);
    })

    //edit incharge
    $(".edit_explanation_btn").on("click",function(event){          
        let id = GetCheckedIds("explanations_list_tbody");
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

            $('#edit_explanation_modal').modal('show');
        }        
    })
    function FillTheEditModal(explanationId){            
        var apiurl = `/api/utilities/GetExplanationNameByExplanationId`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "explanationId=" + explanationId,
            success: function (data) { 
                $("#explanation_name_edit").val(data.ExplanationName);   
                $("#hid_explanation_id").val(data.Id);   
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

    $('#salary_inactive_btn').on('click', function (event) {

        let id = GetCheckedIds("salary_list_tbody");
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
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

function UpdateInsertGrade() {
    var apiurl = "/api/Salaries/";
    let lowUnitPrice = $("#lowUnitPrice").val().trim();
    let highUnitPrice = $("#hightUnitPrice").val().trim();
    let gradePoints = $("#gradePoints").val().trim();
    let gradeId = $("#onchange_gradeId").val();
    let isUpdate = false;
    
    if (gradeId == "" || gradeId == null || gradeId == undefined) {
        gradeId = 0;
    }

    if(parseInt(gradeId)==0){
        isUpdate = false;
    }else{
        isUpdate = true;
    }

    let isValidRequest = true;
    let lowPriceEmpty = false;
    let highPriceEmpty = false;
    let gradeEmpty = false;
     
    if (lowUnitPrice == "") {
        $("#lowPrice").show();
        isValidRequest = false;
        lowPriceEmpty = true;
    }
    else {
        lowPriceEmpty = false;
        $("#lowPrice").hide();
    }
    if (highUnitPrice == "") {
        $("#highPrice").show();
        isValidRequest = false;
        highPriceEmpty = true;
    } else {
        highPriceEmpty = false;
        $("#highPrice").hide();
    }   
    if (gradePoints == "") {
        $("#salaryGradePoints").show();
        isValidRequest = false;
        gradeEmpty = true;
    } else {
        gradeEmpty = false;
        $("#salaryGradePoints").hide();
    }   
    if(lowPriceEmpty){
        $("#lowUnitPrice").focus();
    }else if(highPriceEmpty){
        
        $("#hightUnitPrice").focus()
    }else if(gradeEmpty){
        $("#gradePoints").focus()
    }

    if (isValidRequest) {
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
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }
}
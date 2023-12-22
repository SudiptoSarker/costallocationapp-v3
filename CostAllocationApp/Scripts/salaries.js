$(document).ready(function () {
    //Show Salary List
    GetSalaries();

    //Add Salary Modal Show
    $(".add_master_btn").on("click",function(event){    
        $("#unit_price_from").val('');
        $("#unit_price_to").val('');
        $("#grae_point").val('');     
        $('#add_master_modal').modal('show');
    })

    //Clear Add Modal input fields
    $("#salary_undo_btn").on("click",function(event){        
        $("#unit_price_from").val('');
        $("#unit_price_to").val('');
        $("#grae_point").val(''); 
    })

    //Clear Edit Modal input fields
    $("#salary_edit_undo_btn").on("click",function(event){        
        $("#unit_price_from_edit").val('');
        $("#unit_price_to_edit").val('');
        $("#grae_point_edit").val(''); 
    })
   
    //Insert Salary
    $("#salary_save_btn").on("click",function(event){ 
        let lowUnitPrice = $("#unit_price_from").val().trim();
        let highUnitPrice = $("#unit_price_to").val().trim();
        let gradePoints = $("#grae_point").val().trim();        
       
        if (lowUnitPrice == "" || lowUnitPrice == null || lowUnitPrice == undefined) {
            alert("下限の単価を入力してください");
            return false;
        }
        else if (highUnitPrice == "" || highUnitPrice == null || highUnitPrice == undefined) {
            alert("上限の単価を入力してください");
            return false;
        }
        else if (gradePoints == "" || gradePoints == null || gradePoints == undefined) {
            alert("グレード値を入力してください");
            return false;
        }
        //Call Insert Salary Function
        UpdateInsertGrade(lowUnitPrice,highUnitPrice,gradePoints,0,false);
    })

    //Edit Salary Modal Show
    $(".edit_master_btn").on("click",function(event){          
        let id = GetCheckedIds("salary_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("グレードが選択されていません");
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
    
    //Edit / Update Salary
    $("#salary_edit_btn").on("click",function(event){   
            
        let lowUnitPrice = $("#unit_price_from_edit").val().trim();
        let highUnitPrice = $("#unit_price_to_edit").val().trim();
        let gradePoints = $("#grae_point_edit").val().trim();        
        let gradeId = $("#gradeId_edit").val().trim();

        if (lowUnitPrice == "" || lowUnitPrice == null || lowUnitPrice == undefined) {
            alert("下限の単価を入力してください");
            return false;
        }
        else if (highUnitPrice == "" || highUnitPrice == null || highUnitPrice == undefined) {
            alert("上限の単価を入力してください");
            return false;
        }
        else if (gradePoints == "" || gradePoints == null || gradePoints == undefined) {
            alert("グレード値を入力してください");
            return false;
        }
        //Call Update Function 
        UpdateInsertGrade(lowUnitPrice,highUnitPrice,gradePoints,gradeId,true);       
    })    

    //Delete Salary Modal Show
    $('.delete_master_btn').on('click', function (event) {
        let id = GetCheckedIds("salary_list_tbody");
        if (id == "") {
            alert("グレードが選択されていません");
            return false;
        }else{
            SalaryWithAssignment();
            $('#delete_salary_modal').modal('show');
        }
    });

    //Delete Salary
    $('#salary_del_confirm').on('click', function (event) {
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
                ToastMessageFailed("操作が失敗しました");
            }
        });

        $('#delete_salary_modal').modal('toggle');
    });
});

//Get Salary list
function GetSalaries(){
    $.getJSON('/api/Salaries/')
    .done(function (data) {
        $('#salary_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#salary_list_tbody').append(`<tr><td><input type="checkbox" class="salary_list_chk" data-id='${item.Id}' /></td><td>${item.SalaryLowPointWithComma} ～ ${item.SalaryHighPointWithComma}</td><td>${item.SalaryGrade}</td></tr>`);
        });
    });
}    

//Get Salary Details by Id and fill the edit modal
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

//Show the assignment count for the selected Salaries before delete Salary  
function SalaryWithAssignment() {
    let salaryIds = GetCheckedIds("salary_list_tbody");
    var apiurl = '/api/utilities/SalaryCount?salaryIds=' + salaryIds;
    $.ajax({
        url: apiurl,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('.del_confirm_warning').empty();
            $.each(data, function (key, item) {
                $('.del_confirm_warning').append(`<li class='text-info'>${item}</li>`);
            });
        },
        error: function (data) {
        }
    });
}

//Insert / Update Function
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
            ToastMessageFailed("操作が失敗しました");
        }
    });
}
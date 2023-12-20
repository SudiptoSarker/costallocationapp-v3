$(document).ready(function () {   
    /***************************\                           
        Show department list on page load           
    \***************************/	 
    GetDepartments();
    
    //show add department modal
    $(".add_dept_btn").on("click",function(event){    
        GetSections("add",0);    
        $("#department_name").val('');
        $("#sec_list_for_dept_add").val(-1);
        
        $('#add_department_modal').modal('show');
    })

    //show edit department modal
    $(".edit_dept_btn").on("click",function(event){   

        //get checked department ids
        let id = GetCheckedIds("department_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("部署が選択されていません");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集の場合、複数の選択はできません");
            return false;
        }else{   
            //Get department Details by Id and fill the edit modal
            FillTheDepartmentEditModal(arrIds[0]);

            $('#edit_department_modal').modal('show');
        }        
    })

    //add department modal: clear input fields
    $("#undo_add_frm").on("click",function(event){        
        $("#department_name").val('');
        $("#sec_list_for_dept_add").val(-1);
    })

    //edit department modal: clear input fields
    $("#undo_edit_frm").on("click",function(event){        
        $("#department_name_edit").val('');
        $("#sec_list_for_dept_edit").val(-1);
    })    

    //add department from modal
    $("#dept_add_btn").on("click",function(event){
        let departmentName = $("#department_name").val().trim();
        let sectionId = $("#sec_list_for_dept_add").val().trim();
        if (departmentName == "") {
            alert("部署名を入力してください");
            return false;
        }
        if (sectionId == "" || sectionId < 0){
            alert("区分を選択してください");
            return false;
        }
        departmentId = 0;

        //update function called for editing department
        UpdateInsertDepartment(departmentName,departmentId,sectionId,false);
    })

    //edit department from modal
    $("#dept_edit_btn").on("click",function(event){
        let departmentName = $("#department_name_edit").val().trim();
        let sectionId = $("#sec_list_for_dept_edit").val().trim();
        if (departmentName == "") {
            alert("部署名を入力してください");
            return false;
        }
        if (sectionId == "" || sectionId < 0){
            alert("区分を選択してください");
            return false;
        }        
        departmentId = $("#department_id_for_edit_modal").val();

        //update function called for editing department
        UpdateInsertDepartment(departmentName,departmentId,sectionId,true);
    })
    
    /***************************\                           
        Check if the department is checked for delete/remove
    \***************************/
    $('.delete_dept_btn').on('click', function (event) {

        let id = GetCheckedIds("department_list_tbody");               
        if (id == "") {
            alert("部署が選択されていません");
            return false;
        }
        else{

            //delete department function called for deleting the department
            DepartmentWithAssignment();
            $('#delete_department_modal').modal('show');
        }

    });    

    /***************************\                           
        Delete departments         
    \***************************/	
    $('#dept_del_confirm').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("department_list_tbody");
        id = id.slice(0, -1);

        $.ajax({
            url: '/api/departments?departmentIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);

                //department list show after deletion
                GetDepartments();
            },
            error: function (data) {
                ToastMessageFailed(data);

            }
        });

        $('#delete_department_modal').modal('toggle');

    }); 
});

//get department details by department id
function FillTheDepartmentEditModal(departmentId){            
    var apiurl = `/api/utilities/GetDepartmentByDepartemntId`;
    $.ajax({
        url: apiurl,
        contentType: 'application/json',
        type: 'GET',
        async: true,
        dataType: 'json',
        data: "departmentId=" + departmentId,
        success: function (data) { 
            $("#department_id_for_edit_modal").val(data.Id);
            $("#department_name_edit").val(data.DepartmentName);

            //section list function called by section id for selecting the saved section
            GetSections("edit",data.SectionId);   
        }
    });            
}

//section dropdown with departments and select the existing section with department
function GetSections(addOredit,sectionId){
    $.getJSON('/api/sections/')
    .done(function (data) {
        
        //create section dropdown for add department modal.
        if(addOredit=='add'){
            $('#sec_list_for_dept_add').empty();
            $('#sec_list_for_dept_add').append(`<option value='-1'>Select Section</option>`);        
            $.each(data, function (key, item) {
                $('#sec_list_for_dept_add').append(`<option value='${item.Id}'>${item.SectionName}</option>`);
            });    
        }

        //create section dropdown for edit department modal.
        if(addOredit =='edit'){
            $('#sec_list_for_dept_edit').empty();
            $('#sec_list_for_dept_edit').append(`<option value='-1'>Select Section</option>`);
            $.each(data, function (key, item) {
                if(parseInt(sectionId) == parseInt(item.Id)){
                    $('#sec_list_for_dept_edit').append(`<option value='${item.Id}' selected>${item.SectionName}</option>`)
                }else{
                    $('#sec_list_for_dept_edit').append(`<option value='${item.Id}'>${item.SectionName}</option>`)
                }                    
            });
        }
    });
}

/***************************\                           
Department Update/Insert
\***************************/
function UpdateInsertDepartment(departmentName,departmentId,sectionId,isUpdate) {
    var apiurl = "/api/Departments/";
    var data = {
        Id:departmentId,
        DepartmentName: departmentName,
        SectionId: sectionId,
        IsUpdate:isUpdate
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);
            GetDepartments();
            if(isUpdate){
                $("#edit_department_modal").modal("hide");
                $("#department_name").val('');
                $("#sec_list_for_dept_add").val('');                
                $('#section-name').val('');
            }else{
                $("#add_department_modal").modal("hide");
                $("#department_name").val('');
                $("#sec_list_for_dept_add").val('');                
                $('#section-name').val('');
            }            
        },
        error: function (data) {
            alert(data.responseJSON.Message);
        }
    });
}

/***************************\                           
Delete confirmation function and shows the department assignemnts with the selected departments         
\***************************/	
function DepartmentWithAssignment() {
    let departmentIds = GetCheckedIds("department_list_tbody");    
    var apiurl = '/api/utilities/DepartmentCount?departmentIds=' + departmentIds;
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

/***************************\                           
Get all the department list from database.
\***************************/
function GetDepartments(){
    $.getJSON('/api/departments/')
    .done(function (data) {
        $('#department_list_tbody').empty();
        $.each(data, function (key, item) {
            $('#department_list_tbody').append(`<tr><td><input type="checkbox" class="department_list_chk" data-id='${item.Id}' /></td><td>${item.DepartmentName}</td><td>${item.SectionName}</td></tr>`);
        });
    });
}   
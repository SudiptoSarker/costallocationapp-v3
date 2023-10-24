$(document).ready(function () {   
    /***************************\                           
        Show department list on page load           
    \***************************/	 
    GetDepartments();
    
    $(".add_dept_btn").on("click",function(event){    
        GetSections("add",0);    
        $('#add_department_modal').modal('show');
    })

    $(".edit_dept_btn").on("click",function(event){   
        let id = GetCheckedIds("department_list_tbody");
        var arrIds = id.split(',');        
        var tempLength  =arrIds.length;

        if (id == '' || id == null || id == undefined){
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
        else if(parseInt(tempLength)>2){
            alert("編集したい部門にチェックを入れます");
            return false;
        }else{   
            FillTheDepartmentEditModal(arrIds[0]);

            $('#edit_department_modal').modal('show');
        }        
    })

    $("#undo_add_frm").on("click",function(event){        
        $("#department_name").val('');
        $("#section_list").val(-1);
    })
    $("#undo_edit_frm").on("click",function(event){        
        $("#department_name_edit").val('');
        $("#section_list_edit").val(-1);
    })

    $("#undo_edit_sec").on("click",function(event){        
        $("#section_name_edit").val('');
    })

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
                GetSections("edit",data.SectionId);   
                //$("#section_list_edit").val(data.SectionId);
            }
        });            
    }
    
    //section dropdown
    function GetSections(addOredit,sectionId){
        $.getJSON('/api/sections/')
        .done(function (data) {
            if(addOredit=='add'){
                $('#section_list').empty();
                $('#section_list').append(`<option value='-1'>Select Section</option>`);        
                $.each(data, function (key, item) {
                    $('#section_list').append(`<option value='${item.Id}'>${item.SectionName}</option>`);
                });    
            }
            if(addOredit =='edit'){
                $('#section_list_edit').empty();
                $('#section_list_edit').append(`<option value='-1'>Select Section</option>`);
                $.each(data, function (key, item) {
                    if(parseInt(sectionId) == parseInt(item.Id)){
                        $('#section_list_edit').append(`<option value='${item.Id}' select>${item.SectionName}</option>`)
                    }else{
                        $('#section_list_edit').append(`<option value='${item.Id}'>${item.SectionName}</option>`)
                    }                    
                });
            }
        });
    }

    $("#dept_add_btn").on("click",function(event){
        let departmentName = $("#department_name").val().trim();
        let sectionId = $("#section_list").val().trim();
        if (departmentName == "") {
            alert("please enter department!");
            return false;
        }
        if (sectionId == "" || sectionId < 0){
            alert("please select section!");
            return false;
        }
        departmentId = 0;
        UpdateInsertDepartment(departmentName,departmentId,sectionId,false);
    })

    $("#dept_edit_btn").on("click",function(event){
        let departmentName = $("#department_name_edit").val().trim();
        let sectionId = $("#section_list_edit").val().trim();
        if (departmentName == "") {
            alert("please enter department!");
            return false;
        }
        if (sectionId == "" || sectionId < 0){
            alert("please select section!");
            return false;
        }        
        departmentId = $("#department_id_for_edit_modal").val();
        UpdateInsertDepartment(departmentName,departmentId,sectionId,true);
    })
    /***************************\                           
    Department Insertion function. 
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
                    $("#section_list").val('');                
                    $('#section-name').val('');
                }else{
                    $("#add_department_modal").modal("hide");
                    $("#department_name").val('');
                    $("#section_list").val('');                
                    $('#section-name').val('');
                }            
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }

    /***************************\                           
        Check if the department is checked for delete/remove
    \***************************/
    $('.delete_dept_btn').on('click', function (event) {

        let id = GetCheckedIds("department_list_tbody");               
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
        else{
            onDepartmentInactiveClick();
            $('#inactive_department_modal').modal('show');
        }

    });

    /***************************\                           
    Department In-Active/Remove 
    Also,shows that in how many projec that department is assigned                
    \***************************/	
    function onDepartmentInactiveClick() {
        let departmentIds = GetCheckedIds("department_list_tbody");    
        var apiurl = '/api/utilities/DepartmentCount?departmentIds=' + departmentIds;
        $.ajax({
            url: apiurl,
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                $('.department_count').empty();
                $.each(data, function (key, item) {
                    $('.department_count').append(`<li class='text-info'>${item}</li>`);
                });
            },
            error: function (data) {
            }
        });  
    }

    /***************************\                           
        Department Delete/Remove Confirm Button           
    \***************************/	
    $('#department_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("department_list_tbody");
        id = id.slice(0, -1);

        $.ajax({
            url: '/api/departments?departmentIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetDepartments();
            },
            error: function (data) {
                ToastMessageFailed(data);

            }
        });

        $('#inactive_department_modal').modal('toggle');

    });
    
});


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

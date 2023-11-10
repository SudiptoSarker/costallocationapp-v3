$(document).ready(function () {

    //Show section list on page load           
    GetSectionList();
    
    //Section Delete/Remove Confirm Button           
    $('#section_inactive_confirm_btn').on('click', function (event) {
        event.preventDefault();
        let id = GetCheckedIds("section_list_tbody");

        var sectionWarningTxt = $("#section_warning_text").val();
        $("#section_warning").html(sectionWarningTxt);
        var tempVal = $("#section_warning").html();

        id = id.slice(0, -1);
        $.ajax({
            url: '/api/sections?sectionIds=' + id,
            type: 'DELETE',
            success: function (data) {
                ToastMessageSuccess(data);
                GetSectionList();
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
        $('#delete_section').modal('toggle');
    });
    
    /***************************\                           
    Section In-Active/Remove 
    Also,shows that in how many projec that section is assigned                
    \***************************/	
    function onSectionInactiveClick() {
        let sectionIds = GetCheckedIds("section_list_tbody");
        var apiurl = '/api/utilities/SectionCount?sectionIds=' + sectionIds;
        $.ajax({
            url: apiurl,
            type: 'Get',
            dataType: 'json',
            success: function (data) {
                $('.section_count').empty();
                $.each(data, function (key, item) {
                    $('.section_count').append(`<li class='text-info'>${item}</li>`);
                });
            },
            error: function (data) {
            }
        });  
    }

    /***************************\                           
    Check if the section is checked for delete/remove
    \***************************/
    //$('#section_inactive_btn').on('click', function (event) {
    $('.delete_sec_btn').on('click', function (event) {
        let id = GetCheckedIds("section_list_tbody");
        if (id == "") {
            alert("ファイルが削除されたことを確認してください");
            return false;
        }
        else{
            onSectionInactiveClick();
            $('#delete_section').modal('show');
        }
    });

    //add from modal
    $("#sec_add_btn").on("click",function(event){
        let sectionName = $("#section-name").val().trim();        
        if (sectionName == "") {
            alert("please enter section!");
            return false;
        }

        UpdateInsertSection(sectionName,0,false);
    })    
    //edit from modal
    $("#edit_sec_from_modal").on("click",function(event){        
        var sectionName = $("#section_name_edit").val();   
        var sectionId= $("#edit_section_id").val();   

        if (sectionName == '' || sectionName == null || sectionName == undefined){
            alert("please enter section name!");
            return false;
        }
        else{
            UpdateInsertSection(sectionName,sectionId,true);
        }        
    })
    /***************************\                           
        Section Insertion is done by this function. 
    \***************************/
    function UpdateInsertSection(sectionName,sectionId,isUpdate) {
        var apiurl = "/api/sections/";        
        
        var data = {
            Id:sectionId,
            SectionName: sectionName,
            IsUpdate:isUpdate
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                $("#page_load_after_modal_close").val("yes");
                ToastMessageSuccess(data);
                if(isUpdate){
                    $('#section_name_edit').val('');
                    $("#edit_section_modal").modal("hide");
                }else{
                    $('#section-name').val('');
                    $("#add_section_modal").modal("hide");
                }                
                GetSectionList();
            },
            error: function (data) {
                alert(data.responseJSON.Message);
            }
        });
    }
        
    //get section details by section id
    function FillTheSectionEditModal(sectionId){            
        var apiurl = `/api/utilities/GetSectionNameBySectionId`;
        $.ajax({
            url: apiurl,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: "sectionIds=" + sectionId,
            success: function (data) { 
                $("#section_name_edit").val(data.SectionName);   
                $("#edit_section_id").val(data.Id);   
            }
        });            
    }
    /***************************\                           
    Get all the section list from database.
    \***************************/
    function GetSectionList() {
        $.getJSON('/api/sections/')
            .done(function (data) {
                //SectionList_Datatable(data);
                if (data != '' && data != null && data != undefined){
                    $('#section_list_tbody').empty();
                    $.each(data, function (key, item) {                
                        $('#section_list_tbody').append(`<tr><td><input type="checkbox" class="section_list_chk" onclick="GetCheckedIds(${item.Id});" data-id='${item.Id}' /></td><td>${item.SectionName}</td></tr>`);
                    });
                }                
            });
    }

    function SectionList_Datatable(data){
        $('#section_list_tbl').DataTable({
            destroy: true,
            data: data,
            //ordering: false,
            ordering: true,
            orderCellsTop: true,
            pageLength: 5,
            searching: false,
            searching: true,
            bLengthChange: false,    
            dom: 'lifrtip',
            columns: [            
                {
                    data: 'Id',
                    //className: 'select-checkbox',                    
                },
                {
                    data: 'SectionName'
                }
            ]            
        });
    }
    $(".add_sec_btn").on("click",function(event){   
        $("#section-name").val('');     
        $('#add_section_modal').modal('show');
    })
    $("#sec_undo_btn").on("click",function(event){        
        $("#section-name").val('');
    })
    $("#undo_edit_sec").on("click",function(event){        
        $("#section_name_edit").val('');
    })
    $(".edit_sec_btn").on("click",function(event){   
       
        let id = GetCheckedIds("section_list_tbody");
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
            FillTheSectionEditModal(arrIds[0]);

            $('#edit_section_modal').modal('show');
        }        
    })
});


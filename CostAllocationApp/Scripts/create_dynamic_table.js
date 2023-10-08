var globalCount = 1;
var globalSettingList = [];
var html;
$(document).ready(function () {
    html = $('#total_menu_setting_items_tbl').html();
    //get table lsit and setting list on page load. 
    GetDynamicTables();
    GetDynamicTablesForSetting();
});
// need to omit
function InsertDynamicTables() {
    debugger;
    var apiurlInsert = "/api/Utilities/CreateDynamicTable";
    var apiurlUpdate = "/api/Utilities/UpdateDynamicTable";
    var buttonTag = $('.frm_add_btn').attr('tag');
    var tableName = $("#table_name_input").val();
    var tableTitle = $("#table_title_input").val();
    var tablePosition = $("#table_position_input").val();
    var isValid = true;
    var columnTitle1 = '';
    var columnTitle2 = '';
    var columnTitle3 = '';
    var columnInputDropdown = $('.select_column_no').val();
    if (columnInputDropdown == "1") {
        columnTitle1 = $('#column_input_1').val();
    }
    if (columnInputDropdown == "2") {
        columnTitle1 = $('#column_input_1').val();
        columnTitle2 = $('#column_input_2').val();
    }

    if (columnInputDropdown == "3") {
        columnTitle1 = $('#column_input_1').val();
        columnTitle2 = $('#column_input_2').val();
        columnTitle3 = $('#column_input_3').val();
    }


    if (tableName == "") {
        isValid = false;
        $("#table_name_warning_msg").show();
        $("#table_name").focus();        
    }else{
        $("#table_name_warning_msg").hide();
    }
    if (tableTitle == "") {
        if(isValid){
            $("#table_title").focus();   
        }        
        isValid = false;
        $("#table_title_warning_msg").show();        
    }else{         
        $("#table_title_warning_msg").hide();
    }
    if (tablePosition == "") {
        if(isValid){
            $("#table_position").focus();   
        }  
        isValid = false;
        $("#table_position_warning_msg").show();
    }else{
        $("#table_position_warning_msg").hide();
    }


    if(isValid) {
        $("#table_name_warning_msg").hide();
        $("#table_title_warning_msg").hide();
        $("#table_position_warning_msg").hide();

        var data = {
            TableName: tableName,
            TableTitle: tableTitle,
            TablePosition: tablePosition,
            IsActive: true,
            CategoryTitle: columnTitle1,
            SubCategoryTitle: columnTitle2,
            DetailsTitle: columnTitle3,
        };

        if (buttonTag.toLowerCase()=="add") {
            $.ajax({
                url: apiurlInsert,
                type: 'POST',
                dataType: 'json',
                data: data,
                success: function (data) {
                    if (data == 1) {
                        alert("同一データが登録済みです.");
                    } else {
                        $("#page_load_after_modal_close").val("yes");
                        $("#table_name").val('');
                        $("#table_title").val('');
                        $("#table_position").val('');
                        ToastMessageSuccess(data);
                        GetDynamicTables();
                        GetDynamicTablesForSetting();
                        GetDynamicTables();
                        ClearInputEditForm();
                        $('.table_input_frm_div').hide();
                    }
                },
                error: function (data) {
                    ToastMessageFailed(data);
                }
            });
        }
        else {

            var rowId = $('#table_id_for_edit').val();
            data.Id = parseInt(rowId);
            $.ajax({
                url: apiurlUpdate,
                type: 'POST',
                dataType: 'json',
                data: data,
                success: function (data) {
                    if (data == 1) {
                        alert("同一データが登録済みです.");
                    } else {
                        $("#page_load_after_modal_close").val("yes");
                        $("#table_name").val('');
                        $("#table_title").val('');
                        $("#table_position").val('');
                        ToastMessageSuccess(data);
                        GetDynamicTables();
                        GetDynamicTables();
                        GetDynamicTablesForSetting();
                    }
                    ClearInputEditForm();
                    $('.table_input_frm_div').hide();
                },
                error: function (data) {
                    ToastMessageFailed(data);
                }
            });
        }

        
    }
}

function InsertDynamicSetting() {
    globalSettingList = [];
    var title1 = $('#column_title_1').val();
    var title2 = $('#column_title_2').val();
    var title3 = $('#column_title_3').val();
    var dynamicTableId = $('#dynamic_table_list_for_setting').val();

    var allTr = $('#item_tbody tr');
    $.each(allTr, function (index, item) {
        if (parseInt($(item)[0].dataset.count) > 0) {
            var obj = {
                    DynamicTableId: dynamicTableId,
                    CategoryId: $(item).find('[name="main_item"]').val(),
                    SubCategoryId: $(item).find('[name="sub_item"]').val(),
                    DetailsId: $(item).find('[name="detail_item"]').val(),
                    MethodId: $(item).find('[name="method_item"]').val(),
                    ParameterId: $(item).find('[name="data_for_list_dropdown_for_setting"]').val().join(','),
            };

            globalSettingList.push(obj);
        }
    });

    if (globalSettingList.length > 0) {
        $.ajax({
            url: `/api/utilities/UpdateDynamicTablesTitle?dynamicTableId=${dynamicTableId}&categoryTitle=${title1}&subCategoryTitle=${title2}&detailsTitle=${title3}`,
            type: 'POST',
            async: false,
            dataType: 'json',
            success: function (data) {
            }
        });

        $.ajax({
            url: '/api/utilities/CreateDynamicSetting/',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: {
                DynamicSettings: globalSettingList
            },
            success: function (data) {
                ToastMessageSuccess(data);
                GetDynamicSettings();
                globalCount = 1;
                $('#total_menu_setting_items_tbl').html(html);
                DynamicTableSetting();
            }
        });
    }


    
}
function GetDynamicTables() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#dynamic_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#dynamic_list_tbody').append(`<tr><td></thead><div class="form-check"><input class="form-check-input table_list_radio" type="radio" name="flexRadioDefault" id="flexRadioDefault1" value="${item.Id}"><label class="form-check-label" for="flexRadioDefault1">    </label></div></td><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td></tr>`);
                // <td><label id="dynamic_table_delete"><a id="dynamic_table_delete_link" href="javascript:void(0);" data-toggle="modal" data-target="#delete_dynamic_table" onClick="DeleteDynmaicTalbe(${item.Id})">削除</a></label><label id="dynamic_table_edit_label"><a id="dynamic_table_edit_link" href="javascript:void(0);" data-toggle="modal" data-target="#edit_dynamic_table_modal" onClick="GetDynamicTalbeById(${item.Id})">編集</a></label></td>
            });
        });
}
function GetDynamicTalbeById(dynamicTableId){    
    $.getJSON(`/api/utilities/GetDynamicTableById/${dynamicTableId}`)
        .done(function (data) {
            $("#table_id_for_edit").val(data.Id);
            $("#table_name_edit").val(data.TableName);
            $("#table_title_edit").val(data.TableTitle);
            $("#table_position_edit").val(data.TablePosition);    
        });
}

$(document).on('change', '#dynamic_list_tbody tr td .table_list_radio', function () {
    var selectedValue  = $('input[name="flexRadioDefault"]:checked').val();
    $('#table_id_for_edit').val(selectedValue);
    $('#table_id_for_delete').val(selectedValue);
    ClearInputEditForm();
    $('.table_input_frm_div').hide();
});

//edit dynamic table by table id
$(document).on('click', '#update_dynamic_table ', function () { 
    var apiurl = "/api/Utilities/UpdateDynamicTable";
    var id = $("#table_id_for_edit").val();
    var tableName = $("#table_name_edit").val();
    var tableTitle = $("#table_title_edit").val();
    var tablePosition = $("#table_position_edit").val();
    if (tableName == "") {
        alert("please enter table name!")
        return false;
    }
    if (tableTitle == "") {
        alert("please enter table title!")
        return false;
    }
    else {        
        var data = {
            Id:id,
            TableName: tableName,
            TableTitle: tableTitle,
            TablePosition: tablePosition,
            IsActive: true,
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                if(data==1){
                    alert("同一データが登録済みです.");
                }else{
                    ToastMessageSuccess(data);
                    GetDynamicTables();
                    GetDynamicTablesForSetting();
                    $('#edit_dynamic_table_modal').modal('toggle');
                }                
            },
            error: function (data) {
                ToastMessageFailed(data);
                $('#edit_dynamic_table_modal').modal('toggle');
            }
        });
    }  
    
    
});

//add main item
$(document).on('click', '.main_item_add_btn ', function () {   
    var mainItem = $('#section-name').val();
    var tableId = $('#table_id_for_edit').val();
    var dynamicTable;
    if (mainItem == '' || mainItem == undefined || mainItem == null) {
        alert('Main item required!');
        return;
    }
    else {
        $.ajax({
            url: `/api/utilities/CreateCategory`,
            type: 'POST',
            dataType: 'json',
            data: {
                CategoryName: mainItem,
                DynamicTableId: tableId
                },
            success: function (data) {
                ToastMessageSuccess(data);
                $.ajax({
                    url: '/api/utilities/GetDynamicTableById/' + tableId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        dynamicTable = data;

                    },
                    error: function (data) {
                    }
                });
                // pull data for main item
                $.ajax({
                    url: '/api/utilities/GetCategories?dynamicTableId=' + tableId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        $('.main_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.main_item_list_tbl tbody').append(`
                                <tr data-id='${value.Id}'>
                                    <td class="main_item_input_td">
                                        <a class="main_item_link" href="#" >${value.CategoryName}</a>
                                    </td>
                                    <td class="main_item_input_td">
                                        <a class="main_item_add_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="main_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });
                        
                    },
                    error: function (data) {
                    }
                });
                if (dynamicTable.SubCategoryTitle == "" || dynamicTable.SubCategoryTitle == null || dynamicTable.SubCategoryTitle == undefined) {
                    $('.main_item_list_tbl tbody .main_item_input_td a').removeClass('main_item_link');
                }
              
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }
    
});

//update main item
$(document).on('click', '.main_item_edit_btn ', function () {
    var mainItemId = $(this).closest('tr').data('id');
    $('#main_item_id_edit_input').val(mainItemId);


    $.ajax({
        url: '/api/utilities/GetCategoryById?categoryId=' + mainItemId,
        type: 'Get',
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#main_item_edit_input').val(data.CategoryName);

        },
        error: function (data) {
        }
    });


    $('#main_item_list_modal').modal('hide');

    setTimeout(() => {
        $('#main_item_edit_modal').modal('show');
    }, 600);

        

});

$(document).on('click', '.main_item_edit_action ', function () {
    debugger;
    var mainItemId = $('#main_item_id_edit_input').val();
    var mainItemName = $('#main_item_edit_input').val();
    var dynamicTableId = $('#table_id_for_edit').val();

    if (mainItemName == '' || mainItemName == undefined || mainItemName == null) {
        alert('Main item required!');
        return;
    }
    else {
        $.ajax({
            url: `/api/utilities/UpdateCategory`,
            type: 'PUT',
            dataType: 'json',
            data: {
                Id: mainItemId,
                CategoryName: mainItemName
            },
            success: function (data) {
                ToastMessageSuccess(data);
                // pull data for main item
                $.ajax({
                    url: '/api/utilities/GetCategories?dynamicTableId=' + dynamicTableId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        $('.main_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.main_item_list_tbl tbody').append(`
                                <tr data-id='${value.Id}'>
                                    <td class="main_item_input_td">
                                        <a class="main_item_link" href="#" >${value.CategoryName}</a>
                                    </td>
                                    <td class="main_item_input_td">
                                        <a class="main_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="main_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });
                        $('#main_item_edit_modal').modal('hide');
                        setTimeout(() => {
                            $('#main_item_list_modal').modal('show');
                        }, 600);
                    },
                    error: function (data) {
                    }
                });


            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }

});

$(document).on('click', '#main_item_edit_modal_close', () => {

    $('#main_item_edit_modal').modal('hide');
    setTimeout(() => {
        $('#main_item_list_modal').modal('show');
    }, 600);


});

$(document).on('click', '#sub_item_edit_modal_close', () => {

    $('#sub_item_edit_modal').modal('hide');
    setTimeout(() => {
        $('#sub_item_list_modal').modal('show');
    }, 600);


});

$(document).on('click', '#detail_item_edit_modal_close', () => {

    $('#detail_item_edit_modal').modal('hide');
    setTimeout(() => {
        $('#detail_item_list_modal').modal('show');
    }, 600);


});
//update sub item
$(document).on('click', '.sub_item_edit_btn', function () {
    var subItemId = $(this).closest('tr').data('sub-item-id');
    $('#sub_item_id_edit_input').val(subItemId);


    $.ajax({
        url: '/api/utilities/GetSubCategorieById?subCategoryId=' + subItemId,
        type: 'Get',
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#main_item_id_edit_input').val(data.CategoryId);
            $('#sub_item_edit_input').val(data.SubCategoryName);

        },
        error: function (data) {
        }
    });


    $('#sub_item_list_modal').modal('hide');

    setTimeout(() => {
        $('#sub_item_edit_modal').modal('show');
    }, 600);



});
$(document).on('click', '.sub_item_edit_action', function () {
    var mainItemId = $('#main_item_id_edit_input').val();
    var subItemId = $('#sub_item_id_edit_input').val();
    var subItemName = $('#sub_item_edit_input').val();
    var tableId = $('#table_id_for_edit').val();
    var dynamicTable;

    if (subItemName == '' || subItemName == undefined || subItemName == null) {
        alert('Sub item required!');
        return;
    }
    else {
        $.ajax({
            url: '/api/utilities/GetDynamicTableById/' + tableId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                dynamicTable = data;

            },
            error: function (data) {
            }
        });
        $.ajax({
            url: `/api/utilities/UpdateSubCategory`,
            type: 'PUT',
            dataType: 'json',
            data: {
                Id: subItemId,
                SubCategoryName: subItemName
            },
            success: function (data) {
                ToastMessageSuccess(data);
                // pull data for sub item
                $.ajax({
                    url: '/api/utilities/GetSubCategoriesByCategory?categoryId=' + mainItemId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {

                        $('.sub_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.sub_item_list_tbl tbody').append(`
                                <tr data-sub-item-id='${value.Id}'>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_link" href="#">${value.SubCategoryName}</a>
                                    </td>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="sub_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });

                        $('#sub_item_edit_modal').modal('hide');
                        setTimeout(() => {
                            $('#sub_item_list_modal').modal('show');
                        }, 600);
                    },
                    error: function (data) {
                    }
                });

                if (dynamicTable.DetailsTitle == "" || dynamicTable.DetailsTitle == null || dynamicTable.DetailsTitle == undefined) {
                    $('.sub_item_list_tbl tbody .sub_item_input_td a').removeClass('sub_item_link');
                }
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }

});


$(document).on('click', '.sub_item_add_btn ', function () {   
    var mainItemId = $('#main_item_id').val();
    var subItem = $('#input_sub_item').val();
    var tableId = $('#table_id_for_edit').val();
    var dynamicTable;
    $.ajax({
        url: '/api/utilities/GetDynamicTableById/' + tableId,
        type: 'Get',
        dataType: 'json',
        async: false,
        success: function (data) {
            dynamicTable = data;

        },
        error: function (data) {
        }
    });
    $.ajax({
        url: `/api/utilities/CreateSubCategory/`,
        type: 'POST',
        dataType: 'json',
        data: {
            CategoryId: mainItemId,
            SubCategoryName: subItem
        },
        success: function (data) {
            if (data == 1) {
                alert("同一データが登録済みです.");
            } else {
                ToastMessageSuccess(data);
                $.ajax({
                    url: '/api/utilities/GetSubCategoriesByCategory?categoryId=' + mainItemId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {

                        $('.sub_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.sub_item_list_tbl tbody').append(`
                                <tr data-sub-item-id='${value.Id}'>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_link" href="#">${value.SubCategoryName}</a>
                                    </td>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="sub_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });
                    },
                    error: function (data) {
                    }
                });

                if (dynamicTable.DetailsTitle == "" || dynamicTable.DetailsTitle == null || dynamicTable.DetailsTitle == undefined) {
                    $('.sub_item_list_tbl tbody .sub_item_input_td a').removeClass('sub_item_link');
                }
            }
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });

});

$(document).on('click', '.detail_item_add_btn ', function () { 
    var subItemId = $('#sub_item_id').val();
    var detailsItemName = $('#input_detail_item').val();

    $.ajax({
        url: `/api/utilities/CreateDetailsItem/`,
        type: 'POST',
        dataType: 'json',
        data: {
            SubCategoryId: subItemId,
            DetailsItemName: detailsItemName
        },
        success: function (data) {
            ToastMessageSuccess(data);
            
            $.ajax({
                url: '/api/utilities/GetDetailsItemBySubItemsId?subItemId=' + subItemId,
                type: 'Get',
                dataType: 'json',
                async: false,
                success: function (data) {

                    $('.detail_item_list_tbl tbody').empty();
                    $.each(data, function (index, value) {
                        $('.detail_item_list_tbl tbody').append(`
                                <tr data-detail-item-id='${value.Id}'>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_link" href="#">${value.DetailsItemName}</a>
                                    </td>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                    });
                },
                error: function (data) {
                }
            });
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
});
//update sub item/
$(document).on('click', '.detail_item_edit_btn', function () {
    
    var detailItemId = $(this).closest('tr').data('detail-item-id');
    $('#detail_item_id_edit_input').val(detailItemId);


    $.ajax({
        url: '/api/utilities/GetDetailsItemById?detailsId=' + detailItemId,
        type: 'Get',
        dataType: 'json',
        async: false,
        success: function (data) {
            debugger;
            $('#sub_item_id_edit_input').val(data.SubCategoryId);
            $('#detail_item_edit_input').val(data.DetailsItemName);

        },
        error: function (data) {
        }
    });


    $('#detail_item_list_modal').modal('hide');

    setTimeout(() => {
        $('#detail_item_edit_modal').modal('show');
    }, 600);



});
//.
$(document).on('click', '.detail_item_edit_action', function () {
    var subItemId = $('#sub_item_id_edit_input').val();
    var detailItemId = $('#detail_item_id_edit_input').val();
    var detailItemName = $('#detail_item_edit_input').val();

    if (detailItemName == '' || detailItemName == undefined || detailItemName == null) {
        alert('Detail item required!');
        return;
    }
    else {
        $.ajax({
            url: `/api/utilities/UpdateDetailItem`,
            type: 'PUT',
            dataType: 'json',
            data: {
                Id: detailItemId,
                DetailsItemName: detailItemName
            },
            success: function (data) {
                ToastMessageSuccess(data);
                // pull data for detail item
                $.ajax({
                    url: '/api/utilities/GetDetailsItemBySubItemsId?subItemId=' + subItemId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        $('.detail_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.detail_item_list_tbl tbody').append(`
                                <tr data-detail-item-id='${value.Id}'>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_link" href="#">${value.DetailsItemName}</a>
                                    </td>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });

                        $('#detail_item_edit_modal').modal('hide');
                        setTimeout(() => {
                            $('#detail_item_list_modal').modal('show');
                        }, 600);
                    },
                    error: function (data) {
                    }
                });


            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
    }

});



//update table setting
$(document).on('click', '#udpate_tbl_setting ', function () {     
});


//delete dynamic table by table id
$(document).on('click', '#delete_table_link ', function () { 

    $('#delete_dynamic_table').modal('toggle');
});

$(document).on('click', '.confrim_del_btn ', function () {
    var id = $("#table_id_for_delete").val();
    var apiurl = "/api/Utilities/RemoveDynamicTable?tableId=" + id;
    

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            ToastMessageSuccess(data);
            GetDynamicTables();
            GetDynamicTablesForSetting();
            ClearInputEditForm();
            $('.table_input_frm_div').hide();
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });

    $('#delete_dynamic_table').modal('toggle');
});
//delete dynamic table by table id
$(document).on('click', '#delete_setting_link ', function () { 
    
    $('#delete_dynamic_table').modal('toggle');
});

$(document).on('click', 'confrim_del_btn ', function () {

    var apiurl = "/api/Utilities/InactiveDynamicTable";
    var id = $("#table_id_for_delete").val();
    var tableName = "";
    var tableTitle = "";
    var tablePosition = "";

    var data = {
        Id: id,
        TableName: tableName,
        TableTitle: tableTitle,
        TablePosition: tablePosition,
        IsActive: false,
    };

    $.ajax({
        url: apiurl,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (data) {
            ToastMessageSuccess(data);
            GetDynamicTables();
            GetDynamicTablesForSetting();
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });

    $('#delete_dynamic_table').modal('toggle');
});

function GetDynamicTablesForSetting() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#dynamic_table_list_for_setting').empty();
            $('#dynamic_table_list_for_setting').append(`<option value=''>Select Tables</option>`)
            $.each(data, function (key, item) {
                $('#dynamic_table_list_for_setting').append(`<option value='${item.Id}'>${item.TableName}</option>`)
            });
        });
}

function DynamicTableSetting(){
    var tableId = $("#dynamic_table_list_for_setting").val();
    if (tableId == '' || tableId == null || tableId == undefined) {
        $("#total_menu_settings").hide();
        alert('テーブルを選択してください!!!');        
        return false;
    } else {
        GetDynamicSettings();
        $("#total_menu_settings").show();
        
        // pull columns title
        $.ajax({
            url: `/api/utilities/GetDynamicTableById/${tableId}`,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                $('#column_title_1').val(data.CategoryTitle);
                $('#column_title_2').val(data.SubCategoryTitle);
                $('#column_title_3').val(data.DetailsTitle);
            },
            error: function (data) {
            }
        });

        // pull data for main item
        $.ajax({
            url: '/api/utilities/GetCategories/',
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                $('#main_item').empty();
                $('#main_item').append(`<option value=''>Select Item</option>`);
                $.each(data, function (key, item) {
                    $('#main_item').append(`<option value='${item.Id}'>${item.CategoryName}</option>`);
                });
                $('#main_item').append(`<option disabled="disabled">---------</option>`);
                $('#main_item').append(`<option value='main'>Add New</option>`);
            },
            error: function (data) {
            }
        }); 

        // pull method list
        $.ajax({
            url: '/api/utilities/GetMethodList/',
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                $('#method_list_dropdown').empty();
                $('#method_list_dropdown').append(`<option value=''>Select Item</option>`);
                console.log(data);
                $.each(data, function (key, item) {
                    $('#method_list_dropdown').append(`<option value='${item.Id}'data-dependency=${item.Dependency}>${item.MethodName}</option>`);
                });
            },
            error: function (data) {
            }
        }); 
    }         
}

$(document).ready(function() {   
    $(document).on('change', '.main_item_dropdown', function () {
        var sub_item_options = "";
        var mainItem = $(this).val();
        if (mainItem == "" || mainItem == undefined || mainItem == null) {
            return;
        } else if (mainItem == "main") {
            $('#add_main_item_modal').modal('toggle');
        }
        else {            
            // pull data for sub item
            $.ajax({
                url: `/api/utilities/GetSubCategoriesByCategory?categoryId=${mainItem}`,
                type: 'Get',
                dataType: 'json',
                async: false,
                success: function (data) {
                    sub_item_options = sub_item_options + "<option value=''>select sub item</option>";                  
                    $.each(data, function (key, item) {                    
                        sub_item_options = sub_item_options +`<option value='${item.Id}'>${item.SubCategoryName}</option>`;
                    });
                    sub_item_options = sub_item_options +"<option disabled='disabled'>---------</option>";
                    sub_item_options = sub_item_options +"<option value='sub'>Add New</option>";
                },
                error: function (data) {
                }
            }); 

            $(this).closest('tr').find('.sub_item_dropdown').empty().append(sub_item_options); 
        }
    });



    $(document).on('change', '.select_column_no', function () {
        var columnNo = $(this).val();
        var columnInputContaner = $('.input-container-3');
        columnInputContaner.empty();
        if (columnNo=="1") {
            for (var i = 1; i <= 1; i++) {
                columnInputContaner.append(`
                            <div class="form-group row">
                                <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                <div class="col-md-7">
                                    <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_${i}">
                                </div>
                            </div>
                `);
            }
        }
        if (columnNo == "2") {
            for (var i = 1; i <= 2; i++) {
                columnInputContaner.append(`
                            <div class="form-group row">
                                <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                <div class="col-md-7">
                                    <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_${i}">
                                </div>
                            </div>
                `);
            }
        }
        if (columnNo == "3") {
            for (var i = 1; i <= 3; i++) {
                columnInputContaner.append(`
                            <div class="form-group row">
                                <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                <div class="col-md-7">
                                    <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_${i}">
                                </div>
                            </div>
                `);
            }
        }
    });


    $(document).on('change', '.method_dropdown', function () {
        var data_for_options = "";
        var methodId = $(this).val();
        var dependency = $('option:selected', this).attr('data-dependency');

        if (methodId == "" || methodId == undefined || methodId == null) {
            return;
        }
        else {
            // pull data for dependency
            if (dependency == "dp") {
                $.ajax({
                    url: `/api/Departments`,
                    type: 'Get',
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        data_for_options = "";
                        data_for_options = "<option value=''>select item</option>";

                        $.each(data, function (key, item) {
                            data_for_options = data_for_options + `<option value='${item.Id}'>${item.DepartmentName}</option>`;
                        });
                    },
                    error: function (data) {
                    }
                });
            }
            if (dependency == "in") {
                $.ajax({
                    url: `/api/InCharges`,
                    type: 'Get',
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        data_for_options = "";
                        data_for_options = "<option value=''>select item</option>";
                        $.each(data, function (key, item) {
                            data_for_options = data_for_options + `<option value='${item.Id}'>${item.InChargeName}</option>`;
                        });
                    },
                    error: function (data) {
                    }
                });
            }

            $(this).closest('tr').find('.data_for_dropdown').empty().append(data_for_options);
            $(this).closest('tr').find('.data_for_dropdown').select2();
        }
    });

    $(document).on('change', '.sub_item_dropdown', function () {
        var subItem = $(this).val();
        if (subItem == '' || subItem == null || subItem == undefined) {

        } else {

            if (subItem == "sub") {
                var row = $(this).closest("tr");
                $('#row_number_sub_item').val(row[0].dataset.count);
                $.ajax({
                    url: '/api/utilities/GetCategories/',
                    type: 'Get',
                    dataType: 'json',
                    success: function (data) {
                        $('#setting_dropdowns').empty();
                        $('#setting_dropdowns').append(`<option value=''>Select Item</option>`);
                        $.each(data, function (key, item) {
                            $('#setting_dropdowns').append(`<option value='${item.Id}'>${item.CategoryName}</option>`);
                        });
                    },
                    error: function (data) {
                    }
                });
                $('#add_sub_item_modal').modal('toggle');
            } else {
                var details_item_options = "";

                // pull data for details item
                $.ajax({
                    url: `/api/utilities/GetDetailsItemBySubItemsId?subItemId=${subItem}`,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        debugger;
                        //var sub_item_options = "";
                        details_item_options = details_item_options + "<option value=''>select details item</option>";
                        $.each(data, function (key, item) {
                            details_item_options = details_item_options + `<option value='${item.Id}'>${item.DetailsItemName}</option>`;
                        });
                        details_item_options = details_item_options + "<option disabled='disabled'>---------</option>";
                        details_item_options = details_item_options + "<option value='detail'>Add New</option>";

                    },
                    error: function (data) {
                    }
                });

                $(this).closest('tr').find('.detail_item_dropdown').empty().append(details_item_options);




            }
        }
    });
    $(document).on('change', '.detail_item_dropdown', function () {
        var detailItem = $(this).val();
        if (detailItem == "detail") {
            $('#add_detail_item_modal').modal('toggle');
            var row = $(this).closest("tr");
            $('#row_number_details_item').val(row[0].dataset.count);
            $.ajax({
                url: '/api/utilities/GetCategories/',
                type: 'Get',
                async: false,
                dataType: 'json',
                success: function (data) {
                    $('.category_dropdown').empty();
                    $('.category_dropdown').append(`<option value=''>Select Item</option>`);
                    $.each(data, function (key, item) {
                        $('.category_dropdown').append(`<option value='${item.Id}'>${item.CategoryName}</option>`);
                    });
                },
                error: function (data) {
                }
            });
        }
    });

    $(document).on('change', '#add_details_item_modal .category_dropdown', function () {
        var categoryId = $(this).val();
        $.ajax({
            url: `/api/utilities/GetSubCategoriesByCategory?categoryId=${categoryId}`,
            type: 'Get',
            async: false,
            dataType: 'json',
            success: function (data) {
                debugger;
                $('#add_details_item_modal #sub_category_dropdown').empty();
                $('#add_details_item_modal #sub_category_dropdown').append(`<option value=''>Select Item</option>`);
                $.each(data, function (key, item) {
                    $('#add_details_item_modal #sub_category_dropdown').append(`<option value='${item.Id}'>${item.SubCategoryName}</option>`);
                });
            },
            error: function (data) {
            }
        });
    });

    //create new row and concate
    $(document).on('click', '#item_row_add ', function () {
        debugger;
        var $tr = $(this).closest('.item_row');
        var $clone = $tr.clone();
        $($clone[0].cells[4]).empty();
        $($clone[0].cells[4]).append(`<select class="data_for_dropdown" name="data_for_list_dropdown_for_setting" id="data_for_list_dropdown_for_setting${++globalCount}" multiple="multiple"></select>`);
        $clone[0].dataset.count = ++globalCount;
        $clone.find(':text').val('');
        $tr.after($clone);
        $clone.find('.setting_plus_icon').hide();
        $clone.find('.setting_minus_icon').show();
    });

    //remove rows
    $(document).on('click', '.setting_minus_icon ', function () {
        $(this).closest("tr").remove();
    });

    //clear dynamic table input fields
    $(document).on('click', '#clear_input_frm ', function () {
        $("#table_name").val('');
        $("#table_title").val('');
        $("#table_position").val('');
        $("#table_name_warning_msg").hide();
        $("#table_title_warning_msg").hide();
        $("#table_position_warning_msg").hide();
    });
    //clear dynamic table input fields
    $(document).on('click', '#clear_setting_form ', function (e) {
        globalCount = 1;
        //var html = $('#total_menu_setting_items_tbl').html();
        //$('#total_menu_setting_items_tbl').html('#total_menu_setting_items_tbl');
        $('#total_menu_setting_items_tbl').html(html);
        DynamicTableSetting();
    });


    //add form show when add button click
    $(document).on('click', '.list_table_add_btn ', function (e) {
        ClearInputEditForm();
        $(".frm_add_btn").text("追加 (add)");
        $(".frm_add_btn").attr("tag", "add");
        $('.table_input_frm_div').show();

    });

    //add form show when add button click
    $(document).on('click', '#table_input_frm_div_close ', function (e) {
        ClearInputEditForm();
        $('.table_input_frm_div').hide();

    });

    //edit button click: fill up the input form with checked value
    $(document).on('click', '.list_table_edit_btn ', function (e) {
        var responseData = '';
        var tableId = $('.table_list_radio:checked').val();
        if (tableId == null || tableId == undefined || tableId == "") {
            alert("please select a table!");
        } else {
            //ajax call here
            debugger;
            var count = 0;
            var columnInputContainer = $('.input-container-3');
            $.ajax({
                url: `/api/utilities/GetDynamicTableById/${tableId}`,
                type: 'Get',
                async: false,
                dataType: 'json',
                success: function (data) {

                    responseData = data;
                    $("#table_name_input").val(data.TableName);
                    $("#table_title_input").val(data.TableTitle);
                    $("#table_position_input").val(data.TablePosition);
                    if (data.CategoryTitle != "") {
                        count++;
                    }
                    if (data.SubCategoryTitle != "") {
                        count++;
                    }
                    if (data.DetailsTitle != "") {
                        count++;
                    }
                },
                error: function (data) {
                }
            });
            //debugger;
            //get value by id and set to the form
            //$("#table_name_input").val("test-1111	");
            //$("#table_title_input").val("test--332222");
            //$("#table_position_input").val("33322111");
            //$(".select_column_no").val(2);

            //$("#table_main_item_input").val("NewBlend");
            //$("#table_sub_item_input").val("sub-item");
            $('.table_input_frm_div').show();
            $(".select_column_no").val(count);
            columnInputContainer.empty();
            if (count == 1) {
                if (responseData.CategoryTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-7">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_1" value='${responseData.CategoryTitle}'>
                                    </div>
                                </div>
                    `);

                }

                if (responseData.SubCategoryTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-7">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_2" value='${responseData.SubCategoryTitle}'>
                                    </div>
                                </div>
                    `);
                }

                if (responseData.DetailsTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-7">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_3" value='${responseData.DetailsTitle}'>
                                    </div>
                                </div>
                    `);
                }
            }
            if (count == 2) {
                if (responseData.CategoryTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-7">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_1" value='${responseData.CategoryTitle}'>
                                    </div>
                                </div>
                    `);

                }

                if (responseData.SubCategoryTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-7">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_2" value='${responseData.SubCategoryTitle}'>
                                    </div>
                                </div>
                    `);
                }

                if (responseData.DetailsTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-7">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_3" value='${responseData.DetailsTitle}'>
                                    </div>
                                </div>
                    `);
                }
            }
            if (count == 3) {
                if (responseData.CategoryTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_1" value='${responseData.CategoryTitle}'>
                                    </div>
                                </div>
                    `);

                }

                if (responseData.SubCategoryTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_2" value='${responseData.SubCategoryTitle}'>
                                    </div>
                                </div>
                    `);
                }

                if (responseData.DetailsTitle != "") {

                    columnInputContainer.append(`
                                <div class="form-group row">
                                    <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_3" value='${responseData.DetailsTitle}'>
                                    </div>
                                </div>
                    `);
                }
            }


            $(".frm_add_btn").text("編集​ (edit)");
            $(".frm_add_btn").attr("tag", "edit");

        }
    });
    //$(document).on('click', '.frm_cancel_btn ', function (e) {
    //    ClearInputEditForm();
    //});

    //setting button click, show the item modal.
    $(document).on('click', '.frm_setting_btn ', function (e) {
        var tableId = $('.table_list_radio:checked').val();
        var dynamicTable;
        if (tableId == null || tableId == undefined || tableId == "") {
            alert("please select a table!");
        } else {
            //ajax call here
            $.ajax({
                url: '/api/utilities/GetDynamicTableById/' + tableId,
                type: 'Get',
                dataType: 'json',
                async: false,
                success: function (data) {
                    dynamicTable = data;

                },
                error: function (data) {
                }
            });
            if (dynamicTable.CategoryTitle == "" || dynamicTable.CategoryTitle == null || dynamicTable.CategoryTitle == undefined) {
                alert('Main Title not defined!');
                return false;
            }
            //get value by id and set to the modal 
            $('.main_item_list_tbl tbody').empty();

            // pull data for main item
            $.ajax({
                url: '/api/utilities/GetCategories?dynamicTableId=' + tableId,
                type: 'Get',
                dataType: 'json',
                async: false,
                success: function (data) {
                    $('.main_item_list_tbl tbody').empty();
                    $.each(data, function (index, value) {
                        $('.main_item_list_tbl tbody').append(`
                                <tr data-id='${value.Id}'>
                                    <td class="main_item_input_td">
                                        <a class="main_item_link" href="#">${value.CategoryName}</a>
                                    </td>
                                    <td class="main_item_input_td">
                                        <a class="main_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="main_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                    });
                    

                },
                error: function (data) {
                }
            });

            if (dynamicTable.SubCategoryTitle == "" || dynamicTable.SubCategoryTitle == null || dynamicTable.SubCategoryTitle == undefined) {
                $('.main_item_list_tbl tbody .main_item_input_td a').removeClass('main_item_link');
            }

            $('#main_item_list_modal').modal('show');
        }
    });

    $(document).on('click', '.main_item_del_btn', function () {
        var buttonElement = $(this);
        var categoryId = buttonElement.closest('tr').attr('data-id');
        $('#main_item_del_id').val(categoryId);
        $('#main_item_list_modal').modal('hide');
        setTimeout(() => {
            $('#delete_main_item_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.confrim_cancel_btn_main_item', function () {

        $('#delete_main_item_modal').modal('hide');
        setTimeout(() => {
            $('#main_item_list_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.confrim_del_btn_main_item', function () {
        var categoryId = $('#main_item_del_id').val();
        var apiurl = "/api/Utilities/RemoveCategory?categoryId=" + categoryId;
        var tableId = $('#table_id_for_edit').val();
        var dynamicTable; 
        var dynamicTableId = $('#table_id_for_edit').val();

        $.ajax({
            url: apiurl,
            type: 'DELETE',
            dataType: 'json',
            success: function (data) {
                ToastMessageSuccess(data);

                $.ajax({
                    url: '/api/utilities/GetDynamicTableById/' + tableId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        dynamicTable = data;

                    },
                    error: function (data) {
                    }
                });


                // pull data for main item
                $.ajax({
                    url: '/api/utilities/GetCategories?dynamicTableId=' + dynamicTableId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        $('.main_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.main_item_list_tbl tbody').append(`
                                <tr data-id='${value.Id}'>
                                    <td class="main_item_input_td">
                                        <a class="main_item_link" href="#" onclick='clickInside();'">${value.CategoryName}</a>
                                    </td>
                                    <td class="main_item_input_td">
                                        <a class="main_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="main_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });

                    },
                    error: function (data) {
                    }
                });

                if (dynamicTable.SubCategoryTitle == "" || dynamicTable.SubCategoryTitle == null || dynamicTable.SubCategoryTitle == undefined) {
                    $('.main_item_list_tbl tbody .main_item_input_td a').removeClass('main_item_link');
                }
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#delete_main_item_modal').modal('hide');
        setTimeout(() => {
            $('#main_item_list_modal').modal('show');
        }, 600);
    });



    $(document).on('click', '.sub_item_del_btn', function () {
        var buttonElement = $(this);
        var mainItemId = $('#main_item_id').val();
        var subItemId = buttonElement.closest('tr').attr('data-sub-item-id');
        $('#sub_item_del_id').val(subItemId);
        $('#main_item_del_id_in_sub_item_delete_modal').val(mainItemId);
        $('#sub_item_list_modal').modal('hide');
        setTimeout(() => {
            $('#delete_sub_item_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.confrim_cancel_btn_sub_item', function () {

        $('#delete_sub_item_modal').modal('hide');
        setTimeout(() => {
            $('#sub_item_list_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.confrim_del_btn_sub_item', function () {
        var mainItemId = $('#main_item_del_id_in_sub_item_delete_modal').val();
        var subItemId = $('#sub_item_del_id').val();
        var apiurl = "/api/Utilities/RemoveSubCategory?subCategoryId=" + subItemId;
        var tableId = $('#table_id_for_edit').val();
        var dynamicTable;

        $.ajax({
            url: '/api/utilities/GetDynamicTableById/' + tableId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                dynamicTable = data;

            },
            error: function (data) {
            }
        });

        $.ajax({
            url: apiurl,
            type: 'DELETE',
            dataType: 'json',
            async: false,
            success: function (data) {
                ToastMessageSuccess(data);

                // pull data for sub main item
                $.ajax({
                    url: '/api/utilities/GetSubCategoriesByCategory?categoryId=' + mainItemId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {

                        $('.sub_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.sub_item_list_tbl tbody').append(`
                                <tr data-sub-item-id='${value.Id}'>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_link" href="#">${value.SubCategoryName}</a>
                                    </td>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="sub_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });
                    },
                    error: function (data) {
                    }
                });

                if (dynamicTable.DetailsTitle == "" || dynamicTable.DetailsTitle == null || dynamicTable.DetailsTitle == undefined) {
                    $('.sub_item_list_tbl tbody .sub_item_input_td a').removeClass('sub_item_link');
                }
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#delete_sub_item_modal').modal('hide');
        setTimeout(() => {
            $('#sub_item_list_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.detail_item_del_btn', function () {
        var buttonElement = $(this);
        var detailId = buttonElement.closest('tr').attr('data-detail-item-id');
        var subItemId = $('#sub_item_id').val();
        $('#detail_item_del_id').val(detailId);
        $('#sub_item_del_id_in_detail_item_delete_modal').val(subItemId);
        $('#detail_item_list_modal').modal('hide');
        setTimeout(() => {
            $('#delete_detail_item_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.confrim_cancel_btn_detail_item', function () {

        $('#delete_detail_item_modal').modal('hide');
        setTimeout(() => {
            $('#detail_item_list_modal').modal('show');
        }, 600);
    });


    $(document).on('click', '.confrim_del_btn_detail_item', function () {

        var detailId = $('#detail_item_del_id').val();
        var subItemId = $('#sub_item_del_id_in_detail_item_delete_modal').val();
        var apiurl = "/api/Utilities/RemoveDetailItem?detailId=" + detailId;
        debugger;

        $.ajax({
            url: apiurl,
            type: 'DELETE',
            dataType: 'json',
            async: false,
            success: function (data) {
                ToastMessageSuccess(data);

                // pull data for detail item
                $.ajax({
                    url: '/api/utilities/GetDetailsItemBySubItemsId?subItemId=' + subItemId,
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {

                        $('.detail_item_list_tbl tbody').empty();
                        $.each(data, function (index, value) {
                            $('.detail_item_list_tbl tbody').append(`
                                <tr data-detail-item-id='${value.Id}'>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_link" href="#">${value.DetailsItemName}</a>
                                    </td>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                        });
                    },
                    error: function (data) {
                    }
                });
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });

        $('#delete_detail_item_modal').modal('hide');
        setTimeout(() => {
            $('#detail_item_list_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.main_item_link', function (e) {
        
        var mainItemId = $(this).closest('tr').data('id');
        var tableId = $('#table_id_for_edit').val();
        var dynamicTable;

        $('#main_item_list_modal').modal('hide');

        $.ajax({
            url: '/api/utilities/GetDynamicTableById/' + tableId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                dynamicTable = data;

            },
            error: function (data) {
            }
        });

        $.ajax({
            url: '/api/utilities/GetCategoryById?categoryId=' + mainItemId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                $('.main_item_name').empty();
                $('.main_item_name').append(data.CategoryName);
                $('#main_item_id').val(data.Id);

            },
            error: function (data) {
            }
        });

        $.ajax({
            url: '/api/utilities/GetSubCategoriesByCategory?categoryId=' + mainItemId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {

                $('.sub_item_list_tbl tbody').empty();
                $.each(data, function (index, value) {
                    $('.sub_item_list_tbl tbody').append(`
                                <tr data-sub-item-id='${value.Id}'>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_link" href="#">${value.SubCategoryName}</a>
                                    </td>
                                    <td class="sub_item_input_td">
                                        <a class="sub_item_edit_btn" href="javascript:void(0);" data-toggle="modal" data-target="#main_item_edit">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="sub_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                });
            },
            error: function (data) {
            }
        });

        if (dynamicTable.DetailsTitle == "" || dynamicTable.DetailsTitle == null || dynamicTable.DetailsTitle == undefined) {
            $('.sub_item_list_tbl tbody .sub_item_input_td a').removeClass('sub_item_link');
        }

        setTimeout(function () {
            $('#sub_item_list_modal').modal('show');
        }, 600);
        
    });

    $(document).on('click', '.sub_item_link', function (e) {
        debugger;
        var subItemId = $(this).closest('tr').data('sub-item-id');

        $('#sub_item_list_modal').modal('hide');

        $.ajax({
            url: '/api/utilities/GetSubCategorieById?subCategoryId=' + subItemId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                console.log(data);
                $('.main_item_name').empty();
                $('.main_item_name').append(data.CategoryName);
                $('.sub_item_name').empty();
                $('.sub_item_name').append(data.SubCategoryName);
                $('#sub_item_id').val(data.Id);

            },
            error: function (data) {
            }
        });

        $.ajax({
            url: '/api/utilities/GetDetailsItemBySubItemsId?subItemId=' + subItemId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {

                $('.detail_item_list_tbl tbody').empty();
                $.each(data, function (index, value) {
                    $('.detail_item_list_tbl tbody').append(`
                                <tr data-detail-item-id='${value.Id}'>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_link" href="#">${value.DetailsItemName}</a>
                                    </td>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_edit_btn" href="javascript:void(0);">編集 (edit)</a>
                                        <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除​ (delete)</a>
                                    </td>
                                </tr>

                            `);
                });
            },
            error: function (data) {
            }
        });

        setTimeout(function () {
            $('#detail_item_list_modal').modal('show');
        }, 600);

    });

    $(document).on('click', '#sub_item_close', function () {
        $('#sub_item_list_modal').modal('hide');
        setTimeout(function () {
            $('#main_item_list_modal').modal('show');
        }, 600);
    });

    $(document).on('click', '.sub_item_close_btn_footer', function () {
        $('#sub_item_list_modal').modal('hide');
        setTimeout(function () {
            $('#main_item_list_modal').modal('show');
        }, 600);
    });


    $(document).on('click', '#detail_item_close', function () {
        $('#detail_item_list_modal').modal('hide');
        setTimeout(function () {
            $('#sub_item_list_modal').modal('show');
        }, 600);
    });
    $(document).on('click', '.detail_item_close_btn_footer', function () {
        $('#detail_item_list_modal').modal('hide');
        setTimeout(function () {
            $('#sub_item_list_modal').modal('show');
        }, 600);
    });



});

//function clickInside() {
//    alert('clicked');
//}

//const modally = new Modally({
//    disableScroll: true,
//    selector: '.modally-init',
//})

//modally.add('ipsum', {
//    maxWidth: 800,
//    selector: '#ipsum'
//})

//document.addEventListener('modally:opening:ipsum', function (e) {
//    console.log(e.detail);
//    e.detail.template.querySelector('.modally-content').innerHTML = 'Hello world!';
//});



//get setting list value
function GetAllSettingValue() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#setting_list_body').empty();
            $.each(data, function (key, item) {
                $('#setting_list_body').append(`<tr><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td><td><label id="dynamic_table_delete"><a id="dynamic_table_delete_link" href="javascript:void(0);" data-toggle="modal" data-target="#delete_dynamic_table" onClick="DeleteDynmaicTalbe(${item.Id})">削除</a></label><label id="dynamic_table_edit_label"><a id="dynamic_table_edit_link" href="javascript:void(0);" data-toggle="modal" data-target="#edit_dynamic_table_modal" onClick="GetDynamicTalbeById(${item.Id})">編集</a></label></td></tr>`);                
            });
        });
}



// not needed may be.
function GetDynamicSettings() {
    var dynamicTableId = $('#dynamic_table_list_for_setting').val();

    $.ajax({
        url: `/api/utilities/GetDynamicSettingsByDynamicTableId?dynamicTableId=${dynamicTableId}`,
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            $('#setting_list_body').empty();
            $.each(data, function (key, item) {
                $('#setting_list_body').append(`<tr><td>${item.DynamicTableName}</td><td>${item.CategoryName}</td><td>${item.SubCategoryName}</td><td>${item.DetailsItemName}</td><td>${item.MethodName}</td><td>${item.CommaSeperatedParameterName}</td></tr>`);
            });
        },
        error: function (data) {
        }
    });
}


//input form clear button
function ClearInputEditForm(){
    $("#table_name_input").val("");
    $("#table_title_input").val("");
    $("#table_position_input").val("");

    $(".select_column_no").val(-1);
    
    $("#table_main_item_input").val("");
    $("#table_sub_item_input").val("");

    $('.input-container-3').empty();
}

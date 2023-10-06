var globalCount = 1;
var globalSettingList = [];
var html;
$(document).ready(function () {
    html = $('#total_menu_setting_items_tbl').html();
    //get table lsit and setting list on page load. 
    GetDynamicTables();
    GetDynamicTablesForSetting();
});

function InsertDynamicTables() {
    debugger;
    var apiurl = "/api/Utilities/CreateDynamicTable";
    var tableName = $("#table_name").val();
    var tableTitle = $("#table_title").val();
    var tablePosition = $("#table_position").val();
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

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                if(data==1){
                    alert("同一データが登録済みです.");
                }else{
                    $("#page_load_after_modal_close").val("yes");
                    $("#table_name").val('');
                    $("#table_title").val('');
                    $("#table_position").val('');
                    ToastMessageSuccess(data);
                    GetDynamicTables();
                    GetDynamicTablesForSetting();
                }                
            },
            error: function (data) {
                ToastMessageFailed(data);
            }
        });
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

//update main item
$(document).on('click', '#update_main_item ', function () {   
    var categoryName = $('#input_main_item').val();
    if (categoryName == '' || categoryName == undefined || categoryName == null) {
        alert('Main item required!');
        return;
    }
    else {
        $.ajax({
            url: `/api/utilities/CreateCategory`,
            type: 'POST',
            dataType: 'json',
            data: {
                CategoryName: categoryName
                },
            success: function (data) {
                ToastMessageSuccess(data);
                // pull data for main item
                $.ajax({
                    url: '/api/utilities/GetCategories/',
                    type: 'Get',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        $('.main_item_dropdown').empty();
                        $('.main_item_dropdown').append(`<option value=''>Select Item</option>`);
                        $.each(data, function (key, item) {
                            $('.main_item_dropdown').append(`<option value='${item.Id}'>${item.CategoryName}</option>`);
                        });
                        $('.main_item_dropdown').append(`<option disabled="disabled">---------</option>`);
                        $('.main_item_dropdown').append(`<option value='main'>Add New</option>`);
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
//update sub item
$(document).on('click', '#update_sub_item ', function () {   

    var categoryId = $('.setting_dropdowns').val();
    var subCategoryName = $('#input_sub_item').val();
    var rowNumber = $('#row_number_sub_item').val();

    $.ajax({
        url: `/api/utilities/CreateSubCategory/`,
        type: 'POST',
        dataType: 'json',
        data: {
            CategoryId: categoryId,
            SubCategoryName: subCategoryName
        },
        success: function (data) {
            if (data == 1) {
                alert("同一データが登録済みです.");
            } else {
                ToastMessageSuccess(data);
                var allTr = $('#item_tbody tr');
                $.each(allTr, function (index, item) {
                    if (parseInt($(item)[0].dataset.count) == parseInt(rowNumber)) {
                       
                        var sub_item_options = "";
                        // pull data for sub item
                        $.ajax({
                            url: `/api/utilities/GetSubCategoriesByCategory?categoryId=${categoryId}`,
                            type: 'Get',
                            dataType: 'json',
                            async: false,
                            success: function (data) {
                                sub_item_options = sub_item_options + "<option value=''>select sub item</option>";
                                $.each(data, function (key, item) {
                                    sub_item_options = sub_item_options + `<option value='${item.Id}'>${item.SubCategoryName}</option>`;
                                });
                                sub_item_options = sub_item_options + "<option disabled='disabled'>---------</option>";
                                sub_item_options = sub_item_options + "<option value='sub'>Add New</option>";
                            },
                            error: function (data) {
                            }
                        });
                        $(item).closest('tr').find('.sub_item_dropdown').empty().append(sub_item_options); 
                      
                    }
                });
                
            }
        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });

});
//update detail item
$(document).on('click', '#update_detail_item ', function () { 
    var subItemId = $('#sub_category_dropdown').val();
    var detailsItemName = $('#input_detail_item').val();
    var rowNumber = $('#row_number_details_item').val();
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
            var allTr = $('#item_tbody tr');
            $.each(allTr, function (index, item) {
                if (parseInt($(item)[0].dataset.count) == parseInt(rowNumber)) {

                    var details_item_options = "";
                    // pull data for details item
                    $.ajax({
                        url: `/api/utilities/GetDetailsItemBySubItemsId?subItemId=${subItemId}`,
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
                    $(item).closest('tr').find('.detail_item_dropdown').empty().append(details_item_options);

                }
            });

        },
        error: function (data) {
            ToastMessageFailed(data);
        }
    });
});
//update table setting
$(document).on('click', '#udpate_tbl_setting ', function () {     
});

function DeleteDynmaicTalbe(dynamicTableId){
    $("#table_id_for_delete").val(dynamicTableId);
}

//delete dynamic table by table id
$(document).on('click', '#delete_table_link ', function () { 

    var apiurl = "/api/Utilities/InactiveDynamicTable";
    var id = $("#table_id_for_delete").val();
    var tableName ="";
    var tableTitle ="";
    var tablePosition ="";

    var data = {
        Id:id,
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
//delete dynamic table by table id
$(document).on('click', '#delete_setting_link ', function () { 

    var apiurl = "/api/Utilities/InactiveDynamicTable";
    var id = $("#table_id_for_delete").val();
    var tableName ="";
    var tableTitle ="";
    var tablePosition ="";

    var data = {
        Id:id,
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

$( document ).ready(function() {   
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
        var columnInputContaner = $('.input_container');
        columnInputContaner.empty();
        if (columnNo=="1") {
            for (var i = 1; i <= 1; i++) {
                columnInputContaner.append(`
                            <div class="form-group row">
                                <label class="col-form-label col-md-1"></label>
                                <label class="col-form-label col-md-3 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                <div class="col-md-4">
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
                                <label class="col-form-label col-md-1"></label>
                                <label class="col-form-label col-md-3 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                <div class="col-md-4">
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
                                <label class="col-form-label col-md-1"></label>
                                <label class="col-form-label col-md-3 input_table_frm_lbl2">大項目ヘッダータイトル​</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_${i}">
                                </div>
                            </div>
                `);
            }
        }
    });

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
                async:false,
                dataType: 'json',
                success: function (data) {    
                    data_for_options ="";
                    data_for_options = "<option value=''>select item</option>";

                    $.each(data, function (key, item) {                        
                        data_for_options = data_for_options +`<option value='${item.Id}'>${item.DepartmentName}</option>`;
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
                async:false,
                dataType: 'json',
                success: function (data) {
                    data_for_options ="";
                    data_for_options = "<option value=''>select item</option>";
                    $.each(data, function (key, item) {
                        data_for_options = data_for_options +`<option value='${item.Id}'>${item.InChargeName}</option>`;
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
	
}else{

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
	}else{
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
    if(detailItem=="detail"){        
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

$(document).ready(function ()
{
    //add form show when add button click
    $(document).on('click', '.list_table_add_btn ', function (e) {
        ClearInputEditForm();
        $(".frm_add_btn").text("追加 (add)");    
        
        $('.table_input_frm_div').show();    
        $('#table_name_input').focus()
    });

    //edit button click: fill up the input form with checked value
    $(document).on('click', '.list_table_edit_btn ', function (e) {
        var tableId = $('.table_list_radio:checked').val();
        if (tableId == null || tableId == undefined || tableId == "") {
            alert("please select a table!");
        }else{
            //ajax call here
            //get value by id and set to the form
            $('#table_name_input').focus();
            $("#table_name_input").val("test-1111");
            
            $("#table_title_input").val("test--332222");
            $("#table_position_input").val("33322111");

            $(".select_column_no").val(2);
            
            $("#table_main_item_input").val("NewBlend");
            $("#table_sub_item_input").val("sub-item");
            $(".frm_add_btn").text("編集​ (edit)");
                        
            $('.table_input_frm_div').show();   
        }    
    });
});

$(document).on('click', '.frm_cancel_btn ', function (e) {
    ClearInputEditForm();
});
//input form clear button
function ClearInputEditForm(){
    $("#table_name_input").val("");
    $("#table_title_input").val("");
    $("#table_position_input").val("");

    $(".select_column_no").val(-1);
    
    $("#table_main_item_input").val("");
    $("#table_sub_item_input").val("");
}
//setting button click, show the item modal.
$(document).on('click', '.frm_setting_btn ', function (e) {
    var tableId = $('.table_list_radio:checked').val();
    if (tableId == null || tableId == undefined || tableId == "") {
        alert("please select a table!");
    }else{
        //ajax call here
        //get value by id and set to the modal        
        $('#main_item_list').modal('show');
    }    
});

//modal show hide
// $(document).on('click', '.main_item_add_btn ', function (e) {
//     $('#main_item_list').modal('hide');
//     $('#sub_item_list').modal('show');
// });
//edit modal open for main item

$(document).on('click', '.main_item_add_btn ', function (e) {
    $('#main_item_list').modal('hide');
    //e.preventDefault();
    //console.log("edit clicked");

    //$('#sub_item_list').modal('show');

    // $('#main_item_list').modal('toggle');
    // $('#main_item_edit').modal('toggle');
    // $('#main_item_list').modal('hide');
    // $('#main_item_edit').modal('show');
    
    
});

// $('#myModal').modal('hide')
// $('#myModal').on('hidden.bs.modal', function () {
//   // Load up a new modal...
//   $('#myModalNew').modal('show')
// })

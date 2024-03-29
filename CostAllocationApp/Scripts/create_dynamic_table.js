﻿var globalCount = 1;
var globalSettingList = [];
var html;

$(document).ready(function () {
    html = $('#total_menu_setting_items_tbl').html();

    //get table lsit and setting list on page load. 
    GetDynamicTables();
    GetDynamicTablesForSetting();

    //store tables information when radio button is clicked
    $(document).on('change', '#dynamic_list_tbody tr td .table_list_radio', function () {
        var selectedValue  = $('input[name="flexRadioDefault"]:checked').val();
        $('#table_id_for_edit').val(selectedValue);
        $('#table_id_for_delete').val(selectedValue);
        ClearInputEditForm();
        $('.table_input_frm_div').hide();
    });

    //add main item
    $(document).on('click', '.main_item_add_btn ', function () {
        var mainItem = $('#section-name').val();
        var tableId = $('#table_id_for_edit').val();
        var dynamicTable;
        if (mainItem == '' || mainItem == undefined || mainItem == null) {
            alert('メイン項目必須');
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
                    $('#section-name').val('');
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
                                var strCategoryList = "";
                                strCategoryList = strCategoryList +"    <tr data-id='"+value.Id+"'>";
                                strCategoryList = strCategoryList +"    <td class='main_item_input_td'>";
                                if(value.IsSubTitle){
                                    strCategoryList = strCategoryList +         "<a class='main_item_link' href='javascript:void(0);' >"+value.CategoryName+"</a>";
                                }else{
                                    strCategoryList = strCategoryList +         "<a>"+value.CategoryName+"</a>";
                                }                            
                                strCategoryList = strCategoryList +     "</td>";
                                strCategoryList = strCategoryList +     "<td class='main_item_input_td'>";
                                strCategoryList = strCategoryList +     "            <a class='main_item_edit_btn' href='javascript:void(0);'>編集</a>";
                                strCategoryList = strCategoryList +     "            <a href='javascript:void(0);' class='main_item_del_btn' id=''>削除</a>";
                                strCategoryList = strCategoryList +     "        </td>";
                                strCategoryList = strCategoryList +     "    </tr>";
                                $('.main_item_list_tbl tbody').append(strCategoryList);
                            });
                            
                        },
                        error: function (data) {
                        }
                    });
                },
                error: function (data) {
                    ToastMessageFailed("操作が失敗しました");
                }
            });
        }
    });

    //main item update button clicked, show the edit modal for main item
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

    //main item update from modal
    $(document).on('click', '.main_item_edit_action ', function () {    
        var mainItemId = $('#main_item_id_edit_input').val();
        var mainItemName = $('#main_item_edit_input').val();
        var dynamicTableId = $('#table_id_for_edit').val();
    
        if (mainItemName == '' || mainItemName == undefined || mainItemName == null) {
            alert('メイン項目必須');
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
                                var strCategoryList = "";
                                strCategoryList = strCategoryList +"    <tr data-id='"+value.Id+"'>";
                                strCategoryList = strCategoryList +"    <td class='main_item_input_td'>";
                                if(value.IsSubTitle){
                                    strCategoryList = strCategoryList +         "<a class='main_item_link' href='javascript:void(0);' >"+value.CategoryName+"</a>";
                                }else{
                                    strCategoryList = strCategoryList +         "<a>"+value.CategoryName+"</a>";
                                }                            
                                strCategoryList = strCategoryList +     "</td>";
                                strCategoryList = strCategoryList +     "<td class='main_item_input_td'>";
                                strCategoryList = strCategoryList +     "            <a class='main_item_edit_btn' href='javascript:void(0);'>編集</a>";
                                strCategoryList = strCategoryList +     "            <a href='javascript:void(0);' class='main_item_del_btn' id=''>削除</a>";
                                strCategoryList = strCategoryList +     "        </td>";
                                strCategoryList = strCategoryList +     "    </tr>";
                                $('.main_item_list_tbl tbody').append(strCategoryList);
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
                    ToastMessageFailed("操作が失敗しました");
                }
            });
        }
    });

    //main item edit modal close 
    $(document).on('click', '.main_item_edit_modal_close', () => {
        $('#main_item_edit_modal').modal('hide');
        setTimeout(() => {
            $('#main_item_list_modal').modal('show');
        }, 600);
    });

    //sub item edit modal close 
    $(document).on('click', '.sub_item_edit_modal_close', () => {
        $('#sub_item_edit_modal').modal('hide');
        setTimeout(() => {
            $('#sub_item_list_modal').modal('show');
        }, 600);
    });

    //detail item edit modal close 
    $(document).on('click', '.detail_item_edit_modal_close', () => {
        $('#detail_item_edit_modal').modal('hide');
        setTimeout(() => {
            $('#detail_item_list_modal').modal('show');
        }, 600);
    });
    
    //sub item update button clicked, show the edit modal for sub item
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
    //sub item update from modal
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
                                var strDetailsItemList = "";
                                strDetailsItemList = strDetailsItemList +"<tr data-sub-item-id='"+value.Id+"'>";
                                strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                                if(value.IsDetailTitle){
                                    strDetailsItemList = strDetailsItemList +"<a class='sub_item_link' href='javascript:void(0)'>"+value.SubCategoryName+"</a>";
                                }else{
                                    strDetailsItemList = strDetailsItemList +"<a>"+value.SubCategoryName+"</a>";
                                }                            
                                strDetailsItemList = strDetailsItemList +"</td>";
                                strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                                strDetailsItemList = strDetailsItemList +"<a class='sub_item_edit_btn' href='javascript:void(0);'>編集</a>";
                                strDetailsItemList = strDetailsItemList +"<a href='javascript:void(0);' class='sub_item_del_btn' id=''>削除</a>";
                                strDetailsItemList = strDetailsItemList +"</td>";
                                strDetailsItemList = strDetailsItemList +"</tr>";
                                $('.sub_item_list_tbl tbody').append(strDetailsItemList);
                            });

                            $('#sub_item_edit_modal').modal('hide');
                            setTimeout(() => {
                                $('#sub_item_list_modal').modal('show');
                            }, 600);
                        },
                        error: function (data) {
                        }
                    });                
                },
                error: function (data) {
                    ToastMessageFailed("操作が失敗しました");
                }
            });
        }
    });
    
    //sub item add from modal
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
                    $('#input_sub_item').val('');
                    ToastMessageSuccess(data);
                    $.ajax({
                        url: '/api/utilities/GetSubCategoriesByCategory?categoryId=' + mainItemId,
                        type: 'Get',
                        dataType: 'json',
                        async: false,
                        success: function (data) {
    
                            $('.sub_item_list_tbl tbody').empty();
                            $.each(data, function (index, value) {
                                var strDetailsItemList = "";
                                strDetailsItemList = strDetailsItemList +"<tr data-sub-item-id='"+value.Id+"'>";
                                strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                                if(value.IsDetailTitle){
                                    strDetailsItemList = strDetailsItemList +"<a class='sub_item_link' href='javascript:void(0)'>"+value.SubCategoryName+"</a>";
                                }else{
                                    strDetailsItemList = strDetailsItemList +"<a>"+value.SubCategoryName+"</a>";
                                }                            
                                strDetailsItemList = strDetailsItemList +"</td>";
                                strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                                strDetailsItemList = strDetailsItemList +"<a class='sub_item_edit_btn' href='javascript:void(0);'>編集</a>";
                                strDetailsItemList = strDetailsItemList +"<a href='javascript:void(0);' class='sub_item_del_btn' id=''>削除</a>";
                                strDetailsItemList = strDetailsItemList +"</td>";
                                strDetailsItemList = strDetailsItemList +"</tr>";
                                $('.sub_item_list_tbl tbody').append(strDetailsItemList);
                            });
                        },
                        error: function (data) {
                        }
                    });                
                }
            },
            error: function (data) {
                ToastMessageFailed("操作が失敗しました");
            }
        });
    
    });

    //detail item add from modal
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
                $('#input_detail_item').val('');
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
                                            <a>${value.DetailsItemName}</a>
                                        </td>
                                        <td class="detail_item_input_td">
                                            <a class="detail_item_edit_btn" href="javascript:void(0);">編集</a>
                                            <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除</a>
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
                ToastMessageFailed("操作が失敗しました");
            }
        });
    });

    //detail item update button clicked, show the edit modal for detail item
    $(document).on('click', '.detail_item_edit_btn', function () {
        var detailItemId = $(this).closest('tr').data('detail-item-id');
        $('#detail_item_id_edit_input').val(detailItemId);
        $.ajax({
            url: '/api/utilities/GetDetailsItemById?detailsId=' + detailItemId,
            type: 'Get',
            dataType: 'json',
            async: false,
            success: function (data) {
                
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

    //detail item update from modal
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
                                            <a>${value.DetailsItemName}</a>
                                        </td>
                                        <td class="detail_item_input_td">
                                            <a class="detail_item_edit_btn" href="javascript:void(0);">編集</a>
                                            <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除</a>
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
                    ToastMessageFailed("操作が失敗しました");
                }
            });
        }
    
    });

    //delete dynamic table
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
                ToastMessageFailed("操作が失敗しました");
            }
        });
    
        $('#delete_dynamic_table').modal('toggle');
    });

    //select column number for table from the column dropdown and create input fields accordingly to that number
    $(document).on('change', '.select_column_no', function () {
        var columnNo = $(this).val();
        var columnInputContaner = $('#dynamic_column_title_block');
        columnInputContaner.empty();
        if (columnNo=="1") {
            for (var i = 1; i <= 1; i++) {
                columnInputContaner.append(`
                            <div class="form-group row">
                                <label class="col-form-label col-md-4 input_table_frm_lbl2">大項目ヘッダータイトル</label>
                                <div class="col-md-7">
                                    <input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_${i}">
                                </div>
                            </div>
                `);
            }
        }
        if (columnNo == "2") {            
            for (var i = 1; i <= 2; i++) {                
                var main_sub_item_title ="";            
                main_sub_item_title = main_sub_item_title +"<div class='form-group row'>";
                if(i==1){
                    main_sub_item_title = main_sub_item_title +"<label class='col-form-label col-md-4 input_table_frm_lbl2'>大項目ヘッダータイトル​</label>";
                    main_sub_item_title = main_sub_item_title +"<div class='col-md-7'>";
                    main_sub_item_title = main_sub_item_title +"<input type='text' class='form-control input_table_text_field' placeholder='ヘッダータイトルを入力​' name='test_input' id='column_input_"+i+"'>";
                }else{
                    main_sub_item_title = main_sub_item_title +"<label class='col-form-label col-md-4 input_table_frm_lbl2'>中項目ヘッダータイトル​</label>";
                    main_sub_item_title = main_sub_item_title +"<div class='col-md-7'>";
                    main_sub_item_title = main_sub_item_title +"<input type='text' class='form-control input_table_text_field' placeholder='ヘッダータイトルを入力​' name='test_input' id='column_input_"+i+"'>";
                }                                
                main_sub_item_title = main_sub_item_title +"</div>";
                main_sub_item_title = main_sub_item_title +"</div>";                
                columnInputContaner.append(main_sub_item_title);
            }
        }
        if (columnNo == "3") {            
            for (var i = 1; i <= 3; i++) {
                var main_sub_detial_item_title ="";
                main_sub_detial_item_title = main_sub_detial_item_title +"<div class='form-group row'>";
                if(i==1){
                    main_sub_detial_item_title = main_sub_detial_item_title +"<label class='col-form-label col-md-4 input_table_frm_lbl2'>大項目ヘッダータイトル​</label>";
                }else if(i==2){
                    main_sub_detial_item_title = main_sub_detial_item_title +"<label class='col-form-label col-md-4 input_table_frm_lbl2'>中項目ヘッダータイトル​</label>";
                }else {
                    main_sub_detial_item_title = main_sub_detial_item_title +"<label class='col-form-label col-md-4 input_table_frm_lbl2'>詳細項目ヘッダータイトル​</label>";
                }                
                main_sub_detial_item_title = main_sub_detial_item_title +"<div class='col-md-7'>";
                main_sub_detial_item_title = main_sub_detial_item_title +"<input type='text' class='form-control input_table_text_field' placeholder='ヘッダータイトルを入力​' name='test_input' id='column_input_"+i+"'>";
                main_sub_detial_item_title = main_sub_detial_item_title +"</div>";
                main_sub_detial_item_title = main_sub_detial_item_title +"</div>";
                columnInputContaner.append(main_sub_detial_item_title);
            }
        }
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

    //add form show when add button click
    $(document).on('click', '.list_table_add_btn ', function (e) {
        ClearInputEditForm();
        $("#dynamic_input_form_header").text("集計表追加");
        $("#table_format_add_btn").text("追　加");
        $("#table_format_add_btn").attr("tag", "add");
        $('#dynamic_column_title_block').empty();
        $('.table_input_frm_div').show();

    });

    //add form show when add button click
    $(document).on('click', '.table_input_frm_div_close ', function (e) {
        ClearInputEditForm();
        $('.table_input_frm_div').hide();

    });

    //edit button click: fill up the input form with checked value
    $(document).on('click', '.list_table_edit_btn ', function (e) {
        var responseData = '';
        var tableId = $('.table_list_radio:checked').val();
        if (tableId == null || tableId == undefined || tableId == "") {
            alert("テーブルを選択してください！");
        } else {
            var count = 0;
            var columnInputContainer = $('#dynamic_column_title_block');
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
            $('.table_input_frm_div').show();
            $(".select_column_no").val(count);
            columnInputContainer.empty();
            
            if (count == 1) {
                if (responseData.CategoryTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">大項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_1" value="' + responseData.CategoryTitle + '">' +
                        '</div>' +
                        '</div>');

                }

                if (responseData.SubCategoryTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">中項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_2" value="' + responseData.SubCategoryTitle + '">' +
                        '</div>' +
                        '</div>');
                }

                if (responseData.DetailsTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">詳細項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_3" value="' + responseData.DetailsTitle + '">' +
                        '</div>' +
                        '</div>');
                }
            }
            if (count == 2) {
                if (responseData.CategoryTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">大項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_1" value="' + responseData.CategoryTitle + '">' +
                        '</div>' +
                        '</div>');

                }

                if (responseData.SubCategoryTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">中項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_2" value="' + responseData.SubCategoryTitle + '">' +
                        '</div>' +
                        '</div>');
                }

                if (responseData.DetailsTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">詳細項目ヘッダーのタイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_3" value="' + responseData.DetailsTitle + '">' +
                        '</div>' +
                        '</div>');
                }
            }
            if (count == 3) {
                if (responseData.CategoryTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">大項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_1" value="' + responseData.CategoryTitle + '">' +
                        '</div>' +
                        '</div>');

                }

                if (responseData.SubCategoryTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">中項目ヘッダータイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_2" value="' + responseData.SubCategoryTitle + '">' +
                        '</div>' +
                        '</div>');
                }

                if (responseData.DetailsTitle != "") {

                    columnInputContainer.append('<div class="row mt-1">' +
                        '<div class="col-md-1" ></div >' +
                        '<div class="col-md-5 dynamic_column_title_label">' +
                        '<label class="input_table_frm_lbl">詳細項目ヘッダーのタイトル</label>' +
                        '</div>' +
                        '<div class="col-md-6 dynamic_column_title_input">' +
                        '<input type="text" class="form-control input_table_text_field" placeholder="ヘッダータイトルを入力​​" name="test_input" id="column_input_3" value="' + responseData.DetailsTitle + '">' +
                        '</div>' +
                        '</div>');
                }
            }

            $("#dynamic_input_form_header").text("集計表編集");
            $("#table_format_add_btn").text("保　存");
            $("#table_format_add_btn").attr("tag", "edit");

        }
    });

    //setting button click, show the item modal.
    $(document).on('click', '.frm_setting_btn ', function (e) {
        ClearInputEditForm();
        $('.table_input_frm_div').hide();
        var tableId = $('.table_list_radio:checked').val();
        var dynamicTable;
        if (tableId == null || tableId == undefined || tableId == "") {
            alert("テーブルを選択してください！");
        } else {
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
                alert('メインタイトルが定義されていません！');
                return false;
            }
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
                        var strCategoryList = "";
                        strCategoryList = strCategoryList +"    <tr data-id='"+value.Id+"'>";
                        strCategoryList = strCategoryList +"    <td class='main_item_input_td'>";
                        if(value.IsSubTitle){
                            strCategoryList = strCategoryList +         "<a class='main_item_link' href='javascript:void(0);' >"+value.CategoryName+"</a>";
                        }else{
                            strCategoryList = strCategoryList +         "<a>"+value.CategoryName+"</a>";
                        }                            
                        strCategoryList = strCategoryList +     "</td>";
                        strCategoryList = strCategoryList +     "<td class='main_item_input_td'>";
                        strCategoryList = strCategoryList +     "            <a class='main_item_edit_btn' href='javascript:void(0);'>編集</a>";
                        strCategoryList = strCategoryList +     "            <a href='javascript:void(0);' class='main_item_del_btn' id=''>削除</a>";
                        strCategoryList = strCategoryList +     "        </td>";
                        strCategoryList = strCategoryList +     "    </tr>";
                        $('.main_item_list_tbl tbody').append(strCategoryList);                                              
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

    //setting button click, show the item modal.
    $(document).on('click', '.list_table_delete_btn ', function (e) {        
        $('.table_input_frm_div').hide();
        var tableId = $('.table_list_radio:checked').val();
        var dynamicTable;
        if (tableId == null || tableId == undefined || tableId == "") {
            alert("テーブルを選択してください！");
        } else {            
            $('#delete_dynamic_table').modal('show');            
        }
    });

    //modal actions for: main,sub and details category

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
                            var strCategoryList = "";
                            strCategoryList = strCategoryList +"    <tr data-id='"+value.Id+"'>";
                            strCategoryList = strCategoryList +"    <td class='main_item_input_td'>";
                            if(value.IsSubTitle){
                                strCategoryList = strCategoryList +         "<a class='main_item_link' href='javascript:void(0);' >"+value.CategoryName+"</a>";
                            }else{
                                strCategoryList = strCategoryList +         "<a>"+value.CategoryName+"</a>";
                            }                            
                            strCategoryList = strCategoryList +     "</td>";
                            strCategoryList = strCategoryList +     "<td class='main_item_input_td'>";
                            strCategoryList = strCategoryList +     "            <a class='main_item_edit_btn' href='javascript:void(0);'>編集</a>";
                            strCategoryList = strCategoryList +     "            <a href='javascript:void(0);' class='main_item_del_btn' id=''>削除</a>";
                            strCategoryList = strCategoryList +     "        </td>";
                            strCategoryList = strCategoryList +     "    </tr>";
                            $('.main_item_list_tbl tbody').append(strCategoryList);
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
                ToastMessageFailed("操作が失敗しました");
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
                            var strDetailsItemList = "";
                            strDetailsItemList = strDetailsItemList +"<tr data-sub-item-id='"+value.Id+"'>";
                            strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                            if(value.IsDetailTitle){
                                strDetailsItemList = strDetailsItemList +"<a class='sub_item_link' href='javascript:void(0)'>"+value.SubCategoryName+"</a>";
                            }else{
                                strDetailsItemList = strDetailsItemList +"<a>"+value.SubCategoryName+"</a>";
                            }                            
                            strDetailsItemList = strDetailsItemList +"</td>";
                            strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                            strDetailsItemList = strDetailsItemList +"<a class='sub_item_edit_btn' href='javascript:void(0);'>編集</a>";
                            strDetailsItemList = strDetailsItemList +"<a href='javascript:void(0);' class='sub_item_del_btn' id=''>削除</a>";
                            strDetailsItemList = strDetailsItemList +"</td>";
                            strDetailsItemList = strDetailsItemList +"</tr>";
                            $('.sub_item_list_tbl tbody').append(strDetailsItemList);
                        });
                    },
                    error: function (data) {
                    }
                });                
            },
            error: function (data) {
                ToastMessageFailed("操作が失敗しました");
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
                                        <a>${value.DetailsItemName}</a>
                                    </td>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_edit_btn" href="javascript:void(0);">編集</a>
                                        <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除</a>
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
                ToastMessageFailed("操作が失敗しました");
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
                    var strDetailsItemList = "";
                    strDetailsItemList = strDetailsItemList +"<tr data-sub-item-id='"+value.Id+"'>";
                    strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                    if(value.IsDetailTitle){
                        strDetailsItemList = strDetailsItemList +"<a class='sub_item_link' href='javascript:void(0)'>"+value.SubCategoryName+"</a>";
                    }else{
                        strDetailsItemList = strDetailsItemList +"<a>"+value.SubCategoryName+"</a>";
                    }                            
                    strDetailsItemList = strDetailsItemList +"</td>";
                    strDetailsItemList = strDetailsItemList +"<td class='sub_item_input_td'>";
                    strDetailsItemList = strDetailsItemList +"<a class='sub_item_edit_btn' href='javascript:void(0);'>編集</a>";
                    strDetailsItemList = strDetailsItemList +"<a href='javascript:void(0);' class='sub_item_del_btn' id=''>削除</a>";
                    strDetailsItemList = strDetailsItemList +"</td>";
                    strDetailsItemList = strDetailsItemList +"</tr>";
                    $('.sub_item_list_tbl tbody').append(strDetailsItemList);
                });
            },
            error: function (data) {
            }
        });        
        setTimeout(function () {
            $('#sub_item_list_modal').modal('show');
        }, 600);
        
    });

    $(document).on('click', '.sub_item_link', function (e) {
       
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
                                        <a>${value.DetailsItemName}</a>
                                    </td>
                                    <td class="detail_item_input_td">
                                        <a class="detail_item_edit_btn" href="javascript:void(0);">編集</a>
                                        <a href="javascript:void(0);" class="detail_item_del_btn" id="">削除</a>
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

//Create/Update dynamic tables
function AddUpdateDynamicTables() {
    var apiurlInsert = "/api/Utilities/CreateDynamicTable";
    var apiurlUpdate = "/api/Utilities/UpdateDynamicTable";
    var buttonTag = $('#table_format_add_btn').attr('tag');

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
        alert("テーブル名を入力してください");
        return false;        
    }
    
    if (tableTitle == "") {
        alert("テーブルのタイトルを入力してください");            
        isValid = false;
        return false;     
    }

    if (tablePosition == "") {
        alert("テーブルの位置を入力してください");         
        isValid = false;
        return false;     
    }


    if(isValid) {    
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
                    ToastMessageFailed("操作が失敗しました");
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
                    ToastMessageFailed("操作が失敗しました");
                }
            });
        }

        
    }
}

//get all dynamic tables when page is loaded
function GetDynamicTables() {
    $.getJSON('/api/Utilities/GetDynamicTables')
        .done(function (data) {
            $('#dynamic_list_tbody').empty();
            $.each(data, function (key, item) {
                $('#dynamic_list_tbody').append(`<tr><td></thead><div class="form-check"><input class="form-check-input table_list_radio" type="radio" name="flexRadioDefault" id="flexRadioDefault1" value="${item.Id}"><label class="form-check-label" for="flexRadioDefault1">    </label></div></td><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td></tr>`);                
            });
        });
}

//get all dynamic tables for settings
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

//input form clear button
function ClearInputEditForm(){
    $("#table_name_input").val("");
    $("#table_title_input").val("");
    $("#table_position_input").val("");

    $(".select_column_no").val(-1);
    
    $("#table_main_item_input").val("");
    $("#table_sub_item_input").val("");

    //$('.input-container-3').empty();
    $('#dynamic_column_title_block').empty();
}

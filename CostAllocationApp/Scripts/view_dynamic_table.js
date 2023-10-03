$(document).ready(function () {
    html = $('#total_menu_setting_items_tbl').html();
    //get table lsit and show it to the dropdown
    GetDynamicTables();
    //GetDynamicTablesForSetting();

    function GetDynamicTables() {
        $.getJSON('/api/Utilities/GetDynamicTables')
            .done(function (data) {           
                $('#table_list').empty();
                $('#table_list').append(`<option value=''>クリックして集計表を選択</option>`);
                $.each(data, function (key, item) {
                    $('#table_list').append(`<option value='${item.Id}'>${item.TableName}</option>`);
                });
            });
    }    

    //show table details
    $(document).on('click', '#search_dynamic_table ', function () {
        //ajax call here 
        //appen the table body with dynamic value
        // $('#total_menu_list_tbody').empty();
        // $.each(data, function (key, item) {
        //     $('#total_menu_list_tbody').append(`<tr><td>${item.TableName}</td><td>${item.TableTitle}</td><<td>${item.TablePosition}</td><td><label id="dynamic_table_delete"><a id="dynamic_table_delete_link" href="javascript:void(0);" data-toggle="modal" data-target="#delete_dynamic_table" onClick="DeleteDynmaicTalbe(${item.Id})">削除</a></label><label id="dynamic_table_edit_label"><a id="dynamic_table_edit_link" href="javascript:void(0);" data-toggle="modal" data-target="#edit_dynamic_table_modal" onClick="GetDynamicTalbeById(${item.Id})">編集</a></label></td></tr>`);                
        // });
        //var dada = "";
        var listItemBody  = GetTotalMenuListHtml();
        $('#total_menu_list_tbody').empty();                
        $('#total_menu_list_tbody').append(`${listItemBody}`);     
        $(".total_menu_list_tbl").show();         
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

    //create total menu dynamic list
    function GetTotalMenuListHtml(){
        var startTR = "";
        var endTR = "";
        var checkItem = "";
        var mainItem = "";
        var subItem = "";
        var detailItem = "";
        var methodList = "";
        var dataForList = "";
        var totalListItem = "";
        
        startTR = "<tr data-count='1'>";
        endTR = "</tr>";        

        checkItem = "<td class='setting_items_td'><input  type='checkbox' value=''></td>";        

        mainItem = "";
        mainItem = mainItem +" <td class='setting_items_td'>";
        mainItem = mainItem +"    <select class='main_item_dropdown'>";
        mainItem = mainItem +"      <option value='0'>設定する大項目を入力</option>";
        mainItem = mainItem +"      <option value='1' selected>main item-1</option>";
        mainItem = mainItem +"      <option value='2'>main item-2</option>";
        mainItem = mainItem +"    </select>";
        mainItem = mainItem +" </td>";                

        subItem = "";
        subItem = subItem +" <td class='setting_items_td'>";
        subItem = subItem +"    <select class='sub_item_dropdown'> ";
        subItem = subItem +"      <option value=''>select sub item</option>";
        subItem = subItem +"      <option value='3'>sub item-1</option>";
        subItem = subItem +"      <option value='4'>sub item-2</option>";
        subItem = subItem +"    </select>";
        subItem = subItem +" </td>";        

        detailItem = "";
        detailItem = detailItem +" <td class='setting_items_td'>";
        detailItem = detailItem +"    <select class='detail_item_dropdown'> ";
        detailItem = detailItem +"      <option value=''>select detail item</option>";
        detailItem = detailItem +"      <option value='5'>detail item-1</option>";
        detailItem = detailItem +"      <option value='6'>detail item-2</option>";
        detailItem = detailItem +"    </select>";
        detailItem = detailItem +" </td>";
        
        methodList = "";
        methodList = methodList +" <td class='setting_items_td'>";
        methodList = methodList +"    <select class='method_dropdown'> ";
        methodList = methodList +"      <option value=''>select method</option>";
        methodList = methodList +"      <option value='7'>method-1</option>";
        methodList = methodList +"      <option value='8'>method-2</option>";
        methodList = methodList +"    </select>";
        methodList = methodList +" </td>";
        
        dataForList = "";
        dataForList = dataForList +" <td class='setting_items_td'>";
        dataForList = dataForList +"    <select class='data_for_dropdown'> ";
        dataForList = dataForList +"      <option value=''>select data</option>";
        dataForList = dataForList +"      <option value='9'>data-1</option>";
        dataForList = dataForList +"      <option value='10'>data-2</option>";
        dataForList = dataForList +"    </select>";
        dataForList = dataForList +" </td>";
        
        totalListItem = ""        
        totalListItem = startTR+""+checkItem+""+mainItem+""+subItem+""+detailItem+""+methodList+""+dataForList+""+endTR;

        return totalListItem;
    }
});

var globalSearchObject = '';
var globalPreviousValue = '0.0';
var globalPreviousId = '';
var jss;
var globalX = 0;
var globalY = 0;
var newRowCount = 1;
var beforeChangedValue = 0;
var jssUpdatedData = [];
var jssInsertedData = [];
var allEmployeeName = [];
var allEmployeeName1 = [];
var cellwiseColorCode = [];
var cellwiseColorCodeForInsert = [];
var changeCount = 0;
var newRowChangeEventFlag = false;
var deletedExistingRowIds = [];

function ClearnAllJexcelData(){
    globalSearchObject = '';
    globalPreviousValue = '0.0';
    globalPreviousId = '';
    jss;
    globalX = 0;
    globalY = 0;
    newRowCount = 1;
    beforeChangedValue = 0;
    jssUpdatedData = [];
    jssInsertedData = [];
    allEmployeeName = [];
    allEmployeeName1 = [];
    cellwiseColorCode = [];
    cellwiseColorCodeForInsert = [];
    changeCount = 0;
    newRowChangeEventFlag = false;
    deletedExistingRowIds = [];
}


//loader functions
function LoaderShow() {    
    $("#loading").css("display", "block");
}
function LoaderHide() {    
    $("#loading").css("display", "none");
}
function LoaderShowJexcel() {
    $("#loading").css("display", "block");
    $("#jspreadsheet").hide();  
    $("#export_budget").hide(); 
}
function LoaderHideJexcel(){
    $("#jspreadsheet").show(); 
    $("#loading").css("display", "none");
}

//shorting column functions
function ColumnOrder(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_p_asc').css('background-color', 'lightsteelblue');
        $('#search_p_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_p_asc').css('background-color', 'grey');
        $('#search_p_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Section(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_section_asc').css('background-color', 'lightsteelblue');
        $('#search_section_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_section_asc').css('background-color', 'grey');
        $('#search_section_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Department(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_department_asc').css('background-color', 'lightsteelblue');
        $('#search_department_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_department_asc').css('background-color', 'grey');
        $('#search_department_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_InCharge(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_incharge_asc').css('background-color', 'lightsteelblue');
        $('#search_incharge_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_incharge_asc').css('background-color', 'grey');
        $('#search_incharge_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Role(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_role_asc').css('background-color', 'lightsteelblue');
        $('#search_role_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_role_asc').css('background-color', 'grey');
        $('#search_role_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Explanation(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_explanation_asc').css('background-color', 'lightsteelblue');
        $('#search_explanation_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_explanation_asc').css('background-color', 'grey');
        $('#search_explanation_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Company(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_company_asc').css('background-color', 'lightsteelblue');
        $('#search_company_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_company_asc').css('background-color', 'grey');
        $('#search_company_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_Grade(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_grade_asc').css('background-color', 'lightsteelblue');
        $('#search_grade_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_grade_asc').css('background-color', 'grey');
        $('#search_grade_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function ColumnOrder_UnitPrice(columnNumber, orderBy) {
    jss.orderBy(columnNumber, orderBy);
    if (orderBy == 0) {
        $('#search_unit_price_asc').css('background-color', 'lightsteelblue');
        $('#search_unit_price_desc').css('background-color', 'grey');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
        jexcelHeadTdEmployeeName.addClass('arrow-up');
    }
    if (orderBy == 1) {
        $('#search_unit_price_asc').css('background-color', 'grey');
        $('#search_unit_price_desc').css('background-color', 'lightsteelblue');
        var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
        jexcelHeadTdEmployeeName.addClass('arrow-down');
    }
}
function onCancel() {
    LoaderShow();
    LoadForecastData()
}

var expanded = false;
$(function () {
    var chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#save_notifications').append(`<li>${name} ${message}</li>`);
    };

});

$(document).ready(function () {

    //checking the data is finalize or not.
    //save and finalize button disable/enable
    $("#budget_years").change(function () {        
        $("#is_finalize_budget").val('');

        var select_year_type = this.value;
        if (select_year_type != '' && select_year_type != null && select_year_type != undefined){
            var arrYear = select_year_type.split("_");
    
            $("#selected_budget_year").val(arrYear[0]);


            $.ajax({
                url: `/api/utilities/CheckYearIfFinalize/`,
                contentType: 'application/json',
                type: 'GET',
                async: true,
                dataType: 'json',
                data: "select_year_type=" + arrYear[0]+"&budgetReqType="+arrYear[1],
                success: function (data) {
                    if(data){
                        $("#is_finalize_budget").val(1);
                        $("#save_bedget").prop("disabled",true);
                        $("#budget_finalize").prop("disabled",true);
                    }
                    else{
                        $("#is_finalize_budget").val(0);
                        $("#save_bedget").prop("disabled",false);
                        $("#budget_finalize").prop("disabled",false);
                    }
                }
            });
        }       
    });
 
    //import budget selction menu
    $('#select_import_year').on('change', function() {
        var selectedBudgetYear = this.value;
    
        if (selectedBudgetYear != '' && selectedBudgetYear != null || selectedBudgetYear != undefined) {
            //get budget initial and 2nd half data if exists
            $.ajax({
                url: `/api/utilities/CheckBudgetWithYear/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: "BudgetYear=" + selectedBudgetYear,
                success: function (data) {
                    $('#select_budget_type').empty();
                                                   
                    $('#select_budget_type').append(`<option value="">select type</option>`);
                    //create fist half budget dropdown                    
                    if(data.FirstHalfFinalize){
                        $('#select_budget_type').append(`<option value="1" disabled style='color:red;'>${selectedBudgetYear} Initial Budget Created</option>`);
                    }else if(data.FirstHalfBudget){
                        $('#select_budget_type').append(`<option value="1" disabled style='color:orange;'>${selectedBudgetYear} Initial Budget Created But Not Finalize</option>`);
                    }else if(!data.FirstHalfBudget){
                        $('#select_budget_type').append(`<option value="1">${selectedBudgetYear} Initial Budget</option>`);
                    }  

                    //create second half budget dropdown
                    if(data.SecondHalfFinalize){
                        $('#select_budget_type').append(`<option value="2" disabled style='color:red;'>${selectedBudgetYear} 2nd Half Budget Created</option>`);
                    }else if(data.SecondHalfBudget){
                        $('#select_budget_type').append(`<option value="2" disabled style='color:orange;'>${selectedBudgetYear} 2nd Half Budget Created But Not Finalize</option>`);
                    }else if(!data.SecondHalfBudget){
                        if(data.FirstHalfFinalize){
                            $('#select_budget_type').append(`<option value="2">${selectedBudgetYear} 2nd Half Budget</option>`);
                        }else{
                            $('#select_budget_type').append(`<option value="2" disabled style='color:gray;'>${selectedBudgetYear} 2nd Half Budget</option>`);
                        }                        
                    }  
                }
            });
        }    
    });

    //duplicate budget selction menu
    $('#duplciateYear').on('change', function() {
        var selectedBudgetYear = this.value;
    
        if (selectedBudgetYear != '' && selectedBudgetYear != null || selectedBudgetYear != undefined) {
            //get budget initial and 2nd half data if exists
            $.ajax({
                url: `/api/utilities/CheckBudgetWithYear/`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                data: "BudgetYear=" + selectedBudgetYear,
                success: function (data) {
                    $('#select_duplicate_budget_type').empty();
                                                   
                    $('#select_duplicate_budget_type').append(`<option value="">select type</option>`);
                    //create fist half budget dropdown                    
                    if(data.FirstHalfFinalize){
                        $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:red;'>${selectedBudgetYear} Initial Budget Created</option>`);
                    }else if(data.FirstHalfBudget){
                        $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:orange;'>${selectedBudgetYear} Initial Budget Created But Not Finalize</option>`);
                    }else if(!data.FirstHalfBudget){
                        $('#select_duplicate_budget_type').append(`<option value="1">${selectedBudgetYear} Initial Budget</option>`);
                    }  

                    //create second half budget dropdown
                    if(data.SecondHalfFinalize){
                        $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:red;'>${selectedBudgetYear} 2nd Half Budget Created</option>`);
                    }else if(data.SecondHalfBudget){
                        $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:orange;'>${selectedBudgetYear} 2nd Half Budget Created But Not Finalize</option>`);
                    }else if(!data.SecondHalfBudget){
                        if(data.FirstHalfFinalize){
                            $('#select_duplicate_budget_type').append(`<option value="2">${selectedBudgetYear} 2nd Half Budget</option>`);
                        }else{
                            $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:gray;'>${selectedBudgetYear} 2nd Half Budget</option>`);
                        }                        
                    }  
                }
            });
        }    
    });

    //From date on change
    $('#duplicate_from').on('change', function() {
        var selectedBudgetYear = this.value;
        if (selectedBudgetYear != '' && selectedBudgetYear != null && selectedBudgetYear != undefined) {
            $("#duplciateYear").prop('disabled', false);
            $('#select_duplicate_budget_type').empty();
        }        
        else{
            $("#duplciateYear").prop('disabled', true);
            $('#select_duplicate_budget_type').empty();
        }
    });

    GetAllBudgetYear();
    GetAllFinalizeYear();

    var year = $('#hidForecastYear').val();
    $("#jspreadsheet").hide();
    $("#export_budget").hide();    
    var count = 1;

    $('#employee_list').select2();       

    //save budget edit data.
    $('#save_bedget').on('click', function () {
        $("#jspreadsheet").hide();    
        LoaderShow();
        var storeMessage = [];
        var _duplicateFlag = false;
        var _employeeIds = [];
        var _uniqueEmployeeIds=[];
        var employeeCount = 0;
        var rowCount = 0;
        
        //get user information
        $.ajax({
            url: `/api/utilities/GetUserLogs/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                employeeCount = data.length;
            }
        });

        if (jssInsertedData.length > 0) {
            for (var i = 0; i < jssInsertedData.length; i++) {
                if (jssInsertedData[i].sectionId == '' || jssInsertedData[i].departmentId == '' || jssInsertedData[i].companyId == '' || (jssInsertedData[i].unitPrice == 0 || isNaN(jssInsertedData[i].unitPrice))) {
                    storeMessage.push('invalid input for ' + jssInsertedData[i].employeeName);
                }
            }
        }

        if (jssUpdatedData.length > 0) {
            for (var i = 0; i < jssUpdatedData.length; i++) {
                if (jssUpdatedData[i].sectionId == '' || jssUpdatedData[i].departmentId == '' || jssUpdatedData[i].companyId == '' || (jssUpdatedData[i].unitPrice == 0 || isNaN(jssUpdatedData[i].unitPrice))) {
                    storeMessage.push('invalid input for ' + jssUpdatedData[i].employeeName);
                }
            }
        }

        if (storeMessage.length > 0) {
            let displayMessage = '';
            $.each(storeMessage, (index, value) => {
                displayMessage += value + '\n';
            });
            alert(displayMessage);
            return false;
        }
        
        var allTableData = jss.getData();

        for (var i = 0; i < jssInsertedData.length; i++) {
            _employeeIds.push(jssInsertedData[i].employeeId);
        }

        for (var i = 0; i < jssUpdatedData.length; i++) {
            _employeeIds.push(jssUpdatedData[i].employeeId);
        }

        if (_employeeIds.length > 0) {
            _uniqueEmployeeIds = _employeeIds.filter((value, index, array) => {
                return array.indexOf(value) === index;
            });
        }
        
        if (_uniqueEmployeeIds.length > 0) {
            var tempArray = [];
            var tempArrayCopy=[];
            for (var i = 0; i < _uniqueEmployeeIds.length; i++) {
                for (var j = 0; j < allTableData.length; j++) {
                    if (_uniqueEmployeeIds[i].toString() == allTableData[j][37].toString()) {
                        tempArray.push(allTableData[j]);
                    }
                }
            }
            var singleRowDuplicationCount = 0;
            for (var i = 0; i < tempArray.length; i++) {
                
                if (tempArrayCopy.length == 0) {
                    tempArrayCopy.push(tempArray[i]);
                }
                else {
                    let tempArrayCount = tempArrayCopy.length;
                    for (var k = 0; k < tempArrayCount; k++) {
                        singleRowDuplicationCount = 0;

                        //section
                        if (tempArray[i][3] == tempArrayCopy[k][3]) {
                            singleRowDuplicationCount++;
                        }
                        //department
                        if (tempArray[i][4] == tempArrayCopy[k][4]) {
                            singleRowDuplicationCount++;
                        }
                        //in-charge
                        if (tempArray[i][5] == tempArrayCopy[k][5]) {
                            singleRowDuplicationCount++;
                        }
                        //role
                        if (tempArray[i][6] == tempArrayCopy[k][6]) {
                            singleRowDuplicationCount++;
                        }
                        //explanation
                        if (tempArray[i][7] == tempArrayCopy[k][7]) {
                            singleRowDuplicationCount++;
                        }
                        //company
                        if (tempArray[i][8]== tempArrayCopy[k][8]) {
                            singleRowDuplicationCount++;
                        }
                        //grade
                        if (tempArray[i][9] == tempArrayCopy[k][9]) {
                            singleRowDuplicationCount++;
                        }
                        //unit price
                        if (tempArray[i][10] == tempArrayCopy[k][10]) {
                            singleRowDuplicationCount++;
                        }                        
                        //employee id
                        if (tempArray[i][37] == tempArrayCopy[k][37]) {
                            singleRowDuplicationCount++;
                        }

                        if (singleRowDuplicationCount == 9) {
                            alert('duplicate row(s) found for ' + tempArray[i][1]);
                            _duplicateFlag = true;
                            break;
                        }
                        else {
                            tempArrayCopy.push(tempArray[i]);
                        }
                    }
                }
                if (_duplicateFlag == true) {
                    break;
                }
            }

            if (_duplicateFlag == true) {
                return false;
            }
        }
        
        if (jssInsertedData.length > 0) {
            var insertedUniqueEmployeeData_unitPrice = [];
            var insertedUniqueEmployeeData_role = [];
            var insertedUniqueEmployeeData_both = [];

            var lastColumnsData_unitPrice = [];
            var lastColumnsData_role = [];
            var lastColumnsData_both = [];

            var _unitPriceFlag = false;
            var _roleFlag = false;
            var _bothFlag = false;

            var _allData = jss.getData();            
                        
            for (let i = 0; i < jssInsertedData.length; i++) {                
                // checking unit price menu
                if (jssInsertedData[i].rowType.toLowerCase().includes('unit')) {
                    {
                        if (jssInsertedData.length > 0) {
                            for (let a = 0; a < jssInsertedData.length; a++) {
                                if (jssInsertedData[a].rowType != '' || jssInsertedData[a].rowType != undefined) {
                                    if (jssInsertedData[a].rowType.toLowerCase().includes('unit')) {
                                        lastColumnsData_unitPrice.push(jssInsertedData[a].rowType);
                                    }
                                    
                                }
                            }

                            if (lastColumnsData_unitPrice.length > 0) {
                                insertedUniqueEmployeeData_unitPrice = lastColumnsData_unitPrice.filter((value, index, array) => {
                                    return array.indexOf(value) === index;
                                });
                            }

                            if (insertedUniqueEmployeeData_unitPrice.length > 0) {
                                var newUnitPriceList = [];
                                var newUnitPriceListCopy = [];
                                
                                var rowCount = 0;
                                for (let b = 0; b < insertedUniqueEmployeeData_unitPrice.length; b++) {
                                    newUnitPriceList = [];
                                    newUnitPriceListCopy = [];
                                    var splittedString = insertedUniqueEmployeeData_unitPrice[b].split('_');
                                    newUnitPriceList.push(jss.getRowData(parseInt(splittedString[2])));


                                    for (let k = 0; k < _allData.length; k++) {
                                        if (insertedUniqueEmployeeData_unitPrice[b] == _allData[k][45]) {
                                            newUnitPriceList.push(_allData[k]);
                                        }
                                    }

                                    for (let l = 0; l < newUnitPriceList.length; l++) {
                                        if (newUnitPriceListCopy.length == 0) {
                                            newUnitPriceListCopy.push(newUnitPriceList[l]);
                                        }
                                        else {
                                            let tempArrayCount = newUnitPriceListCopy.length;
                                            for (let m = 0; m < tempArrayCount; m++) {
                                                rowCount = 0;

                                                //oct point
                                                if (parseFloat(newUnitPriceList[l][11]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][11]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }

                                                //nov point
                                                if (parseFloat(newUnitPriceList[l][12]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][12]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //dec point
                                                if (parseFloat(newUnitPriceList[l][13]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][13]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jan point
                                                if (parseFloat(newUnitPriceList[l][14]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][14]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //feb point
                                                if (parseFloat(newUnitPriceList[l][15]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][15]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //mar point
                                                if (parseFloat(newUnitPriceList[l][16]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][16]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //apr point
                                                if (parseFloat(newUnitPriceList[l][17]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][17]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //may point
                                                if (parseFloat(newUnitPriceList[l][18]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][18]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jun point
                                                if (parseFloat(newUnitPriceList[l][19]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][19]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jul point
                                                if (parseFloat(newUnitPriceList[l][20]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][20]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //aug point
                                                if (parseFloat(newUnitPriceList[l][21]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][21]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //sep point
                                                if (parseFloat(newUnitPriceList[l][22]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][22]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }

                                                newUnitPriceListCopy.push(newUnitPriceList[l]);
                                            }
                                        }
                                    }
                                    if (_unitPriceFlag == true) {
                                        break;
                                    }

                                }// main loop
                            }
                            if (_unitPriceFlag == true) {
                                return false;
                            }
                        }
                    }
                    continue;
                }

                // checking role menu
                if (jssInsertedData[i].rowType.toLowerCase().includes('role')) {
                    {
                        if (jssInsertedData.length > 0) {
                            for (let a = 0; a < jssInsertedData.length; a++) {
                                if (jssInsertedData[a].rowType != '' || jssInsertedData[a].rowType != undefined) {
                                    if (jssInsertedData[a].rowType.toLowerCase().includes('role')){
                                        lastColumnsData_role.push(jssInsertedData[a].rowType);
                                    }
                                    
                                }
                            } 

                            if (lastColumnsData_role.length > 0) {
                                insertedUniqueEmployeeData_role = lastColumnsData_role.filter((value, index, array) => {
                                    return array.indexOf(value) === index;
                                });
                            }

                            if (insertedUniqueEmployeeData_role.length > 0) {
                                var newRoleList = [];
                                var newRoleListCopy = [];
                                var rowCount = 0;
                                for (let b = 0; b < insertedUniqueEmployeeData_role.length; b++) {
                                    newRoleList = [];
                                    newRoleListCopy = [];
                                    var splittedString = insertedUniqueEmployeeData_role[b].split('_');
                                    newRoleList.push(jss.getRowData(parseInt(splittedString[2])));


                                    for (let k = 0; k < _allData.length; k++) {
                                        if (insertedUniqueEmployeeData_role[b] == _allData[k][45]) {
                                            newRoleList.push(_allData[k]);
                                        }
                                    }

                                    for (let l = 0; l < newRoleList.length; l++) {
                                        if (newRoleListCopy.length == 0) {
                                            newRoleListCopy.push(newRoleList[l]);
                                        }
                                        else {
                                            let tempArrayCount = newRoleListCopy.length;
                                            for (let m = 0; m < tempArrayCount; m++) {
                                                let rowCountRole = 0;

                                                //role column
                                                if (newRoleList[l][3] == newRoleListCopy[m][3]) {
                                                    rowCountRole++;
                                                }
                                                //role column
                                                if (newRoleList[l][4] == newRoleListCopy[m][4]) {
                                                    rowCountRole++;
                                                }
                                                //role column
                                                if (newRoleList[l][5] == newRoleListCopy[m][5]) {
                                                    rowCountRole++;
                                                }
                                                //role column
                                                if (newRoleList[l][6] == newRoleListCopy[m][6]) {
                                                    rowCountRole++;
                                                }

                                                if (rowCountRole==4) {
                                                    _roleFlag = true;
                                                    alert('duplicate (role) row(s) found for ' + newRoleList[l][1]);
                                                    break;
                                                }

                                                newRoleListCopy.push(newRoleList[l]);
                                            }
                                        }
                                    }
                                    if (_roleFlag == true) {
                                        break;
                                    }

                                }// main loop
                            }
                            if (_roleFlag == true) {
                                return false;
                            }
                        }
                    }
                    continue;
                }

                // checking both menu
                if (jssInsertedData[i].rowType.toLowerCase().includes('both')) {
                    {
                        if (jssInsertedData.length > 0) {
                            for (let a = 0; a < jssInsertedData.length; a++) {
                                if (jssInsertedData[a].rowType != '' || jssInsertedData[a].rowType != undefined) {
                                    if (jssInsertedData[a].rowType.toLowerCase().includes('both')) {
                                        lastColumnsData_both.push(jssInsertedData[a].rowType);
                                    }
                                    
                                }
                            }

                            if (lastColumnsData_both.length > 0) {
                                insertedUniqueEmployeeData_both = lastColumnsData_both.filter((value, index, array) => {
                                    return array.indexOf(value) === index;
                                });
                            }

                            if (insertedUniqueEmployeeData_both.length > 0) {
                                var newBothList = [];
                                var newBothListCopy = [];

                                var rowCount = 0;
                                for (let b = 0; b < insertedUniqueEmployeeData_both.length; b++) {
                                    newBothList = [];
                                    newBothListCopy = [];
                                    var splittedString = insertedUniqueEmployeeData_both[b].split('_');
                                    newBothList.push(jss.getRowData(parseInt(splittedString[2])));


                                    for (let k = 0; k < _allData.length; k++) {
                                        if (insertedUniqueEmployeeData_both[b] == _allData[k][45]) {
                                            newBothList.push(_allData[k]);
                                        }
                                    }

                                    for (let l = 0; l < newBothList.length; l++) {
                                        if (newBothListCopy.length == 0) {
                                            newBothListCopy.push(newBothList[l]);
                                        }
                                        else {
                                            let tempArrayCount = newBothListCopy.length;
                                            for (let m = 0; m < tempArrayCount; m++) {
                                                bothRowCount = 0;
                                                //section column
                                                if (newBothList[l][3] == newBothListCopy[m][3]) {
                                                    bothRowCount++;
                                                }
                                                //department column
                                                if (newBothList[l][4] == newBothListCopy[m][4]) {
                                                    bothRowCount++;
                                                }
                                                //in-charge column
                                                if (newBothList[l][5] == newBothListCopy[m][5]) {
                                                    bothRowCount++;
                                                }
                                                //role column
                                                if (newBothList[l][6] == newBothListCopy[m][6]) {
                                                    bothRowCount++;
                                                }
                                                if (bothRowCount == 4) {
                                                    _bothFlag = true;
                                                    let _countNumber = 0;
                                                    alert('duplicate (unitprice/role) row(s) found for ' + newBothList[l][1]);
                                                    for (var o = 0; o < _allData.length; o++) {
                                                        if (_allData[o][1] == newBothList[l][1]) {
                                                            break;
                                                        }
                                                        _countNumber++;
                                                    }
                                                    //jss.setStyle(`B${_countNumber + 1}`, "background-color", "red");
                                                    //jss.setStyle(`B${_countNumber + 1}`, "color", "black");
                                                    break;
                                                }
                                                // check unitprice
                                                if (newBothList[l][10] == newBothListCopy[m][10]) {
                                                    rowCount++;
                                                    _bothFlag = true;
                                                    let _countNumber = 0;
                                                    alert('duplicate (unitprice/role) row(s) found for ' + newBothList[l][1]);
                                                    for (var o = 0; o < _allData.length; o++) {
                                                        if (_allData[o][1] == newBothList[l][1]) {
                                                            break;
                                                        }
                                                        _countNumber++;
                                                    }
                                                    //jss.setStyle(`B${_countNumber + 1}`, "background-color", "red");
                                                    //jss.setStyle(`B${_countNumber + 1}`, "color", "black");
                                                    break;
                                                }                                                

                                                newBothListCopy.push(newBothList[l]);
                                            }
                                        }
                                    }
                                    if (_bothFlag == true) {
                                        break;
                                    }

                                }// main loop
                            }

                            if (_bothFlag == true) {
                                return false;
                            }
                        }
                    }
                    continue;
                }
            }

        }

        if (jssInsertedData.length > 0 || jssUpdatedData.length > 0 || deletedExistingRowIds.length > 0) {
            $("#save_modal_header").html("年度データー(Emp. Assignments)");
            $("#back_button_show").css("display", "block");
            $("#save_btn_modal").css("display", "block");
            $("#close_save_modal").css("display", "none");
        } else {
            $("#update_forecast").modal("show");
            $("#save_modal_header").html("変更されていないので、保存できません");
            $("#back_button_show").css("display", "none");
            $("#save_btn_modal").css("display", "none");

            $("#close_save_modal").css("display", "block");
        }

        if (jssUpdatedData.length > 0) {
            $.ajax({
                url: `/api/utilities/GetMatchedRowNumber/`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
                success: function (data) {
                    rowCount = data;
                    $.ajax({
                        url: `/api/utilities/GetMatchedUserNames/`,
                        contentType: 'application/json',
                        type: 'POST',
                        async: false,
                        dataType: 'json',
                        data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
                        success: function (userNames) {
                            $('#user_names').empty();
                            $('#user_names').append(userNames);
                        }
                    });
                    $('#row_count').empty();
                    $('#row_count').append(data);
                }
            });
        }


        if (employeeCount == 1) {
            $('#header_show').css('display', 'none');
            $('#back_button_show').css('display', 'none');
        }
        else {
            if (rowCount == 0) {
                $('#header_show').css('display', 'none');
                $('#back_button_show').css('display', 'none');
            }
            else {
                $('#header_show').css('display', 'block');
                $('#back_button_show').css('display', 'block');
            }
         
        }
        
        
        UpdateBudget();
    });

    //finalize the budget data.
    $('#budget_finalize').on('click', function () {
        $("#save_bedget").prop("disabled",true);
        $("#budget_finalize").prop("disabled",true); 

        var selected_year_for_finalize_budget = $("#budget_years").val();

        if (selected_year_for_finalize_budget == null || selected_year_for_finalize_budget == undefined || selected_year_for_finalize_budget == "") {
            alert("please 年度を選択してください!");
        }
        else{
            $.ajax({
                url: `/api/utilities/FinalizeBudgetAssignment`,
                contentType: 'application/json',
                type: 'GET',
                async: true,
                dataType: 'json',
                data: "year=" + selected_year_for_finalize_budget,
                success: function (data) {        
                    alert("保存されました");              
                          
                }
            });
        }       
    });




    $(document).on('change', '#section_search', function () {
        var sectionId = $(this).val();
        $.getJSON(`/api/utilities/DepartmentsBySection/${sectionId}`)
            .done(function (data) {
                $('#department_search').empty();
                $('#department_search').append(`<option value=''>部署を選択</option>`);
                $.each(data, function (key, item) {
                    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
                });
            });
    });
    
    //show budget data.
    $(document).on('click', '#search_budget ', function () {    
        ClearnAllJexcelData();

        var assignmentYear = $('#budget_years').val();        
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }     
        
        LoaderShowJexcel();
            
        setTimeout(function () {                                
            ShowBedgetResults(assignmentYear);
        }, 3000);
        $("#export_budget").show(); 
    });

    //refresh budget table data 
    $(document).on('click', '#cancele_all_changed_budget ', function () {
        ClearnAllJexcelData();

        var assignmentYear = $('#budget_years').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }

        deletedExistingRowIds = [];
        LoaderShowJexcel();            
        setTimeout(function () {                               
            ShowBedgetResults(assignmentYear);
        }, 3000);
        
    });
    
    $(document).ajaxComplete(function(){
        LoaderHideJexcel();
    });

});

//show the budget results on jexcel table.
function ShowBedgetResults(year) {
    var employeeName = $('#name_search').val();
    employeeName = "";
    var sectionId = $('#section_multi_search').val();
    sectionId = "";
    var inchargeId = $('#incharge_multi_search').val();
    inchargeId = "";
    var roleId = $('#role_multi_search').val();
    roleId = "";
    var companyId = $('#company_multi_search').val();
    companyId = "";
    var departmentId = $('#dept_multi_search').val();
    departmentId = "";
    var explanationId = $('#explanation_multi_search').val();
    explanationId = "";

    if (year == '' || year == undefined) {
        alert('s予算年度を選択');
        return false;
    }

    $('#cancel_forecast').css('display', 'inline-block');
    $('#save_forecast').css('display', 'inline-block');

    var sectionCheck = [];
    var departmentCheck = [];
    var inchargeCheck = [];
    var roleCheck = [];
    var explanationCheck = [];
    var companyCheck = [];

    var data_info = {
        employeeName: employeeName,
        sectionId: sectionId,
        departmentId: departmentId,
        inchargeId: inchargeId,
        roleId: roleId,
        explanationId: explanationId,
        companyId: companyId,
        status: '', year: year, timeStampId: ''
    };
    globalSearchObject = data_info;

    var _retriveddata = [];
    $.ajax({
        url: `/api/utilities/GetAllBudgetData`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            _retriveddata = data;
        }
    });
    
    var sectionsForJexcel = [];
    var departmentsForJexcel = [];
    var inchargesForJexcel = [];
    var rolesForJexcel = [];
    var explanationsForJexcel = [];
    var companiesForJexcel = [];
    var gradesForJexcel = [];

    $.ajax({
        url: `/api/Sections`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                sectionsForJexcel.push({ id: value.Id, name: value.SectionName });
            });
        }
    });
    $.ajax({
        url: `/api/Departments`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                departmentsForJexcel.push({ id: value.Id, name: value.DepartmentName });
            });
        }
    });

    $.ajax({
        url: `/api/InCharges`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                inchargesForJexcel.push({ id: value.Id, name: value.InChargeName });
            });
        }
    });
    $.ajax({
        url: `/api/Roles`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                rolesForJexcel.push({ id: value.Id, name: value.RoleName });
            });
        }
    });
    $.ajax({
        url: `/api/Explanations`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                explanationsForJexcel.push({ id: value.Id, name: value.ExplanationName });
            });
        }
    });
    $.ajax({
        url: `/api/Companies`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                companiesForJexcel.push({ id: value.Id, name: value.CompanyName });
            });
        }
    });
    $.ajax({
        url: `/api/Salaries`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                gradesForJexcel.push({ id: value.Id, name: value.SalaryGrade });
            });
        }
    });
    var _retriveTotal = [];        
    //get total man month
    $.ajax({
        url: `/api/utilities/GetTotalManMonthAndCostForBudgetEdit`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "yearWithType=" + year,
        success: function (data) {
            _retriveTotal = data;         
        }
    });

    
    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }

    var yearHeaderTitleForPoints = "";
    var yearHeaderTitleForCosts = "";
    var arrYear = year.split('_');
    yearHeaderTitleForPoints = "FY"+arrYear[0]+" 見通し";
    yearHeaderTitleForCosts = "FY"+arrYear[0]+" コスト見通し";


    var w = window.innerWidth;
    var h = window.innerHeight;
    
    if (_retriveddata != "" && _retriveddata != null && _retriveddata != undefined) {
        jss = $('#jspreadsheet').jspreadsheet({
            data: _retriveddata,
            filters: true,
            allowComments:true,
            tableOverflow: true,
            freezeColumns: 3,
            //defaultColWidth: 100,
            tableWidth: w-280+ "px",
            tableHeight: (h-150) + "px",           
            minDimensions: [6, 10],
            columnSorting: true,
            oninsertrow: newRowInserted,            

            nestedHeaders:[
                [
                    {
                        title: '',
                        colspan: '10',
                    },
                    //month wise total points
                    {
                        title: _retriveTotal.OctTotalMM,                    
                        type: "decimal",
                        name: "octSumFormula",
                        mask: '#.##,0',
                        decimal: '.'                                       
                    },
                    {
                        title: _retriveTotal.NovTotalMM,                    
                        type: "decimal",
                        name: "=SUM(M3:B3)",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.DecTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.JanTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.FebTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.MarTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.AprTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.MayTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.JunTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.JulTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.AugTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.SepTotalMM,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.TotalManMonth,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    //month wise total cost
                    {
                        title: _retriveTotal.OctTotalCosts,
                        type: "decimal",
                        name: "",
                        mask: '#.##,0',
                        decimal: '.',
                        //width:700
                    },
                    {
                        title: _retriveTotal.NovTotalCosts,
                        type: "decimal",
                        name: "",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.DecTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.JanTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.FebTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.MarTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.AprTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.MayTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.JunTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.JulTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.AugTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                    {
                        title: _retriveTotal.SepTotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },

                    {
                        title: _retriveTotal.TotalCosts,
                        type: "decimal",
                        name: "OctPoints",
                        mask: '#.##,0',
                        decimal: '.'
                    },
                ],
                [
                    {
                        title: '',
                        colspan: '9',
                    },
                    {
                        title: '',
                        colspan: '1',
                    },
                    {
                        title: yearHeaderTitleForPoints,
                        colspan: '12',
                    },
                    
                    {
                        title: '',
                        colspan: '1',
                    },{
                        title: yearHeaderTitleForCosts,
                        colspan: '12',
                    },
                    {
                        title: '',
                        colspan: '1',
                    },
                ],
            ],
            
            columns: [
                { title: "Id", type: 'hidden', name: "Id" },
                { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
                { title: "注記(Remarks)", type: "text", name: "Remarks", width: 60 },
                { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },
                { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },
                { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100 },
                { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60 },
                { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
                { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100 },
                { 
                    title: "グレード(Grade)", 
                    type: "dropdown", 
                    source: gradesForJexcel, 
                    name: "GradeId", 
                    width: 60,
                    filter: (instance, cell, c, r, source) => {
                        
                        let row = parseInt(r);
                        let column = parseInt(c) - 1;
                        
                        var value1 = instance.jexcel.getValueFromCoords(column, row);
                        if (parseInt(value1) != 3) {
                            return [];
                        }
                        else {
                            return gradesForJexcel;
                        }
                    },
                },
                { title: "単価(Unit Price)", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },
                {
                    title: "10月",
                    type: "decimal",
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "11月",
                    type: "decimal",
                    name: "NovPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "12月",
                    type: "decimal",
                    name: "DecPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "1月",
                    type: "decimal",
                    name: "JanPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "2月",
                    type: "decimal",
                    name: "FebPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "3月",
                    type: "decimal",
                    name: "MarPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "4月",
                    type: "decimal",
                    name: "AprPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "5月",
                    type: "decimal",
                    name: "MayPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "6月",
                    type: "decimal",
                    name: "JunPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "7月",
                    type: "decimal",
                    name: "JulPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "8月",
                    type: "decimal",
                    name: "AugPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "9月",
                    type: "decimal",
                    name: "SepPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: "計画工数",
                    type: "decimal",
                    name: "TotalManMonth",
                    mask: '#.##,0',
                    decimal: '.',
                    backgroundColor:"#f46e42",
                    readOnly: true,      
                },
                {
                    title: "10月",
                    type: "number",
                    readOnly: true,
                    mask: "#,##0",
                    name: "OctTotal",
                    width: 60
                },
                {
                    title: "11月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "NovTotal"
                },
                {
                    title: "12月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "DecTotal"
                },
                {
                    title: "1月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JanTotal"
                },
                {
                    title: "2月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "FebTotal"
                },
                {
                    title: "3月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "MarTotal"
                },
                {
                    title: "4月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "AprTotal"
                },
                {
                    title: "5月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "MayTotal"
                },
                {
                    title: "6月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JunTotal"
                },
                {
                    title: "7月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JulTotal"
                },
                {
                    title: "8月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "AugTotal"
                },
                {
                    title: "9月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "SepTotal"
                },
                {
                    title: "実績・見通し",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "TotalCost"
                },
                { title: "Employee Id", type: 'hidden', name: "EmployeeId" },
                { title: "BCYR", type: 'hidden', name: "BCYR" },
                { title: "BCYRCell", type: 'hidden', name: "BCYRCell" },
                { title: "IsActive", type: 'hidden', name: "IsActive" },
                { title: "BCYRApproved", type: 'hidden', name: "BCYRApproved" },
                { title: "BCYRCellApproved", type: 'hidden', name: "BCYRCellApproved" },
                { title: "IsApproved", type: 'hidden', name: "IsApproved" },
                { title: "BCYRCellPending", type: 'hidden', name: "BCYRCellPending" },

                { title: "IsRowPending", type: 'hidden', name: "IsRowPending" },
                { title: "IsDeletePending", type: 'hidden', name: "IsDeletePending" },
                { title: "RowType", type: 'hidden', name: "RowType" }                
            ],
            minDimensions: [6, 10],
            columnSorting: true,
            onbeforechange: function (instance, cell, x, y, value) {

                //alert(value);
                if (x == 11) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 12) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 13) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 14) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 15) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 16) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 17) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 18) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 19) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 20) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 21) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == 22) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
            },
            //onafterchanges: function () {
            //},
            onchange: function (instance, cell, x, y, value) {            
                var checkId = jss.getValueFromCoords(0, y);
                var employeeId = jss.getValueFromCoords(37, y);
                
                if (checkId == null || checkId == '' || checkId == undefined) {
                    //get data for new employee
                    var retrivedData = retrivedObject(jss.getRowData(y));
                    retrivedData.assignmentId = "new-" + newRowCount;

                    jssInsertedData.push(retrivedData);
                    newRowCount++;
                    jss.setValueFromCoords(0, y, retrivedData.assignmentId, false);
                    jss.setValueFromCoords(23, y, `=K${parseInt(y) + 1}*L${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(24, y, `=K${parseInt(y) + 1}*M${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(25, y, `=K${parseInt(y) + 1}*N${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(26, y, `=K${parseInt(y) + 1}*O${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(27, y, `=K${parseInt(y) + 1}*P${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(28, y, `=K${parseInt(y) + 1}*Q${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(29, y, `=K${parseInt(y) + 1}*R${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(30, y, `=K${parseInt(y) + 1}*S${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(31, y, `=K${parseInt(y) + 1}*T${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(32, y, `=K${parseInt(y) + 1}*U${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(33, y, `=K${parseInt(y) + 1}*V${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(34, y, `=K${parseInt(y) + 1}*W${parseInt(y) + 1}`, false);
                }
                else {
                    //get data for existing employee
                    var retrivedData = retrivedObject(jss.getRowData(y));
                    if (retrivedData.assignmentId.toString().includes('new')) {
                        updateArrayForInsert(jssInsertedData, retrivedData, x,y, cell, value, beforeChangedValue);
                    }
                    else {
                        var dataCheck = jssUpdatedData.filter(d => d.assignmentId == retrivedData.assignmentId);                    
                    
                        if (x == 2) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }                        
                            cellwiseColorCode.push(retrivedData.assignmentId+'_'+x);
                        }
                        
                        if (x == 3) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }                    

                        if (x == 4) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }
                        
                        if (x == 5) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }                        
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 6) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 7) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 8) {                    
                            var rowNumber = parseInt(y) + 1;
                            if (parseInt(value) !== 3) {
                                var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                                element[0].cells[10].innerText = '';
                                $(jss.getCell("J" + rowNumber)).addClass('readonly');
                            }
                            else {
                                $(jss.getCell("J" + rowNumber)).removeClass('readonly');                                
                            }

                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x); 
                        }

                        if (x == 9) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 10) {                        
                            if (dataCheck.length == 0) {
                                jssUpdatedData.push(retrivedData);
                            }
                            else {
                                updateArray(jssUpdatedData, retrivedData);
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 11) {                        
                            var octSum = 0;
                            var octPointsSum = 0;
                            var octCostSum = 0;

                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[11] != "" && dataValue[11] != null && dataValue[11] != undefined) {
                                    var octPointPerRow = 0.0;
                                    octPointPerRow = parseFloat(dataValue[11]).toFixed(1);
                                    octPointsSum += parseFloat(octPointPerRow);                            
                                    octCostSum = parseFloat(octCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[11]);     
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    octSum += parseFloat(parseFloat(dataValue[11]));
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[2].innerText= parseFloat(octPointsSum).toFixed(1);

                            octCostSum = new Intl.NumberFormat().format(octCostSum)
                            element[0].cells[15].innerText= octCostSum; 

                            if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
                                octSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {

                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {
                                    updateArray(jssUpdatedData, retrivedData);
                                }

                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }                    

                        if (x == 12) {  
                            var novPointsSum = 0;
                            var novCostSum = 0;

                            var novSum = 0;

                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[12] != "" && dataValue[12] != null && dataValue[12] != undefined) {
                                    var novPointPerRow = 0.0;
                                    novPointPerRow = parseFloat(dataValue[12]).toFixed(1);
                                    novPointsSum += parseFloat(novPointPerRow);   
    
                                    novCostSum = parseFloat(novCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[12]);   
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    novSum += parseFloat(dataValue[12]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[3].innerText= novPointsSum.toFixed(1);
                            novCostSum = new Intl.NumberFormat().format(novCostSum)
                            element[0].cells[16].innerText= novCostSum;

                            if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
                                novSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 13) {
                            var decPointsSum = 0;
                            var decCostSum = 0;

                            var decSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[13] != "" && dataValue[13] != null && dataValue[13] != undefined){
                                    var decPointPerRow = 0.0;
                                    decPointPerRow = parseFloat(dataValue[13]).toFixed(1);
                                    decPointsSum += parseFloat(decPointPerRow); 
                                    
                                    decCostSum = parseFloat(decCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[13]);   
                                } 

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    decSum += parseFloat(dataValue[13]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[4].innerText= decPointsSum.toFixed(1);
                            decCostSum = new Intl.NumberFormat().format(decCostSum)
                            element[0].cells[17].innerText= decCostSum;  
                            
                            if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
                                decSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 14) {        
                            var janPointsSum = 0;
                            var janCostSum = 0;                

                            var janSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[14] != "" && dataValue[14] != null && dataValue[14] != undefined){
                                    var janPointPerRow = 0.0;
                                    janPointPerRow = parseFloat(dataValue[14]).toFixed(1);
                                    janPointsSum += parseFloat(janPointPerRow); 
    
                                    janCostSum = parseFloat(janCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[14]);   
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    janSum += parseFloat(dataValue[14]);
                                }
                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[5].innerText= janPointsSum.toFixed(1);
                            janCostSum = new Intl.NumberFormat().format(janCostSum)
                            element[0].cells[18].innerText= janCostSum;
                            
                            if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
                                janSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 15) {
                            var febPointsSum = 0;
                            var febCostSum = 0;

                            var febSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[15] != "" && dataValue[15] != null && dataValue[15] != undefined){
                                    var febPointPerRow = 0.0;
                                    febPointPerRow = parseFloat(dataValue[15]).toFixed(1);
                                    febPointsSum += parseFloat(febPointPerRow); 
                                    
                                    febCostSum = parseFloat(febCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[15]);   
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    febSum += parseFloat(dataValue[15]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[6].innerText= febPointsSum.toFixed(1);
                            febCostSum = new Intl.NumberFormat().format(febCostSum)
                            element[0].cells[19].innerText= febCostSum; 
                            
                            if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
                                febSum = 1;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 16) {  
                            var marPointsSum = 0;
                            var marCostSum = 0;

                            var marSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[16] != "" && dataValue[16] != null && dataValue[16] != undefined){
                                    var marPointPerRow = 0.0;
                                    marPointPerRow = parseFloat(dataValue[16]).toFixed(1);
                                    marPointsSum += parseFloat(marPointPerRow); 
                                    
                                    marCostSum = parseFloat(marCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[16]);   
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    marSum += parseFloat(dataValue[16]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[7].innerText= marPointsSum.toFixed(1);
                            marCostSum = new Intl.NumberFormat().format(marCostSum)
                            element[0].cells[20].innerText= marCostSum; 
                            
                            if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
                                marSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 17) {  
                            var aprPointsSum = 0;
                            var aprCostSum = 0;

                            var aprSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[17] != "" && dataValue[17] != null && dataValue[17] != undefined){
                                    var aprPointPerRow = 0.0;
                                    aprPointPerRow = parseFloat(dataValue[17]).toFixed(1);
                                    aprPointsSum += parseFloat(aprPointPerRow); 
                                    
                                    aprCostSum = parseFloat(aprCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[17]);
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    aprSum += parseFloat(dataValue[17]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[8].innerText= aprPointsSum.toFixed(1);
                            aprCostSum = new Intl.NumberFormat().format(aprCostSum)
                            element[0].cells[21].innerText= aprCostSum;
                            
                            if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
                                aprSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 18) {   
                            var mayPointsSum = 0;
                            var mayCostSum = 0;

                            var maySum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[18] != "" && dataValue[18] != null && dataValue[18] != undefined){
                                    var mayPointPerRow = 0.0;
                                    mayPointPerRow = parseFloat(dataValue[18]).toFixed(1);
                                    mayPointsSum += parseFloat(mayPointPerRow); 
    
                                    mayCostSum = parseFloat(mayCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[18]); 
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    maySum += parseFloat(dataValue[18]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[9].innerText= mayPointsSum.toFixed(1);
                            mayCostSum = new Intl.NumberFormat().format(mayCostSum)
                            element[0].cells[22].innerText= mayCostSum;
                            
                            if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 19) {      
                            var junPointsSum = 0;
                            var junCostSum = 0;

                            var junSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[19] != "" && dataValue[19] != null && dataValue[19] != undefined){
                                    var junPointPerRow = 0.0;
                                    junPointPerRow = parseFloat(dataValue[19]).toFixed(1);
                                    junPointsSum += parseFloat(junPointPerRow); 
    
                                    junCostSum = parseFloat(junCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[19]); 
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    junSum += parseFloat(dataValue[19]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[10].innerText= junPointsSum.toFixed(1);
                            junCostSum = new Intl.NumberFormat().format(junCostSum)
                            element[0].cells[23].innerText= junCostSum;     
                            
                            if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
                                junSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 20) {    
                            var julPointsSum = 0;
                            var julCostSum = 0;                    

                            var julSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[20] != "" && dataValue[20] != null && dataValue[20] != undefined){
                                    var julPointPerRow = 0.0;
                                    julPointPerRow = parseFloat(dataValue[20]).toFixed(1);
                                    julPointsSum += parseFloat(julPointPerRow); 
    
                                    julCostSum = parseFloat(julCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[20]); 
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    julSum += parseFloat(dataValue[20]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[11].innerText= julPointsSum.toFixed(1);
                            julCostSum = new Intl.NumberFormat().format(julCostSum)
                            element[0].cells[24].innerText= julCostSum;  
                            
                            if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }
                        
                        if (x == 21) {  
                            var augPointsSum = 0;
                            var augCostSum = 0;                      

                            var augSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[21] != "" && dataValue[21] != null && dataValue[21] != undefined){
                                    var augPointPerRow = 0.0;
                                    augPointPerRow = parseFloat(dataValue[21]).toFixed(1);
                                    augPointsSum += parseFloat(augPointPerRow); 
    
                                    augCostSum = parseFloat(augCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[21]); 
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    augSum += parseFloat(dataValue[21]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[12].innerText= augPointsSum.toFixed(1);
                            augCostSum = new Intl.NumberFormat().format(augCostSum)
                            element[0].cells[25].innerText= augCostSum;
                            
                            if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
                                augSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                        }

                        if (x == 22) {    
                            var sepPointsSum = 0;
                            var sepCostSum = 0;                    

                            var sepSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[22] != "" && dataValue[22] != null && dataValue[22] != undefined){
                                    var sepPointPerRow = 0.0;
                                    sepPointPerRow = parseFloat(dataValue[22]).toFixed(1);
                                    sepPointsSum += parseFloat(sepPointPerRow); 
    
                                    sepCostSum = parseFloat(sepCostSum)+parseFloat(dataValue[10])*parseFloat(dataValue[22]); 
                                }

                                if (dataValue[37].toString() == employeeId.toString() && dataValue[40] == true) {
                                    sepSum += parseFloat(dataValue[22]);
                                }

                            });
                            var element = $(`.jexcel > thead > tr:nth-of-type(1)`);
                            element[0].cells[13].innerText= sepPointsSum.toFixed(1);
                            sepCostSum = new Intl.NumberFormat().format(sepCostSum)
                            element[0].cells[26].innerText= sepCostSum;   
                            
                            if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
                                sepSum = 0;
                                alert('入力値が不正です');
                                jss.setValueFromCoords(x, y, beforeChangedValue, false);
                            }
                            else {
                                if (dataCheck.length == 0) {
                                    jssUpdatedData.push(retrivedData);
                                }
                                else {

                                    updateArray(jssUpdatedData, retrivedData);

                                }
                            }
                            cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);                                                        
                        }                                        
                    }
                }
            },

            contextMenu: function (obj, x, y, e) {
                var items = [];
                var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                   return items;
                }
                
                var is_finalize = $("#is_finalize_budget").val();
                if(is_finalize==1){
                    return items;
                }else{
                    items.push({
                        title: '要員を追加 (Add Emp)',
                        onclick: function () {
                            obj.insertRow(1, parseInt(y));
                            var insertedRowNumber = parseInt(obj.getSelectedRows(true)) + 2;
                            
                            setTimeout(function () {
                                jss.setValueFromCoords(38, (insertedRowNumber - 1), true, false);
        
                                $('#jexcel_add_employee_modal').modal('show');
                                globalY = parseInt(y) + 1;
                                GetEmployeeList();
                            },1000);
                            
                            
                        }
                    });   
    
                    items.push({
                        title: '選択した要員の削除 (delete)',                
                        onclick: function () {     
                            var assignmentIds = [];
                                           
                            var value = obj.getSelectedRows();                    
                            var assignementId = jss.getValueFromCoords(0, y);
                            assignmentIds.push(assignementId);
                            var name = jss.getValueFromCoords(1, y);   
                            
                            if (parseInt(assignementId) > 0) {                       
                               $.ajax({
                                    url: `/api/utilities/DeleteBudgetAssignment`,
                                    contentType: 'application/json',
                                    type: 'GET',
                                    async: true,
                                    dataType: 'json',
                                    data: "assignementId=" + assignementId,
                                    success: function (data) {   
                                        if(data==1){                      
                                            jss.deleteRow(parseInt(y),1);                                    
                                            alert("Operation Completed!");
    
                                        }else{
                                            alert("Operation Failed!");
                                        }
                                    }
                                });
                            }else{
                                alert(name +" has not been saved yet. You can not delete this employee!")  
                            }                         
                        }
                    });
                }

                
    
                return items;
            }
        });
    }else{
        $('#jspreadsheet').html("No Budget assign for this year!");
    }

    $("#save_bedget").css("display", "block");
    $("#cancele_all_changed_budget").css("display", "block");
    $("#budget_finalize").css("display", "block");

    //create a row for search in each column
    jss.deleteColumn(48, 19);
    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)');
    jexcelHeadTdEmployeeName.addClass('arrow-down');
    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(3) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelFirstHeaderRow.css('top', '0px');
    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(4) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelSecondHeaderRow.css('top', '20px');

    //employee name column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)').on('click', function () {       
        $('.search_p').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_p').fadeIn("slow");
    });

    //section column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(5)').on('click', function () {               
        $('.search_section').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_section').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(6)').on('click', function () {               
        $('.search_department').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_department').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)').on('click', function () {               
        $('.search_incharge').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_incharge').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)').on('click', function () {               
        $('.search_role').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_role').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)').on('click', function () {               
        $('.search_explanation').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_explanation').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)').on('click', function () {               
        $('.search_company').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_company').fadeIn("slow");
    });
    //grade column sorting
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)').on('click', function () {               
        $('.search_grade').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_grade').fadeIn("slow");
    });
        //unit price column sorting
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)').on('click', function () {               
        $('.search_unit_price').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_unit_price').fadeIn("slow");
    });   
}

$("#hider").hide();
$(".search_p").hide();
$(".search_section").hide();
$(".search_department").hide();
$(".search_incharge").hide();
$(".search_role").hide();
$(".search_explanation").hide();
$(".search_company").hide();
$(".search_grade").hide();
$(".search_unit_price").hide();

$("#buttonClose,#buttonClose_section,#buttonClose_department,#buttonClose_incharge,#buttonClose_role,#buttonClose_explanation,#buttonClose_company,#buttonClose_grade,#buttonClose_unit_price").click(function () {

    $("#hider").fadeOut("slow");
    $('.search_p').fadeOut("slow");
    $('.search_section').fadeOut("slow");
    $('.search_department').fadeOut("slow");
    $('.search_incharge').fadeOut("slow");
    $('.search_role').fadeOut("slow");
    $('.search_explanation').fadeOut("slow");
    $('.search_company').fadeOut("slow");
    $('.search_grade').fadeOut("slow");
    $('.search_unit_price').fadeOut("slow");
});

var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    //jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');

}

//update the arry with the assignment data for cells 
function updateArray(array, retrivedData) {
    var index = jssUpdatedData.findIndex(d => d.assignmentId == retrivedData.assignmentId);

    array[index].employeeId = retrivedData.employeeId;
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    array[index].explanationId = retrivedData.explanationId;
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].octPoint = retrivedData.octPoint;
    array[index].novPoint = retrivedData.novPoint;
    array[index].decPoint = retrivedData.decPoint;
    array[index].janPoint = retrivedData.janPoint;
    array[index].febPoint = retrivedData.febPoint;
    array[index].marPoint = retrivedData.marPoint;
    array[index].aprPoint = retrivedData.aprPoint;
    array[index].mayPoint = retrivedData.mayPoint;
    array[index].junPoint = retrivedData.junPoint;
    array[index].julPoint = retrivedData.julPoint;
    array[index].augPoint = retrivedData.augPoint;
    array[index].sepPoint = retrivedData.sepPoint;
    array[index].year = retrivedData.year;
}

//update the array for add new employee 
function updateArrayForInsert(array, retrivedData, x,y, cell, value, beforeChangedValue) {
    var index = array.findIndex(d => d.assignmentId == retrivedData.assignmentId);
    array[index].assignmentId = retrivedData.assignmentId;
    array[index].employeeId = retrivedData.employeeId;
    array[index].employeeName = retrivedData.employeeName;
    
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].rowType = retrivedData.rowType;
    if (x == 2) {
        array[index].remarks = retrivedData.remarks;
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }
    }
    if (x == 7) {
        array[index].explanationId = retrivedData.explanationId;
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }
    }
    if (x == 11) {
        var octSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                octSum += parseFloat(dataValue[11]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
            octSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);

        }
        else {
            array[index].octPoint = retrivedData.octPoint;
        }

        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39,y);
            currentValue += ',new-x_'+x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }


    }

    if (x == 12) {
        var novSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                novSum += parseFloat(dataValue[12]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
            novSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].novPoint = retrivedData.novPoint;
        }

        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }


    }
    if (x == 13) {
        var decSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                decSum += parseFloat(dataValue[13]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
            decSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].decPoint = retrivedData.decPoint;
        }
        
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 14) {
        var janSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                janSum += parseFloat(dataValue[14]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
            janSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].janPoint = retrivedData.janPoint;
        }
        
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 15) {
        var febSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                febSum += parseFloat(dataValue[15]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
            febSum = 1;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].febPoint = retrivedData.febPoint;
        }
        
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    
    if (x == 16) {
        var marSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                marSum += parseFloat(dataValue[16]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
            marSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].marPoint = retrivedData.marPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 17) {
        var aprSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                aprSum += parseFloat(dataValue[17]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
            aprSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].aprPoint = retrivedData.aprPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 18) {
        var maySum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                maySum += parseFloat(dataValue[18]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].mayPoint = retrivedData.mayPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 19) {
        var junSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                junSum += parseFloat(dataValue[19]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
            junSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].junPoint = retrivedData.junPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 20) {
        var julSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                julSum += parseFloat(dataValue[20]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].julPoint = retrivedData.julPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 21) {
        var augSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                augSum += parseFloat(dataValue[21]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
            augSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].augPoint = retrivedData.augPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 22) {
        var sepSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                sepSum += parseFloat(dataValue[22]);
            }

        });
        if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
            sepSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].sepPoint = retrivedData.sepPoint;
        }
        
        if (!newRowChangeEventFlag) {            
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
        
    array[index].year = retrivedData.year;
    array[index].bcyr= retrivedData.bcyr;   
    
    if(array[index].bCYRCell.length <= retrivedData.bCYRCell.length){
        array[index].bCYRCell= retrivedData.bCYRCell;  
    }            
}

//set value and get value from the objects
function retrivedObject(rowData) {
    return {
        assignmentId: rowData[0],
        employeeName: rowData[1],
        remarks: rowData[2],
        employeeId: rowData[37],
        sectionId: rowData[3],
        departmentId: rowData[4],
        inchargeId: rowData[5],
        roleId: rowData[6],
        explanationId: rowData[7],
        companyId: rowData[8],
        gradeId: rowData[9],
        unitPrice: parseFloat(rowData[10]),
        octPoint: parseFloat(rowData[11]),
        novPoint: parseFloat(rowData[12]),
        decPoint: parseFloat(rowData[13]),
        janPoint: parseFloat(rowData[14]),
        febPoint: parseFloat(rowData[15]),
        marPoint: parseFloat(rowData[16]),
        aprPoint: parseFloat(rowData[17]),
        mayPoint: parseFloat(rowData[18]),
        junPoint: parseFloat(rowData[19]),
        julPoint: parseFloat(rowData[20]),
        augPoint: parseFloat(rowData[21]),
        sepPoint: parseFloat(rowData[22]),
        year: document.getElementById('selected_budget_year').value,

        bcyr: rowData[38],
        bCYRCell: rowData[39],
        isActive: rowData[40],
        bCYRApproved: rowData[41],
        bCYRCellApproved: rowData[42],
        isApproved: rowData[43],
        bCYRCellPending: rowData[44],
        isRowPending: rowData[45],
        isDeletePending: rowData[46],
        rowType: rowData[47],
    };
}

function DeleteRecords() {
    $.getJSON(`/api/utilities/DeleteAssignments/`)
        .done(function (data) {       
        });
}


//employee insert
function InsertEmployee() {
    var apiurl = "/api/utilities/CreateEmployee/";
    let employeeName = $("#employee_name").val();
    if (employeeName == "" || employeeName == null || employeeName == undefined) {
        $(".employee_err").show();
        return false;
    } else {
        $(".employee_err").hide();
        var data = {
            FullName: employeeName.trim()
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result > 0) {
                    jss.setValueFromCoords(1, globalY, data.FullName, false);
                    console.log("result: "+result);
                    console.log("globalY: "+globalY);

                    jss.setValueFromCoords(37, globalY, result, false);
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('データが保存されました!');
                    $('#employee_name').val('');
                    $('#jexcel_add_employee_modal').modal('hide');
                }                
            },
            error: function (result) {
                alert(result.responseJSON.Message);
            }
        });
    }
}

//Get employee list
function GetEmployeeList() {
    var budget_year = $("#budget_years").val();
    $('#employee_list').empty();    
    $.getJSON(`/api/utilities/EmployeeListBudgetEditFiltered/${budget_year}`)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#employee_list').append(`<option value='${item.Id}'>${item.FullName}</option>`);
            });
        });
}

/***************************\                           
  Add Employee through Modal                      
\***************************/
function AddEmployee() {
    var employeeId = $('#employee_list').val();
    var employeeName = $('#employee_list').find("option:selected").text();
    jss.setValueFromCoords(1, globalY, employeeName, false);
    jss.setValueFromCoords(37, globalY, employeeId, false);
    $('#jexcel_add_employee_modal').modal('hide');
}

function UpdateBudget() {    
    $("#update_forecast").modal("hide");
    $("#jspreadsheet").hide();    
    $("#export_budget").hide(); 
    var userName = '';

    $.ajax({
        url: `/Registration/GetSession/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userName = data;
        }
    });


    var updateMessage = "";
    var insertMessage = "";
    var promptValue = "";
    
    var dateObj = new Date();
    var month = dateObj.getUTCMonth() + 1; //months from 1-12
    var day = dateObj.getDate();
    var year = dateObj.getUTCFullYear();
    var miliSeconds = dateObj.getMilliseconds();
    var timestamp = `${year}${month}${day}${miliSeconds}_`;

    if (jssUpdatedData.length > 0) {           
            updateMessage = "Successfully data updated";
            $.ajax({
                url: `/api/utilities/UpdateBudgetData`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode }),
                success: function (data) {
                    var year = $("#budget_years").val();
                    ShowBedgetResults(year);

                    $("#timeStamp_ForUpdateData").val(data);
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been updated by ', userName);
                    });
                    $("#jspreadsheet").show();
                    $("#export_budget").show();
                }
            });
            jssUpdatedData = [];
        
        
    }
    else {
        $("#jspreadsheet").show();
        $("#export_budget").show();
        updateMessage = "";
    }

    if (jssInsertedData.length > 0) {
        var elementIndex = jssInsertedData.findIndex(object => {
            return object.employeeName.toLowerCase() == 'total';
        });
        if (elementIndex >= 0) {
            jssInsertedData.splice(elementIndex, 1);
        }

        
        var update_timeStampId = $("#timeStamp_ForUpdateData").val();
        var add_employee_budget_year = $("#budget_years").val();

        insertMessage = "Successfully data inserted.";
        $.ajax({
            url: `/api/utilities/ExcelAssignmentBudget/`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: jssInsertedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode, YearWithBudgetType: add_employee_budget_year }),
            success: function (data) {                               
                var year = $("#budget_years").val();
                ShowBedgetResults(year);

                $("#timeStamp_ForUpdateData").val(data);
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been updated by ', userName);
                });
                $("#jspreadsheet").show();
                $("#export_budget").show();               
            }
        });
        jssInsertedData = [];
        newRowCount = 1;

    }

    if (deletedExistingRowIds.length > 0) {
        var year = $("#assignment_year_list").val();
        $.ajax({
            url: `/api/utilities/ExcelDeleteAssignment/`,
            contentType: 'application/json',
            type: 'DELETE',
            async: false,
            dataType: 'json',                
            data: JSON.stringify({ ForecastUpdateHistoryDtos: "", HistoryName: timestamp + promptValue,TimeStampId: update_timeStampId,DeletedRowIds: deletedExistingRowIds,Year:year}),
            success: function (data) {
                var year = $("#budget_years").val();
                ShowBedgetResults(year);

                $("#timeStamp_ForUpdateData").val(data);
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been updated by ', userName);
                });
                $("#jspreadsheet").show();
                $("#export_budget").show();    
            }
        });

        $("#timeStamp_ForUpdateData").val('');
        var chat = $.connection.chatHub;
        $.connection.hub.start();
        // Start the connection.
        $.connection.hub.start().done(function () {
            chat.server.send('data has been deleted by ', userName);
        });
        $("#jspreadsheet").show();
        LoaderHide();
        deletedExistingRowIds = [];
    }

    if (updateMessage == "" && insertMessage == "") {
        $("#header_show").html("");
        $("#update_forecast").modal("show");
        $("#save_modal_header").html("変更されていないので、保存できません");
        $("#back_button_show").css("display", "none");
        $("#save_btn_modal").css("display", "none");

        $("#close_save_modal").css("display", "block");
    }
    else if (updateMessage != "" && insertMessage != "") {
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
    else if (updateMessage != "") {
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
    else if (insertMessage != "") {
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }

}

/*
    author: sudipto.
    get all the forecasted year. and build the year dropdown.
*/
function CompareUpdatedData() {
    if (jssUpdatedData.length > 0) {
        $.ajax({
            url: `/api/utilities/GetMatchedRows`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
            success: function (data) {
                $('#display_matched_rows table tbody').empty();
                $.each(data, function (index, element) {
                    $('#display_matched_rows table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                });
            }
        });
    }
}

/*
    author: sudipto.
    import data from excel file.
*/
function ImportCSVFile() {
    var userName = '';


    $.ajax({
        url: `/Registration/GetSession/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userName = data;
        }
    });

    $.ajax({
        url: `/Forecasts/Index/`,
        contentType: 'application/json',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: JSON.stringify(jssInsertedData),
        success: function (data) {
            var chat = $.connection.chatHub;
            $.connection.hub.start();
            $.connection.hub.start().done(function () {
                chat.server.send('data has been inserted by ', userName);
            });
        }
    });
}

/*
    author: sudipto.
    get all the forecasted year. and build the year dropdown.
*/
function GetAllBudgetYear() {
    $.ajax({
        url: `/api/utilities/GetBudgetYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#budget_years').append(`<option value=''>予算年度を選択</option>`);
            $.each(data, function (index, element) {
                $('#budget_years').append(`<option value='${element.Year}_1'>${element.Year}年初期</option>`);
                if(element.SecondHalfBudget){
                    $('#budget_years').append(`<option value='${element.Year}_2'>${element.Year}年下半期</option>`);
                }else{
                    $('#budget_years').append(`<option value='${element.Year}_2' disabled style='gray;'>${element.Year}年下半期</option>`);
                }
            });
        }
    });
}
/*
    author: sudipto.
    get all finalize year list.
*/
function GetAllFinalizeYear() {
    $.ajax({
        url: `/api/utilities/GetBudgetFinalizeYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            if(data==''){                                
                $("#duplciateYear").prop('disabled', true);
                $('#select_duplicate_budget_type').empty();                
            }else{
                $("#duplciateYear").prop('disabled', true);
                $('#select_duplicate_budget_type').empty();  

                $('#duplicate_from').append(`<option value=''>予算年度を選択</option>`);
                $.each(data, function (index, element) {
                    $('#duplicate_from').append(`<option value='${element.Year}'>${element.Year}</option>`);                
                });
            }            
        }
    });
}


function CheckForecastYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!="" && typeof year != "undefined"){
        $('#select_import_year').val(parseInt(year)+1);
    }
}

/*
    author: sudipto.
    validate replicate data. 
*/
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        $('#replicate_from').val(year);
    }else{
        $('#replicate_from').val('');
    }
}

/*
    author: sudipto.
    replicate the budget data from previous year budget.
*/
function DuplicateBudget(){    
    $("#validation_message").html("");

    var fromDate = $('#duplicate_from').val();
    var toDate  = $('#duplciateYear').val();
    var budgetType  = $('#select_duplicate_budget_type').val();
 
    if (fromDate == null || fromDate == undefined || fromDate == "") {
        alert("Please select from date!")
        return false;
    }
    if (toDate == null || toDate == undefined || toDate == "") {
        alert("Please select to date!")
        return false;
    }
    if (budgetType == null || budgetType == undefined || budgetType == "") {
        alert("Please select to budget type!")
        return false;
    }

    if(fromDate!="" && toDate!=""){
        $("#replicate_from_previous_year").modal("hide");
        $("#loading").css("display", "block");
        $.ajax({
            url: `/api/utilities/DuplicateForecastYear`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: "copyYear=" + fromDate+"&insertYear="+toDate+"&budgetType="+budgetType,
            success: function (data) {                       
                if(parseInt(data)==5){
                    $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Data has already imported to " + toDate + ".Please chooose another year to import data..</span>");                        
                }
                else if(parseInt(data)==6){
                    $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>"+fromDate+" has no data to copy!</span>");                        
                }
                else{
                    $("#validation_message").html("<span id='validation_message_success' style='margin-left: 28px;'>インポートデータは正常に処理されました "+toDate+".</span>");                                           
                }
                LoaderHide();
            }
        });
    }else{
        $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Failed to Replicate the data!</span>");                        
        return false;
    }
}

/*
    author: sudipto.
    budget validation and submit the import file to server.
*/
function validate(){
    var selectedYear = $('#select_import_year').val();
    var import_file = $('#import_file_excel').val();
   
    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("please 年度を選択してください!");
        return false;
    }else if(import_file =="" || typeof import_file === "undefined"){
        alert("please select import file!");
        return false;
    }else { return true; }
}

$('#frm_import_year_data').submit(validate);

/*
    author: sudipto
    after cell change, store that cell information into hidden field.
*/
function CheckIfAlreadyExists(selectedCells,assignmentId){
    var isCellExists = false;    

	if (previousChangedCells != '' && previousChangedCells != null && previousChangedCells != undefined){
        var arrPreviousCells = previousChangedCells.split(",");                                
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");
            if(arrNestedCells[0] == assignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }      
        })        
    } 
    return isCellExists;  
}

function StoreChangeCellData(selectedCells,assignmentId){
	var changedCellStored = "";
    var isCellExists = false;    

	if (previousChangedCells != '' && previousChangedCells != null && previousChangedCells != undefined){

        var arrPreviousCells = previousChangedCells.split(",");                                
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");  
            if(arrNestedCells[0] == assignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }   

            if(changedCellStored==""){
                changedCellStored = arrNestedCells[0]+"_"+arrNestedCells[1];
            }else{
                var arrChangedCellStored = changedCellStored.split(",");
                var isExists =false;

                $.each(arrChangedCellStored, function (nextedIndex, nestedValue2){
                    var arrNestedValue2 = nestedValue2.split("_");
                    if(arrNestedValue2[0] == arrNestedCells[0] && arrNestedValue2[1] == arrNestedCells[1]){
                        isExists = true;
                    }      
                })   
                if(!isExists){
                    changedCellStored = changedCellStored +","+arrNestedCells[0]+"_"+arrNestedCells[1];
                }                
            } 
        })

        if(!isCellExists){
            if(changedCellStored == ""){
                changedCellStored = assignmentId+"_"+selectedCells;     
            }else{
                changedCellStored = changedCellStored +","+assignmentId+"_"+selectedCells;     
            }
        }
    }
    else{
        changedCellStored = assignmentId+"_"+selectedCells;     
    }	
}


//export button clicked for download budget data
$('#export_budget').on('click', function () {    
    ExportApprovalHistory();    
});

//submitting the export form to server side. 
function ExportApprovalHistory(){
    var budgetYearWithType = $('#budget_years').val();

    var arrBudgetYear = budgetYearWithType.split("_");

    $("#hid_budget_year").val(arrBudgetYear[0]);
    $("#hid_budget_type").val(arrBudgetYear[1]);        

    $('#frmExportBudget').submit();
}
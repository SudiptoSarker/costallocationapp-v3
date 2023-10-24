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


var jssTableDefinition = {
    assignmentId: { index: 0, cellName: 'A' },
    employeeName: { index: 1, cellName: 'B' },
    remarks: { index: 2, cellName: 'C' },
    section: { index: 3, cellName: 'D' },
    department: { index: 4, cellName: 'E' },
    incharge: { index: 5, cellName: 'F' },
    role: { index: 6, cellName: 'G' },
    explanation: { index: 7, cellName: 'H' },
    company: { index: 8, cellName: 'I' },
    grade: { index: 9, cellName: 'J' },
    unitPrice: { index: 10, cellName: 'K' },
    dbId: { index: 11, cellName: 'L' },
    duplicateFrom: { index: 12, cellName: 'M' },
    duplicateCount: { index: 13, cellName: 'N' },
    roleChanged: { index: 14, cellName: 'O' },
    unitPriceChanged: { index: 15, cellName: 'P' },
    octM: { index: 16, cellName: 'Q' },
    novM: { index: 17, cellName: 'R' },
    decM: { index: 18, cellName: 'S' },
    janM: { index: 19, cellName: 'T' },
    febM: { index: 20, cellName: 'U' },
    marM: { index: 21, cellName: 'V' },
    aprM: { index: 22, cellName: 'W' },
    mayM: { index: 23, cellName: 'X' },
    junM: { index: 24, cellName: 'Y' },
    julM: { index: 25, cellName: 'Z' },
    augM: { index: 26, cellName: 'AA' },
    sepM: { index: 27, cellName: 'AB' },
    totalM: { index: 28, cellName: 'AC' },
    octT: { index: 29, cellName: 'AD' },
    novT: { index: 30, cellName: 'AE' },
    decT: { index: 31, cellName: 'AF' },
    janT: { index: 32, cellName: 'AG' },
    febT: { index: 33, cellName: 'AH' },
    marT: { index: 34, cellName: 'AI' },
    aprT: { index: 35, cellName: 'AJ' },
    mayT: { index: 36, cellName: 'AK' },
    junT: { index: 37, cellName: 'AL' },
    julT: { index: 38, cellName: 'AM' },
    augT: { index: 39, cellName: 'AN' },
    sepT: { index: 40, cellName: 'AO' },
    totalCost: { index: 41, cellName: 'AP' },
    employeeId: { index: 42, cellName: 'AQ' },
    bcyr: { index: 43, cellName: 'AR' },
    bcyrCell: { index: 44, cellName: 'AS' },
    isActive: { index: 45, cellName: 'AT' },
    bcyrApproved: { index: 46, cellName: 'AU' },
    bcyrCellApproved: { index: 47, cellName: 'AV' },
    isApproved: { index: 48, cellName: 'AW' },
    bcyrCellPending: { index: 49, cellName: 'AX' },
    isRowPending: { index: 50, cellName: 'AY' },
    isDeletePending: { index: 51, cellName: 'AZ' },
    rowType: { index: 52, cellName: 'BA' },

};


function LoaderShow() {
    $("#jspreadsheet").css("display", "none");
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#jspreadsheet").css("display", "block");
    $("#loading").css("display", "none");
}
function LoaderShowJexcel() {
    $("#loading").css("display", "block");
    $("#jspreadsheet").hide();  
    //$("#head_total").css("display", "none");
    
}
function LoaderHideJexcel(){
    $("#jspreadsheet").show();  
    //$("#head_total").css("display", "table !important");
    $("#loading").css("display", "none");
}

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
    GetAllForecastYears();
    var year = $('#hidForecastYear').val();
    if (year.toLowerCase() != "imprt") {
        $("#jspreadsheet").hide();  
    }
    var count = 1;
    $('#employee_list').select2();
    
    
    $('#saved_approved_data').on('click', function () {
        var assignmentYear = $('#assignment_year_list').val();
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }  
        
        // var approvedCells = $("#all_selected_cells").val(); 
        // var approvedRows = $("#all_selected_row_for_approve").val();

        var approvedCells = $("#approved_selected_cells").val(); 
        var approvedRows = $("#approved_selected_rows").val();
        
        if ((approvedCells != null && approvedCells != undefined && approvedCells != "") || (approvedRows != null && approvedRows != undefined && approvedRows != "")){
            var approvePromptValue = prompt("承認履歴ファイル保存名", '');
            //$("#timeStamp_ForUpdateData").val('');
            if (approvePromptValue == null || approvePromptValue == undefined || approvePromptValue == "") {
                return false;
            }else{
                // LoaderShowJexcel();        
                LoaderShow();        
                var dateObj = new Date();
                var month = dateObj.getUTCMonth() + 1; //months from 1-12
                var day = dateObj.getDate();
                var year = dateObj.getUTCFullYear();    
                var miliSeconds= dateObj.getMilliseconds();    
                var timestamp = `${year}${month}${day}${miliSeconds}_`; 
                
                $.ajax({
                    url: `/api/utilities/UpdateApprovedData`,
                    contentType: 'application/json',
                    type: 'GET',
                    async: true,
                    dataType: 'json',
                    data: "assignmentYear=" + assignmentYear+"&historyName="+timestamp+approvePromptValue+"&approvalCellsWithAssignmentId="+approvedCells+"&approvedRows="+approvedRows,
                    success: function (data) {
                        if(data==1){
                            $("#approved_selected_cells").val(''); 
                            $("#approved_selected_rows").val('');
                                  
                            ShowForecastResults(assignmentYear);
                        }else{
                            LoaderHide();
                            alert("There is no approved data to save!")
                        }
                    }
                });  
            }       
        }else{
            alert("承認するデータがありません");
        }        
    });

    //row and cell approve together and multi cell or row approve color at a time.
    $('#approve_forecast_data').on('click', function () {
        //$("#all_selected_row_for_approve").val();
        var selectedRowsWithRowNumber = $("#all_selected_row_with_assignmentId_row_number").val();
        var selectedCellsWithPosition = $("#all_selected_cells_with_cellposition").val();
        var isRowApprovalRequest = true;
        var isCellApprovalRequest = true;

        if(selectedRowsWithRowNumber == "" || selectedRowsWithRowNumber == null || selectedRowsWithRowNumber == undefined){
            isRowApprovalRequest = false;
        }
        if(selectedCellsWithPosition == "" || selectedCellsWithPosition == null || selectedCellsWithPosition == undefined){
            isCellApprovalRequest = false;
        }

        if(!isRowApprovalRequest && !isCellApprovalRequest) {
            alert("承認するデータがありません");
        }else{
            if(isRowApprovalRequest){

                var arrSelectedRowsWithRowNumber = selectedRowsWithRowNumber.split(",");
                $.each(arrSelectedRowsWithRowNumber, function (nextedIndex, nestedValue){
                    var arrNestedValue = nestedValue.split("_");  
                    if(arrNestedValue[2] == "true")  {
                        SetRowColor_AfterApproved(parseInt(arrNestedValue[1])+1);           
                    }else{
                        SetRowColor_ForDeletedRow(parseInt(arrNestedValue[1])+1);           
                    }                
                });
            }
            if(isCellApprovalRequest){
                var arrSelectedCellsWithPosition = selectedCellsWithPosition.split(",");
                $.each(arrSelectedCellsWithPosition, function (nextedIndex, nestedValue){
                    var arrNestedValue = nestedValue.split("_");
                    SetCellWiseColor(arrNestedValue[2]);                                   
                });
            }  
        }    
                
        var approvedCells = $("#all_selected_cells").val(); 
        //$("#approved_selected_cells").val('');
        $("#all_selected_cells").val(''); 
        $("#all_selected_cells_with_cellposition").val('');
        
        var previousSelectedCells = $("#approved_selected_cells").val();
        var allSelectedCells = "";
        if(previousSelectedCells == "" || previousSelectedCells == null || previousSelectedCells == undefined){
            if(approvedCells != "" || approvedCells != null || approvedCells != undefined){
                allSelectedCells = approvedCells
            }
        }else{
            if(approvedCells == "" || approvedCells == null || approvedCells == undefined){
                allSelectedCells = previousSelectedCells;
            }else{
                allSelectedCells = previousSelectedCells +","+approvedCells;
            }            
        }
        $("#approved_selected_cells").val(allSelectedCells);
        
        
        var approvedRows = $("#all_selected_row_for_approve").val();
        //$("#approved_selected_rows").val('');
        $("#all_selected_row_for_approve").val('');
        $("#all_selected_row_with_assignmentId_row_number").val('');        
        
        var previousSelectedRows = $("#approved_selected_rows").val();
        if(previousSelectedRows == "" || previousSelectedRows == null || previousSelectedRows == undefined){
            $("#approved_selected_rows").val(approvedRows);
        }else{
            approvedRows = previousSelectedRows +","+approvedRows;
            $("#approved_selected_rows").val(approvedRows);
        }

    });


    // $('#approve_forecast_data').on('click', function () {
    //     var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);
    //     var sCols = $("#jspreadsheet").jexcel("getSelectedColumns", true);

    //     var approveAssignmentId = $("#hidSelectedRow_AssignementId").val();
    //     var isDeleted = $("#hidIsRowDeleted").val();
    //     if (approveAssignmentId =='' || typeof approveAssignmentId === "undefined"){
    //         $("#hidSelectedRow_AssignementId").val('');
    //         alert("承認するデータがありません");
    //     }else{
    //         // LoaderShow();
    //         //return false;

    //         var data_Info = {
    //             Id: approveAssignmentId
    //         };
    //         var cellPosition = $("#hid_SelectedCellPosition").val();
    //         var selectedCells = $("#hid_cellNo").val();
    //         var isRowSelected = $("#hid_IsRowSelected").val();
            
    //         var strApprovedCellsStore = "";
    //         var strApprovedRowsStore = "";

    //         if(isRowSelected=="yes"){
    //             //approve rows
    //             $.ajax({
    //                 //url: `/api/utilities/ApprovedForecastData`,
    //                 url: `/api/utilities/IsValidForApprovalRow`,
    //                 contentType: 'application/json',
    //                 type: 'GET',
    //                 async: true,
    //                 dataType: 'json',
    //                 data: "assignementId=" + approveAssignmentId+"&isDeletedRow="+isDeleted,
    //                 success: function (data) {
    //                     if(data==1){
    //                         var approvedRows = $("#all_selected_row_for_approve").val();                               
    //                         var isApprovedRowColorChanged = false;

    //                         //store approved data!
    //                         if(approvedRows =="" || typeof approvedRows === 'undefined' || approvedRows === null){
    //                             strApprovedRowsStore = approveAssignmentId;
    //                             $("#all_selected_row_for_approve").val(strApprovedRowsStore);
    //                             isApprovedRowColorChanged = true;
    //                         }else{         
    //                             var isValidForApprove = false;
    //                             var arrCells = approvedRows.split(',');        
    //                             $.each(arrCells, function (nextedIndex, nestedValue) {                                            
    //                                 if(approveAssignmentId == nestedValue){
    //                                     isValidForApprove = false;
    //                                 }
    //                                 else{
    //                                     isValidForApprove = true;
    //                                     isApprovedRowColorChanged = true;
    //                                 }                                                            
    //                             });  
    //                             if(isValidForApprove){
    //                                 strApprovedRowsStore = approvedRows+","+approveAssignmentId;   
    //                                 $("#all_selected_row_for_approve").val(strApprovedRowsStore);
    //                             }else{
    //                                 alert("There is no data to approved!")
    //                             }                                           
    //                         } 

    //                         if(isApprovedRowColorChanged){
    //                            var rowNumber = $("#hidSelectedRowNumber").val();
    //                             if(isDeleted =='true'){
    //                                 SetRowColor_AfterApproved(parseInt(rowNumber)+1);
    //                             }else{
    //                                 SetRowColor_ForDeletedRow(parseInt(rowNumber)+1);
    //                             }
    //                             $("#hidSelectedRow_AssignementId").val("");
    //                             $("#hidIsRowDeleted").val("");                                                                          
    //                         }                            
    //                     }
    //                     else{
    //                         alert("There is no data to approved!")
    //                     }
    //                 }
    //             });       
    //         }else{                
    //             $.ajax({
    //                 url: `/api/utilities/IsValidForApprovalCell`,
    //                 contentType: 'application/json',
    //                 type: 'GET',
    //                 async: true,
    //                 dataType: 'json',
    //                 data: "assignementId=" + approveAssignmentId+"&selectedCells="+selectedCells,
    //                 success: function (data) {
    //                     if(data==1){
    //                         var approvedCells = $("#all_selected_cells").val();                               
    //                         var isApprovedCellColorChanged = false;

    //                         //store approved data!
    //                         if(approvedCells =="" || typeof approvedCells === 'undefined' || approvedCells === null){
    //                             strApprovedCellsStore = approveAssignmentId +"_"+ selectedCells;
    //                             $("#all_selected_cells").val(strApprovedCellsStore);
    //                             isApprovedCellColorChanged = true;
    //                         }else{         
    //                             var isValidForApprove = false;
    //                             var arrCells = approvedCells.split(',');        
    //                             $.each(arrCells, function (nextedIndex, nestedValue) {                                            
    //                                 var arrNestedCells = nestedValue.split('_');
    //                                 if(selectedCells == arrNestedCells[1] && approveAssignmentId == arrNestedCells[0]){
    //                                     isValidForApprove = false;
    //                                 }
    //                                 else{
    //                                     isValidForApprove = true;
    //                                     isApprovedCellColorChanged = true;
    //                                 }                                                            
    //                             });  
    //                             if(isValidForApprove){
    //                                 strApprovedCellsStore = approvedCells+","+approveAssignmentId +"_"+ selectedCells;;   
    //                                 $("#all_selected_cells").val(strApprovedCellsStore);
    //                             }else{
    //                                 alert("There is no data to approved!")
    //                             }                                           
    //                         } 
    //                         if(isApprovedCellColorChanged){
    //                             var assignmentYear = $('#assignment_year_list').val();                            
    //                             var cellNo = $("#selectCellNumber").val();                                                                
    //                             SetCellWiseColor(cellNo);       
    //                         }
    //                     }
    //                     else{
    //                         alert("There is no data to approved!")
    //                     }
    //                 }
    //             });       
    //         }            
    //     }       
    // });

    $('#unapprove_forecast_data').on('click', function () {       
        //LoaderShow();  
        var assignmentYear = $('#assignment_year_list').val();
        ///ShowForecastResults(assignmentYear);

        LoaderShowJexcel();
            
        setTimeout(function () {                                 
            ShowForecastResults(assignmentYear);
        }, 3000);
        $("#approved_selected_cells").val(''); 
        $("#all_selected_cells").val('');    
        $("#all_selected_cells_with_cellposition").val('');    

        $("#approved_selected_rows").val(''); 
        $("#all_selected_row_for_approve").val('');    
        $("#all_selected_row_with_assignmentId_row_number").val('');

        // var approveAssignmentId = $("#hidSelectedRow_AssignementId").val();
        // var isDeleted = $("#hidIsRowDeleted").val();
        // if (approveAssignmentId =='' || typeof approveAssignmentId === "undefined"){
        //     $("#hidSelectedRow_AssignementId").val('');
        //     alert("承認するデータがありません");
        // }else{
        //     // LoaderShow();
        //     //return false;

        //     var data_Info = {
        //         Id: approveAssignmentId
        //     };
        //     var cellPosition = $("#hid_SelectedCellPosition").val();
        //     var selectedCells = $("#hid_cellNo").val();
        //     var isRowSelected = $("#hid_IsRowSelected").val();
            
            
        //     if(isRowSelected=="yes"){
        //         //un approve rows
        //         var approvedCells = $("#all_selected_row_for_approve").val();   
        //         var arrCells = approvedCells.split(',');        

        //         var restoreApproveCells = "";
        //         var isValidForUnapprove = true;

        //         $.each(arrCells, function (nextedIndex, nestedValue) {                                            
        //             //var arrNestedCells = nestedValue.split('_');
        //             if(approveAssignmentId == nestedValue){                        
        //                 isValidForUnapprove = false;
        //             }
        //             else{
        //                 if(restoreApproveCells==""){
        //                     restoreApproveCells = nestedValue;
        //                 }else{
        //                     restoreApproveCells = restoreApproveCells+","+nestedValue;
        //                 }                        
        //             }                                                            
        //         }); 
        //         if(isValidForUnapprove){                    
        //             alert("There is no data to approved!");
        //         }else{
        //             $("#all_selected_row_for_approve").val(restoreApproveCells);   
        //             var rowNumber = $("#hidSelectedRowNumber").val();
        //             if(isDeleted =='true'){
        //                 var selectRowIsPendingForApproval = $("#pending_selected_row").val();
        //                 if(selectRowIsPendingForApproval =='true'){
        //                     SetRowColor_UnapprovedDeleteRow(parseInt(rowNumber)+1);
        //                 }else{
        //                     SetRowColor_AfterUnApproved(parseInt(rowNumber)+1);
        //                 }                        
        //             }else{
        //                 var deletedRowIsPendingForApproval = $("#pending_selected_deleted_row").val();
        //                 if(deletedRowIsPendingForApproval =='true'){
        //                     SetRowColor_UnapprovedDeleteRow(parseInt(rowNumber)+1);
        //                 }else{
        //                     SetRowColor_AfterUnApproved_Delete(parseInt(rowNumber)+1);
        //                 }                          
        //             }
        //             $("#hidSelectedRow_AssignementId").val("");
        //             $("#hidIsRowDeleted").val("");
        //         }                  
        //     }else{
        //         //un approve cells
        //         var approvedCells = $("#all_selected_cells").val();   
        //         var arrCells = approvedCells.split(',');        

        //         var restoreApproveCells = "";
        //         var isValidForUnapprove = true;

        //         $.each(arrCells, function (nextedIndex, nestedValue) {                                            
        //             var arrNestedCells = nestedValue.split('_');
        //             if(selectedCells == arrNestedCells[1] && approveAssignmentId == arrNestedCells[0]){                        
        //                 isValidForUnapprove = false;
        //             }
        //             else{
        //                 if(restoreApproveCells==""){
        //                     restoreApproveCells = arrNestedCells[0]+"_"+arrNestedCells[1];
        //                 }else{
        //                     restoreApproveCells = restoreApproveCells+","+arrNestedCells[0]+"_"+arrNestedCells[1];
        //                 }                        
        //             }                                                            
        //         }); 
        //         if(isValidForUnapprove){                    
        //             alert("There is no data to approved!");
        //         }else{
        //             $("#all_selected_cells").val(restoreApproveCells);   
        //             var cellNo = $("#selectCellNumber").val();    
                    
        //             var pendingCells = $("#pending_cells_selected_cells").val();
        //             var arrPendingCells = pendingCells.split(',');        
        //             var isPendingCells = false;
        //             $.each(arrPendingCells, function (nextedIndex, nestedValue) { 
        //                 if(selectedCells == nestedValue){
        //                     isPendingCells = true;  
        //                 }
        //             }); 
        //             if(isPendingCells){
        //                 SetRowColor_PendingCells(cellNo) ;
        //             }
        //             else{
        //                 SetCellWiseColor_ForUnApproved(cellNo);                                                                              
        //             }                
        //             //SetCellWiseColor_ForUnApproved(cellNo)
        //         }

        //         // $.ajax({
        //         //     url: `/api/utilities/UnApprovedCellData`,
        //         //     contentType: 'application/json',
        //         //     type: 'GET',
        //         //     async: true,
        //         //     dataType: 'json',
        //         //     data: "assignementId=" + approveAssignmentId+"&selectedCells="+selectedCells,
        //         //     success: function (data) {
        //         //         if(data==1){
        //         //             var assignmentYear = $('#assignment_year_list').val();
        //         //             // if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
        //         //             //     alert('年度を選択してください!!!');
        //         //             //     return false;
        //         //             // }    
        //         //             var cellNo = $("#selectCellNumber").val();
        //         //             // LoaderHide();
        //         //             SetCellWiseColor_ForUnApproved(cellNo)
        //         //             //alert("保存されました.")                    
        //         //             //ShowForecastResults(assignmentYear);
        //         //             //$(cellPosition).css('color', 'red');                            
        //         //             // alert("保存されました.")
        //         //             // location.reload();

        //         //             // var selectedCells = $("#hid_cellNo").val();

        //         //             // var rowNumber = $("#hidSelectedRowNumber").val();
        //         //             // if(isDeleted =='true'){
        //         //             //     SetRowColor_AfterApproved(parseInt(rowNumber)+1);
        //         //             // }else{
        //         //             //     SetRowColor_ForDeletedRow(parseInt(rowNumber)+1);
        //         //             // }
        //         //             // $("#hidSelectedRow_AssignementId").val("");
        //         //             // $("#hidIsRowDeleted").val("");
                            
        //         //         }
        //         //         else{
        //         //             // LoaderHide();
        //         //             alert("There is no data to approved!")
        //         //         }
        //         //         //_retriveddata = data;
        //         //     }
        //         // });       
        //     }            
        // }       
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
    
    $(document).on('click', '#assignment_year_data ', function () {    
        var assignmentYear = $('#assignment_year_list').val();
        $("#all_selected_cells").val("");
        $("#all_selected_row_for_approve").val("");
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }     
        
        LoaderShowJexcel();
            
        setTimeout(function () {                                 
            ShowForecastResults(assignmentYear);
        }, 3000);

        
    });
    $(document).on('click', '#cancel_forecast_history ', function () {    
        var assignmentYear = $('#assignment_year_list').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }
        LoaderShowJexcel();            
        setTimeout(function () {                               
            ShowForecastResults(assignmentYear);
        }, 3000);
        
    });
    
    $(document).ajaxComplete(function(){
        LoaderHideJexcel();
    });

});

function ShowForecastResults(year) {
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
        alert('年度を選択してください');
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
        url: `/api/utilities/SearchForApprovalEmployee`,
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
    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }
    var w = window.innerWidth;
    var h = window.innerHeight;
    
    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        filters: true,
        tableOverflow: true,
        freezeColumns: 3,
        defaultColWidth: 50,
        // tableWidth: w - 500 + "px",
        // tableHeight: (h - 300) + "px",
        tableWidth: w-280+ "px",
        tableHeight: (h-150) + "px",
        
        columns: [
            { title: "Id", type: 'hidden', name: "Id" },
            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150},
            { title: "Remarks", type: "text", name: "Remarks", width: 60 },
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
                width: 60 
            },
            { title: "単価(Unit Price)", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },
            { title: "Db Id", type: 'text', name: "Id" },
            { title: "DuplicateFrom", type: 'text', name: "DuplicateFrom" },
            { title: "DuplicateCount", type: 'text', name: "DuplicateCount" },
            { title: "RoleChanged", type: 'text', name: "RoleChanged" },
            { title: "UnitPriceChanged", type: 'text', name: "UnitPriceChanged" },
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
            { title: "skip1", type: 'hidden', name: "Id" },
            {
                title: "10月",
                type: "number",
                //readOnly: true,
                mask: "#,##0",
                name: "OctTotal",
                width: 60
            },
            {
                title: "11月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "NovTotal"
            },
            {
                title: "12月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "DecTotal"
            },
            {
                title: "1月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "JanTotal"
            },
            {
                title: "2月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "FebTotal"
            },
            {
                title: "3月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "MarTotal"
            },
            {
                title: "4月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "AprTotal"
            },
            {
                title: "5月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "MayTotal"
            },
            {
                title: "6月",
                type: "decimal",
                // readOnly: true,
                mask: "#,##0",
                name: "JunTotal"
            },
            {
                title: "7月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "JulTotal"
            },
            {
                title: "8月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "AugTotal"
            },
            {
                title: "9月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "SepTotal"
            },
            { title: "skip2", type: 'hidden', name: "Id" },
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
        ],
        minDimensions: [6, 10],
        columnSorting: true,
        oninsertrow: newRowInserted,
        ondeleterow: deleted,
        contextMenu: function (obj, x, y, e) {            
            return "";
        },
        onselection: selectionActive,   
        // onfocus: focus,     
    });

    $("#saved_approved_data").css("display", "block");
    $("#approve_forecast_data").css("display", "block");
    $("#unapprove_forecast_data").css("display", "block");

    //jss.deleteColumn(52, 43);
    jss.deleteColumn(53, 23);
    
    var jexcelHeadTdEmployeeName = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    jexcelHeadTdEmployeeName.addClass('arrow-down');
    var jexcelFirstHeaderRow = $('.jexcel > thead > tr:nth-of-type(1) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelFirstHeaderRow.css('top', '0px');
    var jexcelSecondHeaderRow = $('.jexcel > thead > tr:nth-of-type(2) > td');
    jexcelFirstHeaderRow.css('position', 'sticky');
    jexcelSecondHeaderRow.css('top', '20px');

    var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);
    var sCols = $("#jspreadsheet").jexcel("getSelectedColumns", true);
    
    console.log("sRows: "+sRows);
    console.log("sCols: "+sCols);

    //employee name column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {       
        $('.search_p').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_p').fadeIn("slow");
    });

    //section column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)').on('click', function () {               
        $('.search_section').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_section').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)').on('click', function () {               
        $('.search_department').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_department').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)').on('click', function () {               
        $('.search_incharge').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_incharge').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)').on('click', function () {               
        $('.search_role').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_role').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)').on('click', function () {               
        $('.search_explanation').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_explanation').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)').on('click', function () {               
        $('.search_company').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_company').fadeIn("slow");
    });
    //grade column sorting
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)').on('click', function () {               
        $('.search_grade').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_grade').fadeIn("slow");
    });
        //unit price column sorting
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)').on('click', function () {               
        $('.search_unit_price').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.search_unit_price').fadeIn("slow");
    });
    // $(".jexcel_content").css("max-height",window.innerHeight+200+"px !important");    
    // $("#head_total").css("width",w-300);

    var allRows = jss.getData();
    var count = 1;
    $.each(allRows, function (index,value) {
        // if (value['36'] == true && value['38'] == true) {
        //     SetRowColor_ApprovedRow(count);
        // }else if (value['36'] == true && value['38'] == false) {
        //     SetRowColor(count);
        // }
        if (value[jssTableDefinition.bcyr.index.toString()] == true && value[jssTableDefinition.bcyrApproved.index.toString()] == true) {
            SetRowColor_ApprovedRow(count);
        } else if (value[jssTableDefinition.bcyr.index.toString()] == true && value[jssTableDefinition.bcyrApproved.index.toString()] == false) {
            alert("test")
            SetRowColor(count);
        }
        else {
            var isApprovedCells = value[jssTableDefinition.isApproved.index.toString()];
            var columnInfo = value[jssTableDefinition.bcyrCell.index.toString()];
            var infoArray = columnInfo.split(',');            

            $.each(infoArray, function (nextedIndex, nestedValue) {
                if (parseInt(nestedValue) == jssTableDefinition.employeeName.index) {
                    jss.setStyle(jssTableDefinition.employeeName.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.employeeName.cellName + count, "color", "red");
                                       
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.remarks.index) {
                    jss.setStyle(jssTableDefinition.remarks.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.remarks.cellName + count, "color", "red");
               
                }

                if (parseInt(nestedValue) == jssTableDefinition.section.index) {
                    jss.setStyle(jssTableDefinition.section.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.section.cellName + count, "color", "red");
                   
                }

                if (parseInt(nestedValue) == jssTableDefinition.department.index) {
                    jss.setStyle(jssTableDefinition.department.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.department.cellName + count, "color", "red");
                   
                }

                if (parseInt(nestedValue) == jssTableDefinition.incharge.index) {
                    jss.setStyle(jssTableDefinition.incharge.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.incharge.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.role.index) {
                    jss.setStyle(jssTableDefinition.role.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.role.cellName + count, "color", "red");
                                   
                }

                if (parseInt(nestedValue) == jssTableDefinition.explanation.index) {
                    jss.setStyle(jssTableDefinition.explanation.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.explanation.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.company.index) {
                    jss.setStyle(jssTableDefinition.company.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.company.cellName + count, "color", "red");
                   
                }

                if (parseInt(nestedValue) == jssTableDefinition.grade.index) {
                    jss.setStyle(jssTableDefinition.grade.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.grade.cellName + count, "color", "red");
                
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.unitPrice.index) {
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + count, "color", "red");

                }

                if (parseInt(nestedValue) == jssTableDefinition.octM.index) {
                    jss.setStyle(jssTableDefinition.octM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.octM.cellName + count, "color", "red");
                 
                }

                if (parseInt(nestedValue) == jssTableDefinition.novM.index) {
                    jss.setStyle(jssTableDefinition.novM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.novM.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.decM.index) {
                    jss.setStyle(jssTableDefinition.decM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.decM.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.janM.index) {
                    jss.setStyle(jssTableDefinition.janM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.janM.cellName + count, "color", "red");
                
                }  

                if (parseInt(nestedValue) == jssTableDefinition.febM.index) {
                    jss.setStyle(jssTableDefinition.febM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.febM.cellName + count, "color", "red"); 
                 
                }

                if (parseInt(nestedValue) == jssTableDefinition.marM.index) {
                    jss.setStyle(jssTableDefinition.marM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.marM.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.aprM.index) {
                    jss.setStyle(jssTableDefinition.aprM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.aprM.cellName + count, "color", "red");
                   
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.mayM.index) {
                    jss.setStyle(jssTableDefinition.mayM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.mayM.cellName + count, "color", "red");
                  
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.junM.index) {
                    jss.setStyle(jssTableDefinition.junM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.junM.cellName + count, "color", "red");
                   
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.julM.index) {
                    jss.setStyle(jssTableDefinition.julM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.julM.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.augM.index) {
                    jss.setStyle(jssTableDefinition.augM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.augM.cellName + count, "color", "red");
                 
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.sepM.index) {
                    jss.setStyle(jssTableDefinition.sepM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.sepM.cellName + count, "color", "red");
                 
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.octT.index) {
                    jss.setStyle(jssTableDefinition.octT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.octT.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.novT.index) {
                    jss.setStyle(jssTableDefinition.novT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.novT.cellName + count, "color", "red");
                  
                }
                
                if (parseInt(nestedValue) == jssTableDefinition.decT.index) {
                    jss.setStyle(jssTableDefinition.decT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.decT.cellName + count, "color", "red");
                 
                }

                if (parseInt(nestedValue) == jssTableDefinition.janT.index) {
                    jss.setStyle(jssTableDefinition.janT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.janT.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.febT.index) {
                    jss.setStyle(jssTableDefinition.febT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.febT.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.marT.index) {
                    jss.setStyle(jssTableDefinition.marT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.marT.cellName + count, "color", "red");
                   
                }

                if (parseInt(nestedValue) == jssTableDefinition.aprT.index) {
                    jss.setStyle(jssTableDefinition.aprT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.aprT.cellName + count, "color", "red");
                   
                }

                if (parseInt(nestedValue) == jssTableDefinition.mayT.index) {
                    jss.setStyle(jssTableDefinition.mayT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.mayT.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.junT.index) {
                    jss.setStyle(jssTableDefinition.junT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.junT.cellName + count, "color", "red");
                
                }

                if (parseInt(nestedValue) == jssTableDefinition.julT.index) {
                    jss.setStyle(jssTableDefinition.julT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.julT.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.augT.index) {
                    jss.setStyle(jssTableDefinition.augT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.augT.cellName + count, "color", "red");
                  
                }

                if (parseInt(nestedValue) == jssTableDefinition.sepT.index) {
                    jss.setStyle(jssTableDefinition.sepT.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.sepT.cellName + count, "color", "red");
                                     
                }
                
                //if (parseInt(nestedValue) == 35) {
                //    jss.setStyle("AJ" + count, "background-color", "yellow");
                //    jss.setStyle("AJ" + count, "color", "red");
                   
                //}
            });

            // //approved cells color
            var approvedCells = value[jssTableDefinition.bcyrCellApproved.index.toString()];
            var arrApprovedCells = approvedCells.split(',');
            $.each(arrApprovedCells, function (nextedIndex, nestedValue2) {              
                if (parseInt(nestedValue2) == jssTableDefinition.employeeName.index) {
                    jss.setStyle(jssTableDefinition.employeeName.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.employeeName.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.remarks.index) {
                    jss.setStyle(jssTableDefinition.remarks.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.remarks.cellName + count, "color", "red");
                }

                if (parseInt(nestedValue2) == jssTableDefinition.section.index) {
                    jss.setStyle(jssTableDefinition.section.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.section.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.department.index) {
                    jss.setStyle(jssTableDefinition.department.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.department.cellName + count, "color", "red");
                }
 
                if (parseInt(nestedValue2) == jssTableDefinition.incharge.index) {
                    jss.setStyle(jssTableDefinition.incharge.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.incharge.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.role.index) {
                    jss.setStyle(jssTableDefinition.role.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.role.cellName + count, "color", "red");
                }

                if (parseInt(nestedValue2) == jssTableDefinition.explanation.index) {
                    jss.setStyle(jssTableDefinition.explanation.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.explanation.cellName + count, "color", "red");
                }
             
                if (parseInt(nestedValue2) == jssTableDefinition.company.index) {
                    jss.setStyle(jssTableDefinition.company.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.company.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.grade.index) {
                    jss.setStyle(jssTableDefinition.grade.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.grade.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.unitPrice.index) {
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.octM.index) {
                    jss.setStyle(jssTableDefinition.octM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.octM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.novM.index) {
                    jss.setStyle(jssTableDefinition.novM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.novM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.decM.index) {
                    jss.setStyle(jssTableDefinition.decM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.decM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.janM.index) {
                    jss.setStyle(jssTableDefinition.janM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.janM.cellName + count, "color", "red");
                }
             
                if (parseInt(nestedValue2) == jssTableDefinition.febM.index) {
                    jss.setStyle(jssTableDefinition.febM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.febM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.marM.index) {
                    jss.setStyle(jssTableDefinition.marM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.marM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.aprM.index) {
                    jss.setStyle(jssTableDefinition.aprM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.aprM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.mayM.index) {
                    jss.setStyle(jssTableDefinition.mayM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.mayM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.junM.index) {
                    jss.setStyle(jssTableDefinition.junM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.junM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.julM.index) {
                    jss.setStyle(jssTableDefinition.julM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.julM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.augM.index) {
                    jss.setStyle(jssTableDefinition.augM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.augM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.sepM.index) {
                    jss.setStyle(jssTableDefinition.sepM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.sepM.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.octT.index) {
                    jss.setStyle(jssTableDefinition.octT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.octT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.novT.index) {
                    jss.setStyle(jssTableDefinition.novT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.novT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.decT.index) {
                    jss.setStyle(jssTableDefinition.decT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.decT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.janT.index) {
                    jss.setStyle(jssTableDefinition.janT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.janT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.febT.index) {
                    jss.setStyle(jssTableDefinition.febT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.febT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.marT.index) {
                    jss.setStyle(jssTableDefinition.marT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.marT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.aprT.index) {
                    jss.setStyle(jssTableDefinition.aprT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.aprT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.mayT.index) {
                    jss.setStyle(jssTableDefinition.mayT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.mayT.cellName + count, "color", "red");
                }
                
                if (parseInt(nestedValue2) == jssTableDefinition.junT.index) {
                    jss.setStyle(jssTableDefinition.junT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.junT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.julT.index) {
                    jss.setStyle(jssTableDefinition.julT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.julT.cellName + count, "color", "red");
                }
              
                if (parseInt(nestedValue2) == jssTableDefinition.augT.index) {
                    jss.setStyle(jssTableDefinition.augT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.augT.cellName + count, "color", "red");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.sepT.index) {
                    jss.setStyle(jssTableDefinition.sepT.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.sepT.cellName + count, "color", "red");
                }
                //if (parseInt(nestedValue2) == 35) {
                //    jss.setStyle("AJ" + count, "background-color", "LightBlue");
                //    jss.setStyle("AJ" + count, "color", "red");
                //}
            });

            // //pending cells color
            var bCYRCellPending = value[jssTableDefinition.bcyrCellPending.index.toString()];
            var arrBCYRCellPending = bCYRCellPending.split(',');
            $.each(arrBCYRCellPending, function (nextedIndex, nestedValue2) {              
                if (parseInt(nestedValue2) == jssTableDefinition.employeeName.index) {
                    jss.setStyle(jssTableDefinition.employeeName.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.employeeName.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.remarks.index) {
                    jss.setStyle(jssTableDefinition.remarks.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.remarks.cellName + count, "color", "black");
                }

                if (parseInt(nestedValue2) == jssTableDefinition.section.index) {
                    jss.setStyle(jssTableDefinition.section.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.section.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.department.index) {
                    jss.setStyle(jssTableDefinition.department.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.department.cellName + count, "color", "black");
                }
 
                if (parseInt(nestedValue2) == jssTableDefinition.incharge.index) {
                    jss.setStyle(jssTableDefinition.incharge.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.incharge.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.role.index) {
                    jss.setStyle(jssTableDefinition.role.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.role.cellName + count, "color", "black");
                }

                if (parseInt(nestedValue2) == jssTableDefinition.explanation.index) {
                    jss.setStyle(jssTableDefinition.explanation.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.explanation.cellName + count, "color", "black");
                }
             
                if (parseInt(nestedValue2) == jssTableDefinition.company.index) {
                    jss.setStyle(jssTableDefinition.company.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.company.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.grade.index) {
                    jss.setStyle(jssTableDefinition.grade.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.grade.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.unitPrice.index) {
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.octM.index) {
                    jss.setStyle(jssTableDefinition.octM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.octM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.novM.index) {
                    jss.setStyle(jssTableDefinition.novM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.novM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.decM.index) {
                    jss.setStyle(jssTableDefinition.decM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.decM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.janM.index) {
                    jss.setStyle(jssTableDefinition.janM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.janM.cellName + count, "color", "black");
                }
             
                if (parseInt(nestedValue2) == jssTableDefinition.febM.index) {
                    jss.setStyle(jssTableDefinition.febM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.febM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.marM.index) {
                    jss.setStyle(jssTableDefinition.marM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.marM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.aprM.index) {
                    jss.setStyle(jssTableDefinition.aprM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.aprM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.mayM.index) {
                    jss.setStyle(jssTableDefinition.mayM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.mayM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.junM.index) {
                    jss.setStyle(jssTableDefinition.junM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.junM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.julM.index) {
                    jss.setStyle(jssTableDefinition.julM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.julM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.augM.index) {
                    jss.setStyle(jssTableDefinition.augM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.augM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.sepM.index) {
                    jss.setStyle(jssTableDefinition.sepM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.sepM.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.octT.index) {
                    jss.setStyle(jssTableDefinition.octT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.octT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.novT.index) {
                    jss.setStyle(jssTableDefinition.novT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.novT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.decT.index) {
                    jss.setStyle(jssTableDefinition.decT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.decT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.janT.index) {
                    jss.setStyle(jssTableDefinition.janT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.janT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.febT.index) {
                    jss.setStyle(jssTableDefinition.febT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.febT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.marT.index) {
                    jss.setStyle(jssTableDefinition.marT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.marT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.aprT.index) {
                    jss.setStyle(jssTableDefinition.aprT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.aprT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.mayT.index) {
                    jss.setStyle(jssTableDefinition.mayT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.mayT.cellName + count, "color", "black");
                }
                
                if (parseInt(nestedValue2) == jssTableDefinition.junT.index) {
                    jss.setStyle(jssTableDefinition.junT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.junT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.julT.index) {
                    jss.setStyle(jssTableDefinition.julT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.julT.cellName + count, "color", "black");
                }
              
                if (parseInt(nestedValue2) == jssTableDefinition.augT.index) {
                    jss.setStyle(jssTableDefinition.augT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.augT.cellName + count, "color", "black");
                }
                if (parseInt(nestedValue2) == jssTableDefinition.sepT.index) {
                    jss.setStyle(jssTableDefinition.sepT.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.sepT.cellName + count, "color", "black");
                }
                //if (parseInt(nestedValue2) == 35) {
                //    jss.setStyle("AJ" + count, "background-color", "red");
                //    jss.setStyle("AJ" + count, "color", "black");
                //}
            });
            
        }

        if (value[jssTableDefinition.bcyrApproved.index] == false && value[jssTableDefinition.isActive.index] == false && value[jssTableDefinition.isDeletePending.index] == false) {
            DisableRow(count);
        }
        else if (value[jssTableDefinition.isRowPending.index] == true) {
            SetRowColor_UnapprovedDeleteRow(count)
        }
        else if (value[jssTableDefinition.isDeletePending.index] == true) {
            SetRowColor_UnapprovedDeleteRow(count)
        }
        count++;
    });
    var rowCount = 1;
    $.each(allRows, function (index,value){
        $(jss.getCell(jssTableDefinition.assignmentId.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"opacity", "1");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.assignmentId.cellName + (rowCount))).addClass('readonly');        
        
        $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.employeeName.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.remarks.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.remarks.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.remarks.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.remarks.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.section.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.section.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.section.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.department.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.department.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.department.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.incharge.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.incharge.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.incharge.cellName + (rowCount))).addClass('readonly');     
        
        $(jss.getCell(jssTableDefinition.role.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.role.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.role.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.explanation.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.explanation.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.explanation.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.company.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.company.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.company.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.grade.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.grade.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.grade.cellName + (rowCount))).addClass('readonly');     


        $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.unitPrice.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.octM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.octM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.octM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.novM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.novM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.novM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.decM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.decM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.decM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.janM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.janM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.janM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.febM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.febM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.febM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.marM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.marM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.marM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.aprM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.aprM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.aprM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.mayM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.mayM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.mayM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.junM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.junM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.junM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.julM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.julM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.julM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.augM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.augM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.augM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.sepM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.sepM.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.sepM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.octT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.octT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.octT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.novT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.novT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.novT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.decT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.decT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.decT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.janT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.janT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.janT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.febT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.febT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.febT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.marT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.marT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.marT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.aprT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.aprT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.aprT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.mayT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.mayT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.mayT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.junT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.junT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.junT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.julT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.julT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.julT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.augT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.augT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.augT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.sepT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.sepT.cellName+rowCount,"opacity", "1 !");
        // jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        $(jss.getCell(jssTableDefinition.sepT.cellName + (rowCount))).addClass('readonly');     

        //$(jss.getCell("AJ" + (rowCount))).removeClass('readonly');
        //jss.setStyle("AJ"+rowCount,"opacity", "1 !");
        //// jss.setStyle(jssTableDefinition.assignmentId.cellName+rowCount,"color", "black");
        //$(jss.getCell("AJ" + (rowCount))).addClass('readonly');     

        rowCount++;
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
   // $('#search_p_text_box').val('');
});

var deleted = function (instance, x, y, value) {
    var assignmentIds = [];
    if (value.length > 0) {
        for (let i = 0; i < value.length; i++) {
            if (value[i][0].innerText != '' && value[i][0].innerText.toString().includes('new') == false) {
                assignmentIds.push(value[i][0].innerText);
            }

        }
        if (assignmentIds.length > 0) {
            $.ajax({
                url: `/api/utilities/ExcelDeleteAssignment/`,
                contentType: 'application/json',
                type: 'DELETE',
                async: false,
                dataType: 'json',
                data: JSON.stringify(assignmentIds),
                success: function (data) {
                    alert(data);
                }
            });
        }

    }

}


var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');   
}
function SetCellWiseColor(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "LightBlue");
    jss.setStyle(cellName,"color", "blue");
    jss.setStyle(cellName,"color", "red");
    $(jss.getCell(cellName)).addClass('readonly');    
}
function SetCellWiseColor_ForUnApproved(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "yellow");
    jss.setStyle(cellName,"color", "blue");
    jss.setStyle(cellName,"color", "red");
    $(jss.getCell(cellName)).addClass('readonly');    
}

function CheckForAlreadyApprovedCells(approveAssignmentId,selectedCells){

    var previousApprovedCells = $("#approved_selected_cells").val(); 
    var filteredPreviousApprovedCells = "";

    if(previousApprovedCells =="" || typeof previousApprovedCells === 'undefined' || previousApprovedCells === null){
        return false;
    }else{
        //isAlreadyApprovedCell
        arrPreviousCells = previousApprovedCells.split(",");
        var isCellExists = false;
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");
            if(arrNestedCells[0] == approveAssignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }else{
                if(filteredPreviousApprovedCells==""){
                    filteredPreviousApprovedCells = arrNestedCells[0]+"_"+arrNestedCells[1];
                }else{
                    filteredPreviousApprovedCells = filteredPreviousApprovedCells +","+arrNestedCells[0]+"_"+arrNestedCells[1];
                }
            }        
        })
        $("#approved_selected_cells").val(filteredPreviousApprovedCells); 

        return isCellExists;
    }
}

function CheckForValidCellRequest(approveAssignmentId,selectedCells,bCYRCellPending,cellPosition,selectedCellNo,selectedRowNumber){
    $.ajax({
        url: `/api/utilities/IsValidForApprovalCell`,
        contentType: 'application/json',
        type: 'GET',
        async: true,
        dataType: 'json',
        data: "assignementId=" + approveAssignmentId+"&selectedCells="+selectedCells,
        success: function (data) {
            if(parseInt(data)>0){
                //store the current cells status into hidden fields
                $("#hidSelectedRow_AssignementId").val(approveAssignmentId);
                $("#pending_cells_selected_cells").val(bCYRCellPending);
                
                $("#hid_SelectedCellPosition").val(cellPosition);
                $("#selectCellNumber").val(selectedCellNo);
            
                $("#hid_IsRowSelected").val("no");
                $("#hid_cellNo").val(selectedCells);
                $("#hidSelectedRowNumber").val(selectedRowNumber);
                
                //for multi cell approval 3 things needed:
                //approveAssignmentId_selectedCells_selectedCellNo

                //select multiple cells
                var approvedCells = $("#all_selected_cells").val();                               
                var approvedCellsWithPositions = $("#all_selected_cells_with_cellposition").val();                               

                var isApprovedCellColorChanged = false;
                var isCurrentCellsStore = true;
                
                var isAlreadyApprovedCell = CheckForAlreadyApprovedCells(approveAssignmentId,selectedCells);

                if(!isAlreadyApprovedCell){
                    //store approved data!
                    if(approvedCells =="" || typeof approvedCells === 'undefined' || approvedCells === null){
                        strApprovedCellsStore = approveAssignmentId +"_"+ selectedCells;
                        $("#all_selected_cells").val(strApprovedCellsStore);

                        strApprovedCellsWithSelectCellStore = approveAssignmentId +"_"+ selectedCells+"_"+selectedCellNo;
                        $("#all_selected_cells_with_cellposition").val(strApprovedCellsWithSelectCellStore);

                        isApprovedCellColorChanged = true;
                    }else{         
                        var selectedCellStore = "";
                        var selectedCellStoreWithCellPosition = "";
                        
                        var isValidForApprove = false;
                        var arrCells = approvedCells.split(',');        
                        //store cells and cells for approval
                        $.each(arrCells, function (nextedIndex, nestedValue) {     
                            var arrNestedValue = nestedValue.split("_");
                            //approveAssignmentId _ selectedCells _ selectedCellNo
                            if(arrNestedValue[0] == approveAssignmentId && selectedCells == arrNestedValue[1]){
                                isCurrentCellsStore = false;                                        
                            }else{                            
                                if(selectedCellStore == ""){
                                    selectedCellStore = arrNestedValue[0]+"_"+arrNestedValue[1];
                                }else{                                
                                    var isCellAlreadyExists = false;
                                    var arrSelectedCellStore = selectedCellStore.split(',');  
                                    $.each(arrSelectedCellStore, function (tempIndex, selectedCellItem) {
                                        arrSelectedCellItem = selectedCellItem.split("_");
                                        if(arrSelectedCellItem[0] == arrNestedValue[0] && arrSelectedCellItem[1] == arrNestedValue[1]) {
                                            isCellAlreadyExists = true;
                                        }
                                    }); 
                                    if(!isCellAlreadyExists){                                    
                                        selectedCellStore = selectedCellStore + ","+arrNestedValue[0]+"_"+arrNestedValue[1];                                    
                                    }                                
                                }
                            }                                                                                                      
                        });  
                        
                        //store cells and position for color
                        var arrApprovedCellsWithPositions = approvedCellsWithPositions.split(',');
                        $.each(arrApprovedCellsWithPositions, function (nextedIndex, nestedValue2) {     
                            var arrNestedValue = nestedValue2.split("_");
                            if(arrNestedValue[0] == approveAssignmentId && selectedCells == arrNestedValue[1]){
                                //statement here
                            }
                            else{                                  
                                if(selectedCellStoreWithCellPosition == ""){                                
                                    selectedCellStoreWithCellPosition = arrNestedValue[0]+"_"+arrNestedValue[1]+"_"+arrNestedValue[2];
                                }else{                                
                                    var isCellAlreadyExists = false;
                                    var arrSelectedCellStoreWithCellPosition = selectedCellStoreWithCellPosition.split(',');                                  
                                    $.each(arrSelectedCellStoreWithCellPosition, function (tempIndex, selectedItem2) {
                                        var arrSelectedItem2 = selectedItem2.split('_');
                                        if(arrSelectedItem2[0] == arrNestedValue[0] && arrSelectedItem2[1] == arrNestedValue[1]) {
                                            isCellAlreadyExists = true;
                                        }
                                    }); 
                                    if(!isCellAlreadyExists){
                                        selectedCellStoreWithCellPosition = selectedCellStoreWithCellPosition+","+arrNestedValue[0]+"_"+arrNestedValue[1]+"_"+arrNestedValue[2];
                                    }                                
                                }
                            }                                                                                             
                        });  

                        if(isCurrentCellsStore){
                            if(selectedCellStore ==""){
                                selectedCellStore = approveAssignmentId+"_"+selectedCells;
                                selectedCellStoreWithCellPosition = approveAssignmentId +"_"+selectedCells+"_"+selectedCellNo;
                            }else{
                                selectedCellStore = selectedCellStore +","+approveAssignmentId+"_"+selectedCells;;  
                                selectedCellStoreWithCellPosition = selectedCellStoreWithCellPosition+","+approveAssignmentId +"_"+selectedCells+"_"+selectedCellNo;                          
                            }                        
                        }
                        $("#all_selected_cells").val(selectedCellStore);
                        $("#all_selected_cells_with_cellposition").val(selectedCellStoreWithCellPosition);                                        
                    } 
                }else{
                    isCurrentCellsStore = false;
                }
                
                if(isCurrentCellsStore){
                    SetColor_MultiCell(selectedCellNo);                     
                }else{
                    if(data==1){
                        DeselectColor_Cells(selectedCellNo);
                    }else if(data==2){
                        DeselectColor_PendingCells(selectedCellNo);
                    }
                }
            }            
        }
    }); 
}

function CheckForAlreadyApprovedRows(approveAssignmentId){

    var previousApprovedRows = $("#approved_selected_rows").val(); 
    var filteredPreviousApprovedRows = "";

    if(previousApprovedRows =="" || typeof previousApprovedRows === 'undefined' || previousApprovedRows === null){
        return false;
    }else{
        //isAlreadyApprovedCell
        arrPreviousRows = previousApprovedRows.split(",");
        var isRowsExists = false;
        $.each(arrPreviousRows, function (nextedIndex, nestedValue){
            //var arrNestedRow = nestedValue.split("_");
            if(nestedValue == approveAssignmentId){
                isRowsExists = true;
            }else{
                if(filteredPreviousApprovedRows==""){
                    filteredPreviousApprovedRows = nestedValue;
                }else{
                    filteredPreviousApprovedRows = filteredPreviousApprovedRows +","+nestedValue;
                }
            }        
        })
        $("#approved_selected_rows").val(filteredPreviousApprovedRows); 

        return isRowsExists;
    }
}
function CheckForValidRowRequest(approveAssignmentId,isActive,isRowPending,isDeletePending,rowNumber){    
    $.ajax({        
        url: `/api/utilities/IsValidForApprovalRow`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "assignementId=" + approveAssignmentId+"&isDeletedRow="+isActive,
        success: function (data) {
            if (parseInt(data) >0) {                
                $("#hidSelectedRow_AssignementId").val(approveAssignmentId);                
                $("#pending_selected_row").val(isRowPending);
                $("#pending_selected_deleted_row").val(isDeletePending);                                

                $("#hid_IsRowSelected").val("yes");
                $("#hidIsRowDeleted").val(isActive);
                $("#hidSelectedRowNumber").val(rowNumber);

                var approvedRows = $("#all_selected_row_for_approve").val();    
                var approvedRowsWithAssignmentId = $("#all_selected_row_with_assignmentId_row_number").val();

                var isApprovedRowColorChanged = false;
                var isMultipleRowSelected = false;
                
                var isNewRowDisselect = false;
                var isPendingRowDisselect = false;
                var isInactiveDisselect = false;
                var isDeletePendingDisselect = false;

                //store approved data!
                //1->new row
                //2->pending row
                //3->inactive
                //4->delete pending
                
                //10,11,12
                //again:
                //clicked: 13
                var isCurrentRowStored = true;
                
                var isAlreadyApprovedRows = CheckForAlreadyApprovedRows(approveAssignmentId);

                if(!isAlreadyApprovedRows){
                    if(approvedRows =="" || typeof approvedRows === 'undefined' || approvedRows === null){
                        strApprovedRowsStore = approveAssignmentId;
                        $("#all_selected_row_for_approve").val(strApprovedRowsStore);
                        $("#all_selected_row_with_assignmentId_row_number").val(strApprovedRowsStore+"_"+rowNumber+"_"+isActive);
                        isApprovedRowColorChanged = true;                    
                    }else{         
                        var isValidForApprove = false;
                        var arrCells = approvedRows.split(',');    
                        var selectedRowsStore = "";
                        var selectedRowWithAssignmentIdAndRowNumber = "";

                        $.each(arrCells, function (nextedIndex, nestedValue) { 
                            if(nestedValue == approveAssignmentId){
                                isCurrentRowStored = false;

                                if(data==1){
                                    isNewRowDisselect = true;
                                }else if(data==2){
                                    isPendingRowDisselect = true;
                                }else if(data==3){
                                    isInactiveDisselect = true;
                                }else if(data==4){
                                    isDeletePendingDisselect = true;
                                }                
                            }else{
                                if(selectedRowsStore == ""){
                                    selectedRowsStore = nestedValue;
                                    //selectedRowWithAssignmentIdAndRowNumber = nestedValue+"_"+rowNumber;
                                }else{
                                    //only assignment id with comma seperated value
                                    var isRowAlreadyExists = false;
                                    var arrSelectedRowsStore = selectedRowsStore.split(',');  
                                    $.each(arrSelectedRowsStore, function (tempIndex, selectedItem) {
                                        if(selectedItem == nestedValue) {
                                            isRowAlreadyExists = true;
                                            //selectedRowsStore = selectedRowsStore+ ","+nestedValue;
                                        }
                                    }); 
                                    if(!isRowAlreadyExists){
                                        selectedRowsStore = selectedRowsStore+ ","+nestedValue;
                                        //selectedRowWithAssignmentIdAndRowNumber = selectedRowWithAssignmentIdAndRowNumber+","+nestedValue+"_"+rowNumber;
                                    }                                
                                }
                            }

                            // if(approveAssignmentId == nestedValue){
                            //     isValidForApprove = false;
                            // }
                            // else{
                            //     isValidForApprove = true;
                            //     isApprovedRowColorChanged = true;
                            // }                                              
                        });  
                        
                        //assignment id with row number: start                                        
                        var arrApprovedRowsWithAssignmentId = approvedRowsWithAssignmentId.split(',');                        
                        $.each(arrApprovedRowsWithAssignmentId, function (nextedIndex, nestedValue2) { 
                            var arrNestedValue = nestedValue2.split('_');
                            if(arrNestedValue[0] != approveAssignmentId){                                   
                                if(selectedRowWithAssignmentIdAndRowNumber == ""){                                
                                    selectedRowWithAssignmentIdAndRowNumber = arrNestedValue[0]+"_"+arrNestedValue[1]+"_"+arrNestedValue[2];
                                }else{                                
                                    var isRowAlreadyExists = false;
                                    var arrSelectedRowWithAssignmentIdAndRowNumber = selectedRowWithAssignmentIdAndRowNumber.split(',');                                  
                                    $.each(arrSelectedRowWithAssignmentIdAndRowNumber, function (tempIndex, selectedItem2) {
                                        var arrSelectedItem2 = selectedItem2.split('_');
                                        if(arrSelectedItem2[0] == arrNestedValue[0]) {
                                            isRowAlreadyExists = true;
                                            //selectedRowsStore = selectedRowsStore+ ","+nestedValue2;
                                        }
                                    }); 
                                    if(!isRowAlreadyExists){
                                        selectedRowWithAssignmentIdAndRowNumber = selectedRowWithAssignmentIdAndRowNumber+","+arrNestedValue[0]+"_"+arrNestedValue[1]+"_"+arrNestedValue[2];
                                    }                                
                                }
                            }
                        }); 
                        //assignment id with row number: end 

                        if(isCurrentRowStored){
                            if(selectedRowsStore ==""){
                                selectedRowsStore = approveAssignmentId;
                                selectedRowWithAssignmentIdAndRowNumber = approveAssignmentId +"_"+rowNumber+"_"+isActive;
                            }else{
                                selectedRowsStore = selectedRowsStore +","+approveAssignmentId;  
                                selectedRowWithAssignmentIdAndRowNumber = selectedRowWithAssignmentIdAndRowNumber+","+approveAssignmentId +"_"+rowNumber+"_"+isActive;                          
                            }                        
                        }

                        $("#all_selected_row_for_approve").val(selectedRowsStore);
                        $("#all_selected_row_with_assignmentId_row_number").val(selectedRowWithAssignmentIdAndRowNumber);                                                         
                    }
                }else{
                    if(data==1){
                        isNewRowDisselect = true;
                    }else if(data==2){
                        isPendingRowDisselect = true;
                    }else if(data==3){
                        isInactiveDisselect = true;
                    }else if(data==4){
                        isDeletePendingDisselect = true;
                    }   
                    isCurrentRowStored = false;
                }

                if(isCurrentRowStored){
                    if(data==1){
                        SetRowColor_OnlyLightYellow(parseInt(rowNumber)+1);
                    }else{
                        SetRowColor_MultiRowSelect(parseInt(rowNumber)+1);
                    }
                    //SetRowColor_MultiRowSelect(parseInt(rowNumber)+1);
                }else{
                    if(isNewRowDisselect){
                        SetRowColor_AfterUnApproved(parseInt(rowNumber)+1);
                    }else if(isPendingRowDisselect){
                        SetRowColor_UnapprovedDeleteRow(parseInt(rowNumber)+1);
                    }else if(isInactiveDisselect){
                        SetRowColor_AfterUnApproved_Delete(parseInt(rowNumber)+1);
                    }else if(isDeletePendingDisselect){
                        SetRowColor_UnapprovedDeleteRow(parseInt(rowNumber)+1);
                    }
                }
                $("#hidSelectedRow_AssignementId").val("");
                $("#hidIsRowDeleted").val("");                      
            }
        }
    }); 
}

var selectionActive = function(instance, x1, y1, x2, y2, origin) {

    var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);
    var sCols = $("#jspreadsheet").jexcel("getSelectedColumns", true);
       
    var cellName1 = jexcel.getColumnNameFromId([x1, y1]);
    var cellName2 = jexcel.getColumnNameFromId([x2, y2]);
    
    //get cell information
    //var retrivedData = retrivedObject_ApprovalData(jss.getRowData(y));
    /*
        cell info:x2/x1 ,1-employee name ,2-remarks, 3-sections, 4-dept. ,5-incharge ,6-role, 7-explanations ,8-company ,9-grade ,10-unit price, 11-oct, 12-nov, 13-dec, 14-jan, 15-feb, 16-mar,17-apr, 18-may, 19-jun, 20-july, 21-aug, 22-sept
        x2-34 means row select
    */

    var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);    
    //var selectedRows = $("#jspreadsheet").getSelectedRows();
    var retrivedData = retrivedObject_ApprovalData(jss.getRowData(sRows));

    if(typeof retrivedData != "undefined"){        
        if(x2==34){
            //row approval
            CheckForValidRowRequest(retrivedData.assignmentId,retrivedData.isActive,retrivedData.isRowPending,retrivedData.isDeletePending,sRows);            
        }else{
            //cells approval
            CheckForValidCellRequest(retrivedData.assignmentId,x2,retrivedData.bCYRCellPending,x1,cellName1,sRows);
        }
    }
}
// var focus = function(instance) {
//     $('#log').append('The table ' + $(instance).prop('id') + ' is focus');
// }	
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

function updateArrayForInsert(array, retrivedData) {
    var index = array.findIndex(d => d.assignmentId == retrivedData.assignmentId);
    array[index].employeeId = retrivedData.employeeId;
    array[index].remarks = retrivedData.remarks;
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

function retrivedObject(rowData) {
    return {
        // assignmentId: rowData[0],
        // employeeName: rowData[1],
        // remarks: rowData[2],
        // employeeId: rowData[42],
        // sectionId: rowData[3],
        // departmentId: rowData[4],
        // inchargeId: rowData[5],
        // roleId: rowData[6],
        // explanationId: rowData[7],
        // companyId: rowData[8],
        // gradeId: rowData[9],
        // unitPrice: parseFloat(rowData[10]),
        // octPoint: parseFloat(rowData[11]),
        // novPoint: parseFloat(rowData[12]),
        // decPoint: parseFloat(rowData[13]),
        // janPoint: parseFloat(rowData[14]),
        // febPoint: parseFloat(rowData[15]),
        // marPoint: parseFloat(rowData[16]),
        // aprPoint: parseFloat(rowData[17]),
        // mayPoint: parseFloat(rowData[18]),
        // junPoint: parseFloat(rowData[19]),
        // julPoint: parseFloat(rowData[20]),
        // augPoint: parseFloat(rowData[21]),
        // sepPoint: parseFloat(rowData[22]),
        // year: document.getElementById('assignment_year_list').value,
        // bcyr: rowData[43],
        // bCYRApproved: rowData[46]

        assignmentId: rowData[jssTableDefinition.assignmentId.index],
        employeeName: rowData[jssTableDefinition.employeeName.index],
        remarks: rowData[jssTableDefinition.remarks.index],
        employeeId: rowData[jssTableDefinition.employeeId.index],
        sectionId: rowData[jssTableDefinition.section.index],
        departmentId: rowData[jssTableDefinition.department.index],
        inchargeId: rowData[jssTableDefinition.incharge.index],
        roleId: rowData[jssTableDefinition.role.index],
        explanationId: rowData[jssTableDefinition.explanation.index],
        companyId: rowData[jssTableDefinition.company.index],
        gradeId: rowData[jssTableDefinition.grade.index],
        unitPrice: parseFloat(rowData[jssTableDefinition.unitPrice.index]),
        octPoint: parseFloat(rowData[jssTableDefinition.octM.index]),
        novPoint: parseFloat(rowData[jssTableDefinition.novM.index]),
        decPoint: parseFloat(rowData[jssTableDefinition.decM.index]),
        janPoint: parseFloat(rowData[jssTableDefinition.janM.index]),
        febPoint: parseFloat(rowData[jssTableDefinition.febM.index]),
        marPoint: parseFloat(rowData[jssTableDefinition.marM.index]),
        aprPoint: parseFloat(rowData[jssTableDefinition.aprM.index]),
        mayPoint: parseFloat(rowData[jssTableDefinition.mayM.index]),
        junPoint: parseFloat(rowData[jssTableDefinition.junM.index]),
        julPoint: parseFloat(rowData[jssTableDefinition.julM.index]),
        augPoint: parseFloat(rowData[jssTableDefinition.augM.index]),
        sepPoint: parseFloat(rowData[jssTableDefinition.sepM.index]),
        year: document.getElementById('assignment_year_list').value,
        bcyr: rowData[jssTableDefinition.bcyr.index],
        bCYRCell: rowData[jssTableDefinition.bcyrCell.index],
        isActive: rowData[jssTableDefinition.isActive.index],
        bCYRApproved: rowData[jssTableDefinition.bcyrApproved.index],
        bCYRCellApproved: rowData[jssTableDefinition.bcyrCellApproved.index],
        isApproved: rowData[jssTableDefinition.isApproved.index],
        bCYRCellPending: rowData[jssTableDefinition.bcyrCellPending.index],
        isRowPending: rowData[jssTableDefinition.isRowPending.index],
        isDeletePending: rowData[jssTableDefinition.isDeletePending.index],
        rowType: rowData[jssTableDefinition.rowType.index],
        duplicateFrom: rowData[jssTableDefinition.duplicateFrom.index],
        duplicateCount: rowData[jssTableDefinition.duplicateCount.index],
        roleChanged: rowData[jssTableDefinition.roleChanged.index],
        unitPriceChanged: rowData[jssTableDefinition.unitPriceChanged.index],

    };
}

function retrivedObject_ApprovalData(rowData) {
    if(typeof rowData != "undefined"){
        return {
            // assignmentId: rowData[0],
            // employeeName: rowData[1],
            // remarks: rowData[2],
            // employeeId: rowData[42],
            // sectionId: rowData[3],
            // departmentId: rowData[4],
            // inchargeId: rowData[5],
            // roleId: rowData[6],
            // explanationId: rowData[7],
            // companyId: rowData[8],
            // gradeId: rowData[9],
            // unitPrice: parseFloat(rowData[10]),

            // octPoint: parseFloat(rowData[11]),
            // novPoint: parseFloat(rowData[12]),
            // decPoint: parseFloat(rowData[13]),
            // janPoint: parseFloat(rowData[14]),
            // febPoint: parseFloat(rowData[15]),
            // marPoint: parseFloat(rowData[16]),
            // aprPoint: parseFloat(rowData[17]),
            // mayPoint: parseFloat(rowData[18]),
            // junPoint: parseFloat(rowData[19]),
            // julPoint: parseFloat(rowData[20]),
            // augPoint: parseFloat(rowData[21]),
            // sepPoint: parseFloat(rowData[22]),
            // year: document.getElementById('assignment_year_list').value,
            
            // // bcyr: rowData[36],
            // // bCYRApproved: rowData[37],
            // // isActive: rowData[39],
            // // BCYRCellApproved: rowData[40],
            // // isApproved: rowData[41],
            // // bCYRCellPending: rowData[42],
            // // isRowPending: rowData[43],
            // // isDeletePending: rowData[44]

            // bcyr: rowData[43],
            // bCYRApproved: rowData[46],
            // isActive: rowData[45],
            // BCYRCellApproved: rowData[47],
            // isApproved: rowData[48],
            // bCYRCellPending: rowData[49],
            // isRowPending: rowData[50],
            // isDeletePending: rowData[51]

            assignmentId: rowData[jssTableDefinition.assignmentId.index],
            employeeName: rowData[jssTableDefinition.employeeName.index],
            remarks: rowData[jssTableDefinition.remarks.index],
            employeeId: rowData[jssTableDefinition.employeeId.index],
            sectionId: rowData[jssTableDefinition.section.index],
            departmentId: rowData[jssTableDefinition.department.index],
            inchargeId: rowData[jssTableDefinition.incharge.index],
            roleId: rowData[jssTableDefinition.role.index],
            explanationId: rowData[jssTableDefinition.explanation.index],
            companyId: rowData[jssTableDefinition.company.index],
            gradeId: rowData[jssTableDefinition.grade.index],
            unitPrice: parseFloat(rowData[jssTableDefinition.unitPrice.index]),
            octPoint: parseFloat(rowData[jssTableDefinition.octM.index]),
            novPoint: parseFloat(rowData[jssTableDefinition.novM.index]),
            decPoint: parseFloat(rowData[jssTableDefinition.decM.index]),
            janPoint: parseFloat(rowData[jssTableDefinition.janM.index]),
            febPoint: parseFloat(rowData[jssTableDefinition.febM.index]),
            marPoint: parseFloat(rowData[jssTableDefinition.marM.index]),
            aprPoint: parseFloat(rowData[jssTableDefinition.aprM.index]),
            mayPoint: parseFloat(rowData[jssTableDefinition.mayM.index]),
            junPoint: parseFloat(rowData[jssTableDefinition.junM.index]),
            julPoint: parseFloat(rowData[jssTableDefinition.julM.index]),
            augPoint: parseFloat(rowData[jssTableDefinition.augM.index]),
            sepPoint: parseFloat(rowData[jssTableDefinition.sepM.index]),
            year: document.getElementById('assignment_year_list').value,
            bcyr: rowData[jssTableDefinition.bcyr.index],
            bCYRCell: rowData[jssTableDefinition.bcyrCell.index],
            isActive: rowData[jssTableDefinition.isActive.index],
            bCYRApproved: rowData[jssTableDefinition.bcyrApproved.index],
            bCYRCellApproved: rowData[jssTableDefinition.bcyrCellApproved.index],
            isApproved: rowData[jssTableDefinition.isApproved.index],
            bCYRCellPending: rowData[jssTableDefinition.bcyrCellPending.index],
            isRowPending: rowData[jssTableDefinition.isRowPending.index],
            isDeletePending: rowData[jssTableDefinition.isDeletePending.index],
            rowType: rowData[jssTableDefinition.rowType.index],
            duplicateFrom: rowData[jssTableDefinition.duplicateFrom.index],
            duplicateCount: rowData[jssTableDefinition.duplicateCount.index],
            roleChanged: rowData[jssTableDefinition.roleChanged.index],
            unitPriceChanged: rowData[jssTableDefinition.unitPriceChanged.index],
        };
    }
    
}

function DeleteRecords() {
    $.getJSON(`/api/utilities/DeleteAssignments/`)
        .done(function (data) {
            //$('#department_search').empty();
            //$('#department_search').append(`<option value=''>部署を選択</option>`);
            //$.each(data, function (key, item) {
            //    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
            //});
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
                    jss.setValueFromCoords(34, globalY, result, false);
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('データが保存されました!');
                    $('#employee_name').val('');
                    $('#jexcel_add_employee_modal').modal('hide');
                }

                //GetEmployeeList();
            },
            error: function (result) {
                alert(result.responseJSON.Message);
            }
        });
    }
}

//Get employee list
function GetEmployeeList() {
    $('#employee_list').empty();
    $.getJSON('/api/utilities/EmployeeList/')
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
    jss.setValueFromCoords(35, globalY, employeeId, false);
    $('#jexcel_add_employee_modal').modal('hide');
}
function UpdateForecast(){   
    $("#update_forecast").modal("hide");
    $("#jspreadsheet").hide();
    // $("#head_total").hide();
    LoaderShow(); 
    
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

    if (jssUpdatedData.length > 0) {
        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getDate();
        var year = dateObj.getUTCFullYear();
        var miliSeconds= dateObj.getMilliseconds();    
        var timestamp = `${year}${month}${day}${miliSeconds}_`;
        var promptValue = prompt("履歴ファイル保存名", '');

        if (promptValue == null || promptValue == undefined || promptValue == "") {
            return false;
        }
        else {
            updateMessage = "Successfully data updated";
            $.ajax({
                url: `/api/utilities/UpdateForecastData`,
                contentType: 'application/json',
                type: 'POST',
                async: true,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue, CellInfo:cellwiseColorCode }),
                success: function (data) {
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been updated by ', userName);
                    });        
                    $("#jspreadsheet").show();
                    //$("#head_total").show();
                    LoaderHide();             
                }
            });
            jssUpdatedData = [];
        }
    }
    else {
        $("#jspreadsheet").show();        
        //$("#head_total").show();
        LoaderHide();    
        //alert('追加、修正していないデータがありません!');
        updateMessage = ""
    }

    if (jssInsertedData.length > 0) {
        var elementIndex = jssInsertedData.findIndex(object => {
            return object.employeeName.toLowerCase() == 'total';
        });
        if (elementIndex >= 0) {
            jssInsertedData.splice(elementIndex, 1);
        }

        insertMessage = "Successfully data inserted.";
        $.ajax({
            url: `/api/utilities/ExcelAssignment/`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify(jssInsertedData),
            success: function (data) {
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been inserted by ', userName);
                });
                $("#jspreadsheet").show();
                //$("#head_total").show();
                LoaderHide(); 
            }
        });
        jssInsertedData = [];
        newRowCount = 1;
    } 
    if(updateMessage =="" && insertMessage==""){
        $("#header_show").html("");
        $("#update_forecast").modal("show");
        $("#save_modal_header").html("変更されていないので、保存できません");
        $("#back_button_show").css("display", "none");
        $("#save_btn_modal").css("display", "none");

        $("#close_save_modal").css("display", "block");
    }
    else if(updateMessage !="" && insertMessage !=""){
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
    else if(updateMessage !=""){
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
    else if(insertMessage !=""){
        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");

        alert("保存されました.");
    }
}
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
            // Start the connection.
            $.connection.hub.start().done(function () {
                chat.server.send('data has been inserted by ', userName);
            });
        }
    });
}

function GetAllForecastYears() {
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        //data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            $('#assignment_year_list').append(`<option value=''>年度データーの選択</option>`);
            $('#select_year_to_import').append(`<option value=''>年度を選択してください</option>`);
            $('#replicate_from').append(`<option value=''>年度を選択してください</option>`);
            //var count =1;
            $.each(data, function (index, element) {
                // if(count==1){
                //     $("#hidDefaultForecastYear").val(element.Year)
                // }
                $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
                $('#select_year_to_import').append(`<option value='${element.Year}'>${element.Year}</option>`);
                $('#replicate_from').append(`<option value='${element.Year}'>${element.Year}</option>`);
                //count++;
            });
        }
    });
}



function CheckForecastYear(){
    //var year = $('#assignment_year_list').find(":selected").val();
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!="" && typeof year != "undefined"){
        // $('#inputState').val(parseInt(year)+1);
        $('#select_import_year').val(parseInt(year)+1);
    }
}
// function ValidateYear(){
//     $("#csv_import_modal").modal("hide");
//     LoaderShow();
//     // var selectedYear = $('#inputState').find(":selected").val();
//     var selectedYear = $('#select_import_year').find(":selected").val();
//     if(selectedYear =="" || typeof selectedYear === "undefined"){
//         alert("please 年度を選択してください!");
//         return false;
//     }
    
// }
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        $('#duplciateYear').val(parseInt(year)+1);
        $('#replicate_from').val(year);
    }else{
        $('#duplciateYear').val('');
        $('#replicate_from').val('');
    }
}
function DuplicateForecast(){    
    var insertYear  = $('#duplciateYear').find(":selected").val();
    var copyYear = $('#replicate_from').find(":selected").val();

    if(copyYear!="" && insertYear!=""){
        $("#replicate_from_previous_year").modal("hide");
        $("#loading").css("display", "block");
        //LoaderShow();
        $.ajax({
            url: `/api/utilities/DuplicateForecastYear`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: "copyYear=" + copyYear+"&insertYear="+insertYear,
            success: function (data) {               
                LoaderHide();
                window.location.reload();
                alert("data has been replicated.")
            }
        });
    }else{
        alert("please select From and To year!");
        return false;
    }
}
function validate(){
    var selectedYear = $('#select_import_year').find(":selected").val();
    var import_file = $('#import_file_excel').val();
   
    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("please 年度を選択してください!");
        return false;
    }else if(import_file =="" || typeof import_file === "undefined"){
        alert("please select import file!");
        return false;
    }else { 
        return true; 
    }
}

$('#frm_import_year_data').submit(validate);
function SetRowColor(insertedRowNumber){
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.unitPrice.cellName + insertedRowNumber, "color", "red");
    // 5 columns.
    jss.setStyle(jssTableDefinition.dbId.cellName + insertedRowNumber, "background-color", "yellow");
    jss.setStyle(jssTableDefinition.dbId.cellName + insertedRowNumber, "color", "red");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName + insertedRowNumber, "background-color", "yellow");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName + insertedRowNumber, "color", "red");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName + insertedRowNumber, "background-color", "yellow");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName + insertedRowNumber, "color", "red");
    jss.setStyle(jssTableDefinition.roleChanged.cellName + insertedRowNumber, "background-color", "yellow");
    jss.setStyle(jssTableDefinition.roleChanged.cellName + insertedRowNumber, "color", "red");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName + insertedRowNumber, "background-color", "yellow");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName + insertedRowNumber, "color", "red");


    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "yellow");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "yellow");
    jss.setStyle("AJ"+insertedRowNumber,"color", "red");
}

function SetRowColor_AfterApproved(insertedRowNumber){
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "LightBlue");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}

function SetRowColor_AfterUnApproved(insertedRowNumber){    
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color","yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "yellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}

function SetRowColor_AfterUnApproved_Delete(insertedRowNumber){
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "gray");
    jss.setStyle("AJ"+insertedRowNumber,"color", "black");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}
function SetRowColor_ForDeletedRow(insertedRowNumber){  
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName + insertedRowNumber, "background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName + insertedRowNumber, "color", "black");

    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "Pink");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "Pink");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "black");
    
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"color", "black");
}

function SetRowColor_AfterSaved(insertedRowNumber){
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.employeeName.cellName + insertedRowNumber, "background-color", "white");
    jss.setStyle(jssTableDefinition.employeeName.cellName + insertedRowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "black");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "white");
    jss.setStyle("AJ"+insertedRowNumber,"color", "black");
}

function SetRowColor_ApprovedRow(insertedRowNumber){
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.employeeName.cellName + insertedRowNumber, "background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.employeeName.cellName + insertedRowNumber, "color", "red");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "red");
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle("AJ"+insertedRowNumber,"color", "red");
}
function DisableRow(rowNumber) {

    jss.setStyle(jssTableDefinition.assignmentId.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.assignmentId.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.employeeName.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.employeeName.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.remarks.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.remarks.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.section.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.section.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.section.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.department.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.department.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.department.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.incharge.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.incharge.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.role.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.role.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.role.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.explanation.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.explanation.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.company.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.company.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.company.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.grade.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.grade.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.grade.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.unitPrice.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.unitPrice.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowNumber))).addClass('readonly');

    // 5 columns.
    jss.setStyle(jssTableDefinition.dbId.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.dbId.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.duplicateFrom.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.duplicateCount.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.roleChanged.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.roleChanged.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (rowNumber))).addClass('readonly');



    jss.setStyle(jssTableDefinition.octM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.octM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.octM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.novM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.novM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.novM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.decM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.decM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.decM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.janM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.janM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.janM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.febM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.febM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.febM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.marM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.marM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.marM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.aprM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.aprM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.mayM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.mayM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.junM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.junM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.junM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.julM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.julM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.julM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.augM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.augM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.augM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.sepM.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.sepM.cellName + rowNumber, "color", "black");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (rowNumber))).addClass('readonly');

    jss.setStyle(jssTableDefinition.octT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.octT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.novT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.novT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.decT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.decT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.janT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.janT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.febT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.febT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.marT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.marT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.aprT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.aprT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.mayT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.mayT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.junT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.junT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.julT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.julT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.augT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.augT.cellName + rowNumber, "color", "black");
    jss.setStyle(jssTableDefinition.sepT.cellName + rowNumber, "background-color", "gray");
    jss.setStyle(jssTableDefinition.sepT.cellName + rowNumber, "color", "black");
    jss.setStyle("AJ" + rowNumber, "background-color", "gray");
    jss.setStyle("AJ" + rowNumber, "color", "black");
}

function SetRowColor_UnapprovedDeleteRow(insertedRowNumber){
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "red");
    // jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "white");
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "red");
    jss.setStyle("AJ"+insertedRowNumber,"color", "black");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}


function SetRowColor_PendingCells(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "red");
    jss.setStyle(cellName,"color", "black");    
    $(jss.getCell(cellName)).addClass('readonly');    
}

function SetRowColor_MultiRowSelect(insertedRowNumber){
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle("AJ"+insertedRowNumber,"color", "red");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}

function SetRowColor_OnlyLightYellow(insertedRowNumber){
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "lightyellow");
    //jss.setStyle("AJ"+insertedRowNumber,"color", "red");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}
function SetColor_MultiCell(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "lightyellow");
    jss.setStyle(cellName,"color", "blue");
    jss.setStyle(cellName,"color", "red");
    $(jss.getCell(cellName)).addClass('readonly');    
}
function DeselectColor_Cells(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "yellow");
    jss.setStyle(cellName,"color", "blue");
    jss.setStyle(cellName,"color", "red");
    $(jss.getCell(cellName)).addClass('readonly');    
}
function DeselectColor_PendingCells(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "red");
    jss.setStyle(cellName,"color", "blue");
    jss.setStyle(cellName,"color", "black");
    $(jss.getCell(cellName)).addClass('readonly');    
}

/*
    author: sudipto.
    date:18July23.
    approve,not approved and deleted rows color code functions.
*/
function SetColorCommonRow(rowNumber,backgroundColor,textColor,requestType){         
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.assignmentId.cellName + (rowNumber))).removeClass('readonly');
    }    
    jss.setStyle(jssTableDefinition.assignmentId.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.assignmentId.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.assignmentId.cellName + (rowNumber))).addClass('readonly');
    }    

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.employeeName.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.employeeName.cellName+rowNumber,"color", textColor);    
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.remarks.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.remarks.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.remarks.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.remarks.cellName + (rowNumber))).addClass('readonly');
    }


    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.section.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.section.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.section.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.section.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.department.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.department.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.department.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.department.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.incharge.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.incharge.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.incharge.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.incharge.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.role.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.role.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.role.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.role.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.explanation.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.explanation.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.explanation.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.explanation.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.company.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.company.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.company.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.company.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.grade.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.grade.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.grade.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.grade.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.unitPrice.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.unitPrice.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.octM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.octM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.octM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.octM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.novM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.novM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.novM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.novM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.decM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.decM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.decM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.decM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.janM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.janM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.janM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.janM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.febM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.febM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.febM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.febM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.marM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.marM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.marM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.marM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.aprM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.aprM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.aprM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.aprM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.mayM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.mayM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.mayM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.mayM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.junM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.junM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.junM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.junM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.julM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.julM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.julM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.julM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.augM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.augM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.augM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.augM.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.sepM.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.sepM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.sepM.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.sepM.cellName + (rowNumber))).addClass('readonly');
    }

    $(jss.getCell(jssTableDefinition.octT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.octT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.octT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.novT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.novT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.decT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.decT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.janT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.janT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.febT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.febT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.marT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.marT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.aprT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.aprT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.mayT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.mayT.cellName + (rowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.junT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.junT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.julT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.julT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.augT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.augT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.sepT.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.sepT.cellName + (rowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AJ"+rowNumber,"color", textColor);
    $(jss.getCell("AJ" + (rowNumber))).addClass('readonly');
}
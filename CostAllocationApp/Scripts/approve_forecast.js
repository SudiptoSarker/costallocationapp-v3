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

var _retriveddata = [];
var year="",employeeName="",sectionId="",inchargeId="",roleId="",companyId="",companyId="",departmentId="",explanationId="";

$("#hider").hide();
$(".employee_sorting").hide();
$(".section_sorting").hide();
$(".department_sorting").hide();
$(".incharge_sorting").hide();
$(".role_sorting").hide();
$(".explanation_sorting").hide();
$(".company_sorting").hide();
$(".grade_sorting").hide();
$(".unit_sorting").hide();

$(document).ready(function(){    
    $(".sorting_custom_modal").css("display", "block");

    //user notification message
    var chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#save_notifications').append(`<li>${name} ${message}</li>`);
    };

    //year list function called
    GetAllForecastYears();

    var year = $('#hidForecastYear').val();
    if (year.toLowerCase() != "imprt") {
        $("#jspreadsheet").hide();  
    }
    var count = 1;
    $('#employee_list').select2();
    
    //approved data save into db
    $('#saved_approved_data').on('click', function () {
        var assignmentYear = $('#assignment_year_list').val();
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }  
               
        var approvedCells = $("#approved_selected_cells").val(); 
        var approvedRows = $("#approved_selected_rows").val();
        
        var isValidRequest = false;
        var isApprove = false;
        if ((approvedCells != null && approvedCells != undefined && approvedCells != "") || (approvedRows != null && approvedRows != undefined && approvedRows != "")){
            isValidRequest = true;
            isApprove = true;
        }else{
            if (confirm("承認はありません。 保存しますか?") == true) {
                isValidRequest = true;
                isApprove = false;                   
            } else {
                isValidRequest = false;   
                return false;             
            } 
            
        }

        if (isValidRequest){
            var approvePromptValue = prompt("承認履歴ファイル保存名", '');
            if (approvePromptValue == null || approvePromptValue == undefined || approvePromptValue == "") {
                return false;
            }else{ 
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
                    data: "assignmentYear=" + assignmentYear+"&historyName="+timestamp+approvePromptValue+"&approvalCellsWithAssignmentId="+approvedCells+"&approvedRows="+approvedRows+"&isApprove="+isApprove,
                    success: function (data) {
                        if(data==1){
                            $("#approved_selected_cells").val(''); 
                            $("#approved_selected_rows").val('');                                  
                            ShowForecastResults(assignmentYear,'save');                            
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
            alert("承認するデータがありません ");
        }else{
            if(isRowApprovalRequest){
                var isDeleteRequest = false;
                var isDeleteReqestConfirmed = false;
                var deletedRow_assignmentId = "";

                var arrSelectedRowsWithRowNumber = selectedRowsWithRowNumber.split(",");
                $.each(arrSelectedRowsWithRowNumber, function (nextedIndex, nestedValue){
                    var arrNestedValue = nestedValue.split("_");  
                    if(arrNestedValue[2] == "true")  {                        
                    }else{
                        isDeleteRequest = true;
                    }                
                });
                if(isDeleteRequest){
                    if (confirm("過去データを変更された、及び削除されたデータを承認すると、過去データも変更、削除されます。注意してください！！") == true){
                        isDeleteReqestConfirmed = true;
                    }
                }                
                $.each(arrSelectedRowsWithRowNumber, function (nextedIndex, nestedValue){
                    var arrNestedValue = nestedValue.split("_");  
                    if(arrNestedValue[2] == "true")  {
                        SetRowColor_AfterApproved(parseInt(arrNestedValue[1])+1);           
                    }else{
                        if(deletedRow_assignmentId==""){
                            deletedRow_assignmentId = arrNestedValue[0];
                        }else{
                            deletedRow_assignmentId = deletedRow_assignmentId +","+arrNestedValue[0];
                        }
                        if(isDeleteReqestConfirmed){
                            SetRowColor_ForDeletedRow(parseInt(arrNestedValue[1])+1);           
                        }else{
                            var isPending = false;
                            $.ajax({
                                url: `/api/utilities/IsPendingForDelete`,
                                contentType: 'application/json',
                                type: 'GET',
                                async: false,
                                dataType: 'json',
                                data: "assignementId=" + arrNestedValue[0],
                                success: function (data) {
                                    isPending = data;         
                                }
                            }); 
                            if(isPending){
                                SetRowColor_UnapprovedDeleteRow(parseInt(arrNestedValue[1])+1);   
                            }else{
                                SetRowColor_AfterUnApproved_Delete(parseInt(arrNestedValue[1])+1);   
                            }                            
                        }                        
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
        $("#all_selected_row_for_approve").val('');
        $("#all_selected_row_with_assignmentId_row_number").val('');        
        
        var previousSelectedRows = $("#approved_selected_rows").val();
        if(previousSelectedRows == "" || previousSelectedRows == null || previousSelectedRows == undefined){
            $("#approved_selected_rows").val(approvedRows);
        }else{
            approvedRows = previousSelectedRows +","+approvedRows;
            $("#approved_selected_rows").val(approvedRows);
        }        

        if(!isDeleteReqestConfirmed){
            var approvalAssignmentIds = "";
            var allRowIds = $("#approved_selected_rows").val();

            var arrAllRowIds = allRowIds.split(",");

            $.each(arrAllRowIds, function (nextedIndex, rowItem){                
                var arrDeletedRowIds = deletedRow_assignmentId.split(",");
                
                var isRowIdExistsInAllRows = false;
                $.each(arrDeletedRowIds, function (nextedIndex, deleteItem){
                    if(rowItem == deleteItem){
                        isRowIdExistsInAllRows = true;
                    }                    
                });     
                if(!isRowIdExistsInAllRows){
                    if(approvalAssignmentIds==""){
                        approvalAssignmentIds = rowItem;
                    }else{
                        approvalAssignmentIds = approvalAssignmentIds +","+rowItem;
                    }
                }
            });
            $("#approved_selected_rows").val(approvalAssignmentIds)
        }                
    });

    //refresh saved data and other information 
    $('#unapprove_forecast_data').on('click', function () {               
        LoaderShow();  
        var assignmentYear = $('#assignment_year_list').val();                
        ShowForecastResults(assignmentYear,'show');

        $("#approved_selected_cells").val(''); 
        $("#all_selected_cells").val('');    
        $("#all_selected_cells_with_cellposition").val('');    

        $("#approved_selected_rows").val(''); 
        $("#all_selected_row_for_approve").val('');    
        $("#all_selected_row_with_assignmentId_row_number").val('');            
    });

    //after select year, click on show button to show the forecast data
    $(document).on('click', '#assignment_year_data ', function () {            
        var assignmentYear = $('#assignment_year_list').val();
        $("#all_selected_cells").val("");
        $("#all_selected_row_for_approve").val("");
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }             
        LoaderShow();         
        ShowForecastResults(assignmentYear,'show');                    
    });

    $("#buttonClose,#buttonClose_section,#buttonClose_department,#buttonClose_incharge,#buttonClose_role,#buttonClose_explanation,#buttonClose_company,#buttonClose_grade,#buttonClose_unit_price").click(function () {
        $("#hider").fadeOut("slow");
        $('.employee_sorting').fadeOut("slow");
        $('.section_sorting').fadeOut("slow");
        $('.department_sorting').fadeOut("slow");
        $('.incharge_sorting').fadeOut("slow");
        $('.role_sorting').fadeOut("slow");
        $('.explanation_sorting').fadeOut("slow");
        $('.company_sorting').fadeOut("slow");
        $('.grade_sorting').fadeOut("slow");
        $('.unit_sorting').fadeOut("slow");
    });
});

//loader show
function LoaderShow() {
    $("#jspreadsheet").css("display", "none");
    $("#loading").css("display", "block");
}
//loader hide
function LoaderHide() {
    $("#jspreadsheet").css("display", "block");
    $("#loading").css("display", "none");
}
//get year list
function GetAllForecastYears() {
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#assignment_year_list').append(`<option value=''>年度データーの選択</option>`);
            $.each(data, function (index, element) {                
                $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
            });
        }
    });
}

//show all the forecast data with approve and unapprove color 
function ShowApproveJexcel(){
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
            { title: "ID", type: 'text', name: "Id" },
            { title: "複製元", type: 'text', name: "DuplicateFrom" },
            { title: "複製数", type: 'text', name: "DuplicateCount" },
            { title: "役割等変更", type: 'text', name: "RoleChanged" },
            { title: "単価変更", type: 'text', name: "UnitPriceChanged" },
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
                mask: "#,##0",
                name: "OctTotal",
                width: 60
            },
            {
                title: "11月",
                type: "decimal",
                mask: "#,##0",
                name: "NovTotal"
            },
            {
                title: "12月",
                type: "decimal",
                mask: "#,##0",
                name: "DecTotal"
            },
            {
                title: "1月",
                type: "decimal",
                mask: "#,##0",
                name: "JanTotal"
            },
            {
                title: "2月",
                type: "decimal",
                mask: "#,##0",
                name: "FebTotal"
            },
            {
                title: "3月",
                type: "decimal",
                mask: "#,##0",
                name: "MarTotal"
            },
            {
                title: "4月",
                type: "decimal",
                mask: "#,##0",
                name: "AprTotal"
            },
            {
                title: "5月",
                type: "decimal",
                mask: "#,##0",
                name: "MayTotal"
            },
            {
                title: "6月",
                type: "decimal",
                mask: "#,##0",
                name: "JunTotal"
            },
            {
                title: "7月",
                type: "decimal",
                mask: "#,##0",
                name: "JulTotal"
            },
            {
                title: "8月",
                type: "decimal",
                mask: "#,##0",
                name: "AugTotal"
            },
            {
                title: "9月",
                type: "decimal",
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
        columnSorting: false,
        oninsertrow: newRowInserted,
        ondeleterow: deleted,
        contextMenu: function (obj, x, y, e) {            
            return "";
        },
        onselection: selectionActive,   
    });    
    jss.deleteColumn(52, 25);
    
    //sort arrow on load: employee
    var employee_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    employee_asc_arow.addClass('arrow-down');
    var employee_header = $('.jexcel > thead > tr:nth-of-type(1) > td');
    employee_header.css('position', 'sticky');
    employee_header.css('top', '0px');

    //sort arrow on load: section
    var section_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
    section_asc_arow.addClass('arrow-down');
    var section_header = $('.jexcel > thead > tr:nth-of-type(4) > td');
    section_header.css('position', 'sticky');
    section_header.css('top', '0px');
    
    //sort arrow on load: department
    var dept_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
    dept_asc_arow.addClass('arrow-down');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');

    //sort arrow on load: grade
    var grade_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
    grade_asc_arow.addClass('arrow-down');

    //sort arrow on load: unit
    var unit_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
    unit_asc_arow.addClass('arrow-down');

    //sort employee
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)').on('click', function () {            
        $('.employee_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.employee_sorting').fadeIn("slow");
    });

    // //section column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)').on('click', function () {  
        $('.section_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.section_sorting').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)').on('click', function () {     
        $('.department_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.department_sorting').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)').on('click', function () {  
        $('.incharge_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.incharge_sorting').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)').on('click', function () {         
        $('.role_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.role_sorting').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)').on('click', function () {    
        $('.explanation_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.explanation_sorting').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)').on('click', function () {     
        $('.company_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.company_sorting').fadeIn("slow");
    });
    //grade column sorting
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)').on('click', function () {        
        $('.grade_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.grade_sorting').fadeIn("slow");
    });
        //unit price column sorting
    $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)').on('click', function () { 
        $('.unit_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.unit_sorting').fadeIn("slow");
    });

    var allRows = jss.getData();
    var count = 1;
    $.each(allRows, function (index,value) {
        if (value[jssTableDefinition.bcyr.index.toString()] == true && value[jssTableDefinition.bcyrApproved.index.toString()] == true) {
            SetRowColor_ApprovedRow(count);
        } else if (value[jssTableDefinition.bcyr.index.toString()] == true && value[jssTableDefinition.bcyrApproved.index.toString()] == false) {
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
            });

            //pending cells color
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
        $(jss.getCell(jssTableDefinition.assignmentId.cellName + (rowCount))).addClass('readonly');        
        
        $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.employeeName.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.employeeName.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.remarks.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.remarks.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.remarks.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.section.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.section.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.section.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.department.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.department.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.department.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.incharge.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.incharge.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.incharge.cellName + (rowCount))).addClass('readonly');     
        
        $(jss.getCell(jssTableDefinition.role.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.role.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.role.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.explanation.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.explanation.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.explanation.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.company.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.company.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.company.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.grade.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.grade.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.grade.cellName + (rowCount))).addClass('readonly');     


        $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.unitPrice.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.unitPrice.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.octM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.octM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.octM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.novM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.novM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.novM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.decM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.decM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.decM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.janM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.janM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.janM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.febM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.febM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.febM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.marM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.marM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.marM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.aprM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.aprM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.aprM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.mayM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.mayM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.mayM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.junM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.junM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.junM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.julM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.julM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.julM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.augM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.augM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.augM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.sepM.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.sepM.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.sepM.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.octT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.octT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.octT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.novT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.novT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.novT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.decT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.decT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.decT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.janT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.janT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.janT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.febT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.febT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.febT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.marT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.marT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.marT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.aprT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.aprT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.aprT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.mayT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.mayT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.mayT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.junT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.junT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.junT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.julT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.julT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.julT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.augT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.augT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.augT.cellName + (rowCount))).addClass('readonly');     

        $(jss.getCell(jssTableDefinition.sepT.cellName + (rowCount))).removeClass('readonly');
        jss.setStyle(jssTableDefinition.sepT.cellName+rowCount,"opacity", "1 !");
        $(jss.getCell(jssTableDefinition.sepT.cellName + (rowCount))).addClass('readonly');       

        rowCount++;
    });
}

//show forecast table: calling function. this function called after several actions with showtype
function ShowForecastResults(year,showType) {
    employeeName = $('#name_search').val();
    employeeName = "";
    sectionId = $('#section_multi_search').val();
    sectionId = "";
    inchargeId = $('#incharge_multi_search').val();
    inchargeId = "";
    roleId = $('#role_multi_search').val();
    roleId = "";
    companyId = $('#company_multi_search').val();
    companyId = "";
    departmentId = $('#dept_multi_search').val();
    departmentId = "";
    explanationId = $('#explanation_multi_search').val();
    explanationId = "";    

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
        url: `/api/utilities/SearchForApprovalEmployee`,
        contentType: 'application/json',
        type: 'GET',
        async: true,
        dataType: 'json',
        data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            _retriveddata = data;            
            ShowApproveJexcel();
            LoaderHide();
            var chat = $.connection.chatHub;
            $.connection.hub.start();
            // Start the connection.
            $.connection.hub.start().done(function () {
                //chat.server.send('データは [アドナン]によって承認されている ', userName);
                chat.server.send('データは によって承認されている ', userName);
            });     

            $("#saved_approved_data").css("display", "block");
            $("#approve_forecast_data").css("display", "block");
            $("#unapprove_forecast_data").css("display", "block");            
            if(showType=='save'){
                ToastMessageSuccess('データが保存されました');                            
            }
        }
    });    
}

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
//new row color
var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');   
}
//cell color
function SetCellWiseColor(cellName){
    $(jss.getCell(cellName)).removeClass('readonly');
    jss.setStyle(cellName,"background-color", "LightBlue");
    jss.setStyle(cellName,"color", "blue");
    jss.setStyle(cellName,"color", "red");
    $(jss.getCell(cellName)).addClass('readonly');    
}

//check if the selected cell is already is approved or not
function CheckForAlreadyApprovedCells(approveAssignmentId,selectedCells){
    var previousApprovedCells = $("#approved_selected_cells").val(); 
    var filteredPreviousApprovedCells = "";

    if(previousApprovedCells =="" || typeof previousApprovedCells === 'undefined' || previousApprovedCells === null){
        return false;
    }else{
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

//this is backend validation before approving a cell, checking that if the approved request is valid or not
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

//check if the selected row is already is approved or not
function CheckForAlreadyApprovedRows(approveAssignmentId){

    var previousApprovedRows = $("#approved_selected_rows").val(); 
    var filteredPreviousApprovedRows = "";

    if(previousApprovedRows =="" || typeof previousApprovedRows === 'undefined' || previousApprovedRows === null){
        return false;
    }else{
        arrPreviousRows = previousApprovedRows.split(",");
        var isRowsExists = false;
        $.each(arrPreviousRows, function (nextedIndex, nestedValue){
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

//this is backend validation before approving a row, checking that if the approved request is valid or not
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
                                }else{
                                    //only assignment id with comma seperated value
                                    var isRowAlreadyExists = false;
                                    var arrSelectedRowsStore = selectedRowsStore.split(',');  
                                    $.each(arrSelectedRowsStore, function (tempIndex, selectedItem) {
                                        if(selectedItem == nestedValue) {
                                            isRowAlreadyExists = true;
                                        }
                                    }); 
                                    if(!isRowAlreadyExists){
                                        selectedRowsStore = selectedRowsStore+ ","+nestedValue;
                                    }                                
                                }
                            }                                                                   
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

//onclick check for cell and rows and store some values for approving the data
var selectionActive = function(instance, x1, y1, x2, y2, origin) {
    var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);
    var sCols = $("#jspreadsheet").jexcel("getSelectedColumns", true);
       
    var cellName1 = jexcel.getColumnNameFromId([x1, y1]);
    var cellName2 = jexcel.getColumnNameFromId([x2, y2]);

    var sRows = $("#jspreadsheet").jexcel("getSelectedRows", true);    
    var retrivedData = retrivedObject_ApprovalData(jss.getRowData(sRows));

    if(typeof retrivedData != "undefined"){        
        if(x2==40){
            //row approval
            CheckForValidRowRequest(retrivedData.assignmentId,retrivedData.isActive,retrivedData.isRowPending,retrivedData.isDeletePending,sRows);            
        }else{
            //cells approval
            CheckForValidCellRequest(retrivedData.assignmentId,x2,retrivedData.bCYRCellPending,x1,cellName1,sRows);
        }
    }
}

//update array when changing a data
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

//update array when changing a data
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

//update array when changing a data
function retrivedObject(rowData) {
    return {        
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
//update array when changing a data
function retrivedObject_ApprovalData(rowData) {
    if(typeof rowData != "undefined"){
        return {           
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

//set color for request data
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

//set approve color
function SetRowColor_AfterApproved(insertedRowNumber){
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "LightBlue");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}

function SetRowColor_AfterUnApproved(insertedRowNumber){    
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.assignmentId.cellName+insertedRowNumber,"background-color","yellow");
    
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "yellow");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "yellow");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "yellow");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "yellow");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "yellow");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "yellow");
    
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "yellow");
    $(jss.getCell("AJ" + (insertedRowNumber))).addClass('readonly');
}

//set delete color
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

    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "gray");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');



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

//set deleted row color
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

    //5 column
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"color", "black");

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "#ffcccc");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"color", "black");


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
    
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"color", "black");

    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"color", "black");

    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"color", "black");

    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"color", "black");

    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "white");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"color", "black");

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

    //5 column
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"color", "red");

    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"color", "red");

    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"color", "red");

    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"color", "red");

    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "LightBlue");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"color", "red");


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

    //5 column
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "red");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"color", "black");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');

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

    //5 column
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "lightyellow");
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"color", "red");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');
    

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
    $(jss.getCell(jssTableDefinition.assignmentId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.employeeName.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.employeeName.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.remarks.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.remarks.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.section.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.section.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.department.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.department.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.incharge.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.incharge.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.role.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.role.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.explanation.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.explanation.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.company.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.company.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.grade.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.grade.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPrice.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.unitPrice.cellName + (insertedRowNumber))).addClass('readonly');

    //5 column
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.dbId.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.dbId.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.roleChanged.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.roleChanged.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (insertedRowNumber))).addClass('readonly');


    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.octM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.novM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.decM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.janM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.febM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.marM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.aprM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.mayM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.junM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.julM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.augM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepM.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.sepM.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.octT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.octT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.novT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.novT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.decT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.decT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.janT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.janT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.febT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.febT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.marT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.marT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.aprT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.aprT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.mayT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.mayT.cellName + (insertedRowNumber))).addClass('readonly');
    
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.junT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.junT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.julT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.julT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.augT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.augT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.sepT.cellName+insertedRowNumber,"background-color", "lightyellow");
    $(jss.getCell(jssTableDefinition.sepT.cellName + (insertedRowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (insertedRowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+insertedRowNumber,"background-color", "lightyellow");
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

    //5 column
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.dbId.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.dbId.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.dbId.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.dbId.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.roleChanged.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.roleChanged.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.roleChanged.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.roleChanged.cellName + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (rowNumber))).addClass('readonly');
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

function ShowAllSortingAscIcon(){
    //sort arrow on load: employee
    var employee_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(3)');
    employee_asc_arow.addClass('arrow-down');
    var employee_header = $('.jexcel > thead > tr:nth-of-type(1) > td');
    employee_header.css('position', 'sticky');
    employee_header.css('top', '0px');
    $('#search_p_asc').css('background-color', 'lightsteelblue');
    $('#search_p_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: section
    var section_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(5)');
    section_asc_arow.addClass('arrow-down');
    var section_header = $('.jexcel > thead > tr:nth-of-type(4) > td');
    section_header.css('position', 'sticky');
    section_header.css('top', '0px');
    $('#search_section_asc').css('background-color', 'lightsteelblue');
    $('#search_section_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: department
    var dept_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(6)');
    dept_asc_arow.addClass('arrow-down');
    $('#search_department_asc').css('background-color', 'lightsteelblue');
    $('#search_department_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');
    $('#search_incharge_asc').css('background-color', 'lightsteelblue');
    $('#search_incharge_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');
    $('#search_role_asc').css('background-color', 'lightsteelblue');
    $('#search_role_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');
    $('#search_explanation_asc').css('background-color', 'lightsteelblue');
    $('#search_explanation_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');
    $('#search_company_asc').css('background-color', 'lightsteelblue');
    $('#search_company_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: grade
    var grade_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(11)');
    grade_asc_arow.addClass('arrow-down');
    $('#search_grade_asc').css('background-color', 'lightsteelblue');
    $('#search_grade_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: unit
    var unit_asc_arow = $('.jexcel > thead > tr:nth-of-type(1) > td:nth-of-type(12)');
    unit_asc_arow.addClass('arrow-down');
    $('#search_unit_price_asc').css('background-color', 'lightsteelblue');
    $('#search_unit_price_desc').css('background-color', 'lightsteelblue');
}

//shorting column functions
function ColumnOrder(columnNumber, orderBy,trType,tdType,sortIconIdAsc,sortIconIdDesc) {    
    jss.orderBy(columnNumber, orderBy);    
    ShowAllSortingAscIcon();

    var column_sort_icon = "";
    if (parseInt(orderBy) == 0) {
        $('#'+sortIconIdAsc).css('background-color', 'grey');
        $('#'+sortIconIdDesc).css('background-color', 'lightsteelblue');

        column_sort_icon = $('.jexcel > thead > tr:nth-of-type('+trType+') > td:nth-of-type('+tdType+')');
        column_sort_icon.addClass('arrow-down');                       
    }
    if (parseInt(orderBy) == 1) {
        $('#'+sortIconIdAsc).css('background-color', 'lightsteelblue');
        $('#'+sortIconIdDesc).css('background-color', 'grey');
        column_sort_icon = $('.jexcel > thead > tr:nth-of-type('+trType+') > td:nth-of-type('+tdType+')');
        column_sort_icon.addClass('arrow-down');      
    }        
}
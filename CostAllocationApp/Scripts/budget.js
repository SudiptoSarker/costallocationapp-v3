$(document).ready(function () { 
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


    //get list of budget year
    GetAllBudgetYear();

    //get list of finalize budget year
    GetAllFinalizeYear();

    //import file default text change
    window.pressed = function(){
        var a = document.getElementById('import_file_excel');
        if(a.value == "")
        {
            fileLabel.innerHTML = "Choose file";
        }
        else
        {
            var theSplit = a.value.split('\\');
            fileLabel.innerHTML = theSplit[theSplit.length-1];
        }
    };    

    //replicate modal input field clear
    $('.replicate_forecast_data').on('click', function () {
        ClearReplicateModal();
    });    

    //import budget selction menu
    $('#select_import_year').on('change', function() {
        var selectedBudgetYear = this.value;
        if (selectedBudgetYear != '' && selectedBudgetYear != null || selectedBudgetYear != undefined) {
            
            //check the selected year is valid for import the excel.
            CheckIsValidYearForImport(selectedBudgetYear);       
        }    
    });

    //duplicate budget selction menu
    $('#duplciateYear').on('change', function() {
        var selectedBudgetYear = this.value;
        
        if (selectedBudgetYear != '' && selectedBudgetYear != null || selectedBudgetYear != undefined) {
            
            //check the selected year is valid for replicate data
	        CheckIsValidYearForReplicate(selectedBudgetYear);             
        }    
    });

    //From date on change
    $('#duplicate_from').on('change', function() {
        var selectedBudgetYear = this.value;
        if (selectedBudgetYear != '' && selectedBudgetYear != null && selectedBudgetYear != undefined) {
            $("#duplciateYear").prop('disabled', false);
            $('#select_duplicate_budget_type').empty();
            $('#approval_timestamps').empty();
            GetApprovalTimestampListByYear(selectedBudgetYear);            
            SelectDuplicateBudgetYearAndType();            
        }        
        else{            
            $('#approval_timestamps').empty();
            $("#duplciateYear").val('');
            $("#duplciateYear").prop('disabled', true);
            $('#select_duplicate_budget_type').empty();
        }
    });    
        
    $("#jspreadsheet").hide();      
    var count = 1;

    $('#employee_list').select2();    

    //finalize the budget data.
    $('#budget_finalize').on('click', function () {
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
                    $("#save_bedget").prop("disabled",true);
                    $("#budget_finalize").prop("disabled",true); 
                }
            });
        }       
    });         

    //replicate the budget data from previous year budget.
    $('#create_duplicate_of_previous_year').on('click',function(){
        $("#validation_message").html("");

        var fromDate = $('#duplicate_from').val();
        var toDate  = $('#duplciateYear').val();
        var budgetType  = $('#select_duplicate_budget_type').val();
        var approve_timestamp  = $('#approval_timestamps').val();
    
        if (fromDate == null || fromDate == undefined || fromDate == "") {
            alert("策定する予算のベースとなる年度を選択してください")
            return false;
        } 
        if (approve_timestamp == null || approve_timestamp == undefined || approve_timestamp == "") {
            alert("策定する予算のベースとなる承認履歴を選択してください")
            return false;
        }
        if (toDate == null || toDate == undefined || toDate == "") {
            alert("策定する予算年度を選択してください")
            return false;
        }
        if (budgetType == null || budgetType == undefined || budgetType == "") {
            alert("策定する予算時期を選択してください")
            return false;
        }
        
        if(fromDate!="" && toDate!=""){
            LoaderShow();
            $("#replicate_from_previous_year").modal("hide");            
            $.ajax({
                url: `/api/utilities/DuplicateForecastYear`,
                contentType: 'application/json',
                type: 'GET',
                async: true,
                dataType: 'json',
                data: "copyYear=" + fromDate+"&insertYear="+toDate+"&budgetType="+budgetType+"&approve_timestamp="+approve_timestamp,
                success: function (data) {                       
                    if(parseInt(data)==5){
                        $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>選択した予算時期は既に登録済みです。他の予算時期を選択し、再度インポートしてください</span>");                        
                    }
                    else if(parseInt(data)==6){
                        $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>"+fromDate+" has no data to copy!</span>");                        
                    }
                    else{
                        $("#validation_message").html("<span id='validation_message_success' style='margin-left: 28px;'>予算データは正常に作成されました</span>");                        
                        // if(parseInt(data)>0){
                        //     $("#validation_message").html("<span id='validation_message_success' style='margin-left: 28px;'>インポートデータは正常に処理されました "+toDate+".</span>");                        
                        // }else{
                        //     $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Failed to Replicate the data!</span>");                        
                        // }
                    }
                    LoaderHide();                    
                }
            });
        }else{
            LoaderHide();       
            $("#validation_message").html("<span id='validation_message_failed' style='margin-left: 28px;'>Failed to Replicate the data!</span>");                        
            return false;
        }
    });

    var chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#save_notifications').append(`<li>${name} ${message}</li>`);
    };

});


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

//modal button close functions
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

var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');    
}

//update jexcel cell data to an array
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

//update jexcel add employee row to the array
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }
    }
    if (x == 7) {
        array[index].explanationId = retrivedData.explanationId;
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }
    }
    if (x == 11) {
        var octSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37,y);
            currentValue += ',new-x_'+x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }


    }

    if (x == 12) {
        var novSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }


    }
    if (x == 13) {
        var decSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 14) {
        var janSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 15) {
        var febSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    
    if (x == 16) {
        var marSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 17) {
        var aprSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 18) {
        var maySum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 19) {
        var junSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 20) {
        var julSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 21) {
        var augSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
    if (x == 22) {
        var sepSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[35].toString() == retrivedData.employeeId.toString() && dataValue[38] == true) {
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(37, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(37, y, currentValue, false);
        }

    }
        
    array[index].year = retrivedData.year;
    array[index].bcyr= retrivedData.bcyr;   
    
    if(array[index].bCYRCell.length <= retrivedData.bCYRCell.length){
        array[index].bCYRCell= retrivedData.bCYRCell;  
    }            
}

// set information to the object for retrive
function retrivedObject(rowData) {
    return {
        assignmentId: rowData[0],
        employeeName: rowData[1],
        remarks: rowData[2],
        employeeId: rowData[35],
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
        bcyr: rowData[36],
        bCYRCell: rowData[37],
        isActive: rowData[38],
        bCYRApproved: rowData[39],
        bCYRCellApproved: rowData[40],
        isApproved: rowData[41],
        bCYRCellPending: rowData[42],
        isRowPending: rowData[43],
        isDeletePending: rowData[44],
        rowType: rowData[45],
    };
}

//delete the row data
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


//add/register new employee
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
                    // jss.setValueFromCoords(34, globalY, result, false);
                    console.log("result: "+result);
                    console.log("globalY: "+globalY);

                    jss.setValueFromCoords(35, globalY, result, false);
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

//
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

//auto select csv improt year and budget type when modal is open.
function SelectImportBudgetYearAndType(){

    $.ajax({
        url: `/api/utilities/GetImportYearAndBudgetType`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            //auto select the year
            $('#select_import_year').val(data.Year);
            $("#select_import_year").datepicker({        
                minViewMode: 2,
                format: 'yyyy',
                startDate: "'"+data.Year+"'",
                endDate: "'"+data.Year+"'"
            });
    
            //auto select the budget type
            $('#select_budget_type').empty();
            $('#select_budget_type').append(`<option value="">予算時期を選択</option>`);

            //create fist half budget dropdown            
            if(data.FirstHalfFinalize){
                $('#select_budget_type').append(`<option value="1" disabled style='color:red;'>${data.Year} 初期予算が作成されました</option>`);
            }else if(data.FirstHalfBudget){
                $('#select_budget_type').append(`<option value="1" disabled style='color:orange;'>${data.Year} 初期予算は確定されていません</option>`);
            }else if(!data.FirstHalfBudget){
                $('#select_budget_type').append(`<option value="1" selected >${data.Year} 初期予算</option>`);
            }  
            
            //create second half budget dropdown
            if(data.SecondHalfFinalize){
                $('#select_budget_type').append(`<option value="2" disabled style='color:red;'>${data.Year} 下期修正予算が作成されました</option>`);
            }else if(data.SecondHalfBudget){
                $('#select_budget_type').append(`<option value="2" disabled style='color:orange;'>${data.Year} 下期修正予算は確定されていません</option>`);
            }else if(!data.SecondHalfBudget){
                if(data.FirstHalfBudget){
                    $('#select_budget_type').append(`<option value="2" selected>${data.Year} 下期修正予算</option>`);
                }else{
                    $('#select_budget_type').append(`<option value="2" disabled style='color:gray;'>${data.Year} 下期修正予算</option>`);
                }                                               
            }       
        }
    });
}

//auto select duplicatedata year and budget type when modal is open.
function SelectDuplicateBudgetYearAndType(){
    $.ajax({
        url: `/api/utilities/GetImportYearAndBudgetType`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            //auto select the year
            $('#duplciateYear').val(data.Year);
            $("#duplciateYear").datepicker({        
                minViewMode: 2,
                format: 'yyyy',
                startDate: "'"+data.Year+"'",
                endDate: "'"+data.Year+"'"
            });
            //auto select the budget type
            $('#select_duplicate_budget_type').empty();
            $('#select_duplicate_budget_type').append(`<option value="">予算時期を選択</option>`);

            //create fist half budget dropdown            
            if(data.FirstHalfFinalize){
                $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:red;'>${data.Year} 初期予算が作成されました</option>`);
            }else if(data.FirstHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:orange;'>${data.Year} 初期予算は確定されていません</option>`);
            }else if(!data.FirstHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="1" selected >${data.Year} 初期予算</option>`);
            }  
            
            //create second half budget dropdown
            if(data.SecondHalfFinalize){
                $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:red;'>${data.Year} 下期修正予算が作成されました</option>`);
            }else if(data.SecondHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:orange;'>${data.Year} 下期修正予算は確定されていません</option>`);
            }else if(!data.SecondHalfBudget){
                if(data.FirstHalfBudget){
                    $('#select_duplicate_budget_type').append(`<option value="2" selected>${data.Year} 下期修正予算</option>`);
                }else{
                    $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:gray;'>${data.Year} 下期修正予算</option>`);
                }                                               
            }       
        }
    });
}

//get approval list by year 
function GetApprovalTimestampListByYear(year){
    $.ajax({
        url: `/api/utilities/GetApprovalTimeStamps`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: { year: year },
        success: function (data) {
            if (data != 0){
                $('#approval_timestamps').empty();
                $('#approval_timestamps').append(`<option value="">タイムスタンプの選択</option>`);                
                var hasTimestamps = false;
                var timestampCount = 1;
                $.each(data, function (index, element) {    
                    if(parseInt(timestampCount)==1){
                        $('#approval_timestamps').append(`<option value="${element.Id}" selected>${element.TimeStamp}</option>`);  
                    }else{
                        $('#approval_timestamps').append(`<option value="${element.Id}">${element.TimeStamp}</option>`);  
                    }                    
                    hasTimestamps = true;
                    timestampCount++;            
                });
                if(hasTimestamps){
                    $('#approval_timestamps').append(`<option value="${year}">最新の予算確定 (latest finalize budget)</option>`);
                }else{
                    $('#approval_timestamps').append(`<option value="${year}" selected>最新の予算確定 (latest finalize budget)</option>`);
                }                
            }else{
                $('#approval_timestamps').empty();
                $('#approval_timestamps').append(`<option value="">タイムスタンプの選択</option>`);
                $('#approval_timestamps').append(`<option value="${year}" selected>最新の予算確定 (latest finalize budget)</option>`);                
            }  
        }
    });
}
/*
    author: sudipto.
    validate replicate data. 
*/
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        // $('#duplciateYear').val(parseInt(year)+1);
        $('#replicate_from').val(year);
    }else{
        // $('#duplciateYear').val('');
        $('#replicate_from').val('');
    }
}

/*
    author: sudipto.
    budget validation and submit the import file to server.
*/
function validate(){    
    // var selectedYear = $('#select_import_year').find(":selected").val();
    var selectedYear = $('#select_import_year').val();
    var budgetType = $('#select_budget_type').val();
    var import_file = $('#import_file_excel').val();

    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("年度を選択してください!");
        return false;
    }else if(budgetType =="" || typeof budgetType === "undefined"){
        alert("予算の時期を選択してください");
        return false;
    }
    else if(import_file =="" || typeof import_file === "undefined"){
        alert("インポートするファイルを選択してください");
        return false;
    }
    else { 
        $("#csv_import_modal").modal("hide");
        LoaderShow();
        return true; 
    }
}

$('#frm_import_year_data').submit(validate);

/*
    author: sudipto.
    date:17July23.
    approve,not approved and deleted rows color code functions.
*/
function SetColorCommonRow(rowNumber,backgroundColor,textColor,requestType){         
    if(requestType != "deleted"){
        $(jss.getCell("A" + (rowNumber))).removeClass('readonly');
    }    
    jss.setStyle("A"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("A"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("A" + (rowNumber))).addClass('readonly');
    }    

    if(requestType != "deleted"){
        $(jss.getCell("B" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("B"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("B"+rowNumber,"color", textColor);    
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("B" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("C" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("C"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("C"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("C" + (rowNumber))).addClass('readonly');
    }


    if(requestType != "deleted"){
        $(jss.getCell("D" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("D"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("D"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("D" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("E" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("E"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("E"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("E" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("F" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("F"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("F"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("F" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("G" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("G"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("G"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("G" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("H" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("H"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("H"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("H" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("I" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("I"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("I"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("I" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("J" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("J"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("J"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("J" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("K" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("K"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("K"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("K" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("L" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("L"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("L"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("L" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("M" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("M"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("M"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("M" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("N" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("N"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("N"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("N" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("O" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("O"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("O"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("O" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("P" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("P"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("P"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("P" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("Q" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("Q"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Q"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("Q" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("R" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("R"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("R"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("R" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("S" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("S"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("S"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("S" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("T" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("T"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("T"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("T" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("U" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("U"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("U"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("U" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("V" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("V"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("V"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("V" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("W" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("W"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("W"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("W" + (rowNumber))).addClass('readonly');
    }

    $(jss.getCell("X" + (rowNumber))).removeClass('readonly');
    jss.setStyle("X"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("X"+rowNumber,"color", textColor);
    $(jss.getCell("X" + (rowNumber))).addClass('readonly');

    $(jss.getCell("Y" + (rowNumber))).removeClass('readonly');
    jss.setStyle("Y"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Y"+rowNumber,"color", textColor);
    $(jss.getCell("Y" + (rowNumber))).addClass('readonly');

    $(jss.getCell("Z" + (rowNumber))).removeClass('readonly');
    jss.setStyle("Z"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Z"+rowNumber,"color", textColor);
    $(jss.getCell("Z" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AA" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AA"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AA"+rowNumber,"color", textColor);
    $(jss.getCell("AA" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AB" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AB"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AB"+rowNumber,"color", textColor);
    $(jss.getCell("AB" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AC" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AC"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AC"+rowNumber,"color", textColor);
    $(jss.getCell("AC" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AD" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AD"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AD"+rowNumber,"color", textColor);
    $(jss.getCell("AD" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AE" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AE"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AE"+rowNumber,"color", textColor);
    $(jss.getCell("AE" + (rowNumber))).addClass('readonly');
    
    $(jss.getCell("AF" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AF"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AF"+rowNumber,"color", textColor);
    $(jss.getCell("AF" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AG" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AG"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AG"+rowNumber,"color", textColor);
    $(jss.getCell("AG" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AH" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AH"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AH"+rowNumber,"color", textColor);
    $(jss.getCell("AH" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AI" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AI"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AI"+rowNumber,"color", textColor);
    $(jss.getCell("AI" + (rowNumber))).addClass('readonly');

    // $(jss.getCell("AJ" + (rowNumber))).removeClass('readonly');
    // jss.setStyle("AJ"+rowNumber,"background-color", backgroundColor);
    // jss.setStyle("AJ"+rowNumber,"color", textColor);
    // $(jss.getCell("AJ" + (rowNumber))).addClass('readonly');
}

//Replicate modal input field clear
function ClearReplicateModal(){
    $('#duplicate_from').val('');    
    $('#approval_timestamps').empty();
    $('#approval_timestamps').val('');        
    $("#duplciateYear").val('');
    $("#duplciateYear").prop('disabled', true);
    $('#select_duplicate_budget_type').val('');
    $('#select_duplicate_budget_type').empty();        
}

//check import year validation
function CheckIsValidYearForImport(selectedBudgetYear){
    $.ajax({
        url: `/api/utilities/CheckIsValidYearForImport/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "select_year_type=" + selectedBudgetYear,
        success: function (data) {
            if(data == true){
                CreateBudgetTypeWithYear(selectedBudgetYear);              
            }else{
                $('#select_budget_type').empty();
                $('#select_import_year').val('');
                alert("selected year is not valid to import!");
            }
        }
    });
}

//check replicate year validation
function CheckIsValidYearForReplicate(selectedBudgetYear){
    $.ajax({
        url: `/api/utilities/CheckIsValidYearForImport/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "select_year_type=" + selectedBudgetYear,
        success: function (data) {
            if(data == true){
                ReplicateBudgetFromPreviousYearData(selectedBudgetYear);              
            }else{                    
                $('#select_duplicate_budget_type').empty();
                $('#duplciateYear').val('');
                alert("selected year is not valid to replicate!");
            }
        }
    });
}

//create budget type dropdown and set as html
function CreateBudgetTypeWithYear(selectedBudgetYear){
    $.ajax({
        url: `/api/utilities/CheckBudgetWithYear/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "BudgetYear=" + selectedBudgetYear,
        success: function (data) {
            $('#select_budget_type').empty();
                                           
            $('#select_budget_type').append(`<option value="">予算時期を選択</option>`);
            //create fist half budget dropdown                    
            if(data.FirstHalfFinalize){
                $('#select_budget_type').append(`<option value="1" disabled style='color:red;'>${selectedBudgetYear} 初期予算が作成されました</option>`);
            }else if(data.FirstHalfBudget){
                $('#select_budget_type').append(`<option value="1" disabled style='color:orange;'>${selectedBudgetYear} 初期予算は確定されていません</option>`);
            }else if(!data.FirstHalfBudget){
                $('#select_budget_type').append(`<option value="1">${selectedBudgetYear} 初期予算</option>`);
            }  

            //create second half budget dropdown
            if(data.SecondHalfFinalize){
                $('#select_budget_type').append(`<option value="2" disabled style='color:red;'>${selectedBudgetYear} 下期修正予算が作成されました</option>`);
            }else if(data.SecondHalfBudget){
                $('#select_budget_type').append(`<option value="2" disabled style='color:orange;'>${selectedBudgetYear} 下期修正予算は確定されていません</option>`);
            }else if(!data.SecondHalfBudget){
                if(data.FirstHalfBudget){
                    $('#select_budget_type').append(`<option value="2">${selectedBudgetYear} 下期修正予算</option>`);
                }else{
                    $('#select_budget_type').append(`<option value="2" disabled style='color:gray;'>${selectedBudgetYear} 下期修正予算</option>`);
                }                                               
            }  
        }
    });
}

//replicate budget from selected year
function ReplicateBudgetFromPreviousYearData(selectedBudgetYear){
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
                                           
            $('#select_duplicate_budget_type').append(`<option value="">予算時期を選択</option>`);
            //create fist half budget dropdown                    
            if(data.FirstHalfFinalize){
                $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:red;'>${selectedBudgetYear} 初期予算が作成されました</option>`);
            }else if(data.FirstHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="1" disabled style='color:orange;'>${selectedBudgetYear} 初期予算は確定されていません</option>`);
            }else if(!data.FirstHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="1">${selectedBudgetYear} 初期予算</option>`);
            }  

            //create second half budget dropdown
            if(data.SecondHalfFinalize){
                $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:red;'>${selectedBudgetYear} 下期修正予算が作成されました</option>`);
            }else if(data.SecondHalfBudget){
                $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:orange;'>${selectedBudgetYear} 下期修正予算は確定されていません</option>`);
            }else if(!data.SecondHalfBudget){                         
                if(data.FirstHalfBudget){
                    $('#select_duplicate_budget_type').append(`<option value="2">${selectedBudgetYear} 下期修正予算</option>`);
                }else{
                    $('#select_duplicate_budget_type').append(`<option value="2" disabled style='color:gray;'>${selectedBudgetYear} 下期修正予算</option>`);
                }                        
            }  
        }
    });
}

//after cell change, store that cell information into hidden field.
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

//store the changed data
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

//cell wise color function
function SetColorForCells(strBackgroundColor, strTextColor, CellPosition) {
	jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
}

//cell wise color function
function SetColorForCostsCells(strBackgroundColor,strTextColor,CellPosition){
	$(jss.getCell(CellPosition)).removeClass('readonly');
    jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
    $(jss.getCell(CellPosition)).addClass('readonly');
}

//loader
function LoaderShow() {    
    $("#loading").css("display", "block");
}
function LoaderHide() {    
    $("#loading").css("display", "none");
}

//shorting columns
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
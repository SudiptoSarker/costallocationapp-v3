$(document).ready(function () {   
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

    //submit form
    $('#frm_import_year_data').submit(validate);
});

//get all finalize year list.
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

//validate replicate data. 
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        $('#replicate_from').val(year);
    }else{
        $('#replicate_from').val('');
    }
}

//budget validation and submit the import file to server.
function validate(){        
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

//loader show
function LoaderShow() {    
    $("#loading").css("display", "block");
}

//loader hide
function LoaderHide() {    
    $("#loading").css("display", "none");
}
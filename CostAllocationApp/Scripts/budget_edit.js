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
var insertedOnChangeList = [];
var previousRowDataToDetech = [];

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
var _mwCompanyFromApi = '';

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
    var count = 1;

    //get all budget year list with dropdown
    GetAllBudgetYear();

    //hide jexcel table and export budget button on page load 
    $("#jspreadsheet").hide();
    $("#export_budget").hide();  

    $('#employee_list').select2();    
    
    //sorting modal
    $(".sorting_custom_modal").css("display", "block");

    //budget year select. save/finalize button disabled if already finalized.
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
    
    //save budget edit data.
    $('#save_bedget').on('click', function () {                
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
                if (jssInsertedData[i].sectionId == '' || jssInsertedData[i].sectionId == 0 || jssInsertedData[i].departmentId == '' || jssInsertedData[i].departmentId == 0 || jssInsertedData[i].roleId == '' || jssInsertedData[i].roleId == 0 || jssInsertedData[i].companyId == '' || jssInsertedData[i].companyId == 0 || (jssInsertedData[i].unitPrice == 0 || isNaN(jssInsertedData[i].unitPrice))) {
                    storeMessage.push('不正な入力値です ' + jssInsertedData[i].employeeName);
                }
            }
        }
        if (insertedOnChangeList.length > 0) {
            for (var i = 0; i < insertedOnChangeList.length; i++) {
                if (insertedOnChangeList[i].sectionId == '' || insertedOnChangeList[i].sectionId == 0 || insertedOnChangeList[i].departmentId == '' || insertedOnChangeList[i].departmentId == 0 || insertedOnChangeList[i].roleId == '' || insertedOnChangeList[i].roleId == 0 || insertedOnChangeList[i].companyId == '' || insertedOnChangeList[i].companyId == 0 || (insertedOnChangeList[i].unitPrice == 0 || isNaN(insertedOnChangeList[i].unitPrice))) {
                    storeMessage.push('不正な入力値です ' + insertedOnChangeList[i].employeeName);
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
        LoaderShow();
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
                    if (_uniqueEmployeeIds[i].toString() == allTableData[j][42].toString()) {
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
                        if (tempArray[i][42] == tempArrayCopy[k][42]) {
                            singleRowDuplicationCount++;
                        }

                        if (singleRowDuplicationCount == 9) {
                            alert(tempArray[i][1]+'が重複しています');
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
                                                if (parseFloat(newUnitPriceList[l][16]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][11]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }

                                                //nov point
                                                if (parseFloat(newUnitPriceList[l][17]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][12]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //dec point
                                                if (parseFloat(newUnitPriceList[l][18]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][13]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jan point
                                                if (parseFloat(newUnitPriceList[l][19]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][14]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //feb point
                                                if (parseFloat(newUnitPriceList[l][20]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][15]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //mar point
                                                if (parseFloat(newUnitPriceList[l][21]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][16]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //apr point
                                                if (parseFloat(newUnitPriceList[l][22]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][17]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //may point
                                                if (parseFloat(newUnitPriceList[l][23]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][18]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jun point
                                                if (parseFloat(newUnitPriceList[l][24]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][19]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jul point
                                                if (parseFloat(newUnitPriceList[l][25]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][20]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //aug point
                                                if (parseFloat(newUnitPriceList[l][26]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][21]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //sep point
                                                if (parseFloat(newUnitPriceList[l][27]) > 0) {
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
            LoaderShow();
            $.ajax({
                url: `/api/utilities/FinalizeBudgetAssignment`,
                contentType: 'application/json',
                type: 'GET',
                async: true,
                dataType: 'json',
                data: "year=" + selected_year_for_finalize_budget,
                success: function (data) {        
                    LoaderHide();
                    alert("保存されました");                                  
                }
            });
        }       
    });

    //show budget data and jexcel table
    $(document).on('click', '#show_budget_list ', function () {            
        var assignmentYear = $('#budget_years').val();        
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }     
        LoaderShow();                
        ClearnAllJexcelData();                            
        ShowBedgetResults(assignmentYear,'show');
    });

    //refresh budget table data 
    $(document).on('click', '#undo_edited_budget ', function () {
        LoaderShow();
        ClearnAllJexcelData();
        var assignmentYear = $('#budget_years').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }        
        ShowBedgetResults(assignmentYear,'show');        
    });

    var chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#save_notifications').append(`<li>${name} ${message}</li>`);
    };

    //sorting modal close
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

    /***************************\                           
     Add Employee through Modal                      
    \***************************/    
    $('.employee_add').on('click',function(){
        AddEmployee();
    });

    //export button clicked for download budget data
    $('#export_budget').on('click', function () {    
        ExportApprovalHistory();    
    });
});   

//clear all defined variables
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
    insertedOnChangeList = [];
    previousRowDataToDetech = [];
    allEmployeeName = [];
    allEmployeeName1 = [];
    cellwiseColorCode = [];
    cellwiseColorCodeForInsert = [];
    changeCount = 0;
    newRowChangeEventFlag = false;
    deletedExistingRowIds = [];
}

//loader show
function LoaderShow() {   
    $("#jspreadsheet").hide();   
    $("#export_budget").hide();   
    $("#loading").css("display", "block");
}

//loader hide
function LoaderHide() {    
    $("#jspreadsheet").show();  
    $("#export_budget").show();   
    $("#loading").css("display", "none");
}

//show budget data in jexcel table, filtered by year
function ShowBudgetJexcel(){
    var sectionsForJexcel = [];
    var departmentsForJexcel = [];
    var inchargesForJexcel = [];
    var rolesForJexcel = [];
    var explanationsForJexcel = [];
    var companiesForJexcel = [];
    var gradesForJexcel = [];

    $.ajax({
        url: `/api/utilities/GetMwCompany`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            _mwCompanyFromApi = data;
        }
    });

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
    var totalCal_year = $('#budget_years').val();       
    $.ajax({
        url: `/api/utilities/GetTotalManMonthAndCostForBudgetEdit`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "yearWithType=" + totalCal_year,
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
            tableWidth: w-280+ "px",
            tableHeight: (h-150) + "px",           
            minDimensions: [6, 10],
            columnSorting: false,
            oninsertrow: newRowInserted,            

            nestedHeaders:[
                [
                    {
                        title: '',
                        colspan: '15',
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
                        title: '',
                        colspan: '5',
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
                { title: "要員", type: "text", name: "EmployeeName", width: 150 },
                { title: "注記", type: "text", name: "Remarks", width: 60 },
                { title: "区分", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },
                { title: "部署", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },
                { title: "担当作業", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100 },
                { title: "役割", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60 },
                { title: "説明", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
                { title: "会社", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100 },
                { 
                    title: "グレード", 
                    type: "dropdown", 
                    source: gradesForJexcel, 
                    name: "GradeId", 
                    width: 60,
                    filter: (instance, cell, c, r, source) => {
                        
                        let row = parseInt(r);
                        let column = parseInt(c) - 1;
                        
                        var value1 = instance.jexcel.getValueFromCoords(column, row);
                        if (parseInt(value1) != _mwCompanyFromApi.Id) {
                            return [];
                        }
                        else {
                            return gradesForJexcel;
                        }
                    },
                },
                { title: "単価", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },
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
                //cost
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
                    width: 75
                },
                {
                    title: "11月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "NovTotal",
                    width: 75
                },
                {
                    title: "12月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "DecTotal",
                    width: 75
                },
                {
                    title: "1月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JanTotal",
                    width: 75
                },
                {
                    title: "2月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "FebTotal",
                    width: 75
                },
                {
                    title: "3月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "MarTotal",
                    width: 75
                },
                {
                    title: "4月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "AprTotal",
                    width: 75
                },
                {
                    title: "5月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "MayTotal",
                    width: 75
                },
                {
                    title: "6月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JunTotal",
                    width: 75
                },
                {
                    title: "7月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "JulTotal",
                    width: 75
                },
                {
                    title: "8月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "AugTotal",
                    width: 75
                },
                {
                    title: "9月",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "SepTotal",
                    width: 75
                },
                {
                    title: "実績・見通し",
                    type: "decimal",
                    readOnly: true,
                    mask: "#,##0",
                    name: "TotalCost",
                    width: 75
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
                { title: "RowType", type: 'hidden', name: "RowType" }, 




            ],
            minDimensions: [6, 10],
            columnSorting: false,
            onbeforechange: function (instance, cell, x, y, value) {
                var retrivedData = retrivedObject(jss.getRowData(y));

                if (x == jssTableDefinition.section.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                    

                }
                if (x == jssTableDefinition.department.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                }
                if (x == jssTableDefinition.incharge.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                }
                if (x == jssTableDefinition.role.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                }
                if (x == jssTableDefinition.explanation.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                }
                if (x == jssTableDefinition.company.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                }
                if (x == jssTableDefinition.unitPrice.index) {
                    var flag = true;
                    if (previousRowDataToDetech.length > 0) {
                        for (var i = 0; i < previousRowDataToDetech.length; i++) {
                            if (parseInt(previousRowDataToDetech[i].assignementId) == parseInt(retrivedData.assignementId)) {
                                flag == false;
                            }
                        }
                        if (flag) {
                            previousRowDataToDetech.push(retrivedData);
                        }
                    }
                    else {
                        previousRowDataToDetech.push(retrivedData);
                    }
                }

                if (x == jssTableDefinition.octM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.novM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.decM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.janM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.febM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.marM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.aprM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.mayM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.junM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.julM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.augM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
                if (x == jssTableDefinition.sepM.index) {
                    beforeChangedValue = jss.getValueFromCoords(x, y);
                }
            },                
            onchange: function (instance, cell, x, y, value) {
                var checkId = jss.getValueFromCoords(jssTableDefinition.assignmentId.index, y);
                var employeeId = jss.getValueFromCoords(jssTableDefinition.employeeId.index, y);
                
                if (checkId == null || checkId == '' || checkId == undefined) {
                    //get data for new employee
                    var retrivedData = retrivedObject(jss.getRowData(y));
                    retrivedData.assignmentId = "new-" + newRowCount;

                    jssInsertedData.push(retrivedData);
                    newRowCount++;
            

                    jss.setValueFromCoords(jssTableDefinition.assignmentId.index, y, retrivedData.assignmentId, false);
                    jss.setValueFromCoords(jssTableDefinition.octT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.octM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.novT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.novM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.decT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.decM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.janT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.janM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.febT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.febM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.marT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.marM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.aprT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.aprM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.mayT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.mayM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.junT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.junM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.julT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.julM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.augT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.augM.cellName}${parseInt(y) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.sepT.index, y, `=${jssTableDefinition.unitPrice.cellName}${parseInt(y) + 1}*${jssTableDefinition.sepM.cellName}${parseInt(y) + 1}`, false);
                }
                else {
                    //get data for existing employee
                    var retrivedData = retrivedObject(jss.getRowData(y));
                    var retrivedObjectForOnChangeInsert = retrivedObjectForInsertOnChange(jss.getRowData(y));
                    if (retrivedData.assignmentId.toString().includes('new')) {

                        if (x == jssTableDefinition.grade.index) {                              
                            var rowNumber = parseInt(y) + 1;
                            var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                            var companyName = element[0].cells[9].innerText;

                            var cellValue = jss.getValueFromCoords(jssTableDefinition.company.index, y);

                            if (companyName.toLowerCase() == 'mw') {
                                $.ajax({
                                    url: '/api/Salaries?salaryGradeId=' + value,
                                    contentType: 'application/json',
                                    type: 'GET',
                                    async: false,
                                    dataType: 'json',
                                    success: function (salary) {
                                        jss.setValueFromCoords(jssTableDefinition.unitPrice.index, parseInt(y), salary.SalaryLowPoint, false);
                                        retrivedData = retrivedObject(jss.getRowData(y));
                                    }
                                });
                            }
                        }
                        updateArrayForInsert(jssInsertedData, retrivedData, x,y, cell, value, beforeChangedValue);
                    }
                    else {
                        var dataCheck = jssUpdatedData.filter(d => d.assignmentId == retrivedData.assignmentId);
                        var dataCheckForInsertOnChange = insertedOnChangeList.filter(d => d.assignmentId == retrivedData.assignmentId);

                        if (x == jssTableDefinition.remarks.index) {                        
                            if (dataCheckForInsertOnChange.length == 0) {                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {                                
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }                                                    
                        }

                        if (x == jssTableDefinition.section.index) {                              
                            if (dataCheckForInsertOnChange.length == 0) {                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {                                
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }                            
                        }                    

                        if (x == jssTableDefinition.department.index) {                             
                            if (dataCheckForInsertOnChange.length == 0) {
                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }                            
                        }
                        
                        if (x == jssTableDefinition.incharge.index) {                            
                            if (dataCheckForInsertOnChange.length == 0) {                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }
                        }

                        if (x == jssTableDefinition.role.index) {                              
                            if (dataCheckForInsertOnChange.length == 0) {                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {                                
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }                        
                        }

                        if (x == jssTableDefinition.explanation.index) {                        
                            if (dataCheckForInsertOnChange.length == 0) {                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {                                
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }                            
                        }

                        if (x == jssTableDefinition.company.index) {                                
                            var rowNumber = parseInt(y) + 1;
                            if (parseInt(value) !== _mwCompanyFromApi.Id) {
                                var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                                element[0].cells[10].innerText = '';
                                $(jss.getCell("J" + rowNumber)).addClass('readonly');
                            }
                            else {
                                $(jss.getCell("J" + rowNumber)).removeClass('readonly');                                
                            }

                            if (dataCheckForInsertOnChange.length == 0) {                                
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }
                        }

                        if (x == jssTableDefinition.grade.index) {                                  
                            var rowNumber = parseInt(y) + 1;
                            var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                            var companyName = element[0].cells[9].innerText;

                            var cellValue = jss.getValueFromCoords(jssTableDefinition.company.index, y);

                            if (companyName.toLowerCase() == 'mw') {
                                $.ajax({
                                    url: '/api/Salaries?salaryGradeId=' + value,
                                    contentType: 'application/json',
                                    type: 'GET',
                                    async: false,
                                    dataType: 'json',
                                    success: function (salary) {
                                        jss.setValueFromCoords(jssTableDefinition.unitPrice.index, parseInt(y), salary.SalaryLowPoint, false);
                                    }
                                });
                            }
                            retrivedObjectForOnChangeInsert = retrivedObjectForInsertOnChange(jss.getRowData(y));
                            dataCheckForInsertOnChange = insertedOnChangeList.filter(d => d.assignmentId == retrivedData.assignmentId);

                            if (dataCheckForInsertOnChange.length == 0) {
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }
                        }

                        if (x == jssTableDefinition.unitPrice.index) {                                 
                            if (dataCheckForInsertOnChange.length == 0) {
                                insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                            }
                            else {
                                updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                            }
                        }

                        if (x == jssTableDefinition.octM.index) {                        
                            var octSum = 0;
                            var octPointsSum = 0;
                            var octCostSum = 0;

                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.octM.index] != "" && dataValue[jssTableDefinition.octM.index] != null && dataValue[jssTableDefinition.octM.index] != undefined) {
                                    var octPointPerRow = 0.0;
                                    octPointPerRow = parseFloat(dataValue[jssTableDefinition.octM.index]).toFixed(1);
                                    octPointsSum += parseFloat(octPointPerRow);
                                    octCostSum = parseFloat(octCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.octM.index]);     
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    octSum += parseFloat(parseFloat(dataValue[jssTableDefinition.octM.index]));
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

                        if (x == jssTableDefinition.novM.index) {  
                            var novPointsSum = 0;
                            var novCostSum = 0;

                            var novSum = 0;

                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.novM.index] != "" && dataValue[jssTableDefinition.novM.index] != null && dataValue[jssTableDefinition.novM.index] != undefined) {
                                    var novPointPerRow = 0.0;
                                    novPointPerRow = parseFloat(dataValue[jssTableDefinition.novM.index]).toFixed(1);
                                    novPointsSum += parseFloat(novPointPerRow);   

                                    novCostSum = parseFloat(novCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.novM.index]);   
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    novSum += parseFloat(dataValue[jssTableDefinition.novM.index]);
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

                        if (x == jssTableDefinition.decM.index) {
                            var decPointsSum = 0;
                            var decCostSum = 0;

                            var decSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.decM.index] != "" && dataValue[jssTableDefinition.decM.index] != null && dataValue[jssTableDefinition.decM.index] != undefined){
                                    var decPointPerRow = 0.0;
                                    decPointPerRow = parseFloat(dataValue[jssTableDefinition.decM.index]).toFixed(1);
                                    decPointsSum += parseFloat(decPointPerRow); 

                                    decCostSum = parseFloat(decCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.decM.index]);   
                                } 

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    decSum += parseFloat(dataValue[jssTableDefinition.decM.index]);
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

                        if (x == jssTableDefinition.janM.index) {        
                            var janPointsSum = 0;
                            var janCostSum = 0;                

                            var janSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.janM.index] != "" && dataValue[jssTableDefinition.janM.index] != null && dataValue[jssTableDefinition.janM.index] != undefined){
                                    var janPointPerRow = 0.0;
                                    janPointPerRow = parseFloat(dataValue[jssTableDefinition.janM.index]).toFixed(1);
                                    janPointsSum += parseFloat(janPointPerRow); 

                                    janCostSum = parseFloat(janCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.janM.index]);   
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    janSum += parseFloat(dataValue[jssTableDefinition.janM.index]);
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

                        if (x == jssTableDefinition.febM.index) {
                            var febPointsSum = 0;
                            var febCostSum = 0;

                            var febSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.febM.index] != "" && dataValue[jssTableDefinition.febM.index] != null && dataValue[jssTableDefinition.febM.index] != undefined){
                                    var febPointPerRow = 0.0;
                                    febPointPerRow = parseFloat(dataValue[jssTableDefinition.febM.index]).toFixed(1);
                                    febPointsSum += parseFloat(febPointPerRow); 

                                    febCostSum = parseFloat(febCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.febM.index]);   
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    febSum += parseFloat(dataValue[jssTableDefinition.febM.index]);
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

                        if (x == jssTableDefinition.marM.index) {  
                            var marPointsSum = 0;
                            var marCostSum = 0;

                            var marSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.marM.index] != "" && dataValue[jssTableDefinition.marM.index] != null && dataValue[jssTableDefinition.marM.index] != undefined){
                                    var marPointPerRow = 0.0;
                                    marPointPerRow = parseFloat(dataValue[jssTableDefinition.marM.index]).toFixed(1);
                                    marPointsSum += parseFloat(marPointPerRow); 

                                    marCostSum = parseFloat(marCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.marM.index]);   
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    marSum += parseFloat(dataValue[jssTableDefinition.marM.index]);
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

                        if (x == jssTableDefinition.aprM.index) {  
                            var aprPointsSum = 0;
                            var aprCostSum = 0;

                            var aprSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.aprM.index] != "" && dataValue[jssTableDefinition.aprM.index] != null && dataValue[jssTableDefinition.aprM.index] != undefined){
                                    var aprPointPerRow = 0.0;
                                    aprPointPerRow = parseFloat(dataValue[jssTableDefinition.aprM.index]).toFixed(1);
                                    aprPointsSum += parseFloat(aprPointPerRow); 

                                    aprCostSum = parseFloat(aprCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.aprM.index]);
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    aprSum += parseFloat(dataValue[jssTableDefinition.aprM.index]);
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

                        if (x == jssTableDefinition.mayM.index) {   
                            var mayPointsSum = 0;
                            var mayCostSum = 0;

                            var maySum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.mayM.index] != "" && dataValue[jssTableDefinition.mayM.index] != null && dataValue[jssTableDefinition.mayM.index] != undefined){
                                    var mayPointPerRow = 0.0;
                                    mayPointPerRow = parseFloat(dataValue[jssTableDefinition.mayM.index]).toFixed(1);
                                    mayPointsSum += parseFloat(mayPointPerRow); 

                                    mayCostSum = parseFloat(mayCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.mayM.index]); 
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    maySum += parseFloat(dataValue[jssTableDefinition.mayM.index]);
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

                        if (x == jssTableDefinition.junM.index) {      
                            var junPointsSum = 0;
                            var junCostSum = 0;

                            var junSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.junM.index] != "" && dataValue[jssTableDefinition.junM.index] != null && dataValue[jssTableDefinition.junM.index] != undefined){
                                    var junPointPerRow = 0.0;
                                    junPointPerRow = parseFloat(dataValue[jssTableDefinition.junM.index]).toFixed(1);
                                    junPointsSum += parseFloat(junPointPerRow); 

                                    junCostSum = parseFloat(junCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.junM.index]); 
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    junSum += parseFloat(dataValue[jssTableDefinition.junM.index]);
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

                        if (x == jssTableDefinition.julM.index) {    
                            var julPointsSum = 0;
                            var julCostSum = 0;                    

                            var julSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.julM.index] != "" && dataValue[jssTableDefinition.julM.index] != null && dataValue[jssTableDefinition.julM.index] != undefined){
                                    var julPointPerRow = 0.0;
                                    julPointPerRow = parseFloat(dataValue[jssTableDefinition.julM.index]).toFixed(1);
                                    julPointsSum += parseFloat(julPointPerRow); 

                                    julCostSum = parseFloat(julCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.julM.index]); 
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    julSum += parseFloat(dataValue[jssTableDefinition.julM.index]);
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

                        if (x == jssTableDefinition.augM.index) {  
                            var augPointsSum = 0;
                            var augCostSum = 0;                      

                            var augSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.augM.index] != "" && dataValue[jssTableDefinition.augM.index] != null && dataValue[jssTableDefinition.augM.index] != undefined){
                                    var augPointPerRow = 0.0;
                                    augPointPerRow = parseFloat(dataValue[jssTableDefinition.augM.index]).toFixed(1);
                                    augPointsSum += parseFloat(augPointPerRow); 

                                    augCostSum = parseFloat(augCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.augM.index]); 
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    augSum += parseFloat(dataValue[jssTableDefinition.augM.index]);
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

                        if (x == jssTableDefinition.sepM.index) {    
                            var sepPointsSum = 0;
                            var sepCostSum = 0;                    

                            var sepSum = 0;
                            $.each(jss.getData(), (index, dataValue) => {
                                if (dataValue[jssTableDefinition.sepM.index] != "" && dataValue[jssTableDefinition.sepM.index] != null && dataValue[jssTableDefinition.sepM.index] != undefined){
                                    var sepPointPerRow = 0.0;
                                    sepPointPerRow = parseFloat(dataValue[jssTableDefinition.sepM.index]).toFixed(1);
                                    sepPointsSum += parseFloat(sepPointPerRow); 

                                    sepCostSum = parseFloat(sepCostSum) + parseFloat(dataValue[jssTableDefinition.unitPrice.index]) * parseFloat(dataValue[jssTableDefinition.sepM.index]); 
                                }

                                if (dataValue[jssTableDefinition.employeeId.index].toString() == employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                                    sepSum += parseFloat(dataValue[jssTableDefinition.sepM.index]);
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
                var user_role = $("#user_role").val();
                if(is_finalize==1 || user_role=='visitor'){
                    return items;
                }else{
                    items.push({
                        title: '要員を追加 (Add Emp)',
                        onclick: function () {
                            obj.insertRow(1, parseInt(y));
                            var insertedRowNumber = parseInt(obj.getSelectedRows(true)) + 2;
                            
                            setTimeout(function () {
                                jss.setValueFromCoords(jssTableDefinition.bcyr.index, (insertedRowNumber - 1), true, false);
        
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
                            var assignementId = jss.getValueFromCoords(jssTableDefinition.assignmentId.index, y);
                            assignmentIds.push(assignementId);
                            var name = jss.getValueFromCoords(jssTableDefinition.employeeName.index, y);   
                            
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
                                            alert("正常に処理されました");
    
                                        }else{
                                            alert("操作が失敗しました");
                                        }
                                    }
                                });
                            }else{
                                alert(name +" は保存されていないため、削除できません。")  
                            }                         
                        }
                    });
                }

                
    
                return items;
            },
        });
        LoaderHide();
    }else{
        $('#jspreadsheet').html("今年度の予算はない！");
    }

    $("#save_bedget").css("display", "block");
    $("#undo_edited_budget").css("display", "block");
    $("#budget_finalize").css("display", "block");

    //create a row for search in each column
    jss.deleteColumn(53, 23);
    
    //sort arrow on load: employee
    var employee_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)');
    employee_asc_arow.addClass('arrow-down');
    var employee_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    employee_header.css('position', 'sticky');
    employee_header.css('top', '0px');

    //sort arrow on load: section
    var section_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(5)');
    section_asc_arow.addClass('arrow-down');
    var section_header = $('.jexcel > thead > tr:nth-of-type(4) > td');
    section_header.css('position', 'sticky');
    section_header.css('top', '0px');
    
    //sort arrow on load: department
    var dept_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(6)');
    dept_asc_arow.addClass('arrow-down');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');

    //sort arrow on load: grade
    var grade_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)');
    grade_asc_arow.addClass('arrow-down');

    //sort arrow on load: unit
    var unit_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)');
    unit_asc_arow.addClass('arrow-down');

    //sort employee
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)').on('click', function () {           
        $('.employee_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.employee_sorting').fadeIn("slow");
    });

    // //section column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(5)').on('click', function () {  
        $('.section_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.section_sorting').fadeIn("slow");
    });
    
    //department column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(6)').on('click', function () {     
        $('.department_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.department_sorting').fadeIn("slow");
    });    
    //incharge column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)').on('click', function () {  
        $('.incharge_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.incharge_sorting').fadeIn("slow");
    });
    //role column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)').on('click', function () {         
        $('.role_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.role_sorting').fadeIn("slow");
    });
    //explanation column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)').on('click', function () {    
        $('.explanation_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.explanation_sorting').fadeIn("slow");
    });
    //company column
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)').on('click', function () {     
        $('.company_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.company_sorting').fadeIn("slow");
    });
    //grade column sorting
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)').on('click', function () {        
        $('.grade_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.grade_sorting').fadeIn("slow");
    });
        //unit price column sorting
    $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)').on('click', function () { 
        $('.unit_sorting').css('display', 'block');        
        $("#hider").fadeIn("slow");
        $('.unit_sorting').fadeIn("slow");
    });         
    //$("#export_budget").show();  
}

//show the budget results on jexcel table.
function ShowBedgetResults(year,showType) {        
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

    
    $.ajax({
        url: `/api/utilities/GetAllBudgetData`,
        contentType: 'application/json',
        type: 'GET',
        async: true,
        dataType: 'json',
        data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            _retriveddata = data;
            ShowBudgetJexcel();                
            LoaderHide();
            if(showType=='save'){
                alert("保存されました");
            }
        }
    });        
}

var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
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
    array[index].duplicateFrom = retrivedData.duplicateFrom;
    array[index].duplicateCount = retrivedData.duplicateCount;
    array[index].roleChanged = retrivedData.roleChanged;
    array[index].unitPriceChanged = retrivedData.unitPriceChanged;
}

//update the arry with the assignment data for new rows 
function updateArrayForInsertOnChange(array, retrivedData) {
    var index = insertedOnChangeList.findIndex(d => d.assignmentId == retrivedData.assignmentId);

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
    array[index].duplicateFrom = retrivedData.duplicateFrom;
    array[index].duplicateCount = retrivedData.duplicateCount;
    array[index].roleChanged = retrivedData.roleChanged;
    array[index].unitPriceChanged = retrivedData.unitPriceChanged;
    array[index].bcyrCell =  retrivedData.bcyrCell;
}

//create new with the assignment data for new rows 
function retrivedObjectForInsertOnChange(rowData) {

    var allData = jss.getData();
    var _unitPriceChanged = 0;
    var _roleChanged = 0;
    var _duplicateCount = 1;
    for (let x of allData) {
        if (parseInt(x[jssTableDefinition.employeeId.index]) == parseInt(rowData[jssTableDefinition.employeeId.index])) {
            _duplicateCount++;

        }
    }

    for (var i = 0; i < previousRowDataToDetech.length; i++) {
        if (previousRowDataToDetech[i].assignmentId == rowData[jssTableDefinition.assignmentId.index]) {

            if (previousRowDataToDetech[i].sectionId != rowData[jssTableDefinition.section.index]) {
                _roleChanged = 1;
            }
            if (previousRowDataToDetech[i].departmentId != rowData[jssTableDefinition.department.index]) {
                _roleChanged = 1;
            }
            if (previousRowDataToDetech[i].inchargeId != rowData[jssTableDefinition.incharge.index]) {
                _roleChanged = 1;
            }
            if (previousRowDataToDetech[i].roleId != rowData[jssTableDefinition.role.index]) {
                _roleChanged = 1;
            }
            if (previousRowDataToDetech[i].companyId != rowData[jssTableDefinition.company.index]) {
                _roleChanged = 1;
            }
            if (previousRowDataToDetech[i].gradeId != rowData[jssTableDefinition.grade.index]) {
                _unitPriceChanged = 1;
            }
            if (previousRowDataToDetech[i].unitPrice != rowData[jssTableDefinition.unitPrice.index]) {
                _unitPriceChanged = 1;
            }
        }
    }

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
        octPoint: 0,
        novPoint: 0,
        decPoint: 0,
        janPoint: 0,
        febPoint: 0,
        marPoint: 0,
        aprPoint: 0,
        mayPoint: 0,
        junPoint: 0,
        julPoint: 0,
        augPoint: 0,
        sepPoint: 0,
        year: document.getElementById('selected_budget_year').value,

        bcyr: false,
        bCYRCell: '',
        isActive: rowData[jssTableDefinition.isActive.index],
        bCYRApproved: rowData[jssTableDefinition.bcyrApproved.index],
        bCYRCellApproved: rowData[jssTableDefinition.bcyrCellApproved.index],
        isApproved: rowData[jssTableDefinition.isApproved.index],
        bCYRCellPending: rowData[jssTableDefinition.bcyrCellPending.index],
        isRowPending: rowData[jssTableDefinition.isRowPending.index],
        isDeletePending: rowData[jssTableDefinition.isDeletePending.index],
        rowType: rowData[jssTableDefinition.rowType.index],

        duplicateFrom: rowData[jssTableDefinition.assignmentId.index],
        duplicateCount: _duplicateCount,
        roleChanged: _roleChanged,
        unitPriceChanged: _unitPriceChanged,
    };
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
    if (x == jssTableDefinition.remarks.index) {
        array[index].remarks = retrivedData.remarks;
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }
    }
    if (x == jssTableDefinition.explanation.index) {
        array[index].explanationId = retrivedData.explanationId;
        if (!newRowChangeEventFlag) {
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }
    }
    if (x == jssTableDefinition.octM.index) {
        var octSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                octSum += parseFloat(dataValue[jssTableDefinition.octM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index,y);
            currentValue += ',new-x_'+x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }


    }

    if (x == jssTableDefinition.novM.index) {
        var novSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                novSum += parseFloat(dataValue[jssTableDefinition.novM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }


    }
    if (x == jssTableDefinition.decM.index) {
        var decSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                decSum += parseFloat(dataValue[jssTableDefinition.decM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.janM.index) {
        var janSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                janSum += parseFloat(dataValue[jssTableDefinition.janM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.febM.index) {
        var febSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                febSum += parseFloat(dataValue[jssTableDefinition.febM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }

    if (x == jssTableDefinition.marM.index) {
        var marSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                marSum += parseFloat(dataValue[jssTableDefinition.marM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.aprM.index) {
        var aprSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                aprSum += parseFloat(dataValue[jssTableDefinition.aprM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.mayM.index) {
        var maySum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                maySum += parseFloat(dataValue[jssTableDefinition.mayM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.junM.index) {
        var junSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                junSum += parseFloat(dataValue[jssTableDefinition.junM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.julM.index) {
        var julSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                julSum += parseFloat(dataValue[jssTableDefinition.julM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.augM.index) {
        var augSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                augSum += parseFloat(dataValue[jssTableDefinition.augM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
    if (x == jssTableDefinition.sepM.index) {
        var sepSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[jssTableDefinition.employeeId.index].toString() == retrivedData.employeeId.toString() && dataValue[jssTableDefinition.isActive.index] == true) {
                sepSum += parseFloat(dataValue[jssTableDefinition.sepM.index]);
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
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }

    }
        
    array[index].year = retrivedData.year;
    array[index].bcyr = retrivedData.bcyr;   

    array[index].duplicateFrom = retrivedData.duplicateFrom;
    array[index].duplicateCount = retrivedData.duplicateCount;
    array[index].roleChanged = retrivedData.roleChanged;
    array[index].unitPriceChanged = retrivedData.unitPriceChanged;
    
    if(array[index].bCYRCell.length <= retrivedData.bCYRCell.length){
        array[index].bCYRCell= retrivedData.bCYRCell;  
    }            
}

//set value and get value from the objects
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
        year: document.getElementById('selected_budget_year').value,

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

//new employee add
$('.employee_registration').on('click', function () {
    InsertEmployee();
});
    
//new employee add funcitons
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
                    jss.setValueFromCoords(42, globalY, result, false);
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

//store add employee's information to jexcel for new budget
function AddEmployee() {
    var employeeId = $('#employee_list').val();
    var employeeName = $('#employee_list').find("option:selected").text();
    jss.setValueFromCoords(1, globalY, employeeName, false);
    jss.setValueFromCoords(42, globalY, employeeId, false);
    $('#jexcel_add_employee_modal').modal('hide');
}

//update/add budgets information
function UpdateBudget() {    
    $("#update_forecast").modal("hide");        
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

    var isShowResults = false;

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
                    isShowResults = true;
                    
                    $("#timeStamp_ForUpdateData").val(data);
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been updated by ', userName);
                    });                        
                }
            });
            jssUpdatedData = [];
        
        
    }
    else {            
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
                isShowResults = true;                                                               
                $("#timeStamp_ForUpdateData").val(data);
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been updated by ', userName);
                });            
            }
        });
        jssInsertedData = [];
        newRowCount = 1;

    }

    
    if (insertedOnChangeList.length > 0) {
        var add_employee_budget_year = $("#budget_years").val();
        $.ajax({
            url: `/api/utilities/ExcelAssignmentBudget/`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: insertedOnChangeList, HistoryName: timestamp, CellInfo: '', YearWithBudgetType: add_employee_budget_year }),
            success: function (data) {
                isShowResults = true;                    
                $("#timeStamp_ForUpdateData").val(data);
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been updated by ', userName);
                });                    
            }
        });

        insertedOnChangeList = [];
        previousRowDataToDetech = [];
    }

    if (deletedExistingRowIds.length > 0) {
        var year = $("#budget_years").val();
        $.ajax({
            url: `/api/utilities/ExcelDeleteAssignment/`,
            contentType: 'application/json',
            type: 'DELETE',
            async: false,
            dataType: 'json',                
            data: JSON.stringify({ ForecastUpdateHistoryDtos: "", HistoryName: timestamp + promptValue,TimeStampId: update_timeStampId,DeletedRowIds: deletedExistingRowIds,Year:year}),
            success: function (data) {                
                isShowResults = true;

                $("#timeStamp_ForUpdateData").val(data);
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been updated by ', userName);
                });                    
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

    if(isShowResults){
        $("#export_budget").show();
        var year = $("#budget_years").val();            
        ShowBedgetResults(year,'save');

        $("#save_modal_header").html("年度データー(Emp. Assignments)");
        $("#back_button_show").css("display", "block");
        $("#save_btn_modal").css("display", "block");
        $("#close_save_modal").css("display", "none");
    }else{            
        LoaderHide();
        $("#header_show").html("");
        $("#update_forecast").modal("show");
        $("#save_modal_header").html("変更されていないので、保存できません");
        $("#back_button_show").css("display", "none");
        $("#save_btn_modal").css("display", "none");

        alert("保存するデータがありません!");
    }
}

//get all the forecasted year. and build the year dropdown.
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

//submitting the export form to server side. 
function ExportApprovalHistory(){
    var budgetYearWithType = $('#budget_years').val();

    var arrBudgetYear = budgetYearWithType.split("_");

    $("#hid_budget_year").val(arrBudgetYear[0]);
    $("#hid_budget_type").val(arrBudgetYear[1]);        

    $('#frmExportBudget').submit();
}

//sorting icons
function ShowAllSortingAscIcon(){
    //sort arrow on load: employee
    var employee_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(3)');
    employee_asc_arow.addClass('arrow-down');
    var employee_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    employee_header.css('position', 'sticky');
    employee_header.css('top', '0px');
    $('#search_p_asc').css('background-color', 'lightsteelblue');
    $('#search_p_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: section
    var section_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(5)');
    section_asc_arow.addClass('arrow-down');
    var section_header = $('.jexcel > thead > tr:nth-of-type(4) > td');
    section_header.css('position', 'sticky');
    section_header.css('top', '0px');
    $('#search_section_asc').css('background-color', 'lightsteelblue');
    $('#search_section_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: department
    var dept_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(6)');
    dept_asc_arow.addClass('arrow-down');
    $('#search_department_asc').css('background-color', 'lightsteelblue');
    $('#search_department_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');
    $('#search_incharge_asc').css('background-color', 'lightsteelblue');
    $('#search_incharge_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');
    $('#search_role_asc').css('background-color', 'lightsteelblue');
    $('#search_role_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');
    $('#search_explanation_asc').css('background-color', 'lightsteelblue');
    $('#search_explanation_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');
    $('#search_company_asc').css('background-color', 'lightsteelblue');
    $('#search_company_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: grade
    var grade_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)');
    grade_asc_arow.addClass('arrow-down');
    $('#search_grade_asc').css('background-color', 'lightsteelblue');
    $('#search_grade_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: unit
    var unit_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)');
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
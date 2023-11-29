$(document).ready(function(){    
    $(".sorting_custom_modal").css("display", "block");
    
    var isShowResults = false;
});

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

function LoaderShow() {
    //$("#forecast_table_wrapper").css("display", "none");
    $("#jspreadsheet").hide();  
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $("#jspreadsheet").show();  
    //$("#forecast_table_wrapper").css("display", "block");
    $("#loading").css("display", "none");
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

    $('#update_forecast_history').on('click', function () {
        var storeMessage = [];
        var _duplicateFlag = false;
        var _employeeIds = [];
        var _uniqueEmployeeIds = [];
        var employeeCount = 0;
        var rowCount = 0;
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
                if (jssInsertedData[i].sectionId == '' || jssInsertedData[i].sectionId == 0 || jssInsertedData[i].departmentId == '' || jssInsertedData[i].departmentId == 0 ||jssInsertedData[i].companyId == '' ||jssInsertedData[i].companyId == 0 || (jssInsertedData[i].unitPrice == 0 || isNaN(jssInsertedData[i].unitPrice))) {
                    storeMessage.push('invalid input for ' + jssInsertedData[i].employeeName);
                }
            }
        }

        if (jssUpdatedData.length > 0) {

            var tempUpdateData = [];
            const _distinctAssignmentIds = [...new Set(jssUpdatedData.map((item) => item.assignmentId))];
            var _allData = jss.getData();
            for (var i = 0; i < _distinctAssignmentIds.length; i++) {
                for (var j = 0; j < _allData.length; j++) {
                    if (parseInt(_distinctAssignmentIds[i]) == parseInt(_allData[j][0])) {
                        tempUpdateData.push({
                            assignmentId: _allData[j][jssTableDefinition.assignmentId.index],
                            employeeName: _allData[j][jssTableDefinition.employeeName.index],
                            remarks: _allData[j][jssTableDefinition.remarks.index],
                            sectionId: _allData[j][jssTableDefinition.section.index],
                            departmentId: _allData[j][jssTableDefinition.department.index],
                            inchargeId: _allData[j][jssTableDefinition.incharge.index],
                            roleId: _allData[j][jssTableDefinition.role.index],
                            explanationId: _allData[j][jssTableDefinition.explanation.index],
                            companyId: _allData[j][jssTableDefinition.company.index],
                            gradeId: _allData[j][jssTableDefinition.grade.index],
                            unitPrice: _allData[j][jssTableDefinition.unitPrice.index],
                            octPoint: _allData[j][jssTableDefinition.octM.index],
                            novPoint: _allData[j][jssTableDefinition.novM.index],
                            decPoint: _allData[j][jssTableDefinition.decM.index],
                            janPoint: _allData[j][jssTableDefinition.janM.index],
                            febPoint: _allData[j][jssTableDefinition.febM.index],
                            marPoint: _allData[j][jssTableDefinition.marM.index],
                            aprPoint: _allData[j][jssTableDefinition.aprM.index],
                            mayPoint: _allData[j][jssTableDefinition.mayM.index],
                            junPoint: _allData[j][jssTableDefinition.junM.index],
                            julPoint: _allData[j][jssTableDefinition.julM.index],
                            augPoint: _allData[j][jssTableDefinition.augM.index],
                            sepPoint: _allData[j][jssTableDefinition.sepM.index],
                            bCYRApproved: _allData[j][jssTableDefinition.bcyrApproved.index],
                            bCYRCell: _allData[j][jssTableDefinition.bcyrCell.index],
                            bCYRCellApproved: _allData[j][jssTableDefinition.bcyrCellApproved.index],
                            bCYRCellPending: _allData[j][jssTableDefinition.bcyrCellPending.index],
                            bcyr: _allData[j][jssTableDefinition.bcyr.index],
                            employeeId: _allData[j][jssTableDefinition.employeeId.index],
                            isActive: _allData[j][jssTableDefinition.isActive.index],
                            isApproved: _allData[j][jssTableDefinition.isApproved.index],
                            isDeletePending: _allData[j][jssTableDefinition.isDeletePending.index],
                            isRowPending: _allData[j][jssTableDefinition.isRowPending.index],
                            rowType: _allData[j][jssTableDefinition.rowType.index],
                            year: $('#assignment_year_list').val(),
                        });
                    }
                }
            }

            jssUpdatedData = tempUpdateData;

            // for (var k = 0; k < jssUpdatedData.length; k++) {
            //     if (jssUpdatedData[k].sectionId == '' || jssUpdatedData[k].departmentId == '' || jssUpdatedData[k].companyId == '' || (jssUpdatedData[k].unitPrice == 0 || isNaN(jssUpdatedData[k].unitPrice))) {
            //         storeMessage.push('invalid input for ' + jssUpdatedData[k].employeeName);
            //     }
            // }            
        }

        //added by sudipto,29th nov
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
            var tempArrayCopy = [];
            for (var i = 0; i < _uniqueEmployeeIds.length; i++) {
                for (var j = 0; j < allTableData.length; j++) {
                    if (_uniqueEmployeeIds[i].toString() == allTableData[j][jssTableDefinition.employeeId.index].toString()) {
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
                        if (tempArray[i][8] == tempArrayCopy[k][8]) {
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
                            alert(tempArray[i][1] + " が重複しています");
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

        // if (insertedOnChangeList.length > 0) {                        
        //     UpdateForecast(true);
        // }
        //debugger;
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
                // checking unit price....
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
                                        if (insertedUniqueEmployeeData_unitPrice[b] == _allData[k][jssTableDefinition.isRowPending.index]) {
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
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.octM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.octM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }

                                                //nov point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.novM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.novM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //dec point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.decM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.decM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jan point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.janM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.janM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //feb point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.febM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.febM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //mar point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.marM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.marM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //apr point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.aprM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.aprM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //may point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.mayM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.mayM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jun point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.junM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.junM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jul point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.julM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.julM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //aug point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.augM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.augM.index]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //sep point
                                                if (parseFloat(newUnitPriceList[l][jssTableDefinition.sepM.index]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][jssTableDefinition.sepM.index]) > 0) {
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
                // checking role
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
                                        if (insertedUniqueEmployeeData_role[b] == _allData[k][jssTableDefinition.isRowPending.index]) {
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
                // checking both
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
                                        if (insertedUniqueEmployeeData_both[b] == _allData[k][jssTableDefinition.isRowPending.index]) {
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
                                                    jss.setStyle(`B${_countNumber + 1}`, "background-color", "red");
                                                    jss.setStyle(`B${_countNumber + 1}`, "color", "black");
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
                                                    jss.setStyle(`B${_countNumber + 1}`, "background-color", "red");
                                                    jss.setStyle(`B${_countNumber + 1}`, "color", "black");
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
        var isUpdateInsertDelete = false;

        if (jssInsertedData.length > 0 || jssUpdatedData.length > 0 || deletedExistingRowIds.length > 0 || insertedOnChangeList.length > 0) {
            isUpdateInsertDelete = true;
        } else {
            isUpdateInsertDelete = false;
        }

         //if (insertedOnChangeList.length > 0) {                        
         //    UpdateForecast(isUpdateInsertDelete);
         //}


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
        
        var isUpdate = true;
        if(isUpdateInsertDelete){            
            isUpdate = true;
            UpdateForecast(isUpdate);
        }else{
            if (confirm("変更はありません。 保存しますか?") == true) {                
                isUpdate = false;         
                UpdateForecast(isUpdate);                       
            } else {
                return false;
            }                    
            // alert("変更されていないので、保存できません 1");
            // return false;
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
    
    $(document).on('click', '#assignment_year_data ', function () { 
        ClearnAllJexcelData();
        $('#changed_cell_with_assignmentid').val("");  
        
        var assignmentYear = $('#assignment_year_list').val();
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }      
        LoaderShow();        
        ShowForecastResults(assignmentYear,'show');
        
    });
    $(document).on('click', '#cancel_forecast_history ', function () {    
        ClearnAllJexcelData();

        var assignmentYear = $('#assignment_year_list').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }

        deletedExistingRowIds = [];
        LoaderShow();            
        ShowForecastResults(assignmentYear,'show');        
    });
});

var _retriveddata = [];
var year="",employeeName="",sectionId="",inchargeId="",roleId="",companyId="",companyId="",departmentId="",explanationId="";
var _mwCompanyFromApi = '';

function ShowForecastJexcel(){
    year = $("#assignment_year_list").val();

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
    $.ajax({
        url: `/api/utilities/GetTotalCalculationForManmonthAndCost`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: "year=" + year,
        success: function (data) {
            _retriveTotal = data;           
        }
    });

    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }

    var w = window.innerWidth;
    var h = window.innerHeight;
    
    var yearHeaderTitleForPoints = "";
    var yearHeaderTitleForCosts = "";
    yearHeaderTitleForPoints = "FY"+year+" 見通し";
    yearHeaderTitleForCosts = "FY"+year+" コスト見通し";
    
    var octSumFormula = "=SUM(L3:L13)";
    var novSumFormula = "=SUM(M3:M13)";
    var octTotalPoints = "";
    octTotalPoints = "<label id='oct_total_points'>"+_retriveTotal.OctTotalMM+"</label>"

    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        filters: true,
        allowComments:true,
        tableOverflow: true,
        freezeColumns: 3,
        //defaultColWidth: 75,
        tableWidth: w-280+ "px",
        tableHeight: (h-150) + "px",           
        minDimensions: [6, 10],
        columnSorting: true,
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
                    name: "OctPoints",
                    mask: '#.##,0',
                    decimal: '.'
                },
                {
                    title: _retriveTotal.NovTotalCosts,
                    type: "decimal",
                    name: "OctPoints",
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
            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
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
                width: 60,
                filter: (instance, cell, c, r, source) => {
                    
                    let row = parseInt(r);
                    let column = parseInt(c) - 1;
                    
                    var element = $(`.jexcel > tbody > tr:nth-of-type(${row+1})`);
                    var companyName = element[0].cells[9].innerText;
                    console.log('company filter:' + companyName);
                    if (companyName.toLowerCase() !== "mw") {
                        return [];
                    }
                    else {
                        return gradesForJexcel;
                    }
                },
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
        
        onbeforechange: function (instance, cell, x, y, value) {
            //console.log("onbeforechange");
            //alert(value);
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

                    console.log(jssUpdatedData);

                    if (retrivedData.companyId != _mwCompanyFromApi.Id) {
                        retrivedData.gradeId = '';
                    }
                    
                    var isUnapprovedDeletedRow = retrivedData.isDeletePending;                                                        
                    
                    if(isUnapprovedDeletedRow){
                        var isCellAlreadyChanged = false;
                        if (!isCellAlreadyChanged) {
                            SetColorForCells("white", "black", jssTableDefinition.employeeId.cellName + (parseInt(y) + 1))                                
                        }
                    }

                    if (x == jssTableDefinition.remarks.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId+'_'+x);
                    }
                    else{                         
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.remarks.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.remarks.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.section.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            //retrivedObjectForOnChangeInsert.bcyrCell += retrivedObjectForOnChangeInsert + '_' + x;
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }
                    else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.section.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.section.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.department.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.department.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.department.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }
                    
                    if (x == jssTableDefinition.incharge.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.incharge.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.incharge.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.role.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.role.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.role.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.explanation.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.explanation.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.explanation.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }
                    // for company
                    if (x == jssTableDefinition.company.index) {
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var rowNumber = parseInt(y) + 1;
                        var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                        console.log(element);
                        var companyName = element[0].cells[9].innerText;
                        if (companyName.toLowerCase() !== 'mw') {
                            element[0].cells[10].innerText = '';
                            $(jss.getCell(jssTableDefinition.grade.cellName + rowNumber)).addClass('readonly');
                            $(jss.getCell(jssTableDefinition.grade.cellName + rowNumber)).css('color', 'black');
                            $(jss.getCell(jssTableDefinition.grade.cellName + rowNumber)).css('background-color', 'white');
                            jss.setValueFromCoords(jssTableDefinition.unitPrice.index, parseInt(y), 0, false);
                            
                        }
                        else {
                            $(jss.getCell(jssTableDefinition.grade.cellName + rowNumber)).removeClass('readonly');
                            $(jss.getCell(jssTableDefinition.grade.cellName + rowNumber)).css('color', 'black');
                            $(jss.getCell(jssTableDefinition.grade.cellName + rowNumber)).css('background-color', 'white'); 
                            jss.setValueFromCoords(jssTableDefinition.unitPrice.index, parseInt(y), 0, false);
                        }
                        
                        retrivedObjectForOnChangeInsert = retrivedObjectForInsertOnChange(jss.getRowData(y));
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        dataCheckForInsertOnChange = insertedOnChangeList.filter(d => d.assignmentId == retrivedObjectForOnChangeInsert.assignmentId);

                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x); 
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.company.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.company.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }
                    // for grade
                    if (x == jssTableDefinition.grade.index) {
                        debugger;
                        
                        var rowNumber = parseInt(y) + 1;
                        var element = $(`.jexcel > tbody > tr:nth-of-type(${rowNumber})`);
                        var companyName = element[0].cells[9].innerText;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }


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
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        dataCheckForInsertOnChange = insertedOnChangeList.filter(d => d.assignmentId == retrivedObjectForOnChangeInsert.assignmentId);
                        if (dataCheckForInsertOnChange.length == 0) {
                           //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                           //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                       

                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.grade.index,retrivedData.assignmentId)
                            if(!isCellAlreadyChanged){                                
                                SetColorForCells("white", "black", jssTableDefinition.grade.cellName+(parseInt(y)+1))                                
                            }
                        }
                    }
                    // for unit price
                    if (x == jssTableDefinition.unitPrice.index) {
                        debugger;
                        retrivedObjectForOnChangeInsert.bcyrCell = retrivedObjectForOnChangeInsert.bCYRCell + '_' + x;
                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        if (dataCheckForInsertOnChange.length == 0) {
                            //jssUpdatedData.push(retrivedData);
                            
                            insertedOnChangeList.push(retrivedObjectForOnChangeInsert);
                        }
                        else {
                            
                            //updateArray(jssUpdatedData, retrivedData);
                            updateArrayForInsertOnChange(insertedOnChangeList, retrivedObjectForOnChangeInsert);
                        }
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        //cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.unitPrice.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.unitPrice.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }
                    

                    if (x == jssTableDefinition.octM.index) {
                        var octPointsSum = 0;
                        var octCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var octSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {  
                            if (dataValue[jssTableDefinition.octM.index] != "" && dataValue[jssTableDefinition.octM.index] != null && dataValue[jssTableDefinition.octM.index] != undefined) {
                                var octPointPerRow = 0.0;
                                octPointPerRow = parseFloat(dataValue[jssTableDefinition.octM.index]).toFixed(1);
                                octPointsSum += parseFloat(octPointPerRow);                            
                                octCostSum = parseFloat(octCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.octM.index]);     
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);  
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.octM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.octM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.novM.index) {
                        var novPointsSum = 0;
                        var novCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var novSum = 0;

                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.novM.index] != "" && dataValue[jssTableDefinition.novM.index] != null && dataValue[jssTableDefinition.novM.index] != undefined) {
                                var novPointPerRow = 0.0;
                                novPointPerRow = parseFloat(dataValue[jssTableDefinition.novM.index]).toFixed(1);
                                novPointsSum += parseFloat(novPointPerRow);   

                                novCostSum = parseFloat(novCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.novM.index]);   
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.novM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.novM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.decM.index) {
                        var decPointsSum = 0;
                        var decCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var decSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.decM.index] != "" && dataValue[jssTableDefinition.decM.index] != null && dataValue[jssTableDefinition.decM.index] != undefined){
                                var decPointPerRow = 0.0;
                                decPointPerRow = parseFloat(dataValue[jssTableDefinition.decM.index]).toFixed(1);
                                decPointsSum += parseFloat(decPointPerRow); 
                                
                                decCostSum = parseFloat(decCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.decM.index]);   
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.decM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.decM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.janM.index) {
                        var janPointsSum = 0;
                        var janCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var janSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.janM.index] != "" && dataValue[jssTableDefinition.janM.index] != null && dataValue[jssTableDefinition.janM.index] != undefined){
                                var janPointPerRow = 0.0;
                                janPointPerRow = parseFloat(dataValue[jssTableDefinition.janM.index]).toFixed(1);
                                janPointsSum += parseFloat(janPointPerRow); 

                                janCostSum = parseFloat(janCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.janM.index]);   
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.janM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.janM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.febM.index) {
                        var febPointsSum = 0;
                        var febCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var febSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.febM.index] != "" && dataValue[jssTableDefinition.febM.index] != null && dataValue[jssTableDefinition.febM.index] != undefined){
                                var febPointPerRow = 0.0;
                                febPointPerRow = parseFloat(dataValue[jssTableDefinition.febM.index]).toFixed(1);
                                febPointsSum += parseFloat(febPointPerRow); 
                                
                                febCostSum = parseFloat(febCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.febM.index]);   
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.febM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.febM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.marM.index) {
                        var marPointsSum = 0;
                        var marCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var marSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.marM.index] != "" && dataValue[jssTableDefinition.marM.index] != null && dataValue[jssTableDefinition.marM.index] != undefined){
                                var marPointPerRow = 0.0;
                                marPointPerRow = parseFloat(dataValue[jssTableDefinition.marM.index]).toFixed(1);
                                marPointsSum += parseFloat(marPointPerRow); 
                                
                                marCostSum = parseFloat(marCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.marM.index]);   
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.marM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.marM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.aprM.index) {
                        var aprPointsSum = 0;
                        var aprCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var aprSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.aprM.index] != "" && dataValue[jssTableDefinition.aprM.index] != null && dataValue[jssTableDefinition.aprM.index] != undefined){
                                var aprPointPerRow = 0.0;
                                aprPointPerRow = parseFloat(dataValue[jssTableDefinition.aprM.index]).toFixed(1);
                                aprPointsSum += parseFloat(aprPointPerRow); 
                                
                                aprCostSum = parseFloat(aprCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.aprM.index]);
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.aprM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.aprM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.mayM.index) {
                        var mayPointsSum = 0;
                        var mayCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var maySum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.mayM.index] != "" && dataValue[jssTableDefinition.mayM.index] != null && dataValue[jssTableDefinition.mayM.index] != undefined){
                                var mayPointPerRow = 0.0;
                                mayPointPerRow = parseFloat(dataValue[jssTableDefinition.mayM.index]).toFixed(1);
                                mayPointsSum += parseFloat(mayPointPerRow); 

                                mayCostSum = parseFloat(mayCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.mayM.index]); 
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.mayM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.mayM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.junM.index) {
                        var junPointsSum = 0;
                        var junCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var junSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.junM.index] != "" && dataValue[jssTableDefinition.junM.index] != null && dataValue[jssTableDefinition.junM.index] != undefined){
                                var junPointPerRow = 0.0;
                                junPointPerRow = parseFloat(dataValue[jssTableDefinition.junM.index]).toFixed(1);
                                junPointsSum += parseFloat(junPointPerRow); 

                                junCostSum = parseFloat(junCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.junM.index]); 
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.junM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.junM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.julM.index) {
                        var julPointsSum = 0;
                        var julCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var julSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.julM.index] != "" && dataValue[jssTableDefinition.julM.index] != null && dataValue[jssTableDefinition.julM.index] != undefined){
                                var julPointPerRow = 0.0;
                                julPointPerRow = parseFloat(dataValue[jssTableDefinition.julM.index]).toFixed(1);
                                julPointsSum += parseFloat(julPointPerRow); 

                                julCostSum = parseFloat(julCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.julM.index]); 
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.julM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.julM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.augM.index) {
                        var augPointsSum = 0;
                        var augCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var augSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.augM.index] != "" && dataValue[jssTableDefinition.augM.index] != null && dataValue[jssTableDefinition.augM.index] != undefined){
                                var augPointPerRow = 0.0;
                                augPointPerRow = parseFloat(dataValue[jssTableDefinition.augM.index]).toFixed(1);
                                augPointsSum += parseFloat(augPointPerRow); 

                                augCostSum = parseFloat(augCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.augM.index]); 
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.augM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.augM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }

                    if (x == jssTableDefinition.sepM.index) {
                        var sepPointsSum = 0;
                        var sepCostSum = 0;

                        if(isUnapprovedDeletedRow){
                            StoreChangeCellData(x,retrivedData.assignmentId);
                        }
                        var sepSum = 0;
                        $.each(jss.getData(), (index, dataValue) => {
                            if (dataValue[jssTableDefinition.sepM.index] != "" && dataValue[jssTableDefinition.sepM.index] != null && dataValue[jssTableDefinition.sepM.index] != undefined){
                                var sepPointPerRow = 0.0;
                                sepPointPerRow = parseFloat(dataValue[jssTableDefinition.sepM.index]).toFixed(1);
                                sepPointsSum += parseFloat(sepPointPerRow); 

                                sepCostSum = parseFloat(sepCostSum)+parseFloat(dataValue[jssTableDefinition.unitPrice.index])*parseFloat(dataValue[jssTableDefinition.sepM.index]); 
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
                        $(cell).css('color', 'red');
                        $(cell).css('background-color', 'yellow');
                        cellwiseColorCode.push(retrivedData.assignmentId + '_' + x);
                    }else{ 
                        if(isUnapprovedDeletedRow){
                            var isCellAlreadyChanged = false;
                            isCellAlreadyChanged = CheckIfAlreadyExists(jssTableDefinition.sepM.index,retrivedData.assignmentId)
                            if (!isCellAlreadyChanged) {
                                SetColorForCells("white", "black", jssTableDefinition.sepM.cellName + (parseInt(y) + 1))                                
                            }
                        }
                    }
                    
                    if(isUnapprovedDeletedRow){
                        var isCellAlreadyChanged = false;
                        if (!isCellAlreadyChanged) {
                            SetColorForCostsCells("white", "black", jssTableDefinition.octT.cellName+(parseInt(y)+1));                               
                            SetColorForCostsCells("white", "black", jssTableDefinition.novT.cellName+(parseInt(y)+1));                               
                            SetColorForCostsCells("white", "black", jssTableDefinition.decT.cellName+(parseInt(y)+1));
                            SetColorForCostsCells("white", "black", jssTableDefinition.janT.cellName+(parseInt(y)+1));    
                            SetColorForCostsCells("white", "black", jssTableDefinition.febT.cellName+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black",  jssTableDefinition.marT.cellName+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black",  jssTableDefinition.aprT.cellName+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black",  jssTableDefinition.mayT.cellName+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black",  jssTableDefinition.junT.cellName+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black",  jssTableDefinition.julT.cellName+(parseInt(y)+1));                                
                            SetColorForCostsCells("white","black",  jssTableDefinition.augT.cellName+(parseInt(y)+1));                                                            
                            SetColorForCostsCells("white","black",  jssTableDefinition.sepT.cellName+(parseInt(y)+1));                                
                        }
                    }
                }

            }

        },        
        //ondeleterow: deleted,
        contextMenu: function (obj, x, y, e) {
            var items = [];           
            items.push({
                title: '要員を追加 (Add Emp)',
                onclick: function () {
                    obj.insertRow(1, parseInt(y));
                    var insertedRowNumber = parseInt(obj.getSelectedRows(true)) + 2;
                    
                    setTimeout(function () {
                        SetColorCommonRow(parseInt(y) + 2, "yellow", "red", "newrow");
                        jss.setValueFromCoords(jssTableDefinition.bcyr.index, (insertedRowNumber - 1), true, false);

                        $('#jexcel_add_employee_modal').modal('show');
                        globalY = parseInt(y) + 1;
                        GetEmployeeList();
                    },1000);
                    
                    
                }
            });
            items.push({
                title: '要員のコピー（単価変更）(unit price)',
                onclick: function () {
                    var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                    if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                        return false;
                    }

                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";
                    var activeEmployeeCount = 0;
                    var masterEmployeeName = "";
                    var inactiveEmployeeCount = 0;
                    var _duplicateFrom = "";
                    var _duplicateCount = "";
                    var _roleChanged = "";
                    var _unitPriceChanged = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    _duplicateFrom = retrivedData.assignmentId.toString();
                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[jssTableDefinition.employeeId.index] == retrivedData.employeeId) {
                                if (isNaN(x[jssTableDefinition.assignmentId.index])) {
                                    allSpecificObjectsCount++;
                                    allSameEmployeeId.push(x[jssTableDefinition.assignmentId.index]);
                                }

                            }
                        }
                        var allSameEmployeeIdSplitted = [];
                        for (var i = 0; i < allSameEmployeeId.length; i++) {
                            var singleNewEmployeeId = allSameEmployeeId[i].split('-');
                            allSameEmployeeIdSplitted.push(parseInt(singleNewEmployeeId[1]));
                        }


                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeIdSplitted);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][jssTableDefinition.assignmentId.index] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            if (x[jssTableDefinition.assignmentId.index] == 'new-' + minAssignmentNumber) {
                                newCountedEmployeeName = x[jssTableDefinition.employeeName.index] + ` (${allSpecificObjectsCount + 1})`;
                                break;
                            }
                        }



                    }
                    else {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[jssTableDefinition.employeeId.index] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[jssTableDefinition.assignmentId.index])) {
                                    allSameEmployeeId.push(x[jssTableDefinition.assignmentId.index]);
                                }

                            }
                        }

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][jssTableDefinition.assignmentId.index] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;

                        for (let x of allData) {
                            if (parseInt(x[jssTableDefinition.employeeId.index]) == parseInt(retrivedData.employeeId)) {
                                activeEmployeeCount = activeEmployeeCount + 1;
                            }
                        }

                        $.ajax({
                            url: `/api/utilities/GetEmployeeNameForMenuChange`,
                            contentType: 'application/json',
                            type: 'GET',
                            async: false,
                            dataType: 'json',
                            data: "employeeAssignmentId=" + retrivedData.assignmentId + "&employeeId=" + retrivedData.employeeId + "&menuType=unit" + "&year=" + retrivedData.year,
                            success: function (data) {
                                masterEmployeeName = data.EmployeeName;
                                inactiveEmployeeCount = data.EmployeeCount;
                            }
                        });
                    }


                    _unitPriceChanged = '1';
                    _roleChanged = '0';
                    _duplicateCount = (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1);
                    //newCountedEmployeeName = masterEmployeeName + " (" + (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1) + ")";
                    obj.setValueFromCoords(jssTableDefinition.employeeName.index, nextRow, retrivedData.employeeName, false);
                    allSameEmployeeId = [];

                    obj.setValueFromCoords(jssTableDefinition.duplicateFrom.index, nextRow, _duplicateFrom, false);
                    obj.setValueFromCoords(jssTableDefinition.duplicateCount.index, nextRow, _duplicateCount, false);
                    obj.setValueFromCoords(jssTableDefinition.roleChanged.index, nextRow, _roleChanged, false);
                    obj.setValueFromCoords(jssTableDefinition.unitPriceChanged.index, nextRow, _unitPriceChanged, false);
                    obj.setValueFromCoords(jssTableDefinition.employeeId.index, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(jssTableDefinition.remarks.index, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(jssTableDefinition.section.index, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(jssTableDefinition.department.index, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(jssTableDefinition.incharge.index, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(jssTableDefinition.role.index, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(jssTableDefinition.explanation.index, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(jssTableDefinition.company.index, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(jssTableDefinition.grade.index, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(jssTableDefinition.unitPrice.index, nextRow, retrivedData.unitPrice, false);

                    // color row....
                    jss.setStyle(jssTableDefinition.employeeName.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.employeeName.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.grade.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.grade.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.unitPrice.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + (nextRow + 1), "color", "red");

                    // disable section....
                    $(obj.getCell(jssTableDefinition.section.cellName + (nextRow + 1))).addClass('readonly');
                    // disable department....
                    $(obj.getCell(jssTableDefinition.department.cellName + (nextRow + 1))).addClass('readonly');
                    // disable incharge....
                    $(obj.getCell(jssTableDefinition.incharge.cellName + (nextRow + 1))).addClass('readonly');
                    // disable role....
                    $(obj.getCell(jssTableDefinition.role.cellName + (nextRow + 1))).addClass('readonly');
                    // disable company....
                    $(obj.getCell(jssTableDefinition.company.cellName + (nextRow + 1))).addClass('readonly');

                    obj.setValueFromCoords(jssTableDefinition.bcyr.index, nextRow, false, false);
                    obj.setValueFromCoords(jssTableDefinition.bcyrCell.index, nextRow, `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`, false);
                    obj.setValueFromCoords(jssTableDefinition.isActive.index, nextRow, true, false);
                    obj.setValueFromCoords(jssTableDefinition.rowType.index, nextRow, `unit_${retrivedData.assignmentId}_${y}`, false);

                    obj.setValueFromCoords(jssTableDefinition.octM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.novM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.decM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.janM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.febM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.marM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.aprM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.mayM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.junM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.julM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.augM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.sepM.index, nextRow, '0.0', false);


                    jss.setValueFromCoords(jssTableDefinition.octT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.octM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.novT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.novM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.decT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.decM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.janT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.janM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.febT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.febM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.marT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.marM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.aprT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.aprM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.mayT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.mayM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.junT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.junM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.julT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.julM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.augT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.augM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.sepT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.sepM.cellName}${parseInt(nextRow) + 1}`, false);

                    newRowCount++;
                    newRowChangeEventFlag = false;
                }

            });
            items.push({
                title: '要員のコピー（役割変更）(role)',
                onclick: function () {
                    var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                    if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                        return false;
                    }
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";
                    var activeEmployeeCount = 0;
                    var masterEmployeeName = "";
                    var inactiveEmployeeCount = 0;
                    var _duplicateFrom = "";
                    var _duplicateCount = "";
                    var _roleChanged = "";
                    var _unitPriceChanged = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    _duplicateFrom = retrivedData.assignmentId.toString();
                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[jssTableDefinition.employeeId.index] == retrivedData.employeeId) {

                                if (isNaN(x[0])) {
                                    allSpecificObjectsCount++;
                                    allSameEmployeeId.push(x[jssTableDefinition.assignmentId.index]);
                                }

                            }
                        }
                        var allSameEmployeeIdSplitted = [];
                        for (var i = 0; i < allSameEmployeeId.length; i++) {
                            var singleNewEmployeeId = allSameEmployeeId[i].split('-');
                            allSameEmployeeIdSplitted.push(parseInt(singleNewEmployeeId[1]));
                        }


                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeIdSplitted);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][jssTableDefinition.assignmentId.index] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;


                        for (let x of allData) {
                            if (x[jssTableDefinition.assignmentId.index] == 'new-' + minAssignmentNumber) {
                                newCountedEmployeeName = x[jssTableDefinition.employeeName.index] + ` (${allSpecificObjectsCount + 1})*`;
                                break;
                            }
                        }
                    }
                    else {
                        newEmployeeId = "new-" + newRowCount;

                        var allSpecificObjectsCount = 0;
                        for (let x of allData) {
                            if (x[jssTableDefinition.employeeId.index] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[jssTableDefinition.assignmentId.index])) {
                                    allSameEmployeeId.push(x[jssTableDefinition.assignmentId.index]);
                                }
                            }
                        }

                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);


                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][jssTableDefinition.assignmentId.index] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;

                        for (let x of allData) {
                            if (parseInt(x[jssTableDefinition.employeeId.index]) == parseInt(retrivedData.employeeId)) {
                                activeEmployeeCount = activeEmployeeCount + 1;
                            }
                        }

                        $.ajax({
                            url: `/api/utilities/GetEmployeeNameForMenuChange`,
                            contentType: 'application/json',
                            type: 'GET',
                            async: false,
                            dataType: 'json',
                            data: "employeeAssignmentId=" + retrivedData.assignmentId + "&employeeId=" + retrivedData.employeeId + "&menuType=unit" + "&year=" + retrivedData.year,
                            success: function (data) {
                                masterEmployeeName = data.EmployeeName;
                                inactiveEmployeeCount = data.EmployeeCount;
                            }
                        });
                    }

                    _unitPriceChanged = '0';
                    _roleChanged = '1';
                    _duplicateCount = (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1);
                    //newCountedEmployeeName =   masterEmployeeName +" ("+(parseInt(activeEmployeeCount)+parseInt(inactiveEmployeeCount)+1)+")*";
                    obj.setValueFromCoords(jssTableDefinition.employeeName.index, nextRow, retrivedData.employeeName, false);
                    allSameEmployeeId = [];


                    obj.setValueFromCoords(jssTableDefinition.duplicateFrom.index, nextRow, _duplicateFrom, false);
                    obj.setValueFromCoords(jssTableDefinition.duplicateCount.index, nextRow, _duplicateCount, false);
                    obj.setValueFromCoords(jssTableDefinition.roleChanged.index, nextRow, _roleChanged, false);
                    obj.setValueFromCoords(jssTableDefinition.unitPriceChanged.index, nextRow, _unitPriceChanged, false);
                    obj.setValueFromCoords(jssTableDefinition.employeeId.index, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(jssTableDefinition.remarks.index, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(jssTableDefinition.section.index, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(jssTableDefinition.department.index, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(jssTableDefinition.incharge.index, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(jssTableDefinition.role.index, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(jssTableDefinition.explanation.index, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(jssTableDefinition.company.index, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(jssTableDefinition.grade.index, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(jssTableDefinition.unitPrice.index, nextRow, retrivedData.unitPrice, false);


                    // color row....
                    jss.setStyle(jssTableDefinition.employeeName.cellName, nextRow + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.employeeName.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.section.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.section.cellName + (nextRow + 1), "color", "red");


                    jss.setStyle(jssTableDefinition.department.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.department.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.incharge.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.incharge.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.role.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.role.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.company.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.company.cellName + (nextRow + 1), "color", "red");


                    // disable grade and unit price....
                    $(obj.getCell(jssTableDefinition.grade.cellName + (nextRow + 1))).addClass('readonly');
                    $(obj.getCell(jssTableDefinition.unitPrice.cellName + (nextRow + 1))).addClass('readonly');

                    obj.setValueFromCoords(jssTableDefinition.bcyr.index, nextRow, false, false);
                    obj.setValueFromCoords(jssTableDefinition.bcyrCell.index, nextRow, `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`, false);
                    obj.setValueFromCoords(jssTableDefinition.isActive.index, nextRow, true, false);
                    obj.setValueFromCoords(jssTableDefinition.rowType.index, nextRow, `role_${retrivedData.assignmentId}_${y}`, false);


                    obj.setValueFromCoords(jssTableDefinition.octM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.novM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.decM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.janM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.febM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.marM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.aprM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.mayM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.junM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.julM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.augM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.sepM.index, nextRow, '0.0', false);


                    jss.setValueFromCoords(jssTableDefinition.octT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.octM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.novT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.novM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.decT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.decM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.janT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.janM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.febT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.febM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.marT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.marM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.aprT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.aprM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.mayT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.mayM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.junT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.junM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.julT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.julM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.augT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.augM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.sepT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.sepM.cellName}${parseInt(nextRow) + 1}`, false);

                    newRowCount++;
                    newRowChangeEventFlag = false;

                }
            });
            items.push({
                title: '要員のコピー（役割・単価変更）(role/unit)',
                onclick: function () {
                    var retrivedDataForCheck = retrivedObject(jss.getRowData(y));
                    if (retrivedDataForCheck.assignmentId.toString().includes('new')) {
                        return false;
                    }
                    newRowChangeEventFlag = true;
                    var allData = jss.getData();
                    let nextRow = parseInt(y) + 1;
                    var allSameEmployeeId = [];
                    var newCountedEmployeeName = '';
                    var newEmployeeId = "";
                    var activeEmployeeCount = 0;
                    var masterEmployeeName = "";
                    var inactiveEmployeeCount = 0;
                    var _duplicateFrom = "";
                    var _duplicateCount = "";
                    var _roleChanged = "";
                    var _unitPriceChanged = "";

                    obj.insertRow(1, parseInt(y));

                    var retrivedData = retrivedObject(jss.getRowData(y));
                    _duplicateFrom = retrivedData.assignmentId.toString();

                    if (retrivedData.assignmentId.toString().includes('new')) {
                        newEmployeeId = "new-" + newRowCount;
                        var allSpecificObjectsCount = 0;

                        for (let x of allData) {
                            if (x[jssTableDefinition.employeeId.index] == retrivedData.employeeId) {

                                if (isNaN(x[0])) {
                                    allSpecificObjectsCount++;
                                    allSameEmployeeId.push(x[jssTableDefinition.assignmentId.index]);
                                }

                            }
                        }
                        var allSameEmployeeIdSplitted = [];
                        for (var i = 0; i < allSameEmployeeId.length; i++) {
                            var singleNewEmployeeId = allSameEmployeeId[i].split('-');
                            allSameEmployeeIdSplitted.push(parseInt(singleNewEmployeeId[1]));
                        }


                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeIdSplitted);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][jssTableDefinition.assignmentId.index] == 'new-' + minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            if (x[jssTableDefinition.assignmentId.index] == 'new-' + minAssignmentNumber) {
                                newCountedEmployeeName = x[jssTableDefinition.employeeName.index] + ` (${allSpecificObjectsCount + 1})**`;
                                break;
                            }
                        }
                    } else {
                        newEmployeeId = "new-" + newRowCount;

                        var allSpecificObjectsCount = 0;
                        for (let x of allData) {
                            if (x[jssTableDefinition.employeeId.index] == retrivedData.employeeId) {
                                allSpecificObjectsCount++;
                                if (!isNaN(x[jssTableDefinition.assignmentId.index])) {
                                    allSameEmployeeId.push(x[jssTableDefinition.assignmentId.index]);
                                }
                            }
                        }
                        var minAssignmentNumber = Math.min.apply(null, allSameEmployeeId);

                        for (let x = 0; x < allData.length; x++) {
                            if (allData[x][jssTableDefinition.assignmentId.index] == minAssignmentNumber) {

                                retrivedData = retrivedObject(jss.getRowData(x));

                                break;
                            }
                        }

                        retrivedData.bcyr = false;
                        retrivedData.bCYRCell = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;


                        for (let x of allData) {
                            if (parseInt(x[jssTableDefinition.employeeId.index]) == parseInt(retrivedData.employeeId)) {
                                activeEmployeeCount = activeEmployeeCount + 1;
                            }
                        }
                        $.ajax({
                            url: `/api/utilities/GetEmployeeNameForMenuChange`,
                            contentType: 'application/json',
                            type: 'GET',
                            async: false,
                            dataType: 'json',
                            data: "employeeAssignmentId=" + retrivedData.assignmentId + "&employeeId=" + retrivedData.employeeId + "&menuType=unit" + "&year=" + retrivedData.year,
                            success: function (data) {
                                masterEmployeeName = data.EmployeeName;
                                inactiveEmployeeCount = data.EmployeeCount;
                            }
                        });
                    }

                    _unitPriceChanged = '1';
                    _roleChanged = '1';
                    _duplicateCount = (parseInt(activeEmployeeCount) + parseInt(inactiveEmployeeCount) + 1);
                    //newCountedEmployeeName =   masterEmployeeName +" ("+(parseInt(activeEmployeeCount)+parseInt(inactiveEmployeeCount)+1)+")**";
                    obj.setValueFromCoords(jssTableDefinition.employeeName.index, nextRow, retrivedData.employeeName, false);
                    allSameEmployeeId = [];

                    jss.setStyle(jssTableDefinition.employeeName.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.employeeName.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.section.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.section.cellName + (nextRow + 1), "color", "red");


                    jss.setStyle(jssTableDefinition.department.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.department.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.incharge.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.incharge.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.role.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.role.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.company.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.company.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.grade.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.grade.cellName + (nextRow + 1), "color", "red");

                    jss.setStyle(jssTableDefinition.unitPrice.cellName + (nextRow + 1), "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.unitPrice.cellName + (nextRow + 1), "color", "red");

                    obj.setValueFromCoords(jssTableDefinition.duplicateFrom.index, nextRow, _duplicateFrom, false);
                    obj.setValueFromCoords(jssTableDefinition.duplicateCount.index, nextRow, _duplicateCount, false);
                    obj.setValueFromCoords(jssTableDefinition.roleChanged.index, nextRow, _roleChanged, false);
                    obj.setValueFromCoords(jssTableDefinition.unitPriceChanged.index, nextRow, _unitPriceChanged, false);
                    obj.setValueFromCoords(jssTableDefinition.employeeId.index, nextRow, retrivedData.employeeId, false);
                    obj.setValueFromCoords(jssTableDefinition.remarks.index, nextRow, retrivedData.remarks, false);
                    obj.setValueFromCoords(jssTableDefinition.section.index, nextRow, retrivedData.sectionId, false);
                    obj.setValueFromCoords(jssTableDefinition.department.index, nextRow, retrivedData.departmentId, false);
                    obj.setValueFromCoords(jssTableDefinition.incharge.index, nextRow, retrivedData.inchargeId, false);
                    obj.setValueFromCoords(jssTableDefinition.role.index, nextRow, retrivedData.roleId, false);
                    obj.setValueFromCoords(jssTableDefinition.explanation.index, nextRow, retrivedData.explanationId, false);
                    obj.setValueFromCoords(jssTableDefinition.company.index, nextRow, retrivedData.companyId, false);
                    obj.setValueFromCoords(jssTableDefinition.grade.index, nextRow, retrivedData.gradeId, false);
                    obj.setValueFromCoords(jssTableDefinition.unitPrice.index, nextRow, retrivedData.unitPrice, false);

                    obj.setValueFromCoords(jssTableDefinition.bcyr.index, nextRow, false, false);
                    obj.setValueFromCoords(jssTableDefinition.bcyrCell.index, nextRow, `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`, false);
                    obj.setValueFromCoords(jssTableDefinition.isActive.index, nextRow, true, false);
                    obj.setValueFromCoords(jssTableDefinition.rowType.index, nextRow, `role_${retrivedData.assignmentId}_${y}`, false);

                    obj.setValueFromCoords(jssTableDefinition.octM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.novM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.decM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.janM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.febM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.marM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.aprM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.mayM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.junM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.julM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.augM.index, nextRow, '0.0', false);
                    obj.setValueFromCoords(jssTableDefinition.sepM.index, nextRow, '0.0', false);


                    jss.setValueFromCoords(jssTableDefinition.octT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.octM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.novT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.novM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.decT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.decM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.janT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.janM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.febT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.febM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.marT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.marM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.aprT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.aprM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.mayT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.mayM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.junT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.junM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.julT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.julM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.augT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.augM.cellName}${parseInt(nextRow) + 1}`, false);
                    jss.setValueFromCoords(jssTableDefinition.sepT.index, nextRow, `=${jssTableDefinition.unitPrice.cellName}${parseInt(nextRow) + 1}*${jssTableDefinition.sepM.cellName}${parseInt(nextRow) + 1}`, false);


                    newRowCount++;
                    newRowChangeEventFlag = false;
                }
            });
            items.push({
                title: '選択した要員の削除 (delete)',                
                onclick: function () {                    
                    var value = obj.getSelectedRows();
                    var assignementId = jss.getValueFromCoords(jssTableDefinition.assignmentId.index, y);
                    var name = jss.getValueFromCoords(jssTableDefinition.employeeName.index, y);                                       
                    if(parseInt(assignementId) >0){
                        deletedExistingRowIds.push(assignementId);                                
                        SetColorCommonRow(parseInt(y)+1,"gray","black","deleted");                        
                    }else{
                        alert(name +" は保存されていないため、削除できません")  
                    }      
                }
            });

            return items;
        }
    });

    $("#update_forecast_history").css("display", "block");
    $("#cancel_forecast_history").css("display", "block");

    //delete unwanted column from jexcel table
    jss.deleteColumn(53, 23);
    //jss.deleteColumn(52, 43);

    //first header sticky
    // var first_header_1 = $('.jexcel > thead > tr:nth-of-type(2) > td');
    // first_header_1.css('position', 'sticky');
    // first_header_1.css('top', '0px');

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
    // var dept_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // dept_header.css('position', 'sticky');
    // dept_header.css('top', '0px');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');
    // var incharge_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // incharge_header.css('position', 'sticky');
    // incharge_header.css('top', '0px');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');
    // var incharge_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_role.css('position', 'sticky');
    // jexcelFirstHeaderRow_role.css('top', '0px');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_exp = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_exp.css('position', 'sticky');
    // jexcelFirstHeaderRow_exp.css('top', '0px');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_com = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_com.css('position', 'sticky');
    // jexcelFirstHeaderRow_com.css('top', '0px');

    //sort arrow on load: grade
    var grade_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)');
    grade_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_grade = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_grade.css('position', 'sticky');
    // jexcelFirstHeaderRow_grade.css('top', '0px');

    //sort arrow on load: unit
    var unit_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)');
    unit_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_unit = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_unit.css('position', 'sticky');
    // jexcelFirstHeaderRow_unit.css('top', '0px');

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
    
    var allRows = jss.getData();
    var count = 1;
    $.each(allRows, function (index, value) {
        if (value[jssTableDefinition.bcyr.index.toString()] == true && value[jssTableDefinition.bcyrApproved.index.toString()] == false) {            
            SetColorCommonRow(count,"yellow","red","newrow");
        }
        else {
            var isApprovedCells = value[jssTableDefinition.isApproved.index];
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

                if (parseInt(nestedValue) == jssTableDefinition.totalM.index) {
                    jss.setStyle(jssTableDefinition.totalM.cellName + count, "background-color", "yellow");
                    jss.setStyle(jssTableDefinition.totalM.cellName + count, "color", "red");                   
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
                if (parseInt(nestedValue2) == jssTableDefinition.totalM.index) {
                    jss.setStyle(jssTableDefinition.totalM.cellName + count, "background-color", "LightBlue");
                    jss.setStyle(jssTableDefinition.totalM.cellName + count, "color", "red");
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
                if (parseInt(nestedValue2) == jssTableDefinition.totalM.index) {
                    jss.setStyle(jssTableDefinition.totalM.cellName + count, "background-color", "red");
                    jss.setStyle(jssTableDefinition.totalM.cellName + count, "color", "black");
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
        if (value[jssTableDefinition.isActive.index.toString()] == false && value[jssTableDefinition.bcyrApproved.index.toString()] == false && value[jssTableDefinition.isDeletePending.index.toString()] == false) {
            SetColorCommonRow(count,"gray","black","deleted");
        }
        else if (value[jssTableDefinition.isRowPending.index.toString()] == true || value[jssTableDefinition.isDeletePending.index.toString()] == true) {
            SetColorCommonRow(count,"red","black","editable");
        }        
        count++;
    });
}

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

    if (year == '' || year == undefined) {
        alert('年度を選択してください');
        return false;
    }    

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

    _retriveddata = [];
    $.ajax({
        url: `/api/utilities/GetAllAssignmentData`,
        contentType: 'application/json',
        type: 'GET',
        async: true,
        dataType: 'json',
        data: "employeeName=" + employeeName + "&sectionId=" + sectionId + "&departmentId=" + departmentId + "&inchargeId=" + inchargeId + "&roleId=" + roleId + "&explanationId=" + explanationId + "&companyId=" + companyId + "&status=" + year + "&year=" + year + "&timeStampId=",
        success: function (data) {
            _retriveddata = data;
            ShowForecastJexcel();
            LoaderHide();
            if(showType=='save'){
                ToastMessageSuccess_Center('保存されました.');
            }
        }
    });    
}

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

var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');    
}


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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(jssTableDefinition.bcyrCell.index, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(jssTableDefinition.bcyrCell.index, y, currentValue, false);
        }
    }
    if (x == jssTableDefinition.explanation.index) {
        array[index].explanationId = retrivedData.explanationId;
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
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

function updateArrayForInsertOnChange(array, retrivedData) {
    debugger;
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
    array[index].bcyrCell = array[index].bcyrCell+',' + retrivedData.bcyrCell;
}

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

    //var bcyrCellVal = rowData[jssTableDefinition.assignmentId.index] + '_' + x;
    //var newEmployeeId = rowData[jssTableDefinition.employeeId.index];
    //if (_roleChanged > 0) {
    //    bcyrCellVal = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8`;
    //}
    //else if (_unitPriceChanged > 0) {
    //    bcyrCellVal = `${newEmployeeId}_1,${newEmployeeId}_9,${newEmployeeId}_10`;
    //}
    //else {
    //    bcyrCellVal = `${newEmployeeId}_1,${newEmployeeId}_3,${newEmployeeId}_4,${newEmployeeId}_5,${newEmployeeId}_6,${newEmployeeId}_8,${newEmployeeId}_9,${newEmployeeId}_10`;
    //}


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
        year: document.getElementById('assignment_year_list').value,

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
    var assignment_year = $("#assignment_year_list").val();

    $('#employee_list').empty();
    $.getJSON(`/api/utilities/EmployeeListForEmployeeAssignment/${assignment_year}`)
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

    //show the duplicate name: rule can be for: role/unit/both
    var employeeNameForAddEmployee = '';
    var allTableData = jss.getData();    
    var activeEmployeeCountForAddEmployee =0;
    var masterEmployeeNameForAddEmployee = "";
    var inactiveEmployeeCountForAddEmployee = 0;

    for (let x of allTableData) {            
        console.log("x: "+x);                
        console.log("retrivedData.employeeId: "+employeeId);                
		if(parseInt(x[42]) == parseInt(employeeId)){
			activeEmployeeCountForAddEmployee = activeEmployeeCountForAddEmployee+1;
		}                          
	}
    var tempEmployeeAssignmentId=0;
    var year = $("#assignment_year_list").val();
	$.ajax({
		url: `/api/utilities/GetEmployeeNameForMenuChange`,
		contentType: 'application/json',
		type: 'GET',
		async: false,
		dataType: 'json',
		data: "employeeAssignmentId=" + tempEmployeeAssignmentId+"&employeeId="+employeeId+"&menuType=unit"+"&year="+year,
		success: function (data) { 
			masterEmployeeNameForAddEmployee = data.EmployeeName;
			inactiveEmployeeCountForAddEmployee = data.EmployeeCount;
		}
	});                             
    employeeNameForAddEmployee =   masterEmployeeNameForAddEmployee +" ("+(parseInt(activeEmployeeCountForAddEmployee)+parseInt(inactiveEmployeeCountForAddEmployee)+1)+")";

    jss.setValueFromCoords(1, globalY, employeeName, false);
    jss.setValueFromCoords(42, globalY, employeeId, false);
    $('#jexcel_add_employee_modal').modal('hide');
}

function UpdateForecast(isUpdate) {
    $("#update_forecast").modal("hide");        
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
    var deleteMessage = "";
    var timestampMessage = "";

    var promptValue = prompt("履歴ファイル保存名", '');
    $("#timeStamp_ForUpdateData").val('');
    if (promptValue == null || promptValue == undefined || promptValue == "") {
        return false;
    } else {
        LoaderShow();        

        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getDate();
        var year = dateObj.getUTCFullYear();
        var miliSeconds = dateObj.getMilliseconds();
        var timestamp = `${year}${month}${day}${miliSeconds}_`;
        
        if(isUpdate){
            if (jssUpdatedData.length > 0) {           
                    updateMessage = "Successfully data updated";
                    $.ajax({
                        url: `/api/utilities/UpdateForecastData?changeType=mm`,
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
                updateMessage = ""
            }

            if (jssInsertedData.length > 0) {
                var elementIndex = jssInsertedData.findIndex(object => {
                    return object.employeeName.toLowerCase() == 'total';
                });
                if (elementIndex >= 0) {
                    jssInsertedData.splice(elementIndex, 1);
                }

                
                var update_timeStampId = $("#timeStamp_ForUpdateData").val();
                
                insertMessage = "Successfully data inserted.";
                $.ajax({
                    url: `/api/utilities/ExcelAssignment/`,
                    contentType: 'application/json',
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    data: JSON.stringify({ ForecastUpdateHistoryDtos: jssInsertedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode, TimeStampId: update_timeStampId,IsUpdateInsertDelete:isUpdate }),
                    success: function (data) {                                              
                        isShowResults = true;

                        $("#timeStamp_ForUpdateData").val('');
                        var chat = $.connection.chatHub;
                        $.connection.hub.start();
                        // Start the connection.
                        $.connection.hub.start().done(function () {
                            chat.server.send('data has been inserted by ', userName);
                        });                            
                    }
                });
                jssInsertedData = [];
                newRowCount = 1;
            }

            if (insertedOnChangeList.length > 0) {                              
                var update_timeStampId = $("#timeStamp_ForUpdateData").val();
                insertMessage = "Successfully data inserted.";
                $.ajax({
                    url: `/api/utilities/ExcelAssignment/`,
                    contentType: 'application/json',
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    data: JSON.stringify({ ForecastUpdateHistoryDtos: insertedOnChangeList, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode, TimeStampId: update_timeStampId, IsUpdateInsertDelete: isUpdate }),
                    success: function (data) {
                        isShowResults = true;

                        $("#timeStamp_ForUpdateData").val('');
                        var chat = $.connection.chatHub;
                        $.connection.hub.start();
                        // Start the connection.
                        $.connection.hub.start().done(function () {
                            chat.server.send('data has been inserted by ', userName);
                        });
                    }
                });
                insertedOnChangeList = [];
                previousRowDataToDetech = [];
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
                        isShowResults  =true;
                    }
                });

                $("#timeStamp_ForUpdateData").val('');
                var chat = $.connection.chatHub;
                $.connection.hub.start();
                // Start the connection.
                $.connection.hub.start().done(function () {
                    chat.server.send('data has been deleted by ', userName);
                });                
                deletedExistingRowIds = [];
            }
        }else{
            var selected_forecast_year = $("#assignment_year_list").val();
            $.ajax({
                url: `/api/utilities/ExcelAssignment/`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssInsertedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode, TimeStampId: update_timeStampId,IsUpdateInsertDelete:isUpdate,Year:selected_forecast_year }),
                success: function (data) {       
                    isShowResults = true;

                    $("#timeStamp_ForUpdateData").val('');
                    var chat = $.connection.chatHub;
                    $.connection.hub.start();
                    // Start the connection.
                    $.connection.hub.start().done(function () {
                        chat.server.send('data has been inserted by ', userName);
                    });
                }
            });
            jssInsertedData = [];
            newRowCount = 1;
        }

        if(isShowResults){
            var year = $("#assignment_year_list").val();
            ShowForecastResults(year,'save');
        }else{
            LoaderHide();
        }
    }

    // if (updateMessage == "" && insertMessage == "" && deleteMessage == "" && timestampMessage =="") {       
    //     alert("変更されていないので、保存できません");       
    // }
    // else{   
    //     ToastMessageSuccess_Center('保存されました.');
    // }    
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
        $('#duplciateYear').val(parseInt(year)+1);
        $('#replicate_from').val(year);
    }else{
        $('#duplciateYear').val('');
        $('#replicate_from').val('');
    }
}

/*
    author: sudipto.
    replicate the budget data from previous year budget.
*/
function DuplicateForecast(){    
    var insertYear  = $('#duplciateYear').find(":selected").val();
    var copyYear = $('#replicate_from').find(":selected").val();

    if(copyYear!="" && insertYear!=""){
        $("#replicate_from_previous_year").modal("hide");
        $("#loading").css("display", "block");
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

/*
    author: sudipto.
    budget validation and submit the import file to server.
*/
function validate(){
    var selectedYear = $('#select_import_year').find(":selected").val();
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
    author: sudipto.
    date:17July23.
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

    //for 5 column delete color:start  
    //db id color 
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.dbId.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.dbId.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.dbId.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.dbId.cellName + (rowNumber))).addClass('readonly');
    }
    //duplicateFrom color
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.duplicateFrom.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateFrom.cellName + (rowNumber))).addClass('readonly');
    }
    //duplicateCount
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.duplicateCount.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.duplicateCount.cellName + (rowNumber))).addClass('readonly');
    }
    // roleChanged    
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.roleChanged.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.roleChanged.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.roleChanged.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.roleChanged.cellName + (rowNumber))).addClass('readonly');
    }
    //unitPriceChanged color 
    if(requestType != "deleted"){
        $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.unitPriceChanged.cellName+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell(jssTableDefinition.unitPriceChanged.cellName + (rowNumber))).addClass('readonly');
    }
    //for 5 column delete color:end

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

    $(jss.getCell(jssTableDefinition.totalM.cellName + (rowNumber))).removeClass('readonly');
    jss.setStyle(jssTableDefinition.totalM.cellName+rowNumber,"background-color", backgroundColor);
    jss.setStyle(jssTableDefinition.totalM.cellName+rowNumber,"color", textColor);
    $(jss.getCell(jssTableDefinition.totalM.cellName + (rowNumber))).addClass('readonly');

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
    
    // $(jss.getCell(jssTableDefinition.mayT.cellName + (rowNumber))).removeClass('readonly');
    // jss.setStyle(jssTableDefinition.mayT.cellName+rowNumber,"background-color", backgroundColor);
    // jss.setStyle(jssTableDefinition.mayT.cellName+rowNumber,"color", textColor);
    // $(jss.getCell(jssTableDefinition.mayT.cellName + (rowNumber))).addClass('readonly');

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

    $(jss.getCell("AK" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AK"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AK"+rowNumber,"color", textColor);
    $(jss.getCell("AK" + (rowNumber))).addClass('readonly');
}

/*
    author: sudipto
    after cell change, store that cell information into hidden field.
*/
function CheckIfAlreadyExists(selectedCells,assignmentId){
    var previousChangedCells = $("#changed_cell_with_assignmentid").val();
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
	var previousChangedCells = $("#changed_cell_with_assignmentid").val();
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
    $("#changed_cell_with_assignmentid").val(changedCellStored);
}
function SetColorForCells(strBackgroundColor,strTextColor,CellPosition){
	jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
}
function SetColorForCostsCells(strBackgroundColor,strTextColor,CellPosition){
	$(jss.getCell(CellPosition)).removeClass('readonly');
    jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
    $(jss.getCell(CellPosition)).addClass('readonly');
}

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
    // var dept_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // dept_header.css('position', 'sticky');
    // dept_header.css('top', '0px');
    $('#search_department_asc').css('background-color', 'lightsteelblue');
    $('#search_department_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: inchrg
    var incharge_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(7)');
    incharge_asc_arow.addClass('arrow-down');
    // var incharge_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // incharge_header.css('position', 'sticky');
    // incharge_header.css('top', '0px');
    $('#search_incharge_asc').css('background-color', 'lightsteelblue');
    $('#search_incharge_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: role
    var role_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(8)');
    role_asc_arow.addClass('arrow-down');
    // var incharge_header = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_role.css('position', 'sticky');
    // jexcelFirstHeaderRow_role.css('top', '0px');
    $('#search_role_asc').css('background-color', 'lightsteelblue');
    $('#search_role_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: exp
    var explanation_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(9)');
    explanation_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_exp = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_exp.css('position', 'sticky');
    // jexcelFirstHeaderRow_exp.css('top', '0px');
    $('#search_explanation_asc').css('background-color', 'lightsteelblue');
    $('#search_explanation_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: com
    var company_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(10)');
    company_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_com = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_com.css('position', 'sticky');
    // jexcelFirstHeaderRow_com.css('top', '0px');
    $('#search_company_asc').css('background-color', 'lightsteelblue');
    $('#search_company_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: grade
    var grade_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(11)');
    grade_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_grade = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_grade.css('position', 'sticky');
    // jexcelFirstHeaderRow_grade.css('top', '0px');
    $('#search_grade_asc').css('background-color', 'lightsteelblue');
    $('#search_grade_desc').css('background-color', 'lightsteelblue');

    //sort arrow on load: unit
    var unit_asc_arow = $('.jexcel > thead > tr:nth-of-type(3) > td:nth-of-type(12)');
    unit_asc_arow.addClass('arrow-down');
    // var jexcelFirstHeaderRow_unit = $('.jexcel > thead > tr:nth-of-type(3) > td');
    // jexcelFirstHeaderRow_unit.css('position', 'sticky');
    // jexcelFirstHeaderRow_unit.css('top', '0px');
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